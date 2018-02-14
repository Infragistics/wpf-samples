using Infragistics.Controls.Editors;
using Infragistics.Documents.RichText;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;

namespace IgWord.Infrastructure.Adapters
{
  

    public class CharacterSettingsPreviewAdapter : FrameworkElement, INotifyPropertyChanged
    {
        #region Member Variables

        private CharacterSettings previewCharacterSettings;
        private const double deafultFontSize = 11;

        #endregion //Member Variables

        #region Constructor

        public CharacterSettingsPreviewAdapter()
        {

        }

        #endregion //Constructor

        #region Editor

        public static readonly DependencyProperty EditorProperty = DependencyProperty.Register(EditorPropertyName,
            typeof(XamRichTextEditor), typeof(CharacterSettingsPreviewAdapter),
            new PropertyMetadata(null,
                (o, e) =>
                {
                    ((CharacterSettingsPreviewAdapter)o).OnPropertyChanged(EditorPropertyName, e.OldValue, e.NewValue);
                }));

        internal const string EditorPropertyName = "Editor";

        public XamRichTextEditor Editor
        {
            get
            {
                return (XamRichTextEditor)this.GetValue(CharacterSettingsPreviewAdapter.EditorProperty);
            }
            set
            {
                this.SetValue(CharacterSettingsPreviewAdapter.EditorProperty, value);
            }
        }

        #endregion //Editor

        #region ComplexScriptEditor

        public static readonly DependencyProperty ComplexScriptEditorProperty = DependencyProperty.Register(ComplexScriptEditorPropertyName,
            typeof(XamRichTextEditor), typeof(CharacterSettingsPreviewAdapter),
            new PropertyMetadata(null,
                (o, e) =>
                {
                    ((CharacterSettingsPreviewAdapter)o).OnPropertyChanged(ComplexScriptEditorPropertyName, e.OldValue, e.NewValue);
                }));

        internal const string ComplexScriptEditorPropertyName = "ComplexScriptEditor";

        public XamRichTextEditor ComplexScriptEditor
        {
            get
            {
                return (XamRichTextEditor)this.GetValue(CharacterSettingsPreviewAdapter.ComplexScriptEditorProperty);
            }
            set
            {
                this.SetValue(CharacterSettingsPreviewAdapter.ComplexScriptEditorProperty, value);
            }
        }

        #endregion //ComplexScriptEditor

        #region InputCharacterSettings

        public static readonly DependencyProperty InputCharacterSettingsProperty = DependencyProperty.Register(InputCharacterSettingsPropertyName,
            typeof(CharacterSettings), typeof(CharacterSettingsPreviewAdapter),
            new PropertyMetadata(null,
                (o, e) =>
                {
                    ((CharacterSettingsPreviewAdapter)o).OnPropertyChanged(InputCharacterSettingsPropertyName, e.OldValue, e.NewValue);
                }));

        internal const string InputCharacterSettingsPropertyName = "InputCharacterSettings";

        public CharacterSettings InputCharacterSettings
        {
            get
            {
                return (CharacterSettings)this.GetValue(CharacterSettingsPreviewAdapter.InputCharacterSettingsProperty);
            }
            set
            {
                this.SetValue(CharacterSettingsPreviewAdapter.InputCharacterSettingsProperty, value);
            }
        }

        #endregion //InputCharacterSettings

        #region CharacterSettingsPreviewAdapter_PropertyChanged

