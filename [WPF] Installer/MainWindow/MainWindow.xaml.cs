using System.Windows;

namespace Installer
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            UI.MainWindow = this;
            UI.Dispatcher = Dispatcher;

            Loaded += InitializeApplicationThemeAwareness;

            Title += $"[{Program.AssemblyInformationalVersion} | Build {Program.AssemblyFileVersion.Revision}]";
        }

        private void InitializeApplicationThemeAwareness(object sender, RoutedEventArgs e)
        {
            UI.MainWindowHandle = new System.Windows.Interop.WindowInteropHelper(this).Handle;

            DWMAPI.Initialize();
            ThemeAwareness.Initialize();
        }
    }
}