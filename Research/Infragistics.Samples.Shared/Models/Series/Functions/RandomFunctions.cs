namespace Infragistics.Samples.Shared.Models
{
    public class RandomFunction : Function
    {

        public RandomFunction()
        {
            _generator = new System.Random();
            _current = ValueStart;

        }
        private readonly System.Random _generator;

        private double _current;
        private double _valueStart;
        public double ValueStart
        {
            get { return _valueStart; }
            set
            {
                if (_valueStart == value) return;
                _current = value;
                _valueStart = value; OnPropertyChanged("ValueStart");
            }
        }
        public override NumericDataPoint GenerateDataPoint(int index)
        {
            if (_generator.NextDouble() > .5)
            {
                _current += _generator.NextDouble();
            }
            else
            {
                _current -= _generator.NextDouble();
            }

            return new NumericDataPoint { X = index, Y = _current, Index = index };
        }
    }
    
}