 
namespace System
{
    public static class Calc
    {
        public static int Gcd(int a, int b)
        {
            return b == 0 ? a : Gcd(b, a % b);
        }
        public static double Gcd(double a, double b)
        {
            return b == 0 ? a : Gcd(b, a % b);
        }
        public static double Ratio(int a, int b)
        {
            if (b == 0) return 0;
            return (a * 1.0 / b);
        }
        public static double Ratio(double a, double b)
        {
            if (double.IsNaN(b) ||
                double.IsInfinity(b) || b == 0)
            {
                return 0;
            }
            return (a / b);
        }

        public static double Range(double a, double b)
        {
            if (double.IsNaN(a) || double.IsInfinity(a)) return 0;
            if (double.IsNaN(b) || double.IsInfinity(b)) return 0;
            return (a - b);
        }

        internal static Random Random = new Random();
        /// <summary>
        /// Get random double between two double values multiplied by optional factor
        /// </summary> 
        public static double Rand(double minimum, double maximum, double factor = 1.0)
        {
            var rand = Random.NextDouble() * (maximum - minimum) + minimum;
            return rand * factor;
        }
        /// <summary>
        /// Get random double between two double values multiplied by a thousand
        /// </summary> 
        public static double RandK(double min, double max)
        {
            return Rand(min, max, 1000);
        }
        /// <summary>
        /// Get random double between two double values multiplied by a millions
        /// </summary> 
        public static double RandM(double min, double max)
        {
            return Rand(min, max, 1000000);
        }
    }
    public static class DoubleEx
    {
        public const double Kilo = 1000;
        public const double Mega = 1000000;
        public const double Giga = 1000000000;
        public const double Tera = 1000000000000;
        public const double Peta = 1000000000000000;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="precision">.##</param>
        /// <returns></returns>
        public static string ToPecentage(this double value, string precision = "")
        {
            return value.ToStringShort(precision + "%", true);
        }
        
        //public static string ToBalance(this double value, string format = "{0:0.#}")
        //{
        //    var result = value >= 0 ? "+" : "";
        //    result += string.Format(format, value);
        //    return result;
        //}

        /// <summary>
        /// Converts number to short string with K, M, B, T multipliers
        /// <param name="value"></param>
        /// <param name="precision">.##</param>
        /// <remarks>https://msdn.microsoft.com/en-us/library/0c899ak8.aspx</remarks>
        /// </summary> 
        public static string ToStringShort(this double value, string precision = ".##", bool useSign = false)
        {
            var format = "";
            if (value >= Peta || value <= -Peta)
                format += "0,,,,," + precision + "P";
            
            else if (value >= Tera || value <= -Tera)
                format += "0,,,," + precision + "T";

            else if (value >= Giga || value <= -Giga)
                format += "0,,," + precision + "B";

            else if (value >= Mega || value <= -Mega)
                format += "0,," + precision + "M";

            else if (value >= Kilo || value <= -Kilo)
                format += "0," + precision + "K";

            else
                format += "0" + precision;

            //var result = string.Format(format, value);
            var result = value.ToString(format);
            if (useSign && value > 0)
                result = "+" + result;

            return result; 
        }
        /// <summary>
        /// Parser string number with K, M, B, T notations
        /// </summary> 
        public static double Parse(string valueString)
        {
            var value = double.NaN;
            valueString = valueString.ToUpper();
            var number = valueString
                .Replace(",", "").Replace(" ", "").Replace("+", "")
                .Replace("K", "").Replace("M", "").Replace("B", "").Replace("T", "");
            if (double.TryParse(number, out value))
            {
                     if (valueString.Contains("K")) value *= 1000;
                else if (valueString.Contains("M")) value *= 1000000;
                else if (valueString.Contains("B")) value *= 1000000000;
                else if (valueString.Contains("T")) value *= 1000000000000;
            }
           
            return value;
        }
    }
     
}


