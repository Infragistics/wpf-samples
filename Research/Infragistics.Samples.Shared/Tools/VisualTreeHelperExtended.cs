using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Collections.Generic;

namespace Infragistics.Samples.Shared.Tools
{
    public static class VisualTreeHelperExtended
    {
        /// <summary>
        /// Searches the visual tree for an element with the specified name
        /// </summary>
        /// <param name="reference">The parent visual referenced as a System.Windows.DependencyObject</param>
        /// <param name="name">The Name to search for.</param>
        /// <returns></returns>
        public static object FindElementByName(DependencyObject reference, string name)
        {
            if(reference is FrameworkElement && ((FrameworkElement)reference).Name == name)
                return reference;
                
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(reference); i++)
            {
                object result = FindElementByName(VisualTreeHelper.GetChild(reference, i), name);

                if (result != null)
                    return result;
            }

            return null;
        }

        /// <summary>
        /// Returns a list of all children of the reference DependencyObject that are from the specified type T.
        /// </summary>
        /// <typeparam name="T">The type of elements to look for.</typeparam>
        /// <param name="reference">The parent visual referenced as a System.Windows.DependencyObject</param>
        /// <returns></returns>
        public static List<T> FindElementsByType<T>(DependencyObject reference)
        {
            List<T> result = new List<T>();

            if (reference.GetType() == typeof(T))
                result.Add((T)Convert.ChangeType(reference, typeof(T), null));

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(reference); i++)
            {
                result.AddRange(FindElementsByType<T>(VisualTreeHelper.GetChild(reference, i)));
            }

            return result;
        }
    }
}
