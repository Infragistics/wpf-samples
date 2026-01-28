
namespace Infragistics.Extension
{
	/// <summary>
	/// Class for Chebyshev distancing.
	/// The Chebyshev distance is the greatest distance along any coordinate dimension. In 2-D space it is also known as 
	/// chess distance or chessboard distance and corresponds to the minimum number of moves the king need to reach a destination. 
	/// </summary>
	public class ChebyshevDistance : DistanceFunction
	{
		public override double Distance(double[] p1, double[] p2)
		{
			double d = 0;

			for (int i = 0; i < p1.Length; i++) {
				double diff = p1[i] - p2[i];
				diff = (diff < 0 ? -diff : diff);
				if (diff > d) {
					d = diff;
				}
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
				if (diff > d) {
					d = diff;
				}
			}
			return d;
		}

		#region Singleton Pattern

		public static readonly ChebyshevDistance Instance = new ChebyshevDistance();

		private ChebyshevDistance() { }

		#endregion
	}
}
