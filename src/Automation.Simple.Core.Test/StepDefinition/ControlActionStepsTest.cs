namespace Automation.Simple.Core.Test.StepDefinition
{
    using Automation.Simple.Core.StepDefinitions.ControlSteps;
    using Automation.Simple.Core.UI.Actions.Control;
    using NUnit.Framework;
    using System.Linq;
    using TechTalk.SpecFlow;

    [TestFixture]
    public class ControlActionStepsTest : BaseControlActionTest
    {
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
        public void Test_ControlActionStep_ShouldPerformClickAction()
        {
            ScenarioInfo scenarioInfo = new ScenarioInfo("test scenario", new string[] { "Organization:QA2" });
            ScenarioSetup(scenarioInfo);
            var testclass = new ControlActionSteps(ScenarioContext.Current, new ControlAction());


            testRunner.When("I click More Salad button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
            testRunner.CollectScenarioErrors();
        }
    }

    //[Binding]
    //class MockedSteps : BaseStep
    //{
    //    public MockedSteps(ScenarioContext scenarioContext) : base(scenarioContext)
    //    {
    //    }

    //    [Then(@"I should( not|) see '(.*?)'(?: order|) in ((?!.*column).*?)(?: notification| field| drop-down list| combo-box| label|)(?: on ([^']+?)|)(?: modal| form| section| panel| item| link|)")]
    //    public void ShouldSeeValue(string not, IStepInput actualValue, string controlName,
    //        IStepInput containerName)
    //    {
    //        const string expectedValue = "An error was found in 001, [XYZ] - A/P Credits. The total debits and credits are not equal for the Document.";

    //        Assert.AreEqual(expectedValue, actualValue.ToString(),
    //            "Value '{0}' is not present in {1}.", expectedValue, controlName);
    //    }
    //}
}
