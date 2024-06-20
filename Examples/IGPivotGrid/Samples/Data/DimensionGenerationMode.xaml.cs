using IGPivotGrid.Samples.Controls;
using Infragistics.Controls.Grids;
using Infragistics.Olap.FlatData;
using Infragistics.Samples.Framework;

namespace IGPivotGrid.Samples.Data
{
    public partial class DimensionGenerationMode : SampleContainer
    {
        private SalesCollection data;

        public DimensionGenerationMode()
        {
            InitializeComponent();

            data = new SalesCollection();
            this.dataSelectorProperty.Loaded += DataSelector_Loaded;
            this.dataSelectorMetadata.Loaded += DataSelector_Loaded;
            this.dataSelectorMixed.Loaded += DataSelector_Loaded;
        }

        private void DataSelector_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var dataSelector = sender as XamPivotDataSelector;
            dataSelector.Loaded -= DataSelector_Loaded;
            (dataSelector.DataSource as FlatDataSource).ItemsSource = new SalesCollection();
        }
    }
}
