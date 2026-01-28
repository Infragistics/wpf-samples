using System.Collections.Generic;
using System.Windows;

namespace Infragistics.Samples.Shared.Models.Navigation
{
    public class GeoPath : List<Point>
    {

        public List<List<Point>> ToPointList()
        {
            var path = new List<List<Point>> { this };
            return path;
        }

    }
}