using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Resources;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using System.Windows.Media;
using System.Windows;
using Infragistics.Controls.Gauges;
using IGLinearGauge.Resources;

namespace IGLinearGauge
{
    using Infragistics.Controls.Charts;

    public partial class AnimatedTransitions : Infragistics.Samples.Framework.SampleContainer
    {
        public AnimatedTransitions()
        {
            InitializeComponent();
            this.transitionDurationSlider.Value = 2;

            xamLinearGauge.TransitionDuration = (int)(this.transitionDurationSlider.Value * 1000);
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            xamLinearGauge.TransitionDuration = (int)(this.transitionDurationSlider.Value * 1000);

            xamLinearGauge.Title = "";
            xamLinearGauge.Value = 180;
            xamLinearGauge.Interval = 40;
            xamLinearGauge.MinimumValue = 0;
            xamLinearGauge.MaximumValue = 200;
            xamLinearGauge.NeedleBrush = this.Resources["LinearNeedleFill"] as Brush;
            xamLinearGauge.FontBrush = this.Resources["LabelForeground"] as Brush;
            xamLinearGauge.TickBrush = this.Resources["MinorTickMarkFill"] as Brush;
            xamLinearGauge.MinorTickBrush = this.Resources["MajorTickMarkFill"] as Brush;
            xamLinearGauge.BackingBrush = this.Resources["LinearDialBackground"] as Brush; 

            xamLinearGauge.RangeInnerExtent = 0.05;
            xamLinearGauge.RangeOuterExtent = 0.65;
            xamLinearGauge.MinorTickCount = 4; 
            xamLinearGauge.BackingStrokeThickness = 0;
            xamLinearGauge.BackingInnerExtent = 0;
            xamLinearGauge.BackingOuterExtent = 1;
            xamLinearGauge.TickStartExtent = 0.05;
            xamLinearGauge.TickEndExtent = 0.65;
            xamLinearGauge.LabelExtent = 0;

            xamLinearGauge.Ranges.Clear();
            xamLinearGauge.Ranges = new LinearGraphRangeCollection() {
                new XamLinearGraphRange(){
                    Caption ="range1",
                    StartValue=0,
                    EndValue=50,
                    Brush= this.Resources["settings1brush1"] as Brush,
                    Outline= this.Resources["settings1brush1"] as Brush
                },
                new XamLinearGraphRange(){
                    Caption ="range2",
                    StartValue=50,
                    EndValue=150,
                    Brush= this.Resources["settings1brush2"] as Brush,
                    Outline= this.Resources["settings1brush2"] as Brush
                },
                new XamLinearGraphRange(){
                    Caption ="range3",
                    StartValue=150,
                    EndValue=200,
                    Brush= this.Resources["settings1brush3"] as Brush,
                    Outline= this.Resources["settings1brush3"] as Brush
                }
            };
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            xamLinearGauge.TransitionDuration = (int)(this.transitionDurationSlider.Value * 1000);

            xamLinearGauge.Title = "";
            xamLinearGauge.Value = 80;
            xamLinearGauge.Interval = 50;
            xamLinearGauge.MinimumValue = 0;
            xamLinearGauge.MaximumValue = 100;
            xamLinearGauge.NeedleBrush = this.Resources["settings2valueBrush"] as Brush;
            xamLinearGauge.NeedleOutline = this.Resources["settings2brush1"] as Brush;
            xamLinearGauge.TickBrush = this.Resources["MinorTickMarkFill"] as Brush;
            xamLinearGauge.BackingBrush = null;

            xamLinearGauge.RangeInnerExtent = 0.05;
            xamLinearGauge.RangeOuterExtent = 0.65;
            xamLinearGauge.MinorTickCount = 4;
            xamLinearGauge.BackingStrokeThickness = 0;
            xamLinearGauge.FontBrush = null;
            xamLinearGauge.BackingInnerExtent = 0;
            xamLinearGauge.BackingOuterExtent = 1;
            xamLinearGauge.TickStartExtent = 0.05;
            xamLinearGauge.TickEndExtent = 0.65;
            xamLinearGauge.LabelExtent = 0;

            xamLinearGauge.Ranges.Clear();
            xamLinearGauge.Ranges = new LinearGraphRangeCollection() {
                new XamLinearGraphRange(){
                    Caption ="range1",
                    StartValue=0,
                    EndValue=50,
                    Brush= this.Resources["settings2brush1"] as Brush,
                    Outline= this.Resources["settings2brush1"] as Brush
                },
                new XamLinearGraphRange(){
                    Caption ="range2",
                    StartValue=50,
                    EndValue=80,
                    Brush= this.Resources["settings2brush2"] as Brush,
                    Outline= this.Resources["settings2brush2"] as Brush
                },
                  new XamLinearGraphRange(){
                    Caption ="range3",
                    StartValue=80,
                    EndValue=100,
                    Brush= this.Resources["settings2brush3"] as Brush,
                    Outline= this.Resources["settings2brush3"] as Brush
                }
            };
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            xamLinearGauge.TransitionDuration = (int)(this.transitionDurationSlider.Value * 1000);

            xamLinearGauge.Value = 4.5;
            xamLinearGauge.Interval = double.NaN;
            xamLinearGauge.MinimumValue = 0;
            xamLinearGauge.MaximumValue = 10;
            xamLinearGauge.NeedleBrush = new SolidColorBrush(Colors.White);
            xamLinearGauge.TickBrush = new SolidColorBrush(Colors.White);
            xamLinearGauge.BackingBrush = new SolidColorBrush(Colors.Black);
              
            xamLinearGauge.RangeInnerExtent = -0.1;
            xamLinearGauge.RangeOuterExtent = 0.2;
            xamLinearGauge.MinorTickCount = 0;
            xamLinearGauge.FontBrush = new SolidColorBrush(Colors.White);
            xamLinearGauge.BackingOuterExtent = 1;
            xamLinearGauge.BackingStrokeThickness = 0;
            xamLinearGauge.TickStartExtent = 0.7;
            xamLinearGauge.TickEndExtent = 0.8;
            xamLinearGauge.LabelExtent = 0.5;

            xamLinearGauge.Ranges.Clear();
            xamLinearGauge.Ranges = new LinearGraphRangeCollection() 
            {
                new XamLinearGraphRange()
                {
                    StartValue=0, 
                    EndValue=4,                     
                    Brush= this.Resources["settings1brush3"] as Brush,
                    Outline= this.Resources["settings1brush3"] as Brush},
                new XamLinearGraphRange()
                {
                    StartValue=4, 
                    EndValue=7, 
                    Brush= this.Resources["settings1brush2"] as Brush,
                    Outline= this.Resources["settings1brush2"] as Brush
                },
                new XamLinearGraphRange()
                {
                    StartValue=7, 
                    EndValue=10, 
                    Brush= this.Resources["settings1brush1"] as Brush,
                    Outline= this.Resources["settings1brush1"] as Brush
                }
            };

        }

        private void transitionDurationSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (transitionDurationSlider != null && labeltransitionDuration !=null)
            {
                this.xamLinearGauge.TransitionDuration = (int)(this.transitionDurationSlider.Value * 1000);
                this.labeltransitionDuration.Text = this.transitionDurationSlider.Value == 0 ? "0" : this.transitionDurationSlider.Value.ToString("#.##");
            }
        }
    }
}
