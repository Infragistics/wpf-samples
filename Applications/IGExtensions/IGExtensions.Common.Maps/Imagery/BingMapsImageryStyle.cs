namespace IGExtensions.Common.Maps.Imagery
{
    /// <summary>
    /// Determines style of the geographic imagery tiles from Bing Maps service. 
    /// </summary>
    public enum BingMapsImageryStyle
    {
        /// <summary>
        /// Specifies satellite style of geographic imagery without labels and roads overlay from the from the Bing Maps imagery service
        /// </summary>
        SatelliteNoLabelsMapStyle,
        /// <summary>
        /// Specifies satellite style of geographic imagery from the from the Bing Maps imagery service
        /// </summary>
        SatelliteMapStyle,
        /// <summary>
        /// Specifies the street map style of geographic imagery from the Bing Maps imagery service.
        /// </summary>
        StreetMapStyle,
        #region Not supported Bing Maps styles by the xamGeographicMap control
        ///// <summary>
        ///// Specifies the Bird’s eye (oblique-angle) map style
        ///// </summary>
        //Birdseye,
        ///// <summary>
        ///// Specifies the Bird’s eye map style with road and labels overlay.
        ///// </summary>
        //BirdseyeWithLabels, 
        #endregion
    }


}