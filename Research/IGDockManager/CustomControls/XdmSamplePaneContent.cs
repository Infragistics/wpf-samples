using System.Windows;
using System.Windows.Controls;

namespace IGDockManager.CustomControls
{
    public class XdmSamplePaneContent : ContentControl
    {
        public XdmSamplePaneContent()
        {
        }

        static XdmSamplePaneContent()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(XdmSamplePaneContent), new FrameworkPropertyMetadata(typeof(XdmSamplePaneContent)));
        }
    }
}
