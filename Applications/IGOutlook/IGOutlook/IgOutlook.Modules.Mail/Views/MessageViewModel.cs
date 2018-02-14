using IgOutlook.Business.Contacts;
using IgOutlook.Business.Mail;
using IgOutlook.Infrastructure;
using IgOutlook.Infrastructure.Dialogs;
using IgOutlook.Modules.Mail.Resources;
using IgOutlook.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace IgOutlook.Modules.Mail.Views
{
    public class MessageViewModel : NavigationAwareViewModelBase, IDialogAware
    {
        #region Members

        string _messageMode;
        string _sourceMessageId;
        IEventAggregator _eventAggragtor;
        IMessageBoxService _messageBoxService;
        IMailService _mailService;
        bool _closeRequested;

        #endregion //Members

        #region Properties

        private ObservableCollection<Contact> contacts;
        public ObservableCollection<Contact> Contacts
        {
            get { return contacts; }
            set { contacts = value; OnPropertyChanged(); }
        }

        private string insertedRtf;
        public string InsertedRtf
        {
            get { return insertedRtf; }
            set { insertedRtf = value; OnPropertyChanged(); }
        }

        MailMessage _message;
        public MailMessage Message
        {
            get { return _message; }
            set
            {
                if (_message != null)
                    _message.PropertyChanged -= Message_PropertyChanged;

                _message = value;

                if (_message != null)
                    _message.PropertyChanged += Message_PropertyChanged;

                OnPropertyChanged("Message");
            }
        }

        public DelegateCommand SendMessageCommand { get; private set; }
        public DelegateCommand<bool?> HighImportanceCommand { get; private set; }
        public DelegateCommand<bool?> LowImportanceCommand { get; private set; }

        #endregion //Properties

        #region Constructors

        public MessageViewModel(IEventAggregator eventAggregator, IMessageBoxService messageBoxService, IMailService mailService, IContactService contactService)
        {
            _eventAggragtor = eventAggregator;
            _messageBoxService = messageBoxService;
            _mailService = mailService;

            SendMessageCommand = new DelegateCommand(SendMessage, CanSendMessage);
            HighImportanceCommand = new DelegateCommand<bool?>(HighImportance);
            LowImportanceCommand = new DelegateCommand<bool?>(LowImportance);

            Contacts = contactService.GetContacts();

            IconSource = "pack://application:,,,/IgOutlook.Infrastructure;component/Images/Mail.ico";
        }

        #endregion //Constructors

        #region Commands

        private void LowImportance(bool? state)
        {
            Message.Importance = state == true ? MailPriority.Low : MailPriority.Normal;
        }

        private void HighImportance(bool? state)
        {
            Message.Importance = state == true ? MailPriority.High : MailPriority.Normal;
        }

        private void SendMessage()
        {
            if (_messageMode.Equals(MessageParameters.Reply) || _messageMode.Equals(MessageParameters.ReplyAll))
                _mailService.ReplyToMessage(Message, _sourceMessageId);
            else if (_messageMode.Equals(MessageParameters.Forward))
                _mailService.ForwardMessage(Message, _sourceMessageId);
            else
                _mailService.SendMessage(Message);

            CloseDialog();

            _eventAggragtor.GetEvent<MessageSentEvent>().Publish(Message);
        }

        private bool CanSendMessage()
        {
            return IsValidMessage();
        }

        #endregion //Commands

        #region Base Class Overrides

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            _messageMode = navigationContext.Parameters[MessageParameters.MessageModeKey] as string;
            _sourceMessageId = navigationContext.Parameters[MessageParameters.MessageId] as string;

            CreateNewMessage(_messageMode, _sourceMessageId);
        }

        #endregion //Base Class Overrides

        #region Event Handlers

        void Message_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            SendMessageCommand.RaiseCanExecuteChanged();
        }

        #endregion //Event Handlers

        #region Methods

        async void CreateNewMessage(string messageMode, string sourceMessageId)
        {
            Message = new MailMessage() { From = Resources.ResourceStrings.NewMessageSenderEmail, Id = Guid.NewGuid().ToString() };

            base.UpdateTitleOnPropertyChanged(Message, "Subject", " - " + ResourceStrings.Message_Text, ResourceStrings.Untitled_Text);

            if (!messageMode.Equals(MessageParameters.New))
            {
                var messageToReply = await _mailService.GetMessageByIdAsync(sourceMessageId);
                if (messageToReply == null)
                {
                    _messageBoxService.Show(ResourceStrings.Error_Text, ResourceStrings.MessageNotFound_Message, MessageBoxButtons.Ok);
                    return;
                }

                Message.Contact = messageToReply.Contact;

                if (!messageToReply.Subject.StartsWith("RE:"))
                    Message.Subject = String.Format("RE: {0}", messageToReply.Subject.Replace("FW: ", string.Empty));
                else
                    Message.Subject = messageToReply.Subject;

                if (messageMode.Equals(MessageParameters.Reply))
                {
                    Message.To = new ObservableCollection<string>() { messageToReply.From };
                }
                else if (messageMode.Equals(MessageParameters.ReplyAll))
                {
                    var toAddresses = new ObservableCollection<string>();
                    toAddresses.Add(messageToReply.From);
                    messageToReply.To.Where(x => x != Message.From).ToList().ForEach(x => toAddresses.Add(x));

                    Message.To = toAddresses;
                    Message.Cc = messageToReply.Cc;
                }
                else if (messageMode.Equals(MessageParameters.Forward))
                {
                    if (!messageToReply.Subject.StartsWith("FW:"))
                    {
                        Message.Subject = String.Format("FW: {0}", messageToReply.Subject.Replace("RE: ", string.Empty));
                    }
                }

                Message.Body = messageToReply.Body;
                InsertedRtf = InsertReplyRtf(messageToReply.From, messageToReply.GetToAsString(), messageToReply.GetCcAsString(), messageToReply.Subject, messageToReply.DateSent);
            }
            else
            {
                Title = ResourceStrings.Untitled_Text + " - " + ResourceStrings.Message_Text;
            }
        }

        private string InsertReplyRtf(string from, string to, string cc, string subject, DateTime datetime)
        {
            var messageReplyTemplate = cc == string.Empty ? Resources.ResourceStrings.MessageReplyTemplate : Resources.ResourceStrings.MessageReplyWithCcTemplate;
            
            messageReplyTemplate = messageReplyTemplate.Replace("%to%", to);
            messageReplyTemplate = messageReplyTemplate.Replace("%cc%", cc);
            messageReplyTemplate = messageReplyTemplate.Replace("%from%", from);
            messageReplyTemplate = messageReplyTemplate.Replace("%datetime%", datetime.ToString());
            
            // "subject" contains a JP string in UTF-16/Unicode
            char[] chars = subject.ToCharArray();
            
            // convert chars array to a string in which they are coded in the RTF format ( \uXXXXX? )
            string str = ToRtfUnicode(chars);
            
            // replace the "%subject%" string with the actual subject value
            messageReplyTemplate = messageReplyTemplate.Replace("%subject%", str);
            
            return messageReplyTemplate;
        }

        private string ToRtfUnicode(char[] chars)
        {
            StringBuilder sb = new StringBuilder();

            foreach (char ch in chars)
            {
                int intCh = (int)ch;

                if (intCh <= 0x7f) {
                    sb.Append(ch);
                } else {
                    sb.Append("\\u" + Convert.ToUInt32(ch) + "?");
                }
            }

            return sb.ToString();
        }

        bool IsValidMessage()
        {
            return Message != null && Message.To != null && !String.IsNullOrWhiteSpace(Message.Subject);
        }

        #endregion //Methods

        #region IDialogAware Members

        public bool CanCloseDialog()
        {
            if (_closeRequested)
                return true;

            if (_messageBoxService.Show("IG Outlook", ResourceStrings.MessageNotSent_Message) == InteractionResult.Cancel)
                return false;
            else
                return true;
        }

        public event Action RequestClose;
        void CloseDialog()
        {
            if (RequestClose != null)
            {
                _closeRequested = true;
                RequestClose();
            }
        }

        #endregion //IDialogAware Members
    }
}
