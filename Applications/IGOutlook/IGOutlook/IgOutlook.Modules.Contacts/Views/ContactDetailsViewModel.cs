using IgOutlook.Business.Contacts;
using IgOutlook.Infrastructure.Dialogs;
using IgOutlook.Modules.Contacts.Resources;
using IgOutlook.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System;

namespace IgOutlook.Modules.Contacts.Views
{
    public class ContactDetailsViewModel : ContactsViewBase, IDialogAware
    {
        #region Private Members

        private IMessageBoxService _messageBoxService;
        private string _contactMode;
        private int _contactId;
        private bool _closeRequested;
        private bool _saveChanges;
        private Contact _originalContact;
        private bool _isDirty;
        private bool _isNewContact;
        #endregion //Private Members

        #region Properties

        protected new IContactService ContactService { get; private set; }
        public DelegateCommand SaveAndCloseCommand { get; private set; }
        public DelegateCommand DeleteAndCloseCommand { get; private set; }

        #endregion //Properties

        #region Constructor

        public ContactDetailsViewModel(IEventAggregator eventAggragator, IContactService contactService, IDialogService dialogService, IMessageBoxService messageBoxService)
            : base(eventAggragator, contactService, dialogService)
        {
            ContactService = contactService;

            _messageBoxService = messageBoxService;

            SaveAndCloseCommand = new DelegateCommand(SaveAndClose);
            DeleteAndCloseCommand = new DelegateCommand(DeleteAndClose);

            _isDirty = false;
        }

        #endregion //Constructor

        #region Base Class Overrides

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            _contactMode = navigationContext.Parameters[ContactParameters.ContactModeKey] as string;

            if (_contactMode == ContactParameters.New)
            {
                ActiveContact = new Contact { PhotoUri = new Uri("/IgOutlook.Modules.Contacts;component/Images/unknown.png", UriKind.Relative) };

                _contactId = ContactService.GetNextValidContactId();
                _isNewContact = true;
                ActiveContact.Id = _contactId;

                base.UpdateTitleOnPropertyChanged(ActiveContact, "FileAs", " - " + ResourceStrings.Contact_Text, ResourceStrings.Untitled_Text);
                ActiveContact.FileAs = string.Empty;
            }
            else if (_contactMode == ContactParameters.View)
            {
                _contactId = int.Parse(navigationContext.Parameters[ContactParameters.ContactIdKey] as string);
                _originalContact = ContactService.GetContact(_contactId);

                ActiveContact = new Contact();
                base.UpdateTitleOnPropertyChanged(ActiveContact, "FileAs", " - " + ResourceStrings.Contact_Text, ResourceStrings.Untitled_Text);

                _originalContact.CopyPropertiesTo(ActiveContact);
            }

            ActiveContact.PropertyChanged += (s, a) =>
            {
                if (a.PropertyName != "Notes")
                    _isDirty = true;
            };
        }

        #endregion //Base Class Overrides

        #region IDialogAware Members

        public bool CanCloseDialog()
        {
            InteractionResult msgCloseResult = InteractionResult.None;

            if (!_isDirty && !ActiveContact.HasErrors())
                return true;

            if (!_isDirty && _isNewContact && !_closeRequested)
                return true;

            if (_closeRequested)
            {
                msgCloseResult = _saveChanges ? InteractionResult.Yes : InteractionResult.No;
            }
            else
            {
                msgCloseResult = _messageBoxService.Show("IG Outlook", ResourceStrings.SaveChangesMessage_Text, MessageBoxButtons.YesNoCancel);
            }

            if (msgCloseResult == InteractionResult.Cancel)
            {
                _saveChanges = false;
                return false;
            }
            else
            {
                _saveChanges = msgCloseResult == InteractionResult.Yes;

                if (_saveChanges)
                {
                    InteractionResult msgInvalidDataResult = InteractionResult.None;

                    if (ActiveContact.HasErrors())
                    {
                        msgInvalidDataResult = _messageBoxService.Show("IG Outlook", ResourceStrings.InvalidContactDataMessage_Text, MessageBoxButtons.YesNo);
                    }

                    if (msgInvalidDataResult == InteractionResult.No)
                    {
                        return true;
                    }
                    else
                    {
                        if (_isNewContact)
                        {
                            ContactService.AddContact(ActiveContact);
                            return true;
                        }
                    }

                    ContactService.UpdateContact(ActiveContact);
                    EventAggregator.GetEvent<ContactUpdatedEvent>().Publish(ActiveContact);
                }

                return true;
            }

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

        #region Commands

        private void SaveAndClose()
        {
            _saveChanges = true;

            CloseDialog();
        }

        private void DeleteAndClose()
        {
            base.DeleteContact();

            EventAggregator.GetEvent<ContactDeletedEvent>().Publish(ActiveContact);

            CloseDialog();
        }

        #endregion //Commands
    }
}
