using System;
using System.IO;
using EclipseBoxInstaller.Core;

namespace EclipseBoxInstaller.Services
{
    public class ShortcutCreator
    {
        public void CreateDesktopShortcut(string installRootPath)
        {
            try
            {
                // 1. Calculate paths
                string targetExe = Path.Combine(installRootPath, Constants.LauncherExecutable);
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string shortcutLocation = Path.Combine(desktopPath, "Prism Launcher.lnk");

                // 2. Create the shell object dynamically (Late Binding)
                // This bypasses the need for the specific COM reference in the project.
                Type shellType = Type.GetTypeFromProgID("WScript.Shell");
                dynamic shell = Activator.CreateInstance(shellType);

                // 3. Create the shortcut
                dynamic shortcut = shell.CreateShortcut(shortcutLocation);

                // 4. Set properties
                shortcut.TargetPath = targetExe;
                shortcut.WorkingDirectory = installRootPath;
                shortcut.Description = "Launch Prism Launcher";

                // 5. Save
                shortcut.Save();
            }
            catch (Exception ex)
            {
                // If shortcut creation fails, we don't want to crash the whole installer.
                // We just log it or ignore it since the game is already installed.
                Console.WriteLine($"Failed to create shortcut: {ex.Message}");
            }
        }
    }
}