namespace Automation.Simple.Core.Reports.Reports
{
    using AventStack.ExtentReports;
    using AventStack.ExtentReports.MarkupUtils;
    using AventStack.ExtentReports.Model;
    using log4net;
    using NUnit.Framework;
    using NUnit.Framework.Interfaces;
    using NUnit.Framework.Internal;
    using System;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using TechTalk.SpecFlow;

    /// <summary>
    /// Report Manager class.
    /// </summary>
    public static class ExtentReportManager
    {
        /// <summary>
        /// The logger instance.
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The Test Suite Report formatter.
        /// </summary>
        [ThreadStatic]
        private static ThreadLocal<ExtentTest> _feature;

        /// <summary>
        /// The Scenario Report formatter.
        /// </summary>
        [ThreadStatic]
        private static ThreadLocal<ExtentTest> _scenario;

        /// <summary>
        /// The Step  Report formatter.
        /// </summary>
        [ThreadStatic]
        private static ThreadLocal<ExtentTest> _step;

        /// <summary>
        /// The Background Gherkin keyword.
        /// </summary>
        private const string Background = "Background";

        /// <summary>
        /// The Hook Gherkin keyword.
        /// </summary>
        private const string SetUp = "Set Up";

        /// <summary>
        /// The Feature Gherkin keyword.
        /// </summary>
        private const string Feature = "Feature";

        /// <summary>
        /// The Scenario Gherkin keyword.
        /// </summary>
        private const string Scenario = "Scenario";

        /// <summary>
        /// Failed Scenario test category.
        /// </summary>
        private const string FailedCategory = "Failed";

        /// <summary>
        /// Tag for showing bug id properly formated.
        /// </summary>
        public const string HtmlThTag = "</td><td>";

        /// <summary>
        /// Gets the Test report formatter.
        /// </summary>
        /// <returns>The report formatter.</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest GetTest()
        {
            return _feature.Value;
        }

        /// <summary>
        /// Gets the scenario report formatter.
        /// </summary>
        /// <returns>The report formatter.</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest GetScenario()
        {
            return _scenario.Value;
        }

        /// <summary>
        /// Gets the step report formatter.
        /// </summary>
        /// <returns>The report formatter.</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest GetStep()
        {
            return _step.Value;
        }

        /// <summary>
        /// Creates a test node in the report formatter.
        /// </summary>
        /// <param name="extentReport">instance of <see cref="ExtentReports"/>.</param>
        /// <param name="name">test node name.</param>
        /// <returns>The report formatter.</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest CreateTest(string name, string description = "")
        {
            if (_feature == null)
            {
                _feature = new ThreadLocal<ExtentTest>();
            }

            var test = ExtentManager.Instance.CreateTest(name, description);
            _feature.Value = test;

            return test;
        }

        /// <summary>
        /// Creates a scenario node in the report.
        /// </summary>
        /// <param name="name">Scenario name.</param>
        /// <param name="description">scenario description.</param>
        /// <returns>The report formatter.</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest CreateScenario(string name, string description = null)
        {
            if (_scenario == null)
            {
                _scenario = new ThreadLocal<ExtentTest>();
            }
            var currentScenario = _scenario.Value?.GetModel().Name.Replace(HtmlThTag, string.Empty);
            ExtentTest scenario = null;
            if (currentScenario != name)
            {
                scenario = _feature.Value.CreateNode(new GherkinKeyword(Scenario), name, description);
                _scenario.Value = scenario;
                var currentName = _scenario.Value.GetModel().Name;
                _scenario.Value.GetModel().Name = currentName + HtmlThTag;
                _scenario.Value.GetModel().Parent = _feature.Value.GetModel();
            }
            else
            {
                _feature.Value.GetModel().NodeContext().Add(_scenario.Value.GetModel());
            }

            return scenario;
        }

