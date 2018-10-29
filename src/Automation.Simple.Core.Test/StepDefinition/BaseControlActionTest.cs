using Automation.Simple.Core.Environment;
using Automation.Simple.Core.UI.Actions.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Simple.Core.Test.StepDefinition
{
    public class BaseControlActionTest
    {
        public BrowserActions BrowserAction;
        private ControlAction ControlAction;


        public  BaseControlActionTest()
        {
            BrowserAction = new BrowserActions();
            ControlAction = new ControlAction();
            BrowserAction.GoTo(Config.WebAppUrl);
        }
    }
}
