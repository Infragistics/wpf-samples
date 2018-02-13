using Infragistics.Controls.Editors;
using Infragistics.Documents.RichText;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;

namespace IgWord.Infrastructure.RichTextFormatBar
{
    /// <summary>
    /// Interaction logic for RichTextFormatBar.xaml
    /// </summary>
    public partial class RichTextFormatBar : UserControl, IRichTextFormatBar, INotifyPropertyChanged
    {
        bool _updatingVisualState;

        #region Properties

        public static readonly DependencyProperty FontSizesProperty = DependencyProperty.Register("FontSizes", typeof(List<double>), typeof(RichTextFormatBar));
        public List<double> FontSizes
        {
            get
            {
                return (List<double>)this.GetValue(RichTextFormatBar.FontSizesProperty);
            }
            set
            {
                this.SetValue(RichTextFormatBar.FontSizesProperty, value);
            }
        }

        public static readonly DependencyProperty FontNamesProperty = DependencyProperty.Register("FontNames", typeof(List<string>), typeof(RichTextFormatBar));
        public List<string> FontNames
        {
            get
            {
                return (List<string>)this.GetValue(RichTextFormatBar.FontNamesProperty);
            }
            set
            {
                this.SetValue(RichTextFormatBar.FontNamesProperty, value);
            }
        }

        public static readonly DependencyProperty TargetProperty = DependencyProperty.Register("Target", typeof(XamRichTextEditor), typeof(RichTextFormatBar));
        public XamRichTextEditor Target
        {
            get { return (XamRichTextEditor)GetValue(TargetProperty); }
            set { SetValue(TargetProperty, value); }
        }

        
        private string fontFamilyTooltip;
        public string FontFamilyTooltip
        {
            get { return fontFamilyTooltip; }
            set { fontFamilyTooltip = value; NotifyPropertyChanged(); }
        }

        private string fontSizeTooltip;
        public string FontSizeTooltip
        {
            get { return fontSizeTooltip; }
            set { fontSizeTooltip = value; NotifyPropertyChanged(); }
        }

        private string boldTooltip;
        public string BoldTooltip
        {
            get { return boldTooltip; }
            set { boldTooltip = value; NotifyPropertyChanged(); }
        }

        private string italicTooltip;
        public string ItalicTooltip
        {
            get { return italicTooltip; }
            set { italicTooltip = value; NotifyPropertyChanged(); }
        }

        private string underlineTooltip;
        public string UnderlineTooltip
        {
            get { return underlineTooltip; }
            set { underlineTooltip = value; NotifyPropertyChanged(); }
        }


        private string alignLeftTooltip;
        public string AlignLeftTooltip
        {
            get { return alignLeftTooltip; }
            set { alignLeftTooltip = value; NotifyPropertyChanged(); }
        }

        private string alignCenterTooltip;
        public string AlignCenterTooltip
        {
            get { return alignCenterTooltip; }
            set { alignCenterTooltip = value; NotifyPropertyChanged(); }
        }
        private string alignRightTooltip;
        public string AlignRightTooltip
        {
            get { return alignRightTooltip; }
            set { alignRightTooltip = value; NotifyPropertyChanged(); }
        }

        #endregion //Properties

        #region Constructor

        public RichTextFormatBar()
        {
            InitializeComponent();
        }

        #endregion //Constructor

        #region Events

        private void DragWidget_DragDelta(object sender, DragDeltaEventArgs e)
        {
            ProcessMove(e);
        }

        private void FontFamily_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0 || _updatingVisualState)
                return;

