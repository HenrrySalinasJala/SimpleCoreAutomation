namespace Automation.Simple.Core.UI.Controls.Table
{
    using Automation.Simple.Core.UI.Constants;
    using Automation.Simple.Core.UI.Controls.Browser;
    using Automation.Simple.Core.UI.Enums;
    using Automation.Simple.Core.UI.Exceptions;
    //using Checkbox;
    //using Commons;
    using Link;
    using OpenQA.Selenium;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    //using Utilities;

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
                IReadOnlyCollection<IWebElement> rows = new List<IWebElement>();
                if (!string.IsNullOrEmpty(Body.GetAttribute(DOMAttributes.InnerHtmlAttribute)))
                {
                    rows = Body.FindElements(By.TagName(RowTagName));
                }
                return rows;
            }
        }

        /// <summary>
        /// The table cells.
        /// </summary>
        private IReadOnlyCollection<IReadOnlyCollection<IWebElement>> Cells
        {
            get
            {
                var cells = new List<IReadOnlyCollection<IWebElement>>();
                foreach (var row in Rows)
                {
                    cells.Add(row.FindElements(By.XPath(CellsLocator)));
                }
                return cells;
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
                value = cell.FindElement(By.TagName("div")).GetAttribute("innerHTML");
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
            return value;
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
                foreach (var cellsPerRow in Cells)
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
                    foreach (var transaction in expectedTransactions[i])
                    {
                        if (!"[skip]".Equals(transaction.Value.ToLower()))
                        {
                            var expectedValue = expectedTransactions[i][transaction.Key];
                            bool cellExists = Exist(i, transaction.Key, expectedValue);
                            areContained &= cellExists;
                        }
                    }
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
                throw new Exception($"Unable to search in {Name} grid. Error: {error.Message}");
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



        /// <summary>
        /// Validates if the table contains a cell with disabled value.
        /// </summary>
        /// <param name="rowIndex">The table row index.</param>
        /// <param name="columnName">The table column name.</param>
        /// <param name="cellText">The Table cell content.</param>
        /// <returns>True if the cell content is enabled otherwise false.</returns>
        //public bool IsEnabled(int rowIndex, string columnName, string cellText)
        //{
        //    var columnIndex = GetColumnIndex(columnName);
        //    var cell = Cells.ElementAt(rowIndex).ElementAt(columnIndex);
        //    bool isEnabled = IsCellEnabled(cell, columnName.ToLower(), cellText);

        //    log.Info($"Element '{cellText}' in {Name} table: {rowIndex} row and {columnName} column " +
        //        $"enabled status is: {isEnabled}.");

        //    return isEnabled;
        //}

        ///// <summary>
        ///// Checks if the Table cell is enabled.
        ///// </summary>
        ///// <param name="cell">IWebElement parameter, represents the cell element.</param>
        ///// <param name="columnName">The table column name.</param>
        ///// <param name="cellText">The Table cell content.</param>
        ///// <returns>True if the cell element is enabled otherwise false.</returns>
        ///// <exception cref="Exception">
        ///// throws an exception if it is not possible to check if the cell is enabled.
        ///// </exception>
        //private bool IsCellEnabled(IWebElement cell, string columnName, string cellText)
        //{
        //    switch (columnName)
        //    {
        //        case "action":
        //        case "":
        //            var ActionLinks = cell.FindElements(By.TagName(DOMAttributes.LinkTagName));
        //            var linkElement = ActionLinks.ToList().Find(link => link.Text.Contains(cellText));
        //            var linkControl = new Link(cellText, linkElement, TimeoutInSeconds);
        //            return linkControl.IsEnabled();

        //        case "select":
        //            var checkBoxControl = new Checkbox(columnName, cell, TimeoutInSeconds);
        //            return checkBoxControl.IsEnabled();

        //        default:
        //            throw new Exception(string.Format("Unable to check if the cell with value {0} " +
        //                                            "in column {1} is enabled", cellText, columnName));
        //    }
        //}

        ///// <summary>
        ///// Gets the cell's type.
        ///// </summary>
        ///// <param name="cell">The cell.</param>
        ///// <returns>The cell type.</returns>
        //private CellType GetCellType(IWebElement cell)
        //{
        //    try
        //    {
        //        var innerHTML = cell.GetAttribute("innerHTML");

        //        if (innerHTML.Contains("</a>"))
        //            return CellType.Link;
        //        else
        //        {
        //            BrowserMngr.Instance.GetActions().MoveToElement(cell).Click().Build().Perform();
        //            BrowserExtension.WaitForAngular(TimeoutInSeconds);

        //            innerHTML = cell.GetAttribute("innerHTML");

        //            if (innerHTML.Contains("<input"))
        //            {
        //                var inputField = cell.FindElements(By.XPath("descendant::input")).Last();
        //                var dataRole = inputField.GetAttribute("data-role");
        //                var textType = inputField.GetAttribute("type");

        //                if (!string.IsNullOrEmpty(dataRole))
        //                    switch (dataRole)
        //                    {
        //                        case "combobox":
        //                            return CellType.TextLookup;

        //                        case "datepicker":
        //                            return CellType.DateField;

        //                        case "numerictextbox":
        //                            return CellType.NumericField;

        //                        case "madcalculatorinput":
        //                            return CellType.CurrencyField;

        //                        default:
        //                            break;
        //                    }
        //                else if (!string.IsNullOrEmpty(textType))
        //                    switch (textType)
        //                    {
        //                        case "checkbox":
        //                            return CellType.Checkbox;

        //                        case "text":
        //                            return CellType.TextField;

        //                        default:
        //                            break;
        //                    }
        //            }
        //        }
        //        return CellType.NoEditable;
        //    }
        //    catch (Exception e)
        //    {
        //        log.Error("Unable to get cell type from '{0}' {1}. Error [{2}].", Name,
        //            Type.ToString(), e.Message);

        //        return CellType.NoEditable;
        //    }
        //}

        ///// <summary>
        ///// Sets a value to a cell.
        ///// </summary>
        ///// <param name="cell">The cell control.</param>
        ///// <param name="value">The cell value.</param>
        //private void SetCell(IWebElement cell, string value)
        //{
        //    var cellType = GetCellType(cell);
        //    BrowserExtension.WaitForAngular(TimeoutInSeconds);

        //    switch (cellType)
        //    {
        //        case CellType.TextField:
        //            SetTextField(cell, value);
        //            break;

        //        case CellType.TextLookup:
        //            SetTextLookup(cell, value);
        //            break;

        //        case CellType.DateField:
        //            SetDateField(cell, value);
        //            break;

        //        case CellType.CurrencyField:
        //            SetCurrencyField(cell, value);
        //            break;

        //        case CellType.Checkbox:
        //            SetCheckbox(cell, value);
        //            break;

        //        case CellType.NumericField:
        //            SetNumericField(cell, value);
        //            break;

        //        default:
        //            break;
        //    }

        //    BrowserExtension.WaitForAngular(TimeoutInSeconds);
        //}

        ///// <summary>
        ///// Sets a cell of text field type.
        ///// </summary>
        ///// <param name="cell">The cell.</param>
        ///// <param name="value">The value.</param>
        //private void SetTextField(IWebElement cell, string value)
        //{
        //    var textFieldXPath = "input";
        //    var inputField = cell.FindElement(By.XPath(textFieldXPath));
        //    inputField.Clear();
        //    inputField.SendKeys(value);
        //    inputField.SendKeys(Keys.F2);
        //}

        ///// <summary>
        ///// Sets a cell of numeric field type.
        ///// </summary>
        ///// <param name="cell">The cell.</param>
        ///// <param name="value">The value.</param>
        //private void SetNumericField(IWebElement cell, string value)
        //{
        //    var numericXPath = "descendant::input[@data-role='numerictextbox']";
        //    var inputField = cell.FindElement(By.XPath(numericXPath));
        //    inputField.Clear();
        //    inputField.SendKeys(value);
        //    inputField.SendKeys(Keys.F2);
        //}

        ///// <summary>
        ///// Sets a cell of text lookup type.
        ///// </summary>
        ///// <param name="cell">The cell.</param>
        ///// <param name="value">The value.</param>
        //private void SetTextLookup(IWebElement cell, string value)
        //{
        //    var textLookupXPath = "descendant::input[@role='combobox']";
        //    var inputField = cell.FindElement(By.XPath(textLookupXPath));
        //    inputField.Clear();
        //    inputField.SendKeys(value);
        //    inputField.SendKeys(Keys.F2);
        //}

        ///// <summary>
        ///// Sets a cell of checkbox type.
        ///// </summary>
        ///// <param name="cell">The cell.</param>
        ///// <param name="value">The value.</param>
        //private void SetCheckbox(IWebElement cell, string value)
        //{
        //    var checkboxXPath = "descendant::input[@type='checkbox']";
        //    var inputField = cell.FindElement(By.XPath(checkboxXPath));
        //    bool check;
        //    bool converted = bool.TryParse(value, out check);

        //    if (converted)
        //    {
        //        if (!inputField.Selected.Equals(check))
        //            inputField.Click();
        //    }
        //    else
        //    {
        //        throw new Exception(string.Format("{0} is not a valid value. Try 'true' or 'false'", value));
        //    }
        //}

        ///// <summary>
        ///// Sets a cell of currency field type.
        ///// </summary>
        ///// <param name="cell">The cell.</param>
        ///// <param name="value">The value.</param>
        //private void SetCurrencyField(IWebElement cell, string value)
        //{
        //    var currencyFieldXPath = "descendant::input[@data-role='madcalculatorinput']";
        //    var inputField = cell.FindElement(By.XPath(currencyFieldXPath));
        //    inputField.Clear();
        //    inputField.SendKeys(value);
        //    inputField.SendKeys(Keys.F2);
        //}

        ///// <summary>
        ///// Sets a cell of date field type.
        ///// </summary>
        ///// <param name="cell">The cell.</param>
        ///// <param name="value">The value.</param>
        //private void SetDateField(IWebElement cell, string value)
        //{
        //    var dateFieldXPath = "descendant::input[@data-role='datepicker']";
        //    var inputField = cell.FindElement(By.XPath(dateFieldXPath));
        //    inputField.Clear();
        //    inputField.SendKeys(value);
        //    inputField.SendKeys(Keys.F2);
        //}

        ///// <summary>
        ///// Gets the column index based on the column name.
        ///// </summary>
        ///// <param name="columnName">The column name.</param>
        ///// <returns>The column index.</returns>
        private int GetColumnIndex(string columnName)
        {
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