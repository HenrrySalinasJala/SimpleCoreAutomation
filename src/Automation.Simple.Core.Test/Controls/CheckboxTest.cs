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
    public class CheckboxTest
    {
        [Test]
        public void Test_Control_Checkbox_ReturnsCorrectControl()
        {
            const string controlName = "link";
            int timeout = 60;
            var webControl = new Checkbox(controlName, By.XPath(string.Empty), timeout);
            var expectedType = typeof(Checkbox);
            Assert.IsInstanceOf(expectedType, webControl);
        }

        [Test]
        public void Test_Control_Checkbox_ControlTypeIsButton()
        {
            const string controlName = "ok";
            int timeout = 60;
            var webControl = new Checkbox(controlName, By.XPath(string.Empty), timeout);
            Assert.AreEqual(ControlType.Checkbox, webControl.Type);
        }
    }
}
