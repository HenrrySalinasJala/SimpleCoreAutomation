namespace Automation.Simple.Core.Test.StepDefinition
{
    using Automation.Simple.Core.StepDefinitions;
    using Automation.Simple.Core.StepDefinitions.ControlSteps;
    using Automation.Simple.Core.UI.Actions.Control;
    using Automation.Simple.Core.UI.Actions.Enums;
    using NUnit.Framework;
    using System;
    using System.Linq;
    using TechTalk.SpecFlow;

    [TestFixture]
    public class ControlActionStepsTest 
    {
        const string expectedContainerNameMessage = "expected container name should be different than empty";
        const string expectedControlNameMessage = "expected control name should be different than empty";
        const string expectedControlValeMessage = "expected value should be different than empty";

        ITestRunner testRunner;
        public ControlActionStepsTest()
        {
            testRunner = TestRunnerManager.GetTestRunner();
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            FeatureInfo featureInfo = new FeatureInfo(new System.Globalization.CultureInfo("en-US"), "test feature", "test comment", ProgrammingLanguage.CSharp, new string[] { "TagTest" });
            testRunner.OnFeatureStart(featureInfo);
        }

        [OneTimeTearDown]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }

        [TearDown]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }

        private void ScenarioSetup(ScenarioInfo scenarioInfo)
        {
            var tagsList = scenarioInfo.Tags.ToList();

            scenarioInfo = new ScenarioInfo(scenarioInfo.Title, tagsList.ToArray());
            testRunner.OnScenarioStart(scenarioInfo);
        }

        [Test]
        public void Test_ControlActionSteps_RegexWriteText_ShouldMatchExpectedTextAndControlNameAndFrame()
        {
            ScenarioInfo scenarioInfo = new ScenarioInfo("test scenario", new string[] { "Test" });
            ScenarioSetup(scenarioInfo);
            var testclass = new ControlActionSteps(ScenarioContext.Current, new ControlActions());
            testRunner.Given("Se escribe \'admin\' en campo Usuario en pagina", ((string)(null)), null, "Given ");
            testRunner.CollectScenarioErrors();
        }

        [Test]
        public void Test_ControlActionSteps_RegexWriteText_ShouldMatchOnlyTextAndControlNameAndControlType()
        {
            ScenarioInfo scenarioInfo = new ScenarioInfo("test scenario", new string[] { "Test" });
            ScenarioSetup(scenarioInfo);
            var testclass = new ControlActionSteps(ScenarioContext.Current, new ControlActions());

            testRunner.Given("Se escribe \'admin\' en campo Usuario", ((string)(null)), (null), "Given ");
            Assert.AreEqual(expectedContainerNameMessage, ScenarioContext.Current.TestError.Message, "unexpected message");
        }

        [Test]
        public void Test_ControlActionSteps_RegexWriteText_ShouldMatchOnlyTextAndControlName()
        {
            ScenarioInfo scenarioInfo = new ScenarioInfo("test scenario", new string[] { "Test" });
            ScenarioSetup(scenarioInfo);
            var testclass = new ControlActionSteps(ScenarioContext.Current, new ControlActions());

            testRunner.Given("Se escribe \'admin\' en ", ((string)(null)), (null), "Given ");
            Assert.AreEqual(expectedControlNameMessage, ScenarioContext.Current.TestError.Message, "unexpected message");
        }

        [Test]
        public void Test_ControlActionSteps_RegexWriteText_ShouldMatchOnlyText()
        {
            ScenarioInfo scenarioInfo = new ScenarioInfo("test scenario", new string[] { "Test" });
            ScenarioSetup(scenarioInfo);
            var testclass = new ControlActionSteps(ScenarioContext.Current, new ControlActions());

            testRunner.Given("Se escribe \'\' en Usuario", ((string)(null)), (null), "Given ");
            Assert.AreEqual(expectedControlValeMessage, ScenarioContext.Current.TestError.Message, "unexpected message");
        }
    }

    [Binding]
    class MockedSteps : BaseStepDefinition
    {
        const string expectedContainerNameMessage = "expected container name should be different than empty";
        const string expectedControlNameMessage = "expected control name should be different than empty";
        const string expectedControlValeMessage = "expected value should be different than empty";
        public MockedSteps(ScenarioContext scenarioContext) : base(scenarioContext)
        {
        }

        [Given(@"Se escribe '(.*?)' en(?: campo| combo-box|) (.*?)(?: en ((?!(?:[^en].*en){1}).*?)|)(?: modal| formulario| seccion| panel|)")]
        
        public void SetTextOn(string value, string controlName, string containerName)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new Exception(expectedControlValeMessage);
            }

            if (string.IsNullOrEmpty(controlName))
            {
                throw new Exception(expectedControlNameMessage);
            }

            if (string.IsNullOrEmpty(containerName))
            {
                throw new Exception(expectedContainerNameMessage);
            }
        }
    }
}
