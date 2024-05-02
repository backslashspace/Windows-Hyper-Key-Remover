using System;
using System.Management;
using System.Windows;
using System.Windows.Media;

namespace Installer
{
    internal static partial class ThemeAwareness
    {
        internal static Boolean Initialized = false;

        internal static void Initialize()
        {
            InitializeThemeHandler();

            InitializeAccentColorHandler();

            Initialized = true;
        }

        internal static Boolean AppsUseLightTheme { get; private set; } = true;

        internal static Byte[] RawAccentColor { get; private set; } = new Byte[3];

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

        private static void InitializeAccentColorHandler()
        {
            // only supported in windows 10 and newer
            if (DWMAPI.GetWindowsBuildNumber() <= 9600)
            {
                return;
            }

            AccentColorChanged(null, null);

            new RegistryEvents_CurrentUser(@"Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Accent", "AccentPalette", AccentColorChanged);
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
            return;

            UI.Dispatcher.BeginInvoke(() =>
            {
                SolidColorBrush cc = new(Color.FromRgb(243, 243, 243));
                UI.MainWindow.Resources["Background"] = cc;

                cc = new(Color.FromRgb(22, 22, 22));
                UI.MainWindow.Resources["FontColor"] = cc;
            });
        }

        private static void ApplyDarkTheme()
        {
            return;


            UI.Dispatcher.BeginInvoke(() =>
            {
                SolidColorBrush cc = new(Color.FromRgb(32, 32, 32));

                UI.MainWindow.Resources["Background"] = cc;

                cc = new(Color.FromRgb(230, 230, 230));

                UI.MainWindow.Resources["FontColor"] = cc;
            });
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

        #region AccentColor
        private static void AccentColorChanged(object sender, EventArrivedEventArgs e)
        {
            try
            {
                Object rawAccentPalette = Microsoft.Win32.Registry.GetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Accent", "AccentPalette", null);

                if (rawAccentPalette is Byte[] newAccentPalette && newAccentPalette.Length == 32)
                {
                    for (Byte b = 0; b < 3; ++b)
                    {
                        if (RawAccentColor[b] != newAccentPalette[b + 4])
                        {
                            Buffer.BlockCopy(newAccentPalette, 4, RawAccentColor, 0, 3);

                            ApplyAccentColor();

                            return;
                        }
                    }
                }
            }
            catch { }
        }

        private static void ApplyAccentColor()
        {
            

   


            UI.Dispatcher.Invoke(() =>
            {
                UpdateButtonColors();

                UI.MainWindow.Resources["AccentColor"] = new SolidColorBrush(Color.FromRgb(RawAccentColor[0], RawAccentColor[1], RawAccentColor[2]));


                Int16[] border_Idle = [(Int16)Math.Round(RawAccentColor[0] * 1.05, MidpointRounding.AwayFromZero),
                                   (Int16)Math.Round(RawAccentColor[1] * 1.05, MidpointRounding.AwayFromZero),
                                   (Int16)Math.Round(RawAccentColor[2] * 1.05, MidpointRounding.AwayFromZero)];

                ValidateTo8Bit(ref border_Idle);

                UI.MainWindow.Resources["AccentColorBorder"] = new SolidColorBrush(Color.FromRgb((Byte)border_Idle[0], (Byte)border_Idle[1], (Byte)border_Idle[2]));




            });
        }
        #endregion
    }
}