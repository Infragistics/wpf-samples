using IgWord.Infrastructure;
using IgWord.Infrastructure.Dialogs;
using Infragistics.Documents.RichText;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System;

namespace IgWord.Dialogs
{
    public class ParagraphDialogViewModel : ViewModelBase, IDialogAware, INavigationAware
    {
        #region Member Variables

        private ParagraphSettings paragraphSettings;
        private IEventAggregator eventAggragator;

        #endregion //Member Variables

        #region Public Properties
        public ParagraphSettings ParagraphSettings
        {
            get { return paragraphSettings; }
            set { paragraphSettings = value; NotifyPropertyChanged(); }
        }

        public string HtmlData { get; private set; }

        public DelegateCommand OkCommand { get; private set; }
        public DelegateCommand CancelCommand { get; private set; }

        #endregion //Public Properties

        #region Constructor

        public ParagraphDialogViewModel(IEventAggregator eventAggragator)
        {
            //Previous Paragraph 
            //Following Paragraph 

            this.Title = ResourceStrings.ResourceStrings.Dlg_Paragraph_Title;
            this.HtmlData = string.Format(ResourceStrings.ResourceStrings.Dlg_Paragrapth_PreviewText, ResourceStrings.ResourceStrings.Dlg_Paragrapth_PreviewSampleText);
            this.IconSource = "pack://application:,,,/IgWord.Infrastructure;component/Images/Paragraph.ico";

            OkCommand = new DelegateCommand(ExecuteOk);
            CancelCommand = new DelegateCommand(ExecuteCancel);

            this.eventAggragator = eventAggragator;
        }

        #endregion //Constructor

        #region Commands

        private void ExecuteOk()
        {
            this.eventAggragator.GetEvent<ParagraphSettingsUpdatedEvent>().Publish(this.paragraphSettings);

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

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            this.ParagraphSettings = (ParagraphSettings)navigationContext.Parameters[ParagraphDialogParameters.ParagraphSettingsKey];

            this.ParagraphSettings.RightToLeft = true;
        }

        #endregion //INavigationAware
    }
}
