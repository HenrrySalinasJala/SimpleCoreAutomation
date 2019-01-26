namespace Automation.Simple.Core.StepDefinitions.Hooks
{
    using Automation.Simple.Core.Reports.Reports;
    using Automation.Simple.Core.StepDefinitions.Constants;
    using Automation.Simple.Core.StepDefinitions.SpecFlow;
    using Automation.Simple.Core.UI.Actions.Control;
    using Automation.Simple.Helpers.Utilities;
    using AventStack.ExtentReports;
    using System;
    using System.Linq;
    using TechTalk.SpecFlow;

    [Binding]
    public class BaseHooks : BaseStepDefinition
    {
        protected BaseHooks(ScenarioContext scenarioContext, ControlActions controlAction, BrowserActions browserActions, FeatureContext featureContext)
            : base(scenarioContext, controlAction, browserActions, featureContext)
        {
        }

        [BeforeScenario(Order = 1)]
        public void AppendScenarioToFeature()
        {
            ExtentReportManager.CreateScenario(scenarioContext.ScenarioInfo.Title);
        }

        /// <summary>
        /// Navigates to the home page after a scenario is executed.
        /// </summary>
        [AfterScenario(Order = 50)]
        public void DefaultAfterScenario()
        {
            var browserActions = new BrowserActions();
            var scenarioTags = Enumerable.Union(scenarioContext.ScenarioInfo.Tags, featureContext.FeatureInfo.Tags).ToList();
            scenarioTags.Add(StringConstants.AllTag);
            Exception error = null;
            string screenshot = string.Empty;
            try
            {
                screenshot = browserActions.TakeScreenshotAsBase64();
            }
            catch (Exception exception)
            {
                error = exception;
            }
            finally
            {
                ExtentReportManager.AddScenario(scenarioContext, screenshot, scenarioTags.ToArray(), error);
            }
        }

        /// <summary>
        /// Takes a screenshot after a step fails.
        /// </summary>
        [AfterStep(Order = 50)]
        public void DefaultAfterStep()
        {
            var browserActions = new BrowserActions();
            var stepType = scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();
            var stepText = scenarioContext.StepContext.StepInfo.Text;
            var stepArguments = scenarioContext.StepContext.Values;
            ExtentReportManager.CreateStep(new GherkinKeyword(stepType), stepText);
            if (scenarioContext.StepContext.StepInfo.Table != null)
            {
                string[][] tableString = scenarioContext.StepContext.StepInfo.Table.ToArray();
                ExtentReportManager.logStepTable(tableString);
            }
            if (scenarioContext.TestError != null)
            {
                string screenshot = browserActions.TakeScreenshotAsBase64();
                ExtentReportManager.AddStep(scenarioContext, screenshot);
            }
        }

        /// <summary>
        /// Default Before feature hook.
        /// </summary>
        [BeforeFeature]
        public static void DefaultBeforeFeature(FeatureContext featureContext)
        {
            ExtentReportManager.CreateTest(featureContext.FeatureInfo.Title, featureContext.FeatureInfo.Description);
        }

        /// <summary>
        /// After feature hook.
        /// </summary>
        [AfterFeature]
        public static void DefaultAfterFeature(BrowserActions browserActions)
        {
            ExtentReportManager.EndTest();
            browserActions.CloseBrowser();
        }


        /// <summary>
        /// Before Test Run hook.
        /// </summary>
        [BeforeTestRun]
        public static void DefaultBeforeTestRun()
        {
            ExtentManager.SaveExistingReport();
            ExtentManager.Instance.AddSystemInfo(ExtentManager.OS, OSUtil.GetOSName());
            ExtentManager.Instance.AddSystemInfo(ExtentManager.Host, System.Net.Dns.GetHostName());
            ExtentManager.Instance.AddSystemInfo(ExtentManager.IP, OSUtil.GetLocalIpAddress());
        }

        /// <summary>
        /// After Test run hook.
        /// </summary>
        [AfterTestRun]
        public static void DefaultAfterTestRun()
        {
            ExtentReportManager.WriteTest();
        }
    }
}
