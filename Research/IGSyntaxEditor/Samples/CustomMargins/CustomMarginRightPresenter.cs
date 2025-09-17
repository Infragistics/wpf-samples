using System.Windows.Controls;
using Infragistics.Controls.Editors;

namespace IGSyntaxEditor.Samples.CustomMargins
{
    public class CustomMarginRightPresenter : Control
    {
        private DocumentViewBase documentViewBase;
        private CustomMarginRight margin;

        public CustomMarginRightPresenter(DocumentViewBase dvb, CustomMarginRight m)
        {
            this.documentViewBase = dvb;
            this.margin = m;
        }
    }
}
