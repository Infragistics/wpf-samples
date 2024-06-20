using System;
using Infragistics.Controls.Charts;
using System.Windows;

namespace IGDataChart.Samples.ChartData.Axis
{
    /// <summary>
    /// Represents the probability scaler for vertical numeric axis
    /// </summary>
    public class ProbabilityVerticalScaler : NumericScaler
    {
        /// <summary>
        /// Absolute minimum for probability value on a numeric axis
        /// </summary>
        public const double AbsoluteProbabiltyMinimumValue = 0.0;
        /// <summary>
        /// Absolute maximum for probability value on a numeric axis
        /// </summary>
        public const double AbsoluteProbabiltyMaximumValue = 100.0;
        protected double Mu = 0.5;
        protected double Sigma = 0.075; //0.065;

        /// <summary>
        /// Calculates the actual minimum and actual maximum values for the axis scale
        /// </summary>
        public override void CalculateRange(NumericAxisBase target, double minimumValue, double maximumValue,
                                            out double actualMinimumValue,
                                            out double actualMaximumValue)
        {
            // set actual axis range to absolute probability values
            actualMinimumValue = AbsoluteProbabiltyMinimumValue;
            actualMaximumValue = AbsoluteProbabiltyMaximumValue;
        }
        public override double GetUnscaledValue(double scaledValue, ScalerParams param)
        {
            double unscaledValue = param.WindowRect.Top + param.WindowRect.Height * (scaledValue - param.ViewportRect.Top) / param.ViewportRect.Height;

            if (!param.IsInverted)
            {
                unscaledValue = 1.0 - unscaledValue;
            }

            unscaledValue = 0.5 + 0.318309886183790671 * System.Math.Atan2(unscaledValue - this.Mu, this.Sigma);
            return ActualMinimumValue + unscaledValue * (ActualMaximumValue - ActualMinimumValue);
        }
        /// <summary>
        /// Returns the display (pixel) position corresponding with a numeric value on the axis
        /// </summary>
        public override double GetScaledValue(double unscaledValue, ScalerParams param)
        {
            double scaledValue = (unscaledValue - ActualMinimumValue) / (ActualMaximumValue - ActualMinimumValue);

            if (!param.IsInverted)
            {
                scaledValue = 1.0 - scaledValue;
            }
            scaledValue = this.Mu + this.Sigma * System.Math.Tan(System.Math.PI * (scaledValue - 0.5));
            return param.ViewportRect.Top + param.ViewportRect.Height * (scaledValue - param.WindowRect.Top) / param.WindowRect.Height;
        }
    }
    /// <summary>
    /// Represents the probability scaler for horizontal numeric axis
    /// </summary>
    public class ProbabilityHorizontalScaler : NumericScaler
    {
        /// <summary>
        /// Absolute minimum for probability value on a numeric axis
        /// </summary>
        public const double AbsoluteProbabiltyMinimumValue = 0.0;

        /// <summary>
        /// Absolute maximum for probability value on a numeric axis
        /// </summary>
        public const double AbsoluteProbabiltyMaximumValue = 100.0;

        protected double Mu = 0.5;
        protected double Sigma = 0.065;

        /// <summary>
        /// Calculates the actual minimum and actual maximum values for the axis scale
        /// </summary>
        public override void CalculateRange(NumericAxisBase target, double minimumValue, double maximumValue,
                                            out double actualMinimumValue,
                                            out double actualMaximumValue)
        {
            // set actual axis range to absolute probability values
            actualMinimumValue = AbsoluteProbabiltyMinimumValue;
            actualMaximumValue = AbsoluteProbabiltyMaximumValue;
        }
        /// <summary>
        /// Returns the numeric value corresponding with the display or “pixel” position on the axis
        /// </summary>
        public override double GetUnscaledValue(double scaledValue, ScalerParams param)
        {
            double unscaledValue = param.WindowRect.Left + param.WindowRect.Width * (scaledValue - param.ViewportRect.Left) / param.ViewportRect.Width;

            if (!param.IsInverted)
            {
                unscaledValue = 1.0 - unscaledValue;
            }
            unscaledValue = 0.5 + 0.318309886183790671 * System.Math.Atan2(unscaledValue - this.Mu, this.Sigma);

            return ActualMinimumValue + unscaledValue * (ActualMaximumValue - ActualMinimumValue);
        }
        /// <summary>
        /// Returns the display (pixel) position corresponding with a numeric value on the axis
        /// </summary>
        public override double GetScaledValue(double unscaledValue, ScalerParams param)
        {
            double scaledValue = (unscaledValue - ActualMinimumValue) / (ActualMaximumValue - ActualMinimumValue);

            if (!param.IsInverted)
            {
                scaledValue = 1.0 - scaledValue;
            }
            scaledValue = this.Mu + this.Sigma * System.Math.Tan(System.Math.PI * (scaledValue - 0.5));
            return param.ViewportRect.Left + param.ViewportRect.Width * (scaledValue - param.WindowRect.Left) / param.WindowRect.Width;
        }
    }
}
