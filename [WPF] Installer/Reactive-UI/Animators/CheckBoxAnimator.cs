using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Windows;
using System.Collections.Generic;

namespace Installer
{
    internal static class CheckBoxAnimator
    {
        private const Double Duration = 0.05;

        internal static Boolean Initialized { get; private set; } = false;

        internal static readonly List<CheckBox> HookedCheckBoxes = []; // ref list

        internal static void Initialize()
        {
            if (Initialized) return;

            SetDefaultColors();

            #region MouseEnter
            MouseEnter_Unchecked_BackgroundColorAnimation.Duration = TimeSpan.FromSeconds(Duration);
            MouseEnter_Unchecked_BorderBrushColorAnimation.Duration = TimeSpan.FromSeconds(Duration);
            MouseEnter_Checked_BackgroundColorAnimation.Duration = TimeSpan.FromSeconds(Duration);
            MouseEnter_Unchecked_Storyboard.Children.Add(MouseEnter_Unchecked_BackgroundColorAnimation);
            MouseEnter_Unchecked_Storyboard.Children.Add(MouseEnter_Unchecked_BorderBrushColorAnimation);
            MouseEnter_Checked_Storyboard.Children.Add(MouseEnter_Checked_BackgroundColorAnimation);
            #endregion

            #region MouseLeave
            MouseLeave_Unchecked_BackgroundColorAnimation.Duration = TimeSpan.FromSeconds(Duration);
            MouseLeave_Unchecked_BorderBrushColorAnimation.Duration = TimeSpan.FromSeconds(Duration);
            MouseLeave_Checked_BackgroundColorAnimation.Duration = TimeSpan.FromSeconds(Duration);
            MouseLeave_Unchecked_Storyboard.Children.Add(MouseLeave_Unchecked_BackgroundColorAnimation);
            MouseLeave_Unchecked_Storyboard.Children.Add(MouseLeave_Unchecked_BorderBrushColorAnimation);
            MouseLeave_Checked_Storyboard.Children.Add(MouseLeave_Checked_BackgroundColorAnimation);
            #endregion

            #region MouseDown
            MouseDown_Unchecked_BackgroundColorAnimation.Duration = TimeSpan.FromSeconds(Duration);
            MouseDown_Unchecked_BorderBrushColorAnimation.Duration = TimeSpan.FromSeconds(Duration);
            MouseDown_Checked_BackgroundColorAnimation.Duration = TimeSpan.FromSeconds(Duration);
            MouseDown_Unchecked_Storyboard.Children.Add(MouseDown_Unchecked_BackgroundColorAnimation);
            MouseDown_Unchecked_Storyboard.Children.Add(MouseDown_Unchecked_BorderBrushColorAnimation);
            MouseDown_Checked_Storyboard.Children.Add(MouseDown_Checked_BackgroundColorAnimation);
            #endregion

            #region MouseUp
            MouseUp_Unchecked_BorderThicknessAnimation.Duration = new Duration(TimeSpan.FromSeconds(Duration));
            MouseUp_Unchecked_BorderThicknessAnimation.To = new Thickness(1);
            MouseUp_Unchecked_BackgroundColorAnimation.Duration = TimeSpan.FromSeconds(Duration);
            MouseUp_Unchecked_BorderBrushColorAnimation.Duration = TimeSpan.FromSeconds(Duration);
            MouseUp_Checked_BorderThicknessAnimation.Duration = new Duration(TimeSpan.FromSeconds(Duration));
            MouseUp_Checked_BorderThicknessAnimation.To = new Thickness(0);
            MouseUp_Checked_BackgroundColorAnimation.Duration = TimeSpan.FromSeconds(Duration);
            MouseUp_Unchecked_Storyboard.Children.Add(MouseUp_Unchecked_BorderThicknessAnimation);
            MouseUp_Unchecked_Storyboard.Children.Add(MouseUp_Unchecked_BackgroundColorAnimation);
            MouseUp_Unchecked_Storyboard.Children.Add(MouseUp_Unchecked_BorderBrushColorAnimation);
            MouseUp_Checked_Storyboard.Children.Add(MouseUp_Checked_BorderThicknessAnimation);
            MouseUp_Checked_Storyboard.Children.Add(MouseUp_Checked_BackgroundColorAnimation);
            #endregion

            Initialized = true;
        }

