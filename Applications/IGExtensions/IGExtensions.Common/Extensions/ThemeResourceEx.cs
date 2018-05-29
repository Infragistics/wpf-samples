//using System.Windows;

using System.Collections.Generic;
using System.Windows.Media;

namespace System.Windows 
{
    public static class ThemeResourceEx
    {
        /// <summary>
        /// Removes ResourceDictionary from the list of MergedDictionaries objects that have the specified string in their names.
        /// </summary>
        public static void RemoveResourceDictionariesForControl(this ResourceDictionary dicts, string controlName)
        {
            for (int i = dicts.MergedDictionaries.Count - 1; i >= 0; i--)
            {
                var rd = dicts.MergedDictionaries[i];
                if (rd.Source.OriginalString.Contains(controlName))
                {
                    dicts.MergedDictionaries.RemoveAt(i);
                }
            }
        }
         // int APP
            //  InitializeComponent();
            //var themeUri = new Uri("/IGExtensions.EarthQuake;component/Theme.Colors.xaml", UriKind.RelativeOrAbsolute);
            //var themeResource = new ResourceDictionary { Source = themeUri};


            //this.Resources.ReplaceAllResourcesInDictionary("Theme.Colors", themeResource);
        

        public static void ReplaceAllResourcesInDictionary(this ResourceDictionary dicts, string themeName, ResourceDictionary themeResource)
        {
            for (int i = dicts.MergedDictionaries.Count - 1; i >= 0; i--)
            {
                var rd = dicts.MergedDictionaries[i];
                var rdString = rd.Source.OriginalString;
                foreach (var rdInner in rd.MergedDictionaries)
                {
                   // rdInner.ReplaceResourcesInDictionary(themeName, themeResource);
                    ReplaceAllResources(rdInner, themeName, themeResource);
                }
            }
        }
        public static void ReplaceAllResources(ResourceDictionary dicts, string themeName, ResourceDictionary themeResource)
        {
            //for (int i = dicts.MergedDictionaries.Count - 1; i >= 0; i--)
            //{
            //    var rd = dicts.MergedDictionaries[i];
            var rdString = dicts.Source.OriginalString;
            //foreach (var rdInner in rd.MergedDictionaries)
            //{
            //    rdInner.ReplaceResourcesInDictionary(themeName, themeResource);
            //}
            if (rdString.Contains(themeName))
            {
                var keys = themeResource.Keys as List<string>;
                var values = themeResource.Values as List<object>;
               
                
                var pairs = themeResource as IEnumerable<KeyValuePair<object, object>>;
                foreach (KeyValuePair<object, object> pair in pairs)
                {
                   
                    dicts.FindAndReplaceAllResources(pair.Key as string, pair.Value);

                    var resource = pair.Value;
                    if (resource is Color)
                    {
                    }
                    //else if (pair.Value is SolidColorBrush)
                    //{
                    //    solidBrush = (SolidColorBrush)resource;
                    //}
                    //else if (pair.Value is LinearGradientBrush)
                    //{
                    //    linearBrush = (LinearGradientBrush)resource;
                    //}
                    //else if (pair.Value is RadialGradientBrush)
                    //{
                    //    radialBrush = (RadialGradientBrush)resource;
                    //}
                    //}
                }

                //dicts.MergedDictionaries.RemoveAt(i);
                //dicts.MergedDictionaries.Insert(i, themeResource);
            }
            // }
        }
        public static void FindAndReplaceAllResources(this ResourceDictionary resourceDict, string resourceName, object resourceValue)
        {

            var pairs = resourceDict as IEnumerable<KeyValuePair<object, object>>;
            foreach (KeyValuePair<object, object> pair in pairs)
            {

                if (pair.Key.Equals(resourceName))
                {

                    var resource = pair.Value;
                    if (resource is Color && resourceValue is Color)
                    {
                        var colorTarget = (Color)resourceValue;
                        var colorSource = (Color)resourceDict[pair.Key];

                        if (colorSource != colorTarget)
                            colorSource.A = colorTarget.A;
                        colorSource.B = colorTarget.B;
                        colorSource.R = colorTarget.R;
                        colorSource.G = colorTarget.G;

                        return;

                    }
                    //else if (pair.Value is SolidColorBrush)
                    //{
                    //    solidBrush = (SolidColorBrush)resource;
                    //}
                    //else if (pair.Value is LinearGradientBrush)
                    //{
                    //    linearBrush = (LinearGradientBrush)resource;
                    //}
                    //else if (pair.Value is RadialGradientBrush)
                    //{
                    //    radialBrush = (RadialGradientBrush)resource;
                    //}
                }

            }
        }
       
        public static void ReplaceAllResources(ResourceDictionary dicts, ResourceDictionary resourceTarget)
        {
            var pairs = dicts as IEnumerable<KeyValuePair<object, object>>;
            foreach (KeyValuePair<object, object> pair in pairs)
            {
                var match = FindMatchingResource(pair.Key as string, dicts, resourceTarget);
                if (match.IsFound)
                {
                    // TODO replace values of colors and brushes 
                    // TODO replace styles that use matching names of colors and brushes
                }
            }
        }
        public class ResourceMatch
        {
            public ResourceMatch()
            {
                IsFound = false;
            }
            public ResourceMatch(string resourceKey, object resourceOrgValue, object resourceNewValue)
            {
                IsFound = true;
                ResourceKey = resourceKey;
                ResourceOrgValue = resourceOrgValue;
                ResourceNewValue = resourceNewValue;
            }
            public bool IsFound { get; set; }
            public string ResourceKey { get; set; }
            public object ResourceOrgValue { get; set; }
            public object ResourceNewValue { get; set; }
        }
        public static ResourceMatch FindMatchingResource(string resourceName, ResourceDictionary resourceOrg, ResourceDictionary resourceTarget)
        {
            var resourceOrgPairs = resourceOrg as IEnumerable<KeyValuePair<object, object>>;
            foreach (KeyValuePair<object, object> org in resourceOrgPairs)
            {
                var resourceTargetPairs = resourceTarget as IEnumerable<KeyValuePair<object, object>>;
                foreach (KeyValuePair<object, object> target in resourceTargetPairs)
                {
                    if (org.Key.Equals(target.Key) &&
                     ((org.Value is Color && target.Value is Color) ||
                      (org.Value is SolidColorBrush && target.Value is SolidColorBrush)))

                        return new ResourceMatch(org.Key as string, org.Value, target.Value);
                }
            }
            return new ResourceMatch();
        }
      
      
        public static void ReplaceResourceDictionaries(this ResourceDictionary dicts, string themeName, ResourceDictionary themeResource)
        {
            //for (int i = dicts.MergedDictionaries.Count - 1; i >= 0; i--)
            for (int i = 0; i < dicts.MergedDictionaries.Count - 1; i++)
            {
                var rd = dicts.MergedDictionaries[i];
                var rdString = rd.Source.OriginalString;
                var rdCount = rd.MergedDictionaries.Count;
                var rdChildrenBefore = new List<string>();
                var rdChildrenAfter = new List<string>();
                foreach (var rdInner in rd.MergedDictionaries)
                {
                    rdChildrenBefore.Add(rdInner.Source.OriginalString);
                    rdInner.ReplaceResourceDictionaries(themeName, themeResource);

                    rdChildrenAfter.Add(rdInner.Source.OriginalString);
                }
                if (rdString.Contains(themeName))
                {

                    dicts.MergedDictionaries.RemoveAt(i);
                    dicts.MergedDictionaries.Insert(i, themeResource);
                }
            }
        }
    }
}