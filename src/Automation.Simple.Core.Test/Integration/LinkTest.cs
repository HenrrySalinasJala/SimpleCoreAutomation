namespace Automation.Simple.Core.Test.Integration
{
    using Automation.Simple.Core.UI.Actions.Enums;
    using NUnit.Framework;

    [TestFixture]
    public class LinkTest : BaseTest
    {
        [Test]
        public void Test_Link_ShouldBeDisplayed()
        {
            const string controlName = "link text 1";
            bool? isDisplayed = ControlAction.ExecuteFunction(controlName, ActionType.IsDisplayed, Frame) as bool?;

            Assert.IsTrue(isDisplayed, $"Unable to validate field {controlName} is displayed");
        }

        [Test]
        public void Test_Link_ShouldBeAbleToClick()
        {
            const string controlName = "link text 1";
            bool isClicked = ControlAction.Execute(controlName, ActionType.Click, Frame);

            Assert.IsTrue(isClicked, $"Unable to validate field {controlName} is clicked");
        }

        [Test]
        public void Test_Link_ShouldBeEqualToExpectedValue()
        {
            const string controlName = "link text 1";
            string actualText = ControlAction.ExecuteFunction(controlName, ActionType.GetText, Frame) as string;

            Assert.AreEqual(controlName, actualText, "link text does no match");
        }
    }
}
