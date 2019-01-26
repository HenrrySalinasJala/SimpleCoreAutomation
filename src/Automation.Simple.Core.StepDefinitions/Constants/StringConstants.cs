namespace Automation.Simple.Core.StepDefinitions.Constants
{
    /// <summary>
    /// This static class contains common string constants.
    /// </summary>
    public struct StringConstants
    {
        /// <summary>
        /// The folder name for templates.
        /// </summary>
        public const string TemplateFolderName = "Templates";

        /// <summary>
        /// The row separator.
        /// </summary>
        public const char RowSeparator = ';';

        /// <summary>
        /// The service config cookie name.
        /// </summary>
        public const string ServiceConfigCookieName = "serviceConfig";

        /// <summary>
        /// The token cookie name.
        /// </summary>
        public const string TokenCookieName = "token";

        /// <summary>
        /// The tag separator criteria.
        /// </summary>
        public const char TagSeparator = ':';

        /// <summary>
        /// The tag for the user.
        /// </summary>
        public const string UserTag = "User";

        /// <summary>
        /// The tag for the current organization.
        /// </summary>
        public const string CurrentOrganizationTag = "CurrentOrganization";

        /// <summary>
        /// Acronym to refer to Batch to Post.
        /// </summary>
        public const string BatchToPost = "BP";

        /// <summary>
        /// Acronym to refer to Batch to Suspend.
        /// </summary>
        public const string BatchToSuspend = "BS";

        /// <summary>
        /// The tag for the scenarios that do not require login
        /// </summary>
        public const string NoLoginTag = "NoLogin";

        /// <summary>
        /// The tag for the scenarios that do not require a browser.
        /// </summary>
        public const string NoBrowserTag = "NoBrowser";

        /// <summary>
        /// The constant for invalid token.
        /// </summary>
        public const string InvalidToken = "invalid token";

        /// <summary>
        /// The API step definition criteria.
        /// </summary>
        public const string ApiStepDefinition = "via REST API";

        /// <summary>
        /// The login page title.
        /// </summary>
        public const string LoginPageTitle = "Abila MIP Advance";

        /// <summary>
        /// The home page title.
        /// </summary>
        public const string HomePageTitle = "Abila MIP Advance";

        /// <summary>
        /// The open bracket criteria.
        /// </summary>
        public const string OpenBracket = "[";

        /// <summary>
        /// The close bracket criteria.
        /// </summary>
        public const string CloseBracket = "]";

        /// <summary>
        /// Represents the plus character.
        /// </summary>
        public const string Plus = "+";
        
        /// <summary>
        /// Represents the dollar character.
        /// </summary>
        public const string Dollar = "$";

        /// <summary>
        /// Constant string "yes"
        /// </summary>
        public const string Yes = "yes";

        /// <summary>
        /// constant string "no"
        /// </summary>
        public const string No = "no";

        /// <summary>
        /// represents the skip keyword.
        /// </summary>
        public const string Skip = "[skip]";

        /// <summary>
        /// Session constant value.
        /// </summary>
        public const string Session = "session";

        /// <summary>
        /// Document constant value.
        /// </summary>
        public const string Document = "document";

        /// <summary>
        /// The available token list page.
        /// </summary>
        public const string AvailableTokensPage = "AvailableTokensPage";

        /// <summary>
        /// The Report Token control name.
        /// </summary>
        public const string TokensControlName = "Tokens";

        /// <summary>
        /// The Page container name.
        /// </summary>
        public const string PageContainerName = "Page";

        /// <summary>
        /// The All tag name.
        /// </summary>
        public const string AllTag = "All";

        /// <summary>
        /// The key for the report id in the URL.
        /// For this URL https://[mipadvance]/#/reporting/1/reports/19/builder?rpt=123-456-789
        /// the key will retrive the value for "rpt" that is "123-456-789".
        /// </summary>
        public const string ReportIdKey = "rpt";

        /// <summary>
        /// The tag for Reports scenarios/features
        /// </summary>
        public const string ReportsTag = "Reports";

        /// <summary>
        /// The tag for roles and privileges.
        /// </summary>
        public const string RolesTag = "RolesAndPrivileges";

        /// <summary>
        /// The request response key.
        /// </summary>
        public const string ResponseKey = "response";

        /// <summary>
        /// The Generic API step definition criteria.
        /// </summary>
        public const string ApiRequestStepDefinition = "request";
    }
}
