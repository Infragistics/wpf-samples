using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using IGDataChart.Resources;
using Infragistics;
using Infragistics.Controls.Charts;
using Infragistics.Math.Calculators;
using Infragistics.Samples.Shared.Models;

namespace IGDataChart.Samples.Display.Series
{
    public partial class SeriesErrorBars : Infragistics.Samples.Framework.SampleContainer
    {
        public SeriesErrorBars()
        {
            InitializeComponent();

            _samplesViewModel = new GallerySampleViewModel();
            _samplesViewModel.AddSample(this.LineChart, DataChartStrings.XWDC_CategorySeries_LineSeries);
            _samplesViewModel.AddSample(this.ScatterChart, DataChartStrings.XWDC_ScatterSeries_ScatterSeries);

            this.ScatterData = this.Resources["ScatterData"] as BubbleDataCollection;
            this.CategoryData = this.Resources["CategoryData"] as CategoryDataCollection;

            InitializeSample();

            this.Loaded += OnSampleLoaded;
        }


        private readonly GallerySampleViewModel _samplesViewModel;
        //protected bool IsSampleLoaded;
        protected BubbleDataCollection ScatterData;
        protected CategoryDataCollection CategoryData;

        #region Event Handlers
        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            this.SeriesTypeComboBox.SelectedIndex = 1;
        }
        private void OnNextItemButtonClick(object sender, RoutedEventArgs e)
        {
            this.SeriesTypeComboBox.SelectedIndex = _samplesViewModel.GetNextSampleIndex();
        }
        private void OnPrevItemButtonClick(object sender, RoutedEventArgs e)
        {
            this.SeriesTypeComboBox.SelectedIndex = _samplesViewModel.GetPreviousSampleIndex();
        }
        private void OnItemSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _samplesViewModel.ShowSample(this.SeriesTypeComboBox.SelectedIndex);

