namespace TimeTracker2
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            listBox1.Items.Add("Task 1: Project Setup");
            listBox1.Items.Add("Task 2: Database Design");
            listBox1.Items.Add("Task 3: UI Implementation");
            listBox1.Items.Add("Task 4: Testing & Debugging");

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
            TextRenderer.DrawText(g, listBox1.Items[e.Index].ToString(), listBox1.Font, 
                new Rectangle(e.Bounds.X + 10, e.Bounds.Y, e.Bounds.Width - 10, e.Bounds.Height), 
                textColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);

            e.DrawFocusRectangle();
        }
    }
}
