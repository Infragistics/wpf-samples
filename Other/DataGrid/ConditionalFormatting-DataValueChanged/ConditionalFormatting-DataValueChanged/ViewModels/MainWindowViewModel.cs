using FinancialDataGrid.Services.Finance;
using FinancialDataGrid.Services.Finance.Business;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.Generic;

namespace ConditionalFormatting_DataValueChanged.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        readonly IFinancialDataService _service = new FinancialDataService();

        private bool _isTimerRunning = true;
        public bool CanUpdatePrices
        {
            get { return _isTimerRunning; }
            set { SetProperty(ref _isTimerRunning, value); }
        }

        private List<FinancialRecord> _financialData;
        public List<FinancialRecord> FinancialData
        {
            get { return _financialData; }
            set { SetProperty(ref _financialData, value); }
        }

        private int _frequency = 250;
        public int Frequency
        {
            get { return _frequency; }
            set { SetProperty(ref _frequency, value); }
        }

        private int _numberOfRecords = 1000;
        public int NumberOfRecords
        {
            get { return _numberOfRecords; }
            set { SetProperty(ref _numberOfRecords, value, () => UpdateRecordCount()); }
        }

        private DelegateCommand _stopTimerCommand;
        public DelegateCommand StopTimerCommand =>
            _stopTimerCommand ?? (_stopTimerCommand = new DelegateCommand(ExecuteStopTimerCommand, CanExecuteStopTimerCommand)).ObservesProperty(() => CanUpdatePrices);

        private DelegateCommand _livePricesCommand;
        public DelegateCommand LivePricesCommand =>
            _livePricesCommand ?? (_livePricesCommand = new DelegateCommand(ExecuteLivePricesCommand)).ObservesCanExecute(() => CanUpdatePrices);

        private DelegateCommand _liveAllPricesCommand;
        public DelegateCommand LiveAllPricesCommand =>
            _liveAllPricesCommand ?? (_liveAllPricesCommand = new DelegateCommand(ExecuteLiveAllPricesCommand)).ObservesCanExecute(() => CanUpdatePrices);

        public MainWindowViewModel()
        {
            Initialize();
        }

        async void Initialize()
        {
            FinancialData = await _service.GetDataAsync(NumberOfRecords);
        }

        void ExecuteLivePricesCommand()
        {
            CanUpdatePrices = false;
            _service.StartUpdatingPrices(FinancialData, Frequency);
        }

        void ExecuteLiveAllPricesCommand()
        {
            CanUpdatePrices = false;
            _service.StartUpdatingAllPrices(FinancialData, Frequency);
        }

        async void UpdateRecordCount()
        {
            FinancialData = await _service.GetDataAsync(NumberOfRecords);
        }

        void ExecuteStopTimerCommand()
        {
            CanUpdatePrices = true;
            _service.StopUpdatingPrices();
        }

        private bool CanExecuteStopTimerCommand()
        {
            return !CanUpdatePrices;
        }
    }
}
