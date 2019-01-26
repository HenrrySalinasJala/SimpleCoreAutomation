namespace Automation.Simple.Core.Test.Integration
{
    using Automation.Simple.Core.UI.Actions.Enums;
    using NUnit.Framework;
    using System.Collections.Generic;

    [TestFixture]
    public class MultiselectDropdownTest : BaseTest
    {
        [Test]
        public void Test_MultiselectDropdown_ShouldBeDisplayed()
        {
            const string controlName = "Fruits";
            bool? isDisplayed = ControlAction.ExecuteFunction(controlName, ActionType.IsDisplayed, Frame) as bool?;

            Assert.IsTrue(isDisplayed, $"Unable to validate field {controlName} is displayed");
        }

        [Test]
        public void Test_MultiselectDropdown_ShouldBeAbleToSelectMultipleItems()
        {
            const string controlName = "Fruits";

            var expectedItemsToSelect = new List<string> { "Apple","Grape"};
            var isExecuted = ControlAction.Execute(controlName, ActionType.Select, Frame, expectedItemsToSelect);
            bool? areSelected = ControlAction.ExecuteFunction(controlName, ActionType.AreSelected, Frame, expectedItemsToSelect) as bool?;

            Assert.IsTrue(isExecuted, $"Unable to select {controlName} ");
            Assert.IsTrue(areSelected, $"The values are not selected");
        }

        [Test]
        public void Test_MultiselectDropdown_ShouldReturnFalseWhenItemIsNotSelected()
        {
            const string controlName = "Fruits";

            var expectedItemsToSelect = new List<string> { "Apple", "Grape" };
            var isExecuted = ControlAction.Execute(controlName, ActionType.Select, Frame, expectedItemsToSelect);
            expectedItemsToSelect.Add("Orange");
            bool? areSelected = ControlAction.ExecuteFunction(controlName, ActionType.AreSelected, Frame, expectedItemsToSelect) as bool?;

            Assert.IsTrue(isExecuted, $"Unable to select {controlName} ");
            Assert.IsFalse(areSelected, $"Incorrect values are  selected");
        }
    }
}
