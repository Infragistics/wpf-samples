using System.Data;
using System.Windows;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models.Common;

namespace IGDataGrid.Samples.Editing
{
    /// <summary>
    /// Interaction logic for UndoSimple.xaml
    /// </summary>
    public partial class UndoSimple : SampleContainer
    {
        public UndoSimple()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(UndoSimple_Loaded);
        }

        void UndoSimple_Loaded(object sender, RoutedEventArgs e)
        {
            DataTable table = NWindDataSource.GetTable(NWindTable.Orders, true);
            this.XamDataGrid1.DataSource = table.DefaultView;
        }
    }
}
