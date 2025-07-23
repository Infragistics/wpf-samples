using IGSurfaceChart.Samples.Models;
using Infragistics;
using System;
using System.Collections.Specialized;

namespace IGSurfaceChart.Samples.ViewModels
{
    public class LiveDataCollection : DataShape3D
    {
        public LiveDataCollection(int count, double seed)
        {
            for (int i = 0; i < count; ++i)
            {
                Add(Create(seed));
            }
        }

        public void Update(double seed)
        {
            seed += 0.01;
            foreach (var item in this)
            {
                Update(item, seed);
            }

            OnCollectionChanged(NotifyCollectionChangedAction.Reset);
        }
        private static Random random = new Random();


        public DataPoint3D Create(double seed)
        {
            var v = new DataPoint3D();
            v.X = 100.0 * random.NextDouble();
            v.Y = 100.0 * random.NextDouble();
            v.Z = MathUtilExtended.Noise(0.02 * v.X, 0.02 * v.Y, seed);

            return v;
        }

        public void Update(DataPoint3D v, double seed)
        {
            var z = MathUtilExtended.Noise(0.02 * v.X, 0.02 * v.Y, seed);
            v.Z = z;
        }
    }
}
