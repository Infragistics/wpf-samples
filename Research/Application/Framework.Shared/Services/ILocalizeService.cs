using System; 
using System.Globalization;
 
namespace Infragistics.Framework
{
    public interface ILocalizeService
    {
        CultureInfo GetCurrentCultureInfo();
        void SetLocale(CultureInfo ci);
    }
    /// <summary>
    /// Helper class for splitting locales like  iOS: ms_MY  Android: in-ID
    /// into parts so we can create a .NET culture (or fallback culture)
    /// </summary>
    public class PlatformCulture
    {
        public PlatformCulture(string cultureString)
        {
            if (string.IsNullOrEmpty(cultureString))
                throw new ArgumentException("Expected culture identifier", "cultureString");  

            PlatformString = cultureString.Replace("_", "-"); // .NET expects dash, not underscore
            var dashIndex = PlatformString.IndexOf("-", StringComparison.Ordinal);
            if (dashIndex > 0)
            {
                var parts = PlatformString.Split('-');
                LanguageCode = parts[0];
                LocaleCode = parts[1];
            }
            else
            {
                LanguageCode = PlatformString;
                LocaleCode = "";
            }
        }
        public string PlatformString { get; private set; }
        public string LanguageCode { get; private set; }
        public string LocaleCode { get; private set; }

        public override string ToString()
        {
            return PlatformString;
        }
    }
}
