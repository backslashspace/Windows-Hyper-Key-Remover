using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Windows;

namespace Installer
{
    internal static class ButtonAnimator
    {
        private const Double Duration = 0.075;

        internal static Boolean Initialized { get; private set; } = false;

        internal static void Initialize()
        {
            if (Initialized) return;

            PrimaryButton.Initialize();
            SecondaryButton.Initialize();

            Initialized = true;
        }

        private static readonly PropertyPath BackgroundPropertyPath = new("(Control.Background).(SolidColorBrush.Color)");
        private static readonly PropertyPath BorderBrushPropertyPath = new("(Control.BorderBrush).(SolidColorBrush.Color)");
        private static readonly PropertyPath FontColorPropertyPath = new("(Control.Foreground).(SolidColorBrush.Color)");

        internal static class PrimaryButton
        {
            internal static void Initialize()
            {
                SetDefaultColors();

                #region MouseEnter
                MouseEnter_BackgroundColorAnimation.Duration = TimeSpan.FromSeconds(Duration);
                MouseEnter_BorderBrushColorAnimation.Duration = TimeSpan.FromSeconds(Duration);
                MouseEnter_Storyboard.Children.Add(MouseEnter_BackgroundColorAnimation);
                MouseEnter_Storyboard.Children.Add(MouseEnter_BorderBrushColorAnimation);
                #endregion

                #region MouseLeave
                MouseLeave_BackgroundColorAnimation.Duration = TimeSpan.FromSeconds(Duration);
                MouseLeave_BorderBrushColorAnimation.Duration = TimeSpan.FromSeconds(Duration);
                MouseLeave_Storyboard.Children.Add(MouseLeave_BackgroundColorAnimation);
                MouseLeave_Storyboard.Children.Add(MouseLeave_BorderBrushColorAnimation);
                #endregion

                #region MouseDown
                MouseDown_BackgroundColorAnimation.Duration = TimeSpan.FromSeconds(Duration);
                MouseDown_BorderBrushColorAnimation.Duration = TimeSpan.FromSeconds(Duration);
                MouseDown_Storyboard.Children.Add(MouseDown_BackgroundColorAnimation);
                MouseDown_Storyboard.Children.Add(MouseDown_BorderBrushColorAnimation);
                #endregion

                #region MouseUp
                MouseUp_BackgroundColorAnimation.Duration = TimeSpan.FromSeconds(Duration);
                MouseUp_BorderBrushColorAnimation.Duration = TimeSpan.FromSeconds(Duration);
                MouseUp_Storyboard.Children.Add(MouseUp_BackgroundColorAnimation);
                MouseUp_Storyboard.Children.Add(MouseUp_BorderBrushColorAnimation);
                #endregion
            }

            internal static void SetDefaultColors()
            {
                #region MouseEnter
                MouseEnter_BackgroundColorAnimation.To = Color.FromRgb(71, 177, 232);
                MouseEnter_BorderBrushColorAnimation.To = Color.FromRgb(85, 183, 234);
                #endregion

                #region MouseLeave
                MouseLeave_BackgroundColorAnimation.To = Color.FromRgb(76, 194, 255);
                MouseLeave_BorderBrushColorAnimation.To = Color.FromRgb(90, 199, 255);
                #endregion

                #region MouseDown
                MouseDown_BackgroundColorAnimation.To = Color.FromRgb(66, 161, 210);
                MouseDown_BorderBrushColorAnimation.To = Color.FromRgb(66, 161, 210);
                #endregion

                #region MouseUp
                MouseUp_BackgroundColorAnimation.To = Color.FromRgb(71, 177, 232);
                MouseUp_BorderBrushColorAnimation.To = Color.FromRgb(85, 183, 234);
                #endregion
            }

            internal static void HookChild(ref readonly Button button)
            {
                if (!Initialized) throw new InvalidOperationException("CheckBoxAnimator was not initialized");

                button.MouseEnter += Button_MouseEnter;
                button.MouseLeave += Button_MouseLeave;
                button.PreviewMouseDown += Button_MouseDown;
                button.PreviewMouseUp += Button_Up;
            }

            //

            #region EventHandler
            private static void Button_MouseEnter(object sender, MouseEventArgs e)
            {
                if (sender == null) return;
                Button checkBox = sender as Button;

                MouseEnter_Begin(ref checkBox);
            }

            private static void Button_MouseLeave(object sender, MouseEventArgs e)
            {
                if (sender == null) { return; }
                Button checkBox = sender as Button;

                MouseLeave_Begin(ref checkBox);
            }

