namespace Automation.Simple.Core.UI.Controls.Locators
{
    public abstract class BaseLocator
    {
        public BaseLocator(string locator)
        {
            Locator = locator;
        }

        public string Locator { get; private set; }
    }
}
