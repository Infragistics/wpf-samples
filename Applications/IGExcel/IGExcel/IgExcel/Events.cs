using Infragistics.Documents.Excel;
using Prism.Events;

namespace IgExcel
{
    public class FileOpenedEvent : PubSubEvent<string> { }
    public class FileSavedEvent : PubSubEvent<string> { }
    public class NewDocumentEvent : PubSubEvent<string> { }
    public class TableAddedEvent : PubSubEvent<WorksheetTable> { }
    public class CustomZoomLevelChangedEvent : PubSubEvent<int> { }
    public class PasswordToOpenEnteredEvent : PubSubEvent<string> { }
    public class PasswordToModifyEnteredEvent : PubSubEvent<WriteProtectedFileMode> { }
}
