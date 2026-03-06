using TimeTracker.Enum;

namespace TimeTracker
{
    internal static class Program
    { 
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

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
    }
}