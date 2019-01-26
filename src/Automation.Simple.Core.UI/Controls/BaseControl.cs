namespace Automation.Simple.Core.UI.Controls
{
    using Automation.Simple.Core.Environment;
    using Automation.Simple.Core.Selenium;
    using Automation.Simple.Core.UI.Controls.Browser;
    using Automation.Simple.Core.UI.Controls.Locators;
    using Automation.Simple.Core.UI.Enums;
    using Automation.Simple.Helpers;
    using log4net;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Interactions;
    using OpenQA.Selenium.Support.UI;
    using System;

    public abstract class BaseControl
    {
        /// <summary>
        /// The logger instance.
        /// </summary>
        protected static readonly ILog log = LogManager.GetLogger(System.Reflection
                                                                        .MethodBase
                                                                        .GetCurrentMethod()
                                                                        .DeclaringType);

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
            _type = ControlType.NotExistingControl;
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
                MoveToControl(_control);
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
        /// Moves to the given element.
        /// </summary>
        /// <param name="control">The control to move to.</param>
        public void MoveToControl(IWebElement control)
        {
            try
            {
                Action.MoveToElement(control)
                       .Perform();
            }
            catch (Exception error)
            {
                log.Error($"Unable to move to element {error.Message}");
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
        /// Blocks the current thread until this control ceases to be displayed on the web page,
        /// or until the timeout specified by the same expires.
        /// </summary>
        /// <returns>
        /// True if the wait succeeds i.e. this control ceases to be displayed before the timeout,
        /// false otherwise.
        /// </returns>
        public bool WaitForControlToNotBeDisplayed()
        {
            return WaitForControlToNotBeDisplayed(TimeoutInSeconds);
        }

        /// <summary>
        /// Blocks the current thread until this control ceases to be displayed on the web page,
        /// or until the timeout specified by the same expires.
        /// </summary>
        /// <param name="timeout">The new timeout (in seconds).</param>
        /// <returns>
        /// True if the wait succeeds i.e. this control ceases to be displayed before the timeout,
        /// false otherwise.
        /// </returns>
        public bool WaitForControlToNotBeDisplayed(int timeout)
        {
            try
            {
                log.Info($"Waiting until '{Name}' {Type} is not displayed.");
                var timeoutInSeconds = TimeSpan.FromSeconds(timeout);
                var waitIntervalInMilliseconds = TimeSpan.FromMilliseconds(Config.WaitIntervalInMilliseconds);
                var timeoutHelper = new TimeoutHelper(timeoutInSeconds, waitIntervalInMilliseconds);
                return timeoutHelper.WaitFor(() =>
                {
                    return !IsDisplayed();
                });
            }
            catch (Exception error)
            {
                log.Error($"Unexpected error waiting for the '{Name}' {Type}. Error: [{error.Message}].");
                return false;
            }
        }

        /// <summary>
        /// Blocks the current thread until this control is displayed on the web page,
        /// or until the timeout specified by the control expires.
        /// </summary>
        /// <param name="timeout">The new timeout (in seconds).</param>
        /// <returns>
        /// True if the wait succeeds i.e. the control is displayed before the timeout, false
        /// otherwise.
        /// </returns>
        public bool WaitForControlToBeDisplayed(int timeout)
        {
            try
            {
                log.Info($"Waiting until '{Name}' {Type} is displayed.");
                var timeoutInSeconds = TimeSpan.FromSeconds(timeout);
                var waitIntervalInMilliseconds = TimeSpan.FromMilliseconds(Config.WaitIntervalInMilliseconds);
                var timeoutHelper = new TimeoutHelper(timeoutInSeconds, waitIntervalInMilliseconds);
                return timeoutHelper.WaitFor(() =>
                {
                    return IsDisplayed();
                });
            }
            catch (Exception error)
            {
                log.Error($"Unexpected error waiting for the '{Name}' {Type}. Error: [{error.Message}].");
                return false;
            }
        }

        /// <summary>
        /// Blocks the current thread until this control is displayed on the web page,
        /// or until the timeout specified by the control expires.
        /// </summary>
        /// <returns>
        /// True if the wait succeeds i.e. the control is displayed before the timeout, false
        /// otherwise.
        /// </returns>
        public bool WaitForControlToBeDisplayed()
        {
            return WaitForControlToBeDisplayed(TimeoutInSeconds);
        }

        internal object ExecuteScript(string script, params object[] args)
        {
            try
            {
                var javascriptExecutor = Driver as IJavaScriptExecutor;
                var result = javascriptExecutor.ExecuteScript(script, args);
                log.Debug($"Execute the script '{script}'. Result returned: [{result}].");
                return result;
            }
            catch (Exception e)
            {
                log.Debug($"Unable to execute the script '{script}'. Error [{e.Message}].");
                return null;
            }
        }
    }
}
