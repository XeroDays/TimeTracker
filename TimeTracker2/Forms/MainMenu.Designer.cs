namespace TimeTracker2
{
    partial class MainMenu
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
            components = new System.ComponentModel.Container();
            listBox1 = new ListBox();
            btnClose = new PictureBox();
            btnAddProject = new PictureBox();
            label1 = new Label();
            label2 = new Label();
            lblProject = new Label();
            lblTimer = new Label();
            label5 = new Label();
            lblDate = new Label();
            btnMinimize = new PictureBox();
            trayContextMenu = new ContextMenuStrip(components);
            notifyIcon = new NotifyIcon(components);
            ((System.ComponentModel.ISupportInitialize)btnClose).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnAddProject).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnMinimize).BeginInit();
            SuspendLayout();
            // 
            // listBox1
            // 
            listBox1.BackColor = Color.FromArgb(30, 41, 59);
            listBox1.BorderStyle = BorderStyle.None;
            listBox1.DrawMode = DrawMode.OwnerDrawFixed;
            listBox1.Font = new Font("Segoe UI", 11F);
            listBox1.ForeColor = Color.FromArgb(241, 245, 249);
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 40;
            listBox1.Location = new Point(12, 12);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(303, 400);
            listBox1.TabIndex = 0;
            listBox1.DrawItem += listBox1_DrawItem;
            listBox1.SelectedIndexChanged += ListBox1_SelectedIndexChanged;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.Image = Properties.Resources.icons8_cross_96;
            btnClose.Location = new Point(632, 7);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(32, 31);
            btnClose.SizeMode = PictureBoxSizeMode.StretchImage;
            btnClose.TabIndex = 1;
            btnClose.TabStop = false;
            btnClose.Click += btnClose_Click;
            // 
            // btnAddProject
            // 
            btnAddProject.Image = Properties.Resources.icons8_add_96;
            btnAddProject.Location = new Point(283, 418);
            btnAddProject.Name = "btnAddProject";
            btnAddProject.Size = new Size(32, 31);
            btnAddProject.SizeMode = PictureBoxSizeMode.StretchImage;
            btnAddProject.TabIndex = 2;
            btnAddProject.TabStop = false;
            btnAddProject.Click += btnAddProject_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ButtonFace;
            label1.Location = new Point(340, 67);
            label1.Name = "label1";
            label1.Size = new Size(289, 38);
            label1.TabIndex = 3;
            label1.Text = "Time Sheet Manager";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.White;
            label2.Location = new Point(340, 154);
            label2.Name = "label2";
            label2.Size = new Size(79, 28);
            label2.TabIndex = 4;
            label2.Text = "Project";
            // 
            // lblProject
            // 
            lblProject.AutoSize = true;
            lblProject.BackColor = Color.Transparent;
            lblProject.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblProject.ForeColor = Color.Yellow;
            lblProject.Location = new Point(340, 188);
            lblProject.Name = "lblProject";
            lblProject.Size = new Size(265, 28);
            lblProject.TabIndex = 5;
            lblProject.Text = "Commercial Bank of Dubai";
            // 
            // lblTimer
            // 
            lblTimer.AutoSize = true;
            lblTimer.BackColor = Color.Transparent;
            lblTimer.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTimer.ForeColor = Color.Yellow;
            lblTimer.Location = new Point(340, 273);
            lblTimer.Name = "lblTimer";
            lblTimer.Size = new Size(123, 28);
            lblTimer.TabIndex = 7;
            lblTimer.Text = "1h 43m 10s";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.Transparent;
            label5.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.White;
            label5.Location = new Point(340, 240);
            label5.Name = "label5";
            label5.Size = new Size(67, 28);
            label5.TabIndex = 6;
            label5.Text = "Timer";
            // 
            // lblDate
            // 
            lblDate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblDate.AutoSize = true;
            lblDate.BackColor = Color.Transparent;
            lblDate.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblDate.ForeColor = Color.White;
            lblDate.Location = new Point(484, 426);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(183, 23);
            lblDate.TabIndex = 8;
            lblDate.Text = "Thursday 28-Nov-2026";
            // 
            // btnMinimize
            // 
            btnMinimize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnMinimize.Image = Properties.Resources.icons8_minimize_100;
            btnMinimize.Location = new Point(597, 7);
            btnMinimize.Name = "btnMinimize";
            btnMinimize.Size = new Size(32, 31);
            btnMinimize.SizeMode = PictureBoxSizeMode.StretchImage;
            btnMinimize.TabIndex = 9;
            btnMinimize.TabStop = false;
            btnMinimize.Click += btnMinimize_Click;
            // 
            // trayContextMenu
            // 
            trayContextMenu.ImageScalingSize = new Size(20, 20);
            trayContextMenu.Name = "trayContextMenu";
            trayContextMenu.Size = new Size(211, 4);
            trayContextMenu.Opening += trayContextMenu_Opening;
            // 
            // notifyIcon
            // 
            notifyIcon.ContextMenuStrip = trayContextMenu;
            notifyIcon.Text = "Time Tracker";
            notifyIcon.Visible = false;
            notifyIcon.DoubleClick += notifyIcon_DoubleClick;
            // 
            // MainMenu
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(15, 23, 42);
            ClientSize = new Size(670, 456);
            Controls.Add(btnMinimize);
            Controls.Add(lblDate);
            Controls.Add(lblTimer);
            Controls.Add(label5);
            Controls.Add(lblProject);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnAddProject);
            Controls.Add(btnClose);
            Controls.Add(listBox1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "MainMenu";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MainMenu";
            Load += MainMenu_Load;
            Resize += MainMenu_Resize;
            FormClosing += MainMenu_FormClosing;
            ((System.ComponentModel.ISupportInitialize)btnClose).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnAddProject).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnMinimize).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox listBox1;
        private PictureBox btnClose;
        private PictureBox btnAddProject;
        private Label label1;
        private Label label2;
        private Label lblProject;
        private Label lblTimer;
        private Label label5;
        private Label lblDate;
        private PictureBox btnMinimize;
        private ContextMenuStrip trayContextMenu;
        private NotifyIcon notifyIcon;
    }
}