using System.Collections.Specialized;
using IGGeographicMap.Samples.Custom;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Resources;

namespace IGGeographicMap.Samples.Data
{
    public partial class BindingShapeFiles : Infragistics.Samples.Framework.SampleContainer
    {
        public BindingShapeFiles()
        {
            InitializeComponent();
        }

        private void OnShapefileImportCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            // zoom in geo-map a specific geographic region of the world
            this.GeoMap.NavigateTo(GeoRegions.WorldNonAntarcticRegion); // using GeoMapAdapter class
        }
    }
}
