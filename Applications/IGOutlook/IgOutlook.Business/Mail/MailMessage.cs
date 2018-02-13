using IgOutlook.Business.Calendar;
using IgOutlook.Business.Core;
using System;
using System.Linq;
using System.Collections.ObjectModel;
using IgOutlook.Business.Contacts;

namespace IgOutlook.Business.Mail
{
    public class MailMessage : BusinessBase
    {
        #region Properties

        public MailPriority Importance { get; set; }

        private Contact _contact;

        public Contact Contact
        {
            get { return _contact; }
            set
            {
                _contact = value;
                NotifyPropertyChanged();
            }
        }

        private ActivityCategory _category;
        public ActivityCategory Category
        {
            get { return _category; }
            set
            {
                _category = value;
                NotifyPropertyChanged();
            }
        }

        public string Id { get; set; }

        private MailFlags _flags;
        public MailFlags Flags
        {
            get { return _flags; }
            set
            {
                _flags = value;
                NotifyPropertyChanged();
            }
        }

        private string _from;
        public string From
        {
            get { return _from; }
            set
            {
                _from = value;
                NotifyPropertyChanged();
            }
        }

        private string _subject;
        public string Subject
        {
            get { return _subject; }
            set
            {
                _subject = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<string> _to;
        public ObservableCollection<string> To
        {
            get { return _to; }
            set
            {
                _to = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<string> _cc;
        public ObservableCollection<string> Cc
        {
            get { return _cc; }
            set
            {
                _cc = value;
                NotifyPropertyChanged();
            }
        }

        private string _body;
        public string Body
        {
            get { return _body; }
            set
            {
                _body = value;
                NotifyPropertyChanged();
            }
        }

        private DateTime _dateSent;
        public DateTime DateSent
        {
            get { return _dateSent; }
            set
            {
                _dateSent = value;
                NotifyPropertyChanged();
            }
        }

        public bool IsRead { get { return (Flags & MailFlags.Seen) == MailFlags.Seen; } }
        public bool IsFlagged { get { return (Flags & MailFlags.Flagged) == MailFlags.Flagged; } }

        #endregion //Properties

        #region Constructor

        public MailMessage()
        {
            Importance = MailPriority.Normal;
        }

        #endregion //Constructor

        #region Methods

        public void RemoveFlag()
        {
            Flags &= ~MailFlags.Flagged;
        }

        public void Flag()
        {
            Flags |= MailFlags.Flagged;
        }

        public void MarkAsRead()
        {
            Flags &= ~MailFlags.None; //remove the None
            Flags |= MailFlags.Seen; //add the Seen
        }

        public void MarkAsUnread()
        {
            Flags &= ~MailFlags.Seen; //remove the Seen
            Flags |= MailFlags.None; // add the None
        }

        public void MarkAsReplied()
        {
            Flags &= ~MailFlags.Forwarded; //remove the answered
            Flags |= MailFlags.Answered; //add the replied
        }

        public void MarkAsForwarded()
        {
            Flags &= ~MailFlags.Answered; //remove the answered
            Flags |= MailFlags.Forwarded; //add the forward
        }

        public string GetToAsString()
        {
            if (_to == null)
                return string.Empty;

            return string.Join(";", _to);
        }

        public string GetCcAsString()
        {
            if (_cc == null)
                return string.Empty;

            return string.Join(";", _cc);
        }

        #endregion //Methods
    }
}
