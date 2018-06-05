using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Data;


namespace Infragistics.Controls.Charts
{
    //TODO-DEV copy this Converter to DV project and use it in code behind so it is used across all platforms

    public class LargeNumbersConverter : IValueConverter
    {
        public LargeNumbersConverter()
        {
            Notation = Notation.Financial;
            Precission = 1;
            LargeUnits = null;
            SmallUnits = null;
            ThresholdMin = double.NaN;
            ThresholdMax = double.NaN;
        }

        /// <summary> Gets or sets Units, defaults to { "K", "M", "B", "T", "P", "E" } </summary>
        public string[] LargeUnits { get; set; }
        public string[] SmallUnits { get; set; }
        /// <summary> Gets or sets Threshold for rounding numbers, defaults 1000 </summary>
        public double ThresholdMin { get; set; }
        public double ThresholdMax { get; set; }
        public int Precission { get; set; }
        public Notation Notation { get; set; }

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var test= ConvertUtil.ToShortString(value,
                this.Notation,
                this.LargeUnits, this.SmallUnits,
                this.Precission,
                this.ThresholdMin, this.ThresholdMax);
            return ConvertUtil.ToShortString(value,
                this.Notation,
                this.LargeUnits, this.SmallUnits,
                this.Precission,
                this.ThresholdMin, this.ThresholdMax);
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

namespace Infragistics
{
    public enum Notation
    {
        /// <summary> Specifiies numbers in no form, 5200 for 5200 </summary>
        None,
        /// <summary> Specifiies numbers in form of "N:0.0 Symbol", where N is multiplier of "k", "M", "G", "T", or "P" symbol </summary>
        Metric,
        /// <summary> Specifiies numbers in form of "N:0.0 Symbol", where N is multiplier of "K", "M", "B", "T", or "Q" symbol </summary>
        Financial,
        /// <summary> Specifiies numbers in form of "10^P" (ten raised to P power) </summary>
        Power10,
        /// <summary> Specifiies numbers in form of "N:0.0 x10^P" (N times ten raised to P power), e.g. 5.2 x10^3 is 5200 </summary>
        Power10X,
        /// <summary> Specifiies numbers in form of "N:0.0 E+P" (N times ten raised to P power), e.g. 5.2 E+3 is 5200 </summary>
        ScientificE,
        /// <summary> Specifiies numbers in form of "N:0.0 E+0P" (N times ten raised to P power), e.g. 5.2 E+03 is 5200 </summary>
        ScientificE0,
        /// <summary> Specifiies numbers in form of "N:0.0 E+00P" (N times ten raised to P power), e.g. 5.2 E+003 is 5200 </summary>
        ScientificE00,
        /// <summary> Specifiies numbers in form of Hexadecimal, 0000014D is 333 </summary>
        Hexadecimal,

        //TODO Custom, 
    }
    public static class ConvertUtil
    {
        static ConvertUtil()
        {
            LargeUnits = new Dictionary<Notation, string[]>();
            SmallUnits = new Dictionary<Notation, string[]>();

            LargeUnits[Notation.None] = new[] { "" };
            SmallUnits[Notation.None] = new[] { "" };

            LargeUnits[Notation.Metric] = new[] { "", "k", "M", "G", "T", "P", "E" };
            SmallUnits[Notation.Metric] = new[] { "", "m", "μ", "n", "p", "f", "a" };

            LargeUnits[Notation.Financial] = new[] { "", "K", "M", "B", "T", "Q" };
            SmallUnits[Notation.Financial] = new[] { "", "1/K", "1/M", "1/B", "1/T", "1/Q" };

            LargeUnits[Notation.Power10] = new[] { "10^" };
            SmallUnits[Notation.Power10] = new[] { "10^" };

            LargeUnits[Notation.Power10X] = new[] { "x 10^" };
            SmallUnits[Notation.Power10X] = new[] { "x 10^" };

            LargeUnits[Notation.ScientificE] = new[] { "E+" };
            SmallUnits[Notation.ScientificE] = new[] { "E-" };

            LargeUnits[Notation.ScientificE0] = new[] { "E+0" };
            SmallUnits[Notation.ScientificE0] = new[] { "E-0" };

            LargeUnits[Notation.ScientificE00] = new[] { "E+00" };
            SmallUnits[Notation.ScientificE00] = new[] { "E-00" };
        }


        internal static Dictionary<Notation, string[]> LargeUnits;
        internal static Dictionary<Notation, string[]> SmallUnits;

