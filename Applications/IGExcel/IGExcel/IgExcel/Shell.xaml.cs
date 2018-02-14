using System;
using IgExcel.Infrastructure.Dialogs;
using Infragistics.Windows.Ribbon;

namespace IgExcel
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

			// TFS 191000 - Workaround: initialize the value of the IsBackstageOpen property to true when the ribbon is loaded
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
    }
}
