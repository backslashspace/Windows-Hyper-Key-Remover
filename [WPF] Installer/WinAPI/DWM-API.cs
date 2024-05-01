using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Installer
{
    internal static class DWMAPI
    {
        internal static Boolean Initialized { get; private set; } = false;

        internal static void Initialize()
        {
            if (Initialized) return;

            GetWindowsBuildNumber();

            if (WindowsBuildNumber >= 18985) // windows 10 '20H1' or newer
            {
                InternalThemeAttribute = DWMWINDOWATTRIBUTE.DWMWA_USE_IMMERSIVE_DARK_MODE;
                DarkModeCompatibilityLevel = DWM_Dark_Mode_Compatibility_Level.IMMERSIVE_DARK_MODE;
            }
            else if (WindowsBuildNumber >= 17763)
            {
                InternalThemeAttribute = DWMWINDOWATTRIBUTE.DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_18985_EQUAL_OR_AFTER_17763;
                DarkModeCompatibilityLevel = DWM_Dark_Mode_Compatibility_Level.IMMERSIVE_DARK_MODE_BEFORE_18985_EQUAL_OR_AFTER_17763;
            }
            else
            {
                InternalThemeAttribute = 0;
                DarkModeCompatibilityLevel = DWM_Dark_Mode_Compatibility_Level.NONE;
            }

            Initialized = true;
        }

        //

        internal static DWM_Dark_Mode_Compatibility_Level? DarkModeCompatibilityLevel { get; private set; } = null;
        internal enum DWM_Dark_Mode_Compatibility_Level : Byte
        {
            NONE = 0,
            IMMERSIVE_DARK_MODE_BEFORE_18985_EQUAL_OR_AFTER_17763 = 1,
            IMMERSIVE_DARK_MODE = 2,
        }

        private static DWMWINDOWATTRIBUTE InternalThemeAttribute = 0;
        private enum DWMWINDOWATTRIBUTE : UInt32
        {
            DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_18985_EQUAL_OR_AFTER_17763 = 19,
            DWMWA_USE_IMMERSIVE_DARK_MODE = 20,
            DWMWA_WINDOW_CORNER_PREFERENCE = 33,
            DWMWA_BORDER_COLOR = 34,
            DWMWA_CAPTION_COLOR = 35,
            DWMWA_TEXT_COLOR = 36
        }

        //

        private static Int32 WindowsBuildNumber = -1;
        internal static Int32 GetWindowsBuildNumber()
        {
            if (WindowsBuildNumber != -1)
            {
                return WindowsBuildNumber;
            }

            //

            Object regOutput = Microsoft.Win32.Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion", "CurrentBuildNumber", null);

            if (regOutput == null)
            {
                throw new InvalidDataException("Registry: return value of 'HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion', 'CurrentBuildNumber' was null");
            }

            if (UInt32.TryParse(unchecked((String)regOutput), out UInt32 version))
            {
                WindowsBuildNumber = checked((Int32)version);

                return WindowsBuildNumber;
            }

            throw new InvalidCastException($"GetWindowsBuildNumber(): {regOutput} -> Int32");
        }

        // requires os version 17763 or later  
        internal static unsafe Boolean SetTheme(IntPtr hwnd, Boolean dark)
        {
            if (!Initialized)
            {
                throw new InvalidOperationException("DWMAPI not initialized.");
            }

            if (DarkModeCompatibilityLevel == DWM_Dark_Mode_Compatibility_Level.NONE)
            {
                return false; // not supported
            }

            DwmSetWindowAttribute(hwnd, InternalThemeAttribute, ref *(UInt32*)&dark, sizeof(UInt32));

            return true;
        }

        [DllImport("dwmapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern UInt32 DwmSetWindowAttribute(IntPtr hwnd, DWMWINDOWATTRIBUTE dwAttribute, ref UInt32 pvAttribute, UInt32 cbAttribute);
    }
}