using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using IGExtensions.Framework;
using Infragistics.Controls.Charts;
using IGShowcase.FinanceDashboard.ViewModels;

namespace IGShowcase.FinanceDashboard.Controls
{
    /// <summary>
    /// Represents an extension for the XamDataChart control
    /// </summary>
    public class DataChartEx : XamDataChart
    {
        #region SelectedChartType (DependencyProperty)
        /// <summary>
        /// Gets or sets the type of the selected chart.
        /// </summary>
        /// <value>The type of the selected chart.</value>
        public StockChartType SelectedChartType
        {
            get { return (StockChartType)GetValue(SelectedChartTypeProperty); }
            set { SetValue(SelectedChartTypeProperty, value); }
        }

        public static readonly DependencyProperty SelectedChartTypeProperty =
            DependencyProperty.Register(
                "SelectedChartType",
                typeof(StockChartType),
                typeof(DataChartEx),
                new PropertyMetadata(OnSelectedChartTypeChanged));

        /// <summary>
        /// Called when [selected chart type changed].
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnSelectedChartTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DebugManager.Time("DataChartEx.OnSelectedChartTypeChanged");
            var sourceChart = (DataChartEx)d;
            var windowRect = sourceChart.ActualWindowRect;   

            // save axis settings
            var xAxis = sourceChart.Axes.OfType<CategoryXAxis>().FirstOrDefault();
            object xAxisLabel = null;
            if (xAxis != null) xAxisLabel = xAxis.Label;

            var yAxis = sourceChart.Axes.OfType<NumericYAxis>().FirstOrDefault();
            var yAxisIsLogarithmic = yAxis.IsLogarithmic;

            // load axes and series based on selected chart type
            sourceChart.OnSeriesSourceChanged(null, sourceChart.SeriesSource);
            sourceChart.WindowRect = windowRect;

            // apply axis settings
            xAxis = sourceChart.Axes.OfType<CategoryXAxis>().FirstOrDefault();
            if (xAxis != null && xAxisLabel != null) xAxis.Label = xAxisLabel;

            yAxis = sourceChart.Axes.OfType<NumericYAxis>().FirstOrDefault();
            if (yAxis != null) yAxis.IsLogarithmic = yAxisIsLogarithmic;
          
            DebugManager.Time("DataChartEx.OnSelectedChartTypeChanged");
        }
        #endregion SelectedChartType (DependencyProperty)

        #region SeriesSource (DependencyProperty)
        /// <summary>
        /// Gets or sets the series source.
        /// </summary>
        /// <value>The series source.</value>
        public IEnumerable SeriesSource
        {
            get { return (IEnumerable)GetValue(SeriesSourceProperty); }
            set { SetValue(SeriesSourceProperty, value); }
        }

        public static readonly DependencyProperty SeriesSourceProperty =
            DependencyProperty.Register(
                "SeriesSource",
                typeof(IEnumerable),
                typeof(DataChartEx),
                new PropertyMetadata(OnSeriesSourceChanged));

        /// <summary>
        /// Called when [series source changed].
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnSeriesSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var oldValue = (IEnumerable)e.OldValue;
            var newValue = (IEnumerable)e.NewValue;
            var source = (DataChartEx)d;

            //TODO: Change over to using a Weak Event pattern
            INotifyCollectionChanged ncc;

