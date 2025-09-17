using System.Windows;
using Infragistics.Samples.Framework;

namespace IGDataCards.Samples.Display
{
    /// <summary>
    /// Interaction logic for InterCardSpacing_Samp.xaml
    /// </summary>
    public partial class InterCardSpacing : SampleContainer
    {
        public InterCardSpacing()
        {
            InitializeComponent();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Thickness paddingThickness = this.xamDataCards1.ViewSettings.Padding;

            paddingThickness.Top = e.NewValue;

            this.xamDataCards1.ViewSettings.Padding = paddingThickness;
        }
    }
}