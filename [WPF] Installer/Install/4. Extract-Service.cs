using System;
using System.IO;

namespace Installer
{
    internal static partial class Install
    {
        internal static void ExtractServiceFiles()
        {
            try
            {
                using (FileStream exeFile = File.Create($"{InstallerSettings.InstallPath}\\User-init Enforcer.exe"))
                {
                    InstallerSettings.assembly.GetManifestResourceStream("Installer.resources.User-init Enforcer.exe").CopyTo(exeFile);

                }

                using (FileStream confFile = File.Create($"{InstallerSettings.InstallPath}\\User-init Enforcer.exe.config"))
                {
                    InstallerSettings.assembly.GetManifestResourceStream("Installer.resources.User-init Enforcer.exe.config").CopyTo(confFile);
                }

                LogAppend("Extracted Self-Healing service\n");
                UpdateProgressBar();
            }
            catch (Exception ex)
            {
                ErrorExit($"Unable to extract self healing service:\n\n{ex.Message}");
            }
        }
    }
}