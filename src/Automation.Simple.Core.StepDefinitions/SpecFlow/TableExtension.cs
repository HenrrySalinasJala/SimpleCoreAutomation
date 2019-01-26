namespace Automation.Simple.Core.StepDefinitions.SpecFlow
{
    using System;
    using System.Linq;
    using TechTalk.SpecFlow;

    /// <summary>
    /// Spec flow Table Extensions.
    /// </summary>
    public static class TableExtension
    {
        /// <summary>
        /// Transforms the data table to a bi dimensional array.
        /// </summary>
        /// <param name="table">The data table.</param>
        /// <returns>Bi Dimensional array of strings.</returns>
        public static string[][] ToArray(this Table table)
        {
            try
            {
                string[][] arrayResult = new string[table.Rows.Count + 1][];

                for (int i = 0; i <= table.RowCount; i++)
                {
                    if (i == 0)
                    {
                        arrayResult[i] = table.Header.ToArray();
                    }
                    else
                    {
                        arrayResult[i] = table.Rows[i - 1].Values.ToArray();
                    }
                }

                return arrayResult;
            }
            catch (Exception error)
            {
                //throw new Exception($"Unable to Create Table strings {error.Message}");
                return new string[table.Rows.Count + 1][];
            }
        }
    }
}
