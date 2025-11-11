using Infragistics.Controls.Maps;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace IGShapeChart.Samples
{
    // Simplified loader: restore original inheritance (List<ShapefileRecord>) so chart series creation works.
    // FilePath can be either a base path without extension or full .shp path.
    public class ShapefileLoader : List<ShapefileRecord>, INotifyPropertyChanged
    {
        private ShapefileConverter _converter;

        private void EnsureConverter()
        {
            if (_converter != null) return;
            _converter = new ShapefileConverter();
            _converter.ImportCompleted += (s, e) => UpdateShapes();
        }

        private void LoadShapes()
        {
            if (string.IsNullOrWhiteSpace(FilePath)) return;
            EnsureConverter();

            string shpPath;
            string dbfPath;

            if (FilePath.EndsWith(".shp", StringComparison.OrdinalIgnoreCase))
            {
                shpPath = FilePath;
                dbfPath = Path.ChangeExtension(FilePath, ".dbf");
            }
            else
            {
                // treat as base path (without extensions)
                shpPath = FilePath + ".shp";
                dbfPath = FilePath + ".dbf";
            }

            // If user passed a component path ("/IGShapeChart;component/...") keep it; if it's an absolute file path add file:// scheme.
            UriKind kind = UriKind.RelativeOrAbsolute;
            if (Path.IsPathRooted(shpPath))
            {
                var fullBase = Path.GetFullPath(shpPath);
                shpPath = fullBase; // will be absolute path
                dbfPath = Path.GetFullPath(dbfPath);
                kind = UriKind.Absolute;
            }

            _converter.ShapefileSource = new Uri(shpPath, kind);
            _converter.DatabaseSource = new Uri(dbfPath, kind);
        }

        private void UpdateShapes()
        {
            this.Clear();
            if (_converter == null) return;

            foreach (var record in _converter)
            {
                // Filter by field value if requested
                if (FilterValue != null && record.Fields != null)
                {
                    bool match = false;
                    foreach (var field in record.Fields)
                    {
                        if (field != null && field.Equals(FilterValue)) { match = true; break; }
                    }
                    if (!match) continue;
                }

                // Apply offsets / swap only on a copy of points
                if (!double.IsNaN(OffsetX) || !double.IsNaN(OffsetY) || SwapXY)
                {
                    for (int i = 0; i < record.Points.Count; i++)
                    {
                        var poly = record.Points[i];
                        for (int p = 0; p < poly.Count; p++)
                        {
                            var pt = poly[p];
                            double x = pt.X;
                            double y = pt.Y;
                            if (!double.IsNaN(OffsetX)) x += OffsetX;
                            if (!double.IsNaN(OffsetY)) y += OffsetY;
                            if (SwapXY)
                            {
                                var tmp = x; x = y; y = tmp;
                            }
                            poly[p] = new System.Windows.Point(x, y);
                        }
                    }
                }

                this.Add(record);
            }
        }

        private string _filePath;
        public string FilePath
        {
            get => _filePath;
            set
            {
                if (_filePath == value) return;
                _filePath = value;
                LoadShapes();
                OnPropertyChanged(nameof(FilePath));
            }
        }

        private object _filterValue;
        public object FilterValue
        {
            get => _filterValue;
            set
            {
                if (Equals(_filterValue, value)) return;
                _filterValue = value;
                UpdateShapes();
                OnPropertyChanged(nameof(FilterValue));
            }
        }

        private double _offsetX = double.NaN;
        public double OffsetX
        {
            get => _offsetX;
            set
            {
                if (_offsetX.Equals(value)) return;
                _offsetX = value;
                UpdateShapes();
                OnPropertyChanged(nameof(OffsetX));
            }
        }

        private double _offsetY = double.NaN;
        public double OffsetY
        {
            get => _offsetY;
            set
            {
                if (_offsetY.Equals(value)) return;
                _offsetY = value;
                UpdateShapes();
                OnPropertyChanged(nameof(OffsetY));
            }
        }

        private bool _swapXY;
        public bool SwapXY
        {
            get => _swapXY;
            set
            {
                if (_swapXY == value) return;
                _swapXY = value;
                UpdateShapes();
                OnPropertyChanged(nameof(SwapXY));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
