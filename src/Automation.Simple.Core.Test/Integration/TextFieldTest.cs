namespace Automation.Simple.Core.Test.Integration
{
    using Automation.Simple.Core.Selenium;
    using Automation.Simple.Core.UI.Actions.Control;
    using Automation.Simple.Core.UI.Actions.Enums;
    using Automation.Simple.Helpers.Extensions;
    using NUnit.Framework;
    using System;
    using System.IO;

    [TestFixture]
    public class TextFieldTest : BaseTest
    {
        [Test]
        public void Test_TextField_ShouldNotBeDisplayed()
        {
            const string hidden = "Hidden";
            bool? isDisplayed = ControlAction.ExecuteFunction(hidden, ActionType.IsDisplayed, Frame) as bool?;

            Assert.IsFalse(isDisplayed, $"Unable to validate text field {hidden} is not displayed");
        }

        [Test]
        public void Test_TextField_ShouldNotBeEnabled()
        {
            const string city = "City";
            bool? IsEnabled = ControlAction.ExecuteFunction(city, ActionType.IsEnabled, Frame) as bool?;

            Assert.IsFalse(IsEnabled, $"Unable to validate text field {city} is not enabled");
        }

        [Test]
        public void Test_TextField_ShouldBeEnabled()
        {
            const string street = "Street";
            bool? IsEnabled = ControlAction.ExecuteFunction(street, ActionType.IsEnabled, Frame) as bool?;

            Assert.IsTrue(IsEnabled, $"Unable to validate text field {street} is enabled");
        }

        [Test]
        public void Test_TextField_ShouldBeAbleToSetText()
        {
            var expectedValue = "[random+8]".ParseKeyword();
            const string name = "Name";
            const string street = "Street";
            ControlAction.Execute(name, ActionType.SetText, Frame, expectedValue);
            ControlAction.Execute(street, ActionType.SetText, Frame, expectedValue);
            string actualNameText = ControlAction.ExecuteFunction(name, ActionType.GetText, Frame) as string;
            string actualStreetText = ControlAction.ExecuteFunction(street, ActionType.GetText, Frame) as string;

            Assert.AreEqual(expectedValue, actualNameText, $"Unable to validate text field {name} data");
            Assert.AreEqual(expectedValue, actualStreetText, $"Unable to validate text field {street} data");
        }
    }
}
