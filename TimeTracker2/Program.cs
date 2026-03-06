using Microsoft.Win32;
using TimeTracker.Enum;

namespace TimeTracker
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        { 
            ApplicationConfiguration.Initialize();

            SystemEvents.SessionSwitch += OnSessionSwitch;
            SystemEvents.SessionEnding += OnSessionEnding;

            var fileHelper = new Helpers.FileHelper();
            string authContent = fileHelper.ReadContent(FolderEnum.Authentication);

            if (string.IsNullOrWhiteSpace(authContent))
            {
                Application.Run(new Login());
            }
            else
            {
                Application.Run(new MainMenu());
            }
        }

        private static void OnSessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            var db = new Helpers.DatabaseManager();
            if (e.Reason == SessionSwitchReason.SessionLock)
            {
                db.TrackProject(Helpers.DatabaseManager.PauseProjectName);
            }
            else if (e.Reason == SessionSwitchReason.SessionUnlock)
            {
                string defaultProject = db.GetDefaultProject();
                if (!string.IsNullOrWhiteSpace(defaultProject))
                {
                    db.TrackProject(defaultProject);
                }
            }
        }

        private static void OnSessionEnding(object sender, SessionEndingEventArgs e)
        {
            var db = new Helpers.DatabaseManager();
            db.TrackProject(Helpers.DatabaseManager.PauseProjectName);
        }
    }
}