namespace Automation.Simple.Core.UI.Controls.Dropdown
{
    using Automation.Simple.Core.UI.Enums;
    using Automation.Simple.Core.UI.Exceptions;
    using OpenQA.Selenium;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class MultiselectDropdown : Dropdown, IMultiselect
    {
        /// <summary>
        /// Initializes a new instance of <see cref="MultiselectDropdown"/>.
        /// </summary>
        /// <param name="controlName">The control name.</param>
        /// <param name="searchCriteria">The search criteria.</param>
        /// <param name="timeout">The timeout in seconds.</param>
        public MultiselectDropdown(string controlName, By searchCriteria, int timeout)
            : base(ControlType.MultiselectDropdown, controlName, searchCriteria, timeout)
        {
        }

        public bool AreSelected(List<string> expectedItems)
        {
            try
            {
                log.Info($"check if items are selected {string.Join(",", expectedItems)}");

                var selectedElements = SelectElement.AllSelectedOptions
                                                    .Select(option => option.Text.Trim())
                                                    .ToList();

                log.Info($"actual selected items{string.Join(",", selectedElements)}");

                return !expectedItems.Except(selectedElements).Any(); ;
            }
            catch (Exception error)
            {
                new ControlActionExecutionException(Name, Type, error.Message);
                return false;
            }
        }

        public void Select(List<string> itemsToSelect)
        {
            try
            {
                log.Info($"Selecting elements {string.Join(",", itemsToSelect)}");
                SelectElement.DeselectAll();
                foreach (var itemToSelect in itemsToSelect)
                {
                    Select(itemToSelect);
                }
            }
            catch (Exception error)
            {
                throw new ControlActionExecutionException(Name, Type, error.Message);
            }
        }
    }
}
