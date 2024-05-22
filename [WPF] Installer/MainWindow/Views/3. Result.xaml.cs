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

        internal static void InstallWorker()
        {
            SetProgressBarMaxValue();

            goto SKIP;

            if (InstallerSettings.NeedsCleanUp)
            {
                Install.CleanUp();
            }

            Install.ExtractHyperKeyRemover();

            Install.ApplyCustomUserInit();

            if (InstallerSettings.InstallSelfHealingService)
            {
                Install.ExtractServiceFiles();
                Install.RegisterService();
            }

            Install.ExtractUninstallerFiles();

            Install.RegisterWindowAsApp();

            RunProgram();

            // done

            SKIP:

            ShowFinishButton();
        }

        private static void SetProgressBarMaxValue()
        {
            Byte maxProgressValue;

            if (InstallerSettings.InstallSelfHealingService)
            {
                if (InstallerSettings.NeedsCleanUp) maxProgressValue = 7;
                else maxProgressValue = 6;
            }
            else
            {
                if (InstallerSettings.NeedsCleanUp) maxProgressValue = 5;
                else maxProgressValue = 4;
            }

            UI.Dispatcher.Invoke(() => UI.MainWindow.ResultView.InstallProgressBar.Maximum = maxProgressValue);
        }

        private static void RunProgram()
        {
            Process process = new();
            process.StartInfo.FileName = $"{InstallerSettings.InstallPath}\\HyperKey-Deregisterer.exe";
            process.StartInfo.Verb = "runas";
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            process.WaitForExit();

            Install.LogAppend("Applying to current session");
        }

        private static void ShowFinishButton()
        {
            UI.Dispatcher.Invoke(() =>
            {
                DoubleAnimation opacity = new();
                opacity.Duration = UISettings.AnimationDuration;
                opacity.To = 1;
                opacity.DecelerationRatio = 1;

                UI.MainWindow.ResultView.FinishButton.BeginAnimation(Button.OpacityProperty, opacity);
            });
        }

        private void FinishButton_Click(object sender, RoutedEventArgs e)
        {
            UI.MainWindow.Close();
        }
    }
}