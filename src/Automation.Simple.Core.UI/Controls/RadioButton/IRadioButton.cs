namespace Automation.Simple.Core.UI.Controls.RadioButton
{
    /// <summary>
    /// Radio Button Interface
    /// </summary>
    /// <typeparam name="TControl">The control technology.</typeparam>
    /// <typeparam name="TSearchCriteria">The search criteria.</typeparam>
    public interface  IRadioButton
    {
        /// <summary>
        /// Selects the given radio button.
        /// </summary>
        void Click();

        /// <summary>
        /// Evaluates if the Radio Button is selected.
        /// </summary>
        /// <returns></returns>
        bool IsSelected();
    }
}
