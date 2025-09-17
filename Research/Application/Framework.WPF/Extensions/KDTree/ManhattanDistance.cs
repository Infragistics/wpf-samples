
namespace Infragistics.Extension
{
	/// <summary>
	/// Class for Manhattan distancing.
	/// The Manhattan distance also called taxicab metric, rectilinear distance or city block distance
	/// is the sum of the distances along each coordinate dimension.
	/// </summary>
	public class ManhattanDistance : DistanceFunction
	{
		public override double Distance(double[] p1, double[] p2)
		{
			double d = 0;

			for (int i = 0; i < p1.Length; i++) {
				double diff = (p1[i] - p2[i]);
				d += diff < 0 ? -diff : diff;
			}
			return d;
		}

		public override double DistanceToRect(double[] point, double[] min, double[] max)
		{
			double d = 0;

			for (int i = 0; i < point.Length; i++) {
				double diff = 0;
				if (point[i] > max[i]) {
					diff = point[i] - max[i];
				} else if (point[i] < min[i]) {
					diff = min[i] - point[i];
				}
				d += diff;
			}
			return d;
		}

		#region Singleton Pattern

		public static readonly ManhattanDistance Instance = new ManhattanDistance();

		private ManhattanDistance() { }

		#endregion
	}
}
