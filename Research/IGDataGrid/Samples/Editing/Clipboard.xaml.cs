using System.Data;
using System.Windows;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models.Common;

namespace IGDataGrid.Samples.Editing
{
    /// <summary>
    /// Interaction logic for Clipboard.xaml
    /// </summary>
    public partial class Clipboard : SampleContainer
    {
        public Clipboard()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(Clipboard_Loaded);
        }

        void Clipboard_Loaded(object sender, RoutedEventArgs e)
        {
            DataTable dt = NWindDataSource.GetTable(NWindTable.Orders);
            this.XamDataGrid1.DataSource = dt.DefaultView;
        }
    }
}
