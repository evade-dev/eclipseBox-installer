namespace EclipseBoxInstaller
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblTitle = new Label();
            label1 = new Label();
            txtInstallPath = new TextBox();
            btnBrowse = new Button();
            btnInstall = new Button();
            progressBar = new ProgressBar();
            lblStatus = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(12, 9);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(182, 25);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "eclipsebox Installer";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(41, 92);
            label1.Name = "label1";
            label1.Size = new Size(90, 15);
            label1.TabIndex = 1;
            label1.Text = "Install Location:";
            // 
            // txtInstallPath
            // 
            txtInstallPath.Location = new Point(132, 88);
            txtInstallPath.Name = "txtInstallPath";
            txtInstallPath.ReadOnly = true;
            txtInstallPath.Size = new Size(300, 23);
            txtInstallPath.TabIndex = 2;
            // 
            // btnBrowse
            // 
            btnBrowse.Location = new Point(132, 62);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(75, 25);
            btnBrowse.TabIndex = 3;
            btnBrowse.Text = "Browse...";
            btnBrowse.UseVisualStyleBackColor = true;
            btnBrowse.Click += btnBrowse_Click;
            // 
            // btnInstall
            // 
            btnInstall.Enabled = false;
            btnInstall.Location = new Point(397, 144);
            btnInstall.Name = "btnInstall";
            btnInstall.Size = new Size(75, 25);
            btnInstall.TabIndex = 4;
            btnInstall.Text = "Install";
            btnInstall.UseVisualStyleBackColor = true;
            btnInstall.Click += btnInstall_Click;
            // 
            // progressBar
            // 
            progressBar.Location = new Point(12, 176);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(460, 25);
            progressBar.TabIndex = 5;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(11, 152);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(39, 15);
            lblStatus.TabIndex = 6;
            lblStatus.Text = "Ready";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(303, 9);
            label2.Name = "label2";
            label2.Size = new Size(169, 15);
            label2.TabIndex = 7;
            label2.Text = "This will install Prism Launcher";
            label2.TextAlign = ContentAlignment.TopCenter;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(324, 24);
            label3.Name = "label3";
            label3.Size = new Size(148, 15);
            label3.TabIndex = 8;
            label3.Text = "preconfigured with Java 21";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(335, 39);
            label4.Name = "label4";
            label4.Size = new Size(137, 15);
            label4.TabIndex = 9;
            label4.Text = "and the server modpack.";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(484, 211);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(lblStatus);
            Controls.Add(progressBar);
            Controls.Add(btnInstall);
            Controls.Add(btnBrowse);
            Controls.Add(txtInstallPath);
            Controls.Add(label1);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "eclipsebox Installer";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Label label1;
        private TextBox txtInstallPath;
        private Button btnBrowse;
        private Button btnInstall;
        private ProgressBar progressBar;
        private Label lblStatus;
        private Label label2;
        private Label label3;
        private Label label4;
    }
}
