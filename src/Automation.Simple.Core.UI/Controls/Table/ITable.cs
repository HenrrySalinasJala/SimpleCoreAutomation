namespace Automation.Simple.Core.UI.Controls.Table
{
    
    using System.Collections.Generic;

    public interface ITable
    {
        ///// <summary>
        ///// Gets the header text of the table.
        ///// </summary>
        ///// <returns>The header.</returns>
        //IReadOnlyCollection<string> GetHeader();

        ///// <summary>
        ///// Gets the cell value as text.
        ///// </summary>
        ///// <param name="rowIndex">The row index.</param>
        ///// <param name="columnIndex">The column index.</param>
        ///// <returns>The cell value.</returns>
        //string GetCell(int rowIndex, int columnIndex);

        ///// <summary>
        ///// Gets the table text.
        ///// </summary>
        ///// <returns>The table.</returns>
        //IReadOnlyCollection<IReadOnlyCollection<string>> GetTable();

        ///// <summary>
        ///// Gets a row.
        ///// </summary>
        ///// <param name="rowIndex">The row index.</param>
        ///// <returns>The row.</returns>
        //IReadOnlyCollection<string> GetRow(int rowIndex);

        ///// <summary>
        ///// Clicks on specific cell.
        ///// </summary>
        ///// <param name="rowIndex">The row index.</param>
        ///// <param name="columnIndex">The column index.</param>
        //void ClickOn(int rowIndex, int columnIndex);

        ///// <summary>
        ///// Sets the table values.
        ///// </summary>
        ///// <param name="values">The values.</param>
        //void SetTable(List<List<string>> values);

        ///// <summary>
        ///// Sets the table values.
        ///// </summary>
        ///// <param name="values">The values.</param>
        //void SetTable(List<Dictionary<string, string>> values);

        ///// <summary>
        ///// Sets a specific row.
        ///// </summary>
        ///// <param name="rowIndex">The row index.</param>
        ///// <param name="values">The values.</param>
        //void SetRow(int rowIndex, List<string> values);

        ///// <summary>
        ///// Sets a specific cell.
        ///// </summary>
        ///// <param name="rowIndex">The row index.</param>
        ///// <param name="columnIndex">The column index.</param>
        ///// <param name="value">The value.</param>
        //void SetCell(int rowIndex, int columnIndex, string value);

        /// <summary>
        /// Gets a value indicating if the cell exists in the control.
        /// </summary>
        /// <param name="cell">The cell value </param>
        /// <returns>The boolean  value.</returns>
        bool Exists(List<Dictionary<string, string>> expectedTransactions);
    }
}
