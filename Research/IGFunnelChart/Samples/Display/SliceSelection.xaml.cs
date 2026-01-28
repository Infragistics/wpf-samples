using IGFunnelChart.Model;
using Infragistics.Controls.Charts;
using Infragistics.Samples.Framework;

namespace IGFunnelChart.Samples.Display
{
    public partial class SliceSelection : SampleContainer
    {
        private TestDataItem _item = null;

        public SliceSelection()
        {
            InitializeComponent();
        }

        private void TheChartSliceClicked(object sender, FunnelSliceClickedEventArgs args)
        {
            var testData = Resources["data"] as TestData;

            if (testData != null) return;

            if (_item == null)
            {
                _item = testData[1];
                testData.RemoveAt(1);
            }
            else
            {
                testData.Insert(1, _item);
                _item = null;
            }
        }
    }
}
