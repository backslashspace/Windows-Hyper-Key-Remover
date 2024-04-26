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

            CheckBoxAnimator.Initialize();
            CheckBoxAnimator.HookChild(ref SelfHealingCheckBox);
        }

        private void Next_Button(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
            Pin.MainWindow.ResultView.Visibility = Visibility.Visible;

            Boolean useSelfHealing = (Boolean)SelfHealingCheckBox.IsChecked;

            Thread thread = new(() => Result.InstallWorker(useSelfHealing));
            thread.Start();
        }

        private void Back_Button(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
            Pin.MainWindow.IntroView.Visibility = Visibility.Visible;
        }
    }
}