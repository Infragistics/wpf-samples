using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace IGExtensions.Framework.Controls
{

    
#if SILVERLIGHT
    public class NavigationWindow : System.Windows.Controls.ChildWindow 
#else
    public class NavigationWindow : System.Windows.Window 
#endif
    {

#if SILVERLIGHT
        public void ShowDialog()
        {
            this.Show();
        }
#else // WPF
        public NavigationWindow()
        {
            this.Loaded += OnWindowLoaded;
            this.StateChanged += OnWindowStateChanged;
        }

        private void OnWindowStateChanged(object sender, EventArgs e)
        {
            this.UpdateWindowStateButtons();
        }
        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            this.UpdateWindowStateButtons();
        }

        public new void Show()
        {
            this.ShowDialog();
        }
        public void ToggleWindowState()
        {
            if (this.WindowState == System.Windows.WindowState.Maximized)
                this.WindowState = System.Windows.WindowState.Normal;
            else //if (this.WindowState == System.Windows.WindowState.Normal)
                this.WindowState = System.Windows.WindowState.Maximized;
        }
        public void OnWindowMinmizeButtonClick(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        public void OnWindowMaximizeButtonClick(object sender, RoutedEventArgs e)
        {
            ToggleWindowState();
        }
        public void OnWindowHeaderMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount > 1)
            {
                ToggleWindowState();
            }
            else
            {
                this.DragMove();
            }
        }
        public void UpdateWindowStateButtons()
        {
            switch (this.WindowState)
            {
                case WindowState.Normal:
                    this.WindowMaximizeVisibility = Visibility.Visible;
                    this.WindowRestoreVisibility = Visibility.Collapsed;
                    break;
                case WindowState.Maximized:
                    this.WindowMaximizeVisibility = Visibility.Collapsed;
                    this.WindowRestoreVisibility = Visibility.Visible;
                    break;
            }
        } 
#endif

        public void OnWindowCloseButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
        #region Properties
        public const string WindowRestoreVisibilityPropertyName = "WindowRestoreVisibility";
        public static DependencyProperty WindowRestoreVisibilityProperty = DependencyProperty.Register(
            WindowRestoreVisibilityPropertyName, typeof(Visibility), typeof(NavigationWindow),
            new PropertyMetadata(Visibility.Visible));
         
        /// <summary>
        /// Gets or sets visibilty of window restore button
        /// </summary>
        public Visibility WindowRestoreVisibility
        {
            get
            {
                return (Visibility)GetValue(WindowRestoreVisibilityProperty);
            }
            set
            {
                SetValue(WindowRestoreVisibilityProperty, value);
            }
        }

        public const string WindowMaximizeVisibilityPropertyName = "WindowMaximizeVisibility";
        public static DependencyProperty WindowMaximizeVisibilityProperty = DependencyProperty.Register(
            WindowMaximizeVisibilityPropertyName, typeof(Visibility), typeof(NavigationWindow),
            new PropertyMetadata(Visibility.Collapsed));

        /// <summary>
        /// Gets or sets visibilty of window maximize button
        /// </summary>
        public Visibility WindowMaximizeVisibility
        {
            get
            {
                return (Visibility)GetValue(WindowMaximizeVisibilityProperty);
            }
            set
            {
                SetValue(WindowMaximizeVisibilityProperty, value);
            }
        } 
        #endregion
        private void OnPropertyChanged(PropertyChangedEventArgs ea)
        {
            //switch (ea.PropertyName)
            //{
                //case "TargetWindow":
                //    {
                //        break;
                //    }
            //}
        }
    }
}