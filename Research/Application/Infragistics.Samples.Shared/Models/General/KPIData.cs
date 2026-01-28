
 namespace Infragistics.Samples.Shared.Models
{
    public class KPIData : ObservableModel
    {
        
        private double _minimum;
        public double Minimum
        {
            get
            {
                return _minimum;
            }
            set
            {
                _minimum = value;
                this.OnPropertyChanged("Minimum");
            }
        }

        private double _maximum;
        public double Maximum
        {
            get
            {
                return _maximum;
            }
            set
            {
                _maximum = value;
                this.OnPropertyChanged("Maximum");
            }
        }

        private double _actualPerformance;
        public double ActualPerformance
        {
            get
            {
                return _actualPerformance;
            }
            set
            {
                _actualPerformance = value;
                this.OnPropertyChanged("ActualPerformance");
            }
        }

        private double _targetPerformance;
        public double TargetPerformance
        {
            get
            {
                return _targetPerformance;
            }
            set
            {
                _targetPerformance = value;
                this.OnPropertyChanged("TargetPerformance");
            }
        }

        private double _featuredMeasure;
        public double FeaturedMeasure
        {
            get
            {
                return _featuredMeasure;
            }
            set
            {
                _featuredMeasure = value;
                this.OnPropertyChanged("FeaturedMeasure");
            }
        }

        private double _comparativeMeasure;
        public double ComparativeMeasure
        {
            get
            {
                return _comparativeMeasure;
            }
            set
            {
                _comparativeMeasure = value;
                this.OnPropertyChanged("ComparativeMeasure");
            }
        }
    }
}
