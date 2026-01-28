using System.Windows;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models.DataPresenter;

namespace IGDataCarousel.Samples.Navigation
{
    /// <summary>
    /// Interaction logic for BreadcrumbNavigation.xaml
    /// </summary>

    public partial class BreadcrumbNavigation : SampleContainer
    {
        public BreadcrumbNavigation()
        {
            InitializeComponent();
        }

        void Page_Loaded(object sender, RoutedEventArgs e)
        {
            EmployeeData emp = new EmployeeData();
            this.XamDataCarousel1.DataSource = emp.EmployeeDataView;
        }
    }
}