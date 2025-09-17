using Infragistics.Controls.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Resources;
using IGDataChart.Resources;
using Infragistics.Samples;

namespace IGDataChart.Samples.Display.Axes
{
    /// <summary>
    /// Interaction logic for AxisIntervals.xaml
    /// </summary>
    public partial class AxisIntervals : Infragistics.Samples.Framework.SampleContainer
    {
        NumericXAxis xAxis;
        NumericYAxis yAxis;
        CategoryXAxis categoryXAxis;

        LineSeries lineSeries1;
        LineSeries lineSeries2;
        LineSeries lineSeries3;

        ScatterLineSeries scatterLineSeries1;
        ScatterLineSeries scatterLineSeries2;
        ScatterLineSeries scatterLineSeries3;

        ColumnSeries columnSeries1;
        ColumnSeries columnSeries2;
        ColumnSeries columnSeries3;

        WeatherData data;

        ResourceManager dataChartStringResx;

        enum SampleChartType { CategoryXAxis, NumericXAxis };

        public AxisIntervals()
        {
            InitializeComponent();

            //Event Handlers
            xMajorIntervalThicknessSlider.ValueChanged += xMajorIntervalThicknessSlider_ValueChanged;
            xMinorIntervalThicknessSlider.ValueChanged += xMinorIntervalThicknessSlider_ValueChanged;

            yMajorIntervalThicknessSlider.ValueChanged += yMajorIntervalThicknessSlider_ValueChanged;
            yMinorIntervalThicknessSlider.ValueChanged += yMinorIntervalThicknessSlider_ValueChanged;

            xMajorIntervalToggle.Click += xMajorIntervalToggle_Click;
            xMinorIntervalToggle.Click += xMinorIntervalToggle_Click;

            yMajorIntervalToggle.Click += yMajorIntervalToggle_Click;
            yMinorIntervalToggle.Click += yMinorIntervalToggle_Click;

            xMinorIntervalValueSlider.ValueChanged += xMinorIntervalValueSlider_ValueChanged;
            yMinorIntervalValueSlider.ValueChanged += yMinorIntervalValueSlider_ValueChanged;

            xAxisSelectorCb.SelectionChanged += xAxisSelectorCb_SelectionChanged;

            data = new WeatherData();

            dataChartStringResx = DataChartStrings.ResourceManager;

            //Instantiate Axes
            yAxis = new NumericYAxis();
            xAxis = new NumericXAxis();
            categoryXAxis = new CategoryXAxis();

            scatterLineSeries1 = new ScatterLineSeries();
            scatterLineSeries2 = new ScatterLineSeries();
            scatterLineSeries3 = new ScatterLineSeries();

            lineSeries1 = new LineSeries();
            lineSeries2 = new LineSeries();
            lineSeries3 = new LineSeries();

            columnSeries1 = new ColumnSeries();
            columnSeries2 = new ColumnSeries();
            columnSeries3 = new ColumnSeries();

            //Set Axis title
            xAxis.Title = dataChartStringResx.GetString("XWDC_AxisIntervals_XAxis_Title");
            yAxis.Title = dataChartStringResx.GetString("XWDC_AxisIntervals_YAxis_Title");
            categoryXAxis.Title = dataChartStringResx.GetString("XWDC_AxisIntervals_CategoryXAxis_Title");

            //Set Minor Interval for axis
            yAxis.MinorInterval = 5;
            xAxis.MinorInterval = .5;
            categoryXAxis.MinorInterval = 0.5;

            //Set Interval for axis
            yAxis.Interval = 10;
            xAxis.Interval = 2;
            categoryXAxis.Interval = 2;

            //Set ItemsSource for XAxes
            categoryXAxis.ItemsSource = data;

            //Set Labels for Category Axes
            categoryXAxis.Label = "{Date:MMM}";

            //Set Minor Interval appearance for axis
            yAxis.MinorStrokeThickness = 1;
            xAxis.MinorStrokeThickness = 1;
            yAxis.MinorStroke = new SolidColorBrush(Colors.Red);
            xAxis.MinorStroke = new SolidColorBrush(Colors.Red);
            categoryXAxis.MinorStroke = new SolidColorBrush(Colors.Red);

            categoryXAxis.Gap = 0.5;

            //Set Major Interval appearance for axis
            yAxis.MajorStrokeThickness = 2;
            yAxis.MajorStroke = new SolidColorBrush(Colors.Green);
            xAxis.MajorStroke = new SolidColorBrush(Colors.Green);
            xAxis.MajorStrokeThickness = 2;
            categoryXAxis.MajorStroke = new SolidColorBrush(Colors.Green);

            //Set MinimumValue for xAxis, (to sync axis origin with initial data points)
            xAxis.MinimumValue = 1;

            //Sync labels with actual minor stroke properties
            yMinorIntervalThicknessLbl.Text = dataChartStringResx.GetString("XWDC_AxisIntervals_YMinorIntervalThickness_Label") + " " + yAxis.MinorStrokeThickness;
            yMinorIntervalValueLbl.Text = dataChartStringResx.GetString("XWDC_AxisIntervals_YMinorIntervalValue_Label") + " " + yAxis.MinorInterval;

            xMinorIntervalThicknessLbl.Text = dataChartStringResx.GetString("XWDC_AxisIntervals_XMinorIntervalThickness_Label") + " " + xAxis.MinorStrokeThickness;
            xMinorIntervalValueLbl.Text = dataChartStringResx.GetString("XWDC_AxisIntervals_XMinorIntervalValue_Label") + " " + xAxis.MinorInterval;

            yMajorIntervalThicknessLbl.Text = dataChartStringResx.GetString("XWDC_AxisIntervals_YMajorIntervalThickness_Label") + " " + yAxis.MajorStrokeThickness;
            xMajorIntervalThicknessLbl.Text = dataChartStringResx.GetString("XWDC_AxisIntervals_XMajorIntervalThickness_Label") + " " + xAxis.MajorStrokeThickness;

            //Sync slider values with actual minor interval values
            xMinorIntervalValueSlider.Value = xAxis.MinorInterval;
            yMinorIntervalValueSlider.Value = yAxis.MinorInterval;

            //Add YAxis to the chart as it is constant during runtime
            chart1.Axes.Add(yAxis);

            //Enable zooming on chart
            chart1.IsVerticalZoomEnabled = true;
            chart1.IsHorizontalZoomEnabled = true;

            //Enable panning on chart via modifier key assignment
            chart1.PanModifier = ModifierKeys.Shift;

            //Select CategoryDateTimeXAxis item to within the combo to render it on chart
            xAxisSelectorCb.SelectedIndex = 0;

            SwapOutXAxis(SampleChartType.NumericXAxis);
        }

