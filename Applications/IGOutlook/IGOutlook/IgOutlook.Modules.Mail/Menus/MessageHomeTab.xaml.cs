using IgOutlook.Infrastructure;
using Infragistics.Controls.Editors;
using Infragistics.Documents.RichText;
using System.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;


namespace IgOutlook.Modules.Mail.Menus
{
    public partial class MessageHomeTab : ISupportRichText
    {
        #region Members

        bool _updatingTabVisualState;

        #endregion //Members

        #region Properties

        public static double[] FontSizes
        {
            get
            {
                return new double[] { 
		            3.0, 4.0, 5.0, 6.0, 6.5, 7.0, 7.5, 8.0, 8.5, 9.0, 9.5, 
		            10.0, 10.5, 11.0, 11.5, 12.0, 12.5, 13.0, 13.5, 14.0, 15.0,
		            16.0, 17.0, 18.0, 19.0, 20.0, 22.0, 24.0, 26.0, 28.0, 30.0,
		            32.0, 34.0, 36.0, 38.0, 40.0, 44.0, 48.0, 52.0, 56.0, 60.0, 64.0, 68.0, 72.0, 76.0,
		            80.0, 88.0, 96.0, 104.0, 112.0, 120.0, 128.0, 136.0, 144.0
		            };
            }
        }

        public static readonly DependencyProperty RichTextEditorProperty = DependencyProperty.Register("RichTextEditor", typeof(XamRichTextEditor), typeof(MessageHomeTab), new PropertyMetadata(null, OnRichTextEditorChanged));
        public XamRichTextEditor RichTextEditor
        {
            get { return (XamRichTextEditor)GetValue(RichTextEditorProperty); }
            set { SetValue(RichTextEditorProperty, value); }
        }

        private static void OnRichTextEditorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MessageHomeTab tab = d as MessageHomeTab;
            if (tab != null)
                tab.OnRichTextEditorChanged((XamRichTextEditor)e.OldValue, (XamRichTextEditor)e.NewValue);
        }

        #endregion //Properties

        #region Ctor

        public MessageHomeTab()
        {
            InitializeComponent();

            _fontFamilyList.ItemsSource = Fonts.SystemFontFamilies.ToList().Select(x => x.Source); //load our fonts
            _fontSizeList.ItemsSource = FontSizes; //load our font sizes
        } 

        #endregion

        #region Event Handlers

        private void BulletsTool_Click(object sender, RoutedEventArgs e)
        {
            SetSelectionParagraphListStyle(_bulletsTool);
        }

        private void FontFamily_SelectionChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (_updatingTabVisualState)
                return;

            string fontName = (string)e.NewValue;
            
            if (string.IsNullOrEmpty(fontName))
                return;

