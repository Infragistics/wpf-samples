using IgWord.Infrastructure;
using IgWord.Infrastructure.Dialogs;
using IgWord.Services;
using Infragistics.Documents.RichText;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.Generic;

namespace IgWord.Dialogs
{
    public class FontDialogViewModel : ViewModelBase, IDialogAware, INavigationAware
    {
        #region Member Variables
        private RichTextDocument document;
        private CharacterSettings characterSettings;
        private List<string> fontFamilies;
        private IEventAggregator eventAggragator;

        #endregion //Member Variables

        #region Public Properties
        public CharacterSettings CharacterSettings
        {
            get { return characterSettings; }
            set { characterSettings = value; NotifyPropertyChanged(); }
        }
        public List<string> FontFamilies
        {
            get { return fontFamilies; }
            set { fontFamilies = value; NotifyPropertyChanged(); }
        }
        public static List<double> FontSizes { get; private set; }
        public string TextData { get; private set; }
        public string ComplexTextData { get; private set; }
        public RichTextDocument Document
        {
            get { return document; }
            set { document = value; }
        }

        public DelegateCommand OkCommand { get; private set; }
        public DelegateCommand CancelCommand { get; private set; }
        #endregion //Public Properties

        #region Constructor
        public FontDialogViewModel(IEventAggregator eventAggragator, IFontService fontService)
        {
            this.Title = ResourceStrings.ResourceStrings.Dlg_Font_Title;
            this.IconSource = "pack://application:,,,/IgWord.Infrastructure;component/Images/Font.ico";

            TextData = "Sample";
            ComplexTextData = "عينة";
            OkCommand = new DelegateCommand(ExecuteOk);
            CancelCommand = new DelegateCommand(ExecuteCancel);

            FontSizes = fontService.GetFontSizes();
            FontFamilies = fontService.GetFontNames();

            this.eventAggragator = eventAggragator;
        }
        #endregion //Constructor

        #region Commands

        private void ExecuteOk()
        {
            this.eventAggragator.GetEvent<CharacterSettingsUpdatedEvent>().Publish(this.CharacterSettings);

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
            this.CharacterSettings = (CharacterSettings)navigationContext.Parameters[FontDialogParameters.CharacterSettingsKey];
        }

        #endregion //INavigationAware


    }
}
