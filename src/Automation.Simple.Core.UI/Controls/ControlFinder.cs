namespace Automation.Simple.Core.UI.Controls
{
    using Automation.Simple.Helpers;
    using AngleSharp.Dom;
    using Automation.Simple.Core.UI.Constants;
    using Automation.Simple.Core.UI.Controls.Enums;
    using Automation.Simple.Helpers.Utilities;
    using OpenQA.Selenium;
    using System;
    using Automation.Simple.Core.Environment;
    using System.Collections.Generic;
    using AngleSharp.Parser.Html;
    using AngleSharp.Dom.Html;
    using System.Linq;
    using System.Reflection;
    using Automation.Simple.Core.UI.Exceptions;
    using log4net;
    using Automation.Simple.Core.Selenium;
    using Automation.Simple.Core.UI.Controls.Browser;

    public class ControlFinder
    {
        /// <summary>
        /// The logger instance.
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The generic XPath locator for the controls.
        /// </summary>
        private const string GenericControlXPath = "//*[@data-at-name='{0}']";


        /// <summary>
        /// The generic XPath locator for the control container and controls.
        /// </summary>
        private const string GenericContainerControlXPath =
            "//*[@data-auto-name='{0}']/descendant::*[@data-at-name='{1}']";

        /// <summary>
        /// The separator character for Control Names.
        /// </summary>
        private const char SeparatorCharForControlNames = ';';

        /// <summary>
        /// The current Assembly Name with extension.
        /// </summary>
        private readonly string _currentAssembly = Assembly.GetExecutingAssembly().GetName().Name + ".dll";

        /// <summary>
        /// The description of the containers.
        /// </summary>
        private static string[] _descriptionContainerList;

        public string Source { get; set; }

        public ControlFinder(string source)
        {
            Source = source;
            if (_descriptionContainerList == null)
            {
                _descriptionContainerList = DescriptionAttributeUtil.GetDescriptionValues<ControlType>();
            }
        }
        public ControlFinder()
        {
            if (_descriptionContainerList == null)
            {
                _descriptionContainerList = DescriptionAttributeUtil.GetDescriptionValues<ControlType>();
            }
        }

        public BaseControl Find(string controlName, string frame = "")
        {
            return TryFindControl(controlName, frame);
        }

        /// <summary>
        /// Tries to find a control given its name and container.
        /// </summary>
        /// <param name="controlName">The control name.</param>
        /// <param name="containerName">The container name.</param>
        /// <returns>
        /// The control instance if it exists, NotExistingControl instance after the timeout expires,
        /// otherwise null.
        /// </returns>
        private BaseControl TryFindControl(string controlName, string containerName, bool waitForAngular = true)
        {
            BaseControl control = null;
            var timeoutInSeconds = TimeSpan.FromSeconds(Config.ExplicitTimeoutInSeconds);
            var waitIntervalInMilliseconds = TimeSpan.FromMilliseconds(Config.WaitIntervalInMilliseconds);
            var timeout = new TimeoutHelper(timeoutInSeconds, waitIntervalInMilliseconds);
            try
            {
                var conditionSucceeded = timeout.WaitFor(() =>
                {
                    try
                    {
                        control = FindControl(controlName, containerName);
                        return true;
                    }
                    catch (ControlNotFoundException)
                    {
                        Source = DriverManager.GetInstance().GetDriver().PageSource;
                        return false;
                    }
                });
                if (!conditionSucceeded)
                {
                    log.Error($"Timeout expired for '{controlName}'. NotExistingControl will be returned.");
                    control = new NotExistingControl(controlName, By.XPath(string.Format(GenericControlXPath, controlName)),
                        Config.ExplicitTimeoutInSeconds);
                }
            }
            catch (Exception error)
            {
                log.Error($"Unable to get the control '{controlName}'. Error: {error.Message}.");
            }
            return control;
        }

        public BaseControl FindControl(string controlName, string controlFrame = null)
        {
            IElement control;
            By webControlLocator;

            if (string.IsNullOrEmpty(controlName) && string.IsNullOrEmpty(controlFrame))
            {
                throw new ArgumentException("The control and container names must not have a null or empty value.");
            }

            if (!string.IsNullOrEmpty(controlName) && string.IsNullOrEmpty(controlFrame))
            {
                control = SearchElement(Source, controlName);

                webControlLocator =
                    By.XPath(string.Format(GenericControlXPath, control.GetAttribute(DOMAttributes.ControlNameAttribute)));
            }
            else if (string.IsNullOrEmpty(controlName) && !string.IsNullOrEmpty(controlFrame))
            {
                control = SearchElementContainer(Source, controlFrame);

                controlName = controlFrame;

                webControlLocator =
                    By.XPath(string.Format(GenericControlXPath, control.GetAttribute(DOMAttributes.ControlNameAttribute)));
            }
            else
            {
                // The control has a container
                IElement container = SearchElementContainer(Source, controlFrame);
                control = SearchElement(container.OuterHtml, controlName);

                webControlLocator =
                    By.XPath(string.Format(GenericContainerControlXPath,
                        container.GetAttribute(DOMAttributes.ControlNameAttribute),
                        control.GetAttribute(DOMAttributes.ControlNameAttribute)));
            }

            // The control type value of the custom attribute from the HTML element.
            string controlType = control.GetAttribute(DOMAttributes.ControlTypeAttribute);

            // The enum value from the ControlType enum.
            ControlType enumControlTypeValue = DescriptionAttributeUtil.GetValueFromDescription<ControlType>(controlType);

            // The web control Type from the current assembly.
            Type webControlType = AssemblyHelper.GetTypeFromAssembly(enumControlTypeValue.ToString(), _currentAssembly);

            // the WebControl instance.
            dynamic webControl = Activator.CreateInstance(webControlType, controlName, webControlLocator,
                Config.ImplicitTimeoutInSeconds);

            return webControl;
        }

        /// <summary>
        /// Searches a HTML element container the given automation name attribute in the HTML source.
        /// </summary>
        /// <param name="htmlSource">The HTML source.</param>
        /// <param name="nameAttributeValue">The value of the automation name attribute.</param>
        /// <returns>Returns the HTML element container.</returns>
        /// <exception cref="ControlNotFoundException">Raise an exception if the element container was not found.</exception>
        private IElement SearchElementContainer(string htmlSource, string nameAttributeValue)
        {
            List<IElement> elements = GetAutomationElementsByName(htmlSource, nameAttributeValue);

            var elementContainers =
                elements.Where(
                    control =>
                        _descriptionContainerList.Any(
                            container => container == control.GetAttribute(DOMAttributes.ControlTypeAttribute))).ToList();

            if (elementContainers.Any())
            {
                if (elementContainers.Count() > 1)
                {
                    //TODO: Logger.Warn("{0} containers were found with the name: '{1}'.", elements.Count(), nameAttributeValue);
                }

                return elementContainers.First();
            }

            //TODO: Logger.Fatal("No container was found with the name: '{0}'.", nameAttributeValue);
            throw new ControlNotFoundException($"The container with the name:'{nameAttributeValue}' was not found.");
        }

        /// <summary>
        /// Searches a HTML element that contains the given control automation name attribute in the HTML source.
        /// </summary>
        /// <param name="htmlSource">The HTML source.</param>
        /// <param name="nameAttributeValue">The value of the automation name attribute.</param>
        /// <returns>Returns the HTML element.</returns>
        /// <exception cref="ControlNotFoundException">Raise an exception if the element was not found.</exception>
        private IElement SearchElement(string htmlSource, string nameAttributeValue)
        {
            var elements = GetAutomationElementsByName(htmlSource, nameAttributeValue);

            if (elements.Any())
            {
                if (elements.Count() > 1)
                {
                    //TODO: Logger.Warn("{0} elements were found with the name: '{1}'.", elements.Count(), nameAttributeValue);
                }

                return elements.First();
            }

            //TODO: Logger.Fatal("No element was found with the name: '{0}'.", nameAttributeValue);
            throw new ControlNotFoundException(string.Format("The element with the name:'{0}' was not found.",
                    nameAttributeValue));
        }


        /// <summary>
        /// Gets the HTML elements by name set in data-auto-name attribute.
        /// </summary>
        /// <param name="htmlSource">The HTML source.</param>
        /// <param name="name">The value of automation name attribute.</param>
        /// <returns>A list with HTML elements.</returns>
        private List<IElement> GetAutomationElementsByName(string htmlSource, string name)
        {
            return
                GetElementsWithAutomationAttributes(htmlSource)
                    .Where(
                        control =>
                            control.GetAttribute(DOMAttributes.ControlNameAttribute)
                                .Split(SeparatorCharForControlNames)
                                .Any(dataAutoName =>
                                    dataAutoName.Trim()
                                                .Equals(name.Trim(), StringComparison.CurrentCultureIgnoreCase))).ToList();
        }


        /// <summary>
        /// Gets the HTML elements that contains the automation attributes.
        /// </summary>
        /// <param name="htmlSource">The HTML source.</param>
        /// <returns>A list with HTML elements.</returns>
        private List<IElement> GetElementsWithAutomationAttributes(string htmlSource)
        {
            var parser = new HtmlParser();
            IHtmlDocument htmlDocument = parser.Parse(htmlSource);

            return
                htmlDocument.All.Where(
                    control =>
                        control.HasAttribute(DOMAttributes.ControlNameAttribute) &&
                        control.HasAttribute(DOMAttributes.ControlTypeAttribute)).ToList();
        }

        private void ValidateArguments()
        {
        }
    }
}
