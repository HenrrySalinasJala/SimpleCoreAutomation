namespace Automation.Simple.Core.Test.Controls
{
    using Abila.MIP.AT.UI.Controls.RadioButton;
    using Automation.Simple.Core.UI.Controls;
    using Automation.Simple.Core.UI.Controls.Button;
    using Automation.Simple.Core.UI.Controls.Checkbox;
    using Automation.Simple.Core.UI.Controls.Label;
    using Automation.Simple.Core.UI.Controls.Link;
    using Automation.Simple.Core.UI.Controls.TextField;
    using NUnit.Framework;
    using System;
    using System.IO;

    [TestFixture]
    public class ControlFinderTest
    {
        private readonly string _resourcesPath = AppDomain.CurrentDomain.BaseDirectory + @"\Resources\Controls\";

        [TestCase(typeof(Button), "Cancel", "Controls.html")]
        [TestCase(typeof(Link), "Help", "Controls.html")]
        [TestCase(typeof(TextField), "Invoice", "Controls.html")]
        [TestCase(typeof(Label), "document", "Controls.html")]
        [TestCase(typeof(TextAreaField), "address", "Controls.html")]
        [TestCase(typeof(RadioButton), "Female", "Controls.html")]
        [TestCase(typeof(Checkbox), "Admin", "Controls.html")]
        public void Test_ControlFinder_FindControl_ReturnsCorrectControl(Type expectedType, 
            string controlName, string htmlResourceName)
        {
            string resourcePath = Path.Combine(_resourcesPath, htmlResourceName);
            string source = File.ReadAllText(resourcePath);
            var controlFinder = new ControlFinder(source);
            var webControl = controlFinder.Find(controlName);

            Assert.IsInstanceOf(expectedType, webControl);
        }
    }
}
