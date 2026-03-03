namespace TimeTracker2
{
    partial class Login
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panelCard = new Panel();
            lblTitle = new Label();
            txtName = new TextBox();
            btnLogin = new Button();
            panelCard.SuspendLayout();
            SuspendLayout();
            // 
            // panelCard
            // 
            panelCard.BackColor = Color.FromArgb(30, 41, 59);
            panelCard.Controls.Add(lblTitle);
            panelCard.Controls.Add(txtName);
            panelCard.Controls.Add(btnLogin);
            panelCard.Location = new Point(13, 14);
            panelCard.Margin = new Padding(4, 5, 4, 5);
            panelCard.Name = "panelCard";
            panelCard.Padding = new Padding(27, 31, 27, 31);
            panelCard.Size = new Size(425, 286);
            panelCard.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(148, 163, 184);
            lblTitle.Location = new Point(31, 52);
            lblTitle.Margin = new Padding(4, 0, 4, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(363, 53);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "TimeTracker";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txtName
            // 
            txtName.BackColor = Color.FromArgb(51, 65, 85);
            txtName.BorderStyle = BorderStyle.FixedSingle;
            txtName.Font = new Font("Segoe UI", 11F);
            txtName.ForeColor = Color.FromArgb(241, 245, 249);
            txtName.Location = new Point(31, 146);
            txtName.Margin = new Padding(4, 5, 4, 5);
            txtName.Name = "txtName";
            txtName.PlaceholderText = "Enter your name";
            txtName.Size = new Size(363, 32);
            txtName.TabIndex = 0;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.FromArgb(14, 165, 233);
            btnLogin.Cursor = Cursors.Hand;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatAppearance.MouseOverBackColor = Color.FromArgb(56, 189, 248);
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(31, 188);
            btnLogin.Margin = new Padding(4, 5, 4, 5);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(363, 62);
            btnLogin.TabIndex = 1;
            btnLogin.Text = "Login";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // Login
            // 
            AcceptButton = btnLogin;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(15, 23, 42);
            ClientSize = new Size(450, 314);
            Controls.Add(panelCard);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4, 5, 4, 5);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Login";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Login";
            panelCard.ResumeLayout(false);
            panelCard.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelCard;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Button btnLogin;
    }
}