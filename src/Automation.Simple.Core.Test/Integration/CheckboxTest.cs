namespace Automation.Simple.Core.Test.Integration
{
    using Automation.Simple.Core.UI.Actions.Enums;
    using NUnit.Framework;

    [TestFixture]
    public class CheckboxTest : BaseTest
    {
        [Test]
        public void Test_Checkbox_ShouldBeDisplayed()
        {
            const string controlName = "One";
            bool? isDisplayed = ControlAction.ExecuteFunction(controlName, ActionType.IsDisplayed, Frame) as bool?;

            Assert.IsTrue(isDisplayed, $"Unable to validate field {controlName} is displayed");
        }

        [Test]
        public void Test_Checkbox_ShouldBeEnabled()
        {
            const string controlName = "One";
            bool? IsEnabled = ControlAction.ExecuteFunction(controlName, ActionType.IsEnabled, Frame) as bool?;

            Assert.IsTrue(IsEnabled, $"Unable to validate field {controlName} is enabled");
        }

        [Test]
        public void Test_Checkbox_ShouldNotBeEnabled()
        {
            const string controlName = "Four";
            bool? IsEnabled = ControlAction.ExecuteFunction(controlName, ActionType.IsEnabled, Frame) as bool?;

            Assert.IsFalse(IsEnabled, $"Unable to validate field {controlName} is not enabled");
        }

        [Test]
        public void Test_Checkbox_ShouldBeChecked()
        {
            const string controlName = "Two";
            bool? isExecuted = ControlAction.Execute(controlName, ActionType.Check, Frame);
            bool? isChecked = ControlAction.ExecuteFunction(controlName, ActionType.IsChecked, Frame) as bool?;

            Assert.IsTrue(isExecuted, "Control action was not executed");
            Assert.IsTrue(isChecked, $"Unable to validate field {controlName} is Checked");
        }

        [Test]
        public void Test_Checkbox_ShouldNotBeChecked()
        {
            const string controlName = "One";
            bool? isExecuted = ControlAction.Execute(controlName, ActionType.Uncheck, Frame);
            bool? isChecked = ControlAction.ExecuteFunction(controlName, ActionType.IsChecked, Frame) as bool?;

            Assert.IsTrue(isExecuted, "Control action was not executed");
            Assert.IsFalse(isChecked, $"Unable to validate field {controlName} is not Checked");
        }
    }
}
