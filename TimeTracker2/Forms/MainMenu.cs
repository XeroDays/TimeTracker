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
        }

        private void LoadProjects()
        {
            listBox1.Items.Clear();
            DatabaseManager db = new DatabaseManager();
            var projects = db.GetProjects();

            foreach (var project in projects)
            {
                if (!string.IsNullOrWhiteSpace(project))
                {
                    listBox1.Items.Add(project);
                }
            }

            if (listBox1.Items.Count > 0)
            {
                listBox1.SelectedIndex = 0;
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
            string selectedProject = listBox1.SelectedItem.ToString();
            lblProject.Text = selectedProject; 
            DatabaseManager db = new DatabaseManager();
            db.TrackProject(selectedProject);
             UpdateTimer();
        }

        private void UpdateTimer()
        {
            if (listBox1.SelectedItem == null) return;
            string selectedProject = listBox1.SelectedItem.ToString();

            DatabaseManager db = new DatabaseManager();
            var allTrackings = db.GetTrackings(selectedProject);

            // Filter for current date and order by timestamp ascending
            var todayTrackings = allTrackings
                .Where(t => t.Timestamp.Date == DateTime.Today)
                .OrderBy(t => t.Timestamp)
                .ToList();

            if (todayTrackings.Count == 0)
            {
                lblTimer.Text = "00:00:00";
                return;
            }

            TimeSpan totalDuration = TimeSpan.Zero;

            // Calculate duration between consecutive tracking points
            for (int i = 0; i < todayTrackings.Count - 1; i++)
            {
                totalDuration += todayTrackings[i + 1].Timestamp - todayTrackings[i].Timestamp;
            }

            // Add duration from the last tracking point to current time
            totalDuration += DateTime.Now - todayTrackings.Last().Timestamp;

            // Update label with formatted total time (00h 00m 00s)
            lblTimer.Text = string.Format("{0:D1}h {1:D1}m {2:D1}s", 
                (int)totalDuration.TotalHours, 
                totalDuration.Minutes, 
                totalDuration.Seconds);
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
