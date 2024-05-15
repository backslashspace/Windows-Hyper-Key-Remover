using System;
using System.Windows.Media;

namespace Installer
{
    internal static partial class ThemeAwareness
    {
        private static void ApplyAccentColor()
        {
            SetPrimaryButtonColors();

            SetCheckBoxColors();

            if (AppsUseLightTheme)
            {
                UI.MainWindow.Resources["AccentColor"] = new SolidColorBrush(Color.FromRgb(AccentPalette.LightMode_AccentColor[0], AccentPalette.LightMode_AccentColor[1], AccentPalette.LightMode_AccentColor[2]));
                UI.MainWindow.Resources["AccentColorBorder"] = new SolidColorBrush(Color.FromRgb(ThemeData.ControlColors.LightMode_BorderBrush_Idle[0], ThemeData.ControlColors.LightMode_BorderBrush_Idle[1], ThemeData.ControlColors.LightMode_BorderBrush_Idle[2]));
            }
            else
            {
                UI.MainWindow.Resources["AccentColor"] = new SolidColorBrush(Color.FromRgb(AccentPalette.DarkMode_AccentColor[0], AccentPalette.DarkMode_AccentColor[1], AccentPalette.DarkMode_AccentColor[2]));
                UI.MainWindow.Resources["AccentColorBorder"] = new SolidColorBrush(Color.FromRgb(ThemeData.ControlColors.DarkMode_BorderBrush_Idle[0], ThemeData.ControlColors.DarkMode_BorderBrush_Idle[1], ThemeData.ControlColors.DarkMode_BorderBrush_Idle[2]));
            }
        }

        private static void SetPrimaryButtonColors()
        {
            if (AppsUseLightTheme)
            {
                ButtonAnimator.PrimaryButton.SetColor_MouseEnter(ThemeData.ControlColors.LightMode_Background_MouseOver, ThemeData.ControlColors.LightMode_BorderBrush_MouseOver);
                ButtonAnimator.PrimaryButton.SetColor_MouseLeave(AccentPalette.LightMode_AccentColor, ThemeData.ControlColors.LightMode_BorderBrush_Idle);
                ButtonAnimator.PrimaryButton.SetColor_MouseDown(ThemeData.ControlColors.LightMode_MouseDown);
                ButtonAnimator.PrimaryButton.SetColor_MouseUp(ThemeData.ControlColors.LightMode_Background_MouseOver, ThemeData.ControlColors.LightMode_BorderBrush_MouseOver);
            }
            else
            {
                ButtonAnimator.PrimaryButton.SetColor_MouseEnter(ThemeData.ControlColors.DarkMode_Background_MouseOver, ThemeData.ControlColors.DarkMode_BorderBrush_MouseOver);
                ButtonAnimator.PrimaryButton.SetColor_MouseLeave(AccentPalette.DarkMode_AccentColor, ThemeData.ControlColors.DarkMode_BorderBrush_Idle);
                ButtonAnimator.PrimaryButton.SetColor_MouseDown(ThemeData.ControlColors.DarkMode_MouseDown);
                ButtonAnimator.PrimaryButton.SetColor_MouseUp(ThemeData.ControlColors.DarkMode_Background_MouseOver, ThemeData.ControlColors.DarkMode_BorderBrush_MouseOver);
            }
        }

        private static void SetCheckBoxColors()
        {
            if (AppsUseLightTheme)
            {
                UI.MainWindow.Resources["CheckMarkColor"] = new SolidColorBrush(Color.FromRgb(255, 255, 255));

                CheckBoxAnimator.SetColor_MouseEnter_Unchecked([240, 240, 240], [137, 137, 137]);
                CheckBoxAnimator.SetColor_MouseEnter_Checked(ThemeData.ControlColors.LightMode_Background_MouseOver);

                CheckBoxAnimator.SetColor_MouseLeave_Unchecked([249, 249, 249], [139, 139, 139]);
                CheckBoxAnimator.SetColor_MouseLeave_Checked(AccentPalette.LightMode_AccentColor);

                CheckBoxAnimator.SetColor_MouseDown_Unchecked([231, 231, 231], [191, 191, 191]);
                CheckBoxAnimator.SetColor_MouseDown_Checked(ThemeData.ControlColors.LightMode_MouseDown);

                CheckBoxAnimator.SetColor_MouseUp_Unchecked([240, 240, 240], [137, 137, 137]);
                CheckBoxAnimator.SetColor_MouseUp_Checked(ThemeData.ControlColors.LightMode_Background_MouseOver);

                for (Int32 i = 0; i < CheckBoxAnimator.HookedCheckBoxes.Count; ++i)
                {
                    if ((Boolean)CheckBoxAnimator.HookedCheckBoxes[i].IsChecked)
                    {
                        CheckBoxAnimator.HookedCheckBoxes[i].Background = new SolidColorBrush(Color.FromRgb(AccentPalette.LightMode_AccentColor[0], AccentPalette.LightMode_AccentColor[1], AccentPalette.LightMode_AccentColor[2]));
                    }
                    else
                    {
                        CheckBoxAnimator.HookedCheckBoxes[i].Background = new SolidColorBrush(Color.FromRgb(249, 249, 249));
                        CheckBoxAnimator.HookedCheckBoxes[i].BorderBrush = new SolidColorBrush(Color.FromRgb(139, 139, 139));
                    }
                }
            }
            else
            {
                UI.MainWindow.Resources["CheckMarkColor"] = new SolidColorBrush(Color.FromRgb(171, 171, 171));

                CheckBoxAnimator.SetColor_MouseEnter_Unchecked([52, 52, 52], [160, 160, 160]);
                CheckBoxAnimator.SetColor_MouseEnter_Checked(ThemeData.ControlColors.DarkMode_Background_MouseOver);

                CheckBoxAnimator.SetColor_MouseLeave_Unchecked([39, 39, 39], [158, 158, 158]);
                CheckBoxAnimator.SetColor_MouseLeave_Checked(AccentPalette.DarkMode_AccentColor);

                CheckBoxAnimator.SetColor_MouseDown_Unchecked([58, 58, 58], [82, 82, 82]);
                CheckBoxAnimator.SetColor_MouseDown_Checked(ThemeData.ControlColors.DarkMode_MouseDown);

                CheckBoxAnimator.SetColor_MouseUp_Unchecked([52, 52, 52], [160, 160, 160]);
                CheckBoxAnimator.SetColor_MouseUp_Checked(ThemeData.ControlColors.DarkMode_Background_MouseOver);

                for (Int32 i = 0; i < CheckBoxAnimator.HookedCheckBoxes.Count; ++i)
                {
                    if ((Boolean)CheckBoxAnimator.HookedCheckBoxes[i].IsChecked)
                    {
                        CheckBoxAnimator.HookedCheckBoxes[i].Background = new SolidColorBrush(Color.FromRgb(AccentPalette.DarkMode_AccentColor[0], AccentPalette.DarkMode_AccentColor[1], AccentPalette.DarkMode_AccentColor[2]));
                    }
                    else
                    {
                        CheckBoxAnimator.HookedCheckBoxes[i].Background = new SolidColorBrush(Color.FromRgb(39, 39, 39));
                        CheckBoxAnimator.HookedCheckBoxes[i].BorderBrush = new SolidColorBrush(Color.FromRgb(158, 158, 158));
                    }
                }
            }

            
        }
    }
}