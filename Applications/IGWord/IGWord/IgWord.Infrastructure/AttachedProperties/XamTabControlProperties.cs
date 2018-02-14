using Infragistics.Windows.Controls;
using System.Windows;

namespace IgWord.Infrastructure.AttachedProperties
{
    public class XamTabControlProperties
    {
        #region SelectedItemNameProperty

        public static readonly DependencyProperty SelectedItemNameProperty = DependencyProperty.RegisterAttached("SelectedItemName", typeof(string), typeof(XamTabControlProperties), new PropertyMetadata(null, null, OnCoerceSelectemName));

        private static object OnCoerceSelectemName(DependencyObject d, object baseValue)
        {
            if (baseValue == null) return null;

            XamTabControl tabControl = (XamTabControl)d;
            string value = baseValue.ToString();

            if (!string.IsNullOrEmpty(value))
            {
                int index = 0;
                foreach (var item in tabControl.Items)
                {
                    if (((TabItemEx)item).Name == value)
                    {
                        tabControl.SelectedIndex = index;
                        break;
                    }

                    index++;
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