        void xAxisSelectorCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (xAxisSelectorCb.SelectedIndex)
            {
                case 0:
                    SwapOutXAxis(SampleChartType.NumericXAxis);
                    break;
                case 1:
                    SwapOutXAxis(SampleChartType.CategoryXAxis);
                    break;
                default:
                    break;
            }
        }

        void SwapOutXAxis(SampleChartType sampleChartType)
        {
            switch (sampleChartType)
            {
                case SampleChartType.CategoryXAxis:
                    {
                        chart1.Axes.Remove(xAxis);
                        chart1.Series.Clear();
                        chart1.Axes.Add(categoryXAxis);
                        //Create chart series and assign to chart
                        columnSeries1.ItemsSource = data;
                        columnSeries1.Thickness = 1;
                        columnSeries1.ValueMemberPath = "temperatureHigh";
                        columnSeries1.Title = dataChartStringResx.GetString("XWDC_AxisIntervals_Series_Title_High");
                        columnSeries1.MarkerType = MarkerType.None;
                        columnSeries1.ShadowDepth = 5;
                        columnSeries1.ShadowBlur = 7;
                        columnSeries1.XAxis = categoryXAxis;
                        columnSeries1.YAxis = yAxis;
                        chart1.Series.Add(columnSeries1);

                        columnSeries2.ItemsSource = data;
                        columnSeries2.Thickness = 1;
                        columnSeries2.ValueMemberPath = "averageTemperature";
                        columnSeries2.Title = dataChartStringResx.GetString("XWDC_AxisIntervals_Series_Title_Average");
                        columnSeries2.MarkerType = MarkerType.None;
                        columnSeries2.ShadowDepth = 5;
                        columnSeries2.ShadowBlur = 7;
                        columnSeries2.XAxis = categoryXAxis;
                        columnSeries2.YAxis = yAxis;
                        chart1.Series.Add(columnSeries2);

                        columnSeries3.ItemsSource = data;
                        columnSeries3.Thickness = 1;
                        columnSeries3.ValueMemberPath = "temperatureLow";
                        columnSeries3.Title = dataChartStringResx.GetString("XWDC_AxisIntervals_Series_Title_Low");
                        columnSeries3.MarkerType = MarkerType.None;
                        columnSeries3.ShadowDepth = 5;
                        columnSeries3.ShadowBlur = 7;
                        columnSeries3.XAxis = categoryXAxis;
                        columnSeries3.YAxis = yAxis;
                        chart1.Series.Add(columnSeries3);

                        categoryXAxis.Label = "{Label}";

                        xMinorIntervalValueLbl.Opacity = 1;

                        xMinorIntervalValueSlider.IsEnabled = true;
                    }
                    break;
                case SampleChartType.NumericXAxis:
                    {
                        chart1.Axes.Remove(categoryXAxis);
                        chart1.Series.Clear();
                        chart1.Axes.Add(xAxis);

                        //Create chart series and assign to chart
                        scatterLineSeries1 = new ScatterLineSeries();
                        scatterLineSeries1.ItemsSource = data;
                        scatterLineSeries1.Thickness = 5;
                        scatterLineSeries1.XMemberPath = "DayOfMonth";
                        scatterLineSeries1.YMemberPath = "temperatureHigh";
                        scatterLineSeries1.Title = dataChartStringResx.GetString("XWDC_AxisIntervals_Series_Title_High");
                        scatterLineSeries1.MarkerType = MarkerType.None;
                        scatterLineSeries1.ShadowDepth = 5;
                        scatterLineSeries1.ShadowBlur = 7;
                        scatterLineSeries1.XAxis = xAxis;
                        scatterLineSeries1.YAxis = yAxis;
                        chart1.Series.Add(scatterLineSeries1);

                        scatterLineSeries2 = new ScatterLineSeries();
                        scatterLineSeries2.Thickness = 5;
                        scatterLineSeries2.ItemsSource = data;
                        scatterLineSeries2.XMemberPath = "DayOfMonth";
                        scatterLineSeries2.YMemberPath = "averageTemperature";
                        scatterLineSeries2.Title = dataChartStringResx.GetString("XWDC_AxisIntervals_Series_Title_Average");
                        scatterLineSeries2.MarkerType = MarkerType.None;
                        scatterLineSeries2.ShadowDepth = 5;
                        scatterLineSeries2.ShadowBlur = 7;
                        scatterLineSeries2.XAxis = xAxis;
                        scatterLineSeries2.YAxis = yAxis;
                        chart1.Series.Add(scatterLineSeries2);

                        scatterLineSeries3 = new ScatterLineSeries();
                        scatterLineSeries3.Thickness = 5;
                        scatterLineSeries3.ItemsSource = data;
                        scatterLineSeries3.XMemberPath = "DayOfMonth";
                        scatterLineSeries3.YMemberPath = "temperatureLow";
                        scatterLineSeries3.Title = dataChartStringResx.GetString("XWDC_AxisIntervals_Series_Title_Low");
                        scatterLineSeries3.MarkerType = MarkerType.None;
                        scatterLineSeries3.ShadowDepth = 5;
                        scatterLineSeries3.ShadowBlur = 7;
                        scatterLineSeries3.XAxis = xAxis;
                        scatterLineSeries3.YAxis = yAxis;
                        chart1.Series.Add(scatterLineSeries3);

                        xMinorIntervalValueLbl.Opacity = 1;

                        xMinorIntervalValueSlider.IsEnabled = true;
                    }
                    break;
            }
        }

