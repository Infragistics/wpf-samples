using System.Windows;
using IGFunnelChart.Model;
using Infragistics.Controls.Charts;
using Infragistics.Samples.Framework;

namespace IGFunnelChart.Samples.Display
{
    public partial class UseBezierCurve : SampleContainer
    {
        private TestDataItem _item = null;

        public UseBezierCurve()
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

        private void SliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.funnel == null) return;

            if (sender == slider1X && slider1Y != null)
            {
                this.funnel.UpperBezierControlPoint = new Point(e.NewValue, slider1Y.Value);
                return;
            }

            if (sender == slider1Y && slider1X != null)
            {
                this.funnel.UpperBezierControlPoint = new Point(slider1X.Value, e.NewValue);
                return;
            }

            if (sender == slider2X && slider2Y != null)
            {
                this.funnel.LowerBezierControlPoint = new Point(e.NewValue, slider2Y.Value);
                return;
            }

            if (sender == slider2Y && slider2X != null)
            {
                this.funnel.LowerBezierControlPoint = new Point(slider2X.Value, e.NewValue);
                return;
            }
        }
    }
}
