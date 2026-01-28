using Infragistics.Samples.Framework;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace IGSurfaceChart.Samples.EditingSelection
{
    /// <summary>
    /// Interaction logic for TooltipSample.xaml
    /// </summary>
    public partial class TooltipSample : SampleContainer
    {
        private DataTemplate _defaultToolTipTemplate;
        public TooltipSample()
        {
            InitializeComponent();

            this.SynchronizeScale(this.SurfaceChart); 

            _defaultToolTipTemplate = SurfaceChart.ToolTipTemplate;
        }

        private void Tooltips_CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            SurfaceChart.ToolTipTemplate = this.Resources["CustomToolTipTemplate"] as DataTemplate;
        }

        private void Tooltips_CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            SurfaceChart.ToolTipTemplate = _defaultToolTipTemplate;
        }
    }

    public class TooltipTextColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = new SolidColorBrush(Colors.Black);

            var z = double.Parse(value.ToString());

            if (z > 2.0)
            {
                result = new SolidColorBrush(Color.FromRgb(26, 106, 196));
            }
            else
            {
                result = new SolidColorBrush(Color.FromRgb(192, 40, 65));
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