        /// <summary>
        /// Creates a step node in the report.
        /// </summary>
        /// <param name="keyword">Gherkin keyword e.g.Given, When, Then</param>
        /// <param name="name">step text.</param>
        /// <param name="description">step description.</param>
        /// <returns>The report formatter.</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest CreateStep(GherkinKeyword keyword, string name, string description = null)
        {
            if (_step == null)
            {
                _step = new ThreadLocal<ExtentTest>();
            }
            var step = _scenario.Value.CreateNode(keyword, name, description);
            _step.Value = step;
            _step.Value.GetModel().Parent = _scenario.Value.GetModel();
            log.Info($"Creating Step {keyword} {name}");
            return step;
        }

        /// <summary>
        /// Removes Last Failed Scenario from Report.
        /// </summary>
        /// <param name="extentTest">The report formatter.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        private static void RemoveLastScenarioFromReport()
        {
            var lastScenario = _scenario.Value.GetModel().Parent.NodeContext().GetLast();
            var categories = lastScenario.CategoryContext().GetAllItems().RemoveAll(tag => tag != null);
            _scenario.Value.GetModel().Parent.NodeContext().Remove(lastScenario);
        }

        /// <summary>
        /// Ends The test.
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void EndTest()
        {
            try
            {
                if (_feature.Value.GetModel().NodeContext().GetAllItems().Any())
                {
                    log.Info($"Ending Test {_feature.Value.GetModel().Name}");
                    _feature.Value.GetModel().End();
                }
                else
                {
                    RemoveFeature();
                }
            }
            catch (Exception error)
            {
                log.Error("End Test", error);
            }
            _feature = null;
        }


        /// <summary>
        /// Writes the test result report.
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void WriteTest()
        {
            try
            {
                log.Info("Writing Test Result report.");
                ExtentManager.Instance.Flush();
                log.Debug("HTML report written");
            }
            catch (Exception error)
            {
                log.Error("Unable to Write Test Result.", error);
            }
        }

        /// <summary>
        /// Removes the current feature,
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        private static void RemoveFeature()
        {
            log.Info("Removing Test " + _feature.Value.GetModel().Name);
            ExtentManager.Instance.RemoveTest(_feature.Value);
        }

        /// <summary>
        /// Gets the current test status.
        /// </summary>
        /// <returns>The test Status.</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        private static Status GetTestLogStatus()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            Status logstatus;
            switch (status)
            {
                case TestStatus.Failed:
                    logstatus = Status.Fail;
                    break;
                case TestStatus.Inconclusive:
                    logstatus = Status.Skip;
                    break;
                case TestStatus.Warning:
                    logstatus = Status.Pass;
                    break;
                case TestStatus.Skipped:
                    logstatus = Status.Skip;
                    break;
                default:
                    logstatus = Status.Pass;
                    break;
            }

            return logstatus;
        }

        /// <summary>
        /// Adds the scenario to the report.
        /// </summary>
        /// <param name="scenarioContext">Spec Flow scenario context.</param>
        /// <param name="base64Image">The screenshot.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void AddScenario(ScenarioContext scenarioContext, string base64Image, string[] tags, Exception error = null)
        {
            try
            {
                if (_scenario == null)
                {
                    throw new Exception("Unable to Add End Scenario log to an non Initialized test.");
                }

                var status = GetTestLogStatus();
                if (ScenarioResultManager.IsTestFailed(TestExecutionContext.CurrentContext) || error != null)
                {
                    if (_step == null)
                    {
                        CreateStep(new GherkinKeyword(Background), SetUp);
                    }

                    if (ScenarioResultManager.IsTestRetried())
                    {
                        RemoveRetriedScenario();
                        return;
                    }
                    _scenario.Value.AssignCategory(FailedCategory);
                    AddFailStepLog(scenarioContext, base64Image, error);
                }
                else
                {
                    AddLogStepByStatus(status);
                }
                _scenario.Value.AssignCategory(tags);
                _step = null;
                _scenario = null;
            }
            catch (Exception exception)
            {
                log.Error("Unable to Add Scenario to report", exception);
            }
        }

