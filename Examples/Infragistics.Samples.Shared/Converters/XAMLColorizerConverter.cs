using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using Infragistics.Samples.Shared.Tools;

namespace Infragistics.Samples.Shared.Converters
{
    public class XAMLColorizerConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string xaml = value as string;
            if (String.IsNullOrEmpty(xaml))
            {
                return Binding.DoNothing;
            }
            else
            {
                string massagedXaml = this.PerformReplacements(xaml); //, localizationRoot);

                FlowDocument doc = new FlowDocument();
                ColorizeXAML(massagedXaml, doc);

                RichTextBox rtb = new RichTextBox();
                rtb.Document = doc;
                rtb.FontFamily = new FontFamily("Courier New");
                rtb.IsReadOnly = true;
                rtb.IsReadOnlyCaretVisible = true;
                rtb.BorderThickness = new System.Windows.Thickness(0);
                return rtb;
            }
        }

        string PerformReplacements(string xaml)//LocalizationRoot localizationRoot)
        {
            StringBuilder sb = new StringBuilder(xaml);

            // Tabs take up too much space, so replace them with two spaces.
            sb.Replace("\t", "  ");

            // Many of the XamRibbon samples have this line of XAML in them, which makes no sense after
            // stripping out the bindings that utilize it from the display text.
            sb.Replace("DataContext=\"{Binding Source={StaticResource spy}, Path=DataContext}\"", String.Empty);

            //Wrap the binding Endings      
            xaml = Regex.Replace(xaml, @"\} *""", @"}""" + Environment.NewLine);

            return sb.ToString();
        }

        #region SyntaxColoring
        #region ColorizeXAML
        public void ColorizeXAML(string xamlText, FlowDocument targetDoc)
        {
            XmlTokenizer tokenizer = new XmlTokenizer();
            XmlTokenizerMode mode = XmlTokenizerMode.OutsideElement;

            List<XmlToken> tokens = tokenizer.Tokenize(xamlText, ref mode);
            List<string> tokenTexts = new List<string>(tokens.Count);
            List<Color> colors = new List<Color>(tokens.Count);
            int position = 0;
            foreach (XmlToken token in tokens)
            {
                string tokenText = xamlText.Substring(position, token.Length);
                tokenTexts.Add(tokenText);
                Color color = ColorForToken(token, tokenText);
                colors.Add(color);
                position += token.Length;
            }

            // Clear paragraph
            //targetDoc.Blocks.Clear();
            Paragraph p = new Paragraph();
            //targetDoc.Blocks.Add(p);

            // Loop through tokens
            for (int i = 0; i < tokenTexts.Count; i++)
            {
                Run r = new Run(tokenTexts[i]);
                r.Foreground = new SolidColorBrush(colors[i]);
                p.Inlines.Add(r);
            }

            targetDoc.Blocks.Add(p);
        }
        #endregion ColorizeXAML

        static Color ColorForToken(XmlToken token, string tokenText)
        {
            Color color = Color.FromRgb(0, 0, 0);
            switch (token.Kind)
            {
                case XmlTokenKind.Open:
                case XmlTokenKind.OpenClose:
                case XmlTokenKind.Close:
                case XmlTokenKind.SelfClose:
                case XmlTokenKind.CDataBegin:
                case XmlTokenKind.CDataEnd:
                case XmlTokenKind.Equals:
                case XmlTokenKind.OpenProcessingInstruction:
                case XmlTokenKind.CloseProcessingInstruction:
                case XmlTokenKind.AttributeValue:
                    color = Color.FromRgb(0, 0, 255);
                    // color = "blue";
                    break;
                case XmlTokenKind.ElementName:
                    color = Color.FromRgb(163, 21, 21);
                    // color = "brown";
                    break;
                case XmlTokenKind.TextContent:
                    // color = "black";
                    break;
                case XmlTokenKind.AttributeName:
                case XmlTokenKind.Entity:
                    color = Color.FromRgb(255, 0, 0);
                    // color = "red";
                    break;
                case XmlTokenKind.CommentBegin:
                case XmlTokenKind.CommentEnd:
                case XmlTokenKind.CommentText:
                    color = Color.FromRgb(0, 128, 0);
                    // color = "green";
                    break;
            }
            if (token.Kind == XmlTokenKind.ElementWhitespace
                || (token.Kind == XmlTokenKind.TextContent && tokenText.Trim() == ""))
            {
                // color = null;
            }
            return color;
        }
        #endregion SyntaxColoring


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }
}
