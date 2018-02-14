using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace IgOutlook.Infrastructure.Dialogs
{
    /// <summary>
    /// Interaction logic for Dialog.xaml
    /// </summary>
    public partial class RibbonDialog : IDialog
    {
        #region Members

        IDialogAware _viewModel;

        #endregion //Members

        #region Constructors

        public RibbonDialog()
        {
            InitializeComponent();
            Closed += RibbonDialog_Closed;
            Closing += RibbonDialog_Closing;
        }

        #endregion //Constructors

        #region IDialog

        public void Show(bool isModal = true)
        {
            base.Show();

            var fe = _contentRegion.Content as FrameworkElement;
            if (fe != null)
            {
                _viewModel = fe.DataContext as IDialogAware;

                if (_viewModel != null)
                {
                    _viewModel.RequestClose += ViewModel_RequestClose;

                    var viewModelBase = (ViewModelBase)fe.DataContext;

                    if (!string.IsNullOrEmpty(viewModelBase.IconSource))
                    {
                        Uri iconUri = new Uri(viewModelBase.IconSource, UriKind.RelativeOrAbsolute);
                        this.Icon = BitmapFrame.Create(iconUri);
                    }

                    viewModelBase.PropertyChanged += (s, a) =>
                    {
                        if (a.PropertyName == "Title")
                            if (viewModelBase.Title != null)
                                this.Title = viewModelBase.Title;
                    };

                    viewModelBase.RefreshTitle();
                }
            }

            CalculateDialogWindowsPosition();
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

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #endregion //Event Handlers

        #region Methods

        private void CalculateDialogWindowsPosition()
        {
            var windows = Application.Current.Windows;

            int x = 0;
            foreach (var window in windows)
            {
                if (window is IDialog)
                {
                    if (window == this)
                    {
                        if (x > 1) //don't want the Shell
                        {
                            for (int i = x - 1; i > 0; i--)
                            {
                                if (windows[i] is IDialog)
                                {
                                    var priorWindow = windows[i];
                                    Left = priorWindow.Left - 40;
                                    Top = priorWindow.Top + 40;
                                    break;
                                }
                            }

                        }

                        break;
                    }
                }

                x++;
            }
        }

        #endregion //Methods

    }
}
