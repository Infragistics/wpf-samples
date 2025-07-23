using System.Windows;
using Infragistics.Samples.Framework;

namespace IGPivotGrid.Samples.Display
{
    public partial class PivotDataCommand : SampleContainer
    {
        public PivotDataCommand()
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