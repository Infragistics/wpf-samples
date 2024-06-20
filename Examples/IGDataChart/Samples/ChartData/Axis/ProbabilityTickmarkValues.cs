using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using Infragistics.Controls.Charts;

namespace IGDataChart.Samples.ChartData.Axis
{
    public class ProbabilityTickmarkValues : TickmarkValues
    {
        public ProbabilityTickmarkValues()
        {
            // define probability tickmark values
            this.ProbabilityValues = new DoubleCollection { 0, 5, 10, 20, 30, 40, 50, 60, 70, 80, 90, 95, 100 };
        }
        protected double First;
        protected double Last;
        protected DoubleCollection ProbabilityValues;
        /// <summary>
        /// Initializes tickmark values prior to rendering axis labels, striplines, and gridlines
        /// </summary>
        /// <param name="initializationParameters"></param>
        public override void Initialize(TickmarkValuesInitializationParameters initializationParameters)
        {
            base.Initialize(initializationParameters);
            // Initialize is overridden in order to store the minimum and maximum values, 
            // which will be later used in the MajorValues() and MinorValues() methods.
            this.First = initializationParameters.VisibleMinimum;
            this.Last = initializationParameters.VisibleMaximum;
        }
        /// <summary>
        /// Returns values of major tickmarks
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<double> MajorValues()
        {
            IEnumerable<double> values = ProbabilityValues.Where((value) => value >= this.First && value <= this.Last);
            return values;
        }
        /// <summary>
        /// Returns values of minor tickmarks
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<double> MinorValues()
        {
            return new double[] { /* no minor tickmarks */ };
        }
    }
}