using System.ComponentModel;
using Infragistics.Samples.Framework;

namespace IGPivotGrid.Samples.Styling
{
    public partial class PivotGrid : SampleContainer
    {
        public PivotGrid()
        {
            InitializeComponent();

            this.pivotGrid.DataSource.PropertyChanged += DataSource_PropertyChanged;
        }
        void DataSource_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Processing")
            {
                if (this.pivotGrid.DataSource.Processing)
                {
                    isBusyIndicator.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    isBusyIndicator.Visibility = System.Windows.Visibility.Collapsed;
                }
            }
        }
    }
}
