namespace Infragistics.Samples.Shared.Models
{
    #region Polynomial Functions
    /// <summary>
    /// Numeric Function base class 
    /// </summary>
    public abstract class PolynomialFunctionBase : NumericFunctionBase
    {
        protected PolynomialFunctionBase()
        {
            this.Degree = 0;
            //this.UpdateChanges();
        }

        protected int Degree { get; set; }

    }
    /// <summary>
    /// Constant Function that generates NumericDataPoint using the following forumla:
    /// <para> x = ValueStart + (index * ValueInterval) </para>
    /// <para> y = Constant </para>
    /// </summary>
    public class ConstantFunction : PolynomialFunctionBase
    {
        public ConstantFunction()
        {
            _constant = 1;
            this.Degree = 0;
        }
        private double _constant;
        public double Constant { get { return _constant; } set { _constant = value; OnPropertyChanged("Constant"); } }

        public override NumericDataPoint GenerateDataPoint(int index)
        {
            double x = this.ValueStart + (index * this.ValueInterval);
            double y = this.Constant;
            return new NumericDataPoint { X = x, Y = y, Index = index };
        }
    }

    /// <summary>
    /// Linear Function that generates NumericDataPoint using the following forumla:
    /// <para> x = ValueStart + (index * ValueInterval) </para>
    /// <para> y = (CoefficientX1 * System.Math.Pow(x, 1)) + Coefficient </para>
    /// </summary>
    public class LinearFunction : ConstantFunction
    {

        public LinearFunction()
        {
            _coefficientX1 = 1;
            this.Degree = 1;
            this.Constant = 0;
        }

        private double _coefficientX1;
        public double CoefficientX1 { get { return _coefficientX1; } set { _coefficientX1 = value; OnPropertyChanged("CoefficientX1"); } }

        public override NumericDataPoint GenerateDataPoint(int index)
        {
            double x = this.ValueStart + (index * this.ValueInterval);
            double y = (this.CoefficientX1 * System.Math.Pow(x, Degree)) +
                        this.Constant;
            return new NumericDataPoint { X = x, Y = y, Index = index };
        }
    }
    /// <summary>
    /// Quadratic Function that generates NumericDataPoint using the following forumla:
    /// <para> x = ValueStart + (index * ValueInterval) </para>
    /// <para> y = (CoefficientX2 * System.Math.Pow(x, 2)) +  </para>
    /// <para>     (CoefficientX1 * System.Math.Pow(x, 1)) + Coefficient </para>
    /// </summary>
    public class QuadraticFunction : LinearFunction
    {
        public QuadraticFunction()
        {
            this.CoefficientX2 = 1;
            this.CoefficientX1 = 0;
            this.Degree = 2;
            this.Constant = 0;
        }
        private double _coefficientX2;
        public double CoefficientX2 { get { return _coefficientX2; } set { _coefficientX2 = value; OnPropertyChanged("CoefficientX2"); } }

        public override NumericDataPoint GenerateDataPoint(int index)
        {
            double x = this.ValueStart + (index * this.ValueInterval);
            double y = (this.CoefficientX2 * System.Math.Pow(x, this.Degree)) +
                       (this.CoefficientX1 * System.Math.Pow(x, this.Degree - 1)) +
                        this.Constant;
            return new NumericDataPoint { X = x, Y = y, Index = index };
        }
    }

    /// <summary>
    /// Cubic Function that generates NumericDataPoint using the following forumla:
    /// <para> x = ValueStart + (index * ValueInterval) </para>
    /// <para> y = (CoefficientX3 * System.Math.Pow(x, 3)) +  </para>
    /// <para>     (CoefficientX2 * System.Math.Pow(x, 2)) +  </para>
    /// <para>     (CoefficientX1 * System.Math.Pow(x, 1)) + Coefficient </para>
    /// </summary>
    public class CubicFunction : QuadraticFunction
    {
        public CubicFunction()
        {
            this.CoefficientX3 = 1;
            this.CoefficientX2 = 0;
            this.CoefficientX1 = 0;
            this.Degree = 3;
            this.Constant = 0;
        }
        private double _coefficientX3;
        public double CoefficientX3 { get { return _coefficientX3; } set { _coefficientX3 = value; OnPropertyChanged("CoefficientX3"); } }


        public override NumericDataPoint GenerateDataPoint(int index)
        {
            double x = this.ValueStart + (index * this.ValueInterval);
            double y = (this.CoefficientX3 * System.Math.Pow(x, this.Degree)) +
                       (this.CoefficientX2 * System.Math.Pow(x, this.Degree - 1)) +
                       (this.CoefficientX1 * System.Math.Pow(x, this.Degree - 2)) +
                        this.Constant;
            return new NumericDataPoint { X = x, Y = y, Index = index };
        }
    }
    #endregion

}