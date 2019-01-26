namespace Automation.Simple.Core.Test.Integration
{
    using Automation.Simple.Core.UI.Actions.Control;


    public abstract class BaseTest
    {
        protected string Frame = string.Empty;
        protected BrowserActions BrowserAction;
        protected ControlActions ControlAction;

        public BaseTest()
        {
            BrowserAction = new BrowserActions();
            ControlAction = new ControlActions();
        }


    }
}
