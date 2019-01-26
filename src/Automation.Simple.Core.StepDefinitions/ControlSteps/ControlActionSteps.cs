namespace Automation.Simple.Core.StepDefinitions.ControlSteps
{
    using Automation.Simple.Core.StepDefinitions.DataTransformationTypes;
    using Automation.Simple.Core.UI.Actions.Control;
    using Automation.Simple.Core.UI.Actions.Enums;
    using NUnit.Framework;
    using TechTalk.SpecFlow;

    [Binding]
    public class ControlActionSteps : BaseStepDefinition
    {
        public ControlActionSteps(ScenarioContext scenarioContext, ControlActions controlAction)
            : base(scenarioContext)
        {
        }

        /// <summary>
        /// Clicks on a web control.
        /// </summary>
        /// <param name="controlName">The control name.</param>
        /// <param name="frame">The container name.</param>
        [Given(@"(?i)Se hace click en(?: bot(?:o|ó)n| link| campo| combo-box| lista desplegable| radio|) ([^']+?)(?: en ((?!(?:[^en].*en){2})[^']+?)|)(?: modal| formulario| secci(?:o|ó)n| panel| item| link|)(?-i)")]
        [When(@"(?i)Se hace click en(?: bot(?:o|ó)n| link| campo| combo-box| lista desplegable| radio|) ([^']+?)(?: en ((?!(?:[^en].*en){2})[^']+?)|)(?: modal| formulario| secci(?:o|ó)n| panel| item| link|)(?-i)")]
        public void Click(string controlName, string frame)
        {
            ControlAction.Execute(controlName, ActionType.Click, frame.ToString());
        }

        /// <summary>
        /// Fills a value into a field or combo-box.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="controlName">The control name.</param>
        /// <param name="containerName">The container name.</param>
        [Given(@"(?i)Se escribe '([^']+?)' en(?: campo| combo-box|) ([^']+?)(?: en ((?!(?:[^en].*en){1})[^']+?)|)(?: modal| formulario| secci(?:o|ó)n| panel| item| link|)(?-i)")]
        [When(@"(?i)Se escribe '([^']+?)' en(?: campo| combo-box|) ([^']+?)(?: en ((?!(?:[^en].*en){1})[^']+?)|)(?: modal| formulario| secci(?:o|ó)n| panel| item| link|)(?-i)")]
        public void SetTextOn(IStepArgument value, string controlName, string containerName)
        {
            bool isValueSet = ControlAction.Execute(controlName, ActionType.SetText, containerName, value.ToString());

            Assert.IsTrue(isValueSet, $"No se puede escribir valor {value}");
        }

        [When(@"(?i)Se selecciona '([^']+?)' en(?: campo| combo-box|) ([^']+?)(?: en ((?!(?:[^en].*en){1})[^']+?)|)(?: modal| formulario| secci(?:o|ó)n| panel| item| link|)(?-i)")]
        public void SelectItem(string value, string controlName, string containerName)
        {
            bool isSelected = ControlAction.Execute(controlName, ActionType.Select,
                                                    containerName, value);

            Assert.IsTrue(isSelected, $"No se puede seleccionar el elemento {value}");
        }

    }
}
