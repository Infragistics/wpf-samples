using System.Linq;
using System.Windows.Media;

namespace Infragistics.Samples.Shared.Calculators
{
    public class ProbabilityCalculator
    {
        public DoubleCollection Space { get; set; }
        public ProbabilityEvent Event { get; set; }

        public double Probability { get; private set; }

        public int SampleSpace
        {
            get { return Space == null ? 0 : Space.Count; }
        }
        public double Compute(ProbabilityEvent probEvent)
        {
            if (SampleSpace == 0) return 0;

            int numOutcomes = Space.Count(outcome => IsValid(outcome, probEvent));

            double probability = numOutcomes / (double)SampleSpace * 100.0;

            return probability;
        }
        public bool IsValid(double outome, ProbabilityEvent probEvent)
        {
            switch (probEvent.Condtion)
            {
                case ProbabilityCondtion.Equal:
                    return outome == probEvent.Value;
                case ProbabilityCondtion.GreaterOrEqual:
                    return outome >= probEvent.Value;
                case ProbabilityCondtion.Greater:
                    return outome > probEvent.Value;
                case ProbabilityCondtion.NotEqual:
                    return outome != probEvent.Value;
                case ProbabilityCondtion.LessOrEqual:
                    return outome <= probEvent.Value;
                default:
                    return outome < probEvent.Value;
            }
        }
    }

    public class ProbabilityEvent
    {
        public ProbabilityEvent()
        {
            this.Condtion = ProbabilityCondtion.Equal;
        }
        public ProbabilityEvent(double value)
        {
            this.Condtion = ProbabilityCondtion.Equal;
            this.Value = value;
        }
        public double Value { get; set; }
        public ProbabilityCondtion Condtion { get; set; }
    }
    public enum ProbabilityCondtion
    {
        NotEqual,
        Equal,
        Less,
        LessOrEqual,
        Greater,
        GreaterOrEqual
    }
}