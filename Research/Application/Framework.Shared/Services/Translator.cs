using Infragistics.Framework;
using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

//TODO change namespace to Infragistics.Framework
namespace Samples.Browser
{
    // https://developer.xamarin.com/guides/xamarin-forms/advanced/localization/

    // Exclude the 'Extension' suffix when using in XAML markup
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        public string Text { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return Translator.Localize(Text);
        }
    }

    public static class Translator
    {
        public static string ResxFile;
        public static Assembly Assembly;
        public static ResourceManager Manager; 

        public static string Localize(string resource, CultureInfo culture = null)
        {  
            if (string.IsNullOrEmpty(resource))
                return "";

            if (culture == null)
            {
                culture = AppResources.Culture; 
            };
            if (Assembly == null)
            {
                Assembly = typeof(AppResources).GetTypeInfo().Assembly;

                ResxFile = Assembly.GetName().Name + ".AppResources";
            };
            if (Manager == null)
            {
                Manager = new ResourceManager(ResxFile, Assembly);
            };

            var translation = Manager.GetString(resource, culture);
            if (translation == null)
            {
#if DEBUG 
                var cultureId = culture.TwoLetterISOLanguageName; 

                var msg = string.Format("Cannot find '{0}' resource in {1}.resx' file for '{2}' culture", 
                    resource, ResxFile, cultureId);

                Logs.Error(msg);
                //throw new ArgumentException(msg);
                translation = "{" + resource + "}"; // returns the key, which gets displayed to the user
#else
                translation = "{" + resource + "}"; // returns the key, which gets displayed to the user
#endif
            }
            else
            {
                translation = translation.Replace("\\n", " ");
            }
            return translation;
        }

    }
    
}
