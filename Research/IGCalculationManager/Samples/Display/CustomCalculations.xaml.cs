using Infragistics.Calculations;

namespace IGCalculationManager.Samples.Display
{
    public partial class CustomCalculations : Infragistics.Samples.Framework.SampleContainer
    {
        public CustomCalculations()
        {
            InitializeComponent();

            CustomCalculationFunction absAvg = new CustomCalculationFunction("AbsAvg",
               (x, y) => (System.Math.Abs(x) + System.Math.Abs(y)) / 2);
            calcManager.RegisterUserDefinedFunction(absAvg);
        }
    }
}
