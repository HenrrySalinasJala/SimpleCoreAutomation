namespace Automation.Simple.Core.StepDefinitions.ControlSteps
{
    using Automation.Simple.Core.UI.Actions.Control;
    using Automation.Simple.Core.UI.Actions.Enums;
    using NUnit.Framework;
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
        [Then(@"I should( not|) see '(.*?)'(?: order|) in ((?!.*column).*?)(?: field| combo-box| label|)(?: on ([^']+?)|)(?: modal| form| section| panel| item| link|)")]
        public void IShouldSeeValueInControl(string not, string expectedValue, string controlName,
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
    }
}
