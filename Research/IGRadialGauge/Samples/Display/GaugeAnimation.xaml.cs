using System.Windows.Controls;
using Infragistics.Controls.Gauges;
using System.Windows;
using System.Windows.Media;

namespace IGRadialGauge.Samples.Display
{
    public partial class GaugeAnimation : Infragistics.Samples.Framework.SampleContainer
    {
        protected static SolidColorBrush Blue = new SolidColorBrush(Color.FromArgb(255, 22, 169, 231));
        protected static SolidColorBrush Gray = new SolidColorBrush(Color.FromArgb(100, 62, 62, 62));
        protected static SolidColorBrush LightGray = new SolidColorBrush(Color.FromArgb(255, 224, 224, 224));
        protected static SolidColorBrush Transparent = new SolidColorBrush(Colors.Transparent);
        protected static SolidColorBrush Green = new SolidColorBrush(Color.FromArgb(255, 164, 186, 41));
        protected static SolidColorBrush Yellow = new SolidColorBrush(Color.FromArgb(255, 253, 189, 72));
        protected static SolidColorBrush Red = new SolidColorBrush(Color.FromArgb(255, 211, 64, 75));
        protected SolidColorBrush RangeBrush = Green;
        protected SolidColorBrush RangeBrush2 = Red;
     
        public GaugeAnimation()
        {
            InitializeComponent();
            this.Loaded += GaugeAnimation_Loaded;
            ClearGaugeSettings();
            ApplyGaugeSettings2();
        }

        void GaugeAnimation_Loaded(object sender, RoutedEventArgs e)
        {
#if WPF
            this.radialGauge.SnapsToDevicePixels = false;
#endif
        }
        private void ClearGaugeSettings()
        {
            radialGauge.ClearValue(Infragistics.Controls.Gauges.XamRadialGauge.MinimumValueProperty);
            radialGauge.ClearValue(Infragistics.Controls.Gauges.XamRadialGauge.MaximumValueProperty);
            radialGauge.ClearValue(Infragistics.Controls.Gauges.XamRadialGauge.ValueProperty);
            radialGauge.ClearValue(Infragistics.Controls.Gauges.XamRadialGauge.ScaleStartAngleProperty);
            radialGauge.ClearValue(Infragistics.Controls.Gauges.XamRadialGauge.ScaleEndAngleProperty);
            radialGauge.ClearValue(Infragistics.Controls.Gauges.XamRadialGauge.ScaleBrushProperty);
            radialGauge.ClearValue(Infragistics.Controls.Gauges.XamRadialGauge.CenterYProperty);
            radialGauge.ClearValue(Infragistics.Controls.Gauges.XamRadialGauge.BackingShapeProperty);
            radialGauge.ClearValue(Infragistics.Controls.Gauges.XamRadialGauge.BackingOutlineProperty);
            radialGauge.ClearValue(Infragistics.Controls.Gauges.XamRadialGauge.BackingBrushProperty);
            radialGauge.ClearValue(Infragistics.Controls.Gauges.XamRadialGauge.RadiusMultiplierProperty);
         
            //Needle Settings
            radialGauge.ClearValue(Infragistics.Controls.Gauges.XamRadialGauge.NeedleBrushProperty);
            radialGauge.ClearValue(Infragistics.Controls.Gauges.XamRadialGauge.NeedleShapeProperty);
            radialGauge.ClearValue(Infragistics.Controls.Gauges.XamRadialGauge.NeedlePivotShapeProperty);
            radialGauge.ClearValue(Infragistics.Controls.Gauges.XamRadialGauge.NeedlePivotBrushProperty);
            radialGauge.ClearValue(Infragistics.Controls.Gauges.XamRadialGauge.NeedleEndExtentProperty);
            radialGauge.ClearValue(Infragistics.Controls.Gauges.XamRadialGauge.NeedlePointFeatureExtentProperty);
            radialGauge.ClearValue(Infragistics.Controls.Gauges.XamRadialGauge.NeedlePivotWidthRatioProperty);

            //TickMark Settings
            radialGauge.ClearValue(Infragistics.Controls.Gauges.XamRadialGauge.TickStartExtentProperty);
            radialGauge.ClearValue(Infragistics.Controls.Gauges.XamRadialGauge.TickStartExtentProperty);

            radialGauge.ClearValue(Infragistics.Controls.Gauges.XamRadialGauge.TickEndExtentProperty);
            radialGauge.ClearValue(Infragistics.Controls.Gauges.XamRadialGauge.TickEndExtentProperty);
            radialGauge.ClearValue(Infragistics.Controls.Gauges.XamRadialGauge.MinorTickStartExtentProperty);
            radialGauge.ClearValue(Infragistics.Controls.Gauges.XamRadialGauge.MinorTickEndExtentProperty);
            radialGauge.ClearValue(Infragistics.Controls.Gauges.XamRadialGauge.MinorTickBrushProperty);

            //Label Settings
            radialGauge.ClearValue(Infragistics.Controls.Gauges.XamRadialGauge.LabelIntervalProperty);
            radialGauge.ClearValue(Infragistics.Controls.Gauges.XamRadialGauge.LabelExtentProperty);

            radialGauge.Ranges.Clear();
        }
       