            private static void Button_MouseDown(object sender, MouseButtonEventArgs e)
            {
                if (sender == null) return;
                Button checkBox = sender as Button;

                MouseDown_Begin(ref checkBox);
            }

            private static void Button_Up(object sender, RoutedEventArgs e)
            {
                if (sender == null) { return; }
                Button checkBox = sender as Button;

                MouseUp_Begin(ref checkBox);
            }
            #endregion

            //

            #region MouseEnter
            private static readonly ColorAnimation MouseEnter_BackgroundColorAnimation = new();
            private static readonly ColorAnimation MouseEnter_BorderBrushColorAnimation = new();
            private static readonly Storyboard MouseEnter_Storyboard = new();

            private static void MouseEnter_Begin(ref readonly Button button)
            {
                Storyboard.SetTarget(MouseEnter_BackgroundColorAnimation, button);
                Storyboard.SetTargetProperty(MouseEnter_BackgroundColorAnimation, BackgroundPropertyPath);

                Storyboard.SetTarget(MouseEnter_BorderBrushColorAnimation, button);
                Storyboard.SetTargetProperty(MouseEnter_BorderBrushColorAnimation, BorderBrushPropertyPath);

                MouseEnter_Storyboard.Begin();
            }

            internal static void SetColor_MouseEnter(Byte[] background, Byte[] border)
            {
                MouseEnter_BackgroundColorAnimation.To = Color.FromRgb(background[0], background[1], background[2]);
                MouseEnter_BorderBrushColorAnimation.To = Color.FromRgb(border[0], border[1], border[2]);
            }
            #endregion

            #region MouseLeave
            private static readonly ColorAnimation MouseLeave_BackgroundColorAnimation = new();
            private static readonly ColorAnimation MouseLeave_BorderBrushColorAnimation = new();
            private static readonly Storyboard MouseLeave_Storyboard = new();

            private static void MouseLeave_Begin(ref readonly Button button)
            {
                Storyboard.SetTarget(MouseLeave_BackgroundColorAnimation, button);
                Storyboard.SetTargetProperty(MouseLeave_BackgroundColorAnimation, BackgroundPropertyPath);

                Storyboard.SetTarget(MouseLeave_BorderBrushColorAnimation, button);
                Storyboard.SetTargetProperty(MouseLeave_BorderBrushColorAnimation, BorderBrushPropertyPath);

                MouseLeave_Storyboard.Begin();
            }

            internal static void SetColor_MouseLeave(Byte[] background, Byte[] border)
            {
                MouseLeave_BackgroundColorAnimation.To = Color.FromRgb(background[0], background[1], background[2]);
                MouseLeave_BorderBrushColorAnimation.To = Color.FromRgb(border[0], border[1], border[2]);
            }
            #endregion

            #region MouseDown
            private static readonly ColorAnimation MouseDown_BackgroundColorAnimation = new();
            private static readonly ColorAnimation MouseDown_BorderBrushColorAnimation = new();
            private static readonly Storyboard MouseDown_Storyboard = new();

            private static void MouseDown_Begin(ref readonly Button button)
            {
                Storyboard.SetTarget(MouseDown_BackgroundColorAnimation, button);
                Storyboard.SetTargetProperty(MouseDown_BackgroundColorAnimation, BackgroundPropertyPath);

                Storyboard.SetTarget(MouseDown_BorderBrushColorAnimation, button);
                Storyboard.SetTargetProperty(MouseDown_BorderBrushColorAnimation, BorderBrushPropertyPath);

                MouseDown_Storyboard.Begin();
            }

            internal static void SetColor_MouseDown(Byte[] background)
            {
                MouseDown_BackgroundColorAnimation.To = Color.FromRgb(background[0], background[1], background[2]);
                MouseDown_BorderBrushColorAnimation.To = Color.FromRgb(background[0], background[1], background[2]);
            }
            #endregion

            #region MouseUp
            private static readonly ColorAnimation MouseUp_BackgroundColorAnimation = new();
            private static readonly ColorAnimation MouseUp_BorderBrushColorAnimation = new();
            private static readonly Storyboard MouseUp_Storyboard = new();

            private static void MouseUp_Begin(ref readonly Button button)
            {
                Storyboard.SetTarget(MouseUp_BackgroundColorAnimation, button);
                Storyboard.SetTargetProperty(MouseUp_BackgroundColorAnimation, BackgroundPropertyPath);

                Storyboard.SetTarget(MouseUp_BorderBrushColorAnimation, button);
                Storyboard.SetTargetProperty(MouseUp_BorderBrushColorAnimation, BorderBrushPropertyPath);

                MouseUp_Storyboard.Begin();
            }

