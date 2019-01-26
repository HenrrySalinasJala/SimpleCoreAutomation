using Automation.Simple.Core.UI.Controls;
using Automation.Simple.Core.UI.Controls.Button;
using Automation.Simple.Core.UI.Enums;
using NUnit.Framework;
using OpenQA.Selenium;


namespace Automation.Simple.Core.Test.Controls
{
    [TestFixture]
    public class ButtonTest
    {
        [Test]
        public void Test_Control_Button_ReturnsCorrectControl()
        {
            const string controlName = "ok";
            int timeout = 60;
            var webControl = new Button(controlName, By.XPath(string.Empty), timeout);
            var expectedType = typeof(Button);
            Assert.IsInstanceOf(expectedType, webControl);
        }

        [Test]
        public void Test_Control_Button_ControlTypeIsButton()
        {
            const string controlName = "ok";
            int timeout = 60;
            var webControl = new Button(controlName, By.XPath(string.Empty), timeout);
            var expectedType = typeof(Button);
            Assert.AreEqual(ControlType.Button, webControl.Type);
        }
    }
}
