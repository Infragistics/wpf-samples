using Infragistics.Documents.RichText;
using Prism.Events;

namespace IgWord
{
    public class FileOpenedEvent : PubSubEvent<string> { }
    public class FileSavedEvent : PubSubEvent<string> { }
    public class NewDocumentEvent : PubSubEvent<string> { }

    public class CharacterSettingsUpdatedEvent : PubSubEvent<CharacterSettings> { }
    public class ParagraphSettingsUpdatedEvent : PubSubEvent<ParagraphSettings> { }
}
