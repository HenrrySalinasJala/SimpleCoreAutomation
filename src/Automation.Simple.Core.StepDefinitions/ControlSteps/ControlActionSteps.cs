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
        /// <param name="containerName">The container name.</param>
        [Given(@"I click ([^']+?)(?: button| link| field| combo-box| drop-down list| radio-button|)(?: on ([^']+?)|)(?: modal| form| section| menu| card| panel|)")]
        [When(@"I click ([^']+?)(?: button| link| field| combo-box| drop-down list| radio-button|)(?: on ([^']+?)|)(?: modal| form| section| menu| card| panel|)")]
        public void Click(string controlName, string containerName)
        {
            ControlAction.Execute(controlName, ActionType.Click, containerName.ToString());
        }

    }
}
