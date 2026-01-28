using System.Collections.ObjectModel;

namespace Infragistics.Framework.Models.Finacial
{
    public class FinancialDataViewModel : ObservableModel
    {
        private ObservableCollection<FinancialDataPoint> _dataItems;
        public ObservableCollection<FinancialDataPoint> DataItems
        {
            get
            {
                return _dataItems;
            }
            set
            {
                if (_dataItems != value)
                {
                    _dataItems = value;
                    OnPropertyChanged(nameof(DataItems));
                }
            }
        }

        private ObservableCollection<FinancialDataPoint> _selectedDataItems;
        public ObservableCollection<FinancialDataPoint> SelectedDataItems
        {
            get
            {
                return _selectedDataItems;
            }
            set
            {
                if (_selectedDataItems != value)
                {
                    _selectedDataItems = value;
                    OnPropertyChanged(nameof(SelectedDataItems));
                }
            }
        }
        public FinancialDataViewModel()
        {
            GenerateSampleData();
        }
        private void GenerateSampleData()
        {
            if (DataItems == null)
            {
                DataItems = new ObservableCollection<FinancialDataPoint>
                {
                    new FinancialDataPoint {Spending = 20, Budget = 60, Label = "Administration"},
                    new FinancialDataPoint {Spending = 80, Budget = 40, Label = "Sales"},
                    new FinancialDataPoint {Spending = 30, Budget = 60, Label = "IT"},
                    new FinancialDataPoint {Spending = 80, Budget = 40, Label = "Marketing"},
                    new FinancialDataPoint {Spending = 40, Budget = 60, Label = "Development"},
                    new FinancialDataPoint {Spending = 60, Budget = 20, Label = "Customer Support"}
                };
            }
        }
    }
    public class FinancialDataPoint : ObservableModel
    {
        private string _label;
        public string Label
        {
            get
            {
                return _label;
            }
            set
            {
                if (_label != value)
                {
                    _label = value;
                    OnPropertyChanged(nameof(Label));
                }
            }
        }

        private double _spending;
        public double Spending
        {
            get
            {
                return _spending;
            }
            set
            {
                if (_spending != value)
                {
                    _spending = value;
                    OnPropertyChanged(nameof(Spending));
                }
            }
        }

        private double _budget;
        public double Budget
        {
            get { return _budget; }
            set
            {
                if (_budget != value)
                {
                    _budget = value;
                    OnPropertyChanged(nameof(Budget));
                }
            }
        }
    }
}
