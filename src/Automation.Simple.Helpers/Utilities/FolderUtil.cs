namespace Automation.Simple.Helpers.Utilities
{
    using log4net;
    using System;
    using System.IO;

    /// <summary>
    /// The folder utility class.
    /// </summary>
    public static class FolderUtil
    {
        /// <summary>
        /// The logger instance.
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Creates all directories and subdirectories in the specified path if does not exists.
        /// </summary>
        /// <param name="folderPath">The folder path</param>
        public static void CreateFolderIfDoesNotExist(string folderPath)
        {
            try
            {
                log.Info($"Creating folder if does not exist path: {folderPath}");
                Directory.CreateDirectory(folderPath);
            }
            catch (Exception error)
            {
                log.Error($"Unable to create folder, path: {folderPath}", error);
                throw new Exception($"Unable to create folder at {folderPath} , Error: {error.Message}");
            }

        }

        /// <summary>
        /// Checks if a folder exists.
        /// </summary>
        /// <param name="folderPath">The folder path.</param>
        /// <returns>Returns true if the folder exists, otherwise false.</returns>
        public static bool ExistsFolder(string folderPath)
        {
            try
            {
                log.Info($"check if folder exists, path: {folderPath}");
                return Directory.Exists(folderPath);
            }
            catch (Exception error)
            {
                log.Error($"Unable to check if folder exists, path: {folderPath}");
                throw new Exception($"Unable to determine if the folder at '{folderPath}' exists. Error: {error.Message}.");
            }
        }

        /// <summary>
        /// Deletes the folder and subdirectories in the specified path if recursive is set to true.
        /// </summary>
        /// <param name="folderPath">The folder path to be deleted.</param>
        /// <param name="recursive">Deleted sub-directories if true.</param>
        public static void DeleteFolder(string folderPath, bool recursive = false)
        {
            try
            {
                log.Info($"Deleting folder, path: {folderPath}");
                Directory.Delete(folderPath, recursive);
            }
            catch (Exception error)
            {
                log.Error($"Unable to delete folder, path: {folderPath}");
                throw new Exception($"Unable to delete the folder at '{folderPath}'. Error: {error.Message}.");
            }
        }
    }
}
