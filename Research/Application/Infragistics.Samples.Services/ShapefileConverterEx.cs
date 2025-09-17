using Infragistics.Controls.Maps;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Xml.Linq;

namespace Infragistics.Controls.Maps
{
    public class ShapefileItem
    {
        public ShapefileItem()
        {
            Point = new Point(double.NaN, double.NaN);
        }
        public Point Point { get; set; }
        public ShapefileRecordFields Fields { get; set; }

    }
    public static class ShapefileConverterEx
    {

        static ShapefileConverterEx()
        {
            //var path = "/Infragistics.Samples.Assets;component/storage/en";
            //path += "/shapefiles/america/hawaii-big-island"; 
            //var shapefile = new ShapefileConverter();  
            //shapefile.ImportCompleted += OnShapefileImported;
            //shapefile.ShapefileSource = new Uri(path + ".shp", UriKind.RelativeOrAbsolute);
            //shapefile.DatabaseSource  = new Uri(path + ".dbf", UriKind.RelativeOrAbsolute);

        }
        static void OnShapefileImported(object sender, AsyncCompletedEventArgs e)
        {   
            var shapefile = sender as ShapefileConverter;
            shapefile.Extract();

        }
        public static void Extract(this ShapefileConverter shapefile)
        {
            var data = new List<ShapefileItem>();
            foreach (var record in shapefile)
            {
                foreach (var points in record.Points)
                {
                    foreach (var point in points)
                    {
                        var item = new ShapefileItem();
                        item.Point = point;
                        item.Fields = record.Fields;
                        data.Add(item);
                        //item.Value = double.Parse(record.Fields["CONTOUR"].ToString());
                    }
                }
            } 
        } 
    } 
}
