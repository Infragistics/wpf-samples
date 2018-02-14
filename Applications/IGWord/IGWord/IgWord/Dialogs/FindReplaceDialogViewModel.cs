using IgWord.Infrastructure;
using IgWord.Infrastructure.Dialogs;
using Infragistics.Controls.Editors;
using Infragistics.Documents.RichText;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;

namespace IgWord.Dialogs
{
    public class FindReplaceDialogViewModel : ViewModelBase, IDialogAware, INavigationAware
    {
        #region Member Variables

        private FindReplaceManager findReplaceManager;
        private string findResultsStatusMessage;
        private FindCriteria findCriteria;
        private string findText;
        private string replaceText;
        private bool matchCase;
        private bool useWildcards;
        private string selectedOptionsText;
        private bool selectedOptionsIsVisible;
        private string selectedTabName;

        private bool isHighlightAllVisible;
        private bool isClearHighlightVisible;



        #endregion //Member Variables

        #region Public Properties

        public string SelectedOptionsText
        {
            get { return selectedOptionsText; }
            set { selectedOptionsText = value; NotifyPropertyChanged(); }
        }
        public bool SelectedOptionsIsVisible
        {
            get { return selectedOptionsIsVisible; }
            set { selectedOptionsIsVisible = value; NotifyPropertyChanged(); }
        }
        public string FindText
        {
            get { return findText; }
            set { findText = value; RefreshCanExecute(); NotifyPropertyChanged(); }
        }
        public string FindResultsStatusMessage
        {
            get { return findResultsStatusMessage; }
            set { findResultsStatusMessage = value; NotifyPropertyChanged(); }
        }
        public string ReplaceText
        {
            get { return replaceText; }
            set { replaceText = value; NotifyPropertyChanged(); }
        }
        public string SelectedTabName
        {
            get { return selectedTabName; }
            set { selectedTabName = value; NotifyPropertyChanged(); }
        }

        public bool MatchCase
        {
            get { return this.matchCase; }
            set
            {
                if (value != this.matchCase)
                {
                    this.matchCase = value;

                    this.findCriteria = new FindCriteria() { CaseSensitive = value, Operator = this.UseWildcards ? FindOperator.Wildcard : FindOperator.PlainText };

                    UpdateSelectedOptionsText();
                    NotifyPropertyChanged();
                }
            }
        }
        public bool UseWildcards
        {
            get { return this.useWildcards; }
            set
            {
                if (value != this.useWildcards)
                {
                    this.useWildcards = value;

                    this.findCriteria = new FindCriteria() { CaseSensitive = this.MatchCase, Operator = value ? FindOperator.Wildcard : FindOperator.PlainText };

                    UpdateSelectedOptionsText();
                    NotifyPropertyChanged();
                }
            }
        }

        public bool IsHighlightAllVisible
        {
            get { return isHighlightAllVisible; }
            set { isHighlightAllVisible = value; NotifyPropertyChanged(); }
        }
        public bool IsClearHighlightVisible
        {
            get { return isClearHighlightVisible; }
            set { isClearHighlightVisible = value; NotifyPropertyChanged(); }
        }

        public FindReplaceManager FindReplaceManager
        {
            get { return findReplaceManager; }
            set { findReplaceManager = value; NotifyPropertyChanged(); }
        }
        public DelegateCommand FindNextCommand { get; private set; }
        public DelegateCommand HighlightAllCommand { get; private set; }
        public DelegateCommand ClearHighlightingCommand { get; private set; }
        public DelegateCommand FindPreviousCommand { get; private set; }
        public DelegateCommand FindInDocumentCommand { get; private set; }
        public DelegateCommand ReplaceCommand { get; private set; }
        public DelegateCommand ReplaceAllCommand { get; private set; }
        public DelegateCommand CloseCommand { get; private set; }

        #endregion //Public Properties

        #region Constructor

