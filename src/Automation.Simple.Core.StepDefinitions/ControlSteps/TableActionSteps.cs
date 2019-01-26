using Automation.Simple.Core.UI.Actions.Control;
namespace Automation.Simple.Core.StepDefinitions.ControlSteps
{
    using Automation.Simple.Core.UI.Actions.Enums;
    using FluentAssertions;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TechTalk.SpecFlow;

    [Binding]
    public class TableActionSteps : BaseStepDefinition
    {
        public TableActionSteps(ScenarioContext scenarioContext, ControlActions controlAction)
            : base(scenarioContext, controlAction)
        {
        }

        [Then(@"(?i)(No |)Deber(?:i|í)a ver los siguientes valores en (?: tabla|)(.*?)(?: en ((?!(?:[^en].*en){1})[^']+?)|)(?: modal| formulario| secci(?:o|ó)n| panel|)(?-i)")]
        public void VerifyTableValues(string not, string controlName, string frame, List<Dictionary<string, string>> expectedValues)
        {
            Func<bool> valuesExists = delegate ()
            {
                return (bool)ControlAction.ExecuteFunction(controlName, ActionType.Exists,
                frame, expectedValues);
            };
            if (string.IsNullOrEmpty(not))
            {
                valuesExists.Should()
                            .NotThrow("Unable to check table values")
                            .Which.Should()
                            .BeTrue($"The grid does not contain the values {string.Join(",", expectedValues.Select(k => k.Values)).ToList()}");
            }
            else
            {
                valuesExists.Should()
                            .NotThrow("Unable to check table values")
                            .Which.Should()
                            .BeFalse($"The grid contain unexpected values {string.Join(",", expectedValues.Select(k => k.Values)).ToList()}");
            }
        }
    }
}