            var font = e.AddedItems[0];
            Target.Selection.ApplyFont(new Infragistics.Documents.RichText.RichTextFont(font.ToString()));
        }

        private void FontSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0 || _updatingVisualState)
                return;

            var size = (double)e.AddedItems[0];
            Target.Selection.ApplyFontSize(size);
        }

        #endregion //Events

        #region Methods

        /// <summary>
        /// Processes the dragging of the RichTextFormatBar
        /// </summary>
        private void ProcessMove(DragDeltaEventArgs e)
        {
            AdornerLayer layer = AdornerLayer.GetAdornerLayer(Target);
            UIElementAdorner<Control> adorner = layer.GetAdorners(Target)[0] as UIElementAdorner<Control>;
            adorner.SetOffsets(adorner.OffsetLeft + e.HorizontalChange, adorner.OffsetTop + e.VerticalChange);
        }

        /// <summary>
        /// Updates the visual state of the buttons and other tools of the RichTextFormatBar
        /// </summary>
        public void UpdateVisualState()
        {
            _updatingVisualState = true;

            DocumentSpan documentSpan = Target.Selection == null ? new DocumentSpan(0, 0) : Target.Selection.DocumentSpan;
            var settings = Target.Document.GetCommonCharacterSettings(documentSpan);
            UpdateSelectedFontFamily(settings);
            UpdateSelectedFontSize(settings);

            UpdateBoldState(settings);
            UpdateItalicState(settings);
            UpdateUnderlineState(settings);

            UpdateParagraphAlignmentState(documentSpan);

            _updatingVisualState = false;
        }

        /// <summary>
        /// Updates the selected item in the font family list.
        /// </summary>
        void UpdateSelectedFontFamily(CharacterSettings settings)
        {
            if (settings.FontSettings != null)
            {
                if (settings.FontSettings.Ascii != null)
                {
                    var fontValue = settings.FontSettings.Ascii.Value.GetResolvedFontName(Target.Document.RootNode, settings, 'a');

                    _cmbFontFamilies.SelectedValue = fontValue;
                }
                else
                {
                    _cmbFontFamilies.SelectedValue = string.Empty;
                }
            }
        }

        /// <summary>
        /// Updates the selected item in the font list.
        /// </summary>
        void UpdateSelectedFontSize(CharacterSettings settings)
        {
            _cmbFontSizes.SelectedValue = settings.FontSize.HasValue ? settings.FontSize.Value.Points : 12.0;
        }

        /// <summary>
        /// Updates the state of the bold button
        /// </summary>
        void UpdateBoldState(CharacterSettings settings)
        {
            UpdateToggleButtonCheckedState(_btnBold, settings.Bold);
        }

        /// <summary>
        /// Updates the state of the italic button
        /// </summary>
        void UpdateItalicState(CharacterSettings settings)
        {
            UpdateToggleButtonCheckedState(_btnItalic, settings.Italics);
        }

        /// <summary>
        /// Updates the state of the underline button
        /// </summary>
        void UpdateUnderlineState(CharacterSettings settings)
        {
            if (settings.UnderlineType.HasValue)
                UpdateToggleButtonCheckedState(_btnUnderline, settings.UnderlineType.Value != UnderlineType.None);
        }

        /// <summary>
        /// Updates the state of the paragraph alignment buttons
        /// </summary>
        void UpdateParagraphAlignmentState(DocumentSpan documentSpan)
        {
            ParagraphSettings paragraphSettings = Target.Document.GetCommonParagraphSettings(documentSpan);
            if (paragraphSettings.ParagraphAlignment.HasValue)
            {
                var value = paragraphSettings.ParagraphAlignment.Value;
                switch (value)
                {
                    case ParagraphAlignment.Start:
                        {
                            UpdateToggleButtonCheckedState(_btnAlignLeft, true);
                            break;
                        }
                    case ParagraphAlignment.Center:
                        {
                            UpdateToggleButtonCheckedState(_btnAlignCenter, true);
                            break;
                        }
                    case ParagraphAlignment.End:
                        {
                            UpdateToggleButtonCheckedState(_btnAlignRight, true);
                            break;
                        }
                }
            }
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

        #endregion //Methods

        #region PropertyChangedEventHandler

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void NotifyPropertyChanged([CallerMemberName]string propertyName = "")
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion //PropertyChangedEventHandler
    }
}
