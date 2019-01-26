namespace Automation.Simple.Core.Test.Integration
{
    using Automation.Simple.Core.UI.Actions.Enums;
    using Automation.Simple.Helpers.Extensions;
    using NUnit.Framework;

    [TestFixture]
    public class TextAreaTest : BaseTest
    {
        [Test]
        public void Test_TextArea_ShouldBeDisplayed()
        {
            const string controlName = "Description";
            bool? isDisplayed = ControlAction.ExecuteFunction(controlName, ActionType.IsDisplayed, Frame) as bool?;

            Assert.IsTrue(isDisplayed, $"Unable to validate field {controlName} is displayed");
        }

        [Test]
        public void Test_TextArea_ShouldBeToSetText()
        {
            const string controlName = "Description";
            var expectedValue = "[random+50]".ParseKeyword();
            bool isExecuted = ControlAction.Execute(controlName, ActionType.SetText, Frame, expectedValue);
           string actualValue = ControlAction.ExecuteFunction(controlName, ActionType.GetText, Frame) as string;

            Assert.IsTrue(isExecuted, "The control action is not executed");
            Assert.AreEqual(expectedValue,actualValue, $"Unable to validate field {controlName} data");
        }
    }
}
