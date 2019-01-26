using Automation.Simple.Core.UI.Controls;
using Automation.Simple.Core.UI.Controls.Button;
using Automation.Simple.Core.UI.Controls.Link;
using Automation.Simple.Core.UI.Controls.TextField;
using Automation.Simple.Core.UI.Enums;
using NUnit.Framework;
using OpenQA.Selenium;


namespace Automation.Simple.Core.Test.Controls
{
    [TestFixture]
    public class TextFieldTest
    {
        [Test]
        public void Test_Control_TextField_ReturnsCorrectControl()
        {
            const string controlName = "TextField";
            int timeout = 60;
            var webControl = new TextField(controlName, By.XPath(string.Empty), timeout);
            var expectedType = typeof(TextField);
            Assert.IsInstanceOf(expectedType, webControl);
        }

        [Test]
        public void Test_Control_TextField_ControlTypeIsButton()
        {
            const string controlName = "TextField";
            int timeout = 60;
            var webControl = new TextField(controlName, By.XPath(string.Empty), timeout);
            Assert.AreEqual(ControlType.TextField, webControl.Type);
        }
    }
}
