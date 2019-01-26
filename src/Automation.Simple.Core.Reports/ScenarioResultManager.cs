namespace Automation.Simple.Core.Reports
{
    using Automation.Simple.Core.Environment;
    using NUnit.Framework;
    using NUnit.Framework.Interfaces;
    using NUnit.Framework.Internal;

    /// <summary>
    /// Handles the Scenario results.
    /// </summary>
    public class ScenarioResultManager
    {

        /// <summary>
        /// Check if the current test is failed.
        /// </summary>
        /// <param name="currentContext">Test context.</param>
        /// <returns>true if the test failed otherwise false.</returns>
        public static bool IsTestFailed(TestExecutionContext currentContext)
        {
            return ResultState.Failure.Equals(currentContext.CurrentResult.ResultState) ||
                ResultState.Error.Equals(currentContext.CurrentResult.ResultState) ||
                ResultState.Cancelled.Equals(currentContext.CurrentResult.ResultState) ||
                ResultState.SetUpError.Equals(currentContext.CurrentResult.ResultState) ||
                ResultState.SetUpFailure.Equals(currentContext.CurrentResult.ResultState);
        }

        /// <summary>
        /// Checks if the current scenario is retried.
        /// </summary>
        /// <returns>true if the scenario is retried otherwise false.</returns>
        public static bool IsTestRetried()
        {
            return (TestContext.CurrentContext.CurrentRepeatCount + 1) < Config.RetryTimes;
        }
    }
}
