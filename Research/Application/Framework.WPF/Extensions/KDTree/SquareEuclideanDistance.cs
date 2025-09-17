
namespace Infragistics.Extension
{
	/// <summary>
	/// Class for squared Euclidean distancing.
	/// The Euclidean distance is the ordinary distance between two points given by the Pythagorean formula.
	/// </summary>
	public class SquareEuclideanDistance : DistanceFunction
	{
		public override double Distance(double[] p1, double[] p2)
		{
			double d = 0;

			unchecked {
				for (int i = 0; i < p1.Length; i++) {
					double diff = p1[i] - p2[i];
					d += diff * diff;
				}
			}
			return d;
		}

		public override double DistanceToRect(double[] point, double[] min, double[] max)
		{
			double d = 0;

			unchecked {
				for (int i = 0; i < point.Length; i++) {
					double diff = 0;
					double pt = point[i];
					double maxi = max[i];
					if (pt > maxi) {
						diff = pt - maxi;
					} else {
						double mini = min[i];
						if (pt < mini) {
							diff = pt - mini;
						}
					}
					d += diff * diff;
				}
			}
			return d;
		}

		#region Singleton Pattern

		public static readonly SquareEuclideanDistance Instance = new SquareEuclideanDistance();

		private SquareEuclideanDistance() { }

		#endregion
	}
}
