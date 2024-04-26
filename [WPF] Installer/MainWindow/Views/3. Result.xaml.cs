using System;
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
                    Pin.MainWindow.Dispatcher.Invoke(() => Pin.MainWindow.ResultView.InstallProgressBar.Maximum = 13);
                }
                else
                {
                    Pin.MainWindow.Dispatcher.Invoke(() => Pin.MainWindow.ResultView.InstallProgressBar.Maximum = 12);
                }
            }
            else
            {
                if (Config.NeedsCleanUp)
                {
                    Pin.MainWindow.Dispatcher.Invoke(() => Pin.MainWindow.ResultView.InstallProgressBar.Maximum = 9);
                }
                else
                {
                    Pin.MainWindow.Dispatcher.Invoke(() => Pin.MainWindow.ResultView.InstallProgressBar.Maximum = 8);
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

            InstallerWorker.RegisterWindowAsApp();

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