using System.ComponentModel;
using System.Windows;
using Infragistics.Samples.Framework;

namespace IGPivotGrid.Samples.Organization
{
    public partial class AdvancedFiltering : SampleContainer
    {
        public AdvancedFiltering()
        {
            InitializeComponent();

            this.pivotGrid.DataSource.PropertyChanged += DataSource_PropertyChanged;
        }

        private void DataSource_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Processing")
                isBusyIndicator.Visibility = pivotGrid.DataSource.IsBusy ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
