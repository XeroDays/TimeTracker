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
            listBox1 = new ListBox();
            btnClose = new PictureBox();
            btnAddProject = new PictureBox();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)btnClose).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnAddProject).BeginInit();
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
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.Image = Properties.Resources.icons8_cross_96;
            btnClose.Location = new Point(635, 3);
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
            label1.Location = new Point(340, 12);
            label1.Name = "label1";
            label1.Size = new Size(289, 38);
            label1.TabIndex = 3;
            label1.Text = "Time Sheet Manager";
            // 
            // MainMenu
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(15, 23, 42);
            ClientSize = new Size(670, 456);
            Controls.Add(label1);
            Controls.Add(btnAddProject);
            Controls.Add(btnClose);
            Controls.Add(listBox1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "MainMenu";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MainMenu";
            Load += MainMenu_Load;
            ((System.ComponentModel.ISupportInitialize)btnClose).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnAddProject).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox listBox1;
        private PictureBox btnClose;
        private PictureBox btnAddProject;
        private Label label1;
    }
}