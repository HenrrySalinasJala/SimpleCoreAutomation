namespace Automation.Simple.Core.StepDefinitions.ControlSteps
{
    using Automation.Simple.Core.UI.Actions.Control;
    using Automation.Simple.Core.UI.Actions.Enums;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using TechTalk.SpecFlow;

    [Binding]
    public class ControlActionVerificationSteps : BaseStepDefinition
    {
        protected ControlActionVerificationSteps(ScenarioContext scenarioContext, ControlAction controlAction) : base(scenarioContext, controlAction)
        {
        }

        /// <summary>
        /// Verifies that the web control value is correct or not.
        /// </summary>
        /// <param name="not">The assertion.</param>
        /// <param name="expectedValue">The expected value.</param>
        /// <param name="controlName">The control name.</param>
        /// <param name="frame">The container name.</param>
        [Then(@"(No |)Deberia ver '(.*?)' en(?: campo| combo-box| label| etiqueta|) (.*?)(?: en ([^']+?)|)(?: modal| form| section| panel| item| link|)")]
        public void VerifyControlValue(string not, string expectedValue, string controlName,
            string frame)
        {
            var actualValue = ControlAction.ExecuteFunction(controlName, ActionType.GetText,
                frame.ToString()).ToString();
            if (string.IsNullOrEmpty(not))
            {
                Assert.AreEqual(expectedValue.ToString(), actualValue,
                    $"Value '{expectedValue}' is not present in {controlName}.");
            }
            else
            {
                Assert.AreNotEqual(expectedValue.ToString(), actualValue,
                    $"Value '{expectedValue}' is present in {controlName}");
            }
        }

        [Then(@"I should( not|) see the following values in ([^']+?)(?: grid|)(?: on ([^']+?)|)(?: modal| form| section|)")]
        public void TableHasTheFollowingValues(string not, string controlName, string containerName,
            Dictionary<string, string> dataTable)
        {
            CompareGridValues(not, null, controlName, containerName);
        }

        /// <summary>
        /// Compares the expected values on the transaction table.
        /// </summary>
        /// <param name="not">The value to check to assert true or false</param>
        /// <param name="expectedTransactions">
        /// List&lt;Dictionary&lt;string, string&gt;&gt; represents the expected values in the transaction table.
        /// </param>
        /// <param name="controlName">string parameter represents the control name.</param>
        /// <param name="containerName">The web control's container name</param>
        private void CompareGridValues(string not, List<Dictionary<string, string>> expectedTransactions,
            string controlName, string containerName)
        {
            try
            {
                if (string.IsNullOrEmpty(containerName))
                {
                    containerName = "pagina";
                }
                for (int i = 0; i < expectedTransactions.Count; i++)
                {
                    foreach (var transaction in expectedTransactions[i])
                    {
                        if (!"[skip]".Equals(transaction.Value.ToLower()))
                        {
                            var expectedValue = expectedTransactions[i][transaction.Key];
                            bool cellExists = (bool)ControlAction.ExecuteFunction(controlName, ActionType.Exist,
                                containerName, i, transaction.Key, expectedValue);
                            if (string.IsNullOrEmpty(not))
                            {
                                Assert.True(cellExists, "The grid does not contain the value: " + expectedValue);
                            }
                            else
                            {
                                Assert.False(cellExists, "The grid contains the value: " + expectedValue);
                            }
                        }
                    }
                }
            }
            catch (Exception error)
            {
                throw new Exception(string.Format("Unable to search in {0} grid. Error: {1}", controlName, error.Message));
            }
        }
    }
}
