using System;
using System.Runtime.InteropServices;

namespace Installer
{
    internal static class DWMAPI
    {
        internal static Int32 InternalWindowsBuildNumber { get; private set; } = -1;

        private enum DWMWINDOWATTRIBUTE : UInt32
        {
            DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_18985_EQUAL_OR_AFTER_17763 = 19,
            DWMWA_USE_IMMERSIVE_DARK_MODE = 20,
            DWMWA_WINDOW_CORNER_PREFERENCE = 33,
            DWMWA_BORDER_COLOR = 34,
            DWMWA_CAPTION_COLOR = 35,
            DWMWA_TEXT_COLOR = 36
        }

        //min 17763
        #region Theme
        /// <summary>
        /// min. version 17763
        /// </summary>
        internal static unsafe Boolean SetTheme(IntPtr hwnd, Boolean dark)
        {
            GetWindowsBuildNumber();

            if (!GetDarkModeLevel(out DWMWINDOWATTRIBUTE attribute))
            {
                return false; // not supported
            }

            DwmSetWindowAttribute(hwnd, attribute, ref *(UInt32*)&dark, sizeof(UInt32));

            return true;
        }

        private static Boolean GetDarkModeLevel(out DWMWINDOWATTRIBUTE attribute)
        {
            if (InternalWindowsBuildNumber >= 18985) // windows 10 '20H1' or newer
            {
                attribute = DWMWINDOWATTRIBUTE.DWMWA_USE_IMMERSIVE_DARK_MODE;
                return true;
            }
            else if (InternalWindowsBuildNumber >= 17763)
            {
                attribute = DWMWINDOWATTRIBUTE.DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_18985_EQUAL_OR_AFTER_17763;
                return true;
            }
            else
            {
                attribute = 0;
                return false;
            }
        }
        #endregion

        [DllImport("dwmapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern UInt32 DwmSetWindowAttribute(IntPtr hwnd, DWMWINDOWATTRIBUTE dwAttribute, ref UInt32 pvAttribute, UInt32 cbAttribute);

        internal static void GetWindowsBuildNumber()
        {
            if (InternalWindowsBuildNumber != -1) { return; }

            Object regOutput = Microsoft.Win32.Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion", "CurrentBuildNumber", null);

            if (regOutput == null)
            {
                throw new ArgumentNullException("Registry: return value of 'HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion', 'CurrentBuildNumber' was null");
            }

            if (UInt32.TryParse(unchecked((String)regOutput), out UInt32 version))
            {
                InternalWindowsBuildNumber = checked((Int32)version);

                return;
            }

            throw new InvalidCastException($"GetWindowsBuildNumber(): {regOutput} -> Int32");
        }
    }
}