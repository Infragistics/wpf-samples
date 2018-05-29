using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
//using IGExtensions.Framework.Extensions;      // FrameworkElementEx.ApplyStyle()
#if SILVERLIGHT
using System.Windows.Browser;
#else // WPF
using System.Diagnostics;
#endif

namespace IGExtensions.Framework.Controls
{
    /// <summary>
    /// Represents a navigation link button 
    /// </summary>
    public class NavigationButton : ToggleButton
    {
        /// <summary>
        /// Constructs an instance of the <see cref="NavigationButton"/>
        /// </summary>
        public NavigationButton()
        {
            // add the generic style to the control's resources
             this.DefaultStyleKey = typeof(NavigationButton);
            //const string stylePath = "/IGExtensions.Framework;component/Controls/NavigationButton.xaml";
            // this.ApplyStyle(stylePath);
        }

        //protected override void OnApplyTemplate()
        //{
        //     base.OnApplyTemplate();
 
        //}

        #region NavigationHoverBackground
        public const string NavigationHoverBackgroundPropertyName = "NavigationHoverBackground";
        public static DependencyProperty NavigationHoverBackgroundProperty = DependencyProperty.Register(
            NavigationHoverBackgroundPropertyName, typeof(Brush), typeof(NavigationButton),
            new PropertyMetadata(new SolidColorBrush(Colors.LightGray), (sender, e) =>
                OnPropertyChanged(new PropertyChangedEventArgs(NavigationHoverBackgroundPropertyName))));

        /// <summary>
        /// Gets or sets Hover Brush of the NavigationButton
        /// </summary>
        public Brush NavigationHoverBackground
        {
            get { return (Brush)GetValue(NavigationHoverBackgroundProperty); }
            set { SetValue(NavigationHoverBackgroundProperty, value); }
        } 
        #endregion

        #region NavigationHoverForeground
        public const string NavigationHoverForegroundPropertyName = "NavigationHoverForeground";
        public static DependencyProperty NavigationHoverForegroundProperty = DependencyProperty.Register(
            NavigationHoverForegroundPropertyName, typeof(Brush), typeof(NavigationButton),
            new PropertyMetadata(new SolidColorBrush(Colors.Black), (sender, e) =>
                OnPropertyChanged(new PropertyChangedEventArgs(NavigationHoverForegroundPropertyName))));

        /// <summary>
        /// Gets or sets Hover Brush of the NavigationButton
        /// </summary>
        public Brush NavigationHoverForeground
        {
            get { return (Brush)GetValue(NavigationHoverForegroundProperty); }
            set { SetValue(NavigationHoverForegroundProperty, value); }
        }
        
        #endregion

        #region NavigationSelectedBrush
        public const string NavigationSelectedBrushPropertyName = "NavigationSelectedBrush";
        public static DependencyProperty NavigationSelectedBrushProperty = DependencyProperty.Register(
            NavigationSelectedBrushPropertyName, typeof(Brush), typeof(NavigationButton),
            new PropertyMetadata(new SolidColorBrush(Colors.Blue), (sender, e) =>
                OnPropertyChanged(new PropertyChangedEventArgs(NavigationSelectedBrushPropertyName))));

        /// <summary>
        /// Gets or sets Selected Brush of the NavigationButton
        /// </summary>
        public Brush NavigationSelectedBrush
        {
            get { return (Brush)GetValue(NavigationSelectedBrushProperty); }
            set { SetValue(NavigationSelectedBrushProperty, value); }
        } 
        #endregion

        #region NavigationUri

        /// <summary>
        /// Identifies the <see cref="NavigationUri"/> dependency property. 
        /// </summary>
        public static DependencyProperty NavigationUriProperty = DependencyProperty.Register(
            NavigationUriPropertyName, typeof(Uri), typeof(NavigationButton), 
            new PropertyMetadata(null, (sender, e) => 
                OnPropertyChanged(new PropertyChangedEventArgs(NavigationUriPropertyName))));
            //   typeof(Uri), typeof(NavigationButton), new PropertyMetadata(new PropertyChangedCallback(OnNavigationUriChanged)));
     
        public const string NavigationUriPropertyName = "NavigationUri";
       
        /// <summary>
        /// Gets or sets the URI to navigate to when the <see cref="NavigationButton"/> is clicked. 
        /// </summary>
        public Uri NavigationUri
        {
            get { return (Uri)this.GetValue(NavigationUriProperty); }
            set { this.SetValue(NavigationUriProperty, value); }
        }

        #endregion  

        #region NavigationTarget

        /// <summary>
        /// Identifies the <see cref="IsNavigationTargetExternal"/> dependency property. 
        /// </summary>
        public static readonly DependencyProperty IsNavigationTargetExternalProperty = DependencyProperty.Register(
            IsNavigationTargetExternalPropertyName, typeof(bool), typeof(NavigationButton), 
            new PropertyMetadata(false, (sender, e) => 
               OnPropertyChanged(new PropertyChangedEventArgs(IsNavigationTargetExternalPropertyName))));
      
        public const string IsNavigationTargetExternalPropertyName = "IsNavigationTargetExternal";
           //    typeof(bool), typeof(NavigationButton), new PropertyMetadata(new PropertyChangedCallback(NavigationUriChanged)));

        /// <summary>
        /// Gets or sets whether to navigate to external website
        /// </summary>
        public bool IsNavigationTargetExternal
        {
            get { return (bool)this.GetValue(IsNavigationTargetExternalProperty); }
            set { this.SetValue(IsNavigationTargetExternalProperty, value); }
        }

        
        /// <summary>
        /// Identifies the <see cref="NavigationTarget"/> dependency property. 
        /// </summary>
        public static readonly DependencyProperty NavigationTargetProperty = DependencyProperty.Register("NavigationTarget",
            typeof(Frame), typeof(NavigationButton), new PropertyMetadata(new PropertyChangedCallback(OnNavigationTargetChanged)));

        /// <summary>
        /// Gets or sets the target frame to navigate to.
        /// </summary>
        public Frame NavigationTarget
        {
            get { return (Frame)this.GetValue(NavigationTargetProperty); }
            set
            {
                var targetFrame = value as Frame;
                if (targetFrame != null)
                    this.SetValue(NavigationTargetProperty, value);
            }
        }

        private static void OnNavigationTargetChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var targetFrame = e.NewValue as Frame;
            if (targetFrame != null)
                DebugManager.Log("NavigationFramework.NavigationTarget changed: " + targetFrame.Name);
        }

        private static void OnPropertyChanged(PropertyChangedEventArgs ea)
        {
            //base.OnPropertyChanged(ea.PropertyName);

            //switch (ea.PropertyName)
            //{
            //}
        }

        #endregion  

        protected override void OnClick()
        {
            if (this.NavigationUri == null)
            {
                DebugManager.LogWarning("NavigationFramework.Cannot navigate to null NavigationUri! ");
            }
            else
            {
                if (this.NavigationTarget != null)
                {
                    DebugManager.Log("NavigationFramework.Navigating to frame: " + this.NavigationUri);

                    this.NavigationTarget.Navigate(this.NavigationUri);
                }
                else if (this.IsNavigationTargetExternal)
                {
                    var website = this.NavigationUri.ToString().ToLower();
                    if (!website.StartsWith("http://")) website = "http://" + website;

                    DebugManager.Log("NavigationFramework.Navigating to website: " + website);
#if SILVERLIGHT
                    HtmlPage.Window.Navigate(new Uri(website), "_blank");
#else // WPF
                    Process.Start(website);
#endif

                }
            }
            
            base.OnClick();
        }

    }
}