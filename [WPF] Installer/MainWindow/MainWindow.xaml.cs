using System.Windows;
using static Installer.ThemeAwareness.ThemeData;

namespace Installer
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            UI.MainWindow = this;
            UI.Dispatcher = Dispatcher;

            ButtonAnimator.Initialize();
            ApplyButtonAnimator();

            Loaded += InitializeApplicationThemeAwareness;

            Title += $"[{Program.AssemblyInformationalVersion} | Build {Program.AssemblyFileVersion.Revision}]";
        }

        private void ApplyButtonAnimator()
        {
            ButtonAnimator.PrimaryButton.HookChild(ref IntroView.NextButton);
            ButtonAnimator.PrimaryButton.HookChild(ref SelectView.NextButton);

            ButtonAnimator.SecondaryButton.HookChild(ref IntroView.CancelButton);
        }

        private void InitializeApplicationThemeAwareness(object sender, RoutedEventArgs e)
        {
            UI.MainWindowHandle = new System.Windows.Interop.WindowInteropHelper(this).Handle;

            DWMAPI.Initialize();
            ThemeAwareness.Initialize();
        }
    }
}