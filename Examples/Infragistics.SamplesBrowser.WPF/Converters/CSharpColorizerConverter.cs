using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using Infragistics.SamplesBrowser.Converters.CSharpColorizer;

namespace Infragistics.SamplesBrowser.Converters
{
    public class CSharpColorizerConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            String val = (String)value;
            if (value == null || String.IsNullOrEmpty(value.ToString()))
            {
                return String.Empty;
            }
            else
            {
                CSharpFormat cSharpFormat = new CSharpFormat();
                RichTextBox rtb = new RichTextBox();
                FlowDocument doc = new FlowDocument();
                Paragraph p = new Paragraph();
                p = cSharpFormat.FormatCode(val);
                doc.Blocks.Add(p);
                rtb.FontFamily = new FontFamily("Courier New");
                rtb.Document = doc;
                rtb.IsReadOnly = true;
                rtb.IsReadOnlyCaretVisible = true;
                rtb.BorderThickness = new System.Windows.Thickness(0);
                return rtb;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }
}
