using System.IO;
using Microsoft.Win32;
using System.ServiceProcess;
using System;
using System.Text.RegularExpressions;

namespace Uninstaller
{
    internal static partial class Uninstaller
    {
        private static void GetInstalledComponentInfo()
        {
            if (Directory.Exists(InstallDirectory))
            {
                InstalledComponents.Directory = true;

                if (File.Exists(InstallDirectory + "\\HyperKey-Deregisterer.exe"))
                {
                    InstalledComponents.Remover_exe = true;
                }

                if (File.Exists(InstallDirectory + "\\HyperKey-Deregisterer.exe.config"))
                {
                    InstalledComponents.Remover_exe_config = true;
                }

                //

                if (File.Exists(InstallDirectory + "\\User-init Enforcer.exe"))
                {
                    InstalledComponents.Service_exe = true;
                }

                if (File.Exists(InstallDirectory + "\\User-init Enforcer.exe.config"))
                {
                    InstalledComponents.Service_exe_config = true;
                }
            }

            try
            {
                Object rawUserInitString = Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon", "Userinit", null);

                Match match = Regex.Match((String)rawUserInitString, UserInitRegex, RegexOptions.IgnoreCase);

                if (match.Success)
                {
                    InstalledComponents.UserInit = true;
                }
            }
            catch { }

            try
            {
                using (ServiceController sc = new("Hyper Key User-Init Enforcer"))
                {
                    if (sc.Status != ServiceControllerStatus.Stopped)
                    {
                        sc.Stop();
                    }
                }

                InstalledComponents.Service_deregister = true;
            }
            catch { }

            try
            {
                InstalledComponents.WindowsApp_Deregister = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall", true);
            
                if (InstalledComponents.WindowsApp_Deregister.OpenSubKey("Hyper Key Deregisterer", false) == null)
                {
                    InstalledComponents.WindowsApp_Deregister = null;
                }
            }
            catch { }
        }
    }
}