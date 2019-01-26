namespace Automation.Simple.Core.UI.Controls.Table
{
    using Automation.Simple.Core.UI.Constants;
    using Automation.Simple.Core.UI.Enums;
    using Automation.Simple.Core.UI.Exceptions;
    using Force.DeepCloner;
    using OpenQA.Selenium;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents the table web control.
    /// </summary>
    public class Table : BaseControl, ITable
    {
        /// <summary>
        /// The header XPath.
        /// </summary>
        private const string HeaderXPath = "descendant::thead";

        /// <summary>
        /// The body XPath.
        /// </summary>
        private const string BodyXPath = "descendant::tbody";

        /// <summary>
        /// The row tag name.
        /// </summary>
        private const string RowTagName = "tr";

        /// <summary>
        /// The cell tag name.
        /// </summary>
        private const string CellsLocator = "descendant::td[not(contains(@style,'display:none') or contains(@style,'display: none'))]";

        /// <summary>
        /// The headers tag name.
        /// </summary>
        private const string HeadersLocator = "descendant::th[not(contains(@style,'display:none') or contains(@style,'display: none'))]";

        /// <summary>
        /// The HTML tag added when change a cell value of a table
        /// * It's part of Kendo UI
        /// </summary>
        private const string HtmlTagForEditedCell = "<span class=\"k-dirty\"></span>";

        /// <summary>
        /// String constant, represents the XPath locator for table links.
        /// </summary>
        private const string TableLinkLocator = "descendant::a[text()='{0}']";

        /// <summary>
        /// String constant, represents the XPath locator for finding a cell with the given text.
        /// </summary>
        private const string TableCellTextLocator = "//td//*[text()='{0}']";

        public Table(string controlName, By searchCriteria, int timeout)
            : base(ControlType.Table, controlName, searchCriteria, timeout)
        {
        }

        /// <summary>
        /// The table header.
        /// </summary>
        private IWebElement Header
        {
            get
            {
                log.Info("getting table header");
                return Control.FindElement(By.XPath(HeaderXPath));
            }
        }

        /// <summary>
        /// The table body.
        /// </summary>
        private IWebElement Body
        {
            get
            {
                log.Info("getting table body");
                return Control.FindElement(By.XPath(BodyXPath));
            }
        }

        /// <summary>
        /// The table rows.
        /// </summary>
        private IReadOnlyCollection<IWebElement> Rows
        {
            get
            {
                log.Info("getting table rows");
                IReadOnlyCollection<IWebElement> rows = new List<IWebElement>();
                try
                {
                    if (!string.IsNullOrEmpty(Body.GetAttribute(DOMAttributes.InnerHtmlAttribute)))
                    {
                        rows = Body.FindElements(By.TagName(RowTagName));
                    }
                    return rows;
                }
                catch (Exception error)
                {
                    log.Error("error getting rows");
                    throw new Exception($"Unable to get table rows {error.Message}");
                }
            }
        }

        /// <summary>
        /// The table cells.
        /// </summary>
        private IReadOnlyCollection<IReadOnlyCollection<IWebElement>> Cells
        {
            get
            {
                log.Info("getting cells");
                var cells = new List<IReadOnlyCollection<IWebElement>>();
                try
                {
                    var tableRows = Rows.DeepClone();
                    foreach (var row in tableRows)
                    {
                        if (!row.GetAttribute(DOMAttributes.InnerHtmlAttribute).Contains("</th>"))
                        {
                            cells.Add(row.FindElements(By.XPath(CellsLocator)));
                        }
                    }
                    return cells;
                }
                catch (Exception error)
                {
                    log.Error("error getting cells");
                    throw new Exception($"Unable to get table cells {error.Message}");
                }
            }
        }

        /// <summary>
        /// Clicks on the cell given the row and column.
        /// </summary>
        /// <param name="rowIndex">The row index.</param>
        /// <param name="columnIndex">The column index.</param>
        public void ClickOn(int rowIndex, int columnIndex)
        {
            try
            {
                log.Info($"Click on the cell from '{Name}' {Type}, at the " +
                    $"row index '{rowIndex}' and column '{columnIndex}'.");
                Cells.ElementAt(rowIndex).ElementAt(columnIndex).Click();
            }
            catch (Exception error)
            {
                throw new ControlActionExecutionException(Name, Type, error.Message);
            }
        }

        /// <summary>
        /// Clicks on a link given the row, column and the link text.
        /// </summary>
        /// <param name="rowIndex">The row index.</param>
        /// <param name="columnIndex">The column index.</param>
        /// <param name="linkText">The link text.</param>
        public void ClickOnLink(int rowIndex, int columnIndex, string linkText)
        {
            try
            {
                log.Info($"Click on the link from '{Name}' {Type}, at the " +
                    $"row index '{rowIndex}' and column '{columnIndex}'.");
                Cells.ElementAt(rowIndex).ElementAt(columnIndex).FindElement(By.LinkText(linkText)).Click();
            }
            catch (Exception error)
            {
                throw new ControlActionExecutionException(Name, Type, error.Message);
            }
        }

        /// <summary>
        /// Clicks on a table link given the link text.
        /// </summary>
        /// <param name="rowIndex">int parameter, represents the number of row in which the link is located.</param>
        /// <param name="linkText">string parameter, represents the link text.</param>
        public void ClickOnLink(int rowIndex, string linkText)
        {
            try
            {
                log.Info($"Click on the link from '{Name}' {Type}, at the " +
                    $"row index '{(rowIndex - 1)}''.");
                var linkLocator = string.Format(TableLinkLocator, linkText);
                var rowElements = Rows.ToList();
                var linkElement = rowElements[--rowIndex].FindElement(By.XPath(linkLocator));
                Action.MoveToElement(linkElement).Click().Build().Perform();
            }
            catch (Exception error)
            {
                throw new ControlActionExecutionException(Name, Type, error.Message);
            }
        }

        /// <summary>
        /// Clicks on the cell with the given test.
        /// </summary>
        /// <param name="rowIndex">int parameter, represents the table row number.</param>
        /// <param name="cellText">string parameter, represents the table cell content.</param>
        public void ClickOnCellByTextContent(int rowIndex, string cellText)
        {
            try
            {
                log.Info($"Click on the cell from '{Name}' {Type}, at the " +
                                        $"row index '{(rowIndex - 1)}''.");
                var cellTextLocator = string.Format(TableCellTextLocator, cellText);
                var rowElements = Rows.ToList();
                var cellElement = rowElements[--rowIndex].FindElement(By.XPath(cellTextLocator));
                Action.MoveToElement(cellElement)
                      .Click()
                      .Build()
                      .Perform();
            }
            catch (Exception error)
            {
                throw new ControlActionExecutionException(Name, Type, error.Message);
            }
        }

        /// <summary>
        /// Gets the cell value given the row and column.
        /// </summary>
        /// <param name="rowIndex">The row index.</param>
        /// <param name="columnIndex">The column index.</param>
        /// <returns>The cell value.</returns>
        public string GetCell(int rowIndex, int columnIndex)
        {
            try
            {
                log.Info($"Get the cell value from '{Name}' {Type}, at the " +
                    $"row index '{rowIndex}' and column '{columnIndex}'.");
                return GetCell(Cells.ElementAt(rowIndex).ElementAt(columnIndex));
            }
            catch (Exception error)
            {
                throw new ControlActionExecutionException(Name, Type, error.Message);
            }
        }

        /// <summary>
        /// Gets the cell value given the row index and the column name.
        /// </summary>
        /// <param name="rowIndex">int parameter, represents The row index.</param>
        /// <param name="columnName">string parameter, represents the column name.</param>
        /// <returns>The cell value.</returns>
        public string GetCell(int rowIndex, string columnName)
        {
            try
            {
                log.Info($"Get the cell value from '{Name}' {Type}, at the " +
                    $"row index '{rowIndex}' and column '{columnName}'.");
                var columnIndex = GetColumnIndex(columnName);
                return GetCell(Cells.ElementAt(rowIndex).ElementAt(columnIndex));
            }
            catch (Exception error)
            {
                throw new ControlActionExecutionException(Name, Type, error.Message);
            }
        }

        /// <summary>
        /// Gets the text of a cell.
        /// </summary>
        /// <param name="cell">The cell as web element.</param>
        /// <returns>The text of the cell.</returns>
        private string GetCell(IWebElement cell)
        {
            string innerHTML = cell.GetAttribute("innerHTML");
            string value;

            //link
            if (innerHTML.Contains("</a>"))
            {
                var cellLinks = cell.FindElements(By.TagName(DOMAttributes.LinkTagName));
                var linkTextList = cellLinks.Select(link => link.GetAttribute(DOMAttributes.InnerHtmlAttribute));
                value = string.Join(", ", linkTextList.ToList());
            }
            //empty span
            else if (innerHTML.Contains(HtmlTagForEditedCell))
            {
                value = innerHTML.Replace(HtmlTagForEditedCell, string.Empty);
            }
            //checkbox
            else if (innerHTML.Contains("<input"))
            {
                var isChecked = cell.FindElement(By.TagName("input")).GetAttribute("checked");
                if (string.IsNullOrEmpty(isChecked) || innerHTML.Contains("</span>"))
                {
                    //span (for column header)
                    if (innerHTML.Contains("</span>"))
                    {
                        value = cell.FindElement(By.TagName("span")).GetAttribute("innerHTML");
                    }
                    //unchecked
                    else
                    {
                        value = "false";
                    }
                }
                //checked
                else
                {
                    value = "true";
                }
            }
            else if (innerHTML.Contains("</div>"))
            {
                value = cell.FindElement(By.TagName("div")).GetAttribute("innerHTML").Trim();
            }
            //default
            else
            {
                //hidden or blank space
                if (innerHTML.Equals("&nbsp;"))
                {
                    value = string.Empty;
                }
                else
                {
                    value = innerHTML;
                }
            }
            return value.Trim();
        }

        /// <summary>
        /// Gets the header values.
        /// </summary>
        /// <returns>The header values.</returns>
        public IReadOnlyCollection<string> GetHeader()
        {
            try
            {
                var headerValues = new List<string>();
                log.Info($"Get the header values from '{Name}' {Type}.");
                var headers = Header.FindElements(By.TagName(HeadersLocator));
                foreach (var header in headers)
                {
                    headerValues.Add(GetCell(header));
                }
                return headerValues;
            }
            catch (Exception error)
            {
                throw new ControlActionExecutionException(Name, Type, error.Message);
            }
        }

        /// <summary>
        /// Gets the row values.
        /// </summary>
        /// <param name="rowIndex">The row index.</param>
        /// <returns>The row values.</returns>
        public IReadOnlyCollection<string> GetRow(int rowIndex)
        {
            try
            {
                var rowValues = new List<string>();
                log.Info($"Get the row values from '{Name}' {Type}.");
                var row = Cells.ElementAt(rowIndex);
                foreach (var cell in row)
                {
                    rowValues.Add(GetCell(cell));
                }
                return rowValues;
            }
            catch (Exception error)
            {
                throw new ControlActionExecutionException(Name, Type, error.Message);
            }
        }

        /// <summary>
        /// Gets the value of the cells per row in the control.
        /// </summary>
        /// <returns>The value of the cells.</returns>
        public IReadOnlyCollection<IReadOnlyCollection<string>> GetTable()
        {
            try
            {
                var cellValues = new List<List<string>>();
                log.Info($"Get the value of the cells from '{Name}' {Type}.");
                var currentCells = Cells.DeepClone();
                foreach (var cellsPerRow in currentCells)
                {
                    var rowValues = new List<string>();
                    foreach (var cell in cellsPerRow)
                    {
                        rowValues.Add(GetCell(cell));
                    }
                    cellValues.Add(rowValues);
                }
                return cellValues;
            }
            catch (Exception error)
            {
                throw new ControlActionExecutionException(Name, Type, error.Message);
            }
        }

        public bool Exists(List<Dictionary<string, string>> expectedTransactions)
        {
            try
            {
                bool areContained = true;
                for (int i = 0; i < expectedTransactions.Count; i++)
                {
                    var exists = Exists(expectedTransactions[i]);
                    areContained &= exists;
                }
                return areContained;
            }
            catch (Exception error)
            {
                throw new Exception($"Unable to search in {Name} grid. Error: {error.Message}");
            }
        }

        public bool Exists(Dictionary<string, string> expectedTableValues)
        {
            try
            {
                bool areContained = false;

                var tableValues = GetTable();

                for (int rowCounter = 0; rowCounter < tableValues.Count; rowCounter++)
                {
                    var rowCells = tableValues.ElementAt(rowCounter);
                    bool isContained = ContainsAllItems(rowCells.ToList(), expectedTableValues.Values.ToList());
                    if (isContained)
                    {
                        return isContained;
                    }
                }
                return areContained;
            }
            catch (Exception error)
            {
                throw new ControlActionExecutionException(Name, Type, error.Message);
            }
        }

        public bool ContainsAllItems<T>(IEnumerable<T> a, IEnumerable<T> b)
        {
            return !b.Except(a).Any();
        }

        /// <summary>
        /// Gets a boolean indicating if a cell with text exists in the table
        /// </summary>
        /// <param name="rowIndex">
        /// The row index
        /// </param>
        /// <param name="columnName">
        /// The column name got from header table
        /// </param>
        /// <param name="cellText">
        /// The cell text got from table
        /// </param>
        /// <returns>
        /// The flag true if the text exists otherwise false
        /// </returns>
        public bool Exist(int rowIndex, string columnName, string cellText)
        {
            try
            {
                int columnIndex = GetColumnIndex(columnName);
                string actualCellText = GetCell(rowIndex, columnIndex);
                bool exist = actualCellText.Contains(cellText);
                log.Info($"Cell '{cellText}' in row '{rowIndex}' and column '{columnName}' " +
                    $"exists in '{Name}' {Type.ToString()}: [{exist}].");
                return exist;
            }
            catch (Exception error)
            {
                throw new ControlActionExecutionException(Name, Type, error.Message);
            }
        }

        ///// <summary>
        ///// Gets the column index based on the column name.
        ///// </summary>
        ///// <param name="columnName">The column name.</param>
        ///// <returns>The column index.</returns>
        private int GetColumnIndex(string columnName)
        {
            log.Info($"getting column name {columnName}");
            var headers = Header.FindElements(By.XPath(HeadersLocator));
            for (int index = 0; index < headers.Count; index++)
            {
                var headerValue = GetCell(headers.ElementAt(index)).Trim().ToLower();
                if (headerValue.Equals(columnName.ToLower()))
                    return index;
            }
            return -1;
        }

    }
}