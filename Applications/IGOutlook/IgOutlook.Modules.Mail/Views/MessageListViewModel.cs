using IgOutlook.Business.Core;
using IgOutlook.Business.Mail;
using IgOutlook.Infrastructure;
using IgOutlook.Infrastructure.Dialogs;
using IgOutlook.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace IgOutlook.Modules.Mail.Views
{
    public class MessageListViewModel : MessageViewModelBase
    {
        #region Properties

        private Dictionary<string, Business.Contacts.Contact> contacts;
        private ViewItemsCountChangedEvent viewItemsCountChangedEvent;

        private string searchMailText;
        public string SearchMailText
        {
            get { return searchMailText; }
            set { searchMailText = value; OnPropertyChanged(); ClearSearchMailTextCommand.RaiseCanExecuteChanged(); }
        }

        private ObservableCollection<MailMessage> messages;
        public ObservableCollection<MailMessage> Messages
        {
            get { return messages; }
            set
            {
                messages = value;
                if (value != null)
                {
                    LinkEmailContacts();
                    viewItemsCountChangedEvent.Publish(messages.Count);

                    if (Message == null && value.Count > 0)
                        Message = value[0];
                }
                OnPropertyChanged();
            }
        }

        private ObservableCollection<FieldSortOption> fieldSortOptions;
        public ObservableCollection<FieldSortOption> FieldSortOptions
        {
            get { return fieldSortOptions; }
            set { fieldSortOptions = value; OnPropertyChanged(); }
        }

        private FieldSortOption activeFieldSortOption;
        public FieldSortOption ActiveFieldSortOption
        {
            get { return activeFieldSortOption; }
            set { activeFieldSortOption = value; OnPropertyChanged(); }
        }

        private string searchMailNullText;

        public string SearchMailNullText
        {
            get { return searchMailNullText; }
            set { searchMailNullText = value; OnPropertyChanged(); }
        }
        #endregion //Properties

        #region Commands

        public DelegateCommand ToggleMailSortingCommand { get; private set; }
        public DelegateCommand<string> GroupByFieldCommand { get; private set; }
        public DelegateCommand ClearSearchMailTextCommand { get; private set; }

        #endregion //Commands

        #region Constructors

        public MessageListViewModel(IEventAggregator eventAggregator, IMailService mailService, IDialogService dialogService, ICategoryService categoryService, IContactService contactService)
            : base(eventAggregator, mailService, dialogService, categoryService, contactService)
        {

            contacts = ContactService.GetContactsAsDictionary();

            eventAggregator.GetEvent<MessageSentEvent>().Subscribe(MessageSent);
            eventAggregator.GetEvent<MessageDeletedEvent>().Subscribe(MessageDeleted);


            ToggleMailSortingCommand = new DelegateCommand(ToggleMailSorting);
            GroupByFieldCommand = new DelegateCommand<string>(GroupByField);
            ClearSearchMailTextCommand = new DelegateCommand(ClearSearchMailText, () => { return !string.IsNullOrEmpty(searchMailText); });

            InitFieldSortOptions();

            SetActiveFieldSortOption(FieldSortOptions[0].FieldName);

            viewItemsCountChangedEvent = EventAggregator.GetEvent<ViewItemsCountChangedEvent>();
        }


        #endregion //Constructors

        #region Base Class Overrides

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            LoadMailbox(CurrentMailbox);

            Title = String.Format("{0}", FolderParameters.LocalizedFolderNames[CurrentMailbox]);
            EventAggregator.GetEvent<ViewActivateEvent>().Publish(Title);

            SearchMailNullText = Resources.ResourceStrings.Search_Text + " " + FolderParameters.LocalizedFolderNames[CurrentMailbox];
        }

        public override bool IsNavigationTarget(NavigationContext navigationContext)
        {
            Messages = null;
            return base.IsNavigationTarget(navigationContext);
        }

        protected override void DeleteMessage()
        {
            base.DeleteMessage();
            MessageDeleted(Message);
        }

        protected override void OnMessageChanged()
        {
            base.OnMessageChanged();
            LoadMessageBody();
        }

        #endregion //Base Class Overrides

        #region Methods

        private void LoadMailbox(string mailbox)
        {
            if (mailbox == FolderParameters.Inbox)
                LoadInbox();
            if (mailbox == FolderParameters.Sent)
                LoadSentItems();
            if (mailbox == FolderParameters.Drafts)
                LoadDraftItems();
            if (mailbox == FolderParameters.Deleted)
                LoadDeletedItems();
        }

        async void LoadInbox()
        {
            Messages = new ObservableCollection<MailMessage>(await MailService.GetInboxItemsAsync());
        }

        async void LoadSentItems()
        {
            Messages = new ObservableCollection<MailMessage>(await MailService.GetSentItemsAsync());
        }

        async void LoadDraftItems()
        {
            Messages = new ObservableCollection<MailMessage>(await MailService.GetDraftItemsAsync());
        }

        async void LoadDeletedItems()
        {
            Messages = new ObservableCollection<MailMessage>(await MailService.GetDeletedItemsAsync());
        }

        void LoadMessageBody()
        {
            if (Message != null)
            {
                Message.MarkAsRead();
            }
        }

        public void MessageSent(MailMessage message)
        {
            if (CurrentMailbox == FolderParameters.Sent)
            {
                Messages.Add(message);
                viewItemsCountChangedEvent.Publish(messages.Count);
                LinkEmailContacts();
            }
        }

        public void MessageDeleted(MailMessage message)
        {
            if (Messages.Contains(message))
            {
                Messages.Remove(message);
                viewItemsCountChangedEvent.Publish(messages.Count);
            }
        }

        private void ToggleMailSorting()
        {
            ActiveFieldSortOption.ActiveSortDirection = ActiveFieldSortOption.ActiveSortDirection == ListSortDirection.Ascending ? ListSortDirection.Descending : ListSortDirection.Ascending;
        }

        private void InitFieldSortOptions()
        {
            FieldSortOptions = new ObservableCollection<FieldSortOption>();
            FieldSortOptions.Add(new FieldSortOption("DateSent", Resources.ResourceStrings.Date_Text, Resources.ResourceStrings.Oldest_Text, Resources.ResourceStrings.Newest_Text));
            FieldSortOptions.Add(new FieldSortOption("From", Resources.ResourceStrings.From_Text, Resources.ResourceStrings.AtoZ_Text, Resources.ResourceStrings.ZtoA_Text));
            FieldSortOptions.Add(new FieldSortOption("Subject", Resources.ResourceStrings.Subject_Text, Resources.ResourceStrings.AtoZ_Text, Resources.ResourceStrings.ZtoA_Text));
            FieldSortOptions.Add(new FieldSortOption("Importance", Resources.ResourceStrings.Importance_Text, Resources.ResourceStrings.High_Text, Resources.ResourceStrings.Low_Text));
        }

        private void GroupByField(string fieldName)
        {
            SetActiveFieldSortOption(fieldName);
        }

        private void SetActiveFieldSortOption(string fieldName)
        {
            ActiveFieldSortOption = FieldSortOptions.First(f => f.FieldName == fieldName);

            foreach (var fieldSortOption in FieldSortOptions)
            {
                fieldSortOption.IsActive = false;
            }
            ActiveFieldSortOption.IsActive = true;
        }

        private void LinkEmailContacts()
        {
            foreach (var message in messages)
            {
                if (contacts.ContainsKey(message.From))
                    message.Contact = contacts[message.From];
            }
        }

        private void ClearSearchMailText()
        {
            SearchMailText = string.Empty;
        }

        #endregion //Methods
    }

    #region FieldSortOption Class

    public class FieldSortOption : BusinessBase
    {
        private bool isActive;
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; NotifyPropertyChanged(); }
        }

        private string label;
        public string Label
        {
            get { return label; }
            set { label = value; NotifyPropertyChanged(); }
        }

        private string fieldName;
        public string FieldName
        {
            get { return fieldName; }
            set { fieldName = value; NotifyPropertyChanged(); }
        }

        private ListSortDirection activeSortDirection;
        public ListSortDirection ActiveSortDirection
        {
            get { return activeSortDirection; }
            set { activeSortDirection = value; NotifyPropertyChanged(); NotifyPropertyChanged("ActiveSortDescription"); }
        }

        public string ActiveSortDescription
        {
            get { return SortOptions[activeSortDirection]; }
        }

        public Dictionary<ListSortDirection, string> SortOptions { get; set; }

        public FieldSortOption(string fieldName, string label, string ascendingOption, string descendingOption)
        {
            FieldName = fieldName;
            Label = label;
            SortOptions = new Dictionary<ListSortDirection, string>();
            SortOptions.Add(ListSortDirection.Ascending, ascendingOption);
            SortOptions.Add(ListSortDirection.Descending, descendingOption);
            ActiveSortDirection = ListSortDirection.Descending;
        }

        public FieldSortOption(string fieldName, string ascendingOption, string descendingOption)
        {
            FieldName = fieldName;
            Label = fieldName;
            SortOptions = new Dictionary<ListSortDirection, string>();
            SortOptions.Add(ListSortDirection.Ascending, ascendingOption);
            SortOptions.Add(ListSortDirection.Descending, descendingOption);
            ActiveSortDirection = ListSortDirection.Descending;
        }
    }

    #endregion //FieldSortOption Class

}
