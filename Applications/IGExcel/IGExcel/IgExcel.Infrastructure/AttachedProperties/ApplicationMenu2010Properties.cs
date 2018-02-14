using Infragistics.Windows.Ribbon;
using System.Windows;
using System.Windows.Controls;

namespace IgExcel.Infrastructure.AttachedProperties
{
    public class ApplicationMenu2010Properties
    {
        #region SelectedItemNameProperty

        public static readonly DependencyProperty SelectedItemNameProperty = DependencyProperty.RegisterAttached("SelectedItemName", typeof(string), typeof(ApplicationMenu2010Properties), new PropertyMetadata(null, null, OnCoerceSelectemName));

        private static object OnCoerceSelectemName(DependencyObject d, object baseValue)
        {
            if (baseValue == null) return null;

            ApplicationMenu2010 menu = (ApplicationMenu2010)d;
            string value = baseValue.ToString();

            if (!string.IsNullOrEmpty(value))
            {
                foreach (var item in menu.Items)
                {
                    if (((Control)item).Name == value)
                    {
                        menu.SelectedTabItem = item;
                        break;
                    }
                }
            }
            return baseValue;
        }

        public static void SetSelectedItemName(ApplicationMenu2010 menu, object value)
        {
            menu.SetValue(SelectedItemNameProperty, value);
        }

        public static string GetSelectedItemName(ApplicationMenu2010 menu)
        {
            return menu.GetValue(SelectedItemNameProperty) as string;
        }

        #endregion //SelectedItemNameProperty
    }
}
