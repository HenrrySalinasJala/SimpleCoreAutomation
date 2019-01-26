namespace Automation.Simple.Core.UI.Controls.Dropdown
{
    using Automation.Simple.Core.UI.Constants;
    using Automation.Simple.Core.UI.Controls.Browser;
    using Automation.Simple.Core.UI.Enums;
    using Automation.Simple.Core.UI.Exceptions;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;
    using System;

    public class Dropdown : BaseControl, IDropdown
    {
        /// <summary>
        /// The xpath of text area 
        /// </summary>
        private string _textAreaInputXpath = "descendant::textarea";

        /// <summary>
        /// Initializes a new instance of <see cref="Dropdown"/>.
        /// </summary>
        /// <param name="controlName">The control name.</param>
        /// <param name="searchCriteria">The search criteria.</param>
        /// <param name="timeout">The timeout in seconds.</param>
        public Dropdown(string controlName, By searchCriteria, int timeout)
            : base(ControlType.Dropdown, controlName, searchCriteria, timeout)
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="Dropdown"/>.
        /// </summary>
        /// <param name="controlType">The control type.</param>
        /// <param name="controlName">The control name.</param>
        /// <param name="searchCriteria">The search criteria.</param>
        /// <param name="timeout">The timeout in seconds.</param>
        public Dropdown(ControlType controlType, string controlName, By searchCriteria, int timeout)
            : base(controlType, controlName, searchCriteria, timeout)
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
                    BrowserExtension.WaitForAngular(Driver, TimeoutInSeconds);

                    //waits until the element exits.
                    GetExplicitWait(TimeoutInSeconds).Until(
                        ExpectedConditions.ElementExists(Locator));

                    //waits until the element is visible.
                    GetExplicitWait(TimeoutInSeconds).Until(
                        ExpectedConditions.ElementIsVisible(Locator));

                    //waits until the element is clickable.
                    GetExplicitWait(TimeoutInSeconds).Until(
                        ExpectedConditions.ElementToBeClickable(Locator));

                    //Waits for any angular request.
                    BrowserExtension.WaitForAngular(Driver, TimeoutInSeconds);

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
        /// The text area field inside the control.
        /// </summary>
        public SelectElement SelectElement
        {
            get
            {
                if (DOMAttributes.SelectTagName.Equals(Control.TagName))
                {
                    return new SelectElement(Control);
                }
                var controlElement = Control.FindElement(By.XPath(_textAreaInputXpath));
                return new SelectElement(Control);
            }
        }

        public string GetText()
        {
            try
            {

                var selectedItem = this.SelectElement.SelectedOption;
                log.Info($"Selected item is {selectedItem} in {Name} {Type}");
                return selectedItem.Text.Trim();
            }
            catch (Exception error)
            {
                throw new ControlActionExecutionException(Name, Type, error.Message);
            }
        }

        public void Select(string item)
        {
            try
            {
                log.Info($"Selecting {item} in {Name} {Type} ");
                SelectElement.SelectByText(item);
            }
            catch (Exception error)
            {
                throw new ControlActionExecutionException(Name, Type, error.Message);
            }
        }
    }
}
