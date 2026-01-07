using EclipseBoxInstaller.Core;
using EclipseBoxInstaller.Services;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace EclipseBoxInstaller
{
    public partial class Form1 : Form
    {
        private readonly DownloaderService _downloader;
        private readonly ExtractorService _extractor;
        private readonly ConfigGenerator _configGenerator;
        private readonly ShortcutCreator _shortcutCreator;

        public Form1()
        {
            InitializeComponent();

            _downloader = new DownloaderService();
            _extractor = new ExtractorService();
            _configGenerator = new ConfigGenerator();
            _shortcutCreator = new ShortcutCreator();

            // 1. Set a default path, but allow the user to change it
            string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            txtInstallPath.Text = Path.Combine(localAppData, Constants.InstallFolderName);

            // Enable the install button now that we have a path
            btnInstall.Enabled = true;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (var folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Select a location to install eclipseBox";
                folderDialog.ShowNewFolderButton = true;

                // If the user picks a folder, update the text box
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    // Append our specific folder name to whatever they picked so we don't clutter their root
                    // e.g. D:\Games -> D:\Games\eclipseBox
                    string selectedPath = folderDialog.SelectedPath;
                    if (!selectedPath.EndsWith(Constants.InstallFolderName, StringComparison.OrdinalIgnoreCase))
                    {
                        selectedPath = Path.Combine(selectedPath, Constants.InstallFolderName);
                    }

                    txtInstallPath.Text = selectedPath;
                }
            }
        }

        private async void btnInstall_Click(object sender, EventArgs e)
        {
            try
            {
                // 2. Use the path from the Text Box (User choice)
                string targetPath = txtInstallPath.Text;

                if (string.IsNullOrWhiteSpace(targetPath))
                {
                    MessageBox.Show("Please select a valid install location.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                btnInstall.Enabled = false;
                btnBrowse.Enabled = false; // Lock the browse button too
                btnInstall.Text = "Installing...";

                // Start the workflow using the USER'S path
                await _downloader.DownloadAllFilesAsync(targetPath, UpdateProgress);

                lblStatus.Text = "Extracting files...";
                progressBar.Style = ProgressBarStyle.Marquee;

                await _extractor.ExtractAndSetupAsync(targetPath, (status) =>
                {
                    Invoke(new Action(() => lblStatus.Text = status));
                });

                lblStatus.Text = "Configuring launcher...";
                _configGenerator.CreatePreFlightConfig(targetPath);

                lblStatus.Text = "Creating shortcut...";
                _shortcutCreator.CreateDesktopShortcut(targetPath);

                // --- CHANGED SECTION STARTS HERE ---

                // 1. Update status to Complete
                lblStatus.Text = "Installation Complete!";
                progressBar.Value = 100;
                progressBar.Style = ProgressBarStyle.Blocks;

                // 2. Prepare the launch command (but don't run it yet)
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = Path.Combine(targetPath, Constants.LauncherExecutable);

                // This argument tells Prism: "Open, skip setup, and import this file immediately"
                startInfo.Arguments = $"--import \"{Path.Combine(targetPath, Constants.ModpackFileName)}\"";

                // CRITICAL: Set the Working Directory so Prism finds the "Java21" folder relative to itself
                startInfo.WorkingDirectory = targetPath;

                // 3. SHOW MESSAGE BOX FIRST (Code pauses here until user clicks OK)
                MessageBox.Show("EclipseBox has been successfully installed!\n\nClick 'OK' to launch the application.",
                                "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 4. User clicked OK, now we launch Prism
                if (File.Exists(startInfo.FileName))
                {
                    Process.Start(startInfo);
                }

                // 5. Close the Installer
                Application.Exit();

                // --- CHANGED SECTION ENDS HERE ---
            }
            catch (Exception ex)
            {
                progressBar.Style = ProgressBarStyle.Blocks;
                lblStatus.Text = "Error";
                MessageBox.Show($"Installation Failed:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                btnInstall.Enabled = true;
                btnBrowse.Enabled = true;
                btnInstall.Text = "Install";
            }
        }

        private void UpdateProgress(string statusText, int percentage)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string, int>(UpdateProgress), statusText, percentage);
                return;
            }

            lblStatus.Text = statusText;
            progressBar.Value = percentage;
        }
    }
}