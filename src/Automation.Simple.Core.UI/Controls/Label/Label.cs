

namespace Automation.Simple.Core.UI.Controls.Label
{
    using System;
    using Automation.Simple.Core.UI.Controls.Enums;
    using OpenQA.Selenium;

    public class Label : BaseControl, ILabel
    {
        public Label(string controlName, By searchCriteria, int timeout) 
            : base(ControlType.Label, controlName, searchCriteria, timeout)
        {
        }
        /// <summary>
        /// Gets the text displayed on the control.
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
