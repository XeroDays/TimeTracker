using System.Linq;
using System.Text.Json;
using TimeTracker.Enum;

namespace TimeTracker.Helpers
{
    internal class TrackingEntry
    {
        public string ProjectName { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
    }

    internal class ProjectInfoDTO
    {
        public TimeSpan Duration { get; set; }
        public DateTime FirstRecordDate { get; set; }
    }

    internal class DatabaseManager
    {
        public const string PauseProjectName = "Pause";

        public ProjectInfoDTO GetProjectInfo(string projectName)
        {
            var allTrackings = GetAllTrackings().OrderBy(t => t.Timestamp).ToList();
            
            var todayProjectTrackings = allTrackings
                .Where(t => t.Timestamp.Date == DateTime.Today && t.ProjectName == projectName)
                .ToList();

            var info = new ProjectInfoDTO();

            if (todayProjectTrackings.Count > 0)
            {
                info.FirstRecordDate = todayProjectTrackings.First().Timestamp;
                TimeSpan totalDuration = TimeSpan.Zero;

                foreach (var startEntry in todayProjectTrackings)
                {
                    var nextEntry = allTrackings
                        .Where(t => t.Timestamp > startEntry.Timestamp)
                        .FirstOrDefault();

                    if (nextEntry != null)
                    {
                        totalDuration += nextEntry.Timestamp - startEntry.Timestamp;
                    }
                    else
                    {
                        totalDuration += DateTime.Now - startEntry.Timestamp;
                    }
                }
                
                info.Duration = totalDuration;
            }
            else
            {
                info.FirstRecordDate = DateTime.MinValue;
                info.Duration = TimeSpan.Zero;
            }

            return info;
        }

        public void TrackProject(string projectName)
        {
            var allTrackings = GetAllTrackings();
            string lastProjectName = string.Empty;

            if (allTrackings.Count > 0)
            {
                lastProjectName = allTrackings.Last().ProjectName;
            }

            if (lastProjectName != projectName)
            {
                var trackingData = new TrackingEntry { ProjectName = projectName, Timestamp = DateTime.Now };
                string jsonString = JsonSerializer.Serialize(trackingData);
                var fileHelper = new FileHelper();
                fileHelper.AppendContent(FolderEnum.Trackings, jsonString);
            }
        }

        public List<TrackingEntry> GetAllTrackings()
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
                    if (entry != null)
                    {
                        trackings.Add(entry);
                    }
                }
                catch { /* Skip malformed lines */ }
            }

            return trackings;
        }

        [Obsolete("Use GetAllTrackings() instead and filter by project name manually if needed.")]
        public List<TrackingEntry> GetTrackings(string projectName)
        {
            return GetAllTrackings().Where(t => t.ProjectName == projectName).ToList();
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

        public void SetDefaultProject(string projectName)
        {
            var fileHelper = new FileHelper();
            fileHelper.WriteContent(FolderEnum.DefaultProject, projectName);
        }

        public string GetDefaultProject()
        {
            var fileHelper = new FileHelper();
            return fileHelper.ReadContent(FolderEnum.DefaultProject);
        }
    }
}
