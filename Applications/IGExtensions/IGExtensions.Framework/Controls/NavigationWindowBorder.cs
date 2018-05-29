using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace IGExtensions.Framework.Controls
{
    [TemplatePart(Name = WindowTouchBorderName, Type = typeof(Border))]
    [TemplatePart(Name = WindowVisibleBorderName, Type = typeof(Border))]
    //top
    [TemplatePart(Name = WindowTopLeftResizeElementName, Type = typeof(Border))]
    [TemplatePart(Name = WindowTopMiddleResizeElementName, Type = typeof(Border))]
    [TemplatePart(Name = WindowTopRightResizeElementName, Type = typeof(Border))]
    //middle
    [TemplatePart(Name = WindowMiddleLeftResizeElementName, Type = typeof(Border))]
    [TemplatePart(Name = WindowMiddleRightResizeElementName, Type = typeof(Border))]
    //bottom
    [TemplatePart(Name = WindowBottomLeftResizeElementName, Type = typeof(Border))]
    [TemplatePart(Name = WindowBottomMiddleResizeElementName, Type = typeof(Border))]
    [TemplatePart(Name = WindowBottomRightResizeElementName, Type = typeof(Border))]
    public class NavigationWindowBorder : ContentControl // Border // FrameworkElement
    {
        #region Fields
        protected List<Border> WindowResizeElements = new List<Border>();
        protected bool IsTemplateApplied;
        public const string WindowVisibleBorderName = "WindowVisibleBorder";
        protected Border    WindowVisibleBorder = null;
        public const string WindowTouchBorderName = "WindowTouchBorder";
        protected Border    WindowTouchBorder = null;
        //top
        public const string WindowTopLeftResizeElementName = "WindowTopLeftResizeElement";
        protected Border    WindowTopLeftResizeElement = null;
        public const string WindowTopMiddleResizeElementName = "WindowTopMiddleResizeElement";
        protected Border    WindowTopMiddleResizeElement = null;
        public const string WindowTopRightResizeElementName = "WindowTopRightResizeElement";
        protected Border    WindowTopRightResizeElement = null;
        //middle
        public const string WindowMiddleLeftResizeElementName = "WindowMiddleLeftResizeElement";
        protected Border    WindowMiddleLeftResizeElement = null;
        public const string WindowMiddleRightResizeElementName = "WindowMiddleRightResizeElement";
        protected Border    WindowMiddleRightResizeElement = null;
        //bottom
        public const string WindowBottomLeftResizeElementName = "WindowBottomLeftResizeElement";
        protected Border    WindowBottomLeftResizeElement = null;
        public const string WindowBottomMiddleResizeElementName = "WindowBottomMiddleResizeElement";
        protected Border    WindowBottomMiddleResizeElement = null;
        public const string WindowBottomRightResizeElementName = "WindowBottomRightResizeElement";
        protected Border    WindowBottomRightResizeElement = null;
        #endregion
        
        public NavigationWindowBorder()
        {

            this.BorderThickness = new Thickness(2);
            this.BorderBrush = NamedColors.DodgerBlue.Brush;
     
            //this.DefaultStyleKey = typeof(NavigationWindowBorder);
            const string stylePath = "/IGExtensions.Framework;component/Controls/NavigationWindowBorder.xaml";
            this.ApplyStyle(stylePath);

            this.Loaded += NavigationWindowBorder_Loaded;

        }

        void NavigationWindowBorder_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateWindowBorder();
                      
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            WindowTouchBorder =     GetTemplateChild(WindowTouchBorderName) as Border;
            WindowVisibleBorder =   GetTemplateChild(WindowVisibleBorderName) as Border;

            WindowMiddleLeftResizeElement = GetTemplateChild(WindowMiddleLeftResizeElementName) as Border;
            if (WindowMiddleLeftResizeElement != null)
                WindowResizeElements.Add(WindowMiddleLeftResizeElement);
            WindowMiddleRightResizeElement = GetTemplateChild(WindowMiddleRightResizeElementName) as Border;
            if (WindowMiddleRightResizeElement != null)
                WindowResizeElements.Add(WindowMiddleRightResizeElement);
            WindowBottomLeftResizeElement = GetTemplateChild(WindowBottomLeftResizeElementName) as Border;
            if (WindowBottomLeftResizeElement != null)
                WindowResizeElements.Add(WindowBottomLeftResizeElement);
            
            WindowBottomMiddleResizeElement = GetTemplateChild(WindowBottomMiddleResizeElementName) as Border;
            if (WindowBottomMiddleResizeElement != null)
                WindowResizeElements.Add(WindowBottomMiddleResizeElement);
            WindowBottomRightResizeElement = GetTemplateChild(WindowBottomRightResizeElementName) as Border;
            if (WindowBottomRightResizeElement != null)
                WindowResizeElements.Add(WindowBottomRightResizeElement);


            WindowTopLeftResizeElement = GetTemplateChild(WindowTopLeftResizeElementName) as Border;
            if (WindowTopLeftResizeElement != null)
                WindowResizeElements.Add(WindowTopLeftResizeElement);
            WindowTopMiddleResizeElement = GetTemplateChild(WindowTopMiddleResizeElementName) as Border;
            if (WindowTopMiddleResizeElement != null)
                WindowResizeElements.Add(WindowTopMiddleResizeElement);
            WindowTopRightResizeElement = GetTemplateChild(WindowTopRightResizeElementName) as Border;
            if (WindowTopRightResizeElement != null)
                WindowResizeElements.Add(WindowTopRightResizeElement);
            
            foreach (var resizeElement in this.WindowResizeElements)
            {
                resizeElement.MouseMove += OnWindowResize;
                resizeElement.MouseLeftButtonDown += OnWindowResizeBegin;
                resizeElement.MouseLeftButtonUp += OnWindowResizeStop;
            }
             
            this.IsTemplateApplied = true;
        }

        #region Properties
        public const string TargetWindowPropertyName = "TargetWindow";
        public static DependencyProperty TargetWindowProperty = DependencyProperty.Register(
            TargetWindowPropertyName, typeof(Window), typeof(NavigationWindowBorder),
            new PropertyMetadata(null, (sender, e) =>
            {
                (sender as NavigationWindowBorder).OnPropertyChanged(new PropertyChangedEventArgs(TargetWindowPropertyName));
            }));

        /// <summary>
        /// Gets or sets main window of an application
        /// </summary>
        public Window TargetWindow
        {
            get
            {
                return (Window)GetValue(TargetWindowProperty);
            }
            set
            {
                SetValue(TargetWindowProperty, value);
            }
        } 
        #endregion

        #region Window Resize
        private bool _isResizing = false;
        private const double CursorOffsetSmall = 3;
        private const double CursorOffsetLarge = 5;
        private void OnWindowResizeBegin(object sender, MouseButtonEventArgs e)
        {
            if (this.TargetWindow != null && sender is Border)
            {
                _isResizing = true;
                ((Border)sender).CaptureMouse();
            }
        }
        private void OnWindowResizeStop(object sender, MouseButtonEventArgs e)
        {
            if (this.TargetWindow != null && sender is Border)
            {
                _isResizing = false;
                ((Border)sender).ReleaseMouseCapture();
            }
        }
        private void OnWindowResize(object sender, MouseEventArgs e)
        {
#if WPF
            if (this.TargetWindow != null && _isResizing && (sender is Border))
            {
                var x = e.GetPosition(this).X;
                var y = e.GetPosition(this).Y;

                var mode = ((Border)sender).Name.ToLower();
                if (mode.Contains("left"))
                {
                    x -= CursorOffsetSmall;
                    if ((this.TargetWindow.Width - x >= this.TargetWindow.MinWidth) &&
                        (this.TargetWindow.Width - x <= this.TargetWindow.MaxWidth))
                    {
                        this.TargetWindow.Width -= x;
                        this.TargetWindow.Left += x;
                    }
                }
                if (mode.Contains("right"))
                {
                    this.TargetWindow.Width = Math.Max(this.TargetWindow.MinWidth,
                                            Math.Min(this.TargetWindow.MaxWidth, x + CursorOffsetLarge));
                }
                if (mode.Contains("top"))
                {
                    y -= CursorOffsetSmall;
                    if ((this.TargetWindow.Height - y >= this.TargetWindow.MinHeight) &&
                        (this.TargetWindow.Height - y <= this.TargetWindow.MaxHeight))
                    {
                        this.TargetWindow.Height -= y;
                        this.TargetWindow.Top += y;
                    }
                }
                if (mode.Contains("bottom"))
                {
                    this.TargetWindow.Height = Math.Max(this.TargetWindow.MinHeight,
                                             Math.Min(this.TargetWindow.MaxHeight, y + CursorOffsetSmall));
                }
            } 
#endif
        }
        #endregion

        private void OnPropertyChanged(PropertyChangedEventArgs ea)
        {
            switch (ea.PropertyName)
            {
#if WPF
                case "TargetWindow":
                    {
                        if (this.TargetWindow != null)
                            this.TargetWindow.StateChanged += OnWindowStateChanged;
                        break;
                    } 
#endif
                //case "BorderBrush":
                case "BorderThickness":
                    {
                        //UpdateBorderThickness();
                        UpdateWindowBorder();
                        break;
                    }
            }
        }
        private void OnWindowStateChanged(object sender, EventArgs e)
        {
            UpdateWindowBorder();
        }

        private void UpdateWindowBorder()
        {
            if (TargetWindow == null) return;

            switch (TargetWindow.WindowState)
            {
                case WindowState.Normal:
                    this.WindowVisibleBorder.Visibility = Visibility.Visible;
                    this.WindowTouchBorder.BorderThickness = new Thickness(0);
                    //this.WindowTouchBorder.BorderThickness =new Thickness(3);
                    break;
                case WindowState.Maximized:
                    this.WindowVisibleBorder.Visibility = Visibility.Collapsed;
                    this.WindowTouchBorder.BorderThickness = new Thickness(0);
                    break;
            }
        }


        private void UpdateBorderThickness()
        {
            foreach (var resizeElement in WindowResizeElements)
            {
                var name = resizeElement.Name.ToLower();
                if (name.Contains("top"))
                {
                    resizeElement.Width = this.BorderThickness.Top;
                }
                else if (name.Contains("bottom"))
                {
                    resizeElement.Width = this.BorderThickness.Bottom;
                }
                else if (name.Contains("left"))
                {
                    resizeElement.Width = this.BorderThickness.Left;
                }
                else if (name.Contains("right"))
                {
                    resizeElement.Width = this.BorderThickness.Right;
                }
            }

        }

        private void UpdateBorderBrush()
        {
            foreach (var resizeElement in WindowResizeElements)
            {
                //resizeElement.Background = this.BorderBrush;
            }
        }
    }
}