using IgExcel.Infrastructure;
using IgExcel.Infrastructure.Dialogs;
using Infragistics.Documents.Excel;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System;

namespace IgExcel.Dialogs
{
    public class PasswordDialogViewModel : ViewModelBase, IDialogAware, INavigationAware
    {
        #region Private Variables

        private IEventAggregator eventAggragator;
        private PasswordDialogMode mode;
        private string passwordMessage;
        private bool isReadOnlyVisible;
        private bool isPasswordInvalid;
        private string password;
        private Workbook workbook;

        #endregion //Private Variables

        #region Public Properties

        public string PasswordMessage
        {
            get { return passwordMessage; }
            set { SetProperty<string>(ref passwordMessage, value); }
        }
        public bool IsReadOnlyVisible
        {
            get { return isReadOnlyVisible; }
            set { SetProperty<bool>(ref isReadOnlyVisible, value); }
        }
        public string Password
        {
            get { return password; }
            set { SetProperty<string>(ref password, value); }
        }
        public bool IsPasswordInvalid
        {
            get { return isPasswordInvalid; }
            set { SetProperty<bool>(ref isPasswordInvalid, value); }
        }

        public DelegateCommand OkCommand { get; private set; }
        public DelegateCommand CancelCommand { get; private set; }
        public DelegateCommand ReadOnlyCommand { get; private set; }

        #endregion //Public Properties

        #region Constructor

        public PasswordDialogViewModel(IEventAggregator eventAggragator)
        {
            this.eventAggragator = eventAggragator;

            this.Title = ResourceStrings.ResourceStrings.Text_Password;

            this.IconSource = "pack://application:,,,/IgExcel.Infrastructure;component/Images/Key.ico";

            this.OkCommand = new DelegateCommand(ExecuteOk, CanExecuteOk);
            this.CancelCommand = new DelegateCommand(ExecuteCancel);
            this.ReadOnlyCommand = new DelegateCommand(ExecuteReadOnly);

            this.PropertyChanged += (s, a) =>
            {
                if (a.PropertyName == "Password")
                {
                    OkCommand.RaiseCanExecuteChanged();

                    if (string.IsNullOrEmpty(password))
                        this.IsPasswordInvalid = false;
                }
            };
        }



        #endregion //Constructor

        #region Commands

        private bool CanExecuteOk()
        {
            return !string.IsNullOrEmpty(this.password);
        }

        private void ExecuteOk()
        {
            if (this.mode == PasswordDialogMode.OpenPassword)
            {
                RequestClose();

                this.eventAggragator.GetEvent<PasswordToOpenEnteredEvent>().Publish(this.password);
            }
            else if (this.mode == PasswordDialogMode.ModifyPassword)
            {
                this.IsPasswordInvalid = !this.workbook.ValidateFileWriteProtectionPassword(this.password);

                if (this.IsPasswordInvalid == false)
                {
                    this.eventAggragator.GetEvent<PasswordToModifyEnteredEvent>().Publish(WriteProtectedFileMode.ReadAndWrite);

                    RequestClose();
                }
            }
        }

        private void ExecuteReadOnly()
        {
            this.eventAggragator.GetEvent<PasswordToModifyEnteredEvent>().Publish(WriteProtectedFileMode.ReadOnly);
            RequestClose();
        }

        private void ExecuteCancel()
        {
            if (this.mode == PasswordDialogMode.ModifyPassword)
            {
                this.eventAggragator.GetEvent<PasswordToModifyEnteredEvent>().Publish(WriteProtectedFileMode.ReadOnly);
            }

            RequestClose();
        }

        #endregion //Commands

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

        #region INavigationAware

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            this.mode = (PasswordDialogMode)navigationContext.Parameters[PasswordDialogViewParameters.DialogModeKey];
            this.workbook = (Workbook)navigationContext.Parameters[PasswordDialogViewParameters.WorkbookKey];
            var fileName = (string)navigationContext.Parameters[PasswordDialogViewParameters.FileNameKey];

            PrepareUi(this.mode, fileName);
        }

        #endregion //INavigationAware

        #region Private Methods

        private void PrepareUi(PasswordDialogMode dialogMode, string fileName)
        {
            switch (dialogMode)
            {
                case PasswordDialogMode.OpenPassword:
                    this.PasswordMessage = string.Format(ResourceStrings.ResourceStrings.Text_DocumentIsProtected, fileName);
                    this.IsReadOnlyVisible = false;
                    break;
                case PasswordDialogMode.ModifyPassword:
                    this.PasswordMessage = ResourceStrings.ResourceStrings.Text_EnterPasswordForWriteAccess;
                    this.IsReadOnlyVisible = true;
                    break;
            }
        }

        #endregion //Private Methods
    }
}