        void xMajorIntervalToggle_Click(object sender, RoutedEventArgs e)
        {
            if (chart1.Axes.Contains(xAxis))
            {
                if (xMajorIntervalToggle.IsChecked == true)
                {
                    xMajorIntervalToggle.Content = dataChartStringResx.GetString("XWDC_AxisIntervals_ToggleButton_DisplayInterval");

                    xMajorIntervalThicknessLbl.Opacity = .50;

                    xMajorIntervalThicknessSlider.IsEnabled = false;
                    xAxis.MajorStroke.Opacity = 0;
                }
                else if (xMajorIntervalToggle.IsChecked == false)
                {
                    xMajorIntervalToggle.Content = dataChartStringResx.GetString("XWDC_AxisIntervals_ToggleButton_HideInterval");

                    xMajorIntervalThicknessLbl.Opacity = 1;

                    xMajorIntervalThicknessSlider.IsEnabled = true;
                    xAxis.MajorStroke.Opacity = 1;
                }
            }
            else if (chart1.Axes.Contains(categoryXAxis))
            {
                if (xMajorIntervalToggle.IsChecked == true)
                {
                    xMajorIntervalToggle.Content = dataChartStringResx.GetString("XWDC_AxisIntervals_ToggleButton_DisplayInterval");

                    xMajorIntervalThicknessLbl.Opacity = .50;

                    xMajorIntervalThicknessSlider.IsEnabled = false;
                    categoryXAxis.MajorStroke.Opacity = 0;
                }
                else if (xMajorIntervalToggle.IsChecked == false)
                {
                    xMajorIntervalToggle.Content = dataChartStringResx.GetString("XWDC_AxisIntervals_ToggleButton_HideInterval");

                    xMajorIntervalThicknessLbl.Opacity = 1;

                    xMajorIntervalThicknessSlider.IsEnabled = true;
                    categoryXAxis.MajorStroke.Opacity = 1;
                }
            }
        }