            if (oldValue != null)
            {
                ncc = oldValue as INotifyCollectionChanged;
                if (ncc != null)
                {
                    ncc.CollectionChanged -= source.Series_CollectionChanged;
                }
            }
            if (newValue != null)
            {
                ncc = newValue as INotifyCollectionChanged;
                if (ncc != null)
                {
                    ncc.CollectionChanged += source.Series_CollectionChanged;
                }
            }
            source.OnSeriesSourceChanged(oldValue, newValue);
        }

        /// <summary>
        /// Called when [series source changed].
        /// </summary>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        protected virtual void OnSeriesSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            object xAxisLabel = null;
            var xAxes = this.Axes.OfType<CategoryXAxis>().ToList();
            if (xAxes.Count > 0)
            {
                xAxisLabel = xAxes.First().Label;
            }
           
            DebugManager.Time("DataChartEx.OnSeriesSourceChanged");
            Series.Clear();
            Axes.Clear();

            if (newValue != null)
            {
                #region Axes and Series
                var yAxisTemplate = GetCurentAxisCategoryYTemplate(DataContext);
                if (yAxisTemplate != null)
                {
                    // load data template content
                    var yAxis = yAxisTemplate.LoadContent() as NumericYAxis;
                    if (yAxis != null)
                    {
                        yAxis.IsLogarithmic = AxisValueIsLogarithmic;
                        yAxis.DataContext = DataContext;
                        yAxis.LabelSettings.Visibility = this.AxisCategoryYVisibility;
                        Axes.Add(yAxis);
                    }
                }
                var axisCategoryXTemplate = GetCurentAxisCategoryXTemplate(DataContext);
                foreach (var item in newValue)
                {
                    var dataTemplate = GetCurentSeriesTemplate(item);
                    if (dataTemplate != null)
                    {
                        // load data template content
                        var series = dataTemplate.LoadContent() as Series;
                        // load data template for the axis
                        var axis = axisCategoryXTemplate.LoadContent() as Axis;
                        if (series != null && axis != null)
                        {
                            // set data context
                            series.DataContext = item;
                            axis.DataContext = item;
                            if (xAxisLabel != null) axis.Label = xAxisLabel;
                           
                            Series.Add(series);
                            Axes.Add(axis);

                            series.RefreshXAxis<CategoryXAxis>(this);
                            //series.RefreshXAxis<CategoryDateTimeXAxis>(this);
                            series.RefreshYAxis<NumericYAxis>(this);
                        }
                    }
                } 
                // make only one axis labels pane visible.
                if (Axes.Any(axis => axis is CategoryXAxis))
                {
                    Axes.Last().LabelSettings.Visibility = this.AxisCategoryXVisibility;
                }
                //if (Axes.Where(axis => axis is CategoryDateTimeXAxis).Count() > 0)
                //{
                //    Axes.Last().LabelSettings.Visibility = Visibility.Visible;
                //}
                
                #endregion
            }
            // hover interaction layers
            if (this.SelectedChartType == StockChartType.Area ||
                this.SelectedChartType == StockChartType.StepArea ||
                this.SelectedChartType == StockChartType.RangeArea ||
                this.SelectedChartType == StockChartType.Line)
            {
                var itemHighlightLayer = new CategoryItemHighlightLayer();
                itemHighlightLayer.MarkerTemplate = Application.Current.Resources["ItemHighlightMarkerTemplate"] as DataTemplate;
                itemHighlightLayer.TransitionDuration = TimeSpan.FromMilliseconds(500);
                itemHighlightLayer.UseInterpolation = false;
                Canvas.SetZIndex(itemHighlightLayer, 10002);
                Series.Add(itemHighlightLayer);
            }

            var crosshairLayer = new CrosshairLayer();
            crosshairLayer.UseInterpolation = false;
            crosshairLayer.TransitionDuration = TimeSpan.FromMilliseconds(500);
            Canvas.SetZIndex(crosshairLayer, 10001);
            Series.Add(crosshairLayer);

            var itemTooltipLayer = new ItemToolTipLayer()
            {
                TransitionDuration = TimeSpan.FromMilliseconds(500),
                UseInterpolation = false
            };
            itemTooltipLayer.ToolTipStyle = Application.Current.Resources["SeriesToolTipStyle"] as Style;
            Canvas.SetZIndex(itemTooltipLayer, 10003);
            Series.Add(itemTooltipLayer);

            DebugManager.Time("DataChartEx.OnSeriesSourceChanged");
           
            // TODO Listen for INotifyCollectionChanged with a weak event pattern
        }

        /// <summary>
        /// Handles the CollectionChanged event of the Series control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Collections.Specialized.NotifyCollectionChangedEventArgs"/> instance containing the event data.</param>
        internal void Series_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnSeriesSourceChanged(null, SeriesSource);
        }

        /// <summary>
        /// Gets the current series template.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        private DataTemplate GetCurentSeriesTemplate(object item)
        {
            DataTemplate dataTemplate = null;

            // get data template
            if (SeriesTemplateSelector != null)
            {
                dataTemplate = SeriesTemplateSelector.SelectTemplate(item, this);
            }
            if (dataTemplate == null && SeriesTemplate != null)
            {
                dataTemplate = SeriesTemplate;
            }

            return dataTemplate;
        }

        /// <summary>
        /// Gets the current axis category X template.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        private DataTemplate GetCurentAxisCategoryXTemplate(object item)
        {
            DataTemplate dataTemplate = null;

            if (AxisCategoryXTemplateSelector != null)
            {
                dataTemplate = AxisCategoryXTemplateSelector.SelectTemplate(item, this);
            }
            if (dataTemplate == null && AxisCategoryXTemplate != null)
            {
                dataTemplate = AxisCategoryXTemplate;
            }

            return dataTemplate;
        }

        /// <summary>
        /// Gets the current axis category Y template.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        private DataTemplate GetCurentAxisCategoryYTemplate(object item)
        {
            DataTemplate dataTemplate = null;

            if (AxisCategoryYTemplateSelector != null)
            {
                dataTemplate = AxisCategoryYTemplateSelector.SelectTemplate(item, this);
            }
            if (dataTemplate == null && AxisCategoryYTemplate != null)
            {
                dataTemplate = AxisCategoryYTemplate;
            }

            return dataTemplate;
        }
        #endregion
        /// <summary>
        /// Gets or sets Visibility of x-axis.
        /// </summary>
        public Visibility AxisCategoryXVisibility
        {
            get { return (Visibility)GetValue(AxisCategoryXVisibilityProperty); }
            set { SetValue(AxisCategoryXVisibilityProperty, value); }
        }

        public static readonly DependencyProperty AxisCategoryXVisibilityProperty =
            DependencyProperty.Register(
                "AxisCategoryXVisibility",
                typeof(Visibility),
                typeof(DataChartEx),
                new PropertyMetadata(Visibility.Visible));
        
        /// <summary>
        /// Gets or sets Visibility of y-axis.
        /// </summary>
        public Visibility AxisCategoryYVisibility
        {
            get { return (Visibility)GetValue(AxisCategoryYVisibilityProperty); }
            set { SetValue(AxisCategoryYVisibilityProperty, value); }
        }

        public static readonly DependencyProperty AxisCategoryYVisibilityProperty =
            DependencyProperty.Register(
                "AxisCategoryYVisibility",
                typeof(Visibility),
                typeof(DataChartEx),
                new PropertyMetadata(Visibility.Visible));

        /// <summary>
        /// Gets or sets logarithmic scale of value-axis.
        /// </summary>
        public bool AxisValueIsLogarithmic
        {
            get { return (bool)GetValue(AxisValueIsLogarithmicProperty); }
            set { SetValue(AxisValueIsLogarithmicProperty, value); }
        }

        public static readonly DependencyProperty AxisValueIsLogarithmicProperty =
            DependencyProperty.Register(
                "AxisValueIsLogarithmic",
                typeof(bool),
                typeof(DataChartEx),
                new PropertyMetadata(false, OnAxisScaleChanged));

        #region SeriesTemplate (DependencyProperty)
        private static void OnAxisScaleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DebugManager.Time("DataChartEx.OnAxisScaleChanged");
            var sourceChart = (DataChartEx)d;
            var yAxis = sourceChart.Axes.OfType<NumericYAxis>().FirstOrDefault();
            if (yAxis != null)
            {
                yAxis.IsLogarithmic = (bool)e.NewValue;
            }
            
            DebugManager.Time("DataChartEx.OnAxisScaleChanged");
        }
        /// <summary>
        /// Gets or sets the series template.
        /// </summary>
        /// <value>The series template.</value>
        public DataTemplate SeriesTemplate
        {
            get { return (DataTemplate)GetValue(SeriesTemplateProperty); }
            set { SetValue(SeriesTemplateProperty, value); }
        }

        public static readonly DependencyProperty SeriesTemplateProperty =
            DependencyProperty.Register(
                "SeriesTemplate",
                typeof(DataTemplate),
                typeof(DataChartEx),
                new PropertyMetadata(default(DataTemplate)));

        #endregion SeriesTemplate (DependencyProperty)

        #region SeriesTemplateSelector (DependencyProperty)

        /// <summary>
        /// Gets or sets the series template selector.
        /// </summary>
        /// <value>The series template selector.</value>
        public DataTemplateSelector SeriesTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(SeriesTemplateSelectorProperty); }
            set { SetValue(SeriesTemplateSelectorProperty, value); }
        }
        public static readonly DependencyProperty SeriesTemplateSelectorProperty =
            DependencyProperty.Register(
                "SeriesTemplateSelector",
                typeof(DataTemplateSelector),
                typeof(DataChartEx),
                new PropertyMetadata(default(DataTemplateSelector)));

        #endregion SeriesTemplateSelector (DependencyProperty)

        #region AxisCategoryXTemplate (DependencyProperty)

        /// <summary>
        /// Gets or sets the axis template.
        /// </summary>
        /// <value>The series template.</value>
        public DataTemplate AxisCategoryXTemplate
        {
            get { return (DataTemplate)GetValue(AxisCategoryXTemplateProperty); }
            set { SetValue(AxisCategoryXTemplateProperty, value); }
        }

        public static readonly DependencyProperty AxisCategoryXTemplateProperty =
            DependencyProperty.Register(
                "AxisCategoryXTemplate",
                typeof(DataTemplate),
                typeof(DataChartEx),
                new PropertyMetadata(default(DataTemplate)));

        #endregion AxisCategoryXTemplate (DependencyProperty)

        #region AxisCategoryXTemplateSelector (DependencyProperty)

        /// <summary>
        /// Gets or sets the axis template selector.
        /// </summary>
        /// <value>The series template selector.</value>
        public DataTemplateSelector AxisCategoryXTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(AxisCategoryXTemplateSelectorProperty); }
            set { SetValue(AxisCategoryXTemplateSelectorProperty, value); }
        }

        public static readonly DependencyProperty AxisCategoryXTemplateSelectorProperty =
            DependencyProperty.Register(
                "AxisCategoryXTemplateSelector",
                typeof(DataTemplateSelector),
                typeof(DataChartEx),
                new PropertyMetadata(default(DataTemplateSelector)));

        #endregion AxisCategoryXTemplateSelector (DependencyProperty)

        #region AxisCategoryYTemplate (DependencyProperty)

        /// <summary>
        /// Gets or sets the axis template.
        /// </summary>
        /// <value>The series template.</value>
        public DataTemplate AxisCategoryYTemplate
        {
            get { return (DataTemplate)GetValue(AxisCategoryYTemplateProperty); }
            set { SetValue(AxisCategoryYTemplateProperty, value); }
        }

        public static readonly DependencyProperty AxisCategoryYTemplateProperty =
            DependencyProperty.Register(
                "AxisCategoryYTemplate",
                typeof(DataTemplate),
                typeof(DataChartEx),
                new PropertyMetadata(default(DataTemplate)));

        #endregion AxisCategoryXTemplate (DependencyProperty)

        #region AxisCategoryYTemplateSelector (DependencyProperty)

        /// <summary>
        /// Gets or sets the axis template selector.
        /// </summary>
        /// <value>The series template selector.</value>
        public DataTemplateSelector AxisCategoryYTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(AxisCategoryYTemplateSelectorProperty); }
            set { SetValue(AxisCategoryYTemplateSelectorProperty, value); }
        }

        public static readonly DependencyProperty AxisCategoryYTemplateSelectorProperty =
            DependencyProperty.Register(
                "AxisCategoryYTemplateSelector",
                typeof(DataTemplateSelector),
                typeof(DataChartEx),
                new PropertyMetadata(default(DataTemplateSelector)));

        #endregion AxisCategoryYTemplateSelector (DependencyProperty)
    }

    internal static class DataChartExtensions
    {
        /// <summary>
        /// Refreshes the X axis.
        /// </summary>
        /// <typeparam name="T">The type of the axis type.</typeparam>
        /// <param name="series">The series.</param>
        /// <param name="source">The source.</param>
        internal static void RefreshXAxis<T>(this Series series, XamDataChart source) where T : CategoryAxisBase
        {
            string symbol = ((StockInfoViewModel)series.DataContext).Symbol;
            var at = (T)source.Axes
                    .Where(axes => axes.DataContext is StockInfoViewModel)
                    .Single(axis => ((StockInfoViewModel)axis.DataContext).Symbol == symbol);

            if (series is HorizontalAnchoredCategorySeries)
            {
                ((HorizontalAnchoredCategorySeries)series).XAxis = at;
            }
            else if (series is HorizontalRangeCategorySeries)
            {
                ((HorizontalRangeCategorySeries)series).XAxis = at;
            }
            else if (series is FinancialSeries)
            {
                ((FinancialSeries)series).XAxis = at;
            }
        }

        /// <summary>
        /// Refreshes the Y axis.
        /// </summary>
        /// <typeparam name="T">The type of the axis.</typeparam>
        /// <param name="series">The series.</param>
        /// <param name="source">The source.</param>
        internal static void RefreshYAxis<T>(this Series series, XamDataChart source) where T : NumericYAxis
        {
            BindingExpression b; 
            var yAxisName = string.Empty;

            if (series is HorizontalAnchoredCategorySeries)
            {
                b = series.GetBindingExpression(HorizontalAnchoredCategorySeries.YAxisProperty);
                if (b != null) yAxisName = b.ParentBinding.ElementName;

                var axis = (T)source.Axes.FirstOrDefault((a) => a != null && a.Name == yAxisName) ??
                           (T)source.Axes.FirstOrDefault((a) => a is T);

                ((HorizontalAnchoredCategorySeries)series).YAxis = axis;
            }
            else if (series is HorizontalRangeCategorySeries)
            {
                b = series.GetBindingExpression(HorizontalRangeCategorySeries.YAxisProperty);
                if (b != null) yAxisName = b.ParentBinding.ElementName;

                var axis = (T)source.Axes.FirstOrDefault((a) => a != null && a.Name == yAxisName) ??
                           (T)source.Axes.FirstOrDefault((a) => a is T);

                ((HorizontalRangeCategorySeries)series).YAxis = axis;
            }
            else if (series is FinancialSeries)
            {
                b = series.GetBindingExpression(FinancialSeries.YAxisProperty);
                if (b != null) yAxisName = b.ParentBinding.ElementName;

                var at = (T)source.Axes.FirstOrDefault((a) => a != null && a.Name == yAxisName) ??
                         (T)source.Axes.FirstOrDefault((a) => a is T);

                ((FinancialSeries)series).YAxis = at;
            }
        }
    }
}
