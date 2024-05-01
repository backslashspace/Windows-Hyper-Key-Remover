using System;
using System.Management;
using System.Windows;

namespace Installer
{
    internal static class ThemeAwareness
    {
        internal static Boolean Initialized = false;

        internal static void Initialize()
        {
            InitializeThemeHandler();

            InitializeAccentColorHandler();

            Initialized = true;
        }

        internal static Boolean AppsUseLightTheme { get; private set; } = true;

        internal static Byte[] RawPalette { get; private set; }


        // # # # # # # # # # # # # # # # # # # # # # # #

        private static void InitializeThemeHandler()
        {
            if (DWMAPI.DarkModeCompatibilityLevel == DWMAPI.DWM_Dark_Mode_Compatibility_Level.NONE)
            {
                ApplyLightTheme();

                return;
            }

            AppThemeChanged(null, null);

            new RegistryEvents_CurrentUser(@"Software\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize", "AppsUseLightTheme", AppThemeChanged);
        }

        #region Dark/LightMode
        private static void AppThemeChanged(object sender, EventArrivedEventArgs e)
        {
            Boolean newThemeStateIsLightMode;

            try
            {
                Object rawAppsUseLightTheme = Microsoft.Win32.Registry.GetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize", "AppsUseLightTheme", null);

                if (rawAppsUseLightTheme is Int32 i && i == 0)
                {
                    newThemeStateIsLightMode = false;
                }
                else
                {
                    newThemeStateIsLightMode = true;
                }
            }
            catch
            {
                newThemeStateIsLightMode = true;
            }

            if (newThemeStateIsLightMode == AppsUseLightTheme) return;

            AppsUseLightTheme = newThemeStateIsLightMode;

            DWMAPI.SetTheme(UI.MainWindowHandle, !newThemeStateIsLightMode);

            if (newThemeStateIsLightMode)
            {
                ApplyLightTheme();
            }
            else
            {
                ApplyDarkTheme();
            }

            if (DWMAPI.GetWindowsBuildNumber() < 22000)
            {
                UpdateWindow();
            }
        }

        //

        private static void ApplyLightTheme()
        {

        }

        private static void ApplyDarkTheme()
        {

        }

        //

        private static void UpdateWindow()
        {
            UI.Dispatcher.Invoke(new Action(() =>
            {
                if (UI.MainWindow.WindowStyle != WindowStyle.ToolWindow && UI.MainWindow.WindowStyle != WindowStyle.None)
                {
                    WindowStyle current = UI.MainWindow.WindowStyle;

                    UI.MainWindow.WindowStyle = current switch
                    {
                        WindowStyle.SingleBorderWindow => WindowStyle.ThreeDBorderWindow,
                        WindowStyle.ThreeDBorderWindow => WindowStyle.SingleBorderWindow,
                        WindowStyle.ToolWindow => WindowStyle.SingleBorderWindow,
                        _ => current,
                    };

                    UI.MainWindow.WindowStyle = current;
                }
                else
                {
                    ResizeMode current = UI.MainWindow.ResizeMode;

                    UI.MainWindow.ResizeMode = current switch
                    {
                        ResizeMode.CanResize => ResizeMode.CanMinimize,
                        ResizeMode.NoResize => ResizeMode.CanMinimize,
                        _ => ResizeMode.CanResize
                    };

                    UI.MainWindow.ResizeMode = current;
                }
            }));
        }
        #endregion








        private static void InitializeAccentColorHandler()
        {

        }
    }
}