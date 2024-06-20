using System.Windows.Controls;
using Infragistics.Controls.Editors;

namespace IGSyntaxEditor.Samples.CustomMargins
{
    public class CustomMarginLeftPresenter : Control
    {
        private DocumentViewBase documentViewBase;
        private CustomMarginLeft margin;

        public CustomMarginLeftPresenter(DocumentViewBase dvb, CustomMarginLeft m)
        {
            this.documentViewBase = dvb;
            this.margin = m;
        }
    }
}
