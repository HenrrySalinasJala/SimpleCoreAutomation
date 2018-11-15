namespace Automation.Simple.Core.StepDefinitions.ControlSteps
{
    using Automation.Simple.Core.UI.Actions.Control;
    using Automation.Simple.Core.UI.Actions.Enums;
    using TechTalk.SpecFlow;

    [Binding]
    public class ControlActionSteps : BaseStepDefinition
    {
        public ControlActionSteps(ScenarioContext scenarioContext, ControlAction controlAction) 
            : base(scenarioContext)
        {
        }

        /// <summary>
        /// Click on a web control.
        /// </summary>
        /// <param name="controlName">The control name.</param>
        /// <param name="frame">The container name.</param>
        [Given(@"I click ([^']+?)(?: button| link| field| combo-box| drop-down list| radio-button|)(?: on ([^']+?)|)(?: modal| form| section| menu| card| panel|)")]
        [When(@"I click ([^']+?)(?: button| link| field| combo-box| drop-down list| radio-button|)(?: on ([^']+?)|)(?: modal| form| section| menu| card| panel|)")]
        public void Click(string controlName, string frame)
        {
            ControlAction.Execute(controlName, ActionType.Click, frame.ToString());
        }

        /// <summary>
        /// Fills a value into a field or combo-box.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="controlName">The control name.</param>
        /// <param name="containerName">The container name.</param>
        [Given(@"I fill '([^']+?)' in ([^']+?)(?: field| combo-box|)(?: on ([^']+?)|)(?: modal| form| section| panel|)")]
        [When(@"I fill '([^']+?)' in ([^']+?)(?: field| combo-box|)(?: on ([^']+?)|)(?: modal| form| section| panel|)")]
        public void SetTextOn(string value, string controlName, string containerName)
        {
            ControlAction.Execute(controlName, ActionType.SetText, containerName, value.ToString());
        }
    }
}