        public FindReplaceDialogViewModel(IEventAggregator eventAggragator)
        {
            Title = ResourceStrings.ResourceStrings.Dlg_FindandReplace_Title;

            FindNextCommand = new DelegateCommand(ExecuteFindNext, CanExecuteFindNext);
            FindPreviousCommand = new DelegateCommand(ExecuteFindPrevious, CanExecuteFindPrevious);
            FindInDocumentCommand = new DelegateCommand(ExecuteFindInDocument, CanExecuteFindInDocument);
            HighlightAllCommand = new DelegateCommand(ExecuteHighlightAll, CanExecuteHighlightAll);
            ClearHighlightingCommand = new DelegateCommand(ExecuteClearHighlighting, CanExecuteClearHighlighting);
            ReplaceCommand = new DelegateCommand(ExecuteReplace, CanExecuteReplace);
            ReplaceAllCommand = new DelegateCommand(ExecuteReplaceAll, CanExecuteReplaceAll);
            CloseCommand = new DelegateCommand(Close);

            this.findCriteria = new FindCriteria() { CaseSensitive = false, Operator = FindOperator.PlainText };

            this.IsHighlightAllVisible = true;
            this.IsClearHighlightVisible = false;

            this.IconSource = "pack://application:,,,/IgWord.Infrastructure;component/Images/Find.ico";
        }

        #endregion //Constructor

        #region Private Methods

        private void UpdateSelectedOptionsText()
        {
            if (matchCase == true || useWildcards == true)
            {
                selectedOptionsText = string.Empty;

                if (matchCase)
                    selectedOptionsText += selectedOptionsText.Length > 0 ? ", Match Case" : "Match Case";

                if (useWildcards)
                    selectedOptionsText += selectedOptionsText.Length > 0 ? ", Use Wildcards" : "Use Wildcards";

                NotifyPropertyChanged("SelectedOptionsText");
                SelectedOptionsIsVisible = true;
            }
            else
            {
                SelectedOptionsIsVisible = false;
            }
        }

        private void RefreshCanExecute()
        {
            FindNextCommand.RaiseCanExecuteChanged();
            FindPreviousCommand.RaiseCanExecuteChanged();
            FindInDocumentCommand.RaiseCanExecuteChanged();
            HighlightAllCommand.RaiseCanExecuteChanged();
            ReplaceCommand.RaiseCanExecuteChanged();
            ReplaceAllCommand.RaiseCanExecuteChanged();
        }

        private void UpdateFindResultsStatusMessage(int totalResults, string action)
        {
            if (totalResults == 1)
                this.FindResultsStatusMessage = string.Format(ResourceStrings.ResourceStrings.Text_OneItemMatching, action);
            else
                this.FindResultsStatusMessage = string.Format(ResourceStrings.ResourceStrings.Text_MultipleItemsMatching, action, totalResults);
        }

        #endregion //Private Methods

        #region Commands

        #region FindNext

        private bool CanExecuteFindNext()
        {
            return false == string.IsNullOrEmpty(this.FindText);
        }

        private void ExecuteFindNext()
        {
            string error = null;
            int totalResults = this.FindReplaceManager.FindInDocument(this.FindText, out error, this.findCriteria);

            if (totalResults == 0)
            {
                this.FindResultsStatusMessage = string.Format(ResourceStrings.ResourceStrings.Text_ItemNotFound, findText);
            }
            else
            {
                this.FindResultsStatusMessage = "";
            }

            this.FindReplaceManager.SelectNextMatch();
        }

        #endregion //FindNext

        #region FindPrevious

        private bool CanExecuteFindPrevious()
        {
            return false == string.IsNullOrEmpty(this.FindText);
        }

        private void ExecuteFindPrevious()
        {
            string error = null;
            int totalResults = this.FindReplaceManager.FindInDocument(this.FindText, out error, this.findCriteria);

            if (totalResults == 0)
            {
                this.FindResultsStatusMessage = string.Format(ResourceStrings.ResourceStrings.Text_ItemNotFound, findText);
            }
            else
            {
                this.FindResultsStatusMessage = "";
            }

            this.FindReplaceManager.SelectPreviousMatch();
        }
        #endregion //FindPrevious

        #region HighlightAll

        internal bool CanExecuteHighlightAll()
        {
            return false == string.IsNullOrEmpty(this.FindText);
        }

