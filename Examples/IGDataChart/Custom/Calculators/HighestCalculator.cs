using System.Collections.Generic;
using Infragistics.Math.Calculators;

namespace IGDataChart.Custom.Calculators
{
    public class HighestCalculator : ValueCalculator
    {
        /// <summary>
        /// Calculates the highest value in the specified input.
        /// </summary>
        /// <param name="input">The input list of double values.</param>
        /// <returns></returns>
        public override double Calculate(IList<double> input)
        {
            if (input == null || input.Count == 0)
            {
                return double.NaN;
            }

            double highest = double.NegativeInfinity;
            foreach (double value in input)
            {
                highest = System.Math.Max(highest, value);
            }
            return highest;
        }
    }
}