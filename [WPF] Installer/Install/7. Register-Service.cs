using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Media;

namespace Installer
{
    internal static partial class InstallerWorker
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
                    Pin.MainWindow.Dispatcher.Invoke(() =>
                    {
                        Pin.MainWindow.ResultView.InstallProgressBar.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ffcc00"));

                        MessageBoxWindow.MessageBox messageBox;

                        if (exitCode == 1072)
                        {
                            messageBox = new("SC.exe error", $"Unable to register Self-Healing service:\n\nSC exit code was: {exitCode}\n\nTry again after closing all instances of mmc.exe", MessageBoxWindow.MessageBox.Icons.Circle_Error, "Exit");
                        }
                        else
                        {
                            messageBox = new("SC.exe error", $"Unable to register Self-Healing service:\n\nSC exit code was: {exitCode}", MessageBoxWindow.MessageBox.Icons.Circle_Error, "Exit");
                        }

                        messageBox.ShowDialog();
                    });

                    Environment.Exit(-1);
                }

                LogAppend("Registered Self-Healing Service\n");
                UpdateProgressBar();
            }
            catch (Exception ex)
            {
                Pin.MainWindow.Dispatcher.Invoke(() =>
                {
                    Pin.MainWindow.ResultView.InstallProgressBar.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ffcc00"));

                    MessageBoxWindow.MessageBox messageBox = new("Error", $"Unable to extract HyperKey Deregisterer application:\n{ex.Message}", MessageBoxWindow.MessageBox.Icons.Circle_Error, "Exit");

                    messageBox.ShowDialog();
                });

                Environment.Exit(-1);
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
                process.StartInfo.Arguments = $"create \"Hyper Key User-Init Enforcer\" type=own start=auto binpath=\"{Config.InstallPath}\\User-init Enforcer.exe\" displayname=\"Windows Hyper Key Deregisterer Self-Healing Service\"";
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