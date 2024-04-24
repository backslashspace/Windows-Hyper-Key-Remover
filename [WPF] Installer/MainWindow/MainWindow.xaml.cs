using System;
using System.Windows;

namespace Installer
{
    public partial class MainWindow : Window
    {
        internal static IntPtr WindowHandle { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            Pin.MainWindow = this;
            Loaded += InitThemeAwareness;

            Title += Program.AssemblyFileVersion;
        }

        private void InitThemeAwareness(object sender, RoutedEventArgs e)
        {
            WindowHandle = new System.Windows.Interop.WindowInteropHelper(this).Handle;

            ThemeAwareness.StartExternalEventListener();
        }
    }
}