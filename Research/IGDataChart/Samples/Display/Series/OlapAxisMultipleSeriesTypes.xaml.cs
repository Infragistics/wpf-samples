using System.Linq;
using System.Windows.Controls;
using Infragistics.Controls.Charts;
using Infragistics.Olap;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Controls;
using System;

namespace IGDataChart.Samples.Display.Series
{
    public partial class OlapAxisMultipleSeriesTypes : SampleContainer
    {
        // when creating data chart series, holds the current count of created series
        int seriesCount = 0;
        IOlapViewModel dataSource;

        public OlapAxisMultipleSeriesTypes()
        {
            InitializeComponent();
            SeriesTypes.ItemsSource = EnumValuesProvider.GetEnumValues<OlapXAxisDefaultSeries>();
            EnumValuesProvider.GetEnumValues<OlapXAxisDefaultSeries>().Take(3).Select(seriesType => new OlapAxisSeriesWrapper(seriesType)).
                ToList().ForEach(seriesWrapper => SeriesTypesUsed.Items.Add(seriesWrapper));

            dataSource = ((OlapXAxis)DataChart.Axes["olapXAxis"]).DataSource;
        }

        private void OlapXAxis_SeriesInitializing(object sender, SeriesInitializingEventArgs e)
        {
            seriesCount = 0;
        }

        private void OlapXAxis_SeriesCreating(object sender, Infragistics.Controls.Charts.SeriesCreatingEventArgs e)
        {
            if (SeriesTypesUsed == null)
                return;

            int currentUsedSeriesIndex;

            if (SeriesTypesUsed.Items.Count == 1)
                currentUsedSeriesIndex = 0;
            else
                currentUsedSeriesIndex = seriesCount > SeriesTypesUsed.Items.Count - 1 ? seriesCount % (SeriesTypesUsed.Items.Count - 1) : seriesCount;

            switch (((OlapAxisSeriesWrapper)SeriesTypesUsed.Items[currentUsedSeriesIndex]).SeriesType)
            {
                case OlapXAxisDefaultSeries.AreaSeries:
                    e.Series = new AreaSeries()
                    {
                        ItemsSource = e.SeriesInfo.ItemsSource,
                        ValueMemberPath = "Cell.Value",
                        Name = e.SeriesInfo.Name,
                        Title = e.SeriesInfo.Title,
                        IsHighlightingEnabled = true,
                        IsTransitionInEnabled = true,
                    };
                    break;
                case OlapXAxisDefaultSeries.ColumnSeries:
                    e.Series = new ColumnSeries()
                    {
                        ItemsSource = e.SeriesInfo.ItemsSource,
                        ValueMemberPath = "Cell.Value",
                        Name = e.SeriesInfo.Name,
                        Title = e.SeriesInfo.Title,
                        IsHighlightingEnabled = true,
                        IsTransitionInEnabled = true,
                    };
                    break;
                case OlapXAxisDefaultSeries.LineSeries:
                    e.Series = new LineSeries()
                    {
                        ItemsSource = e.SeriesInfo.ItemsSource,
                        ValueMemberPath = "Cell.Value",
                        Name = e.SeriesInfo.Name,
                        Title = e.SeriesInfo.Title,
                        IsHighlightingEnabled = true,
                        IsTransitionInEnabled = true,
                    };
                    break;
                case OlapXAxisDefaultSeries.PointSeries:
                    e.Series = new PointSeries()
                    {
                        ItemsSource = e.SeriesInfo.ItemsSource,
                        ValueMemberPath = "Cell.Value",
                        Name = e.SeriesInfo.Name,
                        Title = e.SeriesInfo.Title,
                        IsHighlightingEnabled = true,
                        IsTransitionInEnabled = true,
                    };
                    break;
                case OlapXAxisDefaultSeries.StepAreaSeries:
                    e.Series = new StepAreaSeries()
                    {
                        ItemsSource = e.SeriesInfo.ItemsSource,
                        ValueMemberPath = "Cell.Value",
                        Name = e.SeriesInfo.Name,
                        Title = e.SeriesInfo.Title,
                        IsHighlightingEnabled = true,
                        IsTransitionInEnabled = true,
                    };
                    break;
                case OlapXAxisDefaultSeries.StepLineSeries:
                    e.Series = new StepLineSeries()
                    {
                        ItemsSource = e.SeriesInfo.ItemsSource,
                        ValueMemberPath = "Cell.Value",
                        Name = e.SeriesInfo.Name,
                        Title = e.SeriesInfo.Title,
                        IsHighlightingEnabled = true,
                        IsTransitionInEnabled = true,
                    };
                    break;
                case OlapXAxisDefaultSeries.WaterfallSeries:
                    e.Series = new WaterfallSeries()
                    {
                        ItemsSource = e.SeriesInfo.ItemsSource,
                        ValueMemberPath = "Cell.Value",
                        Name = e.SeriesInfo.Name,
                        Title = e.SeriesInfo.Title,
                        IsHighlightingEnabled = true,
                        IsTransitionInEnabled = true,
                    };
                    break;
            }
            seriesCount++;
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var button = sender as Button;
            int selectedIndex = SeriesTypesUsed.SelectedIndex == -1 ? 0 : SeriesTypesUsed.SelectedIndex;

            if (button.Tag.ToString() == "right")
            {
                var seriesTypeToInsert = (OlapXAxisDefaultSeries)(SeriesTypes.SelectedItem != null ? SeriesTypes.SelectedItem : SeriesTypes.Items[0]);
                SeriesTypesUsed.Items.Insert(selectedIndex, new OlapAxisSeriesWrapper(seriesTypeToInsert));
            }
            else if (button.Tag.ToString() == "remove")
            {
                if (SeriesTypesUsed.Items.Count > 1)
                    SeriesTypesUsed.Items.RemoveAt(selectedIndex);
            }
            else if (button.Tag.ToString() == "up")
            {
                if (selectedIndex != 0)
                {
                    SeriesTypesUsed.Items.Insert(selectedIndex - 1, SeriesTypesUsed.Items[selectedIndex]);
                    SeriesTypesUsed.Items.RemoveAt(selectedIndex + 1);
                }
            }
            else // down
            {
                if (selectedIndex != SeriesTypesUsed.Items.Count - 1)
                {
                    SeriesTypesUsed.Items.Insert(selectedIndex + 2, SeriesTypesUsed.Items[selectedIndex]);
                    SeriesTypesUsed.Items.RemoveAt(selectedIndex);
                }
            }

            dataSource.RefreshGrid();
        }
    }

    // simple class in order for the listbox to be able to differenciate different items if two series of the same type are added
    public class OlapAxisSeriesWrapper
    {
        public OlapAxisSeriesWrapper(OlapXAxisDefaultSeries seriesType)
        {
            SeriesType = seriesType;
            TimeStamp = DateTime.Now;
        }

        public OlapXAxisDefaultSeries SeriesType { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