        internal static void SetDefaultColors()
        {
            #region MouseEnter
            MouseEnter_Unchecked_BackgroundColorAnimation.To = Color.FromRgb(52, 52, 52);
            MouseEnter_Unchecked_BorderBrushColorAnimation.To = Color.FromRgb(160, 160, 160);
            MouseEnter_Checked_BackgroundColorAnimation.To = Color.FromRgb(72, 178, 233);
            #endregion

            #region MouseLeave
            MouseLeave_Unchecked_BackgroundColorAnimation.To = Color.FromRgb(39, 39, 39);
            MouseLeave_Unchecked_BorderBrushColorAnimation.To = Color.FromRgb(158, 158, 158);
            MouseLeave_Checked_BackgroundColorAnimation.To = Color.FromRgb(76, 194, 255);
            #endregion

            #region MouseDown
            MouseDown_Unchecked_BackgroundColorAnimation.To = Color.FromRgb(58, 58, 58);
            MouseDown_Unchecked_BorderBrushColorAnimation.To = Color.FromRgb(82, 82, 82);
            MouseDown_Checked_BackgroundColorAnimation.To = Color.FromRgb(69, 164, 213);
            #endregion

            #region MouseUp
            MouseUp_Unchecked_BackgroundColorAnimation.To = Color.FromRgb(52, 52, 52);
            MouseUp_Unchecked_BorderBrushColorAnimation.To = Color.FromRgb(160, 160, 160);
            MouseUp_Checked_BackgroundColorAnimation.To = Color.FromRgb(72, 178, 233);
            #endregion
        }

        internal static void HookChild(ref readonly CheckBox checkBox)
        {
            if (!Initialized) throw new InvalidOperationException("CheckBoxAnimator was not initialized");

            checkBox.MouseEnter += CheckBox_MouseEnter;
            checkBox.MouseLeave += CheckBox_MouseLeave;
            checkBox.PreviewMouseDown += CheckBox_MouseDown;
            checkBox.Checked += CheckBox_CheckStateChanged;
            checkBox.Unchecked += CheckBox_CheckStateChanged;

            checkBox.IsEnabledChanged += CheckBox_IsEnabledChanged;

            HookedCheckBoxes.Add(checkBox);
        }

