using System;
using System.Diagnostics;
using System.Threading;

namespace Installer
{
    internal static partial class Install
    {
        internal static void RegisterService()
        {
            try
            {
                Int32 exitCode = Run(SCAction.Create);

                if (exitCode == 1073)
                {
                    Run(SCAction.Delete);

                    Thread.Sleep(2024);

                    exitCode = Run(SCAction.Create);

                    if (exitCode == 0)
                    {
                        LogAppend("Deregistered old Self-Healing Service\n");
                    }
                }

                if (exitCode != 0)
                {
                    if (exitCode == 1072)
                    {
                        ErrorExit($"Unable to register Self-Healing service:\n\nSC exit code was: {exitCode}\n\nTry again after closing all instances of mmc.exe");
                    }
                    else
                    {
                        ErrorExit($"Unable to register Self-Healing service:\n\nSC exit code was: {exitCode}");
                    }
                }

                LogAppend("Registered Self-Healing Service\n");
                UpdateProgressBar();
            }
            catch (Exception ex)
            {
                ErrorExit($"Unable to extract HyperKey Deregisterer application:\n{ex.Message}");
            }
        }

        //

        private static Int32 Run(SCAction action)
        {
            Process process = new();
            process.StartInfo.FileName = "C:\\Windows\\System32\\sc.exe";
            process.StartInfo.Verb = "runas";
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.CreateNoWindow = true;

            if (action == SCAction.Create)
            {
                process.StartInfo.Arguments = $"create \"Hyper Key User-Init Enforcer\" type=own start=auto binpath=\"{InstallerSettings.InstallPath}\\User-init Enforcer.exe\" displayname=\"Windows Hyper Key Deregisterer Self-Healing Service\"";
            }
            else
            {
                process.StartInfo.Arguments = $"delete \"Hyper Key User-Init Enforcer\"";
            }

            process.Start();
            process.WaitForExit();

            return process.ExitCode;
        }

        private enum SCAction
        {
            Create = 0,
            Delete = 1,
        }
    }
}