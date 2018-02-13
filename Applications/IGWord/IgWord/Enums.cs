using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IgWord
{
    public enum RichTextAdapterRefreshTrigger
    {
        ContentChanged,
        Explicit,
        Delayed
    }

    public enum IgWordDocumentStatus
    {
        NoDocumentLoaded,
        ExisitngNotModified,
        ExistingModified,
        BlankNotModified,
        BlankModified,
        TemplateNotModified,
        //DocumentLoadedNotModified,
        //DocumentLoadedModified,
        //NewNotSaved,
        //NewEmpty,
       // PendingSaveAndClose
    }

    public enum HyperlinkDialogMode
    {
        InsertingHyperlink,
        EditingHyperlink,
        MakingHyperlink
    }
}
