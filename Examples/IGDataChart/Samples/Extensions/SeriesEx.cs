 
namespace Infragistics.Controls.Charts
{
    public static class SeriesEx
    {
        #region Validation
        /// <summary>
        /// Checks if the series has stacked series
        /// </summary>
        public static bool IsHorizontalSeries(this Series series)
        {
            if (series != null)
            {
                if (series is HorizontalAnchoredCategorySeries ||
                    series is HorizontalStackedSeriesBase ||
                    series is FinancialSeries)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if the series has stacked series
        /// </summary>
        public static bool IsVerticalSeries(this Series series)
        {
            if (series != null)
            {
                if (series is VerticalAnchoredCategorySeries ||
                    series is VerticalStackedSeriesBase)
                    return true;
            }
            return false;
        }
        /// <summary>
        /// Checks if the series has financial series
        /// </summary>
        public static bool IsFinancialSeries(this Series series)
        {
            if (series != null)
            {
                if (series is FinancialSeries)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if the series has stacked series
        /// </summary>
        public static bool IsStackedSeries(this Series series)
        {
            if (series != null)
            {
                if (series is StackedSeriesBase)
                    return true;
            }
            return false;
        }
        /// <summary>
        /// Checks if the series has category series
        /// </summary>
        public static bool IsCategorySeries(this Series series)
        {
            if (series != null)
            {
                if (series is CategorySeries)
                    return true;
            }
            return false;
        }
        /// <summary>
        /// Checks if the series has category series
        /// </summary>
        public static bool IsAnnotationLayer(this Series series)
        {
            if (series != null)
            {
                if (series.IsAnnotationLayer)
                    return true;
            }
            return false;
        }
        /// <summary>
        /// Checks if the series has range series
        /// </summary>
        public static bool IsRangeSeries(this Series series)
        {
            if (series != null)
            {
                if (series.IsRange)
                    return true;
            }
            return false;
        }
        /// <summary>
        /// Checks if the series has scatter series
        /// </summary>
        public static bool IsScatterSeries(this Series series)
        {
            if (series != null)
            {
                if (series is ScatterBase)
                    return true;
            }
            return false;
        }
        /// <summary>
        /// Checks if the series has radial series
        /// </summary>
        public static bool IsRadialSeries(this Series series)
        {
            if (series != null)
            {
                if (series is RadialBase)
                    return true;
            }
            return false;
        }
        /// <summary>
        /// Checks if the series has polar series
        /// </summary>
        public static bool IsPolarSeries(this Series series)
        {
            if (series != null)
            {
                if (series is PolarBase)
                    return true;
            }
            return false;
        }
        #endregion
    }
}