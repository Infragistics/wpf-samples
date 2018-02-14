using IgOutlook.Infrastructure.Dialogs;
using IgOutlook.Modules.Mail.Resources;
using IgOutlook.Services;
using Prism.Events;
using Prism.Regions;
using System;

namespace IgOutlook.Modules.Mail.Views
{
    public class MessageReadOnlyViewModel : MessageViewModelBase, IDialogAware
    {
        string _messageId;

        #region Constructors

        public MessageReadOnlyViewModel(IEventAggregator eventAggregator, IMailService mailService, IDialogService dialogService, ICategoryService categoryService, IContactService contactService)
            : base(eventAggregator, mailService, dialogService, categoryService, contactService)
        { }

        #endregion //Constructors

        #region Base Class Overrides

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            _messageId = navigationContext.Parameters[MessageParameters.MessageId] as string;

            LoadMessage(_messageId);
        }

        protected override void DeleteMessage()
        {
            base.DeleteMessage();
            RequestClose();
            EventAggregator.GetEvent<MessageDeletedEvent>().Publish(Message);
        }

        #endregion //Base Class Overrides

        #region Methods

        async void LoadMessage(string messageId)
        {
            Message = await MailService.GetMessageByIdAsync(messageId);

            Title = Message.Subject + " - " + ResourceStrings.Message_Text;
        }

        #endregion //Methods

        #region IDialogAware Members

        public bool CanCloseDialog()
        {
            return true;
        }

        public event Action RequestClose;
        void CloseDialog()
        {
            if (RequestClose != null)
            {
                RequestClose();
            }
        }

        #endregion //IDialogAware Members
    }
}
