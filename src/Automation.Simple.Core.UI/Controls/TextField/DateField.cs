namespace Automation.Simple.Core.UI.Controls.TextField
{
    using Automation.Simple.Core.UI.Constants;
    using Automation.Simple.Core.UI.Enums;
    using Automation.Simple.Core.UI.Exceptions;
    using OpenQA.Selenium;
    using System;

    public class DateField : TextField, ITextField
    {
        public DateField(string controlName, By searchCriteria, int timeout)
            : base(ControlType.DateField, controlName, searchCriteria, timeout)
        {
        }

        /// <summary>
        /// Simulates typing text into the control.
        /// </summary>
        /// <param name="text">The text value.</param>
        public void SetText(string text)
        {
            try
            {
                log.Info($"Set the text '{text}' in '{Name}' {Type}.");
                Control.Clear();
                DateTime oDate = Convert.ToDateTime(text);
                var isoDate = oDate.ToString("dd-MM-yyyy");
                Control.SendKeys(isoDate);
            }
            catch (Exception error)
            {
                log.Error($"Unable to set the text in '{Name}' {Type}. Error [{error.Message}].");
                throw new ControlActionExecutionException(Name, Type, error.Message);
            }
        }

        /// <summary>
        /// Gets the text of the control.
        /// </summary>
        /// <returns>The text.</returns>
        public string GetText()
        {
            try
            {
                var text = Control.GetAttribute(DOMAttributes.ValueAttribute);
                DateTime oDate = Convert.ToDateTime(text);
                var formattedDate = oDate.ToString("dd/MM/yyyy");
                log.Info($"Text retrieved from '{Name}' {Type}: [{formattedDate}].");
                return formattedDate;
            }
            catch (Exception error)
            {
                log.Error($"Unable to get the text from '{Name}' {Type}. Error [{error.Message}].");
                throw new ControlActionExecutionException(Name, Type, error.Message);
            }
        }
    }
}
