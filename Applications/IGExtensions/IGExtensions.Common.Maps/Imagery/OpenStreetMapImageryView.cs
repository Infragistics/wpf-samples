using IGExtensions.Common.Maps.Assets.Resources;

namespace IGExtensions.Common.Maps.Imagery // Infragistics.Controls.Maps 
{
    #region OpenStreetMap Imagery View

    /// <summary>
    /// Represents a map view for the OpenStreetMap geo-imagery.
    /// </summary>
    public class OpenStreetMapImageryView : GeoImageryViewModel
    {
        public OpenStreetMapImageryView()
        {
            this.ImagerySource = GeoImagerySource.OpenStreetMapImagery;
            this.ImageryDisplayName = ImageryStrings.Imagery_OpenStreetMap;  
        }
        /// <summary>
        /// Gets the style name of the OpenStreetMap geo-imagery. 
        /// </summary>
        public override string ImageryName { get { return this.ImagerySource.ToString(); } }
        public override string ImageryFileName { get { return ImageryName + ".png"; } }
    }
    #endregion
}