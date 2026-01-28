using System.Windows;

namespace Infragistics.Samples.Shared.Tools
{
    /// <summary>
    /// Provides useful methods when working with Theme resource dictionary
    /// </summary>
    public class ThemeResourceHelper
    {
        /// <summary>
        /// Removes ResourceDictionary from the list of ResourceDictionary objects in the given ResourceDictionary's MergedDictionaries,
        /// which has the specified string in their names.
        /// </summary>
        public static void RemoveResourceDictionariesForControl(ResourceDictionary dicts, string controlName)
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
    }
}