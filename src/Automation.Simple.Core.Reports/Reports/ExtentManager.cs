namespace Automation.Simple.Core.Reports.Reports
{
    using Automation.Simple.Helpers.Utilities;
    using AventStack.ExtentReports;
    using AventStack.ExtentReports.Reporter;
    using AventStack.ExtentReports.Reporter.Configuration;
    using log4net;
    using System;
    using System.IO;

    /// <summary>
    /// Extent Manager.
    /// </summary>
    public class ExtentManager
    {
        /// <summary>
        /// The logger instance.
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// System info Operating System
        /// </summary>
        public const string OS = "OS :";

        /// <summary>
        /// System info IP address.
        /// </summary>
        public const string IP = "IP Address :";

        /// <summary>
        /// System info Host name.
        /// </summary>
        public const string Host = "Host : ";

        /// <summary>
        /// Custom JS script.
        /// </summary>
        public const string CustomJS = "$(document).ready(function() {$('.brand-logo').text(''); " +
            "var scenariosPanelXpath=\"//div[contains(@class,'card-panel nm-v')]/div[contains(.,'Scenarios')]" +
            "/parent::div\";var panel=document.evaluate(scenariosPanelXpath, document.body, null, " +
            "XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;var htmlContent='';" +
            "$(panel).children('.block').each(function(){htmlContent+=$(this).wrap('<div />').parent().html();}); " +
            "$(panel).find($('.block')).not('.panel-name').not('.chart-box').remove(); " +
            "var newHtml=htmlContent.replace(/step/g,'scenario'); $(panel).append(newHtml);" +
            "$('th:contains(Status)').each(function(){var BugIdHeader = $(this).text().replace(/Status/g, 'Bug Id');" +
            " $(this).text(BugIdHeader);});$('.table-results > thead > tr').append('<th>Status</th>');" +
            "$('.subview-left').each(function() {$(this).width(250); });});";

        /// <summary>
        /// Custom CSS style.
        /// </summary>
        public const string CustomCSS = ".step-img {display:block;border:2px white solid;border-radius:10%;width:20%!important} ";

        /// <summary>
        /// Reports folder.
        /// </summary>
        public static string ReportFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "reports");

        /// <summary>
        /// Report file name.
        /// </summary>
        public const string ReportFileName = "index.html";

        /// <summary>
        /// The lazy initialization of <see cref="ExtentReports"/>.
        /// </summary>
        private static readonly Lazy<ExtentReports> _lazy = new Lazy<ExtentReports>(() => new ExtentReports());

        /// <summary>
        /// The <see cref="ExtentReports"/> instance.
        /// </summary>
        public static ExtentReports Instance { get { return _lazy.Value; } }

        /// <summary>
        /// ExtentManager static Initialization 
        /// </summary>
        static ExtentManager()
        {
            FolderUtil.CreateFolderIfDoesNotExist(ReportFolderPath);
            var reportPath = Path.Combine(ReportFolderPath, ReportFileName);
            var htmlReporter = new ExtentHtmlReporter(reportPath);
            htmlReporter.AppendExisting = true;
            htmlReporter.Configuration().Theme = Theme.Standard;
            htmlReporter.Configuration().ChartLocation = ChartLocation.Top;
            htmlReporter.Configuration().JS = CustomJS;
            htmlReporter.Configuration().CSS = CustomCSS;
            Instance.AttachReporter(htmlReporter);

        }

        /// <summary>
        /// Initialize a new instance of <see cref="ExtentManager"/>.
        /// </summary>
        private ExtentManager()
        {
        }

        /// <summary>
        /// Saves the existing reports into a separated folder.
        /// </summary>
        public static void SaveExistingReport()
        {
            try
            {
                var reportPath = Path.Combine(ReportFolderPath, ReportFileName);
                if (File.Exists(reportPath))
                {
                    DateTime creation = File.GetCreationTime(reportPath);
                    const string folderNameFormat = "dd-MM-yyyy HH-mm-ss";
                    var existingReportFolderName = Path.Combine(ReportFolderPath, creation.ToString(folderNameFormat));
                    FolderUtil.CreateFolderIfDoesNotExist(existingReportFolderName);
                    var destinationPath = Path.Combine(existingReportFolderName, ReportFileName);
                    File.Move(reportPath, destinationPath);
                }
            }
            catch (Exception error)
            {
                log.Error($"Unable to save existing report {error.Message}", error);
            }
        }
    }
}
