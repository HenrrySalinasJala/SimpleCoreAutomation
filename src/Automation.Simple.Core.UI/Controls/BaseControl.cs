namespace Automation.Simple.Core.UI.Controls
{
    using Automation.Simple.Core.Environment;
    using Automation.Simple.Core.Selenium;
    using Automation.Simple.Core.UI.Controls.Browser;
    using Automation.Simple.Core.UI.Controls.Locators;
    using Automation.Simple.Core.UI.Controls.Table;
    using Automation.Simple.Core.UI.Enums;
    using Automation.Simple.Helpers;
    using log4net;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Interactions;
    using OpenQA.Selenium.Support.UI;
    using System;
    using System.Collections.Generic;

    public abstract class BaseControl : IWebControl
    {
        /// <summary>
        /// The logger instance.
        /// </summary>
        protected static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected IWebDriver Driver;

        protected WebDriverWait Wait;

        protected Actions Action;

        public string Name { get; private set; }

        /// <summary>
        /// The control type.
        /// </summary>
        public ControlType Type
        {
            get
            {
                return _type;
            }
        }

        protected By Locator { get; private set; }

        private ControlType _type;

        /// <summary>
        /// The selenium control timeout in seconds.
        /// </summary>
        public int TimeoutInSeconds { get; set; }

        /// <summary>
        /// The native control.
        /// </summary>
        protected IWebElement _control;


        public BaseControl(XPath locator)
        {
            Locator = By.XPath(locator.Locator);
            SetUpControl();
        }

        public BaseControl(ControlType type, string controlName, By searchCriteria, int timeout)
        {
            _type = type;
            Name = controlName;
            TimeoutInSeconds = timeout;
            Locator = searchCriteria;
            SetUpControl();
        }

        public BaseControl(ControlType type, string controlName, IWebElement control, int timeout)
        {
            _type = type;
            Name = controlName;
            TimeoutInSeconds = timeout;
            _control = control;
            SetUpControl();
        }
        private void SetUpControl()
        {
            
            _type = ControlType.NotExistingControl;
            Driver = DriverManager.GetInstance().GetDriver();
            Wait = DriverManager.GetInstance().GetWait();
            Action = Driver != null ? new Actions(Driver) : null;
        }


        /// <summary>
        /// The selenium control.
        /// </summary>
        public virtual IWebElement Control
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
        /// Gets a new instance of the web driver wait.
        /// </summary>
        /// <param name="timeoutInSeconds">The timeout in seconds.</param>
        /// <returns>The web driver wait.</returns>
        internal WebDriverWait GetExplicitWait(int timeoutInSeconds)
        {
            return new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutInSeconds));
        }

        /// <summary>
        /// Gets a value indicating whether or not this element is displayed.
        /// </summary>
        /// <returns>True if the control is displayed, false otherwise.</returns>
        public virtual bool IsDisplayed()
        {
            try
            {

                BrowserExtension.WaitForAngular(Driver, TimeoutInSeconds);
                var displayed = Driver.FindElement(Locator).Displayed;
                log.Debug($"The '{Name}' {Type.ToString()} is displayed: [{displayed}]");

                return displayed;
            }
            catch (Exception error)
            {
                log.Debug($"Unable to determine if the '{Name}' { Type.ToString()} is being displayed. Error [{ error.Message}].");
                return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether or not this element is enabled.
        /// </summary>
        public virtual bool IsEnabled()
        {
            try
            {
                var enabled = Control.Enabled;
                log.Info($"The '{Name}' {Type.ToString()} is enabled: [{enabled}].");
                return enabled;
            }
            catch (Exception error)
            {
                log.Error($"Unable to determine if the '{Name}' {Type.ToString()} is enabled. Error [{error.Message}].");
                return false;
            }
        }



        /// <summary>
        /// Gets a value indicating if the browser manager instance is null or not.
        /// </summary>
        public bool IsDriverNull
        {
            get { return Driver == null; }
        }
    }
}
