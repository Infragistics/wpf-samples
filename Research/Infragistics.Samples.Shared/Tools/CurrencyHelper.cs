using System.Globalization;

namespace Infragistics.Samples.Shared.Tools
{
    public class CurrencyHelper
    {
        private static readonly CultureInfo PriceCulture = new CultureInfo("en-US");

        public static string FormatToUsd(double value)
        {
            return value.ToString("c", CurrencyHelper.PriceCulture);
        }
    }
}