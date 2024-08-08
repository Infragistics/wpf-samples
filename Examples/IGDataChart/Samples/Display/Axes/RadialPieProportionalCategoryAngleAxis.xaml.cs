using Infragistics.Controls.Charts;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace IGDataChart.Samples.Display.Axes
{
    public partial class RadialPieProportionalCategoryAngleAxis : Infragistics.Samples.Framework.SampleContainer
    {
        public RadialPieProportionalCategoryAngleAxis()
        {
            InitializeComponent();
            this.Loaded += OnLoaded;
            this.DataContext = new SimpleViewModel();

        }

        private AxisAngleLabelMode[] labelModeValues = (AxisAngleLabelMode[])Enum.GetValues(typeof(AxisAngleLabelMode));

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
        
        }
    }

    public class SimpleViewModel
    {
        public ObservableCollection<SimpleItem> Items { get; set; }
        public ObservableCollection<SimpleItem> Filters { get; set; }
        public SimpleViewModel()
        {
            Items = new ObservableCollection<SimpleItem>();
            Items.Add(new SimpleItem("A", 100, 75, 50));
            Items.Add(new SimpleItem("B", 100, 100, 75));
            Items.Add(new SimpleItem("C", 100, 80, 140));
            Items.Add(new SimpleItem("D", 100, 60, 220));
            Items.Add(new SimpleItem("E", 100, 90, 30));
            Items.Add(new SimpleItem("F", 100, 95, 120));
            Items.Add(new SimpleItem("G", 100, 100, 200));
            Items.Add(new SimpleItem("H", 100, 80, 120));
        }
    }

    public class SimpleItem
    {
        public string Label { get; set; }
        public double Value { get; set; }
        public double Radius { get; set; }
        public double Radius2 { get; set; }
        public SimpleItem(string l, double r, double r2, double v)
        {
            Label = l; Value = v; Radius = r;
            Radius2 = r2;
        }
    }
}
