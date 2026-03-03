using TimeTracker2.Enum;

namespace TimeTracker2
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
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