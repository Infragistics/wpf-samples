using Infragistics.Windows.Controls;
using System.Windows;
using System.Windows.Controls;

namespace IgExcel.Infrastructure.AttachedProperties
{
    #region SelectedItemNameProperty

    public class XamTabControlProperties
    {
        public static readonly DependencyProperty SelectedItemNameProperty = DependencyProperty.RegisterAttached("SelectedItemName", typeof(string), typeof(XamTabControlProperties), new PropertyMetadata(null, null, OnCoerceSelectemName));

        private static object OnCoerceSelectemName(DependencyObject d, object baseValue)
        {
            if (baseValue == null) return null;

            XamTabControl tabControl = (XamTabControl)d;
            string value = baseValue.ToString();

            if (!string.IsNullOrEmpty(value))
            {
                foreach (var item in tabControl.Items)
                {
                    if (((Control)item).Name == value)
                    {
                        tabControl.SelectedItem = item;
                        break;
                    }
                }
            }
            return baseValue;
        }

        public static void SetSelectedItemName(XamTabControl source, object value)
        {
            source.SetValue(SelectedItemNameProperty, value);
        }

        public static string GetSelectedItemName(XamTabControl source)
        {
            return source.GetValue(SelectedItemNameProperty) as string;
        }

    #endregion //SelectedItemNameProperty
    }
}