            internal static void SetColor_MouseUp(Byte[] background, Byte[] border)
            {
                MouseUp_BackgroundColorAnimation.To = Color.FromRgb(background[0], background[1], background[2]);
                MouseUp_BorderBrushColorAnimation.To = Color.FromRgb(border[0], border[1], border[2]);
            }
            #endregion
        }

        internal static class SecondaryButton
        {
            internal static void Initialize()
            {
                SetDarkMode();

                #region MouseEnter
                MouseEnter_BackgroundColorAnimation.Duration = TimeSpan.FromSeconds(Duration);
                MouseEnter_Storyboard.Children.Add(MouseEnter_BackgroundColorAnimation);
                #endregion

                #region MouseLeave
                MouseLeave_BackgroundColorAnimation.Duration = TimeSpan.FromSeconds(Duration);
                MouseLeave_Storyboard.Children.Add(MouseLeave_BackgroundColorAnimation);
                #endregion

                #region MouseDown
                MouseDown_BackgroundColorAnimation.Duration = TimeSpan.FromSeconds(Duration);
                MouseDown_BorderBrushColorAnimation.Duration = TimeSpan.FromSeconds(Duration);
                MouseDown_FontColorAnimation.Duration = TimeSpan.FromSeconds(Duration);
                MouseDown_Storyboard.Children.Add(MouseDown_BackgroundColorAnimation);
                MouseDown_Storyboard.Children.Add(MouseDown_BorderBrushColorAnimation);
                MouseDown_Storyboard.Children.Add(MouseDown_FontColorAnimation);
                #endregion

                #region MouseUp
                MouseUp_BackgroundColorAnimation.Duration = TimeSpan.FromSeconds(Duration);
                MouseUp_BorderBrushColorAnimation.Duration = TimeSpan.FromSeconds(Duration);
                MouseUp_FontColorAnimation.Duration = TimeSpan.FromSeconds(Duration);
                MouseUp_Storyboard.Children.Add(MouseUp_BackgroundColorAnimation);
                MouseUp_Storyboard.Children.Add(MouseUp_BorderBrushColorAnimation);
                MouseUp_Storyboard.Children.Add(MouseUp_FontColorAnimation);
                #endregion
            }

            internal static void SetLightMode()
            {
                UI.MainWindow.Resources["Button_Secondary_Background"] = new SolidColorBrush(Color.FromRgb(251, 251, 251));
                UI.MainWindow.Resources["Button_Secondary_BorderBrush"] = new SolidColorBrush(Color.FromRgb(215, 215, 215));

                #region MouseEnter
                MouseEnter_BackgroundColorAnimation.To = Color.FromRgb(246, 246, 246);
                #endregion

                #region MouseLeave
                MouseLeave_BackgroundColorAnimation.To = Color.FromRgb(251, 251, 251);
                #endregion

                #region MouseDown
                MouseDown_BackgroundColorAnimation.To = Color.FromRgb(245, 245, 245);
                MouseDown_BorderBrushColorAnimation.To = Color.FromRgb(204, 204, 204);
                MouseDown_FontColorAnimation.To = Color.FromRgb(93, 93, 93);
                #endregion

                #region MouseUp
                MouseUp_BackgroundColorAnimation.To = Color.FromRgb(246, 246, 246);
                MouseUp_BorderBrushColorAnimation.To = Color.FromRgb(215, 215, 215);
                MouseUp_FontColorAnimation.To = Color.FromRgb(22, 22, 22);
                #endregion
            }

            internal static void SetDarkMode()
            {
                UI.MainWindow.Resources["Button_Secondary_Background"] = new SolidColorBrush(Color.FromRgb(45, 45, 45));
                UI.MainWindow.Resources["Button_Secondary_BorderBrush"] = new SolidColorBrush(Color.FromRgb(48, 48, 48));

                #region MouseEnter
                MouseEnter_BackgroundColorAnimation.To = Color.FromRgb(50, 50, 50);
                #endregion

                #region MouseLeave
                MouseLeave_BackgroundColorAnimation.To = Color.FromRgb(45, 45, 45);
                #endregion

                #region MouseDown
                MouseDown_BackgroundColorAnimation.To = Color.FromRgb(39, 39, 39);
                MouseDown_BorderBrushColorAnimation.To = Color.FromRgb(48, 48, 48);
                MouseDown_FontColorAnimation.To = Color.FromRgb(206, 206, 206);
                #endregion

                #region MouseUp
                MouseUp_BackgroundColorAnimation.To = Color.FromRgb(50, 50, 50);
                MouseUp_BorderBrushColorAnimation.To = Color.FromRgb(53, 53, 53);
                MouseUp_FontColorAnimation.To = Color.FromRgb(230, 230, 230);
                #endregion
            }

