namespace Automation.Simple.Core.UI.Controls.Table
{
    using System.Collections.Generic;

    public interface ITable
    {
        /// <summary>
        /// Gets a value indicating if the cell exists in the control.
        /// </summary>
        /// <param name="cell">The cell value </param>
        /// <returns>The boolean  value.</returns>
        bool Exists(List<Dictionary<string, string>> expectedTransactions);
    }
}