        #region EventHandler
        private static void CheckBox_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender == null) return;
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.IsChecked == null) return;
            if (!checkBox.IsEnabled) return;

            if ((Boolean)checkBox.IsChecked)
            {
                MouseEnter_Checked_Begin(ref checkBox);
            }
            else
            {
                MouseEnter_Unchecked_Begin(ref checkBox);
            }
        }

        private static void CheckBox_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender == null) return;
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.IsChecked == null) return;
            if (!checkBox.IsEnabled) return;

            if ((Boolean)checkBox.IsChecked)
            {
                MouseLeave_Checked_Begin(ref checkBox);
            }
            else
            {
                MouseLeave_Unchecked_Begin(ref checkBox);
            }
        }

        private static void CheckBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender == null) return;
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.IsChecked == null) return;

            if ((Boolean)checkBox.IsChecked)
            {
                MouseDown_Begin_Checked(ref checkBox);
            }
            else
            {
                MouseDown_Begin_Unchecked(ref checkBox);
            }
        }

        private static void CheckBox_CheckStateChanged(object sender, RoutedEventArgs e)
        {
            if (sender == null) return;
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.IsChecked == null) return;

            if ((Boolean)checkBox.IsChecked)
            {
                MouseUp_Checked_Begin(ref checkBox);
            }
            else
            {
                MouseUp_Unchecked_Begin(ref checkBox);
            }
        }

        private static void CheckBox_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender == null) return;
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.IsChecked == null) return;

            if (checkBox.IsEnabled)
            {
                if ((Boolean)checkBox.IsChecked)
                {
                    if (ThemeAwareness.AppsUseLightTheme)
                    {
                        checkBox.Background = new SolidColorBrush(Color.FromRgb(ThemeAwareness.AccentPalette.LightMode_AccentColor[0], ThemeAwareness.AccentPalette.LightMode_AccentColor[1], ThemeAwareness.AccentPalette.LightMode_AccentColor[2]));
                    }
                    else
                    {
                        checkBox.Background = new SolidColorBrush(Color.FromRgb(ThemeAwareness.AccentPalette.DarkMode_AccentColor[0], ThemeAwareness.AccentPalette.DarkMode_AccentColor[1], ThemeAwareness.AccentPalette.DarkMode_AccentColor[2]));
                    }
                }
                else
                {
                    if (ThemeAwareness.AppsUseLightTheme)
                    {
                        checkBox.Background = new SolidColorBrush(Color.FromRgb(249, 249, 249));
                        checkBox.BorderBrush = new SolidColorBrush(Color.FromRgb(139, 139, 139));
                    }
                    else
                    {
                        checkBox.Background = new SolidColorBrush(Color.FromRgb(39, 39, 39));
                        checkBox.BorderBrush = new SolidColorBrush(Color.FromRgb(158, 158, 158));
                    }
                }
            }
            else
            {
                if ((Boolean)checkBox.IsChecked)
                {
                    if (ThemeAwareness.AppsUseLightTheme)
                    {
                        checkBox.Background = new SolidColorBrush(Color.FromRgb(197, 197, 197));
                        checkBox.BorderBrush = new SolidColorBrush(Color.FromRgb(176, 176, 176));
                    }
                    else
                    {
                        checkBox.Background = new SolidColorBrush(Color.FromRgb(76, 76, 76));
                        checkBox.BorderBrush = new SolidColorBrush(Color.FromRgb(91, 91, 91));
                    }
                }
                else
                {
                    if (ThemeAwareness.AppsUseLightTheme)
                    {
                        checkBox.Background = new SolidColorBrush(Color.FromRgb(251, 251, 251));
                        checkBox.BorderBrush = new SolidColorBrush(Color.FromRgb(197, 197, 197));
                    }
                    else
                    {
                        checkBox.Background = new SolidColorBrush(Color.FromRgb(43, 43, 43));
                        checkBox.BorderBrush = new SolidColorBrush(Color.FromRgb(76, 76, 76));
                    }
                }
            }
        }
        #endregion

        private static readonly PropertyPath BackgroundPropertyPath = new("(Control.Background).(SolidColorBrush.Color)");
        private static readonly PropertyPath BorderBrushPropertyPath = new("(Control.BorderBrush).(SolidColorBrush.Color)");
        private static readonly PropertyPath BorderThicknessPropertyPath = new("(Control.BorderThickness)");

        #region MouseEnter
        private static readonly ColorAnimation MouseEnter_Unchecked_BackgroundColorAnimation = new();
        private static readonly ColorAnimation MouseEnter_Unchecked_BorderBrushColorAnimation = new();
        private static readonly Storyboard MouseEnter_Unchecked_Storyboard = new();

        private static readonly ColorAnimation MouseEnter_Checked_BackgroundColorAnimation = new();
        private static readonly Storyboard MouseEnter_Checked_Storyboard = new();

        private static void MouseEnter_Unchecked_Begin(ref readonly CheckBox checkBox)
        {
            Storyboard.SetTarget(MouseEnter_Unchecked_BackgroundColorAnimation, checkBox);
            Storyboard.SetTargetProperty(MouseEnter_Unchecked_BackgroundColorAnimation, BackgroundPropertyPath);

            Storyboard.SetTarget(MouseEnter_Unchecked_BorderBrushColorAnimation, checkBox);
            Storyboard.SetTargetProperty(MouseEnter_Unchecked_BorderBrushColorAnimation, BorderBrushPropertyPath);

            MouseEnter_Unchecked_Storyboard.Begin();
        }

        private static void MouseEnter_Checked_Begin(ref readonly CheckBox checkBox)
        {
            Storyboard.SetTarget(MouseEnter_Checked_BackgroundColorAnimation, checkBox);
            Storyboard.SetTargetProperty(MouseEnter_Checked_BackgroundColorAnimation, BackgroundPropertyPath);

            MouseEnter_Checked_Storyboard.Begin();
        }

        internal static void SetColor_MouseEnter_Unchecked(Byte[] background, Byte[] borderBrush)
        {
            MouseEnter_Unchecked_BackgroundColorAnimation.To = Color.FromRgb(background[0], background[1], background[2]);
            MouseEnter_Unchecked_BorderBrushColorAnimation.To = Color.FromRgb(borderBrush[0], borderBrush[1], borderBrush[2]);
        }
        internal static void SetColor_MouseEnter_Checked(Byte[] background)
        {
            MouseEnter_Checked_BackgroundColorAnimation.To = Color.FromRgb(background[0], background[1], background[2]);
        }
        #endregion

        #region MouseLeave
        private static readonly ColorAnimation MouseLeave_Unchecked_BackgroundColorAnimation = new();
        private static readonly ColorAnimation MouseLeave_Unchecked_BorderBrushColorAnimation = new();
        private static readonly Storyboard MouseLeave_Unchecked_Storyboard = new();

        private static readonly ColorAnimation MouseLeave_Checked_BackgroundColorAnimation = new();
        private static readonly Storyboard MouseLeave_Checked_Storyboard = new();

        private static void MouseLeave_Unchecked_Begin(ref readonly CheckBox checkBox)
        {
            Storyboard.SetTarget(MouseLeave_Unchecked_BackgroundColorAnimation, checkBox);
            Storyboard.SetTargetProperty(MouseLeave_Unchecked_BackgroundColorAnimation, BackgroundPropertyPath);

            Storyboard.SetTarget(MouseLeave_Unchecked_BorderBrushColorAnimation, checkBox);
            Storyboard.SetTargetProperty(MouseLeave_Unchecked_BorderBrushColorAnimation, BorderBrushPropertyPath);

            MouseLeave_Unchecked_Storyboard.Begin();
        }

        private static void MouseLeave_Checked_Begin(ref readonly CheckBox checkBox)
        {
            Storyboard.SetTarget(MouseLeave_Checked_BackgroundColorAnimation, checkBox);
            Storyboard.SetTargetProperty(MouseLeave_Checked_BackgroundColorAnimation, BackgroundPropertyPath);

            MouseLeave_Checked_Storyboard.Begin();
        }

        internal static void SetColor_MouseLeave_Unchecked(Byte[] background, Byte[] borderBrush)
        {
            MouseLeave_Unchecked_BackgroundColorAnimation.To = Color.FromRgb(background[0], background[1], background[2]);
            MouseLeave_Unchecked_BorderBrushColorAnimation.To = Color.FromRgb(borderBrush[0], borderBrush[1], borderBrush[2]);
        }
        internal static void SetColor_MouseLeave_Checked(Byte[] background)
        {
            MouseLeave_Checked_BackgroundColorAnimation.To = Color.FromRgb(background[0], background[1], background[2]);
        }
        #endregion

        #region MouseDown
        private static readonly ColorAnimation MouseDown_Unchecked_BackgroundColorAnimation = new();
        private static readonly ColorAnimation MouseDown_Unchecked_BorderBrushColorAnimation = new();
        private static readonly Storyboard MouseDown_Unchecked_Storyboard = new();

        private static readonly ColorAnimation MouseDown_Checked_BackgroundColorAnimation = new();
        private static readonly Storyboard MouseDown_Checked_Storyboard = new();

        private static void MouseDown_Begin_Unchecked(ref readonly CheckBox checkBox)
        {
            Storyboard.SetTarget(MouseDown_Unchecked_BackgroundColorAnimation, checkBox);
            Storyboard.SetTargetProperty(MouseDown_Unchecked_BackgroundColorAnimation, BackgroundPropertyPath);

            Storyboard.SetTarget(MouseDown_Unchecked_BorderBrushColorAnimation, checkBox);
            Storyboard.SetTargetProperty(MouseDown_Unchecked_BorderBrushColorAnimation, BorderBrushPropertyPath);

            MouseDown_Unchecked_Storyboard.Begin();
        }

        private static void MouseDown_Begin_Checked(ref readonly CheckBox checkBox)
        {
            Storyboard.SetTarget(MouseDown_Checked_BackgroundColorAnimation, checkBox);
            Storyboard.SetTargetProperty(MouseDown_Checked_BackgroundColorAnimation, BackgroundPropertyPath);

            MouseDown_Checked_Storyboard.Begin();
        }

        internal static void SetColor_MouseDown_Unchecked(Byte[] background, Byte[] borderBrush)
        {
            MouseDown_Unchecked_BackgroundColorAnimation.To = Color.FromRgb(background[0], background[1], background[2]);
            MouseDown_Unchecked_BorderBrushColorAnimation.To = Color.FromRgb(borderBrush[0], borderBrush[1], borderBrush[2]);
        }
        internal static void SetColor_MouseDown_Checked( Byte[] background)
        {
            MouseDown_Checked_BackgroundColorAnimation.To = Color.FromRgb(background[0], background[1], background[2]);
        }
        #endregion

        #region MouseUp
        private static readonly ThicknessAnimation MouseUp_Unchecked_BorderThicknessAnimation = new();
        private static readonly ColorAnimation MouseUp_Unchecked_BackgroundColorAnimation = new();
        private static readonly ColorAnimation MouseUp_Unchecked_BorderBrushColorAnimation = new();
        private static readonly Storyboard MouseUp_Unchecked_Storyboard = new();

        private static readonly ThicknessAnimation MouseUp_Checked_BorderThicknessAnimation = new();
        private static readonly ColorAnimation MouseUp_Checked_BackgroundColorAnimation = new();
        private static readonly Storyboard MouseUp_Checked_Storyboard = new();

        private static void MouseUp_Unchecked_Begin(ref readonly CheckBox checkBox)
        {
            Storyboard.SetTarget(MouseUp_Unchecked_BorderThicknessAnimation, checkBox);
            Storyboard.SetTargetProperty(MouseUp_Unchecked_BorderThicknessAnimation, BorderThicknessPropertyPath);

            Storyboard.SetTarget(MouseUp_Unchecked_BackgroundColorAnimation, checkBox);
            Storyboard.SetTargetProperty(MouseUp_Unchecked_BackgroundColorAnimation, BackgroundPropertyPath);

            Storyboard.SetTarget(MouseUp_Unchecked_BorderBrushColorAnimation, checkBox);
            Storyboard.SetTargetProperty(MouseUp_Unchecked_BorderBrushColorAnimation, BorderBrushPropertyPath);

            MouseUp_Unchecked_Storyboard.Begin();
        }

        private static void MouseUp_Checked_Begin(ref readonly CheckBox checkBox)
        {
            Storyboard.SetTarget(MouseUp_Checked_BorderThicknessAnimation, checkBox);
            Storyboard.SetTargetProperty(MouseUp_Checked_BorderThicknessAnimation, BorderThicknessPropertyPath);

            Storyboard.SetTarget(MouseUp_Checked_BackgroundColorAnimation, checkBox);
            Storyboard.SetTargetProperty(MouseUp_Checked_BackgroundColorAnimation, BackgroundPropertyPath);

            MouseUp_Checked_Storyboard.Begin();
        }

        internal static void SetColor_MouseUp_Unchecked(Byte[] background, Byte[] borderBrush)
        {
            MouseUp_Unchecked_BackgroundColorAnimation.To = Color.FromRgb(background[0], background[1], background[2]);
            MouseUp_Unchecked_BorderBrushColorAnimation.To = Color.FromRgb(borderBrush[0], borderBrush[1], borderBrush[2]);
        }
        internal static void SetColor_MouseUp_Checked(Byte[] background)
        {
            MouseUp_Checked_BackgroundColorAnimation.To = Color.FromRgb(background[0], background[1], background[2]);
        }
        #endregion
    }
}