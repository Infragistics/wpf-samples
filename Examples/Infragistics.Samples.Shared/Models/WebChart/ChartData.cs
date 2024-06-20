
namespace Infragistics.Samples.Shared.Models.WebChart
{
    public class ChartData
    {
        public ChartData(double v1, double v2, double v3, double v4, double v5)
        {
            Col1 = v1;
            Col2 = v2;
            Col3 = v3;
            Col4 = v4;
            Col5 = v5;
        }

        public double Col1 { get; set; }
        public double Col2 { get; set; }
        public double Col3 { get; set; }
        public double Col4 { get; set; }
        public double Col5 { get; set; }
    }
}