            internal static void HookChild(ref readonly Button button)
            {
                if (!Initialized) throw new InvalidOperationException("CheckBoxAnimator was not initialized");

                button.MouseEnter += Button_MouseEnter;
                button.MouseLeave += Button_MouseLeave;
                button.PreviewMouseDown += Button_MouseDown;
                button.PreviewMouseUp += Button_Up;
            }

            //

            #region EventHandler
            private static void Button_MouseEnter(object sender, MouseEventArgs e)
            {
                if (sender == null) return;
                Button checkBox = sender as Button;

                MouseEnter_Begin(ref checkBox);
            }

            private static void Button_MouseLeave(object sender, MouseEventArgs e)
            {
                if (sender == null) { return; }
                Button checkBox = sender as Button;

                MouseLeave_Begin(ref checkBox);
            }

            private static void Button_MouseDown(object sender, MouseButtonEventArgs e)
            {
                if (sender == null) return;
                Button checkBox = sender as Button;

                MouseDown_Begin(ref checkBox);
            }

            private static void Button_Up(object sender, RoutedEventArgs e)
            {
                if (sender == null) { return; }
                Button checkBox = sender as Button;

                MouseUp_Begin(ref checkBox);
            }
            #endregion

            //

            #region MouseEnter
            private static readonly ColorAnimation MouseEnter_BackgroundColorAnimation = new();
            private static readonly Storyboard MouseEnter_Storyboard = new();

            private static void MouseEnter_Begin(ref readonly Button button)
            {
                Storyboard.SetTarget(MouseEnter_BackgroundColorAnimation, button);
                Storyboard.SetTargetProperty(MouseEnter_BackgroundColorAnimation, BackgroundPropertyPath);

                MouseEnter_Storyboard.Begin();
            }
            #endregion

            #region MouseLeave
            private static readonly ColorAnimation MouseLeave_BackgroundColorAnimation = new();
            private static readonly Storyboard MouseLeave_Storyboard = new();

            private static void MouseLeave_Begin(ref readonly Button button)
            {
                Storyboard.SetTarget(MouseLeave_BackgroundColorAnimation, button);
                Storyboard.SetTargetProperty(MouseLeave_BackgroundColorAnimation, BackgroundPropertyPath);

                MouseLeave_Storyboard.Begin();
            }
            #endregion

            #region MouseDown
            private static readonly ColorAnimation MouseDown_BackgroundColorAnimation = new();
            private static readonly ColorAnimation MouseDown_BorderBrushColorAnimation = new();
            private static readonly ColorAnimation MouseDown_FontColorAnimation = new();
            private static readonly Storyboard MouseDown_Storyboard = new();

            private static void MouseDown_Begin(ref readonly Button button)
            {
                Storyboard.SetTarget(MouseDown_BackgroundColorAnimation, button);
                Storyboard.SetTargetProperty(MouseDown_BackgroundColorAnimation, BackgroundPropertyPath);

                Storyboard.SetTarget(MouseDown_BorderBrushColorAnimation, button);
                Storyboard.SetTargetProperty(MouseDown_BorderBrushColorAnimation, BorderBrushPropertyPath);

                Storyboard.SetTarget(MouseDown_FontColorAnimation, button);
                Storyboard.SetTargetProperty(MouseDown_FontColorAnimation, FontColorPropertyPath);

                MouseDown_Storyboard.Begin();
            }
            #endregion

            #region MouseUp
            private static readonly ColorAnimation MouseUp_BackgroundColorAnimation = new();
            private static readonly ColorAnimation MouseUp_BorderBrushColorAnimation = new();
            private static readonly ColorAnimation MouseUp_FontColorAnimation = new();
            private static readonly Storyboard MouseUp_Storyboard = new();

            private static void MouseUp_Begin(ref readonly Button button)
            {
                Storyboard.SetTarget(MouseUp_BackgroundColorAnimation, button);
                Storyboard.SetTargetProperty(MouseUp_BackgroundColorAnimation, BackgroundPropertyPath);

                Storyboard.SetTarget(MouseUp_BorderBrushColorAnimation, button);
                Storyboard.SetTargetProperty(MouseUp_BorderBrushColorAnimation, BorderBrushPropertyPath);

                Storyboard.SetTarget(MouseUp_FontColorAnimation, button);
                Storyboard.SetTargetProperty(MouseUp_FontColorAnimation, FontColorPropertyPath);

                MouseUp_Storyboard.Begin();
            }
            #endregion
        }
    }
}