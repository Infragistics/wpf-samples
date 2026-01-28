using System;
using System.Windows.Controls;
using IGMap.Models;                         // MapAdapter
using Infragistics.Controls.Maps;           // MapWindowAnimationMode
using Infragistics.Samples.Shared.Models;   // GeoRegions

namespace IGMap.Samples.Navigation
{
    public partial class MapWindowAnimation : Infragistics.Samples.Framework.SampleContainer
    {
        public MapWindowAnimation()
        {
            InitializeComponent();
            InitializeMap();
        }

        private void InitializeMap()
        {
            this.igMap.WindowAnimationMode = MapWindowAnimationMode.Logarithmic;

            // initialize xamMap boundary to specific map region
            MapAdapter.SetMapBoundary(this.igMap, GeoRegions.WorldFullRegion);
            MapAdapter.ZoomMapToRegion(this.igMap, GeoRegions.WorldNonPolarRegion);
        }

        private void cmbAnimationType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            if (cmb == null) return;
            if (cmb.SelectedItem == null) return;
            if (this.igMap == null) return;

            string tag = ((ComboBoxItem) cmb.SelectedItem).Tag.ToString();
            this.igMap.WindowAnimationMode =
                (MapWindowAnimationMode) Enum.Parse(typeof (MapWindowAnimationMode), tag, true);
        }
    }

 
}