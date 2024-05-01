using System;
using System.IO;

namespace Installer
{
    internal static partial class Install
    {
        internal static void ExtractUninstallerFiles()
        {
            try
            {
                using (FileStream exeFile = File.Create($"{InstallerSettings.InstallPath}\\HyperKey Deregisterer Uninstaller.exe"))
                {
                    InstallerSettings.assembly.GetManifestResourceStream("Installer.resources.HyperKey Deregisterer Uninstaller.exe").CopyTo(exeFile);
                }

                using (FileStream confFile = File.Create($"{InstallerSettings.InstallPath}\\HyperKey Deregisterer Uninstaller.exe.config"))
                {
                    InstallerSettings.assembly.GetManifestResourceStream("Installer.resources.HyperKey Deregisterer Uninstaller.exe.config").CopyTo(confFile);
                }

                LogAppend("Extracted uninstaller\n");
                UpdateProgressBar();
            }
            catch (Exception ex)
            {
                ErrorExit($"Unable to extract uninstaller:\n\n{ex.Message}");
            }
        }
    }
}