        /// <summary>
        /// Adds a step log given its status,
        /// </summary>
        /// <param name="status">The step status.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        private static void AddLogStepByStatus(Status status)
        {
            _step.Value.Log(status, status.ToString());
        }

        /// <summary>
        /// Logs a retried scenario.
        /// </summary>
        /// <param name="scenarioContext">Spec Flow scenario context.</param>
        /// <param name="base64Image">The screenshot.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        private static void RemoveRetriedScenario()
        {
            RemoveLastScenarioFromReport();
            _step = null;
        }

        /// <summary>
        /// Adds an step to the report.
        /// </summary>
        /// <param name="scenarioContext">Spec Flow scenario context.</param>
        /// <param name="base64Image">The screenshot.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void AddStep(ScenarioContext scenarioContext, string base64Image, Exception error = null)
        {
            if (_step == null)
            {
                throw new Exception("Unable to Add logs to a non Initialized Scenario");
            }

            var status = GetTestLogStatus();
            if (ScenarioResultManager.IsTestFailed(TestExecutionContext.CurrentContext) || error != null || status.Equals(Status.Skip))
            {
                if (ScenarioResultManager.IsTestRetried())
                {
                    return;
                }
                AddFailStepLog(scenarioContext, base64Image, error);
            }
            else
            {
                _step.Value.Log(status, status.ToString());
            }
        }

        /// <summary>
        /// Adds a failed step log.
        /// </summary>
        /// <param name="scenarioContext">Spec Flow scenario context.</param>
        /// <param name="base64Image">The screenshot.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        private static void AddFailStepLog(ScenarioContext scenarioContext, string base64Image, Exception error = null)
        {
            var currentLogs = _step.Value.GetModel().LogContext().GetEnumerator();
            if (!HasExpectedStatusLog(currentLogs, Status.Fail))
            {
                var logOutput = error != null ? error.StackTrace : TestExecutionContext.CurrentContext.CurrentResult.Output;
                var errorMessage = scenarioContext.TestError != null ? scenarioContext.TestError.Message : "No message";
                if (error != null)
                {
                    errorMessage = error.Message;
                }
                var errorMessageMark = MarkupHelper.CreateCodeBlock("MESSAGE: " + errorMessage);
                var errorMark = MarkupHelper.CreateCodeBlock("ERROR: " + logOutput);
                AddScreenshot(Status.Fail, base64Image);
                _step.Value.Fail(errorMessageMark);
                _step.Value.Fail(errorMark);
            }
        }


        /// <summary>
        /// Check if the scenario has failed logs.
        /// </summary>
        /// <param name="stepLogs">The scenario logs.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        private static bool HasExpectedStatusLog(TIterator<Log> stepLogs, Status expectedStatus)
        {
            bool hasExpectedStatus = false;

            foreach (var log in stepLogs)
            {
                if (log.Status == expectedStatus)
                {
                    hasExpectedStatus = true;
                    break;
                }
            }

            return hasExpectedStatus;
        }

        /// <summary>
        /// Adds an screenshot link to the step.
        /// </summary>
        /// <param name="testStatus">The step status.</param>
        /// <param name="base64Image">The screenshot.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        private static void AddScreenshot(Status testStatus, string base64Image)
        {
            if (!string.IsNullOrEmpty(base64Image))
            {
                var imageLink = $"<a href=\"data:image/gif;base64,{base64Image}\" data-featherlight=\"image\">" +
                $"<img src=\"data:image/gif;base64,{base64Image}\" class=\"step-img\" width=\"150px\"/></a>";
                _step.Value.Log(testStatus, imageLink);
            }
        }

        /// <summary>
        /// Adds an step log.
        /// </summary>
        /// <param name="scenarioContext">The scenario context.</param>
        /// <param name="table">The step table,</param>
        /// <param name="imageBase64">The image as base64.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void logStepTable(string[][] table)
        {
            if (table != null && table.Length > 0)
            {
                var tableMark = MarkupHelper.CreateTable(table);
                _step.Value.Info(tableMark);
            }
        }
    }
}
