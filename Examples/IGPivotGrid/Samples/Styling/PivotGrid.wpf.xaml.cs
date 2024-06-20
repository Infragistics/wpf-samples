using System.Windows;
using Infragistics.Samples.Framework;

namespace IGPivotGrid.Samples.Styling
{
    public partial class PivotGrid : SampleContainer
    {
        public PivotGrid()
        {
            InitializeComponent();

            this.pivotGrid.DataSource.PropertyChanged += (s, e) =>
                {
                    if (e.PropertyName == "Processing")
                        isBusyIndicator.Visibility = pivotGrid.DataSource.IsBusy ? Visibility.Visible : Visibility.Collapsed;
                };
        }
    }
}
