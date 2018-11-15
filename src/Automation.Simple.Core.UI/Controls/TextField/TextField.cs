namespace Automation.Simple.Core.UI.Controls.TextField
{
    using System;
    using OpenQA.Selenium;
    using Automation.Simple.Core.UI.Enums;
    using Automation.Simple.Core.UI.Controls.Browser;
    using OpenQA.Selenium.Support.UI;
    using Automation.Simple.Core.UI.Constants;

    /// <summary>
    /// Represents the text field web control.
    /// </summary>
    public class TextField : BaseControl, ITextField
    {
        public TextField(string controlName, By searchCriteria, int timeout)
            : base(ControlType.TextField, controlName, searchCriteria, timeout)
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
                BrowserExtension.WaitForAngular(Driver,TimeoutInSeconds);

                if (Locator != null)
                {
                    //waits until the element exits.
                    GetExplicitWait(TimeoutInSeconds).Until(
                        ExpectedConditions.ElementExists(Locator));

                    //waits until the element is visible.
                    GetExplicitWait(TimeoutInSeconds).Until(
                        ExpectedConditions.ElementIsVisible(Locator));

                    //Waits for any angular request.
                    BrowserExtension.WaitForAngular(Driver,TimeoutInSeconds);

                    //returns the element.
                    _control = Driver.FindElement(Locator);

                    if (!_control.TagName.Equals(DOMAttributes.InputTagName))
                    {
                        _control = _control.FindElement(By.TagName(DOMAttributes.InputTagName));
                    }
                }
                return _control;
            }
            set
            {
                _control = value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether or not this element is enabled.
        /// </summary>
        public override bool IsEnabled()
        {
            try
            {
                var enabled = string.IsNullOrEmpty(Control.GetAttribute(DOMAttributes.DisabledFieldAttributeName));
                log.Info($"The '{Name}' {Type} is enabled: [{enabled}].");
                return enabled;
            }
            catch (Exception error)
            {
                log.Error($"Unable to determine if the '{Name}' {Type} is enabled. Error [{error.Message}].");
                return false;
            }
        }

        /// <summary>
        /// Clears the content of the control.
        /// </summary>
        public void Clear()
        {
            try
            {
                log.Info($"Clear text in '{Name}' {Type}.");
                Control.Clear();
            }
            catch (Exception error)
            {
                log.Error($"Unable to clear the text in '{Name}' {Type}. Error [{error.Message}].");
                throw;
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
                log.Info($"Text retrieved from '{Name}' {Type}: [{text}].");
                return text;
            }
            catch (Exception error)
            {
                log.Error($"Unable to get the text from '{Name}' {Type}. Error [{error.Message}].");
                throw;
            }
        }

        /// <summary>
        /// Gets the placeholder text of the control.
        /// </summary>
        /// <returns>The text.</returns>
        public string GetPlaceholder()
        {
            try
            {
                var text = Control.GetAttribute(DOMAttributes.PlaceholderAttributeName);
                log.Info($"Placeholder text retrieved from '{Name}' {Type}: [{text}].");
                return text;
            }
            catch (Exception error)
            {
                log.Error($"Unable to get the placeholder text from '{Name}' {Type}. Error [{error.Message}].");
                throw;
            }
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
                Control.SendKeys(text);
            }
            catch (Exception error)
            {
                log.Error($"Unable to set the text in '{Name}' {Type}. Error [{error.Message}].");
                throw;
            }
        }

        /// <summary>
        /// Presses the TAB key into the control.
        /// </summary>
        public void PressTab()
        {
            try
            {
                log.Info($"Press TAB key in '{Name}' {Type}.");
                Control.SendKeys(Keys.Tab);
            }
            catch (Exception error)
            {
                log.Error($"Unable to press the TAB key in '{Name}' {Type}. Error [{error.Message}].");
                throw;
            }
        }

        /// <summary>
        /// Fills the given value within web control.
        /// </summary>
        /// <param name="value">Value to be populated</param>
        public void FillValue(string value)
        {
            SetText(value);
        }
    }
}
