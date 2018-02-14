using System.Windows;
using System.Windows.Controls;

namespace IgExcel.Infrastructure.AttachedProperties
{
    public class ListBoxProperties
    {
        #region AutoscrollToSelectedItemProperty

        public static readonly DependencyProperty AutoscrollToSelectedItemProperty = DependencyProperty.RegisterAttached("AutoscrollToSelectedItem", typeof(bool), typeof(ListBoxProperties), new PropertyMetadata(false, OnAutoscrollToSelectedItemChanged));

        private static void OnAutoscrollToSelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ListBox listBox = (ListBox)d;

            if ((bool)e.NewValue)
            {
                listBox.SelectionChanged += OnListView_SelectionChanged;
                listBox.Loaded += listBox_Loaded;
            }
            else
            {
                listBox.SelectionChanged -= OnListView_SelectionChanged;
            }
        }

        static void listBox_Loaded(object sender, RoutedEventArgs e)
        {
            ListBox listBox = (ListBox)sender;
            listBox.ScrollIntoView(listBox.SelectedItem);
        }

        private static void OnListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox listBox = (ListBox)sender;
            listBox.ScrollIntoView(listBox.SelectedItem);
        }


        public static void SetAutoscrollToSelectedItem(ListBox menu, object value)
        {
            menu.SetValue(AutoscrollToSelectedItemProperty, value);
        }

        public static bool GetAutoscrollToSelectedItem(ListBox menu)
        {
            return (bool)menu.GetValue(AutoscrollToSelectedItemProperty);
        }

        #endregion //SelectedItemNameProperty
    }
}
