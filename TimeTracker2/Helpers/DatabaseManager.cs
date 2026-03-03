using TimeTracker2.Enum;

namespace TimeTracker2.Helpers
{
    internal class DatabaseManager
    {
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
