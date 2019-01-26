namespace Automation.Simple.Core.Test.Integration
{
    using Automation.Simple.Core.UI.Actions.Enums;
    using NUnit.Framework;

    [TestFixture]
    public class ButtonTest : BaseTest
    {
        [Test]
        public void Test_Button_ShouldBeDisplayed()
        {
            const string controlName = "Enabled";
            bool? isDisplayed = ControlAction.ExecuteFunction(controlName, ActionType.IsDisplayed, Frame) as bool?;

            Assert.IsTrue(isDisplayed, $"Unable to validate field {controlName} is displayed");
        }

        [Test]
        public void Test_Button_ShouldBeEnabled()
        {
            const string controlName = "Enabled";
            bool? IsEnabled = ControlAction.ExecuteFunction(controlName, ActionType.IsEnabled , Frame) as bool?;

            Assert.IsTrue(IsEnabled, $"Unable to validate field {controlName} is enabled");
        }

        [Test]
        public void Test_Button_ShouldNotBeEnabled()
        {
            const string controlName = "Disabled";
            bool? IsEnabled = ControlAction.ExecuteFunction(controlName, ActionType.IsEnabled, Frame) as bool?;

            Assert.IsFalse(IsEnabled, $"Unable to validate field {controlName} is not enabled");
        }

        [Test]
        public void Test_Button_ShouldBeAbleToClick()
        {
            const string controlName = "Enabled";
            bool isClicked = ControlAction.Execute(controlName, ActionType.Click, Frame);
            
            Assert.IsTrue(isClicked, $"Unable to click {controlName}");
        }

        [Test]
        public void Test_Button_ShouldNotBeAbleToClick()
        {
            const string controlName = "Disabled";
            bool isClicked = ControlAction.Execute(controlName, ActionType.Click, Frame);

            Assert.IsFalse(isClicked, $"It is possible to click {controlName}");
        }
    }
}
