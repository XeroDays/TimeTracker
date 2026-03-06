using TimeTracker.Forms;
using TimeTracker.Helpers;

namespace TimeTracker
{
    public partial class MainMenu : Form
    {
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        private Icon? _trayIcon;

        public MainMenu()
        {
            InitializeComponent();
            SetupDraggable();
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            LoadProjects();
            StartDefaultProject();
            InitializeTrayIcon();
            BuildTrayContextMenu();
            notifyIcon.Visible = true;
        }

        private void StartDefaultProject()
        {
            var db = new DatabaseManager();
            string defaultProject = db.GetDefaultProject();

            // Temporarily detach to avoid adding a new tracking entry on startup
            listBox1.SelectedIndexChanged -= ListBox1_SelectedIndexChanged;

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
            listBox1.SelectedIndexChanged += ListBox1_SelectedIndexChanged;
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
                BuildTrayContextMenu();
            };
            addForm.Show();
        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                ApplyProjectSelection(listBox1.SelectedItem.ToString()!);
            }
        }

        private void ApplyProjectSelection(string projectName)
        {
            lblProject.Text = projectName;
            var db = new DatabaseManager();
            db.TrackProject(projectName);
            db.SetDefaultProject(projectName);
            SyncListBoxSelection(projectName);
            UpdateTimer();
        }

        private void SyncListBoxSelection(string projectName)
        {
            listBox1.SelectedIndexChanged -= ListBox1_SelectedIndexChanged;
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                if (listBox1.Items[i].ToString() == projectName)
                {
                    listBox1.SelectedIndex = i;
                    break;
                }
            }
            listBox1.SelectedIndexChanged += ListBox1_SelectedIndexChanged;
        }

        private void InitializeTrayIcon()
        {
            using var bmp = new Bitmap(Properties.Resources.Logo__1_, 40, 40);
            var tempIcon = Icon.FromHandle(bmp.GetHicon());
            _trayIcon = (Icon)tempIcon.Clone();
            notifyIcon.Icon = _trayIcon;
        }

        private void trayContextMenu_Opening(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            BuildTrayContextMenu();
        }

        private void BuildTrayContextMenu()
        {
            trayContextMenu.Items.Clear();

            var db = new DatabaseManager();
            var projects = db.GetProjects();
            var defaultProject = db.GetDefaultProject();

            foreach (var project in projects)
            {
                if (string.IsNullOrWhiteSpace(project)) continue;

                var item = new ToolStripMenuItem(project)
                {
                    CheckOnClick = false,
                    Checked = project == defaultProject
                };
                item.Click += TrayProjectItem_Click;
                trayContextMenu.Items.Add(item);
            }

            if (trayContextMenu.Items.Count > 0)
            {
                trayContextMenu.Items.Add(new ToolStripSeparator());
            }

            var showItem = new ToolStripMenuItem("Show");
            showItem.Click += (s, _) => RestoreFromTray();
            trayContextMenu.Items.Add(showItem);

            var startWithWindowsItem = new ToolStripMenuItem("Start with Windows")
            {
                CheckOnClick = true,
                Checked = StartupHelper.IsRunAtStartupEnabled()
            };
            startWithWindowsItem.Click += StartWithWindowsItem_Click;
            trayContextMenu.Items.Add(startWithWindowsItem);

            var exitItem = new ToolStripMenuItem("Exit");
            exitItem.Click += (s, _) => Application.Exit();
            trayContextMenu.Items.Add(exitItem);
        }

        private void StartWithWindowsItem_Click(object? sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem item)
            {
                StartupHelper.SetRunAtStartup(item.Checked);
            }
        }

        private void TrayProjectItem_Click(object? sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem item && !string.IsNullOrWhiteSpace(item.Text))
            {
                ApplyProjectSelection(item.Text);
                BuildTrayContextMenu();
            }
        }

        private void RestoreFromTray()
        {
            ShowInTaskbar = true;
            Show();
            WindowState = FormWindowState.Normal;
            BringToFront();
            Activate();
        }

        private void notifyIcon_DoubleClick(object? sender, EventArgs e)
        {
            RestoreFromTray();
        }

        private void UpdateTimer()
        {
            if (listBox1.SelectedItem == null) return;
            string? selectedProject = listBox1.SelectedItem.ToString();
            if (string.IsNullOrWhiteSpace(selectedProject)) return;

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

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            ShowInTaskbar = false;
            Hide();
        }

        private void MainMenu_Resize(object? sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                ShowInTaskbar = false;
                Hide();
            }
        }

        private void MainMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            notifyIcon.Visible = false;
            notifyIcon.Dispose();
            _trayIcon?.Dispose();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            using var saveDialog = new SaveFileDialog
            {
                Filter = "Excel files (*.xlsx)|*.xlsx",
                DefaultExt = "xlsx",
                AddExtension = true,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

            if (saveDialog.ShowDialog() != DialogResult.OK) return;

            try
            {
                var db = new DatabaseManager();
                ExcelExportHelper.ExportToExcel(saveDialog.FileName, db);
                MessageBox.Show("Export completed successfully.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Export failed: {ex.Message}", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
