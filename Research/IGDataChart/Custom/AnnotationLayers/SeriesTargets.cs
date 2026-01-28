using System.Collections.Generic;

namespace IGDataChart.Custom.AnnotationLayers
{
  public class SeriesTargets : List<string>
  {
    public SeriesTargets()
    {
        Add(IGDataChart.Resources.DataChartStrings.XDC_All);
        Add(IGDataChart.Resources.DataChartStrings.XDC_Series1);
        Add(IGDataChart.Resources.DataChartStrings.XDC_Series2);
     }
  }
}

