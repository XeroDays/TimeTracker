using Microsoft.Win32;

namespace TimeTracker.Helpers
{
    internal static class StartupHelper
    {
        private const string RunKeyPath = @"Software\Microsoft\Windows\CurrentVersion\Run";
        private const string ValueName = "TimeTracker";

        public static bool IsRunAtStartupEnabled()
        {
            try
            {
                using var key = Registry.CurrentUser.OpenSubKey(RunKeyPath, writable: false);
                var currentPath = Application.ExecutablePath;
                var storedPath = key?.GetValue(ValueName) as string;
                return string.Equals(storedPath, currentPath, StringComparison.OrdinalIgnoreCase);
            }
            catch
            {
                return false;
            }
        }

        public static void SetRunAtStartup(bool enable)
        {
            try
            {
                using var key = Registry.CurrentUser.OpenSubKey(RunKeyPath, writable: true);
                if (key == null) return;

                if (enable)
                {
                    key.SetValue(ValueName, Application.ExecutablePath);
                }
                else
                {
                    key.DeleteValue(ValueName, throwOnMissingValue: false);
                }
            }
            catch (UnauthorizedAccessException)
            {
                // User may not have permission to write to registry
            }
        }
    }
}
