using IgOutlook.Business.Mail;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IgOutlook.Services
{
    public interface IMailService
    {
        IList<MailMessage> GetInboxItems();
        Task<IList<MailMessage>> GetInboxItemsAsync();

        IList<MailMessage> GetSentItems();
        Task<IList<MailMessage>> GetSentItemsAsync();

        IList<MailMessage> GetDeletedItems();
        Task<IList<MailMessage>> GetDeletedItemsAsync();

        IList<MailMessage> GetDraftItems();
        Task<IList<MailMessage>> GetDraftItemsAsync();

        Task<MailMessage> GetMessageByIdAsync(string messageId);

        void ReplyToMessage(MailMessage message, string sourceMessageId);
        void ForwardMessage(MailMessage message, string sourceMessageId);

        void SendMessage(MailMessage message);

        void DeleteMessage(MailMessage message, bool permanent = false);
    }
}