        internal void ExecuteHighlightAll()
        {
            string error = null;

            // the FindReplaceManager.Highlight method will highlight only occurences inside the selection (if such exists)

            // so save the current selection ranges
            ObservableCollection <DocumentSpan> oldRanges = new ObservableCollection<DocumentSpan>();
            foreach (Range r in this.FindReplaceManager.RichTextEditor.Selection.Ranges)
            {
                oldRanges.Add(r.DocumentSpan);
            }

            // clear current selection
            this.FindReplaceManager.RichTextEditor.Selection.Collapse(SelectionCollapseDirection.Start);

            // highlight the occurences
            int totalHighlighted = this.FindReplaceManager.Highlight(this.FindText, out error, this.findCriteria);

            this.UpdateFindResultsStatusMessage(totalHighlighted, ResourceStrings.ResourceStrings.Text_Highlighted);
            this.ClearHighlightingCommand.RaiseCanExecuteChanged();

            // restore selection
            this.FindReplaceManager.RichTextEditor.Selection.UpdateSelectionWithSpans(oldRanges, true);
            this.FindReplaceManager.SelectNextMatch();
        }

        #endregion //HighlightAll

        #region ClearHighlighting

        internal bool CanExecuteClearHighlighting()
        {
            if (this.FindReplaceManager == null) return false;

            if (this.FindReplaceManager.HighlightMatchesCount > 0)
            {
                this.IsClearHighlightVisible = true;
                this.IsHighlightAllVisible = false;

                return true;
            }

            return false;
        }

        internal void ExecuteClearHighlighting()
        {
            this.FindReplaceManager.ClearHighlighting();
            this.FindResultsStatusMessage = "";
            this.IsClearHighlightVisible = false;
            this.IsHighlightAllVisible = true;
            this.ClearHighlightingCommand.RaiseCanExecuteChanged();
        }

        #endregion //ClearHighlighting

        #region FindInDocument

        internal bool CanExecuteFindInDocument()
        {
            return false == string.IsNullOrEmpty(this.FindText);
        }

        internal void ExecuteFindInDocument()
        {
            string error = null;
            int totalResults = this.FindReplaceManager.FindInDocument(this.FindText, out error, this.findCriteria);
            this.UpdateFindResultsStatusMessage(totalResults, ResourceStrings.ResourceStrings.Text_Found);

            this.FindReplaceManager.SelectAllMatches();
        }

        #endregion //FindInDocument

        #region Replace

        internal bool CanExecuteReplace()
        {
            return false == string.IsNullOrEmpty(this.FindText);
        }

        internal void ExecuteReplace()
        {
            string error = null;
            if (this.FindReplaceManager.FindMatchesCount < 1)
                this.FindReplaceManager.FindInDocument(this.FindText, out error, this.findCriteria);

            if (null == error)
                this.FindReplaceManager.ReplaceCurrent(this.ReplaceText, out error);

            this.FindResultsStatusMessage = "";
        }

        #endregion //Replace

        #region ReplaceAll

        internal bool CanExecuteReplaceAll()
        {
            return false == string.IsNullOrEmpty(this.FindText);
        }

        internal void ExecuteReplaceAll()
        {
            string error = null;
            int totalReplaced = this.FindReplaceManager.ReplaceAll(this.FindText, this.ReplaceText, out error, this.findCriteria);

            this.UpdateFindResultsStatusMessage(totalReplaced, ResourceStrings.ResourceStrings.Text_Replaced);
        }
        #endregion //ReplaceAll

        #region Close

        private void Close()
        {
            RequestClose();
        }

        #endregion //Close

        #endregion //Commands

        #region IDialogAware Members

        public bool CanCloseDialog()
        {
            if (this.IsClearHighlightVisible)
            {
                ExecuteClearHighlighting();
            }
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
            this.FindReplaceManager = (FindReplaceManager)navigationContext.Parameters[FindReplaceDialogParameters.DataItemKey];

            SelectedTabName = (string)navigationContext.Parameters[FindReplaceDialogParameters.DialogModeKey];

            this.FindReplaceManager.ClearResults();
        }

        #endregion //INavigationAware
    }
}
