using System.ComponentModel;
using System.Windows;
using Infragistics.Olap.Adomd;
using Infragistics.Samples.Framework;

namespace IGPivotGrid.Samples.Data
{
    public partial class DataSourceAdomdNet : SampleContainer
    {
        public DataSourceAdomdNet()
        {
            InitializeComponent();
            AdomdDataSource dataSource = this.Resources["AdomdDataSource"] as AdomdDataSource;
            if (dataSource != null)
            {
                dataSource.LoadDimensionsCompleted += DataSource_LoadDimensionsCompleted;
                dataSource.PropertyChanged += DataSource_PropertyChanged;
            }
        }

        private void DataSource_LoadDimensionsCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (this.loaderMessage.Visibility == Visibility.Visible)
                this.loaderMessage.Visibility = Visibility.Collapsed;
        }

        private void DataSource_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Processing" && loaderMessage.Visibility != Visibility.Visible)
            {
                isBusyIndicator.Visibility = pivotGrid.DataSource.IsBusy ? Visibility.Visible : Visibility.Collapsed;
            }
        }
    }
}