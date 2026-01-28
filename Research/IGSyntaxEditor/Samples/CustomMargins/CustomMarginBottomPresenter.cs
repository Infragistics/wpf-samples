using System.Windows.Controls;
using Infragistics.Controls.Editors;

namespace IGSyntaxEditor.Samples.CustomMargins
{
    public class CustomMarginBottomPresenter : Control
    {
        private DocumentViewBase _documentViewBase;
        private CustomMarginBottom _margin;

        public CustomMarginBottomPresenter(DocumentViewBase dvb, CustomMarginBottom m)
        {
            _documentViewBase = dvb;
            _margin = m;
        }
    }
}
