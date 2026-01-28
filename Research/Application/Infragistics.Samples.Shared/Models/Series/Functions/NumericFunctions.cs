namespace Infragistics.Samples.Shared.Models
{
    /// <summary>
    /// Numeric Function base class 
    /// </summary>
    public abstract class NumericFunctionBase : Function
    {
        protected NumericFunctionBase()
        {
            _valueStart = 0;
            _valueInterval = 1;
        }

        private double _valueStart;
        public double ValueStart
        {
            get { return _valueStart; }
            set
            {
                if (_valueStart == value) return;
                _valueStart = value; OnPropertyChanged("ValueStart");
            }
        }
        private double _valueInterval;
        public double ValueInterval
        {
            get { return _valueInterval; }
            set
            {
                if (_valueInterval == value) return;
                if (_valueInterval != 0) return;
                _valueInterval = value;
                OnPropertyChanged("ValueInterval");
            }
        }

    }

    /// <summary>
    /// Variable Function that generates NumericDataPoint by multipling and shifting the Y value
    /// </summary>
    public abstract class VariableFunctionBase : NumericFunctionBase
    {
        protected VariableFunctionBase()
        {
            _multiplier = 1;
            _shift = 0;
        }
        private double _shift;
        public double Shift { get { return _shift; } set { _shift = value; OnPropertyChanged("Shift"); } }

        private double _multiplier;
        public double Multiplier { get { return _multiplier; } set { _multiplier = value; OnPropertyChanged("Multiplier"); } }

    }
    
}