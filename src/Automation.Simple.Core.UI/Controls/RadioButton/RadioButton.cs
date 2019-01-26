namespace Abila.MIP.AT.UI.Controls.RadioButton
{
    using System;
    using OpenQA.Selenium;
    using Automation.Simple.Core.UI.Controls;
    using Automation.Simple.Core.UI.Enums;
    using Automation.Simple.Core.UI.Exceptions;

    /// <summary>
    /// Radio Button Control.
    /// </summary>
    public class RadioButton : BaseControl
    {
        /// <summary>
        /// Initializes a new instance of RadioButton.
        /// </summary>
        /// <param name="controlName">The control name.</param>
        /// <param name="searchCriteria">The search criteria.</param>
        /// <param name="timeout">The timeout in seconds.</param>
        public RadioButton(string controlName, By searchCriteria, int timeout)
            : base(ControlType.RadioButton, controlName, searchCriteria, timeout)
        {
        }

        /// <summary>
        /// Selects the Radio Button
        /// </summary>
        public void Click()
        {
            try
            {
                log.Info(string.Format("Select {0} radio button.", Name));
                Control.Click();
            }
            catch (Exception error)
            {
                log.Error($"Unable to select the '{Name}' {Type}. Error [{error.Message}].");
                throw new ControlActionExecutionException(Name, Type, error.Message); ; ;
            }
        }

        /// <summary>
        /// Returns true if the radio button is select otherwise false.
        /// </summary>
        /// <returns>Boolean value, true if the radio button is selected otherwise false</returns>
        public bool IsSelected()
        {
            try
            {
                log.Info($"Get the 'selected' attribute from '{Name}' {Type}.");
                return Control.Selected;
            }
            catch (Exception error)
            {
                log.Error($"Unable to retrieve the 'selected' attribute from '{Name}' {Type}. Error [{error.Message}].");
                throw new ControlActionExecutionException(Name, Type, error.Message); ; ;
            }
        }
    }
}
