using System.Windows;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models.DataPresenter;

namespace IGDataCarousel.Samples.Display
{
    public partial class ContinuousListBehavior : SampleContainer
    {
        public ContinuousListBehavior()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            EmployeeData emp = new EmployeeData();
            this.XamDataCarousel1.DataSource = emp.EmployeeDataView;
        }
    }
}