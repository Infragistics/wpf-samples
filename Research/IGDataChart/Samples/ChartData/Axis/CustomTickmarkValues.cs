using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using Infragistics.Controls.Charts;

namespace IGDataChart.Samples.ChartData.Axis
{
    public class CustomTickmarkValues : TickmarkValues
    {
        public CustomTickmarkValues()
        {
            // define custom tickmark values
            this.Tickmarks = new DoubleCollection { 0.0, 4.0, 5.0, 6.0, 10.0, 14.0, 15.0, 16.0, 20.0 };
        }
        protected double First;
        protected double Last;
        protected DoubleCollection Tickmarks;
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
            IEnumerable<double> values = Tickmarks.Where((value) => value >= this.First && value <= this.Last);
            return values;
        }
        /// <summary>
        /// Returns values of minor tickmarks
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<double> MinorValues()
        {
            double start = this.First;
            for (double minorValue = start; minorValue < this.Last; minorValue += 1.0)
            {
                yield return minorValue;
            }
        }
    }
}