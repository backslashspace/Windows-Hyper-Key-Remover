using System;
using System.Diagnostics;
using System.Reflection;

namespace Installer
{
    internal static class Pin
    {
        internal static MainWindow MainWindow;
    }

    internal static class Config
    {
        internal const String InstallPath = "C:\\Program Files\\Hyper Key Remover";

        internal static Boolean NeedsCleanUp;
    }
}