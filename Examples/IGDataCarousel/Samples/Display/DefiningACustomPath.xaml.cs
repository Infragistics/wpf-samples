using System.Windows;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models.DataPresenter;

namespace IGDataCarousel.Samples.Display
{
    /// <summary>
    /// Interaction logic for DefiningACustomPath.xaml
    /// </summary>

    public partial class DefiningACustomPath : SampleContainer
    {
        public DefiningACustomPath()
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