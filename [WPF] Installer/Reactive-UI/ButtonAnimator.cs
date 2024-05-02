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
        internal static Boolean Initialized { get; private set; } = false;

        internal static void Initialize()
        {
            if (Initialized) return;

            SetDefaultColors();

            Double duration = 0.075;

            #region MouseEnter
            MouseEnter_BackgroundColorAnimation.Duration = TimeSpan.FromSeconds(duration);
            MouseEnter_BorderBrushColorAnimation.Duration = TimeSpan.FromSeconds(duration);
            MouseEnter_Storyboard.Children.Add(MouseEnter_BackgroundColorAnimation);
            MouseEnter_Storyboard.Children.Add(MouseEnter_BorderBrushColorAnimation);
            #endregion

            #region MouseLeave
            MouseLeave_BackgroundColorAnimation.Duration = TimeSpan.FromSeconds(duration);
            MouseLeave_BorderBrushColorAnimation.Duration = TimeSpan.FromSeconds(duration);
            MouseLeave_Storyboard.Children.Add(MouseLeave_BackgroundColorAnimation);
            MouseLeave_Storyboard.Children.Add(MouseLeave_BorderBrushColorAnimation);
            #endregion

            #region MouseDown
            MouseDown_BackgroundColorAnimation.Duration = TimeSpan.FromSeconds(duration);
            MouseDown_BorderBrushColorAnimation.Duration = TimeSpan.FromSeconds(duration);
            MouseDown_Storyboard.Children.Add(MouseDown_BackgroundColorAnimation);
            MouseDown_Storyboard.Children.Add(MouseDown_BorderBrushColorAnimation);
            #endregion

            #region MouseUp
            MouseUp_BackgroundColorAnimation.Duration = TimeSpan.FromSeconds(duration);
            MouseUp_BorderBrushColorAnimation.Duration = TimeSpan.FromSeconds(duration);
            MouseUp_Storyboard.Children.Add(MouseUp_BackgroundColorAnimation);
            MouseUp_Storyboard.Children.Add(MouseUp_BorderBrushColorAnimation);
            #endregion

            Initialized = true;
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

        private static readonly PropertyPath BackgroundPropertyPath = new("(Control.Background).(SolidColorBrush.Color)");
        private static readonly PropertyPath BorderBrushPropertyPath = new("(Control.BorderBrush).(SolidColorBrush.Color)");

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

        internal static void SetColor_MouseEnter(ref readonly Int16[] background, ref readonly Int16[] border)
        {
            MouseEnter_BackgroundColorAnimation.To = Color.FromRgb((Byte)background[0], (Byte)background[1], (Byte)background[2]);
            MouseEnter_BorderBrushColorAnimation.To = Color.FromRgb((Byte)border[0], (Byte)border[1], (Byte)border[2]);
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

        internal static void SetColor_MouseLeave(ref readonly Int16[] background, ref readonly Int16[] border)
        {
            MouseLeave_BackgroundColorAnimation.To = Color.FromRgb((Byte)background[0], (Byte)background[1], (Byte)background[2]);
            MouseLeave_BorderBrushColorAnimation.To = Color.FromRgb((Byte)border[0], (Byte)border[1], (Byte)border[2]);
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

        internal static void SetColor_MouseDown(ref readonly Int16[] background)
        {
            MouseDown_BackgroundColorAnimation.To = Color.FromRgb((Byte)background[0], (Byte)background[1], (Byte)background[2]);
            MouseDown_BorderBrushColorAnimation.To = Color.FromRgb((Byte)background[0], (Byte)background[1], (Byte)background[2]);
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

        internal static void SetColor_MouseUp(ref readonly Int16[] background, ref readonly Int16[] border)
        {
            MouseUp_BackgroundColorAnimation.To = Color.FromRgb((Byte)background[0], (Byte)background[1], (Byte)background[2]);
            MouseUp_BorderBrushColorAnimation.To = Color.FromRgb((Byte)border[0], (Byte)border[1], (Byte)border[2]);
        }
        #endregion
    }
}