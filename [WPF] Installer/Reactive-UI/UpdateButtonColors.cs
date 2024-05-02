using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Installer
{
    internal static partial class ThemeAwareness
    {
        private static void UpdateButtonColors()
        {
            Int16[] background_Idle = [RawAccentColor[0], RawAccentColor[1], RawAccentColor[2],];

            Int16[] background_MouseOver = [(Int16)Math.Round(background_Idle[0] * 0.9, MidpointRounding.AwayFromZero),
                                            (Int16)Math.Round(background_Idle[1] * 0.9, MidpointRounding.AwayFromZero),
                                            (Int16)Math.Round(background_Idle[2] * 0.9, MidpointRounding.AwayFromZero)];

            Int16[] mouseDown = [(Int16)Math.Round(background_Idle[0] * 0.83, MidpointRounding.AwayFromZero),
                                 (Int16)Math.Round(background_Idle[1] * 0.83, MidpointRounding.AwayFromZero),
                                 (Int16)Math.Round(background_Idle[2] * 0.83, MidpointRounding.AwayFromZero)];

            Int16[] border_Idle = [(Int16)Math.Round(background_Idle[0] * 1.05, MidpointRounding.AwayFromZero),
                                   (Int16)Math.Round(background_Idle[1] * 1.05, MidpointRounding.AwayFromZero),
                                   (Int16)Math.Round(background_Idle[2] * 1.05, MidpointRounding.AwayFromZero)];

            Int16[] border_MouseOver = [(Int16)Math.Round(background_Idle[0] * 0.96, MidpointRounding.AwayFromZero),
                                        (Int16)Math.Round(background_Idle[1] * 0.96, MidpointRounding.AwayFromZero),
                                        (Int16)Math.Round(background_Idle[2] * 0.96, MidpointRounding.AwayFromZero)];

            ValidateTo8Bit(ref background_MouseOver);
            ValidateTo8Bit(ref mouseDown);
            ValidateTo8Bit(ref border_Idle);
            ValidateTo8Bit(ref border_MouseOver);

            ButtonAnimator.SetColor_MouseEnter(ref background_MouseOver, ref border_MouseOver);
            ButtonAnimator.SetColor_MouseLeave(ref background_Idle, ref border_Idle);
            ButtonAnimator.SetColor_MouseDown(ref mouseDown);
            ButtonAnimator.SetColor_MouseUp(ref background_MouseOver, ref border_MouseOver);
        }

        private static void ValidateTo8Bit(ref Int16[] color)
        {
            for (Byte b = 0; b < 3; ++b)
            {
                if (color[b] < 0)
                {
                    color[b] = 0;
                }
                else if (color[b] > 255)
                {
                    color[b] = 255;
                }
            }
        }
    }
}