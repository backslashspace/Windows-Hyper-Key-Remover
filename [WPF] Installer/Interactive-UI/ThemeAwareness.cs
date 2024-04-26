using System;
using System.Management;
using System.Windows;

namespace Installer
{
    internal static class ThemeAwareness
    {
        internal struct CurrentColors
        {
            internal static Byte[] RawPalette;
        }


        internal static Boolean? AppsUseLightTheme { get; private set; } = null;

        internal static void StartExternalEventListener()
        {
            Init_AppsUseLightTheme();

            Init_WindowsAccentColor();
        }

        private static void Init_WindowsAccentColor()
        {
            
        }

        #region Dark/LightMode
        private static void Init_AppsUseLightTheme()
        {
            if (AppsUseLightTheme != null) { return; }

            DWMAPI.GetWindowsBuildNumber();

            if (DWMAPI.InternalWindowsBuildNumber < 17763)
            {
                AppsUseLightTheme = true;

                return;
            }

            AppsUseLightThemeChanged.Invoke(null, null);

            RegistryEvents_CurrentUser appDarkModeEvent = new(@"Software\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize", "AppsUseLightTheme", AppsUseLightThemeChanged);
        }

        private static Action<object, EventArrivedEventArgs> AppsUseLightThemeChanged = delegate (object sender, EventArrivedEventArgs e)
        {
            try
            {
                Object rawAppsUseLightTheme = Microsoft.Win32.Registry.GetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize", "AppsUseLightTheme", null);

                if (rawAppsUseLightTheme is Int32 i && i == 0)
                {
                    AppsUseLightTheme = false;
                }
                else
                {
                    AppsUseLightTheme = true;
                }
            }
            catch
            {
                AppsUseLightTheme = true;
            }

            DWMAPI.SetTheme(MainWindow.WindowHandle, !(Boolean)AppsUseLightTheme);

            //update window if win older than win 11
            if (DWMAPI.InternalWindowsBuildNumber < 22000)
            {
                UpdateWindow();
            }
        }; 
        #endregion

        private static void UpdateWindow()
        {
            Pin.MainWindow.Dispatcher.Invoke(new Action(() =>
            {
                if (Pin.MainWindow.WindowStyle != WindowStyle.ToolWindow && Pin.MainWindow.WindowStyle != WindowStyle.None)
                {
                    WindowStyle current = Pin.MainWindow.WindowStyle;

                    Pin.MainWindow.WindowStyle = current switch
                    {
                        WindowStyle.SingleBorderWindow => WindowStyle.ThreeDBorderWindow,
                        WindowStyle.ThreeDBorderWindow => WindowStyle.SingleBorderWindow,
                        WindowStyle.ToolWindow => WindowStyle.SingleBorderWindow,
                        _ => current,
                    };

                    Pin.MainWindow.WindowStyle = current;
                }
                else
                {
                    ResizeMode current = Pin.MainWindow.ResizeMode;

                    Pin.MainWindow.ResizeMode = current switch
                    {
                        ResizeMode.CanResize => ResizeMode.CanMinimize,
                        ResizeMode.NoResize => ResizeMode.CanMinimize,
                        _ => ResizeMode.CanResize
                    };

                    Pin.MainWindow.ResizeMode = current;
                }
            }));
        }
    }
}