namespace Automation.Simple.Helpers.Utilities
{
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// This utility class has common methods for handling files.
    /// </summary>
    public static class FileUtil
    {
        /// <summary>
        /// The logger instance.
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Appends content to a file.
        /// </summary>
        /// <param name="pathFile">file path.</param>
        /// <param name="content">content string.</param>
        public static void AddContentToFile(string pathFile, string content)
        {
            try
            {
                log.Info($"Adding content to file, path: {pathFile} content length: {content.Length}");
                using (StreamWriter sw = File.AppendText(pathFile))
                {
                    sw.WriteLine(content);
                    sw.Close();
                }
            }
            catch (Exception error)
            {
                log.Error($"Unable to add content to file", error);
                throw new Exception(string.Format("Unable to append content to file {0} [ERROR]:", pathFile), error);
            }
        }

        /// <summary>
        /// Creates file if no exists.
        /// </summary>
        /// <param name="pathFile">file path.</param>
        public static void CreateFileIfNotExists(string pathFile)
        {
            try
            {
                log.Info($"Creating file if does not exist {pathFile}");
                using (StreamWriter sw = File.AppendText(pathFile))
                {
                    sw.Close();
                }
            }
            catch (Exception error)
            {
                log.Error($"Unable to create file", error);
                throw new Exception(string.Format("Unable to create file {0} [ERROR]: {1}", pathFile, error.Message));
            }
        }

        /// <summary>
        /// Deletes a file.
        /// </summary>
        /// <param name="pathFile">file path.</param>
        public static void DeleteFile(string pathFile)
        {
            FileInfo fi = new FileInfo(pathFile);
            try
            {
                log.Info($"Deleting file {pathFile}");
                fi.Delete();
            }
            catch (Exception error)
            {
                log.Error($"Unable to delete file", error);
                throw new Exception(string.Format("Unable to delete file {0} [ERROR]: {1}", pathFile, error.Message));
            }
        }

        /// <summary>
        /// Reads file content.
        /// </summary>
        /// <param name="pathFile">file path.</param>
        /// <returns>returns List&lt;string&gt; with the file content</returns>
        public static List<string> ReadContent(string pathFile)
        {
            try
            {
                log.Info($"Reading content {pathFile}");
                var contentLines = new List<string>();
                using (var stream = File.Open(pathFile, FileMode.Open))
                {
                    TextReader reader = new StreamReader(stream);
                    var line = reader.ReadLine();
                    contentLines.Add(line);
                    stream.Close();
                }

                return contentLines;
            }
            catch (Exception error)
            {
                log.Error($"Unable to read file", error);
                throw new Exception(string.Format("Unable to Read Content file from {0}", pathFile));
            }
        }

        /// <summary>
        /// Checks if file exists.
        /// </summary>
        /// <param name="pathFile">file path.</param>
        /// <returns>true if exists otherwise false.</returns>
        public static bool FileExists(string pathFile)
        {
            try
            {
                log.Info($"Check if file exists {pathFile}");
                FileInfo fi = new FileInfo(pathFile);
                return fi.Exists;
            }
            catch (Exception)
            {
                log.Error($"File {pathFile} does not exist");
                return false;
            }
        }

        /// <summary>
        /// Removes a line from the file.
        /// </summary>
        /// <param name="pathFile">file path.</param>
        /// <param name="lineToDelete">line to delete from the file.</param>
        public static void RemoveLineFromFile(string pathFile, string lineToDelete)
        {
            try
            {
                log.Info($"Remove Lines from file {pathFile}");
                File.WriteAllText(pathFile,
                File.ReadAllText(pathFile).Replace(lineToDelete, string.Empty));
            }
            catch (Exception error)
            {
                log.Error($"Unable to remove lines from file", error);
                throw new Exception(string.Format("Unable to remove line {0} from file {1} ERROR: {2}",
                    lineToDelete, pathFile, error.Message));
            }
        }
    }
}
