namespace Automation.Simple.Core.StepDefinitions
{
    using Automation.Simple.Core.UI.Actions.Control;
    using log4net;
    using TechTalk.SpecFlow;


    public abstract class BaseStepDefinition : Steps
    {
        /// <summary>
        /// The logger instance.
        /// </summary>
        protected static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The current scenario context.
        /// </summary>
        protected ScenarioContext scenarioContext;

        /// <summary>
        /// The Feature Context.
        /// </summary>
        public FeatureContext featureContext;

        protected ControlAction ControlAction
        {
            get
            {
                return ScenarioContext.ScenarioContainer.Resolve<ControlAction>();
            }
        }

        protected BrowserActions BrowserAction
        {
            get
            {
                return ScenarioContext.ScenarioContainer.Resolve<BrowserActions>();
            }
        }


        /// <summary>
        /// The constructor of the <see cref="BaseStepDefinition" class./>
        /// </summary>
        /// <param name="scenarioContext">The current scenario context.</param>
        protected BaseStepDefinition(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }
        protected BaseStepDefinition(ScenarioContext scenarioContext, ControlAction controlAction)
        {
            this.scenarioContext = scenarioContext;
//            this.ControlAction = controlAction;
        }
        protected BaseStepDefinition(ScenarioContext scenarioContext, ControlAction controlAction,
            BrowserActions browserActions)
        {
            this.scenarioContext = scenarioContext;
            
        }

        /// <summary>
        /// Initializes a new instance of <see cref="BaseStep"/>.
        /// </summary>
        /// <param name="scenarioContext">The scenario context.</param>
        /// <param name="featureContext">The feature context.</param>
        protected BaseStepDefinition(ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            this.scenarioContext = scenarioContext;
            this.featureContext = featureContext;
        }

    }
}
