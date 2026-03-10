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
                ParseLineToTrackings(line, trackings);
            }

            return trackings;
        }

        private static void ParseLineToTrackings(string line, List<TrackingEntry> trackings)
        {
            var jsonParts = line.Split(new[] { "}{" }, StringSplitOptions.None);
            for (int i = 0; i < jsonParts.Length; i++)
            {
                string json = jsonParts.Length == 1
                    ? jsonParts[i]
                    : i == 0
                        ? jsonParts[i] + "}"
                        : i == jsonParts.Length - 1
                            ? "{" + jsonParts[i]
                            : "{" + jsonParts[i] + "}";
                if (string.IsNullOrWhiteSpace(json)) continue;
                try
                {
                    var entry = JsonSerializer.Deserialize<TrackingEntry>(json);
                    if (entry != null)
                    {
                        trackings.Add(entry);
                    }
                }
                catch { /* Skip malformed JSON */ }
            }
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

        public Dictionary<(string Project, DateTime Date), double> GetAllProjectHoursByDate()
        {
            var result = new Dictionary<(string Project, DateTime Date), double>();
            var allTrackingsFull = GetAllTrackings().OrderBy(t => t.Timestamp).ToList();
            var trackingsExcludingPause = allTrackingsFull
                .Where(t => t.ProjectName != PauseProjectName)
                .ToList();

            var datesWithRecords = allTrackingsFull
                .Select(t => t.Timestamp.Date)
                .Distinct()
                .ToHashSet();

            foreach (var startEntry in trackingsExcludingPause)
            {
                var nextTracking = allTrackingsFull
                    .FirstOrDefault(t => t.Timestamp > startEntry.Timestamp);
                var sessionEnd = nextTracking != null
                    ? nextTracking.Timestamp
                    : DateTime.Now;

                var start = startEntry.Timestamp;
                var end = sessionEnd;
                var project = startEntry.ProjectName;

                var startDate = start.Date;
                var endDate = end.Date;

                var daysSpan = (endDate - startDate).TotalDays;

                if (daysSpan > 1)
                {
                    end = startDate.AddDays(1).AddTicks(-1);
                    endDate = startDate;
                }

                var currentDate = startDate;
                var lastDate = endDate;

                while (currentDate <= lastDate)
                {
                    if (!datesWithRecords.Contains(currentDate))
                    {
                        currentDate = currentDate.AddDays(1);
                        continue;
                    }

                    var dayStart = currentDate;
                    var dayEnd = currentDate.AddDays(1).AddTicks(-1);

                    var effectiveStart = start > dayStart ? start : dayStart;
                    var effectiveEnd = end < dayEnd ? end : dayEnd;

                    if (effectiveStart < effectiveEnd)
                    {
                        var duration = (effectiveEnd - effectiveStart).TotalHours;
                        var key = (project, currentDate);
                        if (!result.ContainsKey(key))
                            result[key] = 0;
                        result[key] += duration;
                    }

                    currentDate = currentDate.AddDays(1);
                }
            }

            var rounded = new Dictionary<(string Project, DateTime Date), double>();
            foreach (var kv in result)
            {
                rounded[kv.Key] = Math.Round(kv.Value, 2);
            }

            var allProjects = trackingsExcludingPause.Select(t => t.ProjectName).Distinct().ToList();
            var rangeFirstDate = allTrackingsFull.Min(t => t.Timestamp.Date);
            var rangeLastDate = allTrackingsFull.Max(t => t.Timestamp.Date);
            var lastEntry = allTrackingsFull.LastOrDefault();
            if (lastEntry != null && lastEntry.ProjectName != PauseProjectName &&
                !allTrackingsFull.Any(t => t.Timestamp > lastEntry.Timestamp) && rangeLastDate < DateTime.Today)
            {
                rangeLastDate = DateTime.Today;
            }
            for (var d = rangeFirstDate; d <= rangeLastDate; d = d.AddDays(1))
            {
                foreach (var project in allProjects)
                {
                    var key = (project, d);
                    if (!rounded.ContainsKey(key))
                    {
                        rounded[key] = 0;
                    }
                }
            }

            return rounded;
        }
    }
}
