using Infragistics.Controls.Charts; 
using System.Collections.Generic; 
using System.Windows;
using System.Windows.Controls; 
using System.Windows.Media;

namespace IGFinancialChart.Samples 
{         
    /// <summary>
    /// Represents a class with attachable properties for toolbar in Financial Chart.
    /// </summary>
    public class ToolbarOptions
    {
        #region IsIndicatorVisible
        public static readonly DependencyProperty IsIndicatorVisibleProperty =
            DependencyProperty.RegisterAttached("IsIndicatorVisible", typeof(bool),
            typeof(ToolbarOptions), new PropertyMetadata(true, OnPropertyChanged));

        public static bool GetIsIndicatorVisible(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsIndicatorVisibleProperty);
        }
        public static void SetIsIndicatorVisible(DependencyObject obj, bool value)
        {
            obj.SetValue(IsIndicatorVisibleProperty, value);
        }
        #endregion

        #region IsOverlayVisible
        public static readonly DependencyProperty IsOverlayVisibleProperty =
            DependencyProperty.RegisterAttached("IsOverlayVisible", typeof(bool),
            typeof(ToolbarOptions), new PropertyMetadata(true, OnPropertyChanged));

        public static bool GetIsOverlayVisible(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsOverlayVisibleProperty);
        }
        public static void SetIsOverlayVisible(DependencyObject obj, bool value)
        {
            obj.SetValue(IsOverlayVisibleProperty, value);
        }        
        #endregion
                		
		#region IsPriceVisible
        public static readonly DependencyProperty IsPriceVisibleProperty =
            DependencyProperty.RegisterAttached("IsPriceVisible", typeof(bool),
            typeof(ToolbarOptions), new PropertyMetadata(true, OnPropertyChanged));

        public static bool GetIsPriceVisible(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsPriceVisibleProperty);
        }
        public static void SetIsPriceVisible(DependencyObject obj, bool value)
        {
            obj.SetValue(IsPriceVisibleProperty, value);
        }        
        #endregion

        #region IsTrendlineVisible
        public static readonly DependencyProperty IsTrendlineVisibleProperty =
            DependencyProperty.RegisterAttached("IsTrendlineVisible", typeof(bool),
            typeof(ToolbarOptions), new PropertyMetadata(true, OnPropertyChanged));

        public static bool GetIsTrendlineVisible(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsTrendlineVisibleProperty);
        }
        public static void SetIsTrendlineVisible(DependencyObject obj, bool value)
        {
            obj.SetValue(IsTrendlineVisibleProperty, value);
        }        
        #endregion
        				
		#region IsVolumeVisible
        public static readonly DependencyProperty IsVolumeVisibleProperty =
            DependencyProperty.RegisterAttached("IsVolumeVisible", typeof(bool),
            typeof(ToolbarOptions), new PropertyMetadata(true, OnPropertyChanged));

        public static bool GetIsVolumeVisible(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsVolumeVisibleProperty);
        }
        public static void SetIsVolumeVisible(DependencyObject obj, bool value)
        {
            obj.SetValue(IsVolumeVisibleProperty, value);
        }        
        #endregion
        		
		#region IsRangeVisible
        public static readonly DependencyProperty IsRangeVisibleProperty =
            DependencyProperty.RegisterAttached("IsRangeVisible", typeof(bool),
            typeof(ToolbarOptions), new PropertyMetadata(true, OnPropertyChanged));

        public static bool GetIsRangeVisible(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsRangeVisibleProperty);
        }
        public static void SetIsRangeVisible(DependencyObject obj, bool value)
        {
            obj.SetValue(IsRangeVisibleProperty, value);
        }        
        #endregion
                		
		#region IsXAxisModeVisible
        public static readonly DependencyProperty IsXAxisModeVisibleProperty =
            DependencyProperty.RegisterAttached("IsXAxisModeVisible", typeof(bool),
            typeof(ToolbarOptions), new PropertyMetadata(true, OnPropertyChanged));

        public static bool GetIsXAxisModeVisible(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsXAxisModeVisibleProperty);
        }
        public static void SetIsXAxisModeVisible(DependencyObject obj, bool value)
        {
            obj.SetValue(IsXAxisModeVisibleProperty, value);
        }        
        #endregion
        		
		#region IsYAxisModeVisible
        public static readonly DependencyProperty IsYAxisModeVisibleProperty =
            DependencyProperty.RegisterAttached("IsYAxisModeVisible", typeof(bool),
            typeof(ToolbarOptions), new PropertyMetadata(true, OnPropertyChanged));

        public static bool GetIsYAxisModeVisible(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsYAxisModeVisibleProperty);
        }
        public static void SetIsYAxisModeVisible(DependencyObject obj, bool value)
        {
            obj.SetValue(IsYAxisModeVisibleProperty, value);
        }        
        #endregion
                		
		#region IsZoomVisible
        public static readonly DependencyProperty IsZoomVisibleProperty =
            DependencyProperty.RegisterAttached("IsZoomVisible", typeof(bool),
            typeof(ToolbarOptions), new PropertyMetadata(false, OnPropertyChanged));

        public static bool GetIsZoomVisible(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsZoomVisibleProperty);
        }
        public static void SetIsZoomVisible(DependencyObject obj, bool value)
        {
            obj.SetValue(IsZoomVisibleProperty, value);
        }        
        #endregion

        internal static XamFinancialChart Chart;
        internal static FinancialChartToolbar Toolbar;
        internal static FinancialChartRangeSelector RangeSelector;
        // TODO-MTRELA enable custom type pickers after adding Framework.EnumComboBox control 
        //internal static FinancialChartTrendLineTypePicker TrendLinePicker;
        //internal static FinancialChartTypePicker PricePicker;
        //internal static FinancialXAxisModePicker XAxisModePicker;
        //internal static FinancialYAxisModePicker YAxisModePicker;
        internal static Control ZoomPicker;
                       
        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (Chart == null)
            {
                Chart = d as XamFinancialChart;
                if (Chart == null) return;

                if (Chart.IsLoaded)
                    InitializeToolbar();
                else
                    Chart.Loaded += (s,o) => InitializeToolbar();
                                
                Chart.Unloaded += Chart_Unloaded; 
            }  
            UpdateToolbar(); 
        }

        private static void InitializeToolbar()
        {
            // TODO-MTRELA enable custom type pickers after adding Framework.EnumComboBox control 
            // find visual elements of the toolbar
            Toolbar = Chart.GetChildOfType<FinancialChartToolbar>();
            RangeSelector = Chart.GetChildOfType<FinancialChartRangeSelector>();
            //TrendLinePicker = Chart.GetChildOfType<FinancialChartTrendLineTypePicker>();
            //PricePicker = Chart.GetChildOfType<FinancialChartTypePicker>();
            //XAxisModePicker = Chart.GetChildOfType<FinancialXAxisModePicker>();
            //YAxisModePicker = Chart.GetChildOfType<FinancialYAxisModePicker>();
            //ZoomPicker = Chart.GetChildOfType<FinancialZoomTypePicker>();
                        
            UpdateToolbar(); 
        }

        private static void UpdateToolbar()
        { 
            // update visibility of elements of the toolbar
            if (Chart == null) return;
            if (Toolbar == null) return;
           
            // TODO-MTRELA enable custom type pickers after adding Framework.EnumComboBox control 
            var visibility = GetIsIndicatorVisible(Chart) ? Visibility.Visible : Visibility.Collapsed;
            //if (Toolbar.IndicatorPicker != null)
            //    Toolbar.IndicatorPicker.Visibility = visibility;
                        
            //visibility = GetIsOverlayVisible(Chart) ? Visibility.Visible : Visibility.Collapsed;
            //if (Toolbar.OverlayPicker != null)
            //    Toolbar.OverlayPicker.Visibility = visibility;
                       
            //visibility = GetIsVolumeVisible(Chart) ? Visibility.Visible : Visibility.Collapsed;
            //if (Toolbar.VolumeTypePicker != null)
            //    Toolbar.VolumeTypePicker.Visibility = visibility;
            
            //visibility = GetIsTrendlineVisible(Chart) ? Visibility.Visible : Visibility.Collapsed;
            //if (TrendLinePicker != null)
            //    TrendLinePicker.Visibility = visibility;

            visibility = GetIsPriceVisible(Chart) ? Visibility.Visible : Visibility.Collapsed;
            //if (PricePicker != null)
            //    PricePicker.Visibility = visibility;

            visibility = GetIsRangeVisible(Chart) ? Visibility.Visible : Visibility.Collapsed;
            if (RangeSelector != null)
                RangeSelector.Visibility = visibility;

            visibility = GetIsXAxisModeVisible(Chart) ? Visibility.Visible : Visibility.Collapsed;
            //if (XAxisModePicker != null)
            //    XAxisModePicker.Visibility = visibility;

            visibility = GetIsYAxisModeVisible(Chart) ? Visibility.Visible : Visibility.Collapsed;
            //if (YAxisModePicker != null)
            //    YAxisModePicker.Visibility = visibility;

            visibility = GetIsZoomVisible(Chart) ? Visibility.Visible : Visibility.Collapsed;
            if (ZoomPicker != null)
                ZoomPicker.Visibility = visibility;
        }
        
        private static void Chart_Unloaded(object sender, RoutedEventArgs e)
        {
            // TODO-MTRELA enable custom type pickers after adding Framework.EnumComboBox control 
            // cleanup static elements
            //XAxisModePicker = null;
            //YAxisModePicker = null;
            //TrendLinePicker = null;
            RangeSelector = null;
            //PricePicker = null;
            ZoomPicker = null;
            Toolbar = null; 
            Chart = null; 
        }
    }
}

