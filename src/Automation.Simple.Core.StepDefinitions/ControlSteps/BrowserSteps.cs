using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation.Simple.Core.UI.Actions.Control;
using TechTalk.SpecFlow;

namespace Automation.Simple.Core.StepDefinitions.ControlSteps
{
    [Binding]
    public class BrowserSteps : BaseStepDefinition
    {
        protected BrowserSteps(ScenarioContext scenarioContext, ControlAction controlAction) : base(scenarioContext, controlAction)
        {
        }

        [Given(@"I go to '(.*?)'")]
        public void GivenIGoThePag(string url)
        {
            var browserActions = new BrowserActions();
            browserActions.GoTo(url);
        }

    }

}