            RichTextEditor.Selection.ApplyFont(new Infragistics.Documents.RichText.RichTextFont(fontName));
        }

        private void FontSize_SelectionChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (_updatingTabVisualState)
                return;

            RichTextEditor.Selection.ApplyFontSize((double)e.NewValue);
        }

        private void NumbersTool_Click(object sender, RoutedEventArgs e)
        {
            SetSelectionParagraphListStyle(_numbersTool);
        }

        void RichTextEditor_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateTabVisualState();
        }

        void RichTextEditor_SelectionChanged(object sender, RichTextSelectionChangedEventArgs e)
        {
            UpdateTabVisualState();
        }

        #endregion //Event Handlers

        #region Methods

        protected virtual void OnRichTextEditorChanged(XamRichTextEditor oldValue, XamRichTextEditor newValue)
        {
            if (oldValue != null)
            {
                newValue.Loaded -= RichTextEditor_Loaded;
                newValue.SelectionChanged -= RichTextEditor_SelectionChanged;
            }

            if (newValue != null)
            {
                newValue.Loaded += RichTextEditor_Loaded;
                newValue.SelectionChanged += RichTextEditor_SelectionChanged;
            }
        }

        void SetSelectionParagraphListStyle(ToggleButton toggleButton)
        {
            if (toggleButton.IsChecked == true)
                RichTextEditor.Selection.ApplyParagraphListStyle(toggleButton.Tag.ToString());
            else
                RichTextEditor.Selection.ClearParagraphListStyle();
        }

        void UpdateBoldState(CharacterSettings settings)
        {
            UpdateToggleButtonCheckedState(_boldTool, settings.Bold);
        }

        void UpdateItalicState(CharacterSettings settings)
        {
            UpdateToggleButtonCheckedState(_italicTool, settings.Italics);
        }

        void UpdateParagraphAlignmentState(DocumentSpan documentSpan)
        {
            ParagraphSettings paragraphSettings = RichTextEditor.Document.GetCommonParagraphSettings(documentSpan);
            if (paragraphSettings.ParagraphAlignment.HasValue)
            {
                var value = paragraphSettings.ParagraphAlignment.Value;
                switch (value)
                {
                    case ParagraphAlignment.Start:
                        {
                            UpdateToggleButtonCheckedState(_alignLeftTool, true);
                            break;
                        }
                    case ParagraphAlignment.Center:
                        {
                            UpdateToggleButtonCheckedState(_alignCenterTool, true);
                            break;
                        }
                    case ParagraphAlignment.End:
                        {
                            UpdateToggleButtonCheckedState(_alignRightTool, true);
                            break;
                        }
                    case ParagraphAlignment.Justify:
                        {
                            UpdateToggleButtonCheckedState(_alignJustifyTool, true);
                            break;
                        }
                }
            }
        }

        void UpdateParagraphListStyleState(DocumentSpan documentSpan)
        {
            string listStyleId = RichTextEditor.Document.GetCommonParagraphListStyle(documentSpan);
            UpdateToggleButtonCheckedState(_bulletsTool, _bulletsTool.Tag.ToString() == listStyleId);
            UpdateToggleButtonCheckedState(_numbersTool, _numbersTool.Tag.ToString() == listStyleId);
        }

        /// <summary>
        /// Updates the selected item in the font family list.
        /// </summary>
        void UpdateSelectedFontFamily(CharacterSettings settings)
        {
            if (settings.FontSettings != null)
                _fontFamilyList.SelectedItem = (settings.FontSettings.Ascii.HasValue && !string.IsNullOrWhiteSpace(settings.FontSettings.Ascii.Value.Name)) ? settings.FontSettings.Ascii.Value.Name : "Arial";
        }

        /// <summary>
        /// Updates the selected item in the font list.
        /// </summary>
        void UpdateSelectedFontSize(CharacterSettings settings)
        {
            _fontSizeList.Value = settings.FontSize.HasValue ? settings.FontSize.Value.Points : 12.0;
        }

        void UpdateTabVisualState()
        {
            _updatingTabVisualState = true;

            DocumentSpan documentSpan = RichTextEditor.Selection == null ? new DocumentSpan(0, 0) : RichTextEditor.Selection.DocumentSpan;

            var settings = RichTextEditor.Document.GetCommonCharacterSettings(documentSpan);

            UpdateSelectedFontFamily(settings);
            UpdateSelectedFontSize(settings);

            UpdateBoldState(settings);
            UpdateItalicState(settings);
            UpdateUnderlineState(settings);

            UpdateParagraphAlignmentState(documentSpan);
            UpdateParagraphListStyleState(documentSpan);

            _updatingTabVisualState = false;
        }

        /// <summary>
        /// Updates the visual state of a toggle button.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <param name="formattingProperty">The formatting property.</param>
        /// <param name="expectedValue">The expected value.</param>
        void UpdateToggleButtonCheckedState(ToggleButton button, bool? value)
        {
            button.IsChecked = value.HasValue ? value.Value : false;
        }

        void UpdateUnderlineState(CharacterSettings settings)
        {
            if (settings.UnderlineType.HasValue)
                UpdateToggleButtonCheckedState(_underlineTool, settings.UnderlineType.Value != UnderlineType.None);
        }

        #endregion //Methods

        private void btnHighImportance_Checked(object sender, RoutedEventArgs e)
        {
            btnLowImportance.IsChecked = false;
        }

        private void btnLowImportance_Checked(object sender, RoutedEventArgs e)
        {
            btnHighImportance.IsChecked = false;
        }
    }
}
