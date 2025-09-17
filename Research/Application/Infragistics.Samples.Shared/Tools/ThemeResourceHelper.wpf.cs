using Infragistics.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace Infragistics.Samples.Shared.Tools
{
    //NOTE: use ThemeLoader.SetTheme to add/remove themes

    ///// <summary>
    ///// Provides useful methods when working with Theme resource dictionary
    ///// </summary>
    public class ThemeResourceHelper
    {
        //    /// <summary>
        //    /// Removes ResourceDictionary from the list of ResourceDictionary objects the gived ResourceDictionary's MergedDictionaries,
        //    /// which has the specified string in their names.
        //    /// </summary>
        //    public static void RemoveResourceDictionariesForControl(ResourceDictionary dicts, string controlName)
        //    {
        //        for (int i = dicts.MergedDictionaries.Count - 1; i >= 0; i--)
        //        {
        //            var rd = dicts.MergedDictionaries[i];
        //            if (rd.Source!=null && rd.Source.OriginalString.Contains(controlName))
        //            {
        //                dicts.MergedDictionaries.RemoveAt(i);
        //            }
        //        }
        //    }

        //    public static void SetOffice2013Theme(FrameworkElement container, string controlName)
        //    {
        //        var themePath = String.Format("/Infragistics.Themes.Office2013Theme;component/Office2013.{0}.xaml", controlName);
        //        var themeUri = new Uri(themePath, UriKind.Relative);

        //        RemoveResourceDictionariesForControl(container.Resources, controlName);

        //        container.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = themeUri });
        //    }

        //    public static void SetApplicationTheme(string controlName, string theme)
        //    {
        //        var themePath = String.Format("/Infragistics.Themes.{0}Theme;component/{0}.{1}.xaml", theme, controlName);
        //        var themeUri = new Uri(themePath, UriKind.Relative);

        //        RemoveResourceDictionariesForControl(Application.Current.Resources, controlName);

        //        Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = themeUri });
        //    }

        public static ResourceDictionary ThemeOptimizer(ResourceDictionary dictionary, ThemeBase themeBase)
        {
            PropertyInfo info = typeof(ThemeBase).GetProperty("Mappings", BindingFlags.NonPublic | BindingFlags.Instance);

            Dictionary<string, string> mappings = info.GetValue(themeBase, null) as Dictionary<string, string>;
            var mapping = mappings.Where(k => k.Value.Contains("xamSchedule")).FirstOrDefault();

            var packURI = mapping.Value;

            dictionary = new ResourceDictionary() { Source = new Uri(packURI) };

            Application.Current.Resources.MergedDictionaries.Add(dictionary);

            return dictionary;
        }
    }
}