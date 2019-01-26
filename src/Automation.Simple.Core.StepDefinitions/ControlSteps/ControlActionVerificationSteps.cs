namespace Automation.Simple.Core.StepDefinitions.ControlSteps
{
    using Automation.Simple.Core.StepDefinitions.DataTransformationTypes;
    using Automation.Simple.Core.UI.Actions.Control;
    using Automation.Simple.Core.UI.Actions.Enums;
    using NUnit.Framework;
    using System;
    using TechTalk.SpecFlow;

    [Binding]
    public class ControlActionVerificationSteps : BaseStepDefinition
    {
        protected ControlActionVerificationSteps(ScenarioContext scenarioContext, ControlActions controlAction) : base(scenarioContext, controlAction)
        {
        }

        /// <summary>
        /// Verifies that the web control value is correct or not.
        /// </summary>
        /// <param name="not">The assertion.</param>
        /// <param name="expectedValue">The expected value.</param>
        /// <param name="controlName">The control name.</param>
        /// <param name="frame">The container name.</param>
        [Then(@"(?i)(No |)Deber(?:i|í)a ver '(.*?)' en(?: campo| drop-down| combo-box| bot(?:o|ó)n| modal| link| label| texto|) (.*?)(?: en ((?!(?:[^en].*en){1})[^']+?)|)(?: modal| formulario| secci(?:o|ó)n| panel| item| link|)(?-i)")]
        public void VerifyControlContainsValue(string not, IStepArgument expectedValue, string controlName,
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

        /// <summary>
        /// Verifies that the web control is or not displayed.
        /// </summary>
        /// <param name="not">The assertion.</param>
        /// <param name="controlName">The control name.</param>
        /// <param name="containerName">The container name.</param>
        [Then(@"(?i)(No |)Deber(?:i|í)a ver(?: campo| drop-down| combo-box| bot(?:o|ó)n| modal| link| label| texto|) (.*?) mostrado(?: en ((?!(?:[^en].*en){1})[^']+?)|)(?: modal| formulario| secci(?:o|ó)n| panel|)(?-i)")]
        public void VerifyControlIsDisplayed(string not, string controlName, string containerName)
        {
            if (string.IsNullOrEmpty(not))
            {
                Func<bool> isControlDisplayed = delegate ()
                {
                    return (bool)ControlAction.ExecuteFunction(controlName.ToString(),
                        ActionType.WaitForControlToBeDisplayed, containerName);
                };

                Assert.That(isControlDisplayed, $"Element '{controlName}' is not displayed.");
            }
            else
            {
                Func<bool> isNotDisplayed = delegate ()
                {
                    return (bool)ControlAction.ExecuteFunction(controlName.ToString(),
                    ActionType.WaitForControlToNotBeDisplayed, containerName); ;
                };
                Assert.That(isNotDisplayed, $"Unable to check if element '{controlName}' is not displayed.");
            }
        }
    }
}
