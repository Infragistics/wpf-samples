using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Resources;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using System.Windows.Media;
using System.Windows;
using Infragistics.Controls.Gauges;

namespace IGBulletGraph
{
    public partial class AnimatedTransitions : Infragistics.Samples.Framework.SampleContainer
    {
        public AnimatedTransitions()
        {
            InitializeComponent();
            this.transitionDurationSlider.Value = 2;

            xamBulletGraph.TransitionDuration = (int)(this.transitionDurationSlider.Value * 1000);
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            xamBulletGraph.TransitionDuration = (int)(this.transitionDurationSlider.Value * 1000);

            xamBulletGraph.Value = 180;
            xamBulletGraph.TargetValue = 160;
            xamBulletGraph.Interval = 40;
            xamBulletGraph.MinimumValue = 0;
            xamBulletGraph.MaximumValue = 200;
            xamBulletGraph.ValueBrush = new SolidColorBrush(Colors.White); 

            xamBulletGraph.Ranges.Clear();
            xamBulletGraph.Ranges = new LinearGraphRangeCollection() {
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
            xamBulletGraph.TransitionDuration = (int)(this.transitionDurationSlider.Value * 1000);

            xamBulletGraph.Value = 80;
            xamBulletGraph.TargetValue = 70;
            xamBulletGraph.Interval = 50;
            xamBulletGraph.MinimumValue = 50;
            xamBulletGraph.MaximumValue = 100;
            xamBulletGraph.ValueBrush = this.Resources["settings2valueBrush"] as Brush;
            xamBulletGraph.TargetValueBrush = this.Resources["settings2valueBrush"] as Brush; 

            xamBulletGraph.Ranges.Clear();
            xamBulletGraph.Ranges = new LinearGraphRangeCollection() {
                new XamLinearGraphRange(){
                    Caption ="range1",
                    StartValue=0,
                    EndValue=60,
                    Brush= this.Resources["settings2brush1"] as Brush,
                    Outline= this.Resources["settings2brush1"] as Brush
                },
                new XamLinearGraphRange(){
                    Caption ="range2",
                    StartValue=60,
                    EndValue=100,
                    Brush= this.Resources["settings2brush2"] as Brush,
                    Outline= this.Resources["settings2brush2"] as Brush
                }
            };
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            xamBulletGraph.TransitionDuration = (int)(this.transitionDurationSlider.Value * 1000);

            xamBulletGraph.Value  = 800;
            xamBulletGraph.TargetValue = 700;
            xamBulletGraph.Interval = 200;
            xamBulletGraph.MinimumValue = 0;
            xamBulletGraph.MaximumValue = 1000;
            xamBulletGraph.ValueBrush = new SolidColorBrush(Colors.White);
            xamBulletGraph.TargetValueBrush = new SolidColorBrush(Colors.White); 

            xamBulletGraph.Ranges.Clear();
            xamBulletGraph.Ranges = new LinearGraphRangeCollection() 
            {
                new XamLinearGraphRange(){StartValue=0, EndValue=100 },
                new XamLinearGraphRange(){StartValue=100, EndValue=250 },
                new XamLinearGraphRange(){StartValue=250, EndValue=500 },
                new XamLinearGraphRange(){StartValue=500, EndValue=700 },
                new XamLinearGraphRange(){StartValue=700, EndValue=1000 },
            };
        }

        private void transitionDurationSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (transitionDurationSlider != null && labeltransitionDuration !=null)
            {
                this.xamBulletGraph.TransitionDuration = (int)(this.transitionDurationSlider.Value * 1000);
                this.labeltransitionDuration.Text = this.transitionDurationSlider.Value == 0 ? "0" : this.transitionDurationSlider.Value.ToString("#.##");
            }
        }
    }
}
