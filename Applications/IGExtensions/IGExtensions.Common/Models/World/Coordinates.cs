using System.Xml.Serialization;

namespace IGExtensions.Common.Models
{
    /// <summary>
    /// Coordinates in Geographical system
    /// </summary>
    public sealed class Coordinates
    {
        #region Public Properties
        /// <summary>
        /// Gets or sets the X coordinate.
        /// </summary>
        /// <value>The X.</value>
        [XmlAttribute("X")]
        public double X { get; set; }

        /// <summary>
        /// Gets or sets the Y coordinate.
        /// </summary>
        /// <value>The Y.</value>
        [XmlAttribute("Y")]
        public double Y { get; set; }
        #endregion Public Properties
    }
}