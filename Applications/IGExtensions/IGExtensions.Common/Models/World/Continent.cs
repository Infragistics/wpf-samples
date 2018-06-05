using System.Xml.Serialization;
using System.Collections.Generic;

namespace IGExtensions.Common.Models
{
    /// <summary>
    /// Data model of Continent business object.
    /// </summary>
    public sealed class Continent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Continent"/> class.
        /// </summary>
        public Continent()
        {
            Poligons = new List<List<Coordinates>>();
        }
        #endregion Constructors

        #region Public Members
        /// <summary>
        /// Gets or sets the name of the continent.
        /// </summary>
        /// <value>The name.</value>
        [XmlAttribute("Name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the poligons(continent can contais more nhan one poligon).
        /// </summary>
        /// <value>The poligons.</value>
        [XmlArray("Poligons")]
        [XmlArrayItem("Poligon")]
        public List<List<Coordinates>> Poligons { get; set; }

        public void GetBoundingBox(out double minLon, out double minLat, out double maxLon, out double maxLat)
        {
            minLon = double.MaxValue;
            minLat = double.MaxValue;
            maxLon = double.MinValue;
            maxLat = double.MinValue;
            foreach (List<Coordinates> poligon in Poligons)
            {
                foreach (Coordinates coordinates in poligon)
                {
                    if (coordinates.X < minLon)
                    {
                        minLon = coordinates.X;
                    }
                    if (coordinates.Y < minLat)
                    {
                        minLat = coordinates.Y;
                    }
                    if (coordinates.X > maxLon)
                    {
                        maxLon = coordinates.X;
                    }
                    if (coordinates.Y > maxLat)
                    {
                        maxLat = coordinates.Y;
                    }
                }
            }
        }
        #endregion Public Members
    }
}