        void yMajorIntervalToggle_Click(object sender, RoutedEventArgs e)
        {
            if (yMajorIntervalToggle.IsChecked == true)
            {
                yMajorIntervalToggle.Content = dataChartStringResx.GetString("XWDC_AxisIntervals_ToggleButton_DisplayInterval");

                yMajorIntervalThicknessLbl.Opacity = .50;

                yMajorIntervalThicknessSlider.IsEnabled = false;
                yAxis.MajorStroke.Opacity = 0;
            }
            else if (yMajorIntervalToggle.IsChecked == false)
            {
                yMajorIntervalToggle.Content = dataChartStringResx.GetString("XWDC_AxisIntervals_ToggleButton_HideInterval");

                yMajorIntervalThicknessLbl.Opacity = 1;

                yMajorIntervalThicknessSlider.IsEnabled = true;
                yAxis.MajorStroke.Opacity = 1;
            }
        }

        void yMajorIntervalThicknessSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            yAxis.MajorStrokeThickness = e.NewValue;
            yMajorIntervalThicknessLbl.Text = dataChartStringResx.GetString("XWDC_AxisIntervals_YMajorIntervalThickness_Label") + " " + Math.Round(e.NewValue, 0);
        }

        void xMajorIntervalThicknessSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (chart1.Axes.Contains(xAxis))
            {
                xAxis.MajorStrokeThickness = e.NewValue;
                xMajorIntervalThicknessLbl.Text = dataChartStringResx.GetString("XWDC_AxisIntervals_XMajorIntervalThickness_Label") + " " + Math.Round(e.NewValue, 0);
            }
            else if (chart1.Axes.Contains(categoryXAxis))
            {
                categoryXAxis.MajorStrokeThickness = e.NewValue;
                xMajorIntervalThicknessLbl.Text = dataChartStringResx.GetString("XWDC_AxisIntervals_XMajorIntervalThickness_Label") + " " + Math.Round(e.NewValue, 0);
            }
        }

        void xMinorIntervalValueSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (chart1.Axes.Contains(xAxis))
            {
                xAxis.MinorInterval = e.NewValue;
                xMinorIntervalValueLbl.Text = dataChartStringResx.GetString("XWDC_AxisIntervals_XMinorIntervalValue_Label") + " " + Math.Round(e.NewValue, 2);

                xMinorIntervalValueSlider.IsEnabled = true;
                xMinorIntervalValueSlider.IsEnabled = true;
            }
            else if (chart1.Axes.Contains(categoryXAxis))
            {
                categoryXAxis.MinorInterval = e.NewValue;
                xMinorIntervalValueLbl.Text = dataChartStringResx.GetString("XWDC_AxisIntervals_XMinorIntervalValue_Label") + " " + Math.Round(e.NewValue, 2);

                xMinorIntervalValueSlider.IsEnabled = true;
                xMinorIntervalValueSlider.IsEnabled = true;
            }
        }

        void yMinorIntervalToggle_Click(object sender, RoutedEventArgs e)
        {
            if (yMinorIntervalToggle.IsChecked == true)
            {
                yMinorIntervalToggle.Content = dataChartStringResx.GetString("XWDC_AxisIntervals_ToggleButton_DisplayInterval");

                yMinorIntervalThicknessLbl.Opacity = .50;
                yMinorIntervalValueLbl.Opacity = .50;

                yMinorIntervalThicknessSlider.IsEnabled = false;
                yMinorIntervalValueSlider.IsEnabled = false;
                yAxis.MinorStroke.Opacity = 0;
            }
            else if (yMinorIntervalToggle.IsChecked == false)
            {
                yMinorIntervalToggle.Content = dataChartStringResx.GetString("XWDC_AxisIntervals_ToggleButton_HideInterval");

                yMinorIntervalThicknessLbl.Opacity = 1;
                yMinorIntervalValueLbl.Opacity = 1;

                yMinorIntervalThicknessSlider.IsEnabled = true;
                yMinorIntervalValueSlider.IsEnabled = true;
                yAxis.MinorStroke.Opacity = 1;
            }
        }

        void xMinorIntervalToggle_Click(object sender, RoutedEventArgs e)
        {
            if (chart1.Axes.Contains(xAxis))
            {
                if (xMinorIntervalToggle.IsChecked == true)
                {
                    xMinorIntervalToggle.Content = dataChartStringResx.GetString("XWDC_AxisIntervals_ToggleButton_DisplayInterval");

                    xMinorIntervalThicknessLbl.Opacity = .50;
                    xMinorIntervalValueLbl.Opacity = .50;

                    xMinorIntervalThicknessSlider.IsEnabled = false;
                    xMinorIntervalValueSlider.IsEnabled = false;
                    xAxis.MinorStroke.Opacity = 0;
                }
                else if (xMinorIntervalToggle.IsChecked == false)
                {
                    xMinorIntervalToggle.Content = dataChartStringResx.GetString("XWDC_AxisIntervals_ToggleButton_HideInterval");

                    xMinorIntervalThicknessLbl.Opacity = 1;
                    xMinorIntervalValueLbl.Opacity = 1;

                    xMinorIntervalThicknessSlider.IsEnabled = true;
                    xMinorIntervalValueSlider.IsEnabled = true;
                    xAxis.MinorStroke.Opacity = 1;
                }
            }
            else if (chart1.Axes.Contains(categoryXAxis))
            {
                if (xMinorIntervalToggle.IsChecked == true)
                {
                    xMinorIntervalToggle.Content = dataChartStringResx.GetString("XWDC_AxisIntervals_ToggleButton_DisplayInterval");

                    xMinorIntervalThicknessLbl.Opacity = .50;
                    xMinorIntervalValueLbl.Opacity = .50;

                    xMinorIntervalThicknessSlider.IsEnabled = false;
                    xMinorIntervalValueSlider.IsEnabled = false;
                    categoryXAxis.MinorStroke.Opacity = 0;
                }
                else if (xMinorIntervalToggle.IsChecked == false)
                {
                    xMinorIntervalToggle.Content = dataChartStringResx.GetString("XWDC_AxisIntervals_ToggleButton_HideInterval");

                    xMinorIntervalThicknessLbl.Opacity = 1;
                    xMinorIntervalValueLbl.Opacity = 1;

                    xMinorIntervalThicknessSlider.IsEnabled = true;
                    xMinorIntervalValueSlider.IsEnabled = true;
                    categoryXAxis.MinorStroke.Opacity = 1;
                }
            }
        }

        void xMinorIntervalThicknessSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (chart1.Axes.Contains(xAxis))
            {
                xAxis.MinorStrokeThickness = e.NewValue;
                xMinorIntervalThicknessLbl.Text = dataChartStringResx.GetString("XWDC_AxisIntervals_XMinorIntervalThickness_Label") + " " + e.NewValue;
            }
            else if (chart1.Axes.Contains(categoryXAxis))
            {
                categoryXAxis.MinorStrokeThickness = e.NewValue;
                xMinorIntervalThicknessLbl.Text = dataChartStringResx.GetString("XWDC_AxisIntervals_XMinorIntervalThickness_Label") + " " + e.NewValue;
            }
        }

        void yMinorIntervalValueSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            yAxis.MinorInterval = e.NewValue;
            yMinorIntervalValueLbl.Text = dataChartStringResx.GetString("XWDC_AxisIntervals_YMinorIntervalValue_Label") + " " + Math.Round(e.NewValue, 2);
        }

        void yMinorIntervalThicknessSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            yAxis.MinorStrokeThickness = e.NewValue;
            yMinorIntervalThicknessLbl.Text = dataChartStringResx.GetString("XWDC_AxisIntervals_YMinorIntervalThickness_Label") + " " + Math.Round(e.NewValue, 2);
        }
    }
}
