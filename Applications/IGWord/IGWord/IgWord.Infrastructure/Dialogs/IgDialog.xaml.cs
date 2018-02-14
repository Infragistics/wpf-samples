using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace IgWord.Infrastructure.Dialogs
{
    public partial class IgDialog : IDialog
    {
        #region Members

        private IDialogAware _viewModel;

        #endregion //Members

        #region Constructors

        public IgDialog()
        {
            InitializeComponent();
            Closed += RibbonDialog_Closed;
            Closing += RibbonDialog_Closing;
        }

        #endregion //Constructors

        #region IDialog

        public void Show(bool isModal = true)
        {
            var fe = _contentRegion.Content as FrameworkElement;

            fe.SizeChanged += (s, a) =>
            {
                var sender = s as FrameworkElement;
                this.Width = sender.Width + 2;
                this.Height = sender.Height + 28;
            };

            this.Width = fe.Width + 2;
            this.Height = fe.Height + 28;

            if (fe != null)
            {
                _viewModel = fe.DataContext as IDialogAware;

                if (_viewModel != null)
                {
                    _viewModel.RequestClose += ViewModel_RequestClose;

                    var viewModelBase = (ViewModelBase)fe.DataContext;

                    if (viewModelBase != null)
                    {
                        if (!string.IsNullOrEmpty(viewModelBase.IconSource))
                        {
                            Uri iconUri = new Uri(viewModelBase.IconSource, UriKind.RelativeOrAbsolute);
                            this.Icon = BitmapFrame.Create(iconUri);
                        }

                        this.Title = viewModelBase.Title;
                    }
                }

            }

            this.WindowState = System.Windows.WindowState.Normal;

            if (isModal)
                base.ShowDialog();
            else
                base.Show();
        }

        #endregion //IDialog

        #region Event Handlers

        void ViewModel_RequestClose()
        {
            this.Close();
        }

        void RibbonDialog_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_viewModel != null && !_viewModel.CanCloseDialog())
                e.Cancel = true;
        }

        void RibbonDialog_Closed(object sender, EventArgs e)
        {
            if (_viewModel != null)
                _viewModel.RequestClose -= ViewModel_RequestClose;
        }

        #endregion //Event Handlers
       
    }
}
