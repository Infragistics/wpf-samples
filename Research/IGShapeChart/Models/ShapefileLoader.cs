using Infragistics.Controls.Maps;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace IGShapeChart.Samples
{ 
    public class ShapefileLoader : List<ShapefileRecord>, INotifyPropertyChanged
    {   
        private ShapefileConverter Shapefile;
        private void LoadShapes()
        {
            if (Shapefile == null)
            {
                Shapefile = new ShapefileConverter();
                Shapefile.ImportCompleted += (s, e) => { UpdateShapes(); }; 
            }

            if (!string.IsNullOrEmpty(FilePath))
            {
                var path = FilePath.Replace(".shp","").Replace(".dbf",""); 
                 
                Shapefile.ShapefileSource = new Uri(path + ".shp", UriKind.RelativeOrAbsolute);
                Shapefile.DatabaseSource  = new Uri(path + ".dbf", UriKind.RelativeOrAbsolute);
            }
         }

        private void UpdateShapes()
        {
            this.Clear();
            if (this.Shapefile == null) return;

            var newRecrods = new List<ShapefileRecord>();

            if (this.FilterValue == null || string.IsNullOrEmpty(this.FilterValue.ToString()))
            {
                newRecrods.AddRange(this.Shapefile);
            }
            else
            { 
                foreach (var record in this.Shapefile)
                { 
                    foreach (var field in record.Fields)
                    {
                        if (field.Equals(this.FilterValue))
                        {
                            newRecrods.Add(record);
                            break;
                        }
                    } 
                }
            }

            if (!double.IsNaN(this.OffsetX) ||
                !double.IsNaN(this.OffsetY))
            {
                foreach (var record in newRecrods)
                { 
                    record.Points.OffsetBy(this.OffsetX, this.OffsetY); 
                }
            }
            if (this.SwapXY)
            {
                foreach (var record in newRecrods)
                { 
                    record.Points.SwapXY(); 
                }
            }
            this.AddRange(newRecrods); 
        }
         
        private string _FilePath; 
        public string FilePath
        {
            get { return _FilePath; }
            set { if (_FilePath == value) return; _FilePath = value; LoadShapes(); OnPropertyChanged("FilePath"); }
        }

        private object _FilterValue; 
        public object FilterValue
        {
            get { return _FilterValue; }
            set { if (_FilterValue == value) return; _FilterValue = value; UpdateShapes(); OnPropertyChanged("FilterValue"); }
        }

        private double _OffsetX = double.NaN; 
        public double OffsetX
        {
            get { return _OffsetX; }
            set { if (_OffsetX == value) return; _OffsetX = value; UpdateShapes(); OnPropertyChanged("OffsetX"); }
        }

        private double _OffsetY = double.NaN; 
        public double OffsetY
        {
            get { return _OffsetY; }
            set { if (_OffsetY == value) return; _OffsetY = value; UpdateShapes(); OnPropertyChanged("OffsetY"); }
        }

        private bool _SwapXY = false; 
        public bool SwapXY
        {
            get { return _SwapXY; }
            set { if (_SwapXY == value) return; _SwapXY = value; UpdateShapes(); OnPropertyChanged("SwapXY"); }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = "")
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName)); 
        }

    }

    public static class ShapeExtensions
    {
        public static void OffsetBy(this List<List<Point>> points, double offsetX, double offsetY)
        {
            foreach (var shape in points)
            {
                for (int i = 0; i < shape.Count; i++)
                {
                    var x = shape[i].X;
                    var y = shape[i].Y;
                    if (!double.IsNaN(offsetX))
                        x += offsetX;
                    if (!double.IsNaN(offsetY))
                        y += offsetY;

                    shape[i] = new Point(x, y);
                }
            }
        }
        public static void SwapXY(this List<List<Point>> points)
        {
            foreach (var shape in points)
            {
                for (int i = 0; i < shape.Count; i++)
                {
                    var x = shape[i].X;
                    var y = shape[i].Y;  
                    shape[i] = new Point(y, x);
                }
            }
        }
    }
    
}
