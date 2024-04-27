using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Uninstaller
{
    public partial class MainWindow : Window
    {
        internal static Duration Duration = new(TimeSpan.FromSeconds(1));

        public MainWindow()
        {
            InitializeComponent();

            Pin.MainWindow = this;
        }

        private void UninstallButton_Click(object sender, RoutedEventArgs e)
        {
            UninstallButton.Click -= UninstallButton_Click;

            ThicknessAnimation bar = new();
            bar.Duration = Duration;
            bar.To = new(48, 0, 48, 48);
            bar.DecelerationRatio = 1;

            UninstallProgressBar.BeginAnimation(ProgressBar.MarginProperty, bar);

            ThicknessAnimation button = new();
            button.Duration = Duration;
            button.To = new(0, 0, 0, -32);
            button.AccelerationRatio = 1;

            DoubleAnimation opacity = new();
            opacity.Duration = Duration;
            opacity.To = 0;
            opacity.AccelerationRatio = 1;

            UninstallButton.BeginAnimation(Button.MarginProperty, button);
            UninstallButton.BeginAnimation(Button.OpacityProperty, opacity);

            ThicknessAnimation logBox = new();
            logBox.Duration = Duration;
            logBox.To = new(0, 32, 0, 0);
            logBox.DecelerationRatio = 1;

            LogBoxBorder.BeginAnimation(Border.MarginProperty, logBox);
        }


        private static Double CurrentProgressState;

        private static void UpdateProgressBar()
        {
            Pin.MainWindow.Dispatcher.Invoke(() =>
            {
                DoubleAnimation doubleAnimation = new();
                doubleAnimation.Duration = MainWindow.Duration;
                doubleAnimation.To = ++CurrentProgressState;
                doubleAnimation.DecelerationRatio = 1;

                Pin.MainWindow.UninstallProgressBar.BeginAnimation(ProgressBar.ValueProperty, doubleAnimation);
            });
        }
    }
}