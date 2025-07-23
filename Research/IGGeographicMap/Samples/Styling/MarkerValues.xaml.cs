using System.Collections.Specialized;
using IGGeographicMap.Models;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Resources;
using Infragistics.Controls.Maps;
using System;
using System.Linq;
using System.Globalization;
using System.Threading;

namespace IGGeographicMap.Samples.Styling
{
    public partial class MarkerValues : Infragistics.Samples.Framework.SampleContainer
    {
        public MarkerValues()
        {
            InitializeComponent();

            var path = "/Infragistics.Samples.Services;component/shapefiles/world/";

            CultureInfo culture = Thread.CurrentThread.CurrentCulture;

            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
            
            Shapefile = new ShapefileConverter();
            Shapefile.ImportCompleted += OnShapefileImported;
            Shapefile.ShapefileSource = new Uri(path + "earthquakes-usgs.shp", UriKind.RelativeOrAbsolute);
            Shapefile.DatabaseSource = new Uri(path + "earthquakes-usgs.dbf", UriKind.RelativeOrAbsolute);

            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }

        protected ShapefileConverter Shapefile;

        private void OnShapefileImported(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            // filtering some ShapefileRecord objects
            var dataSource = Shapefile.Where(i => double.Parse(i.Fields["magnitude"].ToString()) > 5.5).ToList();

            // binding data
            this.GeoMap.DataContext = dataSource;

            // zooming geo-map to a specific geographic region of the world using GeoMapAdapter
            this.GeoMap.NavigateTo(GeoRegions.WorldNonAntarcticRegion);
        }
    }
}
