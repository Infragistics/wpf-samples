
namespace Infragistics.Extension
{
	public abstract class DistanceFunction
	{
		public abstract double Distance(double[] p1, double[] p2);

		public abstract double DistanceToRect(double[] point, double[] min, double[] max);
	}
}
