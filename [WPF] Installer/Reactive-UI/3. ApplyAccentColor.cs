using System.Windows.Media;

namespace Installer
{
    internal static partial class ThemeAwareness
    {
        private static void ApplyAccentColor()
        {
            SetPrimaryButtonColors();

            if (AppsUseLightTheme)
            {
                UI.MainWindow.Resources["AccentColor"] = new SolidColorBrush(Color.FromRgb(AccentPalette.LightMode_AccentColor[0], AccentPalette.LightMode_AccentColor[1], AccentPalette.LightMode_AccentColor[2]));
                UI.MainWindow.Resources["AccentColorBorder"] = new SolidColorBrush(Color.FromRgb(ThemeData.LightMode_BorderBrush_Idle[0], ThemeData.LightMode_BorderBrush_Idle[1], ThemeData.LightMode_BorderBrush_Idle[2]));
            }
            else
            {
                UI.MainWindow.Resources["AccentColor"] = new SolidColorBrush(Color.FromRgb(AccentPalette.DarkMode_AccentColor[0], AccentPalette.DarkMode_AccentColor[1], AccentPalette.DarkMode_AccentColor[2]));
                UI.MainWindow.Resources["AccentColorBorder"] = new SolidColorBrush(Color.FromRgb(ThemeData.DarkMode_BorderBrush_Idle[0], ThemeData.DarkMode_BorderBrush_Idle[1], ThemeData.DarkMode_BorderBrush_Idle[2]));
            }
        }

        private static void SetPrimaryButtonColors()
        {
            if (AppsUseLightTheme)
            {
                ButtonAnimator.PrimaryButton.SetColor_MouseEnter(ThemeData.LightMode_Background_MouseOver, ThemeData.LightMode_BorderBrush_MouseOver);
                ButtonAnimator.PrimaryButton.SetColor_MouseLeave(AccentPalette.LightMode_AccentColor, ThemeData.LightMode_BorderBrush_Idle);
                ButtonAnimator.PrimaryButton.SetColor_MouseDown(ThemeData.LightMode_MouseDown);
                ButtonAnimator.PrimaryButton.SetColor_MouseUp(ThemeData.LightMode_Background_MouseOver, ThemeData.LightMode_BorderBrush_MouseOver);
            }
            else
            {
                ButtonAnimator.PrimaryButton.SetColor_MouseEnter(ThemeData.DarkMode_Background_MouseOver, ThemeData.DarkMode_BorderBrush_MouseOver);
                ButtonAnimator.PrimaryButton.SetColor_MouseLeave(AccentPalette.DarkMode_AccentColor, ThemeData.DarkMode_BorderBrush_Idle);
                ButtonAnimator.PrimaryButton.SetColor_MouseDown(ThemeData.DarkMode_MouseDown);
                ButtonAnimator.PrimaryButton.SetColor_MouseUp(ThemeData.DarkMode_Background_MouseOver, ThemeData.DarkMode_BorderBrush_MouseOver);
            }
        }
    }
}