            XamDataChart chart = _samplesViewModel.GetSelectedSample() as XamDataChart;
            if (chart != null)
            {
                if (chart.Name.Equals("ScatterChart"))
                {
                    this.LineErrorBarsOptionsPanel.Visibility = Visibility.Collapsed;
                    this.ScatterErrorBarsOptionsPanel.Visibility = Visibility.Visible;
                    UpdateScatterSeries();
                }
                if (chart.Name.Equals("LineChart"))
                {
                    this.LineErrorBarsOptionsPanel.Visibility = Visibility.Visible;
                    this.ScatterErrorBarsOptionsPanel.Visibility = Visibility.Collapsed;
                    UpdateLineSeries();
                }
            }
        }

        private void OnScatterErrorBarsChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateScatterSeries();
        }
        private void OnScatterErrorBarsValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            UpdateScatterSeries();
        }

        private void OnLineErrorBarsChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateLineSeries();
        }
        private void OnLineErrorBarsValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            UpdateLineSeries();
        }

        #endregion
        #region Methods
        private void InitializeSample()
        {
            this.ScatterHorizontalErrorBarsCapLengthSliders.ValueChanged += OnScatterErrorBarsValueChanged;
            this.ScatterHorizontalErrorBarsThicknessSlider.ValueChanged += OnScatterErrorBarsValueChanged;
            this.ScatterHorizontalErrorBarsFixedValueCalcSlider.ValueChanged += OnScatterErrorBarsValueChanged;
            this.ScatterHorizontalErrorBarsPercentageCalcSlider.ValueChanged += OnScatterErrorBarsValueChanged;
            this.ScatterHorizontalErrorBarsDisplayType.SelectedIndex = 0;
            this.ScatterHorizontalErrorBarsDisplayType.SelectionChanged += OnScatterErrorBarsChanged;
            this.ScatterHorizontalErrorBarsBrush.SelectedIndex = 1;
            this.ScatterHorizontalErrorBarsBrush.SelectionChanged += OnScatterErrorBarsChanged;
            this.ScatterHorizontalErrorBarsCalcType.SelectedIndex = 1;
            this.ScatterHorizontalErrorBarsCalcType.SelectionChanged += OnScatterErrorBarsChanged;

            this.ScatterVerticalErrorBarsCapLengthSliders.ValueChanged += OnScatterErrorBarsValueChanged;
            this.ScatterVerticalErrorBarsThicknessSlider.ValueChanged += OnScatterErrorBarsValueChanged;
            this.ScatterVerticalErrorBarsFixedValueCalcSlider.ValueChanged += OnScatterErrorBarsValueChanged;
            this.ScatterVerticalErrorBarsPercentageCalcSlider.ValueChanged += OnScatterErrorBarsValueChanged;
            this.ScatterVerticalErrorBarsDisplayType.SelectedIndex = 0;
            this.ScatterVerticalErrorBarsDisplayType.SelectionChanged += OnScatterErrorBarsChanged;
            this.ScatterVerticalErrorBarsBrush.SelectedIndex = 4;
            this.ScatterVerticalErrorBarsBrush.SelectionChanged += OnScatterErrorBarsChanged;
            this.ScatterVerticalErrorBarsCalcType.SelectedIndex = 1;
            this.ScatterVerticalErrorBarsCalcType.SelectionChanged += OnScatterErrorBarsChanged;

            this.LineErrorBarsCapLengthSliders.ValueChanged += OnLineErrorBarsValueChanged;
            this.LineErrorBarsThicknessSlider.ValueChanged += OnLineErrorBarsValueChanged;
            this.LineErrorBarsFixedValueCalcSlider.ValueChanged += OnLineErrorBarsValueChanged;
            this.LineErrorBarsPercentageCalcSlider.ValueChanged += OnLineErrorBarsValueChanged;
            this.LineErrorBarsDisplayType.SelectedIndex = 0;
            this.LineErrorBarsDisplayType.SelectionChanged += OnLineErrorBarsChanged;
            this.LineErrorBarsBrush.SelectedIndex = 4;
            this.LineErrorBarsBrush.SelectionChanged += OnLineErrorBarsChanged;

            this.LineErrorBarsCalcType.SelectedIndex = 1;
            this.LineErrorBarsCalcType.SelectionChanged += OnLineErrorBarsChanged;

            this.PrevSeriesTypeButton.Click += OnPrevItemButtonClick;
            this.NextSeriesTypeButton.Click += OnNextItemButtonClick;

            this.SeriesTypeComboBox.ItemsSource = _samplesViewModel.SamplesNames;
            this.SeriesTypeComboBox.SelectionChanged += OnItemSelectionChanged;

        }
        /// <summary>
        /// Gets CategoryErrorBarSettings based on settings selected on UI 
        /// </summary>
        /// <returns></returns>
        private CategoryErrorBarSettings GetCategoryErrorBarSettings()
        {
            Brush barsStroke = ((BrushCollection)this.Resources["LineBrushes"])[this.LineErrorBarsBrush.SelectedIndex];

            EnableErrorBars barsDisplayType = (EnableErrorBars)this.LineErrorBarsDisplayType.SelectedItem;

            CategoryErrorBarSettings barsSettings = new CategoryErrorBarSettings();
            barsSettings.ErrorBarCapLength = (int)this.LineErrorBarsCapLengthSliders.Value;
            barsSettings.EnableErrorBars = barsDisplayType;
            barsSettings.StrokeThickness = this.LineErrorBarsThicknessSlider.Value;
            barsSettings.Stroke = barsStroke;

            ErrorBarCalculatorType calculatorType = (ErrorBarCalculatorType)this.LineErrorBarsCalcType.SelectedItem;

            if (calculatorType == ErrorBarCalculatorType.FixedValueCalculator)
            {
                this.LineErrorBarsFixedValueCalcPanel.Visibility = Visibility.Visible;
                this.LineErrorBarsPercentageCalcPanel.Visibility = Visibility.Collapsed;
                barsSettings.Calculator = new FixedValueCalculator { Value = this.LineErrorBarsFixedValueCalcSlider.Value };
            }
            else if (calculatorType == ErrorBarCalculatorType.PercentageCalculator)
            {
                this.LineErrorBarsFixedValueCalcPanel.Visibility = Visibility.Collapsed;
                this.LineErrorBarsPercentageCalcPanel.Visibility = Visibility.Visible;

                barsSettings.Calculator = new PercentageCalculator { Value = this.LineErrorBarsPercentageCalcSlider.Value };
            }
            else if (calculatorType == ErrorBarCalculatorType.StandardErrorCalculator)
            {
                this.LineErrorBarsFixedValueCalcPanel.Visibility = Visibility.Collapsed;
                this.LineErrorBarsPercentageCalcPanel.Visibility = Visibility.Collapsed;
                barsSettings.Calculator = new StandardErrorCalculator { ItemsSource = this.CategoryData, ValueMemberPath = "Value" };
            }
            else if (calculatorType == ErrorBarCalculatorType.StandardDeviationCalculator)
            {
                this.LineErrorBarsFixedValueCalcPanel.Visibility = Visibility.Collapsed;
                this.LineErrorBarsPercentageCalcPanel.Visibility = Visibility.Collapsed;
                barsSettings.Calculator = new StandardDeviationCalculator { ItemsSource = this.CategoryData, ValueMemberPath = "Value" };
            }
            else if (calculatorType == ErrorBarCalculatorType.DataCalculator)
            {
                this.LineErrorBarsFixedValueCalcPanel.Visibility = Visibility.Collapsed;
                this.LineErrorBarsPercentageCalcPanel.Visibility = Visibility.Collapsed;
                ErrorBarDataCollection errorBarsData = new ErrorBarDataSample(0.5);

                barsSettings.Calculator = new DataCalculator { ItemsSource = errorBarsData, ValueMemberPath = "Value" };
            }
            return barsSettings;
        }
        /// <summary>
        /// Gets ScatterErrorBarSettings based on settings selected on UI 
        /// </summary>
        /// <returns></returns>
        private ScatterErrorBarSettings GetScatterErrorBarSettings()
        {
            Brush horizontalErrorBarsStroke = ((BrushCollection)this.Resources["LineBrushes"])[this.ScatterHorizontalErrorBarsBrush.SelectedIndex];
            Brush verticalErrorBarsStroke = ((BrushCollection)this.Resources["LineBrushes"])[this.ScatterVerticalErrorBarsBrush.SelectedIndex];

            EnableErrorBars horizontalErrorBarsDisplayType = (EnableErrorBars)this.ScatterHorizontalErrorBarsDisplayType.SelectedItem;

            EnableErrorBars verticalErrorBarsDisplayType = (EnableErrorBars)this.ScatterVerticalErrorBarsDisplayType.SelectedItem;

            ScatterErrorBarSettings barsSettings = new ScatterErrorBarSettings();
            barsSettings.EnableErrorBarsHorizontal = horizontalErrorBarsDisplayType;
            barsSettings.HorizontalErrorBarCapLength = (int)this.ScatterHorizontalErrorBarsCapLengthSliders.Value;
            barsSettings.HorizontalStrokeThickness = this.ScatterHorizontalErrorBarsThicknessSlider.Value;
            barsSettings.HorizontalStroke = horizontalErrorBarsStroke;

            barsSettings.EnableErrorBarsVertical = verticalErrorBarsDisplayType;
            barsSettings.VerticalErrorBarCapLength = (int)this.ScatterVerticalErrorBarsCapLengthSliders.Value;
            barsSettings.VerticalStrokeThickness = this.ScatterVerticalErrorBarsThicknessSlider.Value;
            barsSettings.VerticalStroke = verticalErrorBarsStroke;

            #region update Horizontal Calculator settings
            ErrorBarCalculatorType calculatorType = (ErrorBarCalculatorType)this.ScatterHorizontalErrorBarsCalcType.SelectedItem;

            if (calculatorType == ErrorBarCalculatorType.FixedValueCalculator)
            {
                this.ScatterHorizontalErrorBarsFixedValueCalcPanel.Visibility = Visibility.Visible;
                this.ScatterHorizontalErrorBarsPercentageCalcPanel.Visibility = Visibility.Collapsed;
                barsSettings.HorizontalCalculator = new FixedValueCalculator { Value = this.ScatterHorizontalErrorBarsFixedValueCalcSlider.Value };
            }
            else if (calculatorType == ErrorBarCalculatorType.PercentageCalculator)
            {
                this.ScatterHorizontalErrorBarsFixedValueCalcPanel.Visibility = Visibility.Collapsed;
                this.ScatterHorizontalErrorBarsPercentageCalcPanel.Visibility = Visibility.Visible;
                barsSettings.HorizontalCalculatorReference = ErrorBarCalculatorReference.X;
                barsSettings.HorizontalCalculator = new PercentageCalculator { Value = this.ScatterHorizontalErrorBarsPercentageCalcSlider.Value };
            }
            else if (calculatorType == ErrorBarCalculatorType.StandardErrorCalculator)
            {
                this.ScatterHorizontalErrorBarsFixedValueCalcPanel.Visibility = Visibility.Collapsed;
                this.ScatterHorizontalErrorBarsPercentageCalcPanel.Visibility = Visibility.Collapsed;
                barsSettings.HorizontalCalculator = new StandardErrorCalculator { ItemsSource = this.ScatterData, ValueMemberPath = "X" };
            }
            else if (calculatorType == ErrorBarCalculatorType.StandardDeviationCalculator)
            {
                this.ScatterHorizontalErrorBarsFixedValueCalcPanel.Visibility = Visibility.Collapsed;
                this.ScatterHorizontalErrorBarsPercentageCalcPanel.Visibility = Visibility.Collapsed;
                barsSettings.HorizontalCalculator = new StandardDeviationCalculator { ItemsSource = this.ScatterData, ValueMemberPath = "X" };
            }
            else if (calculatorType == ErrorBarCalculatorType.DataCalculator)
            {
                this.ScatterHorizontalErrorBarsFixedValueCalcPanel.Visibility = Visibility.Collapsed;
                this.ScatterHorizontalErrorBarsPercentageCalcPanel.Visibility = Visibility.Collapsed;
                ErrorBarDataCollection errorBarsData = new ErrorBarDataSample(1.0);
                barsSettings.HorizontalCalculator = new DataCalculator { ItemsSource = errorBarsData, ValueMemberPath = "Value" };
            }
            #endregion

            #region Update Vertical Calculator settings
            calculatorType = (ErrorBarCalculatorType)this.ScatterVerticalErrorBarsCalcType.SelectedItem;

            if (calculatorType == ErrorBarCalculatorType.FixedValueCalculator)
            {
                this.ScatterVerticalErrorBarsFixedValueCalcPanel.Visibility = Visibility.Visible;
                this.ScatterVerticalErrorBarsPercentageCalcPanel.Visibility = Visibility.Collapsed;
                barsSettings.VerticalCalculator = new FixedValueCalculator { Value = this.ScatterVerticalErrorBarsFixedValueCalcSlider.Value };
            }
            else if (calculatorType == ErrorBarCalculatorType.PercentageCalculator)
            {
                this.ScatterVerticalErrorBarsFixedValueCalcPanel.Visibility = Visibility.Collapsed;
                this.ScatterVerticalErrorBarsPercentageCalcPanel.Visibility = Visibility.Visible;
                barsSettings.VerticalCalculatorReference = ErrorBarCalculatorReference.Y;
                barsSettings.VerticalCalculator = new PercentageCalculator { Value = this.ScatterVerticalErrorBarsPercentageCalcSlider.Value };
            }
            else if (calculatorType == ErrorBarCalculatorType.StandardErrorCalculator)
            {
                this.ScatterVerticalErrorBarsFixedValueCalcPanel.Visibility = Visibility.Collapsed;
                this.ScatterVerticalErrorBarsPercentageCalcPanel.Visibility = Visibility.Collapsed;
                barsSettings.VerticalCalculator = new StandardErrorCalculator { ItemsSource = this.ScatterData, ValueMemberPath = "Y" };
            }
            else if (calculatorType == ErrorBarCalculatorType.StandardDeviationCalculator)
            {
                this.ScatterVerticalErrorBarsFixedValueCalcPanel.Visibility = Visibility.Collapsed;
                this.ScatterVerticalErrorBarsPercentageCalcPanel.Visibility = Visibility.Collapsed;
                barsSettings.VerticalCalculator = new StandardDeviationCalculator { ItemsSource = this.ScatterData, ValueMemberPath = "Y" };
            }
            else if (calculatorType == ErrorBarCalculatorType.DataCalculator)
            {
                this.ScatterVerticalErrorBarsFixedValueCalcPanel.Visibility = Visibility.Collapsed;
                this.ScatterVerticalErrorBarsPercentageCalcPanel.Visibility = Visibility.Collapsed;
                ErrorBarDataCollection errorBarsData = new ErrorBarDataSample(1.0);
                barsSettings.VerticalCalculator = new DataCalculator { ItemsSource = errorBarsData, ValueMemberPath = "Value" };
            }
            #endregion
            return barsSettings;
        }
        /// <summary>
        /// Updates the scatter series with the new ScatterErrorBarSettings
        /// </summary>
        private void UpdateScatterSeries()
        {
            if (!IsSampleLoaded) return;
            Infragistics.Controls.Charts.ScatterSeries series = this.ScatterChart.Series["ScatterSeries"] as Infragistics.Controls.Charts.ScatterSeries;
            if (series != null)
            {
                ScatterErrorBarSettings barsSettings = this.GetScatterErrorBarSettings();
                series.ErrorBarSettings = barsSettings;
                series.Visibility = Visibility.Visible;
            }
        }
        /// <summary>
        /// Updates the line series with the new CategoryErrorBarSettings
        /// </summary>
        private void UpdateLineSeries()
        {
            if (!IsSampleLoaded) return;
            LineSeries series = this.LineChart.Series["LineSeries"] as LineSeries;
            if (series != null)
            {
                CategoryErrorBarSettings barsSettings = this.GetCategoryErrorBarSettings();
                series.ErrorBarSettings = barsSettings;
                series.Visibility = Visibility.Visible;
            }
        }

        #endregion

    }
    public enum ErrorBarCalculatorType
    {
        DataCalculator,
        FixedValueCalculator,
        PercentageCalculator,
        StandardDeviationCalculator,
        StandardErrorCalculator,
    }
    public class ErrorBarDataSample : ErrorBarDataCollection
    {
        public ErrorBarDataSample()
            : this(0.1)
        { }
        public ErrorBarDataSample(double variable)
        {
            int items = 100;
            double value = 0;
            for (int i = 0; i < items - 1; i++)
            {
                this.Add(new ErrorBarDataPoint { Value = value });
                if (i < (items / 2)) value += variable; else value -= variable;
            }
        }
    }

    public class ErrorBarDataCollection : ObservableCollection<ErrorBarDataPoint>
    { }
    public class ErrorBarDataPoint
    {
        public double Value { get; set; }
    }
}
