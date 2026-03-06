namespace TimeTracker.Forms
{
    partial class Add
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
            txtProjectName = new TextBox();
            label1 = new Label();
            btnAdd = new Button();
            SuspendLayout();
            // 
            // txtProjectName
            // 
            txtProjectName.BackColor = Color.FromArgb(51, 65, 85);
            txtProjectName.BorderStyle = BorderStyle.FixedSingle;
            txtProjectName.Font = new Font("Segoe UI", 11F);
            txtProjectName.ForeColor = Color.FromArgb(241, 245, 249);
            txtProjectName.Location = new Point(23, 93);
            txtProjectName.Margin = new Padding(4, 5, 4, 5);
            txtProjectName.Name = "txtProjectName";
            txtProjectName.PlaceholderText = "Enter project name";
            txtProjectName.Size = new Size(455, 32);
            txtProjectName.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ButtonFace;
            label1.Location = new Point(23, 27);
            label1.Name = "label1";
            label1.Size = new Size(171, 38);
            label1.TabIndex = 4;
            label1.Text = "Add Project";
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.FromArgb(14, 165, 233);
            btnAdd.Cursor = Cursors.Hand;
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.FlatAppearance.MouseOverBackColor = Color.FromArgb(56, 189, 248);
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnAdd.ForeColor = Color.White;
            btnAdd.Location = new Point(311, 142);
            btnAdd.Margin = new Padding(4, 5, 4, 5);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(167, 40);
            btnAdd.TabIndex = 5;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;
            // 
            // Add
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(15, 23, 42);
            ClientSize = new Size(501, 227);
            Controls.Add(btnAdd);
            Controls.Add(label1);
            Controls.Add(txtProjectName);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "Add";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Add";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtProjectName;
        private Label label1;
        private Button btnAdd;
    }
}