 
namespace Infragistics.Controls.Charts
{
    public static class XamDataChartEx
    {
        #region Validation
        /// <summary>
        /// Checks if the chart has stacked series
        /// </summary>
        public static bool IsHorizontalChart(this XamDataChart chart)
        {
            if (chart != null && chart.Series != null)
            {
                foreach (var series in chart.Series)
                {
                    if (series.IsHorizontalSeries())
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks if the chart has stacked series
        /// </summary>
        public static bool IsVerticalChart(this XamDataChart chart)
        {
            if (chart != null && chart.Series != null)
            {
                foreach (var series in chart.Series)
                {
                    if (series.IsVerticalSeries())
                        return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Checks if the chart has financial series
        /// </summary>
        public static bool IsFinancialChart(this XamDataChart chart)
        {
            if (chart != null && chart.Series != null)
            {
                foreach (var series in chart.Series)
                {
                    if (series.IsFinancialSeries())
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks if the chart has stacked series
        /// </summary>
        public static bool IsStackedChart(this XamDataChart chart)
        {
            if (chart != null && chart.Series != null)
            {
                foreach (var series in chart.Series)
                {
                    if (series.IsStackedSeries())
                        return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Checks if the chart has category series
        /// </summary>
        public static bool IsCategoryChart(this XamDataChart chart)
        {
            if (chart != null && chart.Series != null)
            {
                foreach (var series in chart.Series)
                {
                    if (series.IsCategorySeries())
                        return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Checks if the chart has annotation series
        /// </summary>
        public static bool IsAnnotationChart(this XamDataChart chart)
        {
            if (chart != null && chart.Series != null)
            {
                foreach (var series in chart.Series)
                {
                    if (series.IsAnnotationLayer())
                        return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Checks if the chart has scatter series
        /// </summary>
        public static bool IsScatterChart(this XamDataChart chart)
        {
            if (chart != null && chart.Series != null)
            {
                foreach (var series in chart.Series)
                {
                    if (series.IsScatterSeries())
                        return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Checks if the chart has radial series
        /// </summary>
        public static bool IsRadialChart(this XamDataChart chart)
        {
            if (chart != null && chart.Series != null)
            {
                foreach (var series in chart.Series)
                {
                    if (series.IsRadialSeries())
                        return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Checks if the chart has polar series
        /// </summary>
        public static bool IsPolarChart(this XamDataChart chart)
        {
            if (chart != null && chart.Series != null)
            {
                foreach (var series in chart.Series)
                {
                    if (series.IsPolarSeries())
                        return true;
                }
            }
            return false;
        } 
        #endregion
    }
}