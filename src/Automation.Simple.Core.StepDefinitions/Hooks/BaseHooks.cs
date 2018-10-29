namespace Automation.Simple.Core.StepDefinitions.Hooks
{
    using Automation.Simple.Core.UI.Actions.Control;
    using TechTalk.SpecFlow;

    [Binding]
    public class BaseHooks : BaseStepDefinition
    {
        protected BaseHooks(ScenarioContext scenarioContext, ControlAction controlAction, BrowserActions browserActions) : base(scenarioContext, controlAction, browserActions)
        {
        }

        /// <summary>
        /// After feature hook.
        /// </summary>
        [AfterFeature]
        public static void DefaultAfterFeature( BrowserActions browserActions)
        {
            browserActions.CloseBrowser();
        }
    }
}
