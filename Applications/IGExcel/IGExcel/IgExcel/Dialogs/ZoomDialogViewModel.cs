using IgExcel.Infrastructure;
using IgExcel.Infrastructure.Dialogs;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.Generic;

namespace IgExcel.Dialogs
{
    public class ZoomDialogViewModel : ViewModelBase, IDialogAware, INavigationAware
    {
        #region Private Variables

        private IEventAggregator eventAggragator;
        private int customZoomLevel;
        private bool isCustomZoomLevel;
        private int? selectedPresetZoomLevel;

        #endregion //Private Variables

        #region Public Properties

        public List<int> ZooomLevels
        {
            get
            {
                return new List<int> { 200, 100, 75, 50, 25 };
            }
        }
        public int? SelectedPresetZoomLevel
        {
            get { return selectedPresetZoomLevel; }
            set { SetProperty<int?>(ref selectedPresetZoomLevel, value); }
        }
        public int CustomZoomLevel
        {
            get { return customZoomLevel; }
            set { SetProperty<int>(ref customZoomLevel, value); }
        }
        public bool IsCustomZoomLevel
        {
            get { return isCustomZoomLevel; }
            set { SetProperty<bool>(ref isCustomZoomLevel, value); }
        }

        public DelegateCommand OkCommand { get; private set; }
        public DelegateCommand CancelCommand { get; private set; }

        #endregion //Public Properties

        #region Constructor

        public ZoomDialogViewModel(IEventAggregator eventAggragator)
        {
            this.Title = ResourceStrings.ResourceStrings.Text_Zoom;
            this.IconSource = "pack://application:,,,/IgExcel.Infrastructure;component/Images/Zoom.ico";

            this.eventAggragator = eventAggragator;

            this.OkCommand = new DelegateCommand(ExecuteOk);
            this.CancelCommand = new DelegateCommand(ExecuteCancel);

            this.HookPropertyChanged(true);
           
        }

        #endregion //Constructor

        #region ZoomDialogViewModel_PropertyChanged

        private void ZoomDialogViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            this.HookPropertyChanged(false);

            if (e.PropertyName == "IsCustomZoomLevel")
            {
                if (this.IsCustomZoomLevel)
                {
                    this.SelectedPresetZoomLevel = null;
                }
            }
            else if (e.PropertyName == "SelectedPresetZoomLevel")
            {
                if (this.IsCustomZoomLevel)
                {
                    this.IsCustomZoomLevel = false;
                }

                this.CustomZoomLevel = this.selectedPresetZoomLevel.Value;
            }

            this.HookPropertyChanged(true);
        }

        #endregion //ZoomDialogViewModel_PropertyChanged

        #region Commands

        private bool CanExecuteOk()
        {
            return true;
        }

        private void ExecuteOk()
        {
            this.eventAggragator.GetEvent<CustomZoomLevelChangedEvent>().Publish(this.customZoomLevel);
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
            this.CustomZoomLevel = (int)navigationContext.Parameters[ZoomDialogViewParameters.ZoomLevelKey];

            this.IsCustomZoomLevel = !ZooomLevels.Contains(this.CustomZoomLevel);

            if (this.IsCustomZoomLevel == false)
                this.SelectedPresetZoomLevel = this.CustomZoomLevel;
        }

        #endregion //INavigationAware

        #region Private Methods

        private void HookPropertyChanged(bool hook)
        {
            if (hook)
            {
                this.PropertyChanged += ZoomDialogViewModel_PropertyChanged;
            }
            else
            {
                this.PropertyChanged -= ZoomDialogViewModel_PropertyChanged;
            }
        }

        #endregion //Private Methods
    }
}