        private void ApplyGaugeSettings1()
        {
            radialGauge.TransitionDuration = ((int)this.TransitionDurationSlider.Value);
            radialGauge.MinimumValue = 0;
            radialGauge.MaximumValue = 10;
            radialGauge.Value = 8;
            radialGauge.ScaleStartAngle = 180;
            radialGauge.ScaleEndAngle = 0;
            radialGauge.BackingShape = RadialGaugeBackingShape.Fitted;
            radialGauge.BackingOutline = Blue;
            radialGauge.BackingBrush = Transparent;
            radialGauge.ScaleBrush = Transparent;
            //Needle Settings
            radialGauge.NeedleShape = RadialGaugeNeedleShape.Needle;
            radialGauge.NeedlePivotShape = RadialGaugePivotShape.CircleOverlay;
            radialGauge.NeedleEndExtent = 0.55;
            radialGauge.NeedlePointFeatureExtent = 0.3;
            radialGauge.NeedlePivotWidthRatio = 0.2;
            //TickMark Settings
            radialGauge.TickBrush = Gray;
            radialGauge.MinorTickBrush = Gray;
            //Label Settings
            radialGauge.LabelInterval = 1;
            //Range Settings
            var range1 = new XamRadialGaugeRange();
            range1.Brush = Green;
            range1.StartValue = 0;
            range1.EndValue = 3;
            range1.OuterStartExtent = 0.6;
            range1.OuterEndExtent = 0.66;
            radialGauge.Ranges.Add(range1);

            var range2 = new XamRadialGaugeRange();
            range2.Brush = Yellow;
            range2.StartValue = 3;
            range2.EndValue = 7;
            range2.OuterStartExtent = 0.66;
            range2.OuterEndExtent = 0.72;
            radialGauge.Ranges.Add(range2);

            var range3 = new XamRadialGaugeRange();
            range3.Brush = Red;
            range3.StartValue = 7;
            range3.EndValue = 10;
            range3.OuterStartExtent = 0.72;
            range3.OuterEndExtent = 0.78;
            radialGauge.Ranges.Add(range3);
            
        }
        private void ApplyGaugeSettings2()
        {
            this.radialGauge.TransitionDuration = ((int)this.TransitionDurationSlider.Value);

            radialGauge.NeedlePivotBrush = Transparent;
            radialGauge.NeedleBrush = Blue;
            radialGauge.NeedleEndExtent = 0.6;
            radialGauge.NeedleShape = RadialGaugeNeedleShape.TriangleWithBulb;
            radialGauge.NeedlePivotShape = RadialGaugePivotShape.CircleWithHole;
            radialGauge.BackingShape = RadialGaugeBackingShape.Fitted;
            radialGauge.BackingOutline = Gray;
            radialGauge.BackingBrush = Transparent;
            radialGauge.MinimumValue = 0;
            radialGauge.MaximumValue = 10;
            radialGauge.Value = 2.5;
            radialGauge.ScaleBrush = Transparent;
            //Label Settings
            radialGauge.LabelInterval = 1;
            radialGauge.LabelExtent = 0.65;
            //TickMark Settings
            radialGauge.TickBrush = Gray;
            radialGauge.TickStartExtent = 0.7;
            radialGauge.TickEndExtent = 0.78;
            radialGauge.MinorTickBrush = Gray;
            radialGauge.MinorTickStartExtent = 0.78;
            radialGauge.MinorTickEndExtent = 0.75;

            //Range Settings
            var range1 = new XamRadialGaugeRange();
            range1.Brush = Green;
            range1.StartValue = 0;
            range1.EndValue = 3;
            range1.OuterStartExtent = 0.55;
            range1.OuterEndExtent = 0.5;
            radialGauge.Ranges.Add(range1);

            var range2 = new XamRadialGaugeRange();
            range2.Brush = Yellow;
            range2.StartValue = 3;
            range2.EndValue = 7;
            range2.OuterStartExtent = 0.5;
            range2.OuterEndExtent = 0.4;
            radialGauge.Ranges.Add(range2);

            var range3 = new XamRadialGaugeRange();
            range3.Brush = Red;
            range3.StartValue = 7;
            range3.EndValue = 10;
            range3.OuterStartExtent = 0.4;
            range3.OuterEndExtent = 0.3;
            radialGauge.Ranges.Add(range3);
        }
        private void ApplyGaugeSettings3()
        {
            radialGauge.TransitionDuration = ((int)this.TransitionDurationSlider.Value);

            radialGauge.MinimumValue = 0;
            radialGauge.MaximumValue = 5;
            radialGauge.Value = 2.5;
            radialGauge.ScaleStartAngle = 200;
            radialGauge.ScaleEndAngle = -20;
            radialGauge.ScaleBrush = Transparent;
            radialGauge.BackingOutline = Transparent;
            radialGauge.BackingBrush = Transparent;

            //Needle Settings
            radialGauge.NeedleEndExtent = 0.95;
            radialGauge.NeedleShape = RadialGaugeNeedleShape.Triangle;
            radialGauge.NeedlePivotShape = RadialGaugePivotShape.Circle;
            radialGauge.NeedlePivotWidthRatio = 0.1;

            //TickMark Settings
            radialGauge.TickBrush = Transparent;
            radialGauge.MinorTickBrush = Transparent;

            //Label Settings
            radialGauge.LabelInterval = 2.5;
            radialGauge.LabelExtent = 1;

            //Range Settings   
            var range = new XamRadialGaugeRange();
            range.Brush = RangeBrush;
            range.StartValue = 0;
            range.EndValue = 2.5;
            range.OuterStartExtent = 0.95;
            range.OuterEndExtent = 0.95;
            range.InnerStartExtent = 0.3;
            range.InnerEndExtent = 0.3;
            radialGauge.Ranges.Add(range);

            var range2 = new XamRadialGaugeRange();
            range2.Brush = RangeBrush2;
            range2.StartValue = 2.5;
            range2.EndValue = 5;
            range2.OuterStartExtent = 0.95;
            range2.OuterEndExtent = 0.95;
            range2.InnerStartExtent = 0.3;
            range2.InnerEndExtent = 0.3;
            radialGauge.Ranges.Add(range2);
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            ClearGaugeSettings();
            ApplyGaugeSettings2();
        }
        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            ClearGaugeSettings();
            ApplyGaugeSettings1();
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            ClearGaugeSettings();

            ApplyGaugeSettings3();
        }

        private void transitionDurationSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (TransitionDurationSlider != null)
            {
                this.radialGauge.TransitionDuration = ((int)this.TransitionDurationSlider.Value);
            }
        }

    }
}
