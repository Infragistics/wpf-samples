namespace Infragistics.Samples.Shared.Models 
{
    #region Exponential Functions

    /// <summary>
    /// Exponential Function that generates NumericDataPoint using the following forumla:
    /// <para> x = ValueStart + (index * ValueInterval) </para>
    /// <para> y = Multiplier * Math.Pow(Base, x) + Shift </para>
    /// </summary>
    public class ExponentialFunction : VariableFunctionBase
    {
        public ExponentialFunction()
        {
            _base = 2;
        }
        private double _base;
        public double Base { get { return _base; } set { _base = value; OnPropertyChanged("Exponent"); } }

        public override NumericDataPoint GenerateDataPoint(int index)
        {
            double x = this.ValueStart + (index * this.ValueInterval);
            double y = this.Multiplier * System.Math.Pow(Base, x) + this.Shift;
            return new NumericDataPoint { X = x, Y = y, Index = index };
        }
    }

    #endregion
   
}