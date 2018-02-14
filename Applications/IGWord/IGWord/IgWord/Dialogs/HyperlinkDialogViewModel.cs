using IgWord.Infrastructure;
using IgWord.Infrastructure.Dialogs;
using Infragistics.Documents.RichText;
using Prism.Commands;
using Prism.Regions;
using System;
using System.Linq;

namespace IgWord.Dialogs
{
    public class HyperlinkDialogViewModel : ViewModelBase, IDialogAware, INavigationAware
    {
        #region Member Variables

        private string textToDisplay;
        private string toolTipText;
        private string address;
        private RichTextDocument document;
        private Selection selection;
        private HyperlinkDialogMode dialogMode;
        private HyperlinkNode hyperlinkBeingEdited;
        private IMessageBoxService messageBoxService;

        #endregion //Member Variables

        #region Public Properties

        public string TextToDisplay
        {
            get { return this.textToDisplay; }
            set
            {
                if (value != this.textToDisplay)
                {
                    this.textToDisplay = value;
                    OkCommand.RaiseCanExecuteChanged();
                    this.NotifyPropertyChanged();
                }
            }
        }
        public string ToolTipText
        {
            get { return this.toolTipText; }
            set
            {
                if (value != this.toolTipText)
                {
                    this.toolTipText = value;

                    this.NotifyPropertyChanged();
                }
            }
        }
        public string Address
        {
            get { return this.address; }
            set
            {
                if (value != this.address)
                {
                    this.address = value;
                    OkCommand.RaiseCanExecuteChanged();
                    this.NotifyPropertyChanged();
                }
            }
        }

        public DelegateCommand OkCommand { get; private set; }
        public DelegateCommand CancelCommand { get; private set; }

        #endregion //Public Properties

        #region Constructor

        public HyperlinkDialogViewModel(IMessageBoxService messageBoxService)
        {
            this.Title = ResourceStrings.ResourceStrings.Dlg_Hyperlink_Title;
            this.IconSource = "pack://application:,,,/IgWord.Infrastructure;component/Images/Hyperlink.ico";
            this.messageBoxService = messageBoxService;

            OkCommand = new DelegateCommand(ExecuteOk, CanExecuteOk);
            CancelCommand = new DelegateCommand(ExecuteCancel);
        }

        #endregion //Constructor

        #region Commands

        private bool CanExecuteOk()
        {
            return !string.IsNullOrEmpty(this.textToDisplay) && !string.IsNullOrEmpty(this.address);
        }

        private void ExecuteOk()
        {
            string error;

            switch (this.dialogMode)
            {
                case HyperlinkDialogMode.MakingHyperlink:
                    document.MakeHyperlink(selection.DocumentSpan, this.Address, out error, this.ToolTipText);
                    break;

                case HyperlinkDialogMode.EditingHyperlink:
                    if (null != this.hyperlinkBeingEdited)
                    {
                        string oldUri = this.hyperlinkBeingEdited.Uri;
                        try
                        {
                            this.hyperlinkBeingEdited.Uri = this.Address;
                        }
                        catch (Exception)
                        {
                            this.hyperlinkBeingEdited.Uri = oldUri;
                            this.messageBoxService.Show(ResourceStrings.ResourceStrings.Text_Error, ResourceStrings.ResourceStrings.Msg_WrongUriFormat, MessageBoxButtons.Ok);
                            return;
                        }
                        this.hyperlinkBeingEdited.SetText(this.TextToDisplay);
                        this.hyperlinkBeingEdited.Tooltip = this.ToolTipText;
                    }
                    break;

                case HyperlinkDialogMode.InsertingHyperlink:
                    try
                    {
                        document.InsertHyperlink(selection.Start, this.Address, out error, this.TextToDisplay, this.ToolTipText);
                    }
                    catch (Exception)
                    {
                        this.messageBoxService.Show(ResourceStrings.ResourceStrings.Text_Error, ResourceStrings.ResourceStrings.Msg_WrongUriFormat, MessageBoxButtons.Ok);
                        return;
                    }
                    break;
            }

            RequestClose();
        }

        private void ExecuteCancel()
        {
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

        public void OnNavigatedFrom(NavigationContext navigationContext) { }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            this.document = (RichTextDocument)navigationContext.Parameters[HyperlinkDialogParameters.DocumentKey];
            this.selection = (Selection)navigationContext.Parameters[HyperlinkDialogParameters.SelectionKey];

            HyperlinkNode[] hyperlinksInSpan = document.GetHyperlinks(this.selection.DocumentSpan).ToArray();

            if (hyperlinksInSpan.Length > 0)
            {
                this.hyperlinkBeingEdited = hyperlinksInSpan[0];

                this.TextToDisplay = this.hyperlinkBeingEdited.GetText();
                this.Address = this.hyperlinkBeingEdited.Uri;
                this.ToolTipText = this.hyperlinkBeingEdited.Tooltip;

                this.dialogMode = HyperlinkDialogMode.EditingHyperlink;
            }
            else
            {
                if (selection.Length > 0)
                {
                    this.dialogMode = HyperlinkDialogMode.MakingHyperlink;
                    this.TextToDisplay = this.selection.Text;
                }
                else
                {
                    if (this.SelectionIsWithinWord())
                        selection.SelectCurrentWord();

                    string textInSelection = document.GetTextInSpan(selection.DocumentSpan);
                    if (textInSelection.Trim().Length < 1)
                    {
                        this.dialogMode = HyperlinkDialogMode.InsertingHyperlink;
                        selection.Collapse(SelectionCollapseDirection.Start);
                    }
                    else
                    {
                        this.dialogMode = HyperlinkDialogMode.MakingHyperlink;
                        this.TextToDisplay = selection.Text;
                    }
                }
            }
        }

        #endregion //INavigationAware

        #region SelectionIsWithinWord
        private bool SelectionIsWithinWord()
        {
            DocumentSpan currentCharacterSpan = new DocumentSpan(selection.Start, 1);
            string currentText = document.GetTextInSpan(currentCharacterSpan);

            DocumentSpan previousCharacterSpan = new DocumentSpan(Math.Max(0, selection.Start - 1), 1);
            string previousText = document.GetTextInSpan(previousCharacterSpan);

            return (previousText != " " && currentText != " ");
        }
        #endregion //SelectionIsWithinWord
    }
}
