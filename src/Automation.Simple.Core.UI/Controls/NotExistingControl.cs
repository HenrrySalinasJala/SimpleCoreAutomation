namespace Automation.Simple.Core.UI.Controls
{
    using Automation.Simple.Core.UI.Controls.Enums;
    using OpenQA.Selenium;


    public class NotExistingControl : BaseControl
    {
        public NotExistingControl(string controlName, By searchCriteria, int timeout)
            : base(ControlType.NotExistingControl, controlName, searchCriteria, timeout)
        {
        }

        /// <summary>
        /// Gets a value indicating whether or not this element is enabled.
        /// </summary>
        /// <returns>Since the control does not exist, returns always false.</returns>
        public override bool IsEnabled()
        {
            return false;
        }

        /// <summary>
        /// Gets a value indicating whether or not this element is displayed.
        /// </summary>
        /// <returns>Since the control does not exist, returns always false.</returns>
        public override bool IsDisplayed()
        {
            return false;
        }
    }
}