        void CharacterSettingsPreviewAdapter_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "SmallCaps":
                    previewCharacterSettings.SmallCaps = smallCaps;
                    if (allCaps == true)
                    {
                        PropertyChanged -= CharacterSettingsPreviewAdapter_PropertyChanged;
                        AllCaps = false;
                        PropertyChanged += CharacterSettingsPreviewAdapter_PropertyChanged;
                        previewCharacterSettings.AllCaps = false;
                    }
                    break;
                case "AllCaps": previewCharacterSettings.AllCaps = allCaps;
                    if (smallCaps == true)
                    {
                        PropertyChanged -= CharacterSettingsPreviewAdapter_PropertyChanged;
                        SmallCaps = false;
                        PropertyChanged += CharacterSettingsPreviewAdapter_PropertyChanged;
                        previewCharacterSettings.SmallCaps = false;
                    }
                    break;
                case "FontStyle":
                    switch (fontStyle)
                    {
                        case FontStyle.Bold:
                            previewCharacterSettings.Bold = true;
                            previewCharacterSettings.Italics = false;
                            break;
                        case FontStyle.Regular:
                            previewCharacterSettings.Bold = false;
                            previewCharacterSettings.Italics = false;
                            break;
                        case FontStyle.Italic:
                            previewCharacterSettings.Bold = false;
                            previewCharacterSettings.Italics = true;
                            break;
                        case FontStyle.BoldItalic:
                            previewCharacterSettings.Bold = true;
                            previewCharacterSettings.Italics = true;
                            break;
                    }
                    break;
                case "ComplexScriptFontStyle":
                    switch (complexScriptFontStyle)
                    {
                        case FontStyle.Bold:
                            previewCharacterSettings.ComplexScriptBold = true;
                            previewCharacterSettings.ComplexScriptItalics = false;
                            break;
                        case FontStyle.Regular:
                            previewCharacterSettings.ComplexScriptBold = false;
                            previewCharacterSettings.ComplexScriptItalics = false;
                            break;
                        case FontStyle.Italic:
                            previewCharacterSettings.ComplexScriptBold = false;
                            previewCharacterSettings.ComplexScriptItalics = true;
                            break;
                        case FontStyle.BoldItalic:
                            previewCharacterSettings.ComplexScriptBold = true;
                            previewCharacterSettings.ComplexScriptItalics = true;
                            break;
                    }
                    break;
                case "Superscript":
                    if (superscript)
                    {
                        previewCharacterSettings.VerticalAlignment = RunVerticalAlignment.Superscript;

                        if (subscript)
                        {
                            PropertyChanged -= CharacterSettingsPreviewAdapter_PropertyChanged;
                            Subscript = false;
                            PropertyChanged += CharacterSettingsPreviewAdapter_PropertyChanged;
                        }
                    }
                    else
                    {
                        previewCharacterSettings.VerticalAlignment = RunVerticalAlignment.Baseline;
                    }
                    break;
                case "Subscript":
                    if (subscript)
                    {
                        previewCharacterSettings.VerticalAlignment = RunVerticalAlignment.Subscript;

                        if (superscript)
                        {
                            PropertyChanged -= CharacterSettingsPreviewAdapter_PropertyChanged;
                            Superscript = false;
                            PropertyChanged += CharacterSettingsPreviewAdapter_PropertyChanged;
                        }
                    }
                    else
                    {
                        previewCharacterSettings.VerticalAlignment = RunVerticalAlignment.Baseline;
                    }
                    break;
                case "StrikeThrough":
                    previewCharacterSettings.StrikeThrough = strikeThrough;
                    if (doubleStrikeThrough == true)
                    {
                        PropertyChanged -= CharacterSettingsPreviewAdapter_PropertyChanged;
                        DoubleStrikeThrough = false;
                        PropertyChanged += CharacterSettingsPreviewAdapter_PropertyChanged;
                        previewCharacterSettings.DoubleStrikeThrough = false;
                    }
                    break;
                case "DoubleStrikeThrough":
                    previewCharacterSettings.DoubleStrikeThrough = doubleStrikeThrough;
                    if (strikeThrough == true)
                    {
                        PropertyChanged -= CharacterSettingsPreviewAdapter_PropertyChanged;
                        StrikeThrough = false;
                        PropertyChanged += CharacterSettingsPreviewAdapter_PropertyChanged;
                        previewCharacterSettings.StrikeThrough = false;
                    }
                    break;
                case "Font":
                    previewCharacterSettings.FontSettings.Ascii = new RichTextFont(font);
                    break;
                case "ComplexScriptFont":
                    previewCharacterSettings.FontSettings.ComplexScript = new RichTextFont(complexScriptFont);
                    break;
                case "FontColor":
                    if (fontColor == Colors.Transparent || fontColor.A < 1)
                        previewCharacterSettings.Color = ColorInfo.Automatic;
                    else
                        previewCharacterSettings.Color = new ColorInfo(fontColor);
                    break;
                case "UnderlineColot":
                    if (underlineColot == Colors.Transparent)
                        previewCharacterSettings.UnderlineColor = ColorInfo.Automatic;
                    else
                        previewCharacterSettings.UnderlineColor = new ColorInfo(underlineColot);
                    break;
                case "UnderlineType":
                    previewCharacterSettings.UnderlineType = underlineType;
                    break;
                case "FontSize":
                    var oldSize = previewCharacterSettings.FontSize.Value.LogicalPixels;
                    previewCharacterSettings.FontSize = new Extent(fontSize, ExtentUnitType.Points);
                    var newSize = previewCharacterSettings.FontSize.Value.LogicalPixels;
                    translateTransform.Y += oldSize - newSize;
                    break;
                case "ComplexScriptFontSize":
                    double oldSizeC = 11;

