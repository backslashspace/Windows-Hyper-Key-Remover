using System;
using System.Windows;

namespace Installer
{
    public partial class MainWindow : Window
    {
        internal static IntPtr WindowHandle { get; private set; }

        internal static Duration Duration = new(TimeSpan.FromSeconds(1));

        public MainWindow()
        {
            InitializeComponent();
            Pin.MainWindow = this;

            Loaded += InitThemeAwareness;

            Title += $"[{Program.AssemblyInformationalVersion} | Build {Program.AssemblyFileVersion.Revision}]";
        }

        private void InitThemeAwareness(object sender, RoutedEventArgs e)
        {
            WindowHandle = new System.Windows.Interop.WindowInteropHelper(this).Handle;

            ThemeAwareness.StartExternalEventListener();
        }
    }
}