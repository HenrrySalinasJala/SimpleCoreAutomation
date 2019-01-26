namespace Automation.Simple.Core.UI.Controls.Checkbox
{
    using System;
    using OpenQA.Selenium;
    using Automation.Simple.Core.UI.Enums;
    using Automation.Simple.Core.UI.Controls.Browser;
    using OpenQA.Selenium.Support.UI;
    using Automation.Simple.Core.UI.Constants;

    /// <summary>
    /// Represents the checkbox web control.
    /// </summary>
    public class Checkbox : BaseControl
    {
        /// <summary>
        /// The locator of Checkbox input.
        /// </summary>
        private string _checkboxInputXPath = "descendant::input[@type = 'checkbox']";

        /// <summary>
        /// Initializes a new instance of the <see cref="Checkbox"/> class.
        /// </summary>
        /// <param name="controName">The control name.</param>
        /// <param name="searchCriteria">The search criteria.</param>
        /// <param name="timeout">The timeout in seconds.</param>
        public Checkbox(string controName, By searchCriteria, int timeout)
            : base(ControlType.Checkbox, controName, searchCriteria, timeout)
        {
        }


        /// <summary>
        /// The selenium control.
        /// </summary>
        public override IWebElement Control
        {
            get
            {
                //Waits for any angular request.
                BrowserExtension.WaitForAngular(Driver, TimeoutInSeconds);

                if (Locator != null)
                {
                    //waits until the element exits.
                    GetExplicitWait(TimeoutInSeconds).Until(
                        ExpectedConditions.ElementExists(Locator));

                    //waits until the element is visible.
                    GetExplicitWait(TimeoutInSeconds).Until(
                        ExpectedConditions.ElementIsVisible(Locator));

                    //Waits for any angular request.
                    BrowserExtension.WaitForAngular(Driver, TimeoutInSeconds);

                    //returns the element.
                    _control = Driver.FindElement(Locator);
                }
                return _control;
            }
            set
            {
                _control = value;
            }
        }

        /// <summary>
        /// The checkbox input in the control
        /// </summary>
        public IWebElement CheckboxInput
        {
            get
            {
                if (DOMAttributes.InputTagName.Equals(Control.TagName))
                {
                    return Control;
                }
                return Control.FindElement(By.XPath(_checkboxInputXPath));
            }
        }

        /// <summary>
        /// Gets a value indicating whether or not the checkbox is enabled.
        /// </summary>
        public override bool IsEnabled()
        {
            bool enabled = !string.IsNullOrEmpty(CheckboxInput.GetAttribute(DOMAttributes.DisabledFieldAttributeName)) ? false : true;
            log.Info($"The '{Name}' {Type} is enabled: [{enabled}].");
            return enabled;
        }

        /// <summary>
        /// Checks the control.
        /// </summary>
        public void Check()
        {
            try
            {
                log.Info($"Check the '{Name}' {Type}.");
                if (!CheckboxInput.Selected)
                {
                    CheckboxInput.Click();
                }
            }
            catch (Exception e)
            {
                log.Error($"Unable to check the '{Name}' {Type}. Error [{e.Message}].");
                throw;
            }
        }

        /// <summary>
        /// Gets a value indicating whether or not the checkbox is checked.
        /// </summary>
        /// <returns>The boolean value.</returns>
        public bool IsChecked()
        {
            try
            {
                log.Info($"Get the 'selected' attribute from '{Name}' {Type}.");
                return CheckboxInput.Selected;
            }
            catch (Exception e)
            {
                log.Error($"Unable to retrieve the 'selected' attribute from '{Name}' {Type}. " +
                    $"Error [{e.Message}].");
                throw;
            }
        }

        /// <summary>
        /// Unchecks the control.
        /// </summary>
        public void Uncheck()
        {
            try
            {
                log.Info($"Uncheck the '{Name}' {Type}.");
                if (CheckboxInput.Selected)
                {
                    CheckboxInput.Click();
                }
            }
            catch (Exception e)
            {
                log.Error($"Unable to uncheck the '{Name}' {Type}. Error [{e.Message}].");
                throw;
            }
        }

        /// <summary>
        /// Checks/Unchecks the checkbox given a value true/false.
        /// </summary>
        /// <param name="value">The true/false value.</param>
        /// <exception cref="ArgumentException">If the value is different than true or false.</exception>
        public void FillValue(string value)
        {
            bool check;
            bool converted = bool.TryParse(value, out check);

            if (converted)
            {
                if (check)
                {
                    Check();
                }
                else
                {
                    Uncheck();
                }
            }
            else
            {
                throw new ArgumentException($"{value} is not a valid value. Try 'true' or 'false'");
            }
        }
    }
}
