using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace IGDataGrid.Controls
{
    /// <summary>
    /// Default root element of a Page, subclasses of which provide sample-specific
    /// properties that expose localized resources.  This is necessary because
    /// Blend only works properly with bindings, not x:Static references.  If Blend
    /// worked properly (i.e. didn't destroy x:Static refs in certain situations),
    /// we could just use x:Static refs against the resx-classes.
    /// </summary>
    public class LocalizationRoot : UserControl
    {
        public LocalizationRoot()
        {
            // this.Loaded += this.OnLoaded;
        }

        void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                Debug.Assert(
                    base.Parent == null
                     || base.Parent.GetType().Namespace.StartsWith("Infragistics.Samples.Browser"),
                    "A LocalizationRoot must be the Content (i.e. root element) of a sample's Page/UserControl.");
            }
        }
    }
}
