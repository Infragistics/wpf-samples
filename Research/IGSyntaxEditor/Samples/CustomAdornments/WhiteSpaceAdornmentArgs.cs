using System;

namespace IGSyntaxEditor.Samples.CustomAdornments
{
    public delegate void HighlightingChangedEventHandler(object sender, EventArgs e);

    //This class will be used to transfer (configurable) information between the host application and the adornment.

    public class WhiteSpaceAdornmentArgumentsProvider
    {
        private bool _markSpaces = true;
        private bool _markTabs = true;

        public event HighlightingChangedEventHandler HighlightingChanged;

        public bool MarkSpaces
        {
            get { return _markSpaces; }
            set
            {
                if (_markSpaces != value)
                {
                    _markSpaces = value;
                    if (HighlightingChanged != null)
                    {
                        HighlightingChanged(this, null);
                    }
                }
            }
        }

        public bool MarkTabs
        {
            get { return _markTabs; }
            set
            {
                if (_markTabs != value)
                {
                    _markTabs = value;
                    if (HighlightingChanged != null)
                    {
                        HighlightingChanged(this, null);
                    }
                }
            }
        }

    }
}
