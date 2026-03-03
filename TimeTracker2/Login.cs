using System.Drawing.Drawing2D;

namespace TimeTracker2
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            ApplyRoundedPanel();
        }

        private void ApplyRoundedPanel()
        {
            using (var path = CreateRoundedRect(panelCard.ClientRectangle, 12))
            {
                panelCard.Region = new Region(path);
            }
        }

        private static GraphicsPath CreateRoundedRect(Rectangle rect, int radius)
        {
            var path = new GraphicsPath();
            int diameter = radius * 2;
            var arc = new Rectangle(rect.Location, new Size(diameter, diameter));
            path.AddArc(arc, 180, 90);
            arc.X = rect.Right - diameter;
            path.AddArc(arc, 270, 90);
            arc.Y = rect.Bottom - diameter;
            path.AddArc(arc, 0, 90);
            arc.X = rect.Left;
            path.AddArc(arc, 90, 90);
            path.CloseFigure();
            return path;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var db = new Helpers.DatabaseManager();
            if (!db.Register(txtName.Text))
                return;
            var mainMenu = new MainMenu();
            mainMenu.Show();
            this.Hide();
        }
    }
}
