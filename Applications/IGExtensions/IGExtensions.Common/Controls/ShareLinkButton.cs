using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using IGExtensions.Framework.Controls;      // FrameworkElementEx.ApplyStyle()

namespace IGExtensions.Common.Controls
{
    /// <summary>
    /// Represents the ShareLinkButton
    /// </summary>
    public class ShareLinkButton: ContentControl
    {
        /// <summary>
        /// Constructs an instance of the ShareLinkButton
        /// </summary>
        public ShareLinkButton()
        {
            // add the generic style to the control's resources
            const string stylePath = "/IGExtensions.Common;component/Controls/ShareLinkButton.xaml";
            this.ApplyStyle(stylePath);

             
        }
        public event EventHandler HyperlinkLinkUpdateRequested;
        private TextBlock _hyperlinkTextBlock;

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            VisualStateManager.GoToState(this, "MouseOver", false);
            base.OnMouseEnter(e);
        }
        protected override void OnMouseLeave(MouseEventArgs e)
        {
            VisualStateManager.GoToState(this, "MouseNotOver", false);
            base.OnMouseLeave(e);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            var rootElement = this.GetTemplateChild("RootElement") as Grid;
            if (rootElement != null)
            {
                rootElement.MouseLeftButtonDown += OnMouseLeftButtonUp;
                rootElement.MouseLeftButtonUp += OnMouseLeftButtonDown;

                _hyperlinkTextBlock = rootElement.FindName("HyperlinkTextBlock") as TextBlock;
                 
            }
        }

        void OnMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            
            //HtmlPage.Window.Navigate(this.HyperlinkUri, this.HyperlinkTarget);
        }

        void OnMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (HyperlinkLinkUpdateRequested != null)
            {
                HyperlinkLinkUpdateRequested(this, e);
            }
        }

        #region Properties

        public const string HoverBrushPropertyName = "HoverBrush";

        public static DependencyProperty HoverBrushProperty = DependencyProperty.Register(
            HoverBrushPropertyName, typeof(Brush), typeof(ShareLinkButton), null);

        /// <summary>
        /// Gets or sets 
        /// </summary>
        public Brush HoverBrush
        {
            get { return (Brush)GetValue(HoverBrushProperty); }
            set { SetValue(HoverBrushProperty, value); }
        }

        public const string HyperlinkUriPropertyName = "HyperlinkUri";

        public static DependencyProperty HyperlinkUriProperty = DependencyProperty.Register(
            HyperlinkUriPropertyName, typeof(Uri), typeof(ShareLinkButton), null);

        /// <summary>
        /// Gets or sets 
        /// </summary>
        public Uri HyperlinkUri
        {
            get { return (Uri)GetValue(HyperlinkUriProperty); }
            set { SetValue(HyperlinkUriProperty, value); }
        }

        public const string HyperlinkTargetPropertyName = "HyperlinkTarget";

        public static DependencyProperty HyperlinkTargetProperty = DependencyProperty.Register(
            HyperlinkTargetPropertyName, typeof(string), typeof(ShareLinkButton), null);

        /// <summary>
        /// Gets or sets 
        /// </summary>
        public string HyperlinkTarget
        {
            get { return (string)GetValue(HyperlinkTargetProperty); }
            set { SetValue(HyperlinkTargetProperty, value); }
        }


        public const string HyperlinkTextPropertyName = "HyperlinkText";

        public static DependencyProperty HyperlinkTextProperty = DependencyProperty.Register(
            HyperlinkTextPropertyName, typeof(string), typeof(ShareLinkButton), new PropertyMetadata("Link", (sender, e) =>
            {
                (sender as ShareLinkButton).OnPropertyChanged(new PropertyChangedEventArgs(HyperlinkTextPropertyName));
            }));

        /// <summary>
        /// Gets or sets 
        /// </summary>
        public string HyperlinkText
        {
            get { return (string)GetValue(HyperlinkTextProperty); }
            set { SetValue(HyperlinkTextProperty, value); }
        }


        public void OnPropertyChanged(PropertyChangedEventArgs ea)
        {
            switch (ea.PropertyName)
            {
                case HyperlinkTextPropertyName:
                    {
                        //if (_hyperlinkTextBlock != null)
                        //    _hyperlinkTextBlock.Visibility = string.IsNullOrEmpty(this.HyperlinkText) ? Visibility.Collapsed : Visibility.Visible;

                        break;
                    }
            }
        } 
        #endregion
    }
}