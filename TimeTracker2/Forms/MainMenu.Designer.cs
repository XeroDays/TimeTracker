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
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
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
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.Yellow;
            label3.Location = new Point(340, 192);
            label3.Name = "label3";
            label3.Size = new Size(265, 28);
            label3.TabIndex = 5;
            label3.Text = "Commercial Bank of Dubai";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.Yellow;
            label4.Location = new Point(340, 273);
            label4.Name = "label4";
            label4.Size = new Size(123, 28);
            label4.TabIndex = 7;
            label4.Text = "1h 43m 10s";
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
            // label6
            // 
            label6.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label6.AutoSize = true;
            label6.BackColor = Color.Transparent;
            label6.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.White;
            label6.Location = new Point(484, 426);
            label6.Name = "label6";
            label6.Size = new Size(183, 23);
            label6.TabIndex = 8;
            label6.Text = "Thursday 28-Nov-2026";
            // 
            // MainMenu
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(15, 23, 42);
            ClientSize = new Size(670, 456);
            Controls.Add(label6);
            Controls.Add(label4);
            Controls.Add(label5);
            Controls.Add(label3);
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
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
    }
}