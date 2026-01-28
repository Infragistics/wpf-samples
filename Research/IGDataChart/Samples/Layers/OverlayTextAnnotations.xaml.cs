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
    public partial class OverlayTextAnnotations : Infragistics.Samples.Framework.SampleContainer
    { 
        public OverlayTextAnnotations()
        {
            InitializeComponent();
            UseDefaultTheme = true;

            this.Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var dataSource = new StockTeslaData();

            this.Chart.DataContext = dataSource;
            this.xAxis.ItemsSource = dataSource;

            var AnnoData = new List<DataAnnotation>
            {
                new DataAnnotation() { Value = 0,  }
            };
            AnnoLayer.Brush = new SolidColorBrush(Colors.Red);
            AnnoLayer.Outline = new SolidColorBrush(Colors.Red);
            AnnoLayer.AnnotationTextColor = new SolidColorBrush(Colors.White);
            AnnoLayer.AnnotationValueMinPrecision = 1;
            AnnoLayer.AnnotationLabelMemberPath = "Label";
            AnnoLayer.AnnotationValueMemberPath = "Value";
            AnnoLayer.ItemsSource = AnnoData;
        }

    }
}