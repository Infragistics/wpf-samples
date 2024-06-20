using System.ComponentModel;
using System.Windows;
using Infragistics.Olap;
using Infragistics.Olap.Xmla;
using Infragistics.Samples.Framework;

namespace IGPivotGrid.Samples.Performance
{
    public partial class HandlingLargeHierarchies : SampleContainer
    {
        public HandlingLargeHierarchies()
        {
            InitializeComponent();
            this.pivotGrid.DataSource.LoadDimensionsCompleted += DataSource_LoadDimensionsCompleted;
            ((XmlaDataSource)this.pivotGrid.DataSource).MetadataTreeItemAdding += HandlingLargeHierarchies_MetadataTreeItemAdding;
        }

        private void HandlingLargeHierarchies_MetadataTreeItemAdding(object sender, MetadataTreeEventArgs e)
        {
            // Expand all items in the metadata tree
            e.Item.ExpandWhenInitialized = true;
        }

        private void DataSource_LoadDimensionsCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (this.loaderMessage.Visibility == Visibility.Visible)
            {
                this.pivotGrid.DataSource.PropertyChanged += DataSource_PropertyChanged;
                this.loaderMessage.Visibility = Visibility.Collapsed;
            }
        }

        private void DataSource_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Processing")
                isBusyIndicator.Visibility = pivotGrid.DataSource.IsBusy ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
