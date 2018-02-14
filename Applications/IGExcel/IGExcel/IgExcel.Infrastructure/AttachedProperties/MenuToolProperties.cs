using Infragistics.Windows;
using Infragistics.Windows.Ribbon;
using System.Windows;

namespace IgExcel.Infrastructure.AttachedProperties
{
    public class MenuToolProperties
    {
        #region EnableSelectionTrackingProperty

        public static readonly DependencyProperty EnableSelectionTrackingProperty = DependencyProperty.RegisterAttached("EnableSelectionTracking ", typeof(bool), typeof(MenuToolProperties), new PropertyMetadata(false, OnEnableSelectionTrackingChanged));

        public static void SetEnableSelectionTracking(MenuTool source, bool value)
        {
            source.SetValue(EnableSelectionTrackingProperty, value);
        }

        public static bool GetEnableSelectionTracking(MenuTool source)
        {
            return (bool)source.GetValue(EnableSelectionTrackingProperty);
        }

        private static void OnEnableSelectionTrackingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MenuTool menuTool = d as MenuTool;

            menuTool.Loaded -= OnMenuToolLoaded;
            menuTool.Loaded += OnMenuToolLoaded;
        }

        private static void OnMenuToolLoaded(object sender, RoutedEventArgs e)
        {
            MenuTool menuTool = sender as MenuTool;

            foreach (var item in menuTool.Items)
            {
                if (item is ButtonTool)
                {
                    var buttonTool = (ButtonTool)item;

                    buttonTool.Click += (s, a) =>
                    {
                        var btnTool = (ButtonTool)s;

                        var mTool = (MenuTool)Utilities.GetAncestorFromType(btnTool, typeof(MenuTool), false);

                        if (mTool != null)
                        {
                            mTool.Command = btnTool.Command;
                            mTool.CommandParameter = btnTool.CommandParameter;

                            mTool.SmallImage = btnTool.SmallImage;
                        }
                    };
                }
            }
        }



        #endregion //EnableSelectionTrackingProperty
    }
}