                    if (previewCharacterSettings.ComplexScriptFontSize != null)
                        oldSizeC = previewCharacterSettings.ComplexScriptFontSize.Value.LogicalPixels;

                    previewCharacterSettings.ComplexScriptFontSize = new Extent(complexScriptFontSize, ExtentUnitType.Points);
                    var newSizeC = previewCharacterSettings.ComplexScriptFontSize.Value.LogicalPixels;
                    translateTransformComplexScript.Y += oldSizeC - newSizeC;
                    break;
            }

            string error;

            if (previewCharacterSettings != null)
            {
                Editor.Document.ApplyCharacterSettings(DocumentSpan.All, previewCharacterSettings, out error);

                if (ComplexScriptEditor != null)
                    ComplexScriptEditor.Document.ApplyCharacterSettings(DocumentSpan.All, previewCharacterSettings, out error);
            }

        }

        private TranslateTransform translateTransform;
        private TranslateTransform translateTransformComplexScript;

        #endregion //CharacterSettingsPreviewAdapter_PropertyChanged

        #region CharacterSettings Properties

        private FontStyle fontStyle;
        public FontStyle FontStyle
        {
            get { return fontStyle; }
            set { fontStyle = value; NotifyPropertyChanged(); }
        }

        private FontStyle complexScriptFontStyle;
        public FontStyle ComplexScriptFontStyle
        {
            get { return complexScriptFontStyle; }
            set { complexScriptFontStyle = value; NotifyPropertyChanged(); }
        }

        private Color fontColor;
        public Color FontColor
        {
            get { return fontColor; }
            set { fontColor = value; NotifyPropertyChanged(); }
        }

        private Color underlineColot;
        public Color UnderlineColot
        {
            get { return underlineColot; }
            set { underlineColot = value; NotifyPropertyChanged(); }
        }

        public IEnumerable<UnderlineType> UnderlineTypes
        {
            get
            {
                return new List<UnderlineType> 
                { 
                    UnderlineType.Single,
                    UnderlineType.Double, 
                    UnderlineType.Thick, 
                    UnderlineType.Dotted, 
                    UnderlineType.Dash,
                    UnderlineType.DotDash,
                    UnderlineType.DotDotDash,
                    UnderlineType.None
                };
            }
        }

        private UnderlineType underlineType;
        public UnderlineType UnderlineType
        {
            get { return underlineType; }
            set { underlineType = value; NotifyPropertyChanged(); }
        }

        public Array FontStyles
        {
            get { return Enum.GetValues(typeof(FontStyle)); }
        }

        private string font;
        public string Font
        {
            get { return font; }
            set { font = value; NotifyPropertyChanged(); }
        }

        private string complexScriptFont;
        public string ComplexScriptFont
        {
            get { return complexScriptFont; }
            set { complexScriptFont = value; NotifyPropertyChanged(); }
        }

        private bool? strikeThrough;
        public bool? StrikeThrough
        {
            get { return strikeThrough; }
            set { strikeThrough = value; NotifyPropertyChanged(); }
        }

        private bool? doubleStrikeThrough;
        public bool? DoubleStrikeThrough
        {
            get { return doubleStrikeThrough; }
            set { doubleStrikeThrough = value; NotifyPropertyChanged(); }
        }

        private bool superscript;
        public bool Superscript
        {
            get { return superscript; }
            set { superscript = value; NotifyPropertyChanged(); }
        }

        private bool subscript;
        public bool Subscript
        {
            get { return subscript; }
            set { subscript = value; NotifyPropertyChanged(); }
        }

        private double fontSize;
        public double FontSize
        {
            get { return fontSize; }
            set { fontSize = value; NotifyPropertyChanged(); }
        }

        private double complexScriptFontSize;
        public double ComplexScriptFontSize
        {
            get { return complexScriptFontSize; }
            set { complexScriptFontSize = value; NotifyPropertyChanged(); }
        }

        private bool? smallCaps;
        public bool? SmallCaps
        {
            get { return smallCaps; }
            set { smallCaps = value; NotifyPropertyChanged(); }
        }

        private bool? allCaps;
        public bool? AllCaps
        {
            get { return allCaps; }
            set { allCaps = value; NotifyPropertyChanged(); }
        }

        #endregion // CharacterSettings Properties

        #region OnPropertyChanged

        private void OnPropertyChanged(string propertyName, object oldValue, object newValue)
        {
            switch (propertyName)
            {
                case EditorPropertyName:
                    this.translateTransform = new TranslateTransform(0, 24);
                    Editor.RenderTransform = translateTransform;
                    (newValue as XamRichTextEditor).DocumentContentChanged += CharacterSettingsPreviewAdapter_DocumentContentChanged; break;
                case ComplexScriptEditorPropertyName:
                    this.translateTransformComplexScript = new TranslateTransform(0, 28);
                    ComplexScriptEditor.RenderTransform = translateTransformComplexScript;
                    (newValue as XamRichTextEditor).DocumentContentChanged += CharacterSettingsPreviewAdapter_DocumentContentChanged; break;
                case InputCharacterSettingsPropertyName: ReadInputCharacterSettings(newValue as CharacterSettings); break;
            }
        }

        #endregion //OnPropertyChanged

        #region CharacterSettingsPreviewAdapter_DocumentContentChanged

        void CharacterSettingsPreviewAdapter_DocumentContentChanged(object sender, DocumentContentChangedEventArgs e)
        {
            string error;

            var editor = sender as XamRichTextEditor;

            if (previewCharacterSettings != null)
            {
                editor.Document.ApplyCharacterSettings(DocumentSpan.All, previewCharacterSettings, out error);
            }
        }

        #endregion //CharacterSettingsPreviewAdapter_DocumentContentChanged

        #region ReadInputCharacterSettings

        private void ReadInputCharacterSettings(CharacterSettings characterSettings)
        {
            string error;
            if (characterSettings != null)
            {
                PropertyChanged -= CharacterSettingsPreviewAdapter_PropertyChanged;

                previewCharacterSettings = characterSettings;
                Editor.Document.ApplyCharacterSettings(DocumentSpan.All, characterSettings, out error);

                if (previewCharacterSettings == null) return;

                if (previewCharacterSettings.Bold == true && previewCharacterSettings.Italics == true)
                {
                    this.FontStyle = FontStyle.BoldItalic;
                }
                else if (previewCharacterSettings.Bold == true && previewCharacterSettings.Italics == false)
                {
                    this.FontStyle = FontStyle.Bold;
                }
                else if (previewCharacterSettings.Bold == false && previewCharacterSettings.Italics == true)
                {
                    this.FontStyle = FontStyle.Italic;
                }
                else
                {
                    this.FontStyle = FontStyle.Regular;
                }

                if (previewCharacterSettings.ComplexScriptBold == true && previewCharacterSettings.ComplexScriptItalics == true)
                {
                    this.ComplexScriptFontStyle = FontStyle.BoldItalic;
                }
                else if (previewCharacterSettings.ComplexScriptBold == true && previewCharacterSettings.ComplexScriptItalics == false)
                {
                    this.ComplexScriptFontStyle = FontStyle.Bold;
                }
                else if (previewCharacterSettings.ComplexScriptBold == false && previewCharacterSettings.ComplexScriptItalics == true)
                {
                    this.ComplexScriptFontStyle = FontStyle.Italic;
                }
                else
                {
                    this.ComplexScriptFontStyle = FontStyle.Regular;
                }

                this.SmallCaps = previewCharacterSettings.SmallCaps;
                this.AllCaps = previewCharacterSettings.AllCaps;
                this.Subscript = previewCharacterSettings.VerticalAlignment == RunVerticalAlignment.Subscript;
                this.Superscript = previewCharacterSettings.VerticalAlignment == RunVerticalAlignment.Superscript;
                this.StrikeThrough = previewCharacterSettings.StrikeThrough;
                this.DoubleStrikeThrough = previewCharacterSettings.DoubleStrikeThrough;

                if (previewCharacterSettings.FontSettings != null && previewCharacterSettings.FontSettings.Ascii != null)
                {
                    var fontValue = previewCharacterSettings.FontSettings.Ascii.Value.GetResolvedFontName(Editor.Document.RootNode, previewCharacterSettings, 'a');
                    this.Font = fontValue;
                }

                if (previewCharacterSettings.FontSettings != null && previewCharacterSettings.FontSettings.ComplexScript != null)
                {
                    var fontValue = previewCharacterSettings.FontSettings.ComplexScript.Value.GetResolvedFontName(Editor.Document.RootNode, previewCharacterSettings, 'a');
                    this.ComplexScriptFont = fontValue;
                }

                if (previewCharacterSettings.Color != null && previewCharacterSettings.Color.Color.HasValue)
                {
                    FontColor = previewCharacterSettings.Color.Color.Value;
                }
                else
                {
                    UnderlineColot = Colors.Transparent;
                }

                if (previewCharacterSettings.UnderlineColor != null && previewCharacterSettings.UnderlineColor.Color.HasValue)
                {
                    UnderlineColot = previewCharacterSettings.UnderlineColor.Color.Value;
                }
                else
                {
                    UnderlineColot = Colors.Transparent;
                }

                UnderlineType = previewCharacterSettings.UnderlineType ?? UnderlineType.None;

                if (previewCharacterSettings.ComplexScriptFontSize.HasValue)
                {
                    this.ComplexScriptFontSize = previewCharacterSettings.ComplexScriptFontSize.Value.Points;
                    var offsetYComplex = new Extent(this.FontSize - deafultFontSize, ExtentUnitType.Points);
                    this.translateTransform.Y -= offsetYComplex.LogicalPixels;
                }
                else
                {
                    this.ComplexScriptFontSize = deafultFontSize;
                }

                if (previewCharacterSettings.FontSize.HasValue)
                {
                    this.FontSize = previewCharacterSettings.FontSize.Value.Points;

                    var offsetY = new Extent(this.FontSize - deafultFontSize, ExtentUnitType.Points);
                    this.translateTransform.Y -= offsetY.LogicalPixels;
                }
                PropertyChanged += CharacterSettingsPreviewAdapter_PropertyChanged;
            }
        }

        #endregion //ReadInputCharacterSettings

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void NotifyPropertyChanged([CallerMemberName]string propertyName = "")
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion //PropertyChanged
    }
}
