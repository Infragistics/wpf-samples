
namespace Infragistics.Samples.Shared.Models
{
    #region Periodic Functions

    /// <summary>
    /// Periodic Function base class   
    /// </summary>
    public abstract class PeriodicFunctionBase : Function
    {
        protected PeriodicFunctionBase()
        {
            _amplitude = 1;
            _angleStart = 0;
            _angleInterval = 15;
            _shift = 0;
        }

        private double _amplitude;
        private double _angleStart;
        private double _angleInterval;
        private double _shift;

        public double Amplitude { get { return _amplitude; } set { _amplitude = value; OnPropertyChanged("Amplitude"); } }
        public double AngleStart { get { return _angleStart; } set { _angleStart = value; OnPropertyChanged("AngleStart"); } }
        public double AngleInterval { get { return _angleInterval; } set { _angleInterval = value; OnPropertyChanged("AngleInterval"); } }
        public double Shift { get { return _shift; } set { _shift = value; OnPropertyChanged("Shift"); } }

    }

    /// <summary>
    /// Sin Function that generates NumericDataPoint using the following forumla:
    /// <para> x = AngleStart + (index * AngleInterval) </para>
    /// <para> y = (Amplitude * System.Math.Sin(x)) + Shift </para>
    /// </summary>
    public class SinFunction : PeriodicFunctionBase
    {
        public override NumericDataPoint GenerateDataPoint(int index)
        {
            double x = this.AngleStart + (index * this.AngleInterval);
            double y = this.Amplitude * System.Math.Sin(x * System.Math.PI / 180) + this.Shift;
            return new NumericDataPoint { X = x, Y = y, Index = index };
        }
    }

    /// <summary>
    /// Cos Function that generates NumericDataPoint using the following forumla:
    /// <para> x = AngleStart + (index * AngleInterval) </para>
    /// <para> y = (Amplitude * System.Math.Cos(x)) + Shift </para>
    /// </summary>
    public class CosFunction : PeriodicFunctionBase
    {
        public override NumericDataPoint GenerateDataPoint(int index)
        {
            double x = this.AngleStart + (index * this.AngleInterval);
            double y = this.Amplitude * System.Math.Cos(x * System.Math.PI / 180) + this.Shift;
            return new NumericDataPoint { X = x, Y = y, Index = index };
        }
    }
    #endregion
   
}