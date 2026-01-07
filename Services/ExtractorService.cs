using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using EclipseBoxInstaller.Core;

namespace EclipseBoxInstaller.Services
{
    public class ExtractorService
    {
        public Task ExtractAndSetupAsync(string installPath, Action<string> updateStatus)
        {
            return Task.Run(() =>
            {
                // --- STEP 1: PRISM LAUNCHER ---
                updateStatus("Installing Launcher...");
                string prismZipPath = Path.Combine(installPath, Constants.PrismZipName);
                string prismTemp = Path.Combine(installPath, "PrismTemp");

                if (Directory.Exists(prismTemp)) Directory.Delete(prismTemp, true);
                Directory.CreateDirectory(prismTemp);

                ZipFile.ExtractToDirectory(prismZipPath, prismTemp);

                // Flatten the folder structure (move everything to install root)
                MoveContentsToRoot(prismTemp, installPath);

                Directory.Delete(prismTemp, true);
                File.Delete(prismZipPath);

                // --- STEP 2: JAVA (The Critical Fix) ---
                updateStatus("Installing Java...");
                string javaZipPath = Path.Combine(installPath, Constants.JavaZipName);
                string javaTemp = Path.Combine(installPath, "JavaTemp");

                if (Directory.Exists(javaTemp)) Directory.Delete(javaTemp, true);
                Directory.CreateDirectory(javaTemp);

                ZipFile.ExtractToDirectory(javaZipPath, javaTemp);

                // HUNT FOR JAVA: Don't assume folder names. Find "bin/javaw.exe"
                string javawPath = Directory.GetFiles(javaTemp, "javaw.exe", SearchOption.AllDirectories).FirstOrDefault();

                if (javawPath != null)
                {
                    // Found it! Now determine the "Home" folder (the parent of the bin folder)
                    // javawPath = .../JavaTemp/jdk-21.0.9/bin/javaw.exe
                    // binDir    = .../JavaTemp/jdk-21.0.9/bin
                    // javaHome  = .../JavaTemp/jdk-21.0.9  <-- We want to move THIS

                    string binDir = Directory.GetParent(javawPath).FullName;
                    string javaHome = Directory.GetParent(binDir).FullName;

                    string finalJavaDest = Path.Combine(installPath, "Java21");

                    // Nuke old java if exists
                    if (Directory.Exists(finalJavaDest)) Directory.Delete(finalJavaDest, true);

                    // Move the detected JDK root to "Java21"
                    Directory.Move(javaHome, finalJavaDest);
                }
                else
                {
                    throw new FileNotFoundException("Could not find javaw.exe in the downloaded Java zip!");
                }

                Directory.Delete(javaTemp, true);
                File.Delete(javaZipPath);
            });
        }

        private void MoveContentsToRoot(string sourceDir, string destDir)
        {
            var directories = Directory.GetDirectories(sourceDir);
            var files = Directory.GetFiles(sourceDir);

            // If the zip contained a single wrapper folder (e.g. "PrismLauncher-10.0"), dive inside it
            if (directories.Length == 1 && files.Length == 0)
            {
                string innerDir = directories[0];
                foreach (var dir in Directory.GetDirectories(innerDir))
                {
                    string folderName = Path.GetFileName(dir);
                    string destPath = Path.Combine(destDir, folderName);
                    if (Directory.Exists(destPath)) Directory.Delete(destPath, true);
                    Directory.Move(dir, destPath);
                }
                foreach (var file in Directory.GetFiles(innerDir))
                {
                    string destPath = Path.Combine(destDir, Path.GetFileName(file));
                    if (File.Exists(destPath)) File.Delete(destPath);
                    File.Move(file, destPath);
                }
            }
            else
            {
                // Already flat
                foreach (var dir in directories)
                {
                    string folderName = Path.GetFileName(dir);
                    string destPath = Path.Combine(destDir, folderName);
                    if (Directory.Exists(destPath)) Directory.Delete(destPath, true);
                    Directory.Move(dir, destPath);
                }
                foreach (var file in files)
                {
                    string destPath = Path.Combine(destDir, Path.GetFileName(file));
                    if (File.Exists(destPath)) File.Delete(destPath);
                    File.Move(file, destPath);
                }
            }
        }
    }
}