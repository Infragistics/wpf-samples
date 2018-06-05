namespace IGExtensions.Common.Editors.Assets.Resources
{
    /// <summary>
    /// Represents common assets localizer that provides access to the <see cref="EditorStrings"/> 
    /// </summary>
    public class EditorLocalizer
    {
        private static readonly EditorStrings CommonStringAssets = new EditorStrings();
        
        /// <summary>
        /// Gets common strings resource 
        /// </summary>
        public EditorStrings EditorStrings
		{
            get { return CommonStringAssets; }
           // get { return new IGExtensions.Common.Assets.Resources.CommonStrings(); }
		}

 
    }
}
