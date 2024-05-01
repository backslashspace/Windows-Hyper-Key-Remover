using System;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;

namespace Installer
{
    internal static class UI
    {
        internal static IntPtr MainWindowHandle;

        internal static MainWindow MainWindow;
        internal static Dispatcher Dispatcher;
    }

    internal static class UISettings
    {
        internal static Duration AnimationDuration = new(TimeSpan.FromSeconds(1));
    }

    internal static class InstallerSettings
    {
        internal const String InstallPath = "C:\\Program Files\\Hyper Key Remover";

        internal static Boolean NeedsCleanUp;
        internal static Boolean InstallSelfHealingService = true;

        internal static readonly Assembly assembly = Assembly.GetExecutingAssembly();

        internal const String UserInitRegexString = ",\\s*\"C:\\\\Program Files\\\\Hyper Key Remover\\\\HyperKey-Deregisterer\\.exe\"(\\s*,|\\s*\\z)";
        internal const String UserInitString = ", \"C:\\Program Files\\Hyper Key Remover\\HyperKey-Deregisterer.exe\",";
    }
}