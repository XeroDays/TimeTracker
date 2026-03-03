using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
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
    }
}
