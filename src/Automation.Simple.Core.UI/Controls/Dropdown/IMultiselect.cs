
namespace Automation.Simple.Core.UI.Controls.Dropdown
{
    using System.Collections.Generic;

    interface IMultiselect
    {
        /// <summary>
        /// Selects multiple items in the control.
        /// </summary>
        /// <param name="itemsToSelect">The items to select.</param>
        void Select(List<string> itemsToSelect);

        /// <summary>
        /// Check if the given elements are selected.
        /// </summary>
        /// <param name="expectedItems">The expected items.</param>
        /// <returns>True if are selected otherwise false.</returns>
        bool AreSelected(List<string> expectedItems);
    }
}
