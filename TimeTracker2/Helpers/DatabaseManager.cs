using System.Linq;
using System.Text.Json;
using TimeTracker2.Enum;

namespace TimeTracker2.Helpers
{
    internal class TrackingEntry
    {
        public string ProjectName { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
    }

    internal class DatabaseManager
    {
        public void TrackProject(string projectName)
        {
            var trackingData = new TrackingEntry { ProjectName = projectName, Timestamp = DateTime.Now };
            string jsonString = JsonSerializer.Serialize(trackingData);

            var fileHelper = new FileHelper();
            fileHelper.AppendContent(FolderEnum.Trackings, jsonString);
        }

        public List<TrackingEntry> GetTrackings(string projectName)
        {
            var fileHelper = new FileHelper();
            var lines = fileHelper.ReadLines(FolderEnum.Trackings);
            var trackings = new List<TrackingEntry>();

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                try
                {
                    var entry = JsonSerializer.Deserialize<TrackingEntry>(line);
                    if (entry != null && entry.ProjectName == projectName)
                    {
                        trackings.Add(entry);
                    }
                }
                catch { /* Skip malformed lines */ }
            }

            return trackings;
        }

        public bool Register(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Please enter your name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            var fileHelper = new FileHelper();
            fileHelper.WriteContent( FolderEnum.Authentication, name);

            return true;
        }

        public bool CreateProject(string projectName)
        {
            if (string.IsNullOrWhiteSpace(projectName))
            {
                MessageBox.Show("Please enter a project name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            var fileHelper = new FileHelper();
            fileHelper.AppendContent(FolderEnum.Projects, projectName);

            return true;
        }

        public List<string> GetProjects()
        {
            var fileHelper = new FileHelper();
            return fileHelper.ReadLines(FolderEnum.Projects);
        }
    }
}
