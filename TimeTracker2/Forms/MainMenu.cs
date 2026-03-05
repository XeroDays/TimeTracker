using TimeTracker2.Forms;
using TimeTracker2.Helpers;

namespace TimeTracker2
{
    public partial class MainMenu : Form
    {
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        public MainMenu()
        {
            InitializeComponent();
            SetupDraggable();
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            LoadProjects();
            StartDefaultProject();
        }

        private void StartDefaultProject()
        {
            var db = new DatabaseManager();
            string defaultProject = db.GetDefaultProject();

            // Temporarily detach to avoid adding a new tracking entry on startup
            listBox1.SelectedIndexChanged -= listBox1_SelectedIndexChanged;

            if (!string.IsNullOrWhiteSpace(defaultProject))
            {
                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                    if (listBox1.Items[i].ToString() == defaultProject)
                    {
                        listBox1.SelectedIndex = i;
                        lblProject.Text = defaultProject;
                        break;
                    }
                }
            }
            else if (listBox1.Items.Count > 0)
            {
                listBox1.SelectedIndex = 0;
                lblProject.Text = listBox1.Items[0].ToString();
            }

            UpdateTimer();
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
        }

        private void LoadProjects()
        {
            listBox1.Items.Clear();
            var db = new DatabaseManager();
            var projects = db.GetProjects();

            foreach (var project in projects)
            {
                if (!string.IsNullOrWhiteSpace(project))
                {
                    listBox1.Items.Add(project);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            Color backgroundColor = Color.FromArgb(30, 41, 59);
            Color selectedColor = Color.FromArgb(14, 165, 233); // Sky blue
            Color textColor = Color.FromArgb(241, 245, 249);

            e.DrawBackground();
            Graphics g = e.Graphics;

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                using (SolidBrush brush = new SolidBrush(selectedColor))
                {
                    g.FillRectangle(brush, e.Bounds);
                }
            }
            else
            {
                using (SolidBrush brush = new SolidBrush(backgroundColor))
                {
                    g.FillRectangle(brush, e.Bounds);
                }
            }

            // Draw text with some padding
            TextRenderer.DrawText(g, listBox1.Items[e.Index].ToString(), listBox1.Font, new Rectangle(e.Bounds.X + 10, e.Bounds.Y, e.Bounds.Width - 10, e.Bounds.Height), textColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);

            e.DrawFocusRectangle();
        }

        private void btnAddProject_Click(object sender, EventArgs e)
        {
            var addForm = new Add();
            this.Hide();
            addForm.FormClosed += (s, args) =>
            {
                this.Show();
                LoadProjects();
            };
            addForm.Show();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                StartWorkflow();
            }
        }


        private void StartWorkflow()
        {
            if (listBox1.SelectedItem == null) return;
            
            string selectedProject = listBox1.SelectedItem.ToString();
            lblProject.Text = selectedProject; 
            DatabaseManager db = new DatabaseManager();
            db.TrackProject(selectedProject);
            db.SetDefaultProject(selectedProject); // Mark as default
            UpdateTimer();
        }

        private void UpdateTimer()
        {
            if (listBox1.SelectedItem == null) return;
            string selectedProject = listBox1.SelectedItem.ToString();

            DatabaseManager db = new DatabaseManager();
            var projectInfo = db.GetProjectInfo(selectedProject);

            if (projectInfo.FirstRecordDate == DateTime.MinValue)
            {
                lblTimer.Text = "00h 00m 00s";
                lblDate.Text = DateTime.Today.ToString("dddd dd-MMM-yyyy");
                return;
            }

            // Update label with formatted total time (00h 00m 00s)
            lblTimer.Text = string.Format("{0:D2}h {1:D2}m {2:D2}s", 
                (int)projectInfo.Duration.TotalHours, 
                projectInfo.Duration.Minutes, 
                projectInfo.Duration.Seconds);

            // Update date label with the first record's date
            lblDate.Text = projectInfo.FirstRecordDate.ToString("dddd dd-MMM-yyyy");
        }


        #region Draggable Form Logic

        private void SetupDraggable()
        {
            this.MouseDown += MainMenu_MouseDown;
            this.MouseMove += MainMenu_MouseMove;
            this.MouseUp += MainMenu_MouseUp;
        }

        private void MainMenu_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void MainMenu_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void MainMenu_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        #endregion
    }
}
