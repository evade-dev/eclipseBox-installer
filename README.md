# eclipseBox Installer

**Automated installer that downloads Prism Launcher, JRE 21, and my modpack to create a portable Minecraft instance.**

### What this does
This program handles the setup so you don't have to manually install and configure Prism Launcher or Java. It will:
1. Download the portable version of **Prism Launcher**.
2. Download **Java 21 (JRE)** required to run the game.
3. Download and import the **Modpack** automatically.
4. Configure everything into a single portable folder.

### What this doesn't do
* Most importantly: It does **not** contain any files from Prism Launcher, Java, the Modpack itself, or Minecraft. It simply automates the download and setup process.
* It does **not** install Minecraft itself. You will be able to download Minecraft assets after logging in to Prism Launcher with your Microsoft/Minecraft account.
* It does **not** install Java system-wide. The Java runtime will be contained within the eclipseBox folder.
* It does **not** modify any system settings or files outside of the selected installation folder.

### Prerequisites
* A Windows PC with an internet connection.
* Sufficient disk space (at least 5 GB free).
* Permissions to run executables and install software on your machine.
* Microsoft/Minecraft account to log in to Prism Launcher.

---

### How to Install

1. Go to the [Releases Page](../../releases/latest) (on the right side of this screen).
2. Download the `eclipseBox Installer.exe`.
3. Double-click to run.
4. Click on Browse and select the folder where you want to install eclipseBox.
    * It is recommended to use a new, empty folder.
5. Click Install and wait for the process to complete.
6. Proceed with the Prism Launcher setup as described below.

---

### Important: You must Sign In to Prism Launcher with your Microsoft/Minecraft account!
This installer configures the *files*, but it cannot log you in.
Once the installer finishes downloading and installing files, Prism Launcher will open and begin setup.
During this initial setup, you will follow these steps:
1.  Click next on the first window showing memory and Java settings.
2.  Setup your themes settings and click next.
3.  Click **Add Microsoft**.
4.  Follow the prompts to sign in with your Microsoft/Minecraft account.
5.  Once signed in, the application will proceed to importing the modpack.

---

### "Windows protected your PC" Warning?
Because this is a custom tool made just for our server and isn't digitally signed by Microsoft, Windows might try to block it.

If you see a blue popup:
1. Click **"More info"**.
2. Click **"Run anyway"**.

---

### Credits & Legal
This installer is a fan-made utility. It is not affiliated with, endorsed by, or connected to **Mojang Studios**, **Microsoft**, the **Prism Launcher Team**, or **Eclipse Adoptium**.

* **Prism Launcher:** An open-source Minecraft launcher. Licensed under GPLv3.
    * [Website](https://prismlauncher.org/) | [GitHub](https://github.com/PrismLauncher/PrismLauncher)
* **Java 21 (Temurin):** Provided by Eclipse Adoptium.
    * [Website](https://adoptium.net/)