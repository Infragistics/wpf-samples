using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.Windows.Resources;
using System.Xml;
using System.Xml.Serialization;
using System.Windows;

namespace IGExtensions.Common.Models
{
    /// <summary>
    /// Data model for all the continents
    /// </summary>
    public sealed class ContinentsList : List<Continent>
    {
        #region Public Methods
        /// <summary>
        /// Load Continents from xml file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public static ContinentsList Load2(string fileName)
        {
            if (String.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException("FileName Is Missing.");
            }

            try
            {
                XmlReader xmlReader = XmlReader.Create(fileName);
                var xmlSer = new XmlSerializer(typeof(ContinentsList));
                var list = (ContinentsList)(xmlSer.Deserialize(xmlReader));
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in serialization", ex);
            }
        }

        public static ContinentsList Load(string fileName)
        {
            if (String.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException("FileName Is Missing:" + fileName);
            }

            try
            {
                Stream fileStream;
                var uri = new Uri(fileName, UriKind.Relative);
                StreamResourceInfo info = Application.GetResourceStream(uri);
                fileStream = info.Stream;
           
                XmlReader xmlReader = XmlReader.Create(fileStream);
                var xmlSer = new XmlSerializer(typeof(ContinentsList));
                var list = (ContinentsList)(xmlSer.Deserialize(xmlReader));
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in serialization", ex);
            }
        }
 
        /// <summary>
        /// Check what is the region of point.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>Name of the continent where is the point</returns>
        public string FindRegion(Point point)
        {
            var continent = this.FirstOrDefault(y => y.Poligons.Any(x => PointInPolygon(x, point)));
            return continent != null ? continent.Name : null;
        }

        /// <summary>
        /// Check if point is in polygon.
        /// </summary>
        /// <param name="poligon">The polygon.</param>
        /// <param name="point">The point.</param>
        /// <returns>True if point is in polygon, otherwise false.</returns>
        private static bool PointInPolygon(List<Coordinates> poligon, Point point)
        {
            var count = poligon.Count;
            var prevPoligonPoint = poligon[count - 1];
            var oddNodes = false;
            foreach (var p in poligon)
            {
                if (p.Y < point.Y != prevPoligonPoint.Y < point.Y)
                {
                    if (p.X +
                        (prevPoligonPoint.X - p.X) *
                        ((point.Y - p.Y) / (prevPoligonPoint.Y - p.Y)) < point.X)
                    {
                        oddNodes = !oddNodes;
                    }
                }
                prevPoligonPoint = p;
            }

            return oddNodes;
        }
        #endregion Public Methods
    }
}