using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using IGDataChart.Resources;
using Infragistics;
using Infragistics.Controls.Charts;

namespace IGDataChart.Samples.Navigation
{
    public partial class ChartEvents : Infragistics.Samples.Framework.SampleContainer
    {
        public ChartEvents()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ClearOutput();
        }

        #region Methods
        //================================================================================
        private bool IsEnabledXAxisEvents()
        {
            return this.IsChecked(chbXAxisEvents);
        }
        private bool IsEnabledYAxisEvents()
        {
            return this.IsChecked(chbYAxisEvents);
        }
        private bool IsEnabledLegendEvents()
        {
            return this.IsChecked(chbLegendEvents);
        }
        private bool IsEnabledMarkerEvents()
        {
            return this.IsChecked(chbMarkerEvents);
        }
        private bool IsEnabledSeriesEvents()
        {
            return this.IsChecked(chbSeriesEvents);
        }
        private bool IsEnabledDataChartEvents()
        {
            return this.IsChecked(chbDataChartEvents);
        }
        private bool IsEnabledLabelEvents()
        {
            return this.IsChecked(chbLabelEvents);
        }

        private bool IsChecked(CheckBox checkBox)
        {
            return checkBox == null ?
                false :
                !checkBox.IsChecked.HasValue || checkBox.IsChecked.Value;
        }
        private void DisplayOutput(string content)
        {
            if (this.evtOutput != null)
            {
                this.evtOutput.Text += content + Environment.NewLine;
                this.svOutput.UpdateLayout();
                this.svOutput.ScrollToVerticalOffset(double.MaxValue);
            }
        }
        private void ClearOutput()
        {
            if (this.evtOutput != null)
            {
                this.evtOutput.Text = string.Empty;
                this.svOutput.UpdateLayout();
                this.svOutput.ScrollToVerticalOffset(double.MaxValue);
            }
        }
        private string FormatRect(Rect rc)
        {
            string str = string.Empty;
            str += String.Format("{0:0.00}, ", rc.Left);
            str += String.Format("{0:0.00}, ", rc.Top);
            str += String.Format("{0:0.00}, ", rc.Width);
            str += String.Format("{0:0.00}", rc.Height);
            return str;
        }
        private string FormatPoint(Point pn)
        {
            string str = string.Empty;
            str += String.Format("{0:0.00}, ", pn.X);
            str += String.Format("{0:0.00}", pn.Y);
            return str;
        }
        private string FormatSize(Size sz)
        {
            string str = string.Empty;
            str += String.Format("{0:0.00}, ", sz.Width);
            str += String.Format("{0:0.00}", sz.Height);
            return str;
        }
        private string FormatRange(double min, double max)
        {
            string str = string.Empty;
            str += String.Format("{0:0.00}, ", min);
            str += String.Format("{0:0.00}", max);
            return str;
        }
        //================================================================================
        #endregion
        #region Event Handlers: DataChart
        //================================================================================
        private void DataChart_Loaded(object sender, RoutedEventArgs e)
        {
            if (!IsEnabledDataChartEvents()) return;
            DisplayOutput(DataChartStrings.XWDC_ChartEvents_DataChart + " " + DataChartStrings.XWDC_ChartEvents_Loaded);
        }
        private void DataChart_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!IsEnabledDataChartEvents()) return;
            string location = FormatPoint(e.GetPosition(this.DataChart));
            DisplayOutput(DataChartStrings.XWDC_ChartEvents_DataChart + " " + DataChartStrings.XWDC_ChartEvents_MouseEnter + location);
        }

        private void DataChart_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!IsEnabledDataChartEvents()) return;
            string location = FormatPoint(e.GetPosition(this.DataChart));
            DisplayOutput(DataChartStrings.XWDC_ChartEvents_DataChart + " " + DataChartStrings.XWDC_ChartEvents_MouseLeave + location);
        }

        private void DataChart_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!IsEnabledDataChartEvents()) return;
            string location = FormatPoint(e.GetPosition(this.DataChart));
            DisplayOutput(DataChartStrings.XWDC_ChartEvents_DataChart + " " + DataChartStrings.XWDC_ChartEvents_MouseLeftButtonDown + location);
        }

        private void DataChart_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!IsEnabledDataChartEvents()) return;
            string location = FormatPoint(e.GetPosition(this.DataChart));
            DisplayOutput(DataChartStrings.XWDC_ChartEvents_DataChart + " " + DataChartStrings.XWDC_ChartEvents_MouseLeftButtonUp + location);
        }
        private void DataChart_WindowRectChanged(object sender, RectChangedEventArgs e)
        {
            if (!IsEnabledDataChartEvents()) return;
            DisplayOutput(DataChartStrings.XWDC_ChartEvents_DataChart + " " + DataChartStrings.XWDC_ChartEvents_WindowRectChanged + FormatRect(e.NewRect));
        }

        private void DataChart_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!IsEnabledDataChartEvents()) return;
            var location = FormatPoint(e.GetPosition(this.DataChart));
            DisplayOutput(DataChartStrings.XWDC_ChartEvents_DataChart + " " + DataChartStrings.XWDC_ChartEvents_MouseRightButtonDown + location);
        }

        private void DataChart_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!IsEnabledDataChartEvents()) return;
            var location = FormatPoint(e.GetPosition(this.DataChart));
            DisplayOutput(DataChartStrings.XWDC_ChartEvents_DataChart + " " + DataChartStrings.XWDC_ChartEvents_MouseRightButtonUp + location);
        }
        //================================================================================
        #endregion
        #region Event Handlers: Series
        //================================================================================
        private void Series_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!IsEnabledSeriesEvents()) return;
            var location = FormatPoint(e.GetPosition(this.DataChart));
            DisplayOutput(DataChartStrings.XWDC_ChartEvents_Series + " " + DataChartStrings.XWDC_ChartEvents_MouseEnter + location);
        }
        private void Series_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!IsEnabledSeriesEvents()) return;
            var location = FormatPoint(e.GetPosition(this.DataChart));
            DisplayOutput(DataChartStrings.XWDC_ChartEvents_Series + " " + DataChartStrings.XWDC_ChartEvents_MouseLeave + location);
        }
        private void Series_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!IsEnabledSeriesEvents()) return;
            var location = FormatPoint(e.GetPosition(this.DataChart));
            DisplayOutput(DataChartStrings.XWDC_ChartEvents_Series + " " + DataChartStrings.XWDC_ChartEvents_MouseLeftButtonDown + location);
        }
        private void Series_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!IsEnabledSeriesEvents()) return;
            var location = FormatPoint(e.GetPosition(this.DataChart));
            DisplayOutput(DataChartStrings.XWDC_ChartEvents_Series + " " + DataChartStrings.XWDC_ChartEvents_MouseLeftButtonUp + location);
        }
        private void Series_Loaded(object sender, RoutedEventArgs e)
        {
            if (!IsEnabledSeriesEvents()) return;
            DisplayOutput(DataChartStrings.XWDC_ChartEvents_Series + " " + DataChartStrings.XWDC_ChartEvents_Loaded);
        }

        private void Series_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!IsEnabledSeriesEvents()) return;
            var location = FormatPoint(e.GetPosition(this.DataChart));
            DisplayOutput(DataChartStrings.XWDC_ChartEvents_Series + " " + DataChartStrings.XWDC_ChartEvents_MouseRightButtonDown + location);
        }

        private void Series_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!IsEnabledSeriesEvents()) return;
            var location = FormatPoint(e.GetPosition(this.DataChart));
            DisplayOutput(DataChartStrings.XWDC_ChartEvents_Series + " " + DataChartStrings.XWDC_ChartEvents_MouseRightButtonUp + location);
        }
        //================================================================================
        #endregion
        #region Event Handlers: Legend
        //================================================================================
        private void Legend_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!IsEnabledLegendEvents()) return;
            string location = FormatPoint(e.GetPosition(this.DataChart));
            DisplayOutput(DataChartStrings.XWDC_ChartEvents_Legend + " " + DataChartStrings.XWDC_ChartEvents_MouseEnter + location);
        }
        private void Legend_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!IsEnabledLegendEvents()) return;
            string location = FormatPoint(e.GetPosition(this.DataChart));
            DisplayOutput(DataChartStrings.XWDC_ChartEvents_Legend + " " + DataChartStrings.XWDC_ChartEvents_MouseLeave + location);
        }
        private void Legend_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!IsEnabledLegendEvents()) return;
            string location = FormatPoint(e.GetPosition(this.DataChart));
            DisplayOutput(DataChartStrings.XWDC_ChartEvents_Legend + " " + DataChartStrings.XWDC_ChartEvents_MouseLeftButtonDown + location);
        }
        private void Legend_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!IsEnabledLegendEvents()) return;
            string location = FormatPoint(e.GetPosition(this.DataChart));
            DisplayOutput(DataChartStrings.XWDC_ChartEvents_Legend + " " + DataChartStrings.XWDC_ChartEvents_MouseLeftButtonUp + location);
        }
        //================================================================================
        #endregion
        #region Event Handlers: XAxisLabel
        //================================================================================
        private void XAxisLabel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!IsEnabledLabelEvents()) return;
            string location = FormatPoint(e.GetPosition(this.DataChart));
            DisplayOutput(DataChartStrings.XWDC_ChartEvents_XAxisLabels + " " + DataChartStrings.XWDC_ChartEvents_MouseLeftButtonDown + location);
        }
        private void XAxisLabel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!IsEnabledLabelEvents()) return;
            string location = FormatPoint(e.GetPosition(this.DataChart));
            DisplayOutput(DataChartStrings.XWDC_ChartEvents_XAxisLabels + " " + DataChartStrings.XWDC_ChartEvents_MouseLeftButtonUp + location);
        }

        private void XAxisLabel_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!IsEnabledLabelEvents()) return;
            string location = FormatPoint(e.GetPosition(this.DataChart));
            DisplayOutput(DataChartStrings.XWDC_ChartEvents_XAxisLabels + " " + DataChartStrings.XWDC_ChartEvents_MouseEnter + location);
        }

        private void XAxisLabel_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!IsEnabledLabelEvents()) return;
            string location = FormatPoint(e.GetPosition(this.DataChart));
            DisplayOutput(DataChartStrings.XWDC_ChartEvents_XAxisLabels + " " +
                          DataChartStrings.XWDC_ChartEvents_MouseLeave + location);
        }
        //================================================================================
        #endregion
        #region Event Handlers: YAxisLabel
        //================================================================================
        private void YAxisLabel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!IsEnabledLabelEvents()) return;
            string location = FormatPoint(e.GetPosition(this.DataChart));
            DisplayOutput(DataChartStrings.XWDC_ChartEvents_YAxisLabels + " " + DataChartStrings.XWDC_ChartEvents_MouseLeftButtonDown + location);
        }
        private void YAxisLabel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!IsEnabledLabelEvents()) return;
            string location = FormatPoint(e.GetPosition(this.DataChart));
            DisplayOutput(DataChartStrings.XWDC_ChartEvents_YAxisLabels + " " + DataChartStrings.XWDC_ChartEvents_MouseLeftButtonUp + location);
        }
        private void YAxisLabel_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!IsEnabledLabelEvents()) return;
            string location = FormatPoint(e.GetPosition(this.DataChart));
            DisplayOutput(DataChartStrings.XWDC_ChartEvents_YAxisLabels + " " + DataChartStrings.XWDC_ChartEvents_MouseEnter + location);
        }
        private void YAxisLabel_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!IsEnabledLabelEvents()) return;
            string location = FormatPoint(e.GetPosition(this.DataChart));
            DisplayOutput(DataChartStrings.XWDC_ChartEvents_YAxisLabels + " " + DataChartStrings.XWDC_ChartEvents_MouseLeave + location);
        }
        //================================================================================
        #endregion
        #region Event Handlers: Marker
        //================================================================================
        private void Marker_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!IsEnabledMarkerEvents()) return;
            string location = FormatPoint(e.GetPosition(this.DataChart));
            DisplayOutput(DataChartStrings.XWDC_ChartEvents_Marker + " " +
                          DataChartStrings.XWDC_ChartEvents_MouseEnter + location);
        }

        private void Marker_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!IsEnabledMarkerEvents()) return;
            string location = FormatPoint(e.GetPosition(this.DataChart));
            DisplayOutput(DataChartStrings.XWDC_ChartEvents_Marker + " " +
                          DataChartStrings.XWDC_ChartEvents_MouseLeave + location);
        }

        private void Marker_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!IsEnabledMarkerEvents()) return;
            string location = FormatPoint(e.GetPosition(this.DataChart));
            DisplayOutput(DataChartStrings.XWDC_ChartEvents_Marker + " " +
                          DataChartStrings.XWDC_ChartEvents_MouseLeftButtonUp + location);
        }

        private void Marker_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!IsEnabledMarkerEvents()) return;
            string location = FormatPoint(e.GetPosition(this.DataChart));
            DisplayOutput(DataChartStrings.XWDC_ChartEvents_Marker + " " +
                          DataChartStrings.XWDC_ChartEvents_MouseLeftButtonDown + location);
        }
        //================================================================================
        #endregion
        #region Event Handlers: XAxis
        //================================================================================
        private void XAxis_Loaded(object sender, RoutedEventArgs e)
        {
            if (!this.IsEnabledXAxisEvents()) return;
            DisplayOutput(DataChartStrings.XWDC_ChartEvents_XAxisLabels + " " +
                          DataChartStrings.XWDC_ChartEvents_Loaded);
        }
        private void XAxis_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (!this.IsEnabledXAxisEvents()) return;
            DisplayOutput(DataChartStrings.XWDC_ChartEvents_XAxisLabels + " " +
                          DataChartStrings.XWDC_ChartEvents_SizeChanged + FormatSize(e.NewSize));
        }
        private void XAxis_RangeChanged(object sender, AxisRangeChangedEventArgs e)
        {
            if (!this.IsEnabledXAxisEvents()) return;
            DisplayOutput(DataChartStrings.XWDC_ChartEvents_XAxisLabels + " " +
                          DataChartStrings.XWDC_ChartEvents_SizeChanged + FormatRange(e.MinimumValue, e.MaximumValue));
        }
        private void XAxis_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!IsEnabledXAxisEvents()) return;
            string location = FormatPoint(e.GetPosition(this.DataChart));
            DisplayOutput(DataChartStrings.XWDC_ChartEvents_XAxis + " " +
                          DataChartStrings.XWDC_ChartEvents_MouseEnter + location);
        }

        private void XAxis_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!IsEnabledXAxisEvents()) return;
            string location = FormatPoint(e.GetPosition(this.DataChart));
            DisplayOutput(DataChartStrings.XWDC_ChartEvents_XAxis + " " +
                          DataChartStrings.XWDC_ChartEvents_MouseLeave + location);
        }

        private void XAxis_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!IsEnabledXAxisEvents()) return;
            string location = FormatPoint(e.GetPosition(this.DataChart));
            DisplayOutput(DataChartStrings.XWDC_ChartEvents_XAxis + " " +
                          DataChartStrings.XWDC_ChartEvents_MouseLeftButtonUp + location);
        }

        private void XAxis_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!IsEnabledXAxisEvents()) return;
            string location = FormatPoint(e.GetPosition(this.DataChart));
            DisplayOutput(DataChartStrings.XWDC_ChartEvents_XAxis + " " +
                          DataChartStrings.XWDC_ChartEvents_MouseLeftButtonDown + location);
        }

        //================================================================================
        #endregion
        #region Event Handlers: YAxis
        //================================================================================
        private void YAxis_Loaded(object sender, RoutedEventArgs e)
        {
            if (!this.IsEnabledYAxisEvents()) return;
            DisplayOutput(DataChartStrings.XWDC_ChartEvents_YAxis + " " +
                          DataChartStrings.XWDC_ChartEvents_Loaded);
        }
        private void YAxis_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (!this.IsEnabledYAxisEvents()) return;
            DisplayOutput(DataChartStrings.XWDC_ChartEvents_YAxis + " " +
                          DataChartStrings.XWDC_ChartEvents_SizeChanged + FormatSize(e.NewSize));
        }
        private void YAxis_RangeChanged(object sender, AxisRangeChangedEventArgs e)
        {
            if (!this.IsEnabledYAxisEvents()) return;
            DisplayOutput(DataChartStrings.XWDC_ChartEvents_YAxis + " " +
                          DataChartStrings.XWDC_ChartEvents_SizeChanged + FormatRange(e.MinimumValue, e.MaximumValue));
        }
        private void YAxis_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!IsEnabledYAxisEvents()) return;
            string location = FormatPoint(e.GetPosition(this.DataChart));
            DisplayOutput(DataChartStrings.XWDC_ChartEvents_YAxis + " " +
                          DataChartStrings.XWDC_ChartEvents_MouseEnter + location);
        }
        private void YAxis_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!IsEnabledYAxisEvents()) return;
            string location = FormatPoint(e.GetPosition(this.DataChart));
            DisplayOutput(DataChartStrings.XWDC_ChartEvents_YAxis + " " +
                          DataChartStrings.XWDC_ChartEvents_MouseLeave + location);
        }
        private void YAxis_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!IsEnabledYAxisEvents()) return;
            string location = FormatPoint(e.GetPosition(this.DataChart));
            DisplayOutput(DataChartStrings.XWDC_ChartEvents_YAxis + " " +
                          DataChartStrings.XWDC_ChartEvents_MouseLeftButtonUp + location);
        }

        private void YAxis_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!IsEnabledYAxisEvents()) return;
            string location = FormatPoint(e.GetPosition(this.DataChart));
            DisplayOutput(DataChartStrings.XWDC_ChartEvents_YAxis + " " +
                          DataChartStrings.XWDC_ChartEvents_MouseLeftButtonDown + location);
        }


        //================================================================================
        #endregion
 
    }
}
