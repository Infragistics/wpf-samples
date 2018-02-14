using IgWord.Infrastructure;
using IgWord.Infrastructure.Dialogs;
using IgWord.Services;
using Infragistics.Controls.Editors;
using Infragistics.Documents.RichText;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace IgWord
{
    public class ShellViewModel : ViewModelBase, IDialogAware, IViewModel
    {
        #region Private Memebers

        private IDialogService dialogService;
        private IMessageBoxService messageBoxService;
        private IEventAggregator eventAggragator;
        private IRecentFilesService recentFilesService;
        private IFontService fontService;

        private IgWordDocumentStatus documentStatus;

        private string fileName;
        private string selectedBackstageMenuItem;
        private string fileNameWithoutExtension;
        private string selectedTemplateName;

        private bool isNewTemplateRequested;
        private bool closeRequested;
        private bool isEditorVisible;
        private bool isBackstageOpened;
        private bool isUiPartEnabled;
        private bool isDocumentViewSplit;

        private List<double> fontSizes;
        private List<string> fontNames;

        private string textData;
        private string htmlData;
        private byte[] wordData;
        private string rtfData;

        private RichTextAdapterRefreshTrigger textAdapterRefreshTrigger;
        private RichTextAdapterRefreshTrigger rtfAdapterRefreshTrigger;
        private RichTextAdapterRefreshTrigger wordAdapterRefreshTrigger;
        private RichTextAdapterRefreshTrigger htmlAdapterRefreshTrigger;

        private TimeSpan delayAfterFirstEdit;
        private TimeSpan delayAfterLastEdit;
        private Color textHighlightColor;
        private Color fontColor;
        private Color shading;

        private Selection selection;

        private double zoomLevel;
        private bool autoSaved;

        #endregion //Private Memebers

        #region Public Properties

        public List<string> FontNames
        {
            get { return fontNames; }
            set { fontNames = value; NotifyPropertyChanged(); }
        }
        public List<double> FontSizes
        {
            get { return fontSizes; }
            set { fontSizes = value; NotifyPropertyChanged(); }
        }
        public List<Color> HighlightColors
        {
            get;
            private set;
        }

        public bool IsEditorVisible
        {
            get { return isEditorVisible; }
            set { isEditorVisible = value; NotifyPropertyChanged(); }
        }
        public string SelectedBackstageMenuItem
        {
            get { return selectedBackstageMenuItem; }
            set { selectedBackstageMenuItem = value; NotifyPropertyChanged(); }
        }
        public IgWordDocumentStatus DocumentStatus
        {
            get { return documentStatus; }
            set
            {
                if (documentStatus != value)
                {
                    documentStatus = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool IsBackstageOpened
        {
            get { return isBackstageOpened; }
            set { isBackstageOpened = value; NotifyPropertyChanged(); }
        }
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; NotifyPropertyChanged(); }
        }
        public string FileNameWithoutExtension
        {
            get { return fileNameWithoutExtension; }
            set { fileNameWithoutExtension = value; NotifyPropertyChanged(); }
        }
        public bool IsUiPartEnabled
        {
            get { return isUiPartEnabled; }
            set
            {
                if (isUiPartEnabled != value)
                {
                    isUiPartEnabled = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool IsDocumentViewSplit
        {
            get { return isDocumentViewSplit; }
            set
            {
                isDocumentViewSplit = value;
                NotifyPropertyChanged();
            }
        }



        public string RtfData
        {
            get { return rtfData; }
            set
            {
                rtfData = value;
                MarkDocumentAsModified();
                NotifyPropertyChanged();
            }
        }
        public string TextData
        {
            get { return textData; }
            set
            {
                textData = value;
                MarkDocumentAsModified();
                NotifyPropertyChanged();
            }
        }
        public string HtmlData
        {
            get { return htmlData; }
            set
            {
                htmlData = value;
                MarkDocumentAsModified();
                NotifyPropertyChanged();
            }
        }
        public byte[] WordData
        {
            get { return wordData; }
            set
            {
                wordData = value;
                MarkDocumentAsModified();
                NotifyPropertyChanged();
            }
        }

        public RichTextAdapterRefreshTrigger TextAdapterRefreshTrigger
        {
            get { return textAdapterRefreshTrigger; }
            set { textAdapterRefreshTrigger = value; UpdateAutoSavedState(); NotifyPropertyChanged(); }
        }
        public RichTextAdapterRefreshTrigger RtfAdapterRefreshTrigger
        {
            get { return rtfAdapterRefreshTrigger; }
            set { rtfAdapterRefreshTrigger = value; UpdateAutoSavedState(); NotifyPropertyChanged(); }
        }
        public RichTextAdapterRefreshTrigger HtmlAdapterRefreshTrigger
        {
            get { return htmlAdapterRefreshTrigger; }
            set { htmlAdapterRefreshTrigger = value; UpdateAutoSavedState(); NotifyPropertyChanged(); }
        }
        public RichTextAdapterRefreshTrigger WordAdapterRefreshTrigger
        {
            get { return wordAdapterRefreshTrigger; }
            set { wordAdapterRefreshTrigger = value; UpdateAutoSavedState(); NotifyPropertyChanged(); }
        }

        public TimeSpan DelayAfterFirstEdit
        {
            get { return delayAfterFirstEdit; }
            set { delayAfterFirstEdit = value; NotifyPropertyChanged(); }
        }
        public TimeSpan DelayAfterLastEdit
        {
            get { return delayAfterLastEdit; }
            set { delayAfterLastEdit = value; NotifyPropertyChanged(); }
        }

        public Selection Selection
        {
            get { return selection; }
            set { selection = value; NotifyPropertyChanged(); }
        }

        public double ZoomLevel
        {
            get { return zoomLevel; }
            set { zoomLevel = value; NotifyPropertyChanged(); }
        }
        public bool AutoSaved
        {
            get { return autoSaved; }
            set { autoSaved = value; }
        }
        public Color TextHighlightColor
        {
            get { return textHighlightColor; }
            set { textHighlightColor = value; NotifyPropertyChanged(); }
        }
        public Color FontColor
        {
            get { return fontColor; }
            set { fontColor = value; NotifyPropertyChanged(); }
        }
        public Color Shading
        {
            get { return shading; }
            set { shading = value; NotifyPropertyChanged(); }
        }

        public DelegateCommand ResetZoomLevelCommand { get; private set; }
        public DelegateCommand IncreaseZoomLevelCommand { get; private set; }
        public DelegateCommand DecreaseZoomLevelCommand { get; private set; }
        public DelegateCommand SaveDocumentCommand { get; private set; }
        public DelegateCommand CloseDocumentCommand { get; private set; }
        public DelegateCommand<string> CreateNewDocumentCommand { get; private set; }
        public DelegateCommand<FindReplaceManager> ShowFindDialogCommand { get; private set; }
        public DelegateCommand<FindReplaceManager> ShowReplaceDialogCommand { get; private set; }
        public DelegateCommand<RichTextDocument> ShowHyperlinkDialogCommand { get; private set; }
        public DelegateCommand<RichTextDocument> ShowInsertTableDialogCommand { get; private set; }
        public DelegateCommand<RichTextDocument> ShowFontDialogCommand { get; private set; }
        public DelegateCommand<RichTextDocument> ShowParagraphDialogCommand { get; private set; }
        public DelegateCommand<RichTextDocument> ShowInsertPictureDialogCommand { get; private set; }

        #endregion //Public Properties

        #region Constructor

        public ShellViewModel(IEventAggregator eventAggragator, IDialogService dialogService, IMessageBoxService messageBoxService, IFontService fontService, IRecentFilesService recentFilesService)
        {
            this.SetTitle();

            this.eventAggragator = eventAggragator;
            this.dialogService = dialogService;
            this.messageBoxService = messageBoxService;
            this.recentFilesService = recentFilesService;
            this.fontService = fontService;

            this.FontSizes = fontService.GetFontSizes();
            this.FontNames = fontService.GetFontNames();
            this.HighlightColors = fontService.GetHighlightColors();

            this.ClearDataProperties();
            this.DeactivateAdaptersRefreshTrigger();

            this.DelayAfterFirstEdit = TimeSpan.FromSeconds(10);
            this.DelayAfterLastEdit = TimeSpan.FromSeconds(10);
            this.ZoomLevel = 1;
            this.FontColor = Colors.Red;
            this.TextHighlightColor = Colors.Yellow;

            this.DocumentStatus = IgWordDocumentStatus.NoDocumentLoaded;
            this.SelectedBackstageMenuItem = "New";

			// TFS 190054 - Workaround: do not initialize the value of the IsBackstageOpen property to true before the ribbon is loaded
			//this.IsBackstageOpened = true;

            this.recentFilesService.SetRepositories(Properties.Settings.Default, p => Properties.Settings.Default.RecentFilesRepo, p => Properties.Settings.Default.RecentFoldersRepo);

            //Subscribe to events
            this.eventAggragator.GetEvent<FileOpenedEvent>().Subscribe(OnFileLoaded);
            this.eventAggragator.GetEvent<FileSavedEvent>().Subscribe(OnFileSaved);
            this.eventAggragator.GetEvent<NewDocumentEvent>().Subscribe(OnNewDocumentEvent);
            this.eventAggragator.GetEvent<CharacterSettingsUpdatedEvent>().Subscribe(OnCharacterSettingsUpdatedEvent);
            this.eventAggragator.GetEvent<ParagraphSettingsUpdatedEvent>().Subscribe(OnParagraphSettingsUpdatedEvent);

            //Commands
            SaveDocumentCommand = new DelegateCommand(ExecuteSaveDocument);
            CloseDocumentCommand = new DelegateCommand(ExecuteCloseDocument);
            CreateNewDocumentCommand = new DelegateCommand<string>(CreateNewDocument);
            ShowFontDialogCommand = new DelegateCommand<RichTextDocument>(ExecuteShowFontDialog, CanExecuteShowFormattingDialog);
            ShowParagraphDialogCommand = new DelegateCommand<RichTextDocument>(ExecuteShowParagraphDialog, CanExecuteShowFormattingDialog);
            ShowFindDialogCommand = new DelegateCommand<FindReplaceManager>(ExecuteShowFindDialog, CanExecuteShowFindReplaceDialog);
            ShowReplaceDialogCommand = new DelegateCommand<FindReplaceManager>(ExecuteShowReplaceDialog, CanExecuteShowFindReplaceDialog);
            ShowHyperlinkDialogCommand = new DelegateCommand<RichTextDocument>(ExecuteShowHyperlinkDialog, CanExecuteShowFormattingDialog);
            ShowInsertTableDialogCommand = new DelegateCommand<RichTextDocument>(ExecuteShowInsertTableDialog, CanExecuteShowFormattingDialog);
            ShowInsertPictureDialogCommand = new DelegateCommand<RichTextDocument>(ExecuteShowInsertPictureDialog, CanExecuteShowFormattingDialog);
            ResetZoomLevelCommand = new DelegateCommand(ExecuteResetZoomLevel);
            IncreaseZoomLevelCommand = new DelegateCommand(ExecuteIncreaseZoomLevel);
            DecreaseZoomLevelCommand = new DelegateCommand(ExecuteDecreaseZoomLevel);
        }

        #endregion //Constructor

        #region Commands

        private void ExecuteShowFindDialog(FindReplaceManager findReplaceManager)
        {
            NavigationParameters parameters = new NavigationParameters();
            parameters.Add(FindReplaceDialogParameters.DataItemKey, findReplaceManager);
            parameters.Add(FindReplaceDialogParameters.DialogModeKey, FindReplaceDialogParameters.FindMode);

            this.dialogService.ShowIgDialog("FindReplaceDialogView", parameters, false);
        }

        private void ExecuteShowReplaceDialog(FindReplaceManager findReplaceManager)
        {
            NavigationParameters parameters = new NavigationParameters();
            parameters.Add(FindReplaceDialogParameters.DataItemKey, findReplaceManager);
            parameters.Add(FindReplaceDialogParameters.DialogModeKey, FindReplaceDialogParameters.ReplaceMode);

            this.dialogService.ShowIgDialog("FindReplaceDialogView", parameters, false);
        }

        private void ExecuteShowParagraphDialog(RichTextDocument document)
        {
            NavigationParameters parameters = new NavigationParameters();
            parameters.Add(ParagraphDialogParameters.ParagraphSettingsKey, document.GetCommonParagraphSettings(this.Selection.DocumentSpan));

            this.dialogService.ShowIgDialog("ParagraphDialogView", parameters);
        }

        private void ExecuteShowFontDialog(RichTextDocument document)
        {
            NavigationParameters parameters = new NavigationParameters();
            parameters.Add(FontDialogParameters.CharacterSettingsKey, document.GetCommonCharacterSettings(this.Selection.DocumentSpan));

            this.dialogService.ShowIgDialog("FontDialogView", parameters);
        }

        private void ExecuteShowHyperlinkDialog(RichTextDocument document)
        {
            NavigationParameters parameters = new NavigationParameters();
            parameters.Add(HyperlinkDialogParameters.DocumentKey, document);
            parameters.Add(HyperlinkDialogParameters.SelectionKey, this.Selection);

            this.dialogService.ShowIgDialog("HyperlinkDialogView", parameters);
        }

        private void ExecuteShowInsertTableDialog(RichTextDocument document)
        {
            NavigationParameters parameters = new NavigationParameters();
            parameters.Add(InsertTableDialogParameters.DocumentKey, document);
            parameters.Add(InsertTableDialogParameters.SelectionKey, this.Selection);

            this.dialogService.ShowIgDialog("InsertTableDialogView", parameters);
        }

        private void ExecuteSaveDocument()
        {
            if (DocumentStatus == IgWordDocumentStatus.BlankModified
                || DocumentStatus == IgWordDocumentStatus.BlankNotModified
                || DocumentStatus == IgWordDocumentStatus.TemplateNotModified)
            {
                if (!IsBackstageOpened)
                    IsBackstageOpened = true;

                SelectedBackstageMenuItem = ShellParameters.SaveAsTabName;
            }
            else if (DocumentStatus != IgWordDocumentStatus.NoDocumentLoaded)
            {
                PushChangesToFile(FileName);

                if (this.isNewTemplateRequested)
                {
                    var tempDocumentName = string.Format(ResourceStrings.ResourceStrings.Text_TempDocumentName, 1);

                    LoadTemplate(this.selectedTemplateName);
                    SetTitle(tempDocumentName);

                    this.FileNameWithoutExtension = tempDocumentName;

                    this.isNewTemplateRequested = false;
                    this.selectedTemplateName = string.Empty;
                }
                else
                {
                    DocumentStatus = IgWordDocumentStatus.ExisitngNotModified;
                    UpdateTitle(false);
                }

                IsBackstageOpened = false;
            }
        }

        private void ExecuteCloseDocument()
        {
            if (CanCloseDocument(true))
            {
                UpdateAdaptersRefreshTrigger(RichTextAdapterRefreshTrigger.Explicit,
                                                        RichTextAdapterRefreshTrigger.Explicit,
                                                        RichTextAdapterRefreshTrigger.Explicit,
                                                        RichTextAdapterRefreshTrigger.Explicit);

                this.DocumentStatus = IgWordDocumentStatus.NoDocumentLoaded;
                this.IsDocumentViewSplit = false;
                this.IsEditorVisible = false;
                this.IsUiPartEnabled = false;

                SetTitle();
                RaiseCanExecuteChanged();
            }
        }

        private void LoadTemplate(string templateName)
        {
            UpdateAdaptersRefreshTrigger(RichTextAdapterRefreshTrigger.Explicit,
                                            RichTextAdapterRefreshTrigger.Explicit,
                                            RichTextAdapterRefreshTrigger.Explicit,
                                            RichTextAdapterRefreshTrigger.ContentChanged);

            if (!string.IsNullOrEmpty(templateName))
            {
                WordData = (byte[])ResourceStrings.ResourceStrings.ResourceManager.GetObject(templateName);
                this.DocumentStatus = IgWordDocumentStatus.TemplateNotModified;
            }
            else
            {
                ClearDataProperties();
                this.DocumentStatus = IgWordDocumentStatus.BlankNotModified;
            }
        }

        private void UpdateTitle(bool addStart)
        {
            var findText = " - " + ResourceStrings.ResourceStrings.Text_ApplicationTitle;

            if (addStart)
            {
                Title = Title.Replace(findText, "*" + findText);
            }
            else
            {
                Title = Title.Replace("*" + findText, findText);
            }
        }

        private void ExecuteDecreaseZoomLevel()
        {
            var expected = Math.Truncate(this.zoomLevel * 10) / 10;

            if (this.zoomLevel != expected && expected >= 0.2)
            {
                ZoomLevel = expected;
                return;
            }

            var nextValue = Math.Round(this.zoomLevel - 0.1, 1);

            if (nextValue >= 0.2)
                ZoomLevel = nextValue;
        }

        private void ExecuteIncreaseZoomLevel()
        {
            var expected = (Math.Truncate(this.zoomLevel * 10) / 10) + 0.1;

            if (this.zoomLevel != expected && expected <= 4)
            {
                ZoomLevel = expected;
                return;
            }

            var nextValue = Math.Round(this.zoomLevel + 0.1, 1);

            if (nextValue <= 4)
                ZoomLevel = nextValue;
        }

        private void ExecuteResetZoomLevel()
        {
            ZoomLevel = 1;
        }

        private void ExecuteShowInsertPictureDialog(RichTextDocument document)
        {
            if (document == null)
                return;

            string selectedFilePath;
            var filters = ShellParameters.FileDialogImageFilter;

            var result = dialogService.ShowOpenFileDialog(null, out selectedFilePath, filters);

            if (result == InteractionResult.Ok)
            {
                string error;

                ImageNode node = new ImageNode();
                MemoryStream memStream = new MemoryStream();
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                BitmapImage image = null;
                try
                {
                    image = new BitmapImage(new Uri(String.Format(selectedFilePath), UriKind.Absolute));
                }
                catch (NotSupportedException)
                {
                    messageBoxService.Show(ResourceStrings.ResourceStrings.Text_Warning, ResourceStrings.ResourceStrings.Mg_NotSuppportedFileFormat, MessageBoxButtons.Ok);
                    return;
                }
                encoder.Frames.Add(BitmapFrame.Create(image));
                encoder.Save(memStream);
                node.Image = new RichTextImage(memStream.GetBuffer(), RichTextImageFormat.Jpeg);

                document.InsertContent(selection.DocumentSpan.Offset, node, out error);
            }
        }

        private bool CanExecuteShowFindReplaceDialog(FindReplaceManager findReplaceManager)
        {
            return findReplaceManager != null && this.IsUiPartEnabled;
        }

        private bool CanExecuteShowFormattingDialog(RichTextDocument richTextDocument)
        {
            return richTextDocument != null && this.IsUiPartEnabled;
        }
        #endregion //Commands

        #region Events

        private void OnFileSaved(string filePath)
        {
            this.FileName = filePath;
            this.DocumentStatus = IgWordDocumentStatus.ExisitngNotModified;
            this.recentFilesService.AddFile(filePath, true);

            PushChangesToFile(filePath);

            if (this.isNewTemplateRequested)
            {
                var tempDocumentName = string.Format(ResourceStrings.ResourceStrings.Text_TempDocumentName, 1);

                LoadTemplate(this.selectedTemplateName);
                SetTitle(tempDocumentName);

                this.FileNameWithoutExtension = tempDocumentName;

                this.isNewTemplateRequested = false;
                this.selectedTemplateName = string.Empty;
            }
            else
            {
                this.FileNameWithoutExtension = Path.GetFileNameWithoutExtension(FileName);

                SetTitle(this.FileNameWithoutExtension);
            }

            if (IsBackstageOpened)
                IsBackstageOpened = false;

            if (closeRequested)
                CloseDialog();
        }

        private void OnFileLoaded(string filePath)
        {
            try
            {
                if (DocumentStatus == IgWordDocumentStatus.BlankModified || DocumentStatus == IgWordDocumentStatus.ExistingModified)
                {
                    var result = this.messageBoxService.Show(ResourceStrings.ResourceStrings.Text_ApplicationTitle, string.Format(ResourceStrings.ResourceStrings.Msg_WantToSaveChangesLong, this.FileNameWithoutExtension), MessageBoxButtons.YesNoCancel);

                    if (result == InteractionResult.Yes)
                    {
                        ExecuteSaveDocument();
                        return;
                    }
                    else if (result == InteractionResult.Cancel)
                    {
                        this.IsBackstageOpened = false;
                        return;
                    }
                }

                this.FileName = filePath;
                var extention = Path.GetExtension(filePath);

                switch (extention)
                {
                    case ".rtf":
                        UpdateAdaptersRefreshTrigger(RichTextAdapterRefreshTrigger.Explicit,
                                                     RichTextAdapterRefreshTrigger.ContentChanged,
                                                     RichTextAdapterRefreshTrigger.Explicit,
                                                     RichTextAdapterRefreshTrigger.Explicit);
                        RtfData = File.ReadAllText(filePath); break;
                    case ".docx":
                        UpdateAdaptersRefreshTrigger(RichTextAdapterRefreshTrigger.Explicit,
                                                    RichTextAdapterRefreshTrigger.Explicit,
                                                    RichTextAdapterRefreshTrigger.Explicit,
                                                    RichTextAdapterRefreshTrigger.ContentChanged);
                        WordData = File.ReadAllBytes(filePath); break;
                    case ".txt":
                        UpdateAdaptersRefreshTrigger(RichTextAdapterRefreshTrigger.ContentChanged,
                                                     RichTextAdapterRefreshTrigger.Explicit,
                                                     RichTextAdapterRefreshTrigger.Explicit,
                                                     RichTextAdapterRefreshTrigger.Explicit);
                        TextData = File.ReadAllText(filePath); break;
                    case ".html":
                        UpdateAdaptersRefreshTrigger(RichTextAdapterRefreshTrigger.Explicit,
                                                     RichTextAdapterRefreshTrigger.Explicit,
                                                     RichTextAdapterRefreshTrigger.ContentChanged,
                                                     RichTextAdapterRefreshTrigger.Explicit);
                        HtmlData = File.ReadAllText(filePath); break;
                    default: messageBoxService.Show(ResourceStrings.ResourceStrings.Text_Warning, ResourceStrings.ResourceStrings.Mg_NotSuppportedFileFormat); break;
                }

                this.DocumentStatus = IgWordDocumentStatus.ExisitngNotModified;
                this.IsEditorVisible = true;

                if (IsBackstageOpened)
                    IsBackstageOpened = false;

                this.IsUiPartEnabled = true;

                this.FileNameWithoutExtension = Path.GetFileNameWithoutExtension(FileName);

                SetTitle(this.FileNameWithoutExtension);
                RaiseCanExecuteChanged();
            }
            catch (Exception ex)
            {
                messageBoxService.Show(ResourceStrings.ResourceStrings.Text_Exception, ex.Message);
            }
        }

        private void OnNewDocumentEvent(string obj)
        {
            CreateNewDocument(obj);
            RaiseCanExecuteChanged();
        }

        private void OnCharacterSettingsUpdatedEvent(CharacterSettings settings)
        {
            string error;
            this.Selection.Document.ApplyCharacterSettings(this.Selection.DocumentSpan, settings, out error);
        }

        private void OnParagraphSettingsUpdatedEvent(ParagraphSettings settings)
        {
            string error;
            this.Selection.Document.ApplyParagraphSettings(this.Selection.DocumentSpan, settings, out error);
        }

        #endregion //Events

        #region Private Methods

        private void UpdateAdaptersRefreshTrigger(RichTextAdapterRefreshTrigger text, RichTextAdapterRefreshTrigger rtf, RichTextAdapterRefreshTrigger html, RichTextAdapterRefreshTrigger word)
        {
            TextAdapterRefreshTrigger = text;
            RtfAdapterRefreshTrigger = rtf;
            HtmlAdapterRefreshTrigger = html;
            WordAdapterRefreshTrigger = word;
        }

        private void DeactivateAdaptersRefreshTrigger()
        {
            TextAdapterRefreshTrigger = RichTextAdapterRefreshTrigger.Explicit;
            RtfAdapterRefreshTrigger = RichTextAdapterRefreshTrigger.Explicit;
            HtmlAdapterRefreshTrigger = RichTextAdapterRefreshTrigger.Explicit;
            WordAdapterRefreshTrigger = RichTextAdapterRefreshTrigger.Explicit;
        }

        private void UpdateAutoSavedState()
        {
            AutoSaved = true;
            AutoSaved = false;
        }

        private void PushChangesToFile(string filePath)
        {
            try
            {
                var extention = Path.GetExtension(filePath);

                switch (extention)
                {
                    case ".rtf":
                        UpdateAdaptersRefreshTrigger(RichTextAdapterRefreshTrigger.Explicit, RichTextAdapterRefreshTrigger.ContentChanged, RichTextAdapterRefreshTrigger.Explicit, RichTextAdapterRefreshTrigger.Explicit);
                        File.WriteAllText(filePath, RtfData);
                        break;
                    case ".docx":
                        File.WriteAllBytes(filePath, WordData == null ? new byte[0] : WordData);
                        break;
                    case ".txt":
                        UpdateAdaptersRefreshTrigger(RichTextAdapterRefreshTrigger.ContentChanged, RichTextAdapterRefreshTrigger.Explicit, RichTextAdapterRefreshTrigger.Explicit, RichTextAdapterRefreshTrigger.Explicit);
                        File.WriteAllText(filePath, TextData);
                        break;
                    case ".html":
                        UpdateAdaptersRefreshTrigger(RichTextAdapterRefreshTrigger.Explicit, RichTextAdapterRefreshTrigger.Explicit, RichTextAdapterRefreshTrigger.ContentChanged, RichTextAdapterRefreshTrigger.Explicit);
                        File.WriteAllText(filePath, HtmlData);
                        break;
                }

                UpdateAdaptersRefreshTrigger(RichTextAdapterRefreshTrigger.Explicit, RichTextAdapterRefreshTrigger.Explicit, RichTextAdapterRefreshTrigger.Explicit, RichTextAdapterRefreshTrigger.ContentChanged);
            }
            catch (Exception ex)
            {

                messageBoxService.Show(ResourceStrings.ResourceStrings.Text_Exception, ex.Message, MessageBoxButtons.Ok);
            }
        }

        private void ClearDataProperties()
        {
            RtfData = string.Empty;
            TextData = string.Empty;
            HtmlData = string.Empty;
            WordData = null;
        }

        private void CreateNewDocument(string templateName)
        {
            var tempDocumentName = string.Format(ResourceStrings.ResourceStrings.Text_TempDocumentName, 1);

            if (DocumentStatus == IgWordDocumentStatus.NoDocumentLoaded
                || DocumentStatus == IgWordDocumentStatus.ExisitngNotModified
                || DocumentStatus == IgWordDocumentStatus.BlankNotModified
                || DocumentStatus == IgWordDocumentStatus.TemplateNotModified)
            {

                LoadTemplate(templateName);
                SetTitle(tempDocumentName);

                this.IsBackstageOpened = false;
                this.IsEditorVisible = true;
                this.FileNameWithoutExtension = tempDocumentName;
            }
            else
            {
                var result = this.messageBoxService.Show(ResourceStrings.ResourceStrings.Text_ApplicationTitle, string.Format(ResourceStrings.ResourceStrings.Msg_WantToSaveChanges, tempDocumentName), MessageBoxButtons.YesNoCancel);

                if (result == InteractionResult.Yes)
                {
                    this.isNewTemplateRequested = true;
                    this.selectedTemplateName = templateName;

                    ExecuteSaveDocument();
                }
                else if (result == InteractionResult.No)
                {
                    LoadTemplate(templateName);
                    SetTitle(tempDocumentName);

                    this.FileNameWithoutExtension = tempDocumentName;
                    this.IsBackstageOpened = false;
                }
                else
                {
                    IsBackstageOpened = false;
                }
            }

            this.IsUiPartEnabled = true;
        }

        private bool CanCloseDocument(bool askForSaveLocation)
        {
            if (DocumentStatus == IgWordDocumentStatus.NoDocumentLoaded
                || DocumentStatus == IgWordDocumentStatus.BlankNotModified
                || DocumentStatus == IgWordDocumentStatus.TemplateNotModified
                || DocumentStatus == IgWordDocumentStatus.ExisitngNotModified)

                return true;

            InteractionResult msgCloseResult = InteractionResult.Cancel;

            if (DocumentStatus == IgWordDocumentStatus.ExistingModified)
            {
                // check if the user is closing an opened document
                msgCloseResult = this.messageBoxService.Show(
                    ResourceStrings.ResourceStrings.Text_ApplicationTitle,
                    string.Format(ResourceStrings.ResourceStrings.Msg_WantToSaveChanges,
                        fileName.Substring(fileName.LastIndexOf('\\') + 1)),
                    MessageBoxButtons.YesNoCancel);
            }
            else
            {
                // check if the user is closing a new document
                var tempDocumentName = string.Format(ResourceStrings.ResourceStrings.Text_TempDocumentName, 1);
                msgCloseResult = this.messageBoxService.Show(
                    ResourceStrings.ResourceStrings.Text_ApplicationTitle,
                    string.Format(ResourceStrings.ResourceStrings.Msg_WantToSaveChanges, tempDocumentName),
                    MessageBoxButtons.YesNoCancel);
            }

            if (msgCloseResult == InteractionResult.Cancel)
            {
                return false;
            }

            if (msgCloseResult == InteractionResult.No)
            {
                ClearDataProperties();
                return true;
            }

            if (msgCloseResult == InteractionResult.Yes)
            {
                if (askForSaveLocation)
                {
                    string fileNameTemp;
                    var filters = ShellParameters.FileDialogWordFilter;
                    dialogService.ShowSaveFileDialog(string.Empty, out fileNameTemp, filters);
                    ClearDataProperties();
                    return true;
                }
                else
                {
                    ExecuteSaveDocument();
                    closeRequested = true;
                    return false;
                }
            }

            return true;
        }

        private void MarkDocumentAsModified()
        {
            if (isEditorVisible)
            {
                if (documentStatus != IgWordDocumentStatus.BlankModified && documentStatus != IgWordDocumentStatus.ExistingModified)
                {
                    if (documentStatus == IgWordDocumentStatus.BlankNotModified || documentStatus == IgWordDocumentStatus.TemplateNotModified)
                        documentStatus = IgWordDocumentStatus.BlankModified;
                    else
                        documentStatus = IgWordDocumentStatus.ExistingModified;

                    UpdateTitle(true);
                }
            }
        }

        private void RaiseCanExecuteChanged()
        {
            this.ShowFindDialogCommand.RaiseCanExecuteChanged();
            this.ShowReplaceDialogCommand.RaiseCanExecuteChanged();
            this.ShowParagraphDialogCommand.RaiseCanExecuteChanged();
            this.ShowFontDialogCommand.RaiseCanExecuteChanged();
            this.ShowHyperlinkDialogCommand.RaiseCanExecuteChanged();
            this.ShowInsertPictureDialogCommand.RaiseCanExecuteChanged();
            this.ShowInsertTableDialogCommand.RaiseCanExecuteChanged();
        }

        private void SetTitle(string title = null)
        {
            if (string.IsNullOrEmpty(title))
            {
                this.Title = ResourceStrings.ResourceStrings.Text_ApplicationTitle;
            }
            else
            {
                this.Title = title + " - " + ResourceStrings.ResourceStrings.Text_ApplicationTitle;
            }
        }

        #endregion

        #region IDialogAware Members

        public bool CanCloseDialog()
        {
            if (closeRequested)
            {
                closeRequested = false;
                return true;
            }

            return CanCloseDocument(false);
        }

        public event Action RequestClose;

        void CloseDialog()
        {
            if (RequestClose != null)
            {
                closeRequested = true;
                RequestClose();
            }
        }

        #endregion //IDialogAware Members

        #region IViewModel

        private string title;
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
                NotifyPropertyChanged();
            }
        }

        #endregion //IViewModel
    }
}
