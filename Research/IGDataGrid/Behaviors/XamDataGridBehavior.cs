using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace IGDataGrid.Behaviors
{
    class XamDataGridBehavior
    {
        #region DisplayAdorningEditors

        static readonly Dictionary<Infragistics.Windows.DataPresenter.XamDataGrid, AdorningEditorManager> _gridToAdorningEditorManagerMap =
            new Dictionary<Infragistics.Windows.DataPresenter.XamDataGrid, AdorningEditorManager>();

        public static bool GetDisplayAdorningEditors(DependencyObject obj)
        {
            return (bool)obj.GetValue(DisplayAdorningEditorsProperty);
        }

        public static void SetDisplayAdorningEditors(DependencyObject obj, bool value)
        {
            obj.SetValue(DisplayAdorningEditorsProperty, value);
        }

        /// <summary>
        /// Identifies the DisplayAdorningEditors attached property.
        /// This field is read-only.
        /// </summary>
        public static readonly DependencyProperty DisplayAdorningEditorsProperty =
            DependencyProperty.RegisterAttached(
            "DisplayAdorningEditors",
            typeof(bool),
            typeof(XamDataGridBehavior),
            new UIPropertyMetadata(false, OnDisplayAdorningEditorsChanged));

        static void OnDisplayAdorningEditorsChanged(DependencyObject depObj, DependencyPropertyChangedEventArgs e)
        {
            Infragistics.Windows.DataPresenter.XamDataGrid dataGrid = depObj as Infragistics.Windows.DataPresenter.XamDataGrid;
            if (dataGrid == null)
                throw new ArgumentException("DisplayAdorningEditors can only be set on a XamDataGrid.");

            if (e.NewValue is bool && (bool)e.NewValue)
            {
                _gridToAdorningEditorManagerMap[dataGrid] = new AdorningEditorManager(dataGrid);
            }
            else if (_gridToAdorningEditorManagerMap.ContainsKey(dataGrid))
            {
                _gridToAdorningEditorManagerMap[dataGrid].DetachFromGrid();
                _gridToAdorningEditorManagerMap.Remove(dataGrid);
            }
        }

        #endregion // DisplayAdorningEditors

        #region RequestAdorningEditor

        /// <summary>
        /// Identifies the RequestAdorningEditor attached bubbling event.
        /// This field is read-only.
        /// </summary>
        public static readonly RoutedEvent RequestAdorningEditorEvent =
            EventManager.RegisterRoutedEvent(
                "RequestAdorningEditor",
                RoutingStrategy.Bubble,
                typeof(RequestAdorningEditorRoutedEventHandler),
                typeof(XamDataGridBehavior));

        public static void AddRequestAdorningEditorHandler(DependencyObject depObj, RequestAdorningEditorRoutedEventHandler handler)
        {
            (depObj as UIElement).AddHandler(RequestAdorningEditorEvent, handler);
        }

        public static void RemoveRequestAdorningEditorHandler(DependencyObject depObj, RequestAdorningEditorRoutedEventHandler handler)
        {
            (depObj as UIElement).RemoveHandler(RequestAdorningEditorEvent, handler);
        }

        #endregion // RequestAdorningEditor        
    }
}
