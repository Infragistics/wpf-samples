using System.ComponentModel;
using System.Windows;
using Infragistics.Samples.Framework;

namespace IGPivotGrid.Samples.Data
{
    public partial class DataSlicer : SampleContainer
    {
        public DataSlicer()
        {
            InitializeComponent();
            this.pivotGrid.DataSource.LoadDimensionsCompleted += (s, e) =>
                {
                    if (this.loaderMessage.Visibility == Visibility.Visible)
                    {
                        this.pivotGrid.DataSource.PropertyChanged += DataSource_PropertyChanged;
                        this.loaderMessage.Visibility = Visibility.Collapsed;
                    }
                };
        }

        private void DataSource_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Processing")
            {
                isBusyIndicator.Visibility = pivotGrid.DataSource.IsBusy ? Visibility.Visible : Visibility.Collapsed;
            }
        }
    }
}
