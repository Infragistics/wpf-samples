
using System.Linq;
using Infragistics.Framework;

namespace Infragistics.Controls.Charts
{
    public static class XamDataChartEx
    {
        public static T GetAxis<T>(this XamDataChart chart)
        {
            return chart.Axes.OfType<T>().FirstOrDefault();
        }
        ////Visualize
        //public static void Visualize(this XamDataChart chart, ShapeDataSources data, SeriesType seriesType)
        //{ 
        //    //var chartModel = new DataChartViewModel
        //    //{
        //    //    Chart = chart,
        //    //    Data = data,
        //    //    SeriesType = seriesType
        //    //};
        //    chart.DataContext = new DataChartViewModel(chart)
        //    {
        //        SeriesType = seriesType
        //    };
      
        //}
    }
}