namespace Automation.Simple.Core.UI.Controls.TextField
{
    using OpenQA.Selenium;
    using System;
    using Automation.Simple.Core.UI.Enums;
    using Automation.Simple.Core.UI.Constants;
    using Automation.Simple.Core.UI.Exceptions;

    /// <summary>
    /// Represents the Text Area field web control.
    /// </summary>
    public class TextAreaField : BaseControl, ITextField
    {
        /// <summary>
        /// The xpath of text area 
        /// </summary>
        private string _textAreaInputXpath  = "descendant::select";

        public TextAreaField(string controlName, By searchCriteria, int timeout)
            : base(ControlType.TextAreaField, controlName, searchCriteria, timeout)
        {
        }

        /// <summary>
        /// The text area field inside the control.
        /// </summary>
        public IWebElement TextAreaInput
        {
            get
            {
                if (DOMAttributes.TextAreaTagName.Equals(Control.TagName))
                {
                    return Control;
                }
                return Control.FindElement(By.XPath(_textAreaInputXpath));
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
                TextAreaInput.Clear();
            }
            catch (Exception error)
            {
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
                var text = TextAreaInput.GetAttribute(DOMAttributes.ValueAttribute);
                log.Info($"Text retrieved from '{Name}' {Type}: [{text}].");
                return text.Trim();
            }
            catch (Exception error)
            {
                throw new ControlActionExecutionException(Name, Type, error.Message);
            }
        }

        /// <summary>
        /// 
        /// Presses the TAB key into the control.
        /// </summary>
        public void PressTab()
        {
            try
            {
                log.Info($"Press TAB key in '{Name}' {Type}.");
                TextAreaInput.SendKeys(Keys.Tab);
            }
            catch (Exception error)
            {
                throw new ControlActionExecutionException(Name, Type, error.Message);
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
                TextAreaInput.Clear();
                TextAreaInput.SendKeys(text);
            }
            catch (Exception error)
            {
                throw new ControlActionExecutionException(Name, Type, error.Message);
            }
        }
    }
}