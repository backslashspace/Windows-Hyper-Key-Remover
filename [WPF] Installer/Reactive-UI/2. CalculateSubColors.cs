using System;
using System.Windows.Data;
using System.Windows.Media;

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
            //background_MouseOver[0] = (Int16)Math.Round(AccentPalette.DarkMode_AccentColor[0] * 0.90909, MidpointRounding.AwayFromZero);
            //background_MouseOver[1] = (Int16)Math.Round(AccentPalette.DarkMode_AccentColor[1] * 0.90909, MidpointRounding.AwayFromZero);
            //background_MouseOver[2] = (Int16)Math.Round(AccentPalette.DarkMode_AccentColor[2] * 0.90909, MidpointRounding.AwayFromZero);
            //
            //mouseDown[0] = (Int16)Math.Round(AccentPalette.DarkMode_AccentColor[0] * 0.83636, MidpointRounding.AwayFromZero);
            //mouseDown[1] = (Int16)Math.Round(AccentPalette.DarkMode_AccentColor[1] * 0.83636, MidpointRounding.AwayFromZero);
            //mouseDown[2] = (Int16)Math.Round(AccentPalette.DarkMode_AccentColor[2] * 0.83636, MidpointRounding.AwayFromZero);
            //
            //border_Idle[0] = (Int16)Math.Round(AccentPalette.DarkMode_AccentColor[0] * 1.05454, MidpointRounding.AwayFromZero);
            //border_Idle[1] = (Int16)Math.Round(AccentPalette.DarkMode_AccentColor[1] * 1.05454, MidpointRounding.AwayFromZero);
            //border_Idle[2] = (Int16)Math.Round(AccentPalette.DarkMode_AccentColor[2] * 1.05454, MidpointRounding.AwayFromZero);
            //
            //border_MouseOver[0] = (Int16)Math.Round(AccentPalette.DarkMode_AccentColor[0] * 0.98181, MidpointRounding.AwayFromZero);
            //border_MouseOver[1] = (Int16)Math.Round(AccentPalette.DarkMode_AccentColor[1] * 0.98181, MidpointRounding.AwayFromZero);
            //border_MouseOver[2] = (Int16)Math.Round(AccentPalette.DarkMode_AccentColor[2] * 0.98181, MidpointRounding.AwayFromZero);
            //
            //CapTo8Bit(ref border_Idle);
            //CapTo8Bit(ref background_MouseOver);
            //CapTo8Bit(ref border_MouseOver);
            //CapTo8Bit(ref mouseDown);
            //
            //// pass static, ref new
            //SetNewColors(ThemeData.DarkMode_BorderBrush_Idle, ref border_Idle);
            //SetNewColors(ThemeData.DarkMode_Background_MouseOver, ref background_MouseOver);
            //SetNewColors(ThemeData.DarkMode_BorderBrush_MouseOver, ref border_MouseOver);
            //SetNewColors(ThemeData.DarkMode_MouseDown, ref mouseDown);
            #endregion

            #region LightTheme
            background_MouseOver[0] = LightMode_BackgroundMouseOver(ref AccentPalette.LightMode_AccentColor[0]);
            background_MouseOver[1] = LightMode_BackgroundMouseOver(ref AccentPalette.LightMode_AccentColor[1]);
            background_MouseOver[2] = LightMode_BackgroundMouseOver(ref AccentPalette.LightMode_AccentColor[2]);

            mouseDown[0] = LightMode_MouseDown(ref AccentPalette.LightMode_AccentColor[0]);
            mouseDown[1] = LightMode_MouseDown(ref AccentPalette.LightMode_AccentColor[1]);
            mouseDown[2] = LightMode_MouseDown(ref AccentPalette.LightMode_AccentColor[2]);

            border_Idle[0] = LightMode_BorderIdle(ref AccentPalette.LightMode_AccentColor[0]);
            border_Idle[1] = LightMode_BorderIdle(ref AccentPalette.LightMode_AccentColor[1]);
            border_Idle[2] = LightMode_BorderIdle(ref AccentPalette.LightMode_AccentColor[2]);

            border_MouseOver[0] = LightMode_MouseOverBorder(ref AccentPalette.LightMode_AccentColor[0]);
            border_MouseOver[1] = LightMode_MouseOverBorder(ref AccentPalette.LightMode_AccentColor[1]);
            border_MouseOver[2] = LightMode_MouseOverBorder(ref AccentPalette.LightMode_AccentColor[2]);

            SetNewColors(ThemeData.LightMode_BorderBrush_Idle, ref border_Idle);
            SetNewColors(ThemeData.LightMode_Background_MouseOver, ref background_MouseOver);
            SetNewColors(ThemeData.LightMode_BorderBrush_MouseOver, ref border_MouseOver);
            SetNewColors(ThemeData.LightMode_MouseDown, ref mouseDown);
            #endregion
        }

        private static Byte LightMode_BackgroundMouseOver(ref readonly Byte baseColor)
        {
            // -10x = y - 245

            if (baseColor == 0)
            {
                return 25;
            }
            else
            {
               return (Byte)(baseColor + (((baseColor - 245) * -0.1) + MAGIC_NUMBER - MAGIC_NUMBER));
            }
        }

        private static Byte LightMode_MouseDown(ref readonly Byte baseColor)
        {
            // -5x = y - 243

            return (Byte)(baseColor + (((baseColor - 243) * -0.2) + MAGIC_NUMBER - MAGIC_NUMBER));
        }

        private static Byte LightMode_BorderIdle(ref readonly Byte baseColor)
        {
            // -12.75x = y - 255

            return (Byte)(baseColor + (((baseColor - 255) * -0.0784313725490196) + MAGIC_NUMBER - MAGIC_NUMBER));
        }

        private static Byte LightMode_MouseOverBorder(ref readonly Byte baseColor)
        {
            // -5.82x = y - 248

            return (Byte)(baseColor + (Int32)(((baseColor - 248) * -0.1718213058419244) + MAGIC_NUMBER - MAGIC_NUMBER));
        }

        private static void SetNewColors(Byte[] targetArray, ref Byte[] sourceArray)
        {
            // targetArray <- pass by ref (static array)
            targetArray[0] = unchecked((Byte)sourceArray[0]);
            targetArray[1] = unchecked((Byte)sourceArray[1]);
            targetArray[2] = unchecked((Byte)sourceArray[2]);
        }
    }
}