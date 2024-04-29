using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Installer.Views
{
    public partial class Result : UserControl
    {
        public Result()
        {
            InitializeComponent();
        }

        internal static void InstallWorker(Boolean installService)
        {
            if (installService)
            {
                if (Config.NeedsCleanUp)
                {
                    Pin.MainWindow.Dispatcher.Invoke(() => Pin.MainWindow.ResultView.InstallProgressBar.Maximum = 7);
                }
                else
                {
                    Pin.MainWindow.Dispatcher.Invoke(() => Pin.MainWindow.ResultView.InstallProgressBar.Maximum = 6);
                }
            }
            else
            {
                if (Config.NeedsCleanUp)
                {
                    Pin.MainWindow.Dispatcher.Invoke(() => Pin.MainWindow.ResultView.InstallProgressBar.Maximum = 5);
                }
                else
                {
                    Pin.MainWindow.Dispatcher.Invoke(() => Pin.MainWindow.ResultView.InstallProgressBar.Maximum = 4);
                }
            }

            if (Config.NeedsCleanUp)
            {
                InstallerWorker.CleanUp();
            }

            InstallerWorker.ExtractHyperKeyRemover();

            InstallerWorker.ApplyCustomUserInit();

            if (installService)
            {
                InstallerWorker.ExtractServiceFiles();
                InstallerWorker.RegisterService();
            }

            InstallerWorker.ExtractUninstallerFiles();

            InstallerWorker.RegisterWindowAsApp();

            // apply apply to current session

            Process process = new();
            process.StartInfo.FileName = $"{Config.InstallPath}\\HyperKey-Deregisterer.exe";
            process.StartInfo.Verb = "runas";
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            process.WaitForExit();

            InstallerWorker.LogAppend("Applying to current session");

            // done

            ShowFinishButton();
        }

        private static void ShowFinishButton()
        {
            Pin.MainWindow.Dispatcher.Invoke(() =>
            {
                DoubleAnimation opacity = new();
                opacity.Duration = MainWindow.Duration;
                opacity.To = 1;
                opacity.DecelerationRatio = 1;

                ThicknessAnimation margin = new();
                margin.Duration = MainWindow.Duration;
                margin.To = new(0, 0, 48, 32);
                margin.DecelerationRatio = 1;

                Pin.MainWindow.ResultView.FinishButton.BeginAnimation(Button.OpacityProperty, opacity);
                Pin.MainWindow.ResultView.FinishButton.BeginAnimation(Button.MarginProperty, margin);
            });
        }

        private void Finish(object sender, RoutedEventArgs e)
        {
            Pin.MainWindow.Close();
        }
    }
}