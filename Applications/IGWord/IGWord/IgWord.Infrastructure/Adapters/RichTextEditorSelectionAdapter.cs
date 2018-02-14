using IgWord.Infrastructure.Commands;
using Infragistics.Controls.Editors;
using Infragistics.Documents.RichText;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace IgWord.Infrastructure.Adapters
{

    public class RichTextEditorSelectionAdapter : FrameworkElement, INotifyPropertyChanged
    {
        #region Member Variables

        private bool _isUpdatingSelectionInfo;

        private Dictionary<Color, HighlightColor> colorToHighlightColorTable;

        public Dictionary<Color, HighlightColor> ColorToHighlightColorTable
        {
            get { return colorToHighlightColorTable; }
            set { colorToHighlightColorTable = value; }
        }
        private Dictionary<HighlightColor, Color> highlightColorToColorTable;

        public Dictionary<HighlightColor, Color> HighlightColorToColorTable
        {
            get { return highlightColorToColorTable; }
            set { highlightColorToColorTable = value; }
        }
        public List<Color> HighlightColors { get; private set; }

        private string[] bulletListStylesList;
        private string[] numberListStylesList;

        //Commands
        private AdapterFontColorCommand fontColorCommand;
        private AdapterTextHighlightColorCommand textHighlightColorCommand;
        private AdapterApplyListStyleCommand applyListStyleCommand;
        private AdapterShadingCommand shadingCommand;
        private AdapterApplyUnderlineCommand applyUnderlineCommand;
        private AdapterToggleUnderlineCommand toggleUnderlineCommand;
        private AdapterFontSizeCommand fontSizeCommand;
        private AdapterClearAllFormatingCommand clearAllFormatingCommand;
        private AdapterUpperCaseCommand upperCaseCommand;
        private AdapterLowerCaseCommand lowerCaseCommand;
        private AdapterToggleCaseCommand toggleCaseCommand;
        private AdapterLineSpacingCommand lineSpacingCommand;

        #endregion //Member Variables

        #region Constructor

        public RichTextEditorSelectionAdapter()
        {
            colorToHighlightColorTable = new Dictionary<Color, HighlightColor>();
            highlightColorToColorTable = new Dictionary<HighlightColor, Color>();
            HighlightColors = new List<Color>();

            foreach (HighlightColor highlightColor in Enum.GetValues(typeof(HighlightColor)))
            {
                if (highlightColor == HighlightColor.None)
                {
                    highlightColorToColorTable.Add(highlightColor, Colors.Transparent);
                    colorToHighlightColorTable.Add(Colors.Transparent, highlightColor);
                }
                else if (highlightColor == HighlightColor.DarkYellow)
                {
                    var color = new Color() { R = 128, G = 128, B = 0, A = 255 };
                    highlightColorToColorTable.Add(highlightColor, color);
                    colorToHighlightColorTable.Add(color, highlightColor);
                }
                else
                {
                    var color = (Color)ColorConverter.ConvertFromString(highlightColor.ToString());
                    highlightColorToColorTable.Add(highlightColor, color);
                    colorToHighlightColorTable.Add(color, highlightColor);
                }

                HighlightColors.Add(highlightColorToColorTable[highlightColor]);
            }

            HighlightColors.Reverse();

            bulletListStylesList = Enum.GetNames(typeof(BulletListStyles));
            numberListStylesList = Enum.GetNames(typeof(NumberListStyles));

            this.LastAppliedUnderline = UnderlineType.Single;

            this.PropertyChanged += RichTextEditorSelectionAdapter_PropertyChanged;

            //Init Commands
            fontColorCommand = new AdapterFontColorCommand(this);
            textHighlightColorCommand = new AdapterTextHighlightColorCommand(this);
            applyListStyleCommand = new AdapterApplyListStyleCommand(this);
            shadingCommand = new AdapterShadingCommand(this);
            applyUnderlineCommand = new AdapterApplyUnderlineCommand(this);
            toggleUnderlineCommand = new AdapterToggleUnderlineCommand(this);
            fontSizeCommand = new AdapterFontSizeCommand(this);
            clearAllFormatingCommand = new AdapterClearAllFormatingCommand(this);
            upperCaseCommand = new AdapterUpperCaseCommand(this);
            lowerCaseCommand = new AdapterLowerCaseCommand(this);
            toggleCaseCommand = new AdapterToggleCaseCommand(this);
            lineSpacingCommand = new AdapterLineSpacingCommand(this);
        }

        #endregion //Constructor

        #region DependencyProperties

        #region Editor

        /// <summary>
        /// Identifies the <see cref="Editor"/> dependency property
        /// </summary>
        public static readonly DependencyProperty EditorProperty = DependencyProperty.Register(EditorPropertyName,
            typeof(XamRichTextEditor), typeof(RichTextEditorSelectionAdapter),
            new PropertyMetadata(null,
                (o, e) =>
                {
                    ((RichTextEditorSelectionAdapter)o).OnDependencyPropertyChanged(EditorPropertyName, e.OldValue, e.NewValue);
                }));

        internal const string EditorPropertyName = "Editor";

        /// <summary>
        /// Returns or sets the editor whose selection information is being exposed.
        /// </summary>
        /// <seealso cref="EditorProperty"/>
        public XamRichTextEditor Editor
        {
            get
            {
                return (XamRichTextEditor)this.GetValue(RichTextEditorSelectionAdapter.EditorProperty);
            }
            set
            {
                this.SetValue(RichTextEditorSelectionAdapter.EditorProperty, value);
            }
        }

        #endregion //Editor

        #region Selection

        public static readonly DependencyProperty SelectionProperty = DependencyProperty.Register(SelectionPropertyName,
            typeof(Selection), typeof(RichTextEditorSelectionAdapter),
            new PropertyMetadata(null,
                (o, e) =>
                {
                    ((RichTextEditorSelectionAdapter)o).OnDependencyPropertyChanged(SelectionPropertyName, e.OldValue, e.NewValue);
                }));

        internal const string SelectionPropertyName = "Selection";


        public Selection Selection
        {
            get
            {
                return (Selection)this.GetValue(RichTextEditorSelectionAdapter.SelectionProperty);
            }
            set
            {
                this.SetValue(RichTextEditorSelectionAdapter.SelectionProperty, value);
            }
        }

        #endregion //Selection

        #endregion //DependencyProperties

        #region Properties

        private bool isBold;
        public bool IsBold
        {
            get { return isBold; }
            set
            {
                if (isBold != value)
                {
                    isBold = value;
                    NotifyPropertyChanged();
                }
            }
        }
        internal const string IsBoldPropertyName = "IsBold";

        private bool isItalic;
        public bool IsItalic
        {
            get { return isItalic; }
            set
            {
                if (isItalic != value)
                {
                    isItalic = value;
                    NotifyPropertyChanged();
                }
            }
        }
        internal const string IsItalicPropertyName = "IsItalic";

        private bool isStrikethrough;
        public bool IsStrikethrough
        {
            get { return isStrikethrough; }
            set
            {
                if (isStrikethrough != value)
                {
                    isStrikethrough = value;
                    NotifyPropertyChanged();
                }
            }
        }
        internal const string IsStrikethroughPropertyName = "IsStrikethrough";

        private string fontName;
        public string FontName
        {
            get { return fontName; }
            set
            {
                if (fontName != value)
                {
                    fontName = value;

                    NotifyPropertyChanged();
                }
            }
        }
        internal const string FontNamePropertyName = "FontName";

        private double? fontSize;
        public double? FontSize
        {
            get { return fontSize; }
            set
            {
                if (fontSize != value)
                {
                    fontSize = value;
                    NotifyPropertyChanged();
                }
            }
        }
        internal const string FontSizePropertyName = "FontSize";

        private Color? shading;
        public Color? Shading
        {
            get { return shading; }
            set
            {
                if (shading != value)
                {
                    shading = value;
                    NotifyPropertyChanged();
                }
            }
        }
        internal const string ShadingPropertyName = "Shading";

        private Color? fontColor;
        public Color? FontColor
        {
            get { return fontColor; }
            set
            {
                if (fontColor != value)
                {
                    fontColor = value;
                    NotifyPropertyChanged();
                }
            }
        }
        internal const string FontColorPropertyName = "FontColor";

        private Color textHighlightColor;
        public Color TextHighlightColor
        {
            get { return textHighlightColor; }
            set
            {
                if (textHighlightColor != value)
                {
                    textHighlightColor = value;
                    NotifyPropertyChanged();
                }
            }
        }
        internal const string TextHighlightColorPropertyName = "TextHighlightColor";

        private bool isSuperscript;
        public bool IsSuperscript
        {
            get { return isSuperscript; }
            set
            {
                if (isSuperscript != value)
                {
                    isSuperscript = value;
                    NotifyPropertyChanged();
                }
            }
        }
        internal const string IsSuperscriptPropertyName = "IsSuperscript";

        private bool isSubscript;
        public bool IsSubscript
        {
            get { return isSubscript; }
            set
            {
                if (isSubscript != value)
                {
                    isSubscript = value;
                    NotifyPropertyChanged();
                }
            }
        }
        internal const string IsSubscriptPropertyName = "IsSubscript";

        private UnderlineType? underline;
        public UnderlineType? Underline
        {
            get { return underline; }
            set
            {
                if (underline != value)
                {
                    underline = value;
                    NotifyPropertyChanged();
                }
            }
        }
        internal const string UnderlinePropertyName = "Underline";

        private ParagraphAlignment? paragraphAlignment;
        public ParagraphAlignment? ParagraphAlignment
        {
            get { return paragraphAlignment; }
            set
            {
                if (paragraphAlignment != value)
                {
                    paragraphAlignment = value;
                    NotifyPropertyChanged();
                }
            }
        }
        internal const string ParagraphAlignmentPropertyName = "ParagraphAlignment";

        private string bulletListStyle;
        public string BulletListStyle
        {
            get { return bulletListStyle; }
            set
            {
                if (bulletListStyle != value)
                {
                    bulletListStyle = value;
                    NotifyPropertyChanged();
                }
            }
        }
        internal const string BulletListStylePropertyName = "BulletListStyle";

        private string numberListStyle;
        public string NumberListStyle
        {
            get { return numberListStyle; }
            set
            {
                if (numberListStyle != value)
                {
                    numberListStyle = value;
                    NotifyPropertyChanged();
                }
            }
        }
        internal const string NumberListStylePropertyName = "NumberListStyle";


        private bool isNumberList;
        public bool IsNumberList
        {
            get { return isNumberList; }
            set
            {
                if (isNumberList != value)
                {
                    isNumberList = value;
                    NotifyPropertyChanged();
                }
            }
        }
        internal const string IsNumberListPropertyName = "IsNumberList";

        private bool isBulletList;
        public bool IsBulletList
        {
            get { return isBulletList; }
            set
            {
                if (isBulletList != value)
                {
                    isBulletList = value;
                    NotifyPropertyChanged();
                }
            }
        }
        internal const string IsBulletListPropertyName = "IsBulletList";

        private bool isUnderline;
        public bool IsUnderline
        {
            get { return isUnderline; }
            set
            {
                if (isUnderline != value)
                {
                    isUnderline = value;
                    NotifyPropertyChanged();
                }
            }
        }
        internal const string IsUnderlinePropertyName = "IsUnderline";

        public IEnumerable<string> BulletListStyles
        {
            get
            {
                var result = new List<string>();

                foreach (var item in Enum.GetValues(typeof(BulletListStyles)))
                {
                    result.Add(item.ToString());
                }

                return result;
            }
        }

        public IEnumerable<string> NumberListStyles
        {
            get
            {
                var result = new List<string>();

                foreach (var item in Enum.GetValues(typeof(NumberListStyles)))
                {
                    result.Add(item.ToString());
                }

                return result;
            }
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


        private UnderlineType lastAppliedUnderline;
        public UnderlineType LastAppliedUnderline
        {
            get { return lastAppliedUnderline; }
            set
            {
                if (lastAppliedUnderline != value)
                {
                    lastAppliedUnderline = value;
                    NotifyPropertyChanged();
                }
            }
        }
        internal const string LastAppliedUnderlinePropertyName = "LastAppliedUnderline";

        #endregion //Properties

        #region Commands

        public ICommand FontColorCommand
        {
            get { return fontColorCommand; }
        }

        public ICommand TextHighlightColorCommand
        {
            get { return textHighlightColorCommand; }
        }

        public ICommand ShadingCommand
        {
            get { return shadingCommand; }
        }

        public ICommand ApplyListStyleCommand
        {
            get { return applyListStyleCommand; }
        }

        public ICommand ApplyUnderlineCommand
        {
            get { return applyUnderlineCommand; }
        }

        public ICommand ToggleUnderlineCommand
        {
            get { return toggleUnderlineCommand; }
        }

        public ICommand ClearAllFormatingCommand
        {
            get { return clearAllFormatingCommand; }
        }

        public ICommand UpperCaseCommand
        {
            get { return upperCaseCommand; }
        }

        public ICommand LowerCaseCommand
        {
            get { return lowerCaseCommand; }
        }

        public ICommand ToggleCaseCommand
        {
            get { return toggleCaseCommand; }
        }

        public ICommand LineSpacingCommand
        {
            get { return lineSpacingCommand; }
        }
        #endregion //Commands

        #region OnDependencyPropertyChanged

        private void OnDependencyPropertyChanged(string propertyName, object oldValue, object newValue)
        {
            switch (propertyName)
            {
                case EditorPropertyName:
                    this.HookEditor(oldValue as XamRichTextEditor, false);
                    this.HookEditor(newValue as XamRichTextEditor, true);
                    this.UpdateSelectionInfo();
                    break;
            }
        }

        #endregion //OnDependencyPropertyChanged

        #region RichTextEditorSelectionAdapter_PropertyChanged

        void RichTextEditorSelectionAdapter_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (Selection == null)
                return;

            this.HookEditor(Editor, false);

            string error;

            switch (e.PropertyName)
            {
                case IsBoldPropertyName:
                    if (isBold == true)
                        Selection.ApplyBoldFormatting();
                    else Selection.RemoveBoldFormatting();
                    break;
                case FontSizePropertyName:
                    if (this.fontSize.HasValue)
                        Selection.ApplyFontSize(this.fontSize.Value);
                    break;
                case FontNamePropertyName:
                    if (this.fontName != null)
                        Selection.ApplyFont(new RichTextFont(this.fontName));
                    break;
                case FontColorPropertyName:
                    if (this.fontColor.HasValue)
                        Selection.ApplyTextForecolor(new ColorInfo(this.fontColor.Value));
                    else
                        Selection.ApplyTextForecolor(ColorInfo.Automatic);
                    break;
                case TextHighlightColorPropertyName:
                    Selection.ApplyTextHighlightColor(colorToHighlightColorTable[this.textHighlightColor]);
                    break;
                case ShadingPropertyName:
                    if (this.shading.HasValue)
                        Selection.ApplyParagraphShading(new Shading(new ColorInfo(this.shading.Value)));
                    else
                        Selection.ApplyParagraphShading(new Shading(ColorInfo.Automatic));
                    break;
                case IsSuperscriptPropertyName:
                    Selection.ApplySuperscriptFormatting();
                    break;
                case IsSubscriptPropertyName:
                    Selection.ApplySubscriptFormatting();
                    break;
                case IsStrikethroughPropertyName:
                    if (isStrikethrough)
                        Selection.ApplySingleStrikethroughFormatting();
                    else
                        Selection.RemoveSingleStrikethroughFormatting();
                    break;
                case BulletListStylePropertyName:
                    if (bulletListStyle == "None")
                        Editor.Document.ClearParagraphListStyle(Selection.DocumentSpan, out error);
                    else
                        Editor.Document.ApplyParagraphListStyle(Selection.DocumentSpan, bulletListStyle, out error);
                    break;
                case NumberListStylePropertyName:
                    if (numberListStyle == "None")
                        Editor.Document.ClearParagraphListStyle(Selection.DocumentSpan, out error);
                    else
                        Editor.Document.ApplyParagraphListStyle(Selection.DocumentSpan, numberListStyle, out error);
                    break;
            }

            this.HookEditor(Editor, true);
        }

        #endregion //RichTextEditorSelectionAdapter_PropertyChanged

        #region Methods

        #region HookEditor
        private void HookEditor(XamRichTextEditor editor, bool hook)
        {
            if (editor != null)
            {
                if (hook)
                {
                    editor.DocumentChanged += OnDocumentChanged;
                    editor.DocumentContentChanged += OnDocumentContentChanged;
                    editor.SelectionChanged += OnEditorSelectionChanged;
                    editor.IsVisibleChanged += OnEditorIsVisibleChanged;
                }
                else
                {
                    editor.DocumentChanged -= OnDocumentChanged;
                    editor.DocumentContentChanged -= OnDocumentContentChanged;
                    editor.SelectionChanged -= OnEditorSelectionChanged;
                    editor.IsVisibleChanged -= OnEditorIsVisibleChanged;
                }
            }
        }

        #endregion //HookEditor

        #region OnEditorIsVisibleChanged

        private void OnEditorIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Editor.Visibility == System.Windows.Visibility.Visible)
            {
                this.UpdateSelectionInfo();
            }
            else
            {
                this.ParagraphAlignment = null;
                this.FontSize = null;
                this.FontName = null;
            }
        }

        #endregion //OnEditorIsVisibleChanged

        #region OnDocumentChanged

        private void OnDocumentChanged(object sender, DocumentChangedEventArgs e)
        {
            this.UpdateSelectionInfo();
        }

        #endregion //OnDocumentChanged

        #region OnEditorSelectionChanged
        private void OnEditorSelectionChanged(object sender, RichTextSelectionChangedEventArgs e)
        {
            this.UpdateSelectionInfo();
        }
        #endregion //OnEditorSelectionChanged

        #region OnDocumentContentChanged
        private void OnDocumentContentChanged(object sender, DocumentContentChangedEventArgs e)
        {
            this.UpdateSelectionInfo();
        }
        #endregion //OnDocumentContentChanged

        #region UpdateSelectionInfo

        private void UpdateSelectionInfo()
        {
            this.PropertyChanged -= RichTextEditorSelectionAdapter_PropertyChanged;

            try
            {
                var editor = this.Editor;

                if (editor.Visibility != System.Windows.Visibility.Visible)
                    return;

                var document = editor != null ? editor.Document : null;
                var selectionSpan = this.GetCurrentSelectionDocumentSpan();
                var charSettings = document != null ? document.GetCommonCharacterSettings(selectionSpan) : null;
                var paraSettings = document != null ? document.GetCommonParagraphSettings(selectionSpan) : null;

                _isUpdatingSelectionInfo = true;

                if (this.Editor.Selection != null)
                {
                    this.Selection = this.Editor.Selection;
                }


                if (charSettings == null)
                {
                    this.IsBold = this.IsItalic = this.IsStrikethrough = false;
                    this.Underline = null;
                    this.FontSize = null;
                    this.FontName = null;
                    this.FontColor = null;
                    this.Shading = null;
                }
                else
                {
                    this.IsBold = (bool)charSettings.Bold;
                    this.IsItalic = (bool)charSettings.Italics;
                    this.IsStrikethrough = (bool)charSettings.StrikeThrough;
                    this.Underline = charSettings.UnderlineType;
                    this.FontSize = charSettings.FontSize != null ? charSettings.FontSize.Value.Points : (double?)null;
                    this.IsSubscript = charSettings.VerticalAlignment == RunVerticalAlignment.Subscript;
                    this.IsSuperscript = charSettings.VerticalAlignment == RunVerticalAlignment.Superscript;
                    this.IsStrikethrough = (bool)charSettings.StrikeThrough;

                    if (charSettings.UnderlineType.HasValue)
                    {
                        this.IsUnderline = charSettings.UnderlineType != UnderlineType.None;
                    }
                    else
                    {
                        this.IsUnderline = false;
                    }

                    if (this.Editor.Selection != null)
                    {
                        var style = Editor.Document.GetCommonParagraphListStyle(selectionSpan);

                        if (!string.IsNullOrEmpty(style))
                        {
                            if (numberListStylesList.Contains(style))
                            {

                                NumberListStyle = style;
                                BulletListStyle = Infrastructure.BulletListStyles.None.ToString();
                                IsNumberList = true;
                                IsBulletList = false;
                            }
                            else
                            {
                                BulletListStyle = style;
                                NumberListStyle = Infrastructure.NumberListStyles.None.ToString();
                                IsNumberList = false;
                                IsBulletList = true;
                            }
                        }
                        else
                        {
                            NumberListStyle = BulletListStyle = Infrastructure.BulletListStyles.None.ToString();
                            IsBulletList = IsNumberList = false;
                        }
                    }

                    if (charSettings.FontSettings != null && charSettings.FontSettings.ComplexScript != null)
                    {
                        if (charSettings.FontSettings.Ascii.HasValue)
                        {
                            var fontValue = charSettings.FontSettings.Ascii.Value.GetResolvedFontName(Editor.Document.RootNode, charSettings, 'a');
                            this.FontName = fontValue;
                        }
                        else
                        {
                            this.FontName = null;
                        }
                    }
                    else
                    {
                        this.FontName = null;
                    }

                    if (charSettings.Color != null && charSettings.Color.Color.HasValue)
                    {
                        FontColor = charSettings.Color.Color.Value;
                    }
                    else
                    {
                        FontColor = Colors.Black;
                    }

                    if (charSettings.Shading.HasValue)
                        this.Shading = charSettings.Shading.Value.Background.Color;

                    if (charSettings.HighlightColor.HasValue)
                    {
                        TextHighlightColor = highlightColorToColorTable[charSettings.HighlightColor.Value];
                    }
                    else
                    {
                        TextHighlightColor = Colors.Transparent;
                    }
                }

                if (paraSettings == null || editor.Visibility != System.Windows.Visibility.Visible)
                {
                    this.ParagraphAlignment = null;
                }
                else
                {
                    this.ParagraphAlignment = paraSettings.ParagraphAlignment;
                }

                _isUpdatingSelectionInfo = false;
            }
            catch
            {
                _isUpdatingSelectionInfo = false;
            }

            this.PropertyChanged += RichTextEditorSelectionAdapter_PropertyChanged;

            fontColorCommand.RaiseCanExecuteChanged();
            textHighlightColorCommand.RaiseCanExecuteChanged();
            applyListStyleCommand.RaiseCanExecuteChanged();
            shadingCommand.RaiseCanExecuteChanged();
            applyUnderlineCommand.RaiseCanExecuteChanged();
            toggleUnderlineCommand.RaiseCanExecuteChanged();
            fontSizeCommand.RaiseCanExecuteChanged();
            clearAllFormatingCommand.RaiseCanExecuteChanged();
            upperCaseCommand.RaiseCanExecuteChanged();
            lowerCaseCommand.RaiseCanExecuteChanged();
            toggleCaseCommand.RaiseCanExecuteChanged();
            lineSpacingCommand.RaiseCanExecuteChanged();
        }

        #endregion //UpdateSelectionInfo

        #region GetCurrentSelectionDocumentSpan

        private DocumentSpan GetCurrentSelectionDocumentSpan()
        {
            DocumentSpan documentSpan = new DocumentSpan(0, 0);
            var editor = this.Editor;

            if (editor != null)
            {
                Selection selection = editor.Selection;
                if (null != selection)
                    documentSpan = selection.DocumentSpan;
            }

            return documentSpan;
        }

        #endregion //GetCurrentSelectionDocumentSpan

        #endregion //Methods

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void NotifyPropertyChanged([CallerMemberName]string propertyName = "")
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion //INotifyPropertyChanged
    }
}
