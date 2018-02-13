using System.Windows;
using System.Windows.Controls;

namespace IgWord.Infrastructure.AttachedProperties
{
    public class ListBoxProperties
    {
        #region EnableScrollToSelcetedItemProperty

        public static readonly DependencyProperty EnableScrollToSelcetedItemProperty = DependencyProperty.RegisterAttached("EnableScrollToSelcetedItem", typeof(bool), typeof(ListBoxProperties), new PropertyMetadata(false, OnEnableScrollToSelcetedItemChanged));

        public static void SetEnableScrollToSelcetedItem(ListBox source, bool value)
        {
            source.SetValue(EnableScrollToSelcetedItemProperty, value);
        }

        public static bool GetEnableScrollToSelcetedItem(ListBox source)
        {
            return (bool)source.GetValue(EnableScrollToSelcetedItemProperty);
        }

        private static void OnEnableScrollToSelcetedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ListBox listBox = d as ListBox;

            if (listBox != null)
            {
                if ((bool)e.NewValue == true)
                {
                    listBox.SelectionChanged += listBox_SelectionChanged;
                }
                else
                {
                    listBox.SelectionChanged -= listBox_SelectionChanged;
                }

            }
        }

        private static void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox listBox = sender as ListBox;
            listBox.ScrollIntoView(listBox.SelectedItem);
        }

        #endregion //EnableScrollToSelcetedItemProperty
    }
}
