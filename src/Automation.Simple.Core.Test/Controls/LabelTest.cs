using Automation.Simple.Core.UI.Controls.Label;
using Automation.Simple.Core.UI.Enums;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Automation.Simple.Core.Test.Controls
{
    [TestFixture]
    public class LabelTest
    {
        [Test]
        public void Test_Control_Label_ReturnsCorrectControl()
        {
            const string controlName = "label";
            int timeout = 60;
            var webControl = new Label(controlName, By.XPath(string.Empty), timeout);
            var expectedType = typeof(Label);
            Assert.IsInstanceOf(expectedType, webControl);
        }

        [Test]
        public void Test_Control_Label_ControlTypeIsButton()
        {
            const string controlName = "label";
            int timeout = 60;
            var webControl = new Label(controlName, By.XPath(string.Empty), timeout);
            Assert.AreEqual(ControlType.Label, webControl.Type);
        }
    }
}
