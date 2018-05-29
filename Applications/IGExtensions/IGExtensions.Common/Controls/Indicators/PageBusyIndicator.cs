using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using IGExtensions.Framework.Controls;      // FrameworkElementEx.ApplyStyle()

namespace IGExtensions.Common.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public class PageBusyIndicator : Control
    {
        /// <summary>
        /// 
        /// </summary>
        public PageBusyIndicator()
        {
            // add the generic style to the control's resources
            //const string stylePath = "/IGExtensions.Common;component/Controls/Indicators/PageBusyIndicator.xaml";
            //this.ApplyStyle(stylePath);
            this.DefaultStyleKey = typeof(PageBusyIndicator);
         
//#if WPF
//            popup.PlacementTarget = this;
//            popup.Placement = System.Windows.Controls.Primitives.PlacementMode.Relative;
//            popup.AllowsTransparency = true;
//#endif
        }

        private Popup _indicatorPopup;
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();


            var rootElement = this.GetTemplateChild("IndicatorPopup") as Popup;
          //var rootElement = this.GetTemplateChild("IndicatorRoot") as Grid;
            if (rootElement != null)
            {
                _indicatorPopup = rootElement;
                _indicatorPopup.IsOpen = true;
                //_stackPanel = rootElement.FindName("stackPanel") as StackPanel;
            }
        }

        public const string VisibilityPropertyName = "Visibility";
        private Visibility _visibility;// = Visibility.Collapsed;
        /// <summary>
        /// Gets or sets Visibility of the  PageBusyIndicator
        /// </summary>
        public new Visibility Visibility
        {
            get { return _visibility; }
            set
            {
                if (value != _visibility)
                {
                    base.Visibility = value;
                    //if (value == Visibility.Visible)
                    //{
                    //    if (_indicatorPopup != null) _indicatorPopup.IsOpen = true;
                    // //   this.popup.IsOpen = true;
                    // //   this.busyImage.Width = this.IndicatorSize;
                    // //   this.busyImage.Height = this.IndicatorSize;
                    //}
                    //else
                    //{
                    //    if (_indicatorPopup != null) _indicatorPopup.IsOpen = false;
                   
                    //}
               //     else this.popup.IsOpen = false;
                    _visibility = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(VisibilityPropertyName));
                }
            }
        }
         
        public const string IndicatorIsBusyPropertyName = "IndicatorIsBusy";
        public static DependencyProperty IndicatorIsBusyProperty = DependencyProperty.Register(
            IndicatorIsBusyPropertyName, typeof(bool), typeof(PageBusyIndicator), new PropertyMetadata(false, (sender, e) =>
            {
                (sender as PageBusyIndicator).OnPropertyChanged(new PropertyChangedEventArgs(IndicatorIsBusyPropertyName));
            }));
        /// <summary>
        /// Gets or sets the state whether the PageBusyIndicator is busy
        /// </summary>
        public bool IndicatorIsBusy
        {
            get { return (bool)GetValue(IndicatorIsBusyProperty); }
            set { SetValue(IndicatorIsBusyProperty, value); }
        }


        public const string IndicatorOverlayBrushPropertyName = "IndicatorOverlayBrush";
        public static DependencyProperty IndicatorOverlayBrushProperty = DependencyProperty.Register(
            IndicatorOverlayBrushPropertyName, typeof(Brush), typeof(PageBusyIndicator), new PropertyMetadata(new SolidColorBrush(Colors.White), (sender, e) =>
            {
                (sender as PageBusyIndicator).OnPropertyChanged(new PropertyChangedEventArgs(IndicatorOverlayBrushPropertyName));
            }));
        /// <summary>
        /// Gets or sets Overlay Brush of the PageBusyIndicator
        /// </summary>
        public Brush IndicatorOverlayBrush
        {
            get { return (Brush)GetValue(IndicatorOverlayBrushProperty); }
            set { SetValue(IndicatorOverlayBrushProperty, value); }
        }


        public const string IndicatorOverlayOpacityPropertyName = "IndicatorOverlayOpacity";
        public static DependencyProperty IndicatorOverlayOpacityProperty = DependencyProperty.Register(
            IndicatorOverlayOpacityPropertyName, typeof(double), typeof(PageBusyIndicator), new PropertyMetadata(0.7, (sender, e) =>
            {
                (sender as PageBusyIndicator).OnPropertyChanged(new PropertyChangedEventArgs(IndicatorOverlayOpacityPropertyName));
            }));
        /// <summary>
        /// Gets or sets Overlay Brush of the PageBusyIndicator
        /// </summary>
        public double IndicatorOverlayOpacity
        {
            get { return (double)GetValue(IndicatorOverlayOpacityProperty); }
            set { SetValue(IndicatorOverlayOpacityProperty, value); }
        }

        public const string IndicatorSizePropertyName = "IndicatorSize";
        //public static DependencyProperty IndicatorSizeProperty = DependencyProperty.Register(
        //    IndicatorSizePropertyName, typeof(double), typeof(PageBusyIndicator), new PropertyMetadata(50.0, OnIndicatorSizeChanged));

          public static DependencyProperty IndicatorSizeProperty = DependencyProperty.Register(
            IndicatorSizePropertyName, typeof(double), typeof(PageBusyIndicator), new PropertyMetadata(20.0, (sender, e) =>
            {
                (sender as PageBusyIndicator).OnPropertyChanged(new PropertyChangedEventArgs(IndicatorSizePropertyName));
            }));


        private void OnPropertyChanged(PropertyChangedEventArgs ea)
        {
            //base.OnPropertyChanged(ea.PropertyName);

            switch (ea.PropertyName)
            {
                case IndicatorSizePropertyName:
                    {
                      //  this.IndicatorImage.Width = this.IndicatorSize;
                      //  this.IndicatorImage.Height = this.IndicatorSize;  
                        break;
                    }

                case IndicatorIsBusyPropertyName:
                    {
                        this.Visibility = this.IndicatorIsBusy ? Visibility.Visible : Visibility.Collapsed;

                        //  this.IndicatorImage.Width = this.IndicatorSize;
                  //  this.IndicatorImage.Height = this.IndicatorSize;  
                    break;
                    }


                    //case IndicatorOverlayBrushPropertyName:
                //{
                //    this.IndicatorOverlay.Background = this.IndicatorOverlayBrush;
                //    break;
                //}
                //case IndicatorOverlayOpacityPropertyName:
                //{
                //    this.IndicatorOverlay.Opacity = this.IndicatorOverlayOpacity;
                //    break;
                //}

              
            }

            //if (this.Visibility == Visibility.Visible)
            //{
            //    if (_indicatorPopup != null) _indicatorPopup.IsOpen = true;
            //    //   this.popup.IsOpen = true;
            //    //   this.busyImage.Width = this.IndicatorSize;
            //    //   this.busyImage.Height = this.IndicatorSize;
            //}
            //else
            //{
            //    if (_indicatorPopup != null) _indicatorPopup.IsOpen = false;

            //}
        }
       
        /// <summary>
        /// Gets or sets size of the PageBusyIndicator
        /// </summary>
        public double IndicatorSize
        {
            get { return (double)GetValue(IndicatorSizeProperty); }
            set { SetValue(IndicatorSizeProperty, value); }
        }

 
    }
}
