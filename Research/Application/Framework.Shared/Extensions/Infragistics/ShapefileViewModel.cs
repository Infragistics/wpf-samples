
using Infragistics.Controls.Maps;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace Infragistics.Framework
{
    public class ShapefileViewModel : ObservableModel
    {
        public ShapefileViewModel()
        { 
        }
         
        private ShapefileConverter _Shapefile;
        /// <summary> Gets or sets Shapefile </summary>
        public ShapefileConverter Shapefile
        {
            get { return _Shapefile; }
            set { if (_Shapefile == value) return; _Shapefile = value; OnPropertyChanged("Shapefile"); }
        } 

        private List<ShapefileModel> _Models;
        /// <summary> Gets or sets Models </summary>
        public List<ShapefileModel> Models
        {
            get { return _Models; }
            set { if (_Models == value) return; _Models = value; OnPropertyChanged("Models"); }
        }

        private string _FileName;
        /// <summary> Gets or sets FileName </summary>
        public string FileName
        {
            get { return _FileName; }
            set
            {
                if (_FileName == value) return; _FileName = value;
                LoadShapefile();
                OnPropertyChanged("FileName"); }
        }

        public virtual void LoadShapefile()
        {
            var assembly = "Infragistics.Framework.Controls";
            var shapefilePath = "/" + assembly + ";component/shapefiles/";
            var shapefileName = FileName;

            if (_Shapefile == null)
            {
                _Shapefile = new ShapefileConverter();
                _Shapefile.ImportCompleted += OnShapefileImported;
            }
            _Shapefile.ShapefileSource = new Uri(shapefilePath + shapefileName + ".shp", UriKind.RelativeOrAbsolute);
            _Shapefile.DatabaseSource  = new Uri(shapefilePath + shapefileName + ".dbf", UriKind.RelativeOrAbsolute);

        }
        public virtual void OnShapefileImported(object sender, AsyncCompletedEventArgs e)
        {  
            var models = new List<ShapefileModel>();
            foreach (var record in Shapefile)
            {
                var model = new ShapefileModel();
                model.Record = record;
                models.Add(model);
            }
            Models = models;

            OnPropertyChanged("Shapefile");
        } 
    }

    public class ShapefileModel : ObservableModel
    {
        private List<Point> _Locations;
        /// <summary> Gets or sets Locations </summary>
        public List<Point> Locations
        {
            get { return _Locations; }
            set { if (_Locations == value) return; _Locations = value; OnPropertyChanged("Locations"); }
        }

        private ShapefileRecord _Record;
        /// <summary> Gets or sets Record </summary>
        public ShapefileRecord Record
        {
            get { return _Record; }
            set { if (_Record == value) return; _Record = value; OnPropertyChanged("Record"); }
        }
         
        public ShapefileModel()
        {
            Locations = new List<Point>(); 
        }

        protected override void OnPropertyChanged(string propName)
        {
            base.OnPropertyChanged(propName);
            if (propName == "Record")
            {
                var locations = new List<Point>();
                if (Record != null)
                { 
                    foreach (var shape in Record.Points)
                    {
                        locations.AddRange(shape);
                        locations.Add(new Point(double.NaN, double.NaN));
                    }
                }
                this.Locations = locations;
            }
        }
        

    }

}