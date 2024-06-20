using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using Infragistics.Controls;
using Infragistics.Controls.Editors;
using Infragistics.Controls.Editors.Primitives;
using Infragistics.Samples.Framework;

namespace IGSlider.Samples.Style
{
    public partial class CustomDataSlider : Infragistics.Samples.Framework.SampleContainer
    {
        public CustomDataSlider()
        {
            InitializeComponent();
  
            
        }
    }

    public class ColorSlider : XamSimpleSliderBase<Color>
    {
        protected override Color DoubleToValue(double value)
        {
            int pixel = (int)value;

            //Shift int to RGBA values
            byte B = (byte)(pixel & 0xFF); pixel >>= 8;
            byte G = (byte)(pixel & 0xFF); pixel >>= 8;
            byte R = (byte)(pixel & 0xFF); pixel >>= 8;
            byte A = (byte)pixel; // alpha

            return Color.FromArgb(A, R, G, B);
        }

        protected override double ValueToDouble(Color value)
        {
            return value.A << 24 | value.R << 16 | value.G << 8 | value.B;
        }

        protected override object GetParameter(CommandSource source)
        {
            if (source.Command is XamSliderBaseCommandBase)
            {
                return this;
            }
            return null;
        }

        protected override bool SupportsCommand(ICommand command)
        {
            return command is XamSliderBaseCommandBase;
        }
    }

    public class BackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color color = (Color)value;
            return new SolidColorBrush(color);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SolidColorBrush brush = value as SolidColorBrush;
            if (brush != null)
            {
                return brush.Color;
            }
            return Colors.White;
        }
    }
}
