using System;
using System.Collections.Generic; 
using IGSurfaceChart.Samples.Models; 

namespace IGSurfaceChart.Samples 
{
    /// <summary>
    /// Represents 3D data loaded from a shapefile and then flattened to a list of <see cref="DataPoint3D"/> 
    /// </summary>
    /// <remarks>Use only with small shapefiles (e.g. points count less than 1000 </remarks>
    public class ShapefileFlattner : ShapefileBase
    {
        /// <summary> Gets 3D Data Points </summary>
        public List<DataPoint3D> Data { get; set; }

        public ShapefileFlattner()
        {
            Data = new List<DataPoint3D>();
             
        }
        public override void UpdateData()
        {
            if (string.IsNullOrEmpty(ZMemberPath)) return;

            var locations = new Dictionary<string, bool>();
            var total = 0;
             
            // flatten shapefile  
            foreach (var record in this)
            {
                var z = double.Parse(record.Fields[ZMemberPath].ToString());
                 
                foreach (var shape in record.Points)
                {
                    foreach (var item in shape)
                    {
                        total++;
                        
                        var point = new DataPoint3D();
                        point.Z = z;
                        point.X = item.X;
                        point.Y = item.Y;
                        var loc = point.Location.ToString();

                        // skip duplicated points
                        if (!locations.ContainsKey(loc))
                        {
                            locations.Add(loc, true);
                            this.Data.Add(point);
                        } 
                    }
                }
            }
            OnPropertyChanged("Data");
        }

         


    }
}