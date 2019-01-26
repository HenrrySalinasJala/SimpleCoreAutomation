namespace Automation.Simple.Core.UI.Controls.Dropdown
{
    interface IDropdown
    {
        /// <summary>
        /// Selects an item from the dropdown.
        /// </summary>
        /// <param name="item">The item to be selected.</param>
        void Select(string item);

        /// <summary>
        /// Gets the selected item.
        /// </summary>
        /// <returns>The selected item.</returns>
        string GetText();
    }
}
