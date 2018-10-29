namespace Automation.Simple.Core.Test.Controls
{
    using Automation.Simple.Core.UI.Controls;
    using Automation.Simple.Core.UI.Controls.Button;
    using NUnit.Framework;
    using System;
    using System.IO;

    [TestFixture]
    public class ControlFinderTest
    {
        private readonly string _resourcesPath = AppDomain.CurrentDomain.BaseDirectory + @"\Resources\Controls\";
        [Test]
        public void Test_ControlFinder_FindButtonControl()
        {
            string controlName = "Cancel";
            string controlType = "Button";
            var htmlResourceName = "Controls.html";
            string resourcePath = Path.Combine(_resourcesPath, htmlResourceName);
            string source = File.ReadAllText(resourcePath);

            var controlFinder = new ControlFinder(source);
            BaseControl controlFound = controlFinder.Find(controlName, controlType);

            
        }

        [TestCase(typeof(Button), "Cancel", "Controls.html")]
        //[TestCase(typeof(NotExistingControl), "dadasda", "Controls.html")]
        public void ControlFinder_FindControl_ReturnsCorrectControl(Type expectedType, string controlName, string htmlResourceName)
        {
            string resourcePath = Path.Combine(_resourcesPath, htmlResourceName);
            string source = File.ReadAllText(resourcePath);

            var controlFinder = new ControlFinder(source);
            var webControl = controlFinder.Find(controlName);
            Assert.IsInstanceOf(expectedType, webControl);
        }
    }
}
