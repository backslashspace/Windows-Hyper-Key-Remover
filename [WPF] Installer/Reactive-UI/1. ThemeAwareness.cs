﻿using System;
using System.Management;
using System.Windows;
using System.Windows.Media;

namespace Installer
{
    internal static partial class ThemeAwareness
    {
        internal static Boolean Initialized { get; private set; } = false;

        internal static Boolean AppsUseLightTheme { get; private set; } = false;

        internal readonly struct AccentPalette
        {
            internal static readonly Byte[] LightMode_AccentColor = new Byte[3];
            internal static readonly Byte[] DarkMode_AccentColor = new Byte[3];
        }

        internal static class ThemeData
        {
            internal static readonly SolidColorBrush LightMode_FontColor = new(Color.FromRgb(22, 22, 22));
            internal static readonly SolidColorBrush DarkMode_FontColor = new(Color.FromRgb(230, 230, 230));

            internal static readonly SolidColorBrush LightMode_BackgroundColor = new(Color.FromRgb(243, 243, 243));
            internal static readonly SolidColorBrush DarkMode_BackgroundColor = new(Color.FromRgb(32, 32, 32));

            internal static class ControlColors
            {
                internal static readonly Byte[] LightMode_BorderBrush_Idle = new Byte[3];
                internal static readonly Byte[] LightMode_Background_MouseOver = new Byte[3];
                internal static readonly Byte[] LightMode_BorderBrush_MouseOver = new Byte[3];
                internal static readonly Byte[] LightMode_MouseDown = new Byte[3];

                internal static readonly Byte[] DarkMode_BorderBrush_Idle = new Byte[3];
                internal static readonly Byte[] DarkMode_Background_MouseOver = new Byte[3];
                internal static readonly Byte[] DarkMode_BorderBrush_MouseOver = new Byte[3];
                internal static readonly Byte[] DarkMode_MouseDown = new Byte[3];
            }
        }

        // # # # # # # # # # # # # # # # # # # # # # # #

        internal static void Initialize()
        {
            if (Initialized) return;

            InitializeAccentColorHandler();

            InitializeThemeHandler();

            Initialized = true;
        }

        // # # # # # # # # # # # # # # # # # # # # # # #

        #region InitializeEventHandler
        private static void InitializeAccentColorHandler()
        {
            // only supported in windows 10 and newer
            if (DWMAPI.GetWindowsBuildNumber() <= 9600)
            {
                return;
            }

            // get first time
            AccentColorChanged(null, null);

            new RegistryEvents_CurrentUser(@"Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Accent", "AccentPalette", AccentColorChanged);
        }

        private static void InitializeThemeHandler()
        {
            if (DWMAPI.DarkModeCompatibilityLevel == DWMAPI.DWM_Dark_Mode_Compatibility_Level.NONE)
            {
                ApplyLightTheme();

                return;
            }

            // get first time
            AppThemeChanged(null, null);

            new RegistryEvents_CurrentUser(@"Software\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize", "AppsUseLightTheme", AppThemeChanged);
        }
        #endregion

        #region RegistryEventHandler
        private static void AccentColorChanged(object sender, EventArrivedEventArgs e)
        {
            try
            {
                Object rawAccentPalette = Microsoft.Win32.Registry.GetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Accent", "AccentPalette", null);

                if (rawAccentPalette is Byte[] newAccentPalette && newAccentPalette.Length == 32)
                {
                    Boolean colorChanged = false;

                    for (Byte b = 0; b < 3; ++b)
                    {
                        if (AccentPalette.DarkMode_AccentColor[b] != newAccentPalette[b + 4])
                        {
                            colorChanged = true;
                            break;
                        }
                    }

                    if (!colorChanged)
                    {
                        for (Byte b = 0; b < 3; ++b)
                        {
                            if (AccentPalette.LightMode_AccentColor[b] != newAccentPalette[b + 16])
                            {
                                colorChanged = true;
                                break;
                            }
                        }
                    }

                    if (!colorChanged) return;

                    Buffer.BlockCopy(newAccentPalette, 16, AccentPalette.LightMode_AccentColor, 0, 3);
                    Buffer.BlockCopy(newAccentPalette, 4, AccentPalette.DarkMode_AccentColor, 0, 3);

                    CalculateColors();

                    UI.Dispatcher.BeginInvoke(() => ApplyAccentColor());
                }
            }
            catch { }
        }

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

            if (newThemeStateIsLightMode == AppsUseLightTheme && Initialized) return;

            AppsUseLightTheme = newThemeStateIsLightMode;

            DWMAPI.SetTheme(UI.MainWindowHandle, !newThemeStateIsLightMode);

            UI.Dispatcher.BeginInvoke(() =>
            {
                if (DWMAPI.GetWindowsBuildNumber() < 22000)
                {
                    UpdateWindow();
                }

                if (newThemeStateIsLightMode)
                {
                    ApplyLightTheme();
                }
                else
                {
                    ApplyDarkTheme();
                }

                ApplyAccentColor();
            });
        }
        #endregion

        //

        private static void ApplyLightTheme()
        {
            ButtonAnimator.SecondaryButton.SetLightMode();

            UI.MainWindow.Resources["Background"] = ThemeData.LightMode_BackgroundColor;

            UI.MainWindow.Resources["FontColor"] = ThemeData.LightMode_FontColor;
            UI.MainWindow.Resources["FontColor_Inverted"] = ThemeData.DarkMode_FontColor;
        }

        private static void ApplyDarkTheme()
        {
            ButtonAnimator.SecondaryButton.SetDarkMode();

            UI.MainWindow.Resources["Background"] = ThemeData.DarkMode_BackgroundColor;

            UI.MainWindow.Resources["FontColor"] = ThemeData.DarkMode_FontColor;
            UI.MainWindow.Resources["FontColor_Inverted"] = ThemeData.LightMode_FontColor;
        }

        private static void UpdateWindow()
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
        }
    }
}