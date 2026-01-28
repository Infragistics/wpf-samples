using System.Windows;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models.DataPresenter;

namespace IGDataCarousel.Samples.Display
{
    /// <summary>
    /// Interaction logic for UnderstandingItemPathPadding.xaml
    /// </summary>

    public partial class UnderstandingItemPathPadding : SampleContainer
    {
        public UnderstandingItemPathPadding()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            EmployeeData emp = new EmployeeData();
            this.XamDataCarousel1.DataSource = emp.EmployeeDataView;

            this.sliderLeft.Value = this.XamDataCarousel1.ViewSettings.ItemPathPadding.Left;
            this.sliderRight.Value = this.XamDataCarousel1.ViewSettings.ItemPathPadding.Right;
            this.sliderTop.Value = this.XamDataCarousel1.ViewSettings.ItemPathPadding.Top;
            this.sliderBottom.Value = this.XamDataCarousel1.ViewSettings.ItemPathPadding.Bottom;
        }

        /// <param name="sender">Sender</param>
        /// <param name="e">E</param>
        void OnItemPathPaddingChanged(object sender, RoutedEventArgs e)
        {
            this.XamDataCarousel1.ViewSettings.ItemPathPadding = new Thickness(this.sliderLeft.Value, this.sliderTop.Value, this.sliderRight.Value, this.sliderBottom.Value);
        }
    }
}