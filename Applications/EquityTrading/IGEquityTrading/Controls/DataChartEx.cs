//-------------------------------------------------------------------------
// <copyright file="DataChartEx.cs" company="Infragistics">
//
// Copyright (c) 2010 Infragistics, Inc.
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
// </copyright>
//-------------------------------------------------------------------------

using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using IGTrading.ViewModels;
using Infragistics.Controls.Charts;

namespace IGTrading.Controls
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
        public int SelectedChartType
        {
            get { return (int)GetValue(SelectedChartTypeProperty); }
            set { SetValue(SelectedChartTypeProperty, value); }
        }

        public static readonly DependencyProperty SelectedChartTypeProperty =
            DependencyProperty.Register(
                "SelectedChartType",
                typeof(int),
                typeof(DataChartEx),
                new PropertyMetadata(OnSelectedChartTypeChanged));

        /// <summary>
        /// Called when [selected chart type changed].
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnSelectedChartTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = (DataChartEx)d;
            source.OnSeriesSourceChanged(null, source.SeriesSource);
        }
        #endregion SelectedChartType (DependencyProperty)

        #region SeriesSource (DependencyProperty)
        /// <summary>
        /// Gets or sets the series source.
        /// </summary>
        /// <value>The series source.</value>
        public StockInfoViewModel SeriesSource
        {
            get { return (StockInfoViewModel)GetValue(SeriesSourceProperty); }
            set { SetValue(SeriesSourceProperty, value); }
        }

        public static readonly DependencyProperty SeriesSourceProperty =
            DependencyProperty.Register(
                "SeriesSource",
                typeof(StockInfoViewModel),
                typeof(DataChartEx),
                new PropertyMetadata(OnSeriesSourceChanged));

        /// <summary>
        /// Called when [series source changed].
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnSeriesSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var oldValue = (StockInfoViewModel)e.OldValue;
            var newValue = (StockInfoViewModel)e.NewValue;
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
        protected virtual void OnSeriesSourceChanged(StockInfoViewModel oldValue, StockInfoViewModel newValue)
        {
            this.Series.Clear();
            this.Axes.Clear();

            if (newValue != null)
            {
                DataTemplate axisCategoryXTemplate = GetCurentAxisCategoryXTemplate(null);
                DataTemplate axisCategoryYTemplate = GetCurentAxisCategoryYTemplate(null);
                DataTemplate axisFinancialIndicatorYTemplate = GetCurentAxisFinancialIndicatorYTemplate(null);

                // X Axis
                if (axisCategoryXTemplate != null)
                {
                    // load data template content
                    var axisCategoryX = axisCategoryXTemplate.LoadContent() as Axis;

                    if (axisCategoryX != null)
                    {
                        axisCategoryX.DataContext = SeriesSource;

                        this.Axes.Add(axisCategoryX);
                    }
                }

                // Y Axis
                if (axisCategoryYTemplate != null)
                {
                    // load data template content
                    var axisCategoryY = axisCategoryYTemplate.LoadContent() as Axis;

                    if (axisCategoryY != null)
                    {
                        axisCategoryY.DataContext = DataContext;

                        this.Axes.Add(axisCategoryY);
                    }
                }

                //Financial Indicator Y Axis
                if (axisFinancialIndicatorYTemplate != null)
                {
                    // load data template content
                    var axisFinancialIndicatorY = axisFinancialIndicatorYTemplate.LoadContent() as Axis;

                    if (axisFinancialIndicatorY != null)
                    {
                        axisFinancialIndicatorY.DataContext = DataContext;

                        this.Axes.Add(axisFinancialIndicatorY);
                    }
                }

                StockInfoViewModel item = newValue;

                DataTemplate dataTemplate = GetCurentSeriesTemplate(item);

                if (dataTemplate != null)
                {
                    // load data template content
                    var series = dataTemplate.LoadContent() as Series;
                    var financialIndicator = FinancialIndicatorSeriesTemplate.LoadContent() as Series;

                    if (series != null)
                    {
                        // set data context
                        series.DataContext = item;
                        financialIndicator.DataContext = item;

                        this.Series.Add(series);

                        series.RefreshXAxis<CategoryXAxis>(this);
                        series.RefreshYAxis<NumericYAxis>(this);

                        this.Series.Add(financialIndicator);

                        financialIndicator.RefreshXAxis<CategoryXAxis>(this);
                        financialIndicator.RefreshYAxis<NumericYAxis>(this);
                    }
                }
            }

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
        /// Gets the curent series template.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        private DataTemplate GetCurentSeriesTemplate(StockInfoViewModel item)
        {
            DataTemplate dataTemplate = null;

            // get data template
            if (this.SeriesTemplateSelector != null)
            {
                dataTemplate = this.SeriesTemplateSelector.SelectTemplate(item, this);
            }
            if (dataTemplate == null && this.SeriesTemplate != null)
            {
                dataTemplate = this.SeriesTemplate;
            }

            return dataTemplate;
        }

        /// <summary>
        /// Gets the curent axis category X template.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        private DataTemplate GetCurentAxisCategoryXTemplate(StockInfoViewModel item)
        {
            DataTemplate dataTemplate = null;

            if (AxisCategoryXTemplateSelector != null)
            {
                dataTemplate = AxisCategoryXTemplateSelector.SelectTemplate(DataContext, this);
            }
            if (dataTemplate == null && AxisCategoryXTemplate != null)
            {
                dataTemplate = AxisCategoryXTemplate;
            }

            return dataTemplate;
        }

        /// <summary>
        /// Gets the curent axis category Y template.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        private DataTemplate GetCurentAxisCategoryYTemplate(DependencyObject item)
        {
            DataTemplate dataTemplate = null;

            if (AxisCategoryYTemplateSelector != null)
            {
                dataTemplate = AxisCategoryYTemplateSelector.SelectTemplate(DataContext, this);
            }
            if (dataTemplate == null && AxisCategoryYTemplate != null)
            {
                dataTemplate = AxisCategoryYTemplate;
            }

            return dataTemplate;
        }

        /// <summary>
        /// Gets the curent financial indicator axis Y template.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        private DataTemplate GetCurentAxisFinancialIndicatorYTemplate(DependencyObject item)
        {
            DataTemplate dataTemplate = null;

            if (AxisFinancialIndicatorYTemplateSelector != null)
            {
                dataTemplate = AxisFinancialIndicatorYTemplateSelector.SelectTemplate(DataContext, this);
            }
            if (dataTemplate == null && AxisFinancialIndicatorYTemplate != null)
            {
                dataTemplate = AxisFinancialIndicatorYTemplate;
            }

            return dataTemplate;
        }
        #endregion

        #region SeriesTemplate (DependencyProperty)

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

        #region FinancialIndicatorSeriesTemplate (DependencyProperty)

        /// <summary>
        /// Gets or sets the series template.
        /// </summary>
        /// <value>The series template.</value>
        public DataTemplate FinancialIndicatorSeriesTemplate
        {
            get { return (DataTemplate)GetValue(FinancialIndicatorSeriesTemplateProperty); }
            set { SetValue(FinancialIndicatorSeriesTemplateProperty, value); }
        }

        public static readonly DependencyProperty FinancialIndicatorSeriesTemplateProperty =
            DependencyProperty.Register(
                "FinancialIndicatorTemplate",
                typeof(DataTemplate),
                typeof(DataChartEx),
                new PropertyMetadata(default(DataTemplate)));

        #endregion FinancialIndicatorSeriesTemplate (DependencyProperty)

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
            get { return (DataTemplate)GetValue(AxisFinancialIndicatorYTemplateProperty); }
            set { SetValue(AxisFinancialIndicatorYTemplateProperty, value); }
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

        #region AxisFinancialIndicatorYTemplate (DependencyProperty)

        /// <summary>
        /// Gets or sets the financial axis template.
        /// </summary>
        /// <value>The series template.</value>
        public DataTemplate AxisFinancialIndicatorYTemplate
        {
            get { return (DataTemplate)GetValue(AxisFinancialIndicatorYTemplateProperty); }
            set { SetValue(AxisFinancialIndicatorYTemplateProperty, value); }
        }

        public static readonly DependencyProperty AxisFinancialIndicatorYTemplateProperty =
            DependencyProperty.Register(
                "AxisFinancialIndicatorYTemplate",
                typeof(DataTemplate),
                typeof(DataChartEx),
                new PropertyMetadata(default(DataTemplate)));

        #endregion AxisFinancialIndicatorYTemplate (DependencyProperty)

        #region AxisFinancialIndicatorYTemplateSelector (DependencyProperty)

        /// <summary>
        /// Gets or sets the financial axis template selector.
        /// </summary>
        /// <value>The series template selector.</value>
        public DataTemplateSelector AxisFinancialIndicatorYTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(AxisFinancialIndicatorYTemplateSelectorProperty); }
            set { SetValue(AxisFinancialIndicatorYTemplateSelectorProperty, value); }
        }

        public static readonly DependencyProperty AxisFinancialIndicatorYTemplateSelectorProperty =
            DependencyProperty.Register(
                "AxisFinancialIndicatorYTemplateSelector",
                typeof(DataTemplateSelector),
                typeof(DataChartEx),
                new PropertyMetadata(default(DataTemplateSelector)));

        #endregion AxisFinancialIndicatorYTemplateSelector (DependencyProperty)

        #region RefreshData (Public Method)
        /// <summary>
        /// Refreshed the templates of the chart's series.
        /// </summary>
        public void RefreshData()
        {
            foreach (Series series in this.Series)
            {
                series.RefreshXAxis<CategoryXAxis>(this);
                series.RefreshYAxis<NumericYAxis>(this);
            }
        }
        #endregion
    }

    internal static class DataChartExtensions
    {
        /// <summary>
        /// Refreshes the X axis.
        /// </summary>
        /// <typeparam name="TAxisType">The type of the axis type.</typeparam>
        /// <param name="series">The series.</param>
        /// <param name="source">The source.</param>
        internal static void RefreshXAxis<TAxisType>(this Series series, XamDataChart source) where TAxisType : CategoryAxisBase
        {
            BindingExpression b = null;
            string xAxisName = string.Empty;

            if (series is HorizontalAnchoredCategorySeries)
            {
                b = series.GetBindingExpression(HorizontalAnchoredCategorySeries.XAxisProperty);
                if (b != null) xAxisName = b.ParentBinding.ElementName;

                var at = (TAxisType)source.Axes.FirstOrDefault((a) => a != null && a.Name == xAxisName);

                if (at == null)
                {
                    at = (TAxisType)source.Axes.FirstOrDefault((a) => a is TAxisType);
                }

                ((HorizontalAnchoredCategorySeries)series).XAxis = at;
            }
            else if (series is FinancialSeries)
            {
                b = series.GetBindingExpression(FinancialSeries.XAxisProperty);
                if (b != null) xAxisName = b.ParentBinding.ElementName;

                var at = (TAxisType)source.Axes.FirstOrDefault((a) => a != null && a.Name == xAxisName);

                if (at == null)
                {
                    at = (TAxisType)source.Axes.FirstOrDefault((a) => a is TAxisType);
                }

                ((FinancialSeries)series).XAxis = at;
            }
        }

        /// <summary>
        /// Refreshes the Y axis.
        /// </summary>
        /// <typeparam name="TAxisType">The type of the axis type.</typeparam>
        /// <param name="series">The series.</param>
        /// <param name="source">The source.</param>
        internal static void RefreshYAxis<TAxisType>(this Series series, XamDataChart source) where TAxisType : NumericYAxis
        {
            BindingExpression b = null;
            string yAxisName = string.Empty;

            if (series is HorizontalAnchoredCategorySeries)
            {
                b = series.GetBindingExpression(HorizontalAnchoredCategorySeries.YAxisProperty);
                if (b != null) yAxisName = b.ParentBinding.ElementName;

                var at = (TAxisType)source.Axes.FirstOrDefault((a) => a != null && a.Name == yAxisName);

                if (at == null)
                {
                    at = (TAxisType)source.Axes.FirstOrDefault((a) => a is TAxisType);
                }

                ((HorizontalAnchoredCategorySeries)series).YAxis = at;
            }
            else if (series is FinancialSeries)
            {
                b = series.GetBindingExpression(FinancialSeries.YAxisProperty);
                if (b != null) yAxisName = b.ParentBinding.ElementName;

                var at = (TAxisType)source.Axes.FirstOrDefault((a) => a != null && a.Name == yAxisName);

                if (at == null)
                {
                    at = (TAxisType)source.Axes.FirstOrDefault((a) => a is TAxisType);
                }

                ((FinancialSeries)series).YAxis = at;
            }
        }
    }
}
