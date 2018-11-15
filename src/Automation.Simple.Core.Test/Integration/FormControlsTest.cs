namespace Automation.Simple.Core.Test.Integration
{
    using Automation.Simple.Core.Environment;
    using Automation.Simple.Core.Selenium;
    using Automation.Simple.Core.UI.Actions.Control;
    using Automation.Simple.Core.UI.Actions.Enums;
    using Automation.Simple.Helpers.Extensions;
    using NUnit.Framework;
    using System.Collections.Generic;

    [TestFixture]
    public class FormControlsTest
    {
        public BrowserActions BrowserAction;
        private ControlAction ControlAction;
        public string frame = "";
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            BrowserAction = new BrowserActions();
            ControlAction = new ControlAction();
            BrowserAction.GoTo("http://mock1.com/");
            ControlAction.Execute("Usuario", ActionType.SetText, frame, "admin");
            ControlAction.Execute("Password", ActionType.SetText, frame, "admin");
            ControlAction.Execute("Acceder", ActionType.Click, frame);
        }
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            DriverManager.GetInstance().QuitDriver();
        }

        [Test]
        public void Test_ControlAction_Table_Exists_ShouldReturnYtueIfValidValues()
        {
           
            
            string productBarCode = "[random]".ParseKeyword();
            string productName = "[random]".ParseKeyword();
            string productDecription = "[random+9]".ParseKeyword();
            
            ControlAction.Execute("Productos", ActionType.Click, "Menu");
            ControlAction.Execute("Agregar producto", ActionType.Click, frame);
            ControlAction.Execute("Codigo de barras", ActionType.SetText, frame, productBarCode);
            ControlAction.Execute("Nombre", ActionType.SetText, frame, productName);
            ControlAction.Execute("Descripcion", ActionType.SetText, frame, productDecription);
            ControlAction.Execute("Precio de entrada", ActionType.SetText, frame, "90.1");
            ControlAction.Execute("Precio de salida", ActionType.SetText, frame, "80.1");
            ControlAction.Execute("Unidad", ActionType.SetText, frame, "Lts.");
            ControlAction.Execute("presentacion", ActionType.SetText, frame, "Botella");
            ControlAction.Execute("Minima en inventario", ActionType.SetText, frame, "10");
            ControlAction.Execute("Inventario inicial", ActionType.SetText, frame, "90");
            ControlAction.Execute("Agregar producto", ActionType.Click, frame);

            Dictionary<string, string> expectedValues = new Dictionary<string, string>()
            {
                {"Codigo",productBarCode},
                {"Nombre",productName}
            };

            bool isValid = (bool)ControlAction.ExecuteFunction("Lista de Productos", ActionType.Exists,
                frame, expectedValues);

            Assert.True(isValid, $"Unable to validate table data");
        }

        [Test]
        public void Test_ControlAction_Table_Exists_ShouldReturnFalseIfInvalidValues()
        {
            BrowserAction.GoTo("http://mock1.com/");
            string productName = "[random]".ParseKeyword();
            
            ControlAction.Execute("Productos", ActionType.Click, "Menu");

            Dictionary<string, string> expectedValues = new Dictionary<string, string>()
            {
                {"Codigo","NotEXISTING VALuE"},
                {"Nombre",productName}
            };

            bool isValid = (bool)ControlAction.ExecuteFunction("Lista de Productos", ActionType.Exists,
                frame, expectedValues);

            Assert.False(isValid, $"Unable to validate table data");
        }
    }
}
