using Abila.MIP.AT.UI.Controls.RadioButton;
using Automation.Simple.Core.UI.Controls;
using Automation.Simple.Core.UI.Controls.Button;
using Automation.Simple.Core.UI.Controls.Checkbox;
using Automation.Simple.Core.UI.Controls.Link;
using Automation.Simple.Core.UI.Enums;
using NUnit.Framework;
using OpenQA.Selenium;


namespace Automation.Simple.Core.Test.Controls
{
    [TestFixture]
    public class RadioTest
    {
        [Test]
        public void Test_Control_Radio_ReturnsCorrectControl()
        {
            const string controlName = "RadioButton";
            int timeout = 60;
            var webControl = new RadioButton(controlName, By.XPath(string.Empty), timeout);
            var expectedType = typeof(RadioButton);
            Assert.IsInstanceOf(expectedType, webControl);
        }

        [Test]
        public void Test_Control_Checkbox_ControlTypeIsButton()
        {
            const string controlName = "RadioButton";
            int timeout = 60;
            var webControl = new RadioButton(controlName, By.XPath(string.Empty), timeout);
            Assert.AreEqual(ControlType.RadioButton, webControl.Type);
        }
    }
}
