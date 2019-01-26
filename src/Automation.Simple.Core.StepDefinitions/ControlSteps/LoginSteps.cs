using Automation.Simple.Core.UI.Actions.Control;
namespace Automation.Simple.Core.StepDefinitions.ControlSteps
{
    using NUnit.Framework;
    using TechTalk.SpecFlow;

    [Binding]
    public class LoginSteps : BaseStepDefinition
    {
        public LoginSteps(ScenarioContext scenarioContext, LoginActions loginAction)
            : base(scenarioContext, loginAction)
        {
        }

        [Given(@"(?i)Se autentica como usuario por defecto(?-i)")]
        [When(@"(?i)Se autentica como usuario por defecto(?-i)")]
        public void LoginAs()
        {
            bool isLoggedAs = LoginAction.LoginAs();
            Assert.IsTrue(isLoggedAs, "No es posible autenticarse con el usuario por defecto");
        }
    }
}
