using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models.DataPresenter;
using System.Data;
using System.Windows;

namespace IGDataPresenter.Samples.Display
{
    /// <summary>
    /// Interaction logic for PresentationFormats.xaml
    /// </summary>
    public partial class PresentationFormats : SampleContainer
    {
        public PresentationFormats()
        {
            InitializeComponent();
        }

        private void xdp_Loaded(object sender, RoutedEventArgs e)
        {
            // create and set the data source, which will be used for all xamDataPresenter views
            DataView dv = new EmployeeData().EmployeeDataView;
            this.xdp.DataSource = dv;
        }
    }
}
