using System.Collections.Specialized;
using IGGeographicMap.Models;
using IGGeographicMap.Samples.Custom;
using Infragistics.Controls.Maps;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Resources;
using System.Linq;
using System;
using System.Threading;
using System.Globalization;

namespace IGGeographicMap.Samples.Data
{
    public partial class BindingDatabaseFiles : Infragistics.Samples.Framework.SampleContainer
    {
        public BindingDatabaseFiles()
        {
            InitializeComponent();

            CultureInfo culture = Thread.CurrentThread.CurrentCulture;

            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

            var path = "/Infragistics.Samples.Services;component/shapefiles/world/";

            // ShapefileConverter loads data from database (DBF) file and
            // stores it in the Fields property of Dictionary<string, object> type
            Shapefile = new ShapefileConverter();
            Shapefile.ImportCompleted += OnShapefileImported;
            Shapefile.ShapefileSource = new Uri(path + "earthquakes-usgs.shp", UriKind.RelativeOrAbsolute);
            Shapefile.DatabaseSource  = new Uri(path + "earthquakes-usgs.dbf", UriKind.RelativeOrAbsolute);

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
