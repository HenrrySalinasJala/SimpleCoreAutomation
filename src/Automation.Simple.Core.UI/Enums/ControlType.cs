namespace Automation.Simple.Core.UI.Enums
{
    using System.ComponentModel;

    /// <summary>
    /// The supported control types.
    /// </summary>
    public enum ControlType
    {
        /// <summary>
        /// The text field.
        /// </summary>
        [Description("textField")]
        TextField,

        /// <summary>
        /// The date field.
        /// </summary>
        [Description("dateField")]
        DateField,

        /// <summary>
        /// The button.
        /// </summary>
        [Description("button")]
        Button,

        /// <summary>
        /// The link.
        /// </summary>
        [Description("link")]
        Link,

        /// <summary>
        /// The checkbox.
        /// </summary>
        [Description("booleanField")]
        Checkbox,

        /// <summary>
        /// The label.
        /// </summary>
        [Description("label")]
        Label,

        /// <summary>
        /// The dropdown.
        /// </summary>
        [Description("drop-down;combo-box")]
        Dropdown,

        /// <summary>
        /// The multiselect dropdown.
        /// </summary>
        [Description("multiselect-dropdown")]
        MultiselectDropdown,

        /// <summary>
        /// The table.
        /// </summary>
        [Description("grid;table")]
        Table,

        /// <summary>
        /// The Radio Button
        /// </summary>
        [Description("radioBtnField")]
        RadioButton,

        /// <summary>
        /// The not existing control.
        /// </summary>
        NotExistingControl,

        /// <summary>
        /// The container control.
        /// </summary>
        [Description("frame")]
        Frame,

        /// <summary>
        /// The Text Area Field
        /// </summary>
        [Description("textareaField")]
        TextAreaField,
    }
}
