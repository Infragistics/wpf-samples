using System.Windows;
using System.Windows.Controls;

namespace IGDockManager.CustomControls
{
    public class XdmSampleDocumentContent : ContentControl
    {
        public XdmSampleDocumentContent()
        {
        }

        static XdmSampleDocumentContent()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(XdmSampleDocumentContent), new FrameworkPropertyMetadata(typeof(XdmSampleDocumentContent)));
        }
    }
}
