using IgOutlook.Business.Calendar;
using IgOutlook.Business.Contacts;
using IgOutlook.Business.Mail;
using IgOutlook.Infrastructure;
using IgOutlook.Infrastructure.Dialogs;
using IgOutlook.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace IgOutlook.Modules.Mail
{
    public class MessageViewModelBase : NavigationAwareViewModelBase
    {
        #region Properties

        protected IEventAggregator EventAggregator { get; private set; }
        protected IMailService MailService { get; private set; }
        protected IDialogService DialogService { get; private set; }
        protected ICategoryService CategoryService { get; private set; }
        protected IContactService ContactService { get; private set; }

        string _currentMailbox;
        public string CurrentMailbox
        {
            get { return _currentMailbox; }
            set
            {
                _currentMailbox = value;
                OnPropertyChanged("CurrentMailBox");
            }
        }

        MailMessage _message;
        public MailMessage Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnMessageChanged();
            }
        }

        private ObservableCollection<ActivityCategory> _categories;
        public ObservableCollection<ActivityCategory> Categories
        {
            get { return _categories; }
            set { _categories = value; OnPropertyChanged(); }
        }


        private ObservableCollection<Contact> mailParticipants;
        public ObservableCollection<Contact> MailParticipants
        {
            get { return mailParticipants; }
            set { mailParticipants = value; OnPropertyChanged(); }
        }

        public DelegateCommand<string> MessageCommand { get; private set; }
        public DelegateCommand DeleteCommand { get; private set; }
        public DelegateCommand ReadCommand { get; private set; }
        public DelegateCommand<string> ChangeCategoryCommand { get; private set; }
        public DelegateCommand FollowUpCommand { get; private set; }


        #endregion //Properties

        #region Constructors

        public MessageViewModelBase(IEventAggregator eventAggragator, IMailService mailService, IDialogService dialogService, ICategoryService categoryService, IContactService contactService)
        {
            EventAggregator = eventAggragator;
            MailService = mailService;
            DialogService = dialogService;
            CategoryService = categoryService;
            ContactService = contactService;

            Categories = categoryService.GetCategories();

            MessageCommand = new DelegateCommand<string>(ExecuteMessageCommand, CanExecuteMessageCommand);
            DeleteCommand = new DelegateCommand(DeleteMessage, HasValidMessage);
            ReadCommand = new DelegateCommand(MarkMessageAsRead, HasValidMessage);
            ChangeCategoryCommand = new DelegateCommand<string>(ChangeMessageCategory, CanExecuteMessageCommand);
            FollowUpCommand = new DelegateCommand(ChangeMessageFollowUp, HasValidMessage);

            IconSource = "pack://application:,,,/IgOutlook.Infrastructure;component/Images/Mail.ico";
        }

        #endregion //Constructors

        #region Commands

        protected virtual void ExecuteMessageCommand(string action)
        {
            string view = "MessageView";
            NavigationParameters parameters = null;

            if (action.Equals(MessageParameters.New))
                parameters = CreateMessageParameters(MessageParameters.New);
            else if (action.Equals(MessageParameters.Reply))
                parameters = CreateMessageParameters(MessageParameters.Reply);
            else if (action.Equals(MessageParameters.ReplyAll))
                parameters = CreateMessageParameters(MessageParameters.ReplyAll);
            else if (action.Equals(MessageParameters.Forward))
                parameters = CreateMessageParameters(MessageParameters.Forward);
            else if (action.Equals(MessageParameters.View))
            {
                view = "MessageReadOnlyView";
                parameters = CreateMessageParameters(MessageParameters.View);
            }

            DialogService.ShowRibbonDialog(view, parameters);
        }

        protected virtual bool CanExecuteMessageCommand(string action)
        {

            if (!string.IsNullOrEmpty(action) && action.Equals(MessageParameters.New))
                return true;

            return HasValidMessage();
        }

        protected virtual void DeleteMessage()
        {
            bool deletePermanently = false;

            //if we are deleting from the deleted folder, permanently delete it
            if (CurrentMailbox.Equals(FolderParameters.Deleted))
                deletePermanently = true;

            MailService.DeleteMessage(Message, deletePermanently);
        }

        protected virtual void MarkMessageAsRead()
        {
            if (Message.IsRead)
                Message.MarkAsUnread();
            else
                Message.MarkAsRead();
        }

        protected virtual void ChangeMessageCategory(string categoryName)
        {
            if (string.IsNullOrEmpty(categoryName))
                return;

            if (Message.Category != null)
            {
                if (Message.Category.CategoryName == categoryName)
                    Message.Category = null;
                else
                    Message.Category = Categories.First(c => c.CategoryName == categoryName);
            }
            else
            {
                Message.Category = Categories.First(c => c.CategoryName == categoryName);
            }
        }

        protected virtual void ChangeMessageFollowUp()
        {
            if (Message.IsFlagged)
                Message.RemoveFlag();
            else
                Message.Flag();
        }

        #endregion //Commands

        #region Base Class Overrides

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            CurrentMailbox = navigationContext.Parameters[FolderParameters.FolderKey] as string;
        }

        #endregion //Base Class Overrides

        #region Methods

        protected virtual NavigationParameters CreateMessageParameters(string messageMode)
        {
            NavigationParameters parameters = new NavigationParameters();
            parameters.Add(MessageParameters.MessageModeKey, messageMode);
            parameters.Add(FolderParameters.FolderKey, CurrentMailbox);

            var sourceMessageId = "NEW";
            if (!messageMode.Equals(MessageParameters.New) && HasValidMessage())
            {
                sourceMessageId = Message.Id;
            }

            parameters.Add(MessageParameters.MessageId, sourceMessageId);
            return parameters;
        }

        protected virtual bool HasValidMessage()
        {
            return Message != null;
        }

        protected virtual void OnMessageChanged()
        {
            OnPropertyChanged("Message");
            RaiseCanExecuteEvents();
            UpdateMailParticipantsList();
        }

        public void UpdateMailParticipantsList()
        {
            if (Message != null)
            {
                var participants = new List<string>();

                participants.Add(Message.From);

                if (Message.To != null)
                    participants.AddRange(Message.To.Select(s => s).ToList());

                if (Message.Cc != null)
                    participants.AddRange(Message.Cc.Select(s => s).ToList());

                MailParticipants = ContactService.GetContactFromEmails(participants);
            }
        }

        protected virtual void RaiseCanExecuteEvents()
        {
            MessageCommand.RaiseCanExecuteChanged();
            DeleteCommand.RaiseCanExecuteChanged();
            ReadCommand.RaiseCanExecuteChanged();
            ChangeCategoryCommand.RaiseCanExecuteChanged();
            FollowUpCommand.RaiseCanExecuteChanged();
        }

        #endregion //Methods
    }
}
