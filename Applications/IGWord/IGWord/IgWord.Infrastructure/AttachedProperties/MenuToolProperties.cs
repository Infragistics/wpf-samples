using Infragistics.Windows.Ribbon;
using System.Windows;

namespace IgWord.Infrastructure.AttachedProperties
{
    public class MenuToolProperties
    {
        #region SelectedItemProperty

        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.RegisterAttached("SelectedItem", typeof(object), typeof(MenuToolProperties), new PropertyMetadata(null, OnSelectedItemChanged));

        private static void OnSelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }

        public static void SetSelectedItem(MenuTool menu, object value)
        {
            menu.SetValue(SelectedItemProperty, value);
        }

        public static string GetSelectedItem(MenuTool menu)
        {
            return menu.GetValue(SelectedItemProperty) as string;
        }

        #endregion //SelectedItemProperty

        #region EnableSelectionProperty

        public static readonly DependencyProperty EnableSelectionProperty = DependencyProperty.RegisterAttached("EnableSelection", typeof(bool), typeof(MenuToolProperties), new PropertyMetadata(false, OnEnableSelectionChanged));

        private static void OnEnableSelectionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MenuTool menu = d as MenuTool;

            if (menu != null)
            {
                if ((bool)e.NewValue)
                {
                    menu.Opening += menu_Opening;
                }
                else
                {
                    menu.Opening -= menu_Opening;
                }
            }
        }

        private static void menu_Opening(object sender, Infragistics.Windows.Ribbon.Events.ToolOpeningEventArgs e)
        {
            MenuTool menu = sender as MenuTool;
          
            object selectedItem = menu.GetValue(MenuToolProperties.SelectedItemProperty);

            if (selectedItem != null)
            {
                var dd = menu.Items[0];
             
            }
        }

        public static void SetEnableSelection(MenuTool menu, object value)
        {
            menu.SetValue(EnableSelectionProperty, value);
        }

        public static string GetEnableSelection(MenuTool menu)
        {
            return menu.GetValue(EnableSelectionProperty) as string;
        }

        #endregion //EnableSelectionProperty
    }
}
