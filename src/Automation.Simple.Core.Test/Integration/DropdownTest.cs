namespace Automation.Simple.Core.Test.Integration
{
    using Automation.Simple.Core.UI.Actions.Enums;
    using NUnit.Framework;

    [TestFixture]
    public class DropdownTest : BaseTest
    {
        [Test]
        public void Test_Dropdown_ShouldBeDisplayed()
        {
            const string controlName = "Country";
            bool? isDisplayed = ControlAction.ExecuteFunction(controlName, ActionType.IsDisplayed, Frame) as bool?;

            Assert.IsTrue(isDisplayed, $"Unable to validate field {controlName} is displayed");
        }


        [Test]
        public void Test_Dropdown_ShouldBeAbleToSelectDropdownItem()
        {
            const string controlName = "Country";
            const string expectedItemToSelect = "Bolivia";
            var isExecuted = ControlAction.Execute(controlName, ActionType.Select, Frame, expectedItemToSelect);
            var actualItemSelected = ControlAction.ExecuteFunction(controlName, ActionType.GetText, Frame);

            Assert.IsTrue(isExecuted, $"Unable to select {controlName} ");
            Assert.AreEqual(expectedItemToSelect, actualItemSelected, "The expected value is not correct");
        }
    }
}
