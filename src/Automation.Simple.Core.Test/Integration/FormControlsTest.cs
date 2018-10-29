namespace Automation.Simple.Core.Test.Integration
{
    using Automation.Simple.Core.Environment;
    using Automation.Simple.Core.Selenium;
    using Automation.Simple.Core.UI.Actions.Control;
    using Automation.Simple.Core.UI.Actions.Enums;
    using NUnit.Framework;

    [TestFixture]
    public class FormControlsTest
    {
        public BrowserActions BrowserAction;
        private ControlAction ControlAction;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            BrowserAction = new BrowserActions();
            ControlAction = new ControlAction();
        }
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            DriverManager.GetInstance().QuitDriver();
        }
        [Test]
        public void Test_ControlAction_ItIsPossibleBuildABurguer()
        {
            BrowserAction.GoTo(Config.WebAppUrl);
            var frame = "";
            var burgerBuilderMenu = "Burger Builder";
            bool isBurgerLinkClicked = ControlAction.Execute(burgerBuilderMenu, ActionType.Click, frame);
            Assert.True(isBurgerLinkClicked, $"Unable to click control {burgerBuilderMenu}");

            var lessSalad = "More Salad";
            bool isLessSaladButtonClicked = ControlAction.Execute(lessSalad, ActionType.Click, frame);
            Assert.True(isLessSaladButtonClicked, $"Unable to click control {lessSalad }");
            var currentPriceLabel = "current Price";
            string actualCurrentPrice = (string)ControlAction.ExecuteFunction(currentPriceLabel, ActionType.GetText, frame);

            const string expectedCurrentPrice = "4.50";
            Assert.AreEqual(expectedCurrentPrice, actualCurrentPrice, $"{currentPriceLabel} value is {actualCurrentPrice} expected was {expectedCurrentPrice}");
        }
    }
}
