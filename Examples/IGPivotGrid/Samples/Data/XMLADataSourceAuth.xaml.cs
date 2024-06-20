using System.ComponentModel;
using System.Windows;
using Infragistics.Samples.Framework;

namespace IGPivotGrid.Samples.Data
{
    public partial class XMLADataSourceAuth : SampleContainer
    {
        public XMLADataSourceAuth()
        {
            InitializeComponent();
            this.pivotGrid.DataSource.LoadDimensionsCompleted += DataSource_LoadDimensionsCompleted;
        }

        void DataSource_LoadDimensionsCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (this.loaderMessage.Visibility == Visibility.Visible)
            {
                this.pivotGrid.DataSource.PropertyChanged += DataSource_PropertyChanged;
                this.loaderMessage.Visibility = Visibility.Collapsed;
            }
        }

        void DataSource_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Processing")
            {
                isBusyIndicator.Visibility = pivotGrid.DataSource.IsBusy ? Visibility.Visible : Visibility.Collapsed;
            }
        }
    }
}
