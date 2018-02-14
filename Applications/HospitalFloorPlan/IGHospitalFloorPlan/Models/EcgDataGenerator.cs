using System;
using System.Collections.Generic;

namespace IGShowcase.HospitalFloorPlan.Models
{
	public static class EcgDataGenerator
	{
		#region Private Variables
		private static readonly EcgData[] Pattern;
		#endregion Private Variables

		#region Constructors
		/// <summary>
		/// Initializes the <see cref="EcgDataGenerator"/> class.
		/// </summary>
		static EcgDataGenerator()
		{
			Pattern = new[]
				{
					new EcgData(0.0, 0.0),
					new EcgData(0.32, 0.0),
					new EcgData(0.38, 0.2),
					new EcgData(0.44, -0.2),
					new EcgData(0.52, 0.9),
					new EcgData(0.6, -0.9),
					new EcgData(0.66, 0.0),
					new EcgData(0.88, 0.0),
					new EcgData(0.94, 0.2)
				};
		}
		#endregion Constructors

		#region Public Methods
		/// <summary>
		/// Generates approximately 10s of ECG data.
		/// </summary>
		/// <param name="heartRate">The patient's heart rate.</param>
		/// <returns></returns>
		public static EcgData[] Generate(double heartRate)
		{
			EcgData[] result;
			if (heartRate > 0)
			{
				// period = (1/heartRate) * (60/10) <-- heartRate is in beats per minute, 10 seconds are 1/6 of a minute
				double period = 6 / heartRate; 
				double x = 0;

				//Allocates enough space for about 10 seconds of ECG data
				var data = new List<EcgData>(((int) (Math.Ceiling(1/period)))*Pattern.Length);

				int i = 0;
				while (x < 1.0)
				{
					int iter = i/Pattern.Length;
					int index = i%Pattern.Length;
					x = period*(iter + Pattern[index].X);
					data.Add(new EcgData(x, Pattern[index].Y));
					++i;
				}
				result = data.ToArray();
			}
			else
			{
				result = new [] {new EcgData(0.0, 0.0), new EcgData(1.0, 0.0)};
			}
			return result;
		}
		#endregion Public Methods
	}
}
