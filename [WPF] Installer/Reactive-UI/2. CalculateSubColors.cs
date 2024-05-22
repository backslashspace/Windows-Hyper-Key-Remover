using System;

namespace Installer
{
    internal static partial class ThemeAwareness
    {
        private const Double MAGIC_NUMBER = 6755399441055744.0d;

        private static void CalculateColors()
        {
            Byte[] background_MouseOver = new Byte[3];
            Byte[] mouseDown = new Byte[3];
            Byte[] border_Idle = new Byte[3];
            Byte[] border_MouseOver = new Byte[3];

            #region DarkTheme
            // (-10)x = y - 25
            background_MouseOver[0] = (Byte)(AccentPalette.DarkMode_AccentColor[0] + (Int32)(((AccentPalette.DarkMode_AccentColor[0] - 25.1) * -0.1) + MAGIC_NUMBER - MAGIC_NUMBER));
            background_MouseOver[1] = (Byte)(AccentPalette.DarkMode_AccentColor[1] + (Int32)(((AccentPalette.DarkMode_AccentColor[1] - 25.1) * -0.1) + MAGIC_NUMBER - MAGIC_NUMBER));
            background_MouseOver[2] = (Byte)(AccentPalette.DarkMode_AccentColor[2] + (Int32)(((AccentPalette.DarkMode_AccentColor[2] - 25.1) * -0.1) + MAGIC_NUMBER - MAGIC_NUMBER));

            // (-5)x = y - 27.5
            mouseDown[0] = (Byte)(AccentPalette.DarkMode_AccentColor[0] + (Int32)(((AccentPalette.DarkMode_AccentColor[0] - 27.51) * -0.2) + MAGIC_NUMBER - MAGIC_NUMBER));
            mouseDown[1] = (Byte)(AccentPalette.DarkMode_AccentColor[1] + (Int32)(((AccentPalette.DarkMode_AccentColor[1] - 27.51) * -0.2) + MAGIC_NUMBER - MAGIC_NUMBER));
            mouseDown[2] = (Byte)(AccentPalette.DarkMode_AccentColor[2] + (Int32)(((AccentPalette.DarkMode_AccentColor[2] - 27.51) * -0.2) + MAGIC_NUMBER - MAGIC_NUMBER));

            // (-12.75)x = y - 255
            border_Idle[0] = (Byte)(AccentPalette.DarkMode_AccentColor[0] + (Int32)(((AccentPalette.DarkMode_AccentColor[0] - 255) * -0.0784313725490195) + MAGIC_NUMBER - MAGIC_NUMBER));
            border_Idle[1] = (Byte)(AccentPalette.DarkMode_AccentColor[1] + (Int32)(((AccentPalette.DarkMode_AccentColor[1] - 255) * -0.0784313725490195) + MAGIC_NUMBER - MAGIC_NUMBER));
            border_Idle[2] = (Byte)(AccentPalette.DarkMode_AccentColor[2] + (Int32)(((AccentPalette.DarkMode_AccentColor[2] - 255) * -0.0784313725490195) + MAGIC_NUMBER - MAGIC_NUMBER));

            // (-5.8) x = y - 131.2
            border_MouseOver[0] = (Byte)(AccentPalette.DarkMode_AccentColor[0] + (Int32)(((AccentPalette.DarkMode_AccentColor[0] - 131.2) * -0.1724137931034483) + MAGIC_NUMBER - MAGIC_NUMBER));
            border_MouseOver[1] = (Byte)(AccentPalette.DarkMode_AccentColor[1] + (Int32)(((AccentPalette.DarkMode_AccentColor[1] - 131.2) * -0.1724137931034483) + MAGIC_NUMBER - MAGIC_NUMBER));
            border_MouseOver[2] = (Byte)(AccentPalette.DarkMode_AccentColor[2] + (Int32)(((AccentPalette.DarkMode_AccentColor[2] - 131.2) * -0.1724137931034483) + MAGIC_NUMBER - MAGIC_NUMBER));
            #endregion

            SetNewColors(ThemeData.ControlColors.DarkMode_BorderBrush_Idle, ref border_Idle);
            SetNewColors(ThemeData.ControlColors.DarkMode_Background_MouseOver, ref background_MouseOver);
            SetNewColors(ThemeData.ControlColors.DarkMode_BorderBrush_MouseOver, ref border_MouseOver);
            SetNewColors(ThemeData.ControlColors.DarkMode_MouseDown, ref mouseDown);

            #region LightTheme
            // (-10)x = y - 245
            background_MouseOver[0] = (Byte)(AccentPalette.LightMode_AccentColor[0] + (Int32)(((AccentPalette.LightMode_AccentColor[0] - 245.1) * -0.1) + MAGIC_NUMBER - MAGIC_NUMBER));
            background_MouseOver[1] = (Byte)(AccentPalette.LightMode_AccentColor[1] + (Int32)(((AccentPalette.LightMode_AccentColor[1] - 245.1) * -0.1) + MAGIC_NUMBER - MAGIC_NUMBER));
            background_MouseOver[2] = (Byte)(AccentPalette.LightMode_AccentColor[2] + (Int32)(((AccentPalette.LightMode_AccentColor[2] - 245.1) * -0.1) + MAGIC_NUMBER - MAGIC_NUMBER));

            // -5 x = y - 243
            mouseDown[0] = (Byte)(AccentPalette.LightMode_AccentColor[0] + (Int32)(((AccentPalette.LightMode_AccentColor[0] - 243) * -0.2) + MAGIC_NUMBER - MAGIC_NUMBER));
            mouseDown[1] = (Byte)(AccentPalette.LightMode_AccentColor[1] + (Int32)(((AccentPalette.LightMode_AccentColor[1] - 243) * -0.2) + MAGIC_NUMBER - MAGIC_NUMBER));
            mouseDown[2] = (Byte)(AccentPalette.LightMode_AccentColor[2] + (Int32)(((AccentPalette.LightMode_AccentColor[2] - 243) * -0.2) + MAGIC_NUMBER - MAGIC_NUMBER));

            // (-12.75)x = y - 255
            border_Idle[0] = (Byte)(AccentPalette.LightMode_AccentColor[0] + (Int32)(((AccentPalette.LightMode_AccentColor[0] - 255) * -0.0784313725490195) + MAGIC_NUMBER - MAGIC_NUMBER));
            border_Idle[1] = (Byte)(AccentPalette.LightMode_AccentColor[1] + (Int32)(((AccentPalette.LightMode_AccentColor[1] - 255) * -0.0784313725490195) + MAGIC_NUMBER - MAGIC_NUMBER));
            border_Idle[2] = (Byte)(AccentPalette.LightMode_AccentColor[2] + (Int32)(((AccentPalette.LightMode_AccentColor[2] - 255) * -0.0784313725490195) + MAGIC_NUMBER - MAGIC_NUMBER));   

            // (-5.82) x = y - 248
            border_MouseOver[0] = (Byte)(AccentPalette.LightMode_AccentColor[0] + (Int32)(((AccentPalette.LightMode_AccentColor[0] - 248) * -0.1718213058419243) + MAGIC_NUMBER - MAGIC_NUMBER));
            border_MouseOver[1] = (Byte)(AccentPalette.LightMode_AccentColor[1] + (Int32)(((AccentPalette.LightMode_AccentColor[1] - 248) * -0.1718213058419243) + MAGIC_NUMBER - MAGIC_NUMBER));
            border_MouseOver[2] = (Byte)(AccentPalette.LightMode_AccentColor[2] + (Int32)(((AccentPalette.LightMode_AccentColor[2] - 248) * -0.1718213058419243) + MAGIC_NUMBER - MAGIC_NUMBER));

            SetNewColors(ThemeData.ControlColors.LightMode_BorderBrush_Idle, ref border_Idle);
            SetNewColors(ThemeData.ControlColors.LightMode_Background_MouseOver, ref background_MouseOver);
            SetNewColors(ThemeData.ControlColors.LightMode_BorderBrush_MouseOver, ref border_MouseOver);
            SetNewColors(ThemeData.ControlColors.LightMode_MouseDown, ref mouseDown);
            #endregion
        }

        private static void SetNewColors(Byte[] targetArray, ref Byte[] sourceArray)
        {
            // targetArray <- pass by ref (static array)
            targetArray[0] = sourceArray[0];
            targetArray[1] = sourceArray[1];
            targetArray[2] = sourceArray[2];
        }
    }
}