namespace System.Windows
{    
    public static class DependencyObjectEx
    {
        public static void SetVisibility(this Control control, bool isVible)
        { 
            if (control != null)  
                control.Visibility = isVible ? Visibility.Visible : Visibility.Collapsed;
        }

        public static T GetChildOfType<T>(this DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null) return null;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);

                var result = (child as T) ?? GetChildOfType<T>(child);
                if (result != null)
                    return result;
            }
            return null;
        }

        public static List<DependencyObject> GetLogicalChildren(this DependencyObject parent)
        {
            var list = new List<DependencyObject>();
            if (parent == null) return list;

            //use the logical tree for content / framework elements
            foreach (object obj in LogicalTreeHelper.GetChildren(parent))
            {
                var depObj = obj as DependencyObject;
                if (depObj != null)
                    list.Add(depObj);
            }
            return list;
        }
        public static List<DependencyObject> GetVisualChildren(this DependencyObject parent)
        { 
            var list = new List<DependencyObject>();
            if (parent == null) return list;

            //use the visual tree for Visual  
            if (parent is Visual)
            {
                int count = VisualTreeHelper.GetChildrenCount(parent);
                for (int i = 0; i < count; i++)
                {
                    list.Add(VisualTreeHelper.GetChild(parent, i));
                }
            }
            return list;
        }
      
    }
}
