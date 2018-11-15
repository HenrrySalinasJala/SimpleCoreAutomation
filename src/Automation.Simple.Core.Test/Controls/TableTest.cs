namespace Automation.Simple.Core.Test.Controls
{
    using Automation.Simple.Core.Environment;
    using Automation.Simple.Core.UI.Actions.Control;
    using Automation.Simple.Core.UI.Controls;
    using Automation.Simple.Core.UI.Controls.Table;
    using NUnit.Framework;
    using OpenQA.Selenium;
    using System;
    using System.Collections.Generic;
    using System.IO;


    [TestFixture]
    public class TableTest
    {
        private readonly string _resourcesPath = AppDomain.CurrentDomain.BaseDirectory + @"\Resources\Controls\";
        public TableTest()
        {
        }



        [Test]
        public void ControlFinder_Table_IsInstanceValid()
        {
            Type expectedType = typeof(Table);
            var controlName = "testTable";
            var actualTable = new Table(controlName, By.XPath($"//*[data-at-name='{controlName}' ]"), Config.ExplicitTimeoutInSeconds);
            Assert.IsInstanceOf(expectedType, actualTable, "Unable to initialize a Table control type");
        }

        [Test]
        public void ControlFinder_Table_Exists_ShouldReturnTrueIfValidValues()
        {
            var controlName = "testTable";
            var actualTable = new Table(controlName, By.XPath($"//*[data-at-name='{controlName}' ]"), Config.ExplicitTimeoutInSeconds);

            Dictionary<string, string> expectedValues = new Dictionary<string, string>()
            {
                {"Codigo","3331"},
                {"Nombre","dera"}
            };

            bool isValid = actualTable.Exists(new List<Dictionary<string, string>>() { expectedValues });

            Assert.True(isValid, "The table does not contain valid values");
        }

        [Test]
        public void ControlFinder_Table_Exists_ShouldReturnFalseIfInvalidValues()
        {
            var controlName = "testTable";
            var actualTable = new Table(controlName, By.XPath($"//*[data-at-name='{controlName}' ]"), Config.ExplicitTimeoutInSeconds);

            Dictionary<string, string> expectedValues = new Dictionary<string, string>()
            {
                {"Codigo","NOTEXISTIng"},
                {"Nombre","NOT EXIST"}
            };

            bool isValid = actualTable.Exists(new List<Dictionary<string, string>>() { expectedValues });

            Assert.True(isValid, "The table does not contain valid values");
        }

    }

}
