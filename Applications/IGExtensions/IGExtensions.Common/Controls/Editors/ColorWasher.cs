#if SILVERLIGHT
using System.Windows;
using Infragistics;
#else // WPF
using System.Collections.ObjectModel;
using System.Windows;
using Infragistics.Windows.Themes;
#endif

namespace IGExtensions.Common.Controls
{
    /// <summary>
    /// Represents an extension for the <see cref="ResourceWasher"/>
    /// <remarks>
    /// SL: http://www.infragistics.com/products/silverlight/resource-washer
    /// WPF http://www.infragistics.com/products/wpf/resource-washer
    /// </remarks>
    /// </summary>
    public class ColorWasher : ResourceWasher
    {
        public void WashResources(ColorWashSettings washSettings, ResourceDictionary resources)
        {
            this.WashColor = washSettings.WashColor;
            this.WashGroups[0].WashColor = washSettings.WashColor;
            this.WashMode = washSettings.WashMode;
            this.SourceDictionary = resources;
            this.WashResources();
        }
        public void WashResources(ColorWashSettings washSettings)
        {
            WashResources(washSettings, Application.Current.Resources);
        }
    }
    /// Represents an extension for the <see cref="WashGroupCollection"/>
    public class ColorWashGroupCollection : WashGroupCollection  
    {
    }

    /// Represents an extension for the <see cref="WashGroup"/>
    public class ColorWashGroup : WashGroup
    {
    }
    
}