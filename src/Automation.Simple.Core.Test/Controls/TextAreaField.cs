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
    public class TextAreaFieldTest
    {
        [Test]
        public void Test_Control_TextAreaField_ReturnsCorrectControl()
        {
            const string controlName = "TextAreaField";
            int timeout = 60;
            var webControl = new TextAreaField(controlName, By.XPath(string.Empty), timeout);
            var expectedType = typeof(TextAreaField);
            Assert.IsInstanceOf(expectedType, webControl);
        }

        [Test]
        public void Test_Control_TextAreaField_ControlTypeIsButton()
        {
            const string controlName = "TextAreaField";
            int timeout = 60;
            var webControl = new TextAreaField(controlName, By.XPath(string.Empty), timeout);
            Assert.AreEqual(ControlType.TextAreaField, webControl.Type);
        }
    }
}
