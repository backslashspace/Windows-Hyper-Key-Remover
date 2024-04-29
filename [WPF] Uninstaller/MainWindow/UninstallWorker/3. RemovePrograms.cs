using System.Diagnostics;
using System.IO;

namespace Uninstaller
{
    internal static partial class Uninstaller
    {
        private static void RemovePrograms()
        {
            Process process = new();
            process.StartInfo.FileName = "C:\\Windows\\System32\\taskkill.exe";
            process.StartInfo.Verb = "runas";
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.Arguments = "/im HyperKey-Deregisterer.exe /f";
            process.Start();
            process.WaitForExit();

            process.StartInfo.Arguments = "/im 'User-init Enforcer.exe' /f";
            process.Start();
            process.WaitForExit();

            if (InstalledComponents.Remover_exe)
            {
                try
                {
                    File.Delete(InstallDirectory + "\\HyperKey-Deregisterer.exe");

                    MainWindow.LogAppend("Removed: " + InstallDirectory + "\\HyperKey-Deregisterer.exe\n");
                    MainWindow.UpdateProgressBar();
                }
                catch
                {
                    MainWindow.LogAppend("! Failed to remove:" + InstallDirectory + "\\HyperKey-Deregisterer.exe\n");
                }
            }

            if (InstalledComponents.Remover_exe_config)
            {
                try
                {
                    File.Delete(InstallDirectory + "\\HyperKey-Deregisterer.exe.config");
                    MainWindow.UpdateProgressBar();
                }
                catch { }
            }

            if (InstalledComponents.Service_exe)
            {
                try
                {
                    File.Delete(InstallDirectory + "\\User-init Enforcer.exe");

                    MainWindow.LogAppend("Removed: " + InstallDirectory + "\\User-init Enforcer.exe\n");
                    MainWindow.UpdateProgressBar();
                }
                catch
                {
                    MainWindow.LogAppend("! Failed to remove:" + InstallDirectory + "\\User-init Enforcer.exe\n");
                }
            }

            if (InstalledComponents.Service_exe_config)
            {
                try
                {
                    File.Delete(InstallDirectory + "\\User-init Enforcer.exe.config");

                    MainWindow.UpdateProgressBar();
                }
                catch { }                
            }
        }
    }
}