namespace Automation.Simple.Core.Test.Integration
{
    using Automation.Simple.Core.UI.Actions.Enums;
    using NUnit.Framework;

    [TestFixture]
    public class RadioButtonTest : BaseTest
    {
        [Test]
        public void Test_RadioButton_ShouldBeDisplayed()
        {
            const string controlName = "Email";
            bool? isDisplayed = ControlAction.ExecuteFunction(controlName, ActionType.IsDisplayed, Frame) as bool?;

            Assert.IsTrue(isDisplayed, $"Unable to validate field {controlName} is not displayed");
        }

        [Test]
        public void Test_RadioButton_ShouldBeEnabled()
        {
            const string controlName = "Email";
            bool? isEnabled = ControlAction.ExecuteFunction(controlName, ActionType.IsEnabled, Frame) as bool?;

            Assert.IsTrue(isEnabled, $"Unable to validate field {controlName} is not enabled");
        }

        [Test]
        public void Test_RadioButton_ShouldNotBeEnabled()
        {
            const string controlName = "Fax";
            bool? isEnabled = ControlAction.ExecuteFunction(controlName, ActionType.IsEnabled, Frame) as bool?;

            Assert.IsFalse(isEnabled, $"Unable to validate field {controlName} is enabled");
        }

        [Test]
        public void Test_RadioButton_ShouldBeAbleToBeSelected()
        {
            const string controlName = "Phone";
            bool? isExexcuted = ControlAction.Execute(controlName, ActionType.Click, Frame) as bool?;
            bool? isSelected = ControlAction.ExecuteFunction(controlName, ActionType.IsSelected, Frame) as bool?;

            Assert.IsTrue(isExexcuted, $"Unable to execte control action");
            Assert.IsTrue(isSelected, $"The control {controlName} is not selected");
        }
    }
}
