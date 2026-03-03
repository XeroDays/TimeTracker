using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TimeTracker2.Enum;

namespace TimeTracker2.Helpers
{
    internal class FileHelper
    {
        public const string FolderName = "TimeTrackerData";
        public const string DefaultExtension = ".txt";
        private readonly string _folderPath;

        public FileHelper()
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            _folderPath = Path.Combine(documentsPath, FolderName);

            if (!Directory.Exists(_folderPath))
            {
                Directory.CreateDirectory(_folderPath);
            }
        }

        public void WriteContent(FolderEnum fileEnum, string content)
        {
            string filePath = GetFullFilePath(fileEnum.ToString());
            File.WriteAllText(filePath, content);
        }

        public void AppendContent(FolderEnum fileEnum, string content)
        {
            string filePath = GetFullFilePath(fileEnum.ToString());
            File.AppendAllText(filePath, content + Environment.NewLine);
        }

        public string ReadContent(FolderEnum fileEnum)
        {
            string filePath = GetFullFilePath(fileEnum.ToString());
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, string.Empty);
                return string.Empty;
            }
            return File.ReadAllText(filePath);
        }

        public List<string> ReadLines(FolderEnum fileEnum)
        {
            string filePath = GetFullFilePath(fileEnum.ToString());
            if (!File.Exists(filePath))
            {
                File.WriteAllLines(filePath, Array.Empty<string>());
                return new List<string>();
            }
            return new List<string>(File.ReadAllLines(filePath));
        }

        private string GetFullFilePath(string fileName)
        {
            return Path.Combine(_folderPath, fileName + DefaultExtension);
        }
    }
}
