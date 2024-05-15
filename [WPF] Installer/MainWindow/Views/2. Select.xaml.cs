using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace Installer.Views
{
    public partial class Select : UserControl
    {
        public Select()
        {
            InitializeComponent();
        }

        private void Next_Button(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
            UI.MainWindow.ResultView.Visibility = Visibility.Visible;

            InstallerSettings.InstallSelfHealingService = (Boolean)SelfHealingCheckBox.IsChecked;

            Thread thread = new(() => Result.InstallWorker());
            thread.Start();
        }

        private void Back_Button(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
            UI.MainWindow.IntroView.Visibility = Visibility.Visible;
        }
    }
}