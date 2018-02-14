using IgWord.Infrastructure.Dialogs;
using Infragistics.Documents.RichText.Word;
using Infragistics.Windows.Ribbon;
using System;
using System.Windows;

namespace IgWord
{
    public partial class Shell : XamRibbonWindow
    {
        public Shell(ShellViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;

            if (viewModel != null)
            {
                (viewModel as IDialogAware).RequestClose += ViewModel_RequestClose;
            }

			// TFS 190054 - Workaround: initialize the value of the IsBackstageOpen property to true when the ribbon is loaded
			this.Loaded += (s, a) =>
			{
				Dispatcher.BeginInvoke((Action)(() => viewModel.IsBackstageOpened = true));
			};

            this.Closing += (s, a) =>
            {
                if (!(viewModel as IDialogAware).CanCloseDialog())
                    a.Cancel = true;
            };

            this.Closed += (s, a) =>
            {
                (viewModel as IDialogAware).RequestClose -= ViewModel_RequestClose;
            };
        }

        void ViewModel_RequestClose()
        {
            this.Close();
        }

        private void DocumentAdapter_DocumentLoadError(object sender, Infragistics.Documents.RichText.Serialization.DocumentLoadErrorEventArgs e)
        {
            e.RethrowException = false;
            MessageBox.Show(
                ResourceStrings.ResourceStrings.Error_LoadingFile,
                ResourceStrings.ResourceStrings.Text_Error,
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }
}
