namespace EclipseBoxInstaller.Core
{
    public static class Constants
    {
        // Official Prism Launcher GitHub Release (Portable)
        public const string PrismDownloadUrl = "https://github.com/PrismLauncher/PrismLauncher/releases/download/10.0.0/PrismLauncher-Windows-MSVC-Portable-10.0.0.zip";

        // Official Java 21 (Windows x64)
        public const string JavaDownloadUrl = "https://github.com/adoptium/temurin21-binaries/releases/download/jdk-21.0.9%2B10/OpenJDK21U-jre_x64_windows_hotspot_21.0.9_10.zip";

        // Modpack (Exported as .mrpack from Prism)
        // MAKE SURE to change the dl=0 to dl=1 for direct download when using a dropbox link
        public const string ModpackUrl = "https://www.dropbox.com/scl/fi/pja1rj96y1o94jvvvvf1l/MyModpack.mrpack?rlkey=033f5r133sl8mlethvhyphwnt&st=cmby9yrv&dl=1";

        public const string InstallFolderName = "eclipseBox";
        public const string LauncherExecutable = "PrismLauncher.exe";

        // Need specific filenames to track the downloads
        public const string PrismZipName = "PrismLauncher.zip";
        public const string JavaZipName = "Java21.zip";
        public const string ModpackFileName = "MyModpack.mrpack";
    }
}