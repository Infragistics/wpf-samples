using System;
using System.Windows;
using System.Windows.Controls;

namespace IGExtensions.Framework.Extensions
{
    public class GridExtension : DependencyObject
    {
        #region RowHeight

        public static void SetRowHeight(DependencyObject o, double value)
        {
            var row = o as RowDefinition;
            if (row != null && row.MinHeight < value) 
                o.SetValue(RowHeightProperty, value);
        }

        public static double GetRowHeight(DependencyObject o)
        {
            return (double)o.GetValue(RowHeightProperty);
        }

        private static void OnRowHeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            double newValue = Math.Max(0, (double)e.NewValue);
            ((RowDefinition)d).Height = new GridLength(newValue, GridUnitType.Star);
        }

        public static readonly DependencyProperty RowHeightProperty =
          DependencyProperty.RegisterAttached("RowHeight",
            typeof(double), typeof(GridExtension),
            new PropertyMetadata(new PropertyChangedCallback(OnRowHeightChanged)));

        public double RowHeight
        {
            get { return (double)GetValue(RowHeightProperty); }
            set
            {
                SetValue(RowHeightProperty, value);
            }
        }

        #endregion RowHeight

        #region ColumnWidth

        public static void SetColumnWidth(DependencyObject o, double value)
        {
            var column = o as ColumnDefinition;
            if (column != null && column.MinWidth < value)
                o.SetValue(ColumnWidthProperty, value);
        }

        public static double GetColumnWidth(DependencyObject o)
        {
            return (double)o.GetValue(ColumnWidthProperty);
        }

        private static void OnColumnWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            double newValue = Math.Max(0, (double)e.NewValue);
            ((ColumnDefinition)d).Width = new GridLength(newValue, GridUnitType.Pixel);
        }

        public static readonly DependencyProperty ColumnWidthProperty =
           DependencyProperty.RegisterAttached("ColumnWidth",
             typeof(double), typeof(GridExtension),
             new PropertyMetadata(new PropertyChangedCallback(OnColumnWidthChanged)));

        public double ColumnWidth
        {
            get { return (double)GetValue(ColumnWidthProperty); }
            set
            {
                SetValue(ColumnWidthProperty, value);
            }
        }

        #endregion ColumnWidth
    }
    //public class GridExtension : DependencyObject
    //{
    //    #region Row's ExpansionHeight

    //    public static readonly DependencyProperty ExpansionHeightProperty =
    //      DependencyProperty.RegisterAttached("ExpansionHeight",
    //        typeof(double), typeof(GridExtension),
    //        new PropertyMetadata(new PropertyChangedCallback(OnExpansionHeightChanged)));

    //    public double ExpansionHeight
    //    {
    //        get { return (double)GetValue(ExpansionHeightProperty); }
    //        set
    //        {
    //            SetValue(ExpansionHeightProperty, value);
    //        }
    //    }
    //    private static void OnExpansionHeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    //    {
    //        double newValue = System.Math.Max(0, (double)e.NewValue);
    //        ((RowDefinition)d).Height = new GridLength(newValue, GridUnitType.Star);
    //    }
    //    public static void SetRowHeight(DependencyObject o, double value)
    //    {
    //        o.SetValue(ExpansionHeightProperty, value);
    //    }

    //    public static double GetRowHeight(DependencyObject o)
    //    {
    //        return (double)o.GetValue(ExpansionHeightProperty);
    //    }

    //    #endregion RowHeight

    //    #region Column's AnimatioWidth

    //    public static readonly DependencyProperty ExpansionWidthProperty =
    //       DependencyProperty.RegisterAttached("ExpansionWidth",
    //         typeof(double), typeof(GridExtension),
    //         new PropertyMetadata(new PropertyChangedCallback(OnExpansionWidthChanged)));

    //    public double ExpansionWidth
    //    {
    //        get { return (double)GetValue(ExpansionWidthProperty); }
    //        set
    //        {
    //            SetValue(ExpansionWidthProperty, value);
    //        }
    //    }

    //    private static void OnExpansionWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    //    {
    //        double newValue = System.Math.Max(0, (double)e.NewValue);
    //        ((ColumnDefinition)d).Width = new GridLength(newValue, GridUnitType.Pixel);
    //    }

    //    public static void SetColumnWidth(DependencyObject o, double value)
    //    {
    //        o.SetValue(ExpansionWidthProperty, value);
    //    }

    //    public static double GetColumnWidth(DependencyObject o)
    //    {
    //        return (double)o.GetValue(ExpansionWidthProperty);
    //    }
    //    #endregion ColumnWidth
    //}
}