        public static object ToShortString(object value,
            Notation notation = Notation.Financial,
            string[] largeUnits = null,
            string[] smallUnits = null,
            int precission = 1,
            double thresholdMin = double.NaN,
            double thresholdMax = double.NaN)
        {
            if (value == null) return "NUL";

            if (notation == Notation.None)
            {
                return value;
            }


            if (value is double ||
                value is long ||
                value is int)
            {
                var number = double.Parse(value.ToString());
                // skip convertion if the number is within threshold range
                if (!double.IsNaN(thresholdMax) && thresholdMax > number &&
                    !double.IsNaN(thresholdMin) && thresholdMin < number)
                    return value;

                if (notation == Notation.Hexadecimal)
                {
                    var result = ((int)number).ToString("X8");
                    Debug.WriteLine(result + " <- " + number + " " + notation + "");
                    return result;
                }

                var sign = number < 0 ? "-" : "";
                var absolute = System.Math.Abs(number);
                var isZero = absolute < 1.0e-20;
                var isLarge = isZero || number >= 1 || number <= -1;

                if (precission == 0)
                    precission = 1;

                if (notation != Notation.None)
                {
                    largeUnits = LargeUnits[notation];
                    smallUnits = SmallUnits[notation];
                }

                if (largeUnits == null || largeUnits.Length == 0)
                    largeUnits = LargeUnits[Notation.Metric];

                if (smallUnits == null || smallUnits.Length == 0)
                    smallUnits = SmallUnits[Notation.Metric];

                var units = isLarge ? largeUnits : smallUnits;


                if (notation == Notation.ScientificE ||
                    notation == Notation.ScientificE0 ||
                    notation == Notation.ScientificE00)
                {
                    var unit = units[0];
                    if (isZero)
                    {
                        var format = new string('0', precission);
                        var result = "0." + format + unit + "0";

                        Debug.WriteLine(result + " <- " + number + " " + notation + " zero");
                        return result;
                    }
                    else
                    {
                        var result = number.ToString("E" + precission);

                        if (notation == Notation.ScientificE ||
                            notation == Notation.ScientificE0)
                        {
                            while (!result.EndsWith(unit + "0") && result.Contains(unit + "0"))
                            {
                                result = result.Replace(unit + "0", unit);
                            }
                        }

                        Debug.WriteLine(result + " <- " + number + " " + notation);
                        return result;
                    }
                }
                if (notation == Notation.Power10)
                {
                    var unit = units[0];
                    if (isZero)
                    {
                        var result = "0";

                        Debug.WriteLine(result + " <- " + number + " " + notation);
                        return result;
                    }
                    else
                    {
                        var exp = (int)System.Math.Log10(absolute);
                        var result = sign + unit + exp;
                        Debug.WriteLine(result + " <- " + number + " " + notation);
                        return result;
                    }
                }
                if (notation == Notation.Power10X)
                {
                    var unit = units[0];
                    if (isZero)
                    {
                        var result = "0";

                        Debug.WriteLine(result + " <- " + number);
                        return result;
                    }
                    else
                    {
                        var exp = (int)System.Math.Log10(absolute);
                        var coefficient = System.Math.Round(10 * absolute / System.Math.Pow(10.0, exp)) / 10;

                        var format = new string('#', precission - 1);
                        var result = coefficient.ToString(".0" + format);

                        result = sign + result + " " + unit + exp;

                        Debug.WriteLine(result + " <- " + number + " " + notation);
                        return result;
                    }
                }
                if (notation == Notation.Metric ||
                    notation == Notation.Financial)
                {
                    if (isZero)
                    {
                        var result = "0";
                        Debug.WriteLine(result + " <- " + number + " " + notation);
                        return result;
                    }
                    if (isLarge)
                    {
                        var exp = (int)(System.Math.Log10(absolute) / 3.0);
                        var coefficient = System.Math.Round(10 * absolute / System.Math.Pow(1000.0, exp)) / 10;
                        exp = System.Math.Abs(exp);
                        var unit = units.Length <= exp ? units[0] : units[exp];

                        string result;
                        if (string.IsNullOrEmpty(unit))
                        {
                            result = sign + absolute;
                        }
                        else
                        {
                            var format = new string('#', precission);
                            result = coefficient.ToString("0." + format);
                            result = sign + result + " " + unit;
                        }

                        Debug.WriteLine(result + " <- " + number + " " + notation);
                        return result;
                    }
                    else // small number
                    {
                        var result = absolute.ToString("E");
                        var parts = result.Split('E');
                        var coefficient = double.Parse(parts[0]);
                        var exp = int.Parse(result.Replace(parts[0] + "E", ""));

                        var ind = System.Math.Abs(exp);
                        var mod = ind % 3;
                        ind = ind / 3;
                        coefficient *= System.Math.Pow(10, mod);

                        var unit = units.Length <= ind ? units[0] : units[ind];

                        if (string.IsNullOrEmpty(unit))
                        {
                            result = sign + number;
                        }
                        else
                        {
                            var format = new string('#', precission - 1);
                            result = coefficient.ToString(".0" + format);
                            result = sign + result + unit;
                        }

                        Debug.WriteLine(result + " <- " + number + " " + notation);
                        return result;

                    }
                }
            }
            return value;
        }

    }
}
