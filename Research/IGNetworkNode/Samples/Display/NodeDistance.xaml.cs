using System;
using System.Windows;
using System.Windows.Data;

namespace IGNetworkNode.Samples.Display
{
    public partial class NodeDistance : Infragistics.Samples.Framework.SampleContainer
    {
        public NodeDistance()
        {
            InitializeComponent();
        }

        private void Redraw_Button_Click(object sender, RoutedEventArgs e)
        {
            this.xnn.NodeDistance = this.nodeDistanceSlider.Value;
        }    
    }
    
    public class DoubleToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return System.Convert.ToInt32(value);
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
