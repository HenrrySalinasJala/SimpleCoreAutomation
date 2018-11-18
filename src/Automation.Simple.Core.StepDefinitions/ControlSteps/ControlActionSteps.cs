namespace Automation.Simple.Core.StepDefinitions.ControlSteps
{
    using Automation.Simple.Core.UI.Actions.Control;
    using Automation.Simple.Core.UI.Actions.Enums;
    using TechTalk.SpecFlow;

    [Binding]
    public class ControlActionSteps : BaseStepDefinition
    {
        public ControlActionSteps(ScenarioContext scenarioContext, ControlAction controlAction)
            : base(scenarioContext)
        {
        }

        /// <summary>
        /// Clicks on a web control.
        /// </summary>
        /// <param name="controlName">The control name.</param>
        /// <param name="frame">The container name.</param>
        [Given(@"Se hace click en(?: boton| link| campo| combo-box| lista desplegable| radio|) ([^']+?)(?: en ([^']+?)|)(?: modal| formulario|panel|)")]
        [When(@"Se hace click en(?: boton| link| campo| combo-box| lista desplegable| radio|) ([^']+?)(?: en ([^']+?)|)(?: modal| formulario|panel|)")]
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
        [Given(@"Se escribe '([^']+?)' en(?: campo| combo-box|) ([^']+?)(?: en ((?!(?:[^en].*en){1})[^']+?)|)(?: modal| formulario| seccion| panel|)")]
        [When(@"Se escribe '([^']+?)' en(?: campo| combo-box|) ([^']+?)(?: en ((?!(?:[^en].*en){1})[^']+?)|)(?: modal| formulario| seccion| panel|)")]
        public void SetTextOn(string value, string controlName, string containerName)
        {
            ControlAction.Execute(controlName, ActionType.SetText, containerName, value.ToString());
        }
    }
}
