using IgOutlook.Business.Mail;
using Prism.Events;

namespace IgOutlook.Modules.Mail
{
    public class MessageSentEvent : PubSubEvent<MailMessage> { }
    public class MessageDeletedEvent : PubSubEvent<MailMessage> { }
}
