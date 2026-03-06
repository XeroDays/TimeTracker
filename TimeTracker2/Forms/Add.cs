namespace TimeTracker.Forms
{
    public partial class Add : Form
    {
        public Add()
        {
            InitializeComponent();
            
        } 

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var db = new Helpers.DatabaseManager();
            if (db.CreateProject(txtProjectName.Text))
            {
                MessageBox.Show("Project added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }
    }
}
