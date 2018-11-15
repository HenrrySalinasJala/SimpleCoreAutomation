namespace Automation.Simple.Core.UI.Controls.Button
{
    using Automation.Simple.Core.UI.Enums;
    using OpenQA.Selenium;
    using System;

    public class Button : BaseControl, IButton
    {

        public Button(string controlName, By searchCriteria, int timeout)
            : base(ControlType.Button, controlName, searchCriteria, timeout)
        {
        }

        /// <summary>
        /// Clicks on the button.
        /// </summary>
        public void Click()
        {
            try
            {
                log.Info($"Click on '{Name}' {Type}.");
                Control.Click();
            }
            catch (Exception error)
            {
                log.Error($"Unable to click on '{Name}' {Type}. Error [{error.Message}].");
                throw;
            }
        }

        /// <summary>
        /// Gets the control text.
        /// </summary>
        /// <returns>The text.</returns>
        public string GetText()
        {
            try
            {
                log.Info($"Get text from '{Name}' {Type}.");
                string text = Control.Text;
                log.Debug($"Text retrieved from '{Name}' {Type}: [{text}].");
                return text;
            }
            catch (Exception error)
            {
                log.Error($"Unable to get the text from '{Name}' {Type}. Error [{error.Message}].");
                throw;
            }
        }
    }
}
