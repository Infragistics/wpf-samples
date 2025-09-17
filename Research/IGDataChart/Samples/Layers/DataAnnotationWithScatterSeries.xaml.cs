using System; 
using System.Linq;
using System.Windows;
using System.Windows.Controls; 
using System.Windows.Media;
using System.Collections.Generic;

using Infragistics.Controls.Charts;
using IGDataChart.Models;

namespace IGDataChart.Samples.Layers
{
    public partial class DataAnnotationWithScatterSeries : Infragistics.Samples.Framework.SampleContainer
    { 
        public DataAnnotationWithScatterSeries()
        {
            InitializeComponent();
            UseDefaultTheme = true;

            this.Loaded += OnLoaded;
        }


        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.Chart.DataContext = new WorldStatsData();

            // data annotation layer for large countries
            LargeCountriesAnnotationLayer.OverlayTextMemberPath = "Label";
            LargeCountriesAnnotationLayer.OverlayTextLocation = OverlayTextLocation.InsideBottomCenter;
            LargeCountriesAnnotationLayer.StartLabelXDisplayMode = DataAnnotationDisplayMode.DataLabel;
            LargeCountriesAnnotationLayer.StartLabelYDisplayMode = DataAnnotationDisplayMode.Hidden;
            LargeCountriesAnnotationLayer.EndLabelXDisplayMode = DataAnnotationDisplayMode.DataLabel;
            LargeCountriesAnnotationLayer.EndLabelYDisplayMode = DataAnnotationDisplayMode.Hidden;
            LargeCountriesAnnotationLayer.CenterLabelXDisplayMode = DataAnnotationDisplayMode.Hidden;
            LargeCountriesAnnotationLayer.CenterLabelYDisplayMode = DataAnnotationDisplayMode.Hidden;
            LargeCountriesAnnotationLayer.Outline = new SolidColorBrush(Colors.DodgerBlue);
            LargeCountriesAnnotationLayer.Brush = new SolidColorBrush(Colors.DodgerBlue);
            LargeCountriesAnnotationLayer.AnnotationTextColor = new SolidColorBrush(Colors.White);
            LargeCountriesAnnotationLayer.StartValueXMemberPath = "StartX";
            LargeCountriesAnnotationLayer.StartValueYMemberPath = "StartY";
            LargeCountriesAnnotationLayer.EndValueXMemberPath = "EndX";
            LargeCountriesAnnotationLayer.EndValueYMemberPath = "EndY";
            LargeCountriesAnnotationLayer.EndLabelXMemberPath = "EndLabel";
            LargeCountriesAnnotationLayer.StartLabelXMemberPath = "StartLabel";
            LargeCountriesAnnotationLayer.ItemsSource = new List<DataAnnotation>
            {
                new DataAnnotation() {
                    Label = "▲ Large Countries ▲",
                    StartX = 10000000,  // Population
                    StartY = 300,       // GDP
                    EndX = 3000000000,  // Population
                    EndY = 300000,      // GDP
                    EndLabel = "3 B",
                    StartLabel = "10 M",
                },
            };

            // data annotation layer for small countries
            SmallCountriesAnnotationLayer.OverlayTextMemberPath = "Label";
            SmallCountriesAnnotationLayer.OverlayTextLocation = OverlayTextLocation.InsideBottomCenter;
            SmallCountriesAnnotationLayer.StartLabelXDisplayMode = DataAnnotationDisplayMode.DataLabel;
            SmallCountriesAnnotationLayer.StartLabelYDisplayMode = DataAnnotationDisplayMode.Hidden;
            SmallCountriesAnnotationLayer.EndLabelXDisplayMode = DataAnnotationDisplayMode.DataLabel;
            SmallCountriesAnnotationLayer.EndLabelYDisplayMode = DataAnnotationDisplayMode.Hidden;
            SmallCountriesAnnotationLayer.CenterLabelXDisplayMode = DataAnnotationDisplayMode.Hidden;
            SmallCountriesAnnotationLayer.CenterLabelYDisplayMode = DataAnnotationDisplayMode.Hidden;
            SmallCountriesAnnotationLayer.Outline = new SolidColorBrush(Colors.Purple);
            SmallCountriesAnnotationLayer.Brush = new SolidColorBrush(Colors.Purple);
            SmallCountriesAnnotationLayer.AnnotationTextColor = new SolidColorBrush(Colors.White);
            SmallCountriesAnnotationLayer.StartValueXMemberPath = "StartX";
            SmallCountriesAnnotationLayer.StartValueYMemberPath = "StartY";
            SmallCountriesAnnotationLayer.EndValueXMemberPath = "EndX";
            SmallCountriesAnnotationLayer.EndValueYMemberPath = "EndY";
            SmallCountriesAnnotationLayer.EndLabelXMemberPath = "EndLabel";
            SmallCountriesAnnotationLayer.StartLabelXMemberPath = "StartLabel";
            SmallCountriesAnnotationLayer.ItemsSource = new List<DataAnnotation>
            {
                new DataAnnotation() {
                    Label = "▲ Small Countries ▲",
                    StartX = 1000,  // Population
                    StartY = 300,   // GDP
                    EndX = 1000000, // Population
                    EndY = 300000,  // GDP
                    EndLabel = "1 M",
                    StartLabel = "1 K",
                },
            }; 

            // data annotation layer for rich countries
            RichCountriesAnnotationLayer.TargetAxis = yAxis;
            RichCountriesAnnotationLayer.AnnotationValueMemberPath = "Value";
            RichCountriesAnnotationLayer.AnnotationLabelMemberPath = "Label";
            RichCountriesAnnotationLayer.OverlayTextMemberPath = "Overlay";
            RichCountriesAnnotationLayer.OverlayTextLocation = OverlayTextLocation.OutsideTopLeft;
            RichCountriesAnnotationLayer.Brush = new SolidColorBrush(Colors.Green);
            RichCountriesAnnotationLayer.AnnotationTextColor = new SolidColorBrush(Colors.White);
            RichCountriesAnnotationLayer.ItemsSource = new List<DataAnnotation>
            {
                new DataAnnotation() {
                    Overlay = "▲ Rich Countries' Threshold",
                    Label = "40 K",
                    Value = 40000, // GDP
                },
            };

            // data annotation layer for poor countries
            PoorCountriesAnnotationLayer.TargetAxis = yAxis;
            PoorCountriesAnnotationLayer.AnnotationValueMemberPath = "Value";
            PoorCountriesAnnotationLayer.AnnotationLabelMemberPath = "Label";
            PoorCountriesAnnotationLayer.OverlayTextMemberPath = "Overlay";
            PoorCountriesAnnotationLayer.OverlayTextLocation = OverlayTextLocation.OutsideBottomLeft;
            PoorCountriesAnnotationLayer.Brush = new SolidColorBrush(Colors.Red);
            PoorCountriesAnnotationLayer.AnnotationTextColor = new SolidColorBrush(Colors.White);
            PoorCountriesAnnotationLayer.ItemsSource = new List<DataAnnotation>
            {
                new DataAnnotation() {
                    Overlay = "▼ Poor Countries' Threshold",
                    Label = "4 K",
                    Value = 4000, // GDP
                },
            };


        }

    }
}