using System.IO;
using System.Text;

namespace EclipseBoxInstaller.Services
{
    public class ConfigGenerator
    {
        public void CreatePreFlightConfig(string installPath)
        {
            string configPath = Path.Combine(installPath, "prismlauncher.cfg");

            // 1. Path to our portable Java
            string javaPath = "Java21/bin/javaw.exe";

            // 2. Define Optimized JVM Arguments
            // These are my preferred args for Java 21 + ZGC. Adjust as needed.
            string jvmArgs = "-XX:+UseZGC -XX:+ZGenerational -XX:+AlwaysPreTouch -XX:+DisableExplicitGC -XX:+PerfDisableSharedMem -XX:-ZUncommit";

            StringBuilder sb = new StringBuilder();

            // --- GENERAL SETTINGS ---
            sb.AppendLine("[General]");
            sb.AppendLine("Language=en_US");
            // We keep this false, but Prism might trigger the wizard anyway for Microsoft Account Auth
            sb.AppendLine("FirstStart=false");

            // --- JAVA SETTINGS ---
            sb.AppendLine($"JavaPath={javaPath}");

            // RAM Settings (MB)
            sb.AppendLine("MinMemAlloc=6144");
            sb.AppendLine("MaxMemAlloc=6144"); // 6GB Default

            // --- THE FIX ---
            // The key must be "JvmArgs", not "JavaArgs"
            sb.AppendLine($"JvmArgs={jvmArgs}");

            // --- UI SETTINGS ---
            sb.AppendLine("ConsoleWindow=false"); // Hide the debug console
            sb.AppendLine("IconTheme=org.kde.breeze.desktop"); // Fixes missing icons

            // This ensures Prism doesn't nag about "Java 8 is missing" if you only have 21
            sb.AppendLine("CheckJava=false");

            File.WriteAllText(configPath, sb.ToString());
        }

        // --- OPTIONAL: INSTANCE SPECIFIC CONFIG ---
        // If the arguments still don't show up, call this method *after* you extract the modpack.
        // It forces the settings onto the specific instance, overriding any global defaults.
        public void CreateInstanceConfig(string instancePath)
        {
            string configPath = Path.Combine(instancePath, "instance.cfg");
            string jvmArgs = "-XX:+UseZGC -XX:+ZGenerational -XX:+AlwaysPreTouch -XX:+DisableExplicitGC -XX:+PerfDisableSharedMem -XX:-ZUncommit";

            // If the file exists, append/overwrite specific lines, 
            // but for a fresh installer, we can usually just append these overrides.
            StringBuilder sb = new StringBuilder();

            // You must tell Prism to override the global settings for this specific instance
            sb.AppendLine("OverrideJavaArgs=true");
            sb.AppendLine($"JvmArgs={jvmArgs}");

            sb.AppendLine("OverrideMemory=true");
            sb.AppendLine("MinMemAlloc=6144");
            sb.AppendLine("MaxMemAlloc=6144");

            // Append to existing instance.cfg if it exists, or create new
            if (File.Exists(configPath))
            {
                File.AppendAllText(configPath, sb.ToString());
            }
            else
            {
                File.WriteAllText(configPath, sb.ToString());
            }
        }
    }
}