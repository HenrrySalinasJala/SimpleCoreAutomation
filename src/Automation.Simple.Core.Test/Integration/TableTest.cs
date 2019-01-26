namespace Automation.Simple.Core.Test.Integration
{
    using Automation.Simple.Core.UI.Actions.Enums;
    using NUnit.Framework;
    using System.Collections.Generic;

    [TestFixture]
    public class TableTest : BaseTest
    {

        [Test]
        public void Test_Table_Exists_ShouldReturnTrueIfValidValues()
        {
            Dictionary<string, string> expectedValues = new Dictionary<string, string>()
            {
                {"Item Number","S-03"},
                {"Description","Crow"}
            };

            bool isValid = (bool)ControlAction.ExecuteFunction("Table test", ActionType.Exists,
                Frame, expectedValues);

            Assert.True(isValid, $"Unable to validate table data");
        }

        [Test]
        public void Test_Table_Exists_ShouldReturnFalseIfInvalidValues()
        {
            Dictionary<string, string> expectedValues = new Dictionary<string, string>()
            {
                {"Item Number","NotEXISTING VALuE"},
            };

            bool isValid = (bool)ControlAction.ExecuteFunction("Table test", ActionType.Exists,
                Frame, expectedValues);

            Assert.False(isValid, $"Unable to validate table data");
        }
    }
}
