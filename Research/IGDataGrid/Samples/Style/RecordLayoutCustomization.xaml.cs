using System.Windows;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models.Common;

namespace IGDataGrid.Samples.Style
{
    /// <summary>
    /// Interaction logic for RecordLayoutCustomization.xaml
    /// </summary>
    public partial class RecordLayoutCustomization : SampleContainer
    {
        public RecordLayoutCustomization()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(RecordLayoutCustomization_Loaded);
        }

        void RecordLayoutCustomization_Loaded(object sender, RoutedEventArgs e)
        {
            this.XamDataGrid1.DataSource = NWindDataSource.GetTable(NWindTable.Customers, true).DefaultView;
        }
    }
}
