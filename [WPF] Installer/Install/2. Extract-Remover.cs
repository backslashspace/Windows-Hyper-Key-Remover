using System;
using System.IO;

namespace Installer
{
    internal static partial class Install
    {
        internal static void ExtractHyperKeyRemover()
        {
            try
            {
                Directory.CreateDirectory(InstallerSettings.InstallPath);

                using (FileStream exeFile = File.Create($"{InstallerSettings.InstallPath}\\HyperKey-Deregisterer.exe"))
                {
                    InstallerSettings.assembly.GetManifestResourceStream("Installer.resources.HyperKey-Deregisterer.exe").CopyTo(exeFile);
                }

                using (FileStream confFile = File.Create($"{InstallerSettings.InstallPath}\\HyperKey-Deregisterer.exe.config"))
                {
                    InstallerSettings.assembly.GetManifestResourceStream("Installer.resources.HyperKey-Deregisterer.exe.config").CopyTo(confFile);
                }

                LogAppend("Extracted HyperKey Deregisterer\n");
                UpdateProgressBar();
            }
            catch (Exception ex)
            {
                ErrorExit($"Unable to extract HyperKey Deregisterer application:\n\n{ex.Message}");
            }
        }
    }
}