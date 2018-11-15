﻿namespace Automation.Simple.Core.UI.Controls.Link
{
    using System;
    using Automation.Simple.Core.UI.Controls.Browser;
    using Automation.Simple.Core.UI.Enums;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;

    public class Link : BaseControl, ILink
    {
        public Link(string controlName, By searchCriteria, int timeout) :
            base(ControlType.Link, controlName, searchCriteria, timeout)
        {
        }
        public Link(string controName, IWebElement nativeControl, int timeout)
            : base(ControlType.Link, controName, nativeControl, timeout)
        {
        }
        /// <summary>
        /// Gets the control's URL.
        /// </summary>
        /// <returns>The URL.</returns>
        public string GetURL()
        {
            try
            {
                log.Info($"Get url from '{Name}' {Type}.");
                string url = Control.GetAttribute("href");
                log.Debug($"URL retrieved from '{Name}' {Type}: [{url}].");
                return url;
            }
            catch (Exception error)
            {
                log.Error($"Unable to get the URL from '{Name}' {Type}. Error [{error.Message}].");
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
                string text = Control.GetAttribute("innerText");
                log.Debug($"Text retrieved from '{Name}' {Type}: [{text}].");
                return text;
            }
            catch (Exception error)
            {
                log.Error($"Unable to get the text from '{Name}' {Type}. Error [{error.Message}].");
                throw;
            }
        }

        /// <summary>
        /// Clicks on the control.
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
                log.Error($"Unable to click on '{Name}' {Type}. Error [{error}].");
                throw;
            }
        }

    }
}
