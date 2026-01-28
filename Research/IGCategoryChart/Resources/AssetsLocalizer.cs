using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infragistics.Samples.Shared.Resources;

namespace IGCategoryChart.Resources
{
    /// <summary>
    /// Represents an assets localizer that provides access to the <see cref="DataChartStrings"/> as well as to the <see cref="CommonStrings"/> 
    /// </summary>
    public class AssetsLocalizer : CommonLocalizer
    {
        public static readonly CategoryChartStrings StringAssets = new CategoryChartStrings();

        private CategoryChartStrings ChartStrings
        {
            get { return StringAssets; }
        }

    }
}
