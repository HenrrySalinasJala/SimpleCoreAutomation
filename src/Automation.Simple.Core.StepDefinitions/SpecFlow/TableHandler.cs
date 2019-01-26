namespace Automation.Simple.Core.StepDefinitions.SpecFlow
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TechTalk.SpecFlow;
    using System.Collections.ObjectModel;
    using Constants;
    using log4net;
    using Automation.Simple.Helpers.Extensions;
    using Automation.Simple.Core.StepDefinitions.DataTransformationTypes;

    /// <summary>
    /// The Table handler class contains methods for transforming the 
    /// data of the Data from the SpecFlow Tables to generic collections.
    /// </summary>
    public static class TableHandler
    {
        /// <summary>
        /// The logger instance.
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Field string value.
        /// </summary>
        private const string Field = "Field";

        /// <summary>
        /// Value string.
        /// </summary>
        private const string Value = "Value";

        /// <summary>
        /// Builds a transaction table based on given organization. 
        /// An empty string is set if the field value contains [empty].
        /// </summary>
        /// <param name="tableInput">Table input that contains informations for N organizations</param>
        /// <param name="organization">Organization to work with</param>
        /// <returns>Transaction Table</returns>
        public static List<Dictionary<string, string>> BuildTransactionTable(Table tableInput, string organization)
        {
            var transactionTable = new List<Dictionary<string, string>>();
            try
            {
                int numberOfRows = GetNumberOfRows(tableInput, organization);
                for (int rowIndex = 0; rowIndex < numberOfRows; rowIndex++)
                {
                    transactionTable.Add(new Dictionary<string, string>());
                    foreach (var row in tableInput.Rows.Where(row => !String.IsNullOrEmpty(row[organization])))
                    {
                        var cells = row[organization].Split(StringConstants.RowSeparator);
                        var numberOfCells = cells.Length;
                        if (numberOfCells > 0)
                        {
                            transactionTable[rowIndex]
                                .Add(row[0], numberOfCells <= rowIndex ? String.Empty : cells[rowIndex].ParseKeyword());
                        }
                    }
                }
                return transactionTable;
            }
            catch (Exception error)
            {
                var errorMessage = String.Format("[ERROR] TableHandler, There was an error " +
                                                  "trying to build transaction table. Error[{0}]", error.Message);
                log.Error(errorMessage, error);
                throw new Exception(errorMessage);
            }
        }

        /// <summary>
        /// Gets the number of rows for given transaction table, excludes the null entries.
        /// Throws a new exception when number of rows of populated values are not consistent (different number of rows).
        /// </summary>
        /// <param name="tableInput">Table input that contains information for an organizations</param>
        /// <param name="organization">Organization to work with</param>
        /// <returns>number of transaction rows </returns>
        private static int GetNumberOfRows(Table tableInput, string organization)
        {
            int baseRowLenght = -1;
            bool areTransactionRowsConsistent = true;

            foreach (var row in tableInput.Rows.Where(row =>  !String.IsNullOrEmpty(row[organization])))
            {
                var currentRowLength = row[organization].Split(StringConstants.RowSeparator).Count();
                if (baseRowLenght == -1)
                {
                    baseRowLenght = currentRowLength;
                }
                if (currentRowLength != baseRowLenght)
                {
                    areTransactionRowsConsistent = false;
                    log.Warn($"The field: {row[0]} is not consistent with values: {row[organization]}");
                }
                if (!areTransactionRowsConsistent)
                {
                    var errorMessage = "Given table input does not contain same number of rows";
                    log.Fatal(errorMessage);
                    throw new Exception(errorMessage);
                }
            }
            return baseRowLenght;
        }

        /// <summary>
        /// Gets the values set in the data table of the given column
        /// </summary>
        /// <param name="table">
        /// The data table
        /// </param>
        /// <param name="columnName">
        /// The column name
        /// </param>
        /// <returns>
        /// The collection with values
        /// </returns>
        public static ReadOnlyCollection<string> GetTableValues(Table table, string columnName)
        {
            if (!table.Header.Contains(columnName))
            {
                var errorMessage = $"[Error] TableHandler, The data table doesn't contain a value for the column: {columnName}.";
                log.Error(errorMessage);
                throw new Exception(errorMessage);
            }

            if (!table.Rows.Any())
            {
                var errorMessage = "[Error] TableHandler, The data table doesn't contain values in the first row.";
                log.Error(errorMessage);
                throw new Exception(errorMessage);
            }

            List<string> values = table.Rows.Select(row => row[columnName]).ToList();

            return new ReadOnlyCollection<string>(values);
        }

        /// <summary>
        /// Transforms the values of the list of dictionaries applying the given function.
        /// </summary>
        /// <param name="tableValues">List&lt;Dictionary&lt;string, string&gt;&gt; represents the values to transform</param>
        /// <param name="getTransformedValue">Func&lt;string, IStepInput&gt;  represents the function to apply</param>
        /// <returns>returns List&lt;Dictionary&lt;string, string&gt;&gt; </returns>
        public static List<Dictionary<string, string>> TransformTableValues(
            List<Dictionary<string, string>> tableValues,
            Func<string, IStepArgument> getTransformedValue)
        {
            var transformedValuesList = new List<Dictionary<string, string>>();
            tableValues.ForEach(dictionary => {
                var transformedValues = new Dictionary<string, string>();
                dictionary.ToList().ForEach(valuePair =>
                {
                    transformedValues.Add(valuePair.Key, getTransformedValue(valuePair.Value).ToString());
                });
                transformedValuesList.Add(transformedValues);
            });
            return transformedValuesList;
        }

        /// <summary>
        /// Gets the table values for a specific organization from the table data per organization list.
        /// </summary>
        /// <param name="tableDataPerOrgs">
        /// Dictionary&lt;string, Table&gt; tableDataPerOrgs parameter, represents the table values 
        /// for each organization.
        /// </param>
        /// <param name="orgName">string parameter, represents the organization name.</param>
        /// <returns></returns>
        public static Table GetTableDataByOrganization(Dictionary<string, Table> tableDataPerOrgs,
            string orgName)
        {
            Table tableData;
            tableDataPerOrgs.TryGetValue(orgName, out tableData);
            if (tableData == null)
            {
                var errorMessage = $"Unable to get table values for the organization {orgName}";
                log.Error(errorMessage);
                throw new Exception(errorMessage);
            }

            return tableData;
        }

        /// <summary>
        /// Normalize the Template Data Fields to match with Actual Field names.
        /// </summary>
        /// <param name="dictionary">
        /// Type parameter, represents the dictionary raw data.
        /// </param>
        /// <returns>
        /// Returns the template data with fields correctly named in Dictionary&lt;string, string&gt;.
        /// </returns>
        public static Dictionary<string, string> NormalizeTemplateFields(Dictionary<string, string> dictionary)
        {
            return dictionary
                .Select(entry => new KeyValuePair<string, string>(ParseField(entry.Key),
                    entry.Value))
                .ToDictionary(entry => entry.Key, entry => entry.Value);
        }

        /// <summary>
        /// Parse the field name to match UI form names.
        /// </summary>
        /// <param name="fieldName">The current field name.</param>
        /// <returns>The actual field name parsed.</returns>
        public static string ParseField(string fieldName)
        {
            const string CurrentName1099 = "Type 1099";
            const string ActualName1099 = "1099 Type";
            return fieldName.SplitCamelCaseWords().Replace(CurrentName1099, ActualName1099);
        }

        /// <summary>
        /// Iterates over the Table data values to insert them in a dictionary.
        /// </summary>
        /// <param name="table">
        /// Table parameter, represents the table to iterate.
        /// </param>
        /// <returns>
        /// Returns a Dictionary&lt;string, Table&gt; with the Table data.
        /// </returns>
        public static Dictionary<string, Table> GetInputData(Table table)
        {
            var data = new Dictionary<string, Table>();

            table.Header
                .Where(header => header != Field)
                .ToList()
                .ForEach(header => data.Add(header, GetOrganizationData(header, table)));

            return data;
        }

        /// <summary>
        /// Gets a Table instance with the given data by organization.
        /// </summary>
        /// <param name="organization">
        /// string parameter, represents the organization.
        /// </param>
        /// <param name="table">
        /// Table parameter, represents the data to split by organization.
        /// </param>
        /// <returns>
        /// Returns a Table instance with data by organization.
        /// </returns>
        private static Table GetOrganizationData(string organization, Table table)
        {
            var orgTable = new Table(Field, Value);

            int orgIndex = Array.IndexOf(table.Header.ToArray(), organization);

            foreach (var row in table.Rows)
            {
                orgTable.AddRow(row.First().Value, row[orgIndex]);
            }
            return orgTable;
        }
    }
}
