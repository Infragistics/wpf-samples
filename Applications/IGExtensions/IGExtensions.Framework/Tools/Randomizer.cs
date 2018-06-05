using System.Threading;

// ReSharper disable CheckNamespace
namespace System
// ReSharper restore CheckNamespace
{
    /// <summary>
    /// Represents an utility for creating random generators
    /// </summary>
    public class Randomizer
    {
        protected static int SeedCounter = new Random().Next();

        [ThreadStatic]
        protected static Random Random;

        public static Random Instance
        {
            get
            {
                if (Random == null)
                {
                    var seed = Interlocked.Increment(ref SeedCounter);
                    Random = new Random(seed);
                }
                return Random;
            }
        }
        public static Random Create()
        {
            Thread.Sleep(1); // creates one of 1000 random seeds 
            var seed = DateTime.Now.Millisecond;
            var random = new Random(seed);
            return random;
        }
    }
}