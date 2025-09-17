using Infragistics.Core;
using Infragistics.XamarinForms.Controls.Charts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Infragistics.Framework
{ 
    public class ChartDataColumn : DataColumnBase
    {
        public ChartDataColumn()
        {
            HorizontalOptions = LayoutOptions.FillAndExpand;
            VerticalOptions = LayoutOptions.FillAndExpand;
        
            CellPadding = new Thickness(0, 3, 20, 3);
        }
        public override View CreateItemView(object item, bool isSelected)
        {
            var xaxis = new CategoryXAxis { Label = "Label" };
            xaxis.LabelExtent = 0;
            xaxis.LabelVisibility = Visibility.Collapsed;
            xaxis.MajorStrokeThickness = 0;
            xaxis.MinorStrokeThickness = 0;
            xaxis.StrokeThickness = 0;
            xaxis.TickLength = 0;
            xaxis.Stroke = Colors.Transparent.ToSolidBrush();
            xaxis.MajorStroke = Colors.Transparent.ToSolidBrush();
            xaxis.MinorStroke = Colors.Transparent.ToSolidBrush();

            var yaxis = new NumericYAxis();
            yaxis.LabelExtent = 0;
            xaxis.LabelVisibility = Visibility.Collapsed;
            yaxis.MajorStrokeThickness = 0;
            yaxis.MinorStrokeThickness = 0;
            yaxis.StrokeThickness = 0;
            yaxis.Stroke = Colors.Transparent.ToSolidBrush();
            yaxis.MajorStroke = Colors.Transparent.ToSolidBrush();
            yaxis.MinorStroke = Colors.Transparent.ToSolidBrush();

            var series = new LineSeries();
            series.XAxis = xaxis;
            series.YAxis = yaxis;
            series.ValueMemberPath = "Actual";
            series.IsTransitionInEnabled = true;

            var content = new XamDataChart();
            content.HorizontalZoomable = false;
            content.VerticalZoomable = false;
            content.HorizontalOptions = HorizontalOptions;
            content.VerticalOptions = VerticalOptions;
            content.Axes.Add(xaxis);
            content.Axes.Add(yaxis);
            content.Series.Add(series);
            
            content.BackgroundColor = Colors.Transparent;
            content.PlotAreaBackground = Colors.Transparent.ToSolidBrush();
            
            UpdateBinding(content, item);
            UpdateApperance(content, isSelected);

            return content;
        }

        public override void UpdateBindings(IList items)
        {
            if (items == null) return;
            if (items.Count != Cells.Count) return;

            for (var i = 0; i < items.Count; i++)
            {
                var content = Cells[i].Content as XamDataChart;
                if (content == null) return;

                Cells[i].BindingContext = items[i];

                UpdateBinding(content, items[i]);
            }
        }
        public void UpdateBinding(XamDataChart content, object item)
        {
            if (string.IsNullOrEmpty(MemberPath))
            {
                Logs.Warning(this, "missing value for member path property");
            }
            else
            {
                Logs.Trace(this, "updating binding for item: " + item);
                
                content.BindingContext = item;

                var data = item.GetPropertyValue(MemberPath) as IList;

                var series = content.Series.OfType<LineSeries>().First();
                series.ItemsSource = null;
     
                if (data == null) return;
               
                var min = double.MaxValue;
                var max = double.MinValue;
                foreach (var dataItem in data)
                {
                    var value = dataItem.GetPropertyValue<double>("Actual");
                    min = Math.Min(min, value);
                    max = Math.Max(max, value);
                }
                var range = max - min;
              
                if (Mobile.Platform == TargetPlatform.WinPhone)
                {
                    min -= (range * 0.2);
                }
                else
                {
                    min -= (range * 0.2);
                    max += (range * 0.15);
                }
                var xaxis = content.Axes.OfType<CategoryXAxis>().First();
                xaxis.ItemsSource = data;
                // maximize chart plot area by adjusting axis range
                var yaxis = content.Axes.OfType<NumericYAxis>().First();
                yaxis.MinimumValue = min;
                yaxis.MaximumValue = max;

                series.ItemsSource = data;
              
            }
        }

        public override void UpdateSelection(DataCell cell, bool isSelected)
        {
            if (cell == null) return;

            var content = cell.Content as XamDataChart;
            if (content == null) return;

            UpdateApperance(content, isSelected);
        }
        public void UpdateApperance(XamDataChart content, bool isSelected)
        {
            var series = content.Series.OfType<LineSeries>().First();
            //series.IsTransitionInEnabled = true;
            if (isSelected)
            {
                series.Thickness = 2;
                series.Brush = SelectionForeground.ToSolidBrush();
                series.MarkerOutline = SelectionForeground.ToSolidBrush();
                series.MarkerBrush = Colors.White.ToSolidBrush();
                series.MarkerType = MarkerType.None;
            }
            else
            {
                series.Thickness = 2;
                series.Brush = CellForeground.ToSolidBrush();
                series.MarkerOutline = CellForeground.ToSolidBrush();
                series.MarkerBrush = Colors.White.ToSolidBrush();
                series.MarkerType = MarkerType.None;
            }
        }
    }

}