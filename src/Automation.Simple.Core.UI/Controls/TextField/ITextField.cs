namespace Automation.Simple.Core.UI.Controls.TextField
{

    /// <summary>
    /// Represents the text field control.
    /// </summary>
    /// <typeparam name="TControl">The control technology.</typeparam>
    /// <typeparam name="TSearchCriteria">The search criteria.</typeparam>
    public interface ITextField
    {
        /// <summary>
        /// Clears the content of the control.
        /// </summary>
        void Clear();

        /// <summary>
        /// Simulates typing text into the control.
        /// </summary>
        /// <param name="text">The text value.</param>
        void SetText(string text);

        /// <summary>
        /// Gets the text of the control.
        /// </summary>
        /// <returns>The text.</returns>
        string GetText();

        /// <summary>
        /// Presses the TAB key into the control.
        /// </summary>
        void PressTab();
    }
}
