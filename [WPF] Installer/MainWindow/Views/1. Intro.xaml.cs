using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Installer.Views
{
    public partial class Intro : UserControl
    {
        private static readonly SolidColorBrush ActionFieldColorGreen = new((Color)ColorConverter.ConvertFromString("#66ff66"));
        private static readonly Thickness ActionFieldColorGreenThickness = new(5, -6, 0, 0);
        private static readonly SolidColorBrush ActionFieldColorRed = new((Color)ColorConverter.ConvertFromString("#ff6666"));
        private static readonly Thickness ActionFieldColorRedThickness = new(6, -6, 0, 0);

        public Intro()
        {
            InitializeComponent();

            Loaded += CheckInstallPath;
            IsVisibleChanged += UpdateInstallPathState;
        }

        //

        private void CheckInstallPath(object sender, RoutedEventArgs e)
        {
            if (Directory.Exists(Config.InstallPath))
            {
                ActionField.Text = "-";
                ActionField.Foreground = ActionFieldColorRed;
                ActionField.Margin = ActionFieldColorRedThickness;

                Config.NeedsCleanUp = true;
            }
            else
            {
                ActionField.Text = "+";
                ActionField.Foreground = ActionFieldColorGreen;
                ActionField.Margin = ActionFieldColorGreenThickness;

                Config.NeedsCleanUp = false;
            }
        }

        private void UpdateInstallPathState(object sender, DependencyPropertyChangedEventArgs e)
        {
            UserControl userControl = sender as UserControl;

            if (userControl.Visibility != Visibility.Visible)
            {
                CheckInstallPath(null, null);
            }
        }

        private void Next_Button(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
            Pin.MainWindow.SelectView.Visibility = Visibility.Visible;
        }

        private void Cancel_Button(object sender, RoutedEventArgs e)
        {
            Pin.MainWindow.Close();
        }
    }
}