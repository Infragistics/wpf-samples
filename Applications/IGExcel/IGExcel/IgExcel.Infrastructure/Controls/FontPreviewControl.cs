using Infragistics.Controls.Editors;
using Infragistics.Documents.RichText;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace IgExcel.Infrastructure.Controls
{
    public class FontPreviewControl : XamRichTextEditor
    {
        private TranslateTransform translateTransform;
        private bool executeOnPropertyChanged;

        #region Contrsuctor

        public FontPreviewControl()
        {
            this.BorderThickness = new Thickness();
            this.translateTransform = new TranslateTransform(0, 15);
            this.RenderTransform = translateTransform;

            this.executeOnPropertyChanged = true;
        }

        #endregion //Contrsuctor

        #region FontStyle

        public static readonly DependencyProperty FontStyleCustomProperty = DependencyProperty.Register(FontStyleCustomPropertyName, typeof(FontStylesCustom), typeof(FontPreviewControl), new PropertyMetadata(new PropertyChangedCallback((o, e) =>
        {
            ((FontPreviewControl)o).OnPropertyChanged(FontStyleCustomPropertyName, e.OldValue, e.NewValue);
        })));


        internal const string FontStyleCustomPropertyName = "FontStyleCustom";

        public FontStylesCustom FontStyleCustom
        {
            get { return (FontStylesCustom)this.GetValue(FontStyleCustomProperty); }
            set { this.SetValue(FontStyleCustomProperty, value); }
        }

        #endregion // FontStyle

        #region PreviewText

        public static readonly DependencyProperty PreviewTextProperty = DependencyProperty.Register(PreviewTextPropertyName, typeof(string), typeof(FontPreviewControl), new PropertyMetadata(new PropertyChangedCallback((o, e) =>
        {
            ((FontPreviewControl)o).OnPropertyChanged(PreviewTextPropertyName, e.OldValue, e.NewValue);
        })));


        internal const string PreviewTextPropertyName = "PreviewText";

        public string PreviewText
        {
            get { return (string)this.GetValue(PreviewTextProperty); }
            set { this.SetValue(PreviewTextProperty, value); }
        }

        #endregion // FontStyle

        #region PreviewFontFamily

        public static readonly DependencyProperty PreviewFontFamilyProperty = DependencyProperty.Register(PreviewFontFamilyPropertyName, typeof(string), typeof(FontPreviewControl), new PropertyMetadata(new PropertyChangedCallback((o, e) =>
        {
            ((FontPreviewControl)o).OnPropertyChanged(PreviewFontFamilyPropertyName, e.OldValue, e.NewValue);
        })));


        internal const string PreviewFontFamilyPropertyName = "PreviewFontFamily";

        public string PreviewFontFamily
        {
            get { return (string)this.GetValue(FontStyleCustomProperty); }
            set { this.SetValue(FontStyleCustomProperty, value); }
        }

        #endregion // PreviewFontFamily

        #region PreviewFontSize

        public static readonly DependencyProperty PreviewFontSizeProperty = DependencyProperty.Register(PreviewFontSizePropertyName, typeof(double), typeof(FontPreviewControl), new PropertyMetadata(new PropertyChangedCallback((o, e) =>
        {
            ((FontPreviewControl)o).OnPropertyChanged(PreviewFontSizePropertyName, e.OldValue, e.NewValue);
        })));


        internal const string PreviewFontSizePropertyName = "PreviewFontSize";

        public double PreviewFontSize
        {
            get { return (double)this.GetValue(PreviewFontSizeProperty); }
            set { this.SetValue(PreviewFontSizeProperty, value); }
        }

        #endregion // PreviewFontSize

        #region PreviewFontColor

        public static readonly DependencyProperty PreviewFontColorProperty = DependencyProperty.Register(PreviewFontColorPropertyName, typeof(Color), typeof(FontPreviewControl), new PropertyMetadata(new PropertyChangedCallback((o, e) =>
        {
            ((FontPreviewControl)o).OnPropertyChanged(PreviewFontColorPropertyName, e.OldValue, e.NewValue);
        })));


        internal const string PreviewFontColorPropertyName = "PreviewFontColor";

        public Color PreviewFontColor
        {
            get { return (Color)this.GetValue(PreviewFontColorProperty); }
            set { this.SetValue(PreviewFontColorProperty, value); }
        }

        #endregion // PreviewFontColor

        #region PreviewStrikethrough

        public static readonly DependencyProperty PreviewStrikethroughProperty = DependencyProperty.Register(PreviewStrikethroughPropertyName, typeof(bool), typeof(FontPreviewControl), new PropertyMetadata(new PropertyChangedCallback((o, e) =>
        {
            ((FontPreviewControl)o).OnPropertyChanged(PreviewStrikethroughPropertyName, e.OldValue, e.NewValue);
        })));


        internal const string PreviewStrikethroughPropertyName = "PreviewStrikethrough";

        public bool PreviewStrikethrough
        {
            get { return (bool)this.GetValue(PreviewStrikethroughProperty); }
            set { this.SetValue(PreviewStrikethroughProperty, value); }
        }

        #endregion // PreviewFontColor

        #region PreviewSubscript

        public static readonly DependencyProperty PreviewSubscriptProperty = DependencyProperty.Register(PreviewSubscriptPropertyName, typeof(bool), typeof(FontPreviewControl), new PropertyMetadata(new PropertyChangedCallback((o, e) =>
        {
            ((FontPreviewControl)o).OnPropertyChanged(PreviewSubscriptPropertyName, e.OldValue, e.NewValue);
        })));


        internal const string PreviewSubscriptPropertyName = "PreviewSubscript";

        public bool PreviewSubscript
        {
            get { return (bool)this.GetValue(PreviewSubscriptProperty); }
            set { this.SetValue(PreviewSubscriptProperty, value); }
        }

        #endregion // PreviewSubscript

        #region PreviewSuperscript

        public static readonly DependencyProperty PreviewSuperscriptProperty = DependencyProperty.Register(PreviewSuperscriptPropertyName, typeof(bool), typeof(FontPreviewControl), new PropertyMetadata(new PropertyChangedCallback((o, e) =>
        {
            ((FontPreviewControl)o).OnPropertyChanged(PreviewSuperscriptPropertyName, e.OldValue, e.NewValue);
        })));


        internal const string PreviewSuperscriptPropertyName = "PreviewSuperscript";

        public bool PreviewSuperscript
        {
            get { return (bool)this.GetValue(PreviewSuperscriptProperty); }
            set { this.SetValue(PreviewSuperscriptProperty, value); }
        }

        #endregion // PreviewSuperscript

        #region OnPropertyChanged

        private void OnPropertyChanged(string propertyName, object oldValue, object newValue)
        {
            if (!executeOnPropertyChanged)
                return;

            var charSettings = this.Document.GetCommonCharacterSettings(DocumentSpan.All);
            string error;

            switch (propertyName)
            {
                case FontStyleCustomPropertyName:
                    var fontStyle = (FontStylesCustom)newValue;
                    switch (fontStyle)
                    {
                        case FontStylesCustom.Bold:
                            charSettings.Bold = true;
                            charSettings.Italics = false;
                            break;
                        case FontStylesCustom.Regular:
                            charSettings.Bold = false;
                            charSettings.Italics = false;
                            break;
                        case FontStylesCustom.Italic:
                            charSettings.Bold = false;
                            charSettings.Italics = true;
                            break;
                        case FontStylesCustom.BoldItalic:
                            charSettings.Bold = true;
                            charSettings.Italics = true;
                            break;
                    }
                    break;
                case PreviewTextPropertyName:
                    this.Document.GetParagraphs(DocumentSpan.All).ElementAt(0).SetText(newValue.ToString());
                    break;

                case PreviewFontFamilyPropertyName:
                    if (newValue == null)
                    {
                        this.Visibility = System.Windows.Visibility.Hidden;
                    }
                    else
                    {
                        charSettings.FontSettings.Ascii = new RichTextFont(newValue.ToString());
                        this.Visibility = System.Windows.Visibility.Visible;
                    }
                    break;

                case PreviewFontSizePropertyName:
                    var fontSize = (double)newValue;
                    var oldSize = charSettings.FontSize.Value.LogicalPixels;
                    charSettings.FontSize = new Extent(fontSize, ExtentUnitType.Points);
                    var newSize = charSettings.FontSize.Value.LogicalPixels;
                    translateTransform.Y += oldSize - newSize;
                    translateTransform.X += (oldSize - newSize) * 2.5;
                    this.Width += (newSize - oldSize) * 4.5;
                    break;

                case PreviewFontColorPropertyName:
                    var fontColor = (Color)newValue;
                    if (fontColor == Colors.Transparent || fontColor.A < 1)
                        charSettings.Color = ColorInfo.Automatic;
                    else
                        charSettings.Color = new ColorInfo(fontColor);
                    break;
                case PreviewStrikethroughPropertyName:
                    charSettings.StrikeThrough = (bool)newValue;
                    break;
                case PreviewSuperscriptPropertyName:
                    var superscript = (bool)newValue;

                    if (superscript && PreviewSubscript)
                    {
                        this.executeOnPropertyChanged = false;
                        SetCurrentValue(FontPreviewControl.PreviewSubscriptProperty, false);
                        this.executeOnPropertyChanged = true;
                    }

                    charSettings.VerticalAlignment = superscript ? RunVerticalAlignment.Superscript : RunVerticalAlignment.Baseline;
                    break;
                case PreviewSubscriptPropertyName:
                    var subscript = (bool)newValue;
                  
                    if (subscript && PreviewSuperscript)
                    {
                        this.executeOnPropertyChanged = false;
                        SetCurrentValue(FontPreviewControl.PreviewSuperscriptProperty, false);
                        this.executeOnPropertyChanged = true;
                    }

                    charSettings.VerticalAlignment = subscript ? RunVerticalAlignment.Subscript : RunVerticalAlignment.Baseline;
                    break;
            }

            this.Document.ApplyCharacterSettings(DocumentSpan.All, charSettings, out error);
        }

        #endregion //OnPropertyChanged
    }
}
