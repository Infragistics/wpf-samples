using IgOutlook.Business.Contacts;
using Prism.Events;

namespace IgOutlook.Modules.Contacts
{
    public class ContactDeletedEvent : PubSubEvent<Contact> { }
    public class ContactUpdatedEvent : PubSubEvent<Contact> { }
}
