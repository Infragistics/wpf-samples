using System;
using System.Text;

namespace Infragistics.Samples.Shared.Tools
{
    public static class RandomizeUtility
    {

        private static readonly Random Generator = new Random(Environment.TickCount);

        public static int GenerateRandomNumber(int min, int max)
        {
            return RandomizeUtility.Generator.Next(min, max);
        }

        public static double GenerateRandomDouble(int min, int max)
        {
            int generatedValue = RandomizeUtility.Generator.Next(min, max);
            double result = generatedValue + RandomizeUtility.Generator.NextDouble();
            return result;
        }

        public static double GenerateOffSet(double value)
        {
            int postiveNegative = RandomizeUtility.Generator.Next(0, 1);
            double offSet = RandomizeUtility.Generator.NextDouble();

            if (postiveNegative == 1 && offSet > 0)
            {
                offSet = offSet * -1;
            }

            return value + offSet;
        }

        public static string GenerateRandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
             
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(
                        Convert.ToInt32(
                            System.Math.Floor(26 * RandomizeUtility.Generator.NextDouble() + 65)));

                builder.Append(ch);
            }

            string result = builder.ToString();
            if (lowerCase)
            {
                result = result.ToLower();
            }
            return result;
        }
    }
}
