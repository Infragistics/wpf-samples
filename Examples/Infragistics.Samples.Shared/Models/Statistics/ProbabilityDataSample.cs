using System.Collections.Generic;
using System.Windows.Media;
using Infragistics.Samples.Shared.Calculators;
using Infragistics.Samples.Shared.Models;

namespace Infragistics.Samples.Shared.Models
{
    public class ProbabilityDataSample
    {
        
    }
    public class TwoDieProbabilityData : List<ScatterDataPoint>
    {
        public TwoDieProbabilityData()
        {
            DoubleCollection space = new DoubleCollection();
            for (int d1 = 1; d1 <= 6; d1++)
            {
                for (int d2 = 1; d2 <= 6; d2++)
                {
                    space.Add(d1 + d2);
                }
            }
            ProbabilityCalculator calc = new ProbabilityCalculator { Space = space };
            for (int x = 0; x <= 14; x++)
            {
                double probability = calc.Compute(new ProbabilityEvent(x));
                this.Add(new ScatterDataPoint { X = x, Y = probability });
            }
        }
    }
}