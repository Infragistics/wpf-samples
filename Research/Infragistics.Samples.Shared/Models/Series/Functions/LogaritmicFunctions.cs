namespace Infragistics.Samples.Shared.Models 
{
    #region Logaritmic Functions

    /// <summary>
    /// Logaritmic Function base class 
    /// </summary>
    public abstract class LogaritmicFunctionBase : VariableFunctionBase
    {
        protected LogaritmicFunctionBase()
        {
            _logBase = 2;
        }
        private double _logBase;
        public double LogBase { get { return _logBase; } set { _logBase = value; OnPropertyChanged("LogBase"); } }

    }

    /// <summary>
    /// Log Inverse Function that generates NumericDataPoint using the following formula:
    /// <para> x = ValueStart + (index * ValueInterval) </para>
    /// <para> y = (Multiplier * System.Math.Log(x, LogBase)) + Shift </para>
    /// </summary>
    public class LogFunction : LogaritmicFunctionBase
    {
        public LogFunction()
        {
            this.ValueStart = 1;
        }
        public override NumericDataPoint GenerateDataPoint(int index)
        {
            double x = this.ValueStart + (index * this.ValueInterval);
            double y = this.Multiplier * System.Math.Log(x, this.LogBase) + this.Shift;
            return new NumericDataPoint { X = x, Y = y, Index = index };
        }
    }

    /// <summary>
    /// Log Inverse Function that generates NumericDataPoint using the following forumla:
    /// <para> x = ValueStart + (index * ValueInterval) </para>
    /// <para> y = Multiplier * System.Math.Pow(LogBase, x) + Shift </para>
    /// </summary>
    public class LogInverseFunction : LogaritmicFunctionBase
    {
        public LogInverseFunction()
        {
            this.ValueStart = 0;
        }
        public override NumericDataPoint GenerateDataPoint(int index)
        {
            double x = this.ValueStart + (index * this.ValueInterval);
            double y = this.Multiplier * System.Math.Pow(this.LogBase, x) + this.Shift;
            return new NumericDataPoint { X = x, Y = y, Index = index };
        }

    }


    #endregion
}