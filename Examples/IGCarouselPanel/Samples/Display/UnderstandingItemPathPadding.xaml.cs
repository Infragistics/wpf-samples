using System;
using System.Windows;

namespace IGCarouselPanel.Samples.Display
{
    /// <summary>
    /// Interaction logic for UnderstandingItemPathPadding.xaml
    /// </summary>

    public partial class UnderstandingItemPathPadding : Infragistics.Samples.Framework.SampleContainer
    {
        public UnderstandingItemPathPadding()
        {
            InitializeComponent();
        }

        void UnderstandingItemPathPadding_Loaded(object sender, RoutedEventArgs e)
        {
            this.sliderLeft.Value = this.XamCarouselPanel1.ViewSettings.ItemPathPadding.Left;
            this.sliderRight.Value = this.XamCarouselPanel1.ViewSettings.ItemPathPadding.Right;
            this.sliderTop.Value = this.XamCarouselPanel1.ViewSettings.ItemPathPadding.Top;
            this.sliderBottom.Value = this.XamCarouselPanel1.ViewSettings.ItemPathPadding.Bottom;
        }

        void OnItemPathPaddingChanged(object sender, RoutedEventArgs e)
        {
            this.XamCarouselPanel1.ViewSettings.ItemPathPadding = new Thickness(this.sliderLeft.Value, this.sliderTop.Value, this.sliderRight.Value, this.sliderBottom.Value);
        }
    }
}