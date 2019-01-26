namespace Automation.Simple.Core.UI.Controls.Checkbox
{
    /// <summary>
    /// Represents the checkbox control.
    /// </summary>
    /// <typeparam name="TControl">The control technology.</typeparam>
    /// <typeparam name="TSearchCriteria">The search criteria.</typeparam>
    public interface ICheckbox
    {
        /// <summary>
        /// Checks the control.
        /// </summary>
        void Check();

        /// <summary>
        /// Unchecks the control.
        /// </summary>
        void Uncheck();

        /// <summary>
        /// Gets a value indicating whether or not the checkbox is checked.
        /// </summary>
        /// <returns>The boolean value.</returns>
        bool IsChecked();
    }
}
