using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using IGColorPicker.Resources;
using Infragistics.Controls.Editors;
using Infragistics.Samples.Framework;

namespace IGColorPicker.Samples.Editing
{
    public partial class Events : SampleContainer
    {
        public Events()
        {
            InitializeComponent();
        }

        private void MyColorPicker_DropDownClosed(object sender, EventArgs e)
        {
            txtBox.Text += PrintLog(ColorPickerStrings.CP_Events_DropDownClosed);
        }

        private void MyColorPicker_DropDownClosing(object sender, CancelEventArgs cancelEventArgs)
        {
            txtBox.Text += PrintLog(ColorPickerStrings.CP_Events_DropDownClosing);
        }

        private void MyColorPicker_DropDownOpening(object sender, CancelEventArgs cancelEventArgs)
        {
            txtBox.Text += PrintLog(ColorPickerStrings.CP_Events_DropDownOpening);
        }

        private void MyColorPicker_DropDownOpened(object sender, EventArgs e)
        {
            txtBox.Text += PrintLog(ColorPickerStrings.CP_Events_DropDownOpened);
        }     

        private void MyColorPicker_SelectedColorChanged(object sender, SelectedColorChangedEventArgs e)
        {
            txtBox.Text += PrintLog(ColorPickerStrings.CP_Events_SelectedColorChanged_Event);
            txtBox.Text += PrintLog(ColorPickerStrings.CP_Events_SelectedColorChanged_OldColor + e.OriginalColor);
            txtBox.Text += PrintLog(ColorPickerStrings.CP_Events_SelectedColorChanged_NewColor + e.NewColor);
        }

        private void BtnClearLog_Click(object sender, RoutedEventArgs e)
        {
            txtBox.Text = String.Empty;
        }

        private static string PrintLog(string msg)
        {
            string logMsg = "[" + DateTime.Now.ToString("hh:mm") + "] " + msg + "\n";

            return logMsg;
        }
    }

    public class ColorToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var brush = new SolidColorBrush();
            if (value is Color)
            {
                brush = new SolidColorBrush((Color)value);
            }

            return brush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var color = new Color();

            if (value is SolidColorBrush)
            {
                color = (value as SolidColorBrush).Color;
            }

            return color;
        }
    }
}
