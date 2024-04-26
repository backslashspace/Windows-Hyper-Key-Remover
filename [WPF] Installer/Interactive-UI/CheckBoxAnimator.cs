using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Windows;

namespace Installer
{
    internal static class CheckBoxAnimator
    {
        internal static Boolean Initialized { get; private set; } = false;

        internal static void Initialize()
        {
            if (Initialized) { return; }

            SetDefaultColors();

            #region MouseEnter
            MouseEnter_Unchecked_BackgroundColorAnimation.Duration = TimeSpan.FromSeconds(0.05);
            MouseEnter_Unchecked_BorderBrushColorAnimation.Duration = TimeSpan.FromSeconds(0.05);
            MouseEnter_Checked_BackgroundColorAnimation.Duration = TimeSpan.FromSeconds(0.05);
            MouseEnter_Unchecked_Storyboard.Children.Add(MouseEnter_Unchecked_BackgroundColorAnimation);
            MouseEnter_Unchecked_Storyboard.Children.Add(MouseEnter_Unchecked_BorderBrushColorAnimation);
            MouseEnter_Checked_Storyboard.Children.Add(MouseEnter_Checked_BackgroundColorAnimation);
            #endregion

            #region MouseLeave
            MouseLeave_Unchecked_BackgroundColorAnimation.Duration = TimeSpan.FromSeconds(0.05);
            MouseLeave_Unchecked_BorderBrushColorAnimation.Duration = TimeSpan.FromSeconds(0.05);
            MouseLeave_Checked_BackgroundColorAnimation.Duration = TimeSpan.FromSeconds(0.05);
            MouseLeave_Unchecked_Storyboard.Children.Add(MouseLeave_Unchecked_BackgroundColorAnimation);
            MouseLeave_Unchecked_Storyboard.Children.Add(MouseLeave_Unchecked_BorderBrushColorAnimation);
            MouseLeave_Checked_Storyboard.Children.Add(MouseLeave_Checked_BackgroundColorAnimation);
            #endregion

            #region MouseDown
            MouseDown_Unchecked_BackgroundColorAnimation.Duration = TimeSpan.FromSeconds(0.05);
            MouseDown_Unchecked_BorderBrushColorAnimation.Duration = TimeSpan.FromSeconds(0.05);
            MouseDown_Checked_BackgroundColorAnimation.Duration = TimeSpan.FromSeconds(0.05);
            MouseDown_Unchecked_Storyboard.Children.Add(MouseDown_Unchecked_BackgroundColorAnimation);
            MouseDown_Unchecked_Storyboard.Children.Add(MouseDown_Unchecked_BorderBrushColorAnimation);
            MouseDown_Checked_Storyboard.Children.Add(MouseDown_Checked_BackgroundColorAnimation);
            #endregion

            #region MouseUp
            MouseUp_Unchecked_BorderThicknessAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.05));
            MouseUp_Unchecked_BorderThicknessAnimation.To = new Thickness(1);
            MouseUp_Unchecked_BackgroundColorAnimation.Duration = TimeSpan.FromSeconds(0.05);
            MouseUp_Unchecked_BorderBrushColorAnimation.Duration = TimeSpan.FromSeconds(0.05);
            MouseUp_Checked_BorderThicknessAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.05));
            MouseUp_Checked_BorderThicknessAnimation.To = new Thickness(0);
            MouseUp_Checked_BackgroundColorAnimation.Duration = TimeSpan.FromSeconds(0.05);
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
            if (!Initialized)
            {
                throw new InvalidOperationException("CheckBoxAnimator was not initialized");
            }

            checkBox.MouseEnter += CheckBox_MouseEnter.Invoke;
            checkBox.MouseLeave += CheckBox_MouseLeave.Invoke;
            checkBox.PreviewMouseDown += CheckBox_MouseDown.Invoke;
            checkBox.Checked += CheckBox_CheckStateChanged.Invoke;
            checkBox.Unchecked += CheckBox_CheckStateChanged.Invoke;
        }

        #region EventDelegates
        private static readonly Action<object, MouseEventArgs> CheckBox_MouseEnter = delegate (object sender, MouseEventArgs e)
        {
            if (sender == null) { return; }
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.IsChecked == null) { return; }

            if ((Boolean)checkBox.IsChecked)
            {
                MouseEnter_Checked_Begin(ref checkBox);
            }
            else
            {
                MouseEnter_Unchecked_Begin(ref checkBox);
            }
        };

        private static readonly Action<object, MouseEventArgs> CheckBox_MouseLeave = delegate (object sender, MouseEventArgs e)
        {
            if (sender == null) { return; }
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.IsChecked == null) { return; }

            if ((Boolean)checkBox.IsChecked)
            {
                MouseLeave_Checked_Begin(ref checkBox);
            }
            else
            {
                MouseLeave_Unchecked_Begin(ref checkBox);
            }
        };

        private static readonly Action<object, MouseButtonEventArgs> CheckBox_MouseDown = delegate (object sender, MouseButtonEventArgs e)
        {
            if (sender == null) { return; }
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.IsChecked == null) { return; }

            if ((Boolean)checkBox.IsChecked)
            {
                MouseDown_Begin_Checked(ref checkBox);
            }
            else
            {
                MouseDown_Begin_Unchecked(ref checkBox);
            }
        };

        private static readonly Action<object, RoutedEventArgs> CheckBox_CheckStateChanged = delegate (object sender, RoutedEventArgs e)
        {
            if (sender == null) { return; }
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.IsChecked == null) { return; }

            if ((Boolean)checkBox.IsChecked)
            {
                MouseUp_Checked_Begin(ref checkBox);
            }
            else
            {
                MouseUp_Unchecked_Begin(ref checkBox);
            }
        };
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

        internal static void MouseEnter_Unchecked_UpdateScheme(ref readonly Byte Back_R, ref readonly Byte Back_G, ref readonly Byte Back_B, ref readonly Byte Border_R, ref readonly Byte Border_G, ref readonly Byte Border_B)
        {
            MouseEnter_Unchecked_BackgroundColorAnimation.To = Color.FromRgb(Back_R, Back_G, Back_B);
            MouseEnter_Unchecked_BorderBrushColorAnimation.To = Color.FromRgb(Border_R, Border_G, Border_B);
        }
        internal static void MouseEnter_Checked_UpdateScheme(ref readonly Byte Back_R, ref readonly Byte Back_G, ref readonly Byte Back_B)
        {
            MouseEnter_Checked_BackgroundColorAnimation.To = Color.FromRgb(Back_R, Back_G, Back_B);
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

        internal static void MouseLeave_Unchecked_UpdateScheme(ref readonly Byte Back_R, ref readonly Byte Back_G, ref readonly Byte Back_B, ref readonly Byte Border_R, ref readonly Byte Border_G, ref readonly Byte Border_B)
        {
            MouseLeave_Unchecked_BackgroundColorAnimation.To = Color.FromRgb(Back_R, Back_G, Back_B);
            MouseLeave_Unchecked_BorderBrushColorAnimation.To = Color.FromRgb(Border_R, Border_G, Border_B);
        }
        internal static void MouseLeave_Checked_UpdateScheme(ref readonly Byte Back_R, ref readonly Byte Back_G, ref readonly Byte Back_B)
        {
            MouseLeave_Checked_BackgroundColorAnimation.To = Color.FromRgb(Back_R, Back_G, Back_B);
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

        internal static void MouseDown_Unchecked_UpdateScheme(ref readonly Byte Back_R, ref readonly Byte Back_G, ref readonly Byte Back_B, ref readonly Byte Border_R, ref readonly Byte Border_G, ref readonly Byte Border_B)
        {
            MouseDown_Unchecked_BackgroundColorAnimation.To = Color.FromRgb(Back_R, Back_G, Back_B);
            MouseDown_Unchecked_BorderBrushColorAnimation.To = Color.FromRgb(Border_R, Border_G, Border_B);
        }
        internal static void MouseDown_Checked_UpdateScheme(ref readonly Byte Back_R, ref readonly Byte Back_G, ref readonly Byte Back_B)
        {
            MouseDown_Checked_BackgroundColorAnimation.To = Color.FromRgb(Back_R, Back_G, Back_B);
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

        internal static void MouseUp_Unchecked_UpdateScheme(ref readonly Byte Back_R, ref readonly Byte Back_G, ref readonly Byte Back_B, ref readonly Byte Border_R, ref readonly Byte Border_G, ref readonly Byte Border_B)
        {
            MouseUp_Unchecked_BackgroundColorAnimation.To = Color.FromRgb(Back_R, Back_G, Back_B);
            MouseUp_Unchecked_BorderBrushColorAnimation.To = Color.FromRgb(Border_R, Border_G, Border_B);
        }
        internal static void MouseUp_Checked_UpdateScheme(ref readonly Byte Back_R, ref readonly Byte Back_G, ref readonly Byte Back_B)
        {
            MouseUp_Checked_BackgroundColorAnimation.To = Color.FromRgb(Back_R, Back_G, Back_B);
        }
        #endregion
    }
}