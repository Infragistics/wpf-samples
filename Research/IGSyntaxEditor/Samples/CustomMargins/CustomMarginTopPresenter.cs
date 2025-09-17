using System.Windows.Controls;
using Infragistics.Controls.Editors;

namespace IGSyntaxEditor.Samples.CustomMargins
{
    public class CustomMarginTopPresenter : Control
    {
        private DocumentViewBase documentViewBase;
        private CustomMarginTop margin;

        public CustomMarginTopPresenter(DocumentViewBase dvb, CustomMarginTop m)
        {
            this.documentViewBase = dvb;
            this.margin = m;
        }
    }
}
