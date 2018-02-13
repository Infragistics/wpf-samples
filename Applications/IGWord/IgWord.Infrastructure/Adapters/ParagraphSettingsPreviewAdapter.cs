using Infragistics.Controls.Editors;
using Infragistics.Documents.RichText;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

namespace IgWord.Infrastructure.Adapters
{
    public class ParagraphSettingsPreviewAdapter : FrameworkElement, INotifyPropertyChanged
    {
        #region Member Variables

        private ParagraphSettings previewParagraphSettings;
        private DocumentSpan previewParagraphIndexSpan;

        #endregion //Member Variables

        #region Constructor

        public ParagraphSettingsPreviewAdapter()
        {
            previewParagraphIndexSpan = DocumentSpan.All;
        }

        #endregion //Constructor

        #region Editor

        public static readonly DependencyProperty EditorProperty = DependencyProperty.Register(EditorPropertyName,
            typeof(XamRichTextEditor), typeof(ParagraphSettingsPreviewAdapter),
            new PropertyMetadata(null,
                (o, e) =>
                {
                    ((ParagraphSettingsPreviewAdapter)o).OnPropertyChanged(EditorPropertyName, e.OldValue, e.NewValue);
                }));

        internal const string EditorPropertyName = "Editor";

        public XamRichTextEditor Editor
        {
            get
            {
                return (XamRichTextEditor)this.GetValue(ParagraphSettingsPreviewAdapter.EditorProperty);
            }
            set
            {
                this.SetValue(ParagraphSettingsPreviewAdapter.EditorProperty, value);
            }
        }

        #endregion //Editor

        #region InputParagraphSettings

        public static readonly DependencyProperty InputParagraphSettingsProperty = DependencyProperty.Register(InputParagraphSettingsPropertyName,
            typeof(ParagraphSettings), typeof(ParagraphSettingsPreviewAdapter),
            new PropertyMetadata(null,
                (o, e) =>
                {
                    ((ParagraphSettingsPreviewAdapter)o).OnPropertyChanged(InputParagraphSettingsPropertyName, e.OldValue, e.NewValue);
                }));

        internal const string InputParagraphSettingsPropertyName = "InputParagraphSettings";

        public ParagraphSettings InputParagraphSettings
        {
            get
            {
                return (ParagraphSettings)this.GetValue(ParagraphSettingsPreviewAdapter.InputParagraphSettingsProperty);
            }
            set
            {
                this.SetValue(ParagraphSettingsPreviewAdapter.InputParagraphSettingsProperty, value);
            }
        }

        #endregion //InputParagraphSettings

        #region PreviewParagraphIndex

        public static readonly DependencyProperty PreviewParagraphIndexProperty = DependencyProperty.Register(PreviewParagraphIndexPropertyName,
            typeof(int), typeof(ParagraphSettingsPreviewAdapter),
            new PropertyMetadata(-1,
                (o, e) =>
                {
                    ((ParagraphSettingsPreviewAdapter)o).OnPropertyChanged(PreviewParagraphIndexPropertyName, e.OldValue, e.NewValue);
                }));

        internal const string PreviewParagraphIndexPropertyName = "PreviewParagraphIndex";

        public int PreviewParagraphIndex
        {
            get
            {
                return (int)this.GetValue(ParagraphSettingsPreviewAdapter.PreviewParagraphIndexProperty);
            }
            set
            {
                this.SetValue(ParagraphSettingsPreviewAdapter.PreviewParagraphIndexProperty, value);
            }
        }

        #endregion //PreviewParagraphIndex

        #region ParagraphSettingsPreviewAdapter_PropertyChanged

        void ParagraphSettingsPreviewAdapter_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Alignment": previewParagraphSettings.ParagraphAlignment = this.alignment;
                    break;
                case "IndentationBeforeText":
                    if (previewParagraphSettings.Indentation == null)
                        previewParagraphSettings.Indentation = new ParagraphIndentationSettings();

                    previewParagraphSettings.Indentation.Start = new Indentation(new Extent(this.indentationBeforeText, ExtentUnitType.Centimeters));

                    break;
                case "IndentationAfterText":
                    if (previewParagraphSettings.Indentation == null)
                        previewParagraphSettings.Indentation = new ParagraphIndentationSettings();

                    previewParagraphSettings.Indentation.End = new Indentation(new Extent(this.indentationAfterText, ExtentUnitType.Centimeters));
                    break;

                case "SpacingBefore":
                    if (previewParagraphSettings.Spacing == null)
                        previewParagraphSettings.Spacing = new ParagraphSpacingSettings();

                    previewParagraphSettings.Spacing.BeforeParagraph = new ParagraphVerticalSpacing(new Extent(this.spacingBefore, ExtentUnitType.Points));
                    break;
                case "SpacingAfter":
                    if (previewParagraphSettings.Spacing == null)
                        previewParagraphSettings.Spacing = new ParagraphSpacingSettings();

                    previewParagraphSettings.Spacing.AfterParagraph = new ParagraphVerticalSpacing(new Extent(this.spacingAfter, ExtentUnitType.Points));
                    break;
                case "FirstLineType":
                    if (FirstLineType != FirstLineType.None)
                    {
                        if (previewParagraphSettings.Indentation == null)
                            previewParagraphSettings.Indentation = new ParagraphIndentationSettings();

                        PropertyChanged -= ParagraphSettingsPreviewAdapter_PropertyChanged;
                        FirstLineIndent = 1.27;
                        PropertyChanged += ParagraphSettingsPreviewAdapter_PropertyChanged;

                        var isHanging = FirstLineType == FirstLineType.Hanging;
                        previewParagraphSettings.Indentation.FirstLine = new FirstLineIndentation(new Indentation(new Extent(FirstLineIndent, ExtentUnitType.Centimeters)), isHanging);
                    }
                    else
                    {
                        if (previewParagraphSettings.Indentation == null)
                            previewParagraphSettings.Indentation = new ParagraphIndentationSettings();

                        previewParagraphSettings.Indentation.FirstLine = new FirstLineIndentation();
                        PropertyChanged -= ParagraphSettingsPreviewAdapter_PropertyChanged;
                        FirstLineIndent = 0;
                        PropertyChanged += ParagraphSettingsPreviewAdapter_PropertyChanged;
                    }
                    break;

                case "FirstLineIndent":
                    if (previewParagraphSettings.Indentation == null)
                        previewParagraphSettings.Indentation = new ParagraphIndentationSettings();

                    if (FirstLineType == FirstLineType.None)
                    {
                        PropertyChanged -= ParagraphSettingsPreviewAdapter_PropertyChanged;
                        FirstLineType = FirstLineType.FirstLine;
                        PropertyChanged += ParagraphSettingsPreviewAdapter_PropertyChanged;
                    }

                    var hanging = FirstLineType == FirstLineType.Hanging;
                    previewParagraphSettings.Indentation.FirstLine = new FirstLineIndentation(new Indentation(new Extent(FirstLineIndent, ExtentUnitType.Centimeters)), hanging);
                    break;

                case "LineSpacingType":
                    if (previewParagraphSettings.Spacing == null)
                        previewParagraphSettings.Spacing = new ParagraphSpacingSettings();

                    switch (LineSpacingType)
                    {
                        case Infrastructure.LineSpacingTypes.AtLeast:
                        case Infrastructure.LineSpacingTypes.Exactly:
                            PropertyChanged -= ParagraphSettingsPreviewAdapter_PropertyChanged;
                            LineSpacingMask = "nn.nn pt";
                            PropertyChanged += ParagraphSettingsPreviewAdapter_PropertyChanged;
                            ExtentRule rule = LineSpacingType == Infrastructure.LineSpacingTypes.Exactly ? ExtentRule.Exact : ExtentRule.AtLeast;
                            //  LineSpacing = (float)LineSpacing * 13.5f;
                            previewParagraphSettings.Spacing.LineSpacing = new LineSpacing(new Extent(LineSpacing, ExtentUnitType.Points), rule);
                            break;
                        case Infrastructure.LineSpacingTypes.Single:
                        case Infrastructure.LineSpacingTypes.Double:
                            PropertyChanged -= ParagraphSettingsPreviewAdapter_PropertyChanged;
                            LineSpacingMask = "nn.nn";
                            PropertyChanged += ParagraphSettingsPreviewAdapter_PropertyChanged;
                            float factor = LineSpacingType == Infrastructure.LineSpacingTypes.Single ? 1 : 2;
                            LineSpacing = factor;
                            previewParagraphSettings.Spacing.LineSpacing = new LineSpacing(factor);
                            break;

                        case Infrastructure.LineSpacingTypes.OnePointFiveLines:
                            PropertyChanged -= ParagraphSettingsPreviewAdapter_PropertyChanged;
                            LineSpacingMask = "nn.nn";
                            LineSpacing = 1.5f;
                            PropertyChanged += ParagraphSettingsPreviewAdapter_PropertyChanged;
                            previewParagraphSettings.Spacing.LineSpacing = new LineSpacing(LineSpacing);
                            break;
                        case Infrastructure.LineSpacingTypes.Multiple:
                            PropertyChanged -= ParagraphSettingsPreviewAdapter_PropertyChanged;
                            LineSpacingMask = "nn.nn";
                            PropertyChanged += ParagraphSettingsPreviewAdapter_PropertyChanged;
                            previewParagraphSettings.Spacing.LineSpacing = new LineSpacing(LineSpacing);
                            break;
                    }

                    break;

                case "LineSpacing":
                    if (previewParagraphSettings.Spacing == null)
                        previewParagraphSettings.Spacing = new ParagraphSpacingSettings();

                    if (previewParagraphSettings.Spacing.LineSpacing == null)
                        previewParagraphSettings.Spacing.LineSpacing = new LineSpacing();

                    if (LineSpacing == 1)
                    {
                        PropertyChanged -= ParagraphSettingsPreviewAdapter_PropertyChanged;

                        if (LineSpacingType != Infrastructure.LineSpacingTypes.Single)
                            LineSpacingType = Infrastructure.LineSpacingTypes.Single;

                        LineSpacingMask = "nn.nn";
                        previewParagraphSettings.Spacing.LineSpacing = new LineSpacing(LineSpacing);

                        PropertyChanged += ParagraphSettingsPreviewAdapter_PropertyChanged;
                    }
                    else if (LineSpacing == 1.5)
                    {
                        PropertyChanged -= ParagraphSettingsPreviewAdapter_PropertyChanged;

                        if (LineSpacingType != Infrastructure.LineSpacingTypes.OnePointFiveLines)
                            LineSpacingType = Infrastructure.LineSpacingTypes.OnePointFiveLines;

                        LineSpacingMask = "nn.nn";
                        previewParagraphSettings.Spacing.LineSpacing = new LineSpacing(LineSpacing);

                        PropertyChanged += ParagraphSettingsPreviewAdapter_PropertyChanged;
                    }
                    else if (LineSpacing == 2)
                    {
                        PropertyChanged -= ParagraphSettingsPreviewAdapter_PropertyChanged;

                        if (LineSpacingType != Infrastructure.LineSpacingTypes.Double)
                            LineSpacingType = Infrastructure.LineSpacingTypes.Double;

                        LineSpacingMask = "nn.nn";
                        previewParagraphSettings.Spacing.LineSpacing = new LineSpacing(LineSpacing);

                        PropertyChanged += ParagraphSettingsPreviewAdapter_PropertyChanged;
                    }
                    else
                    {
                        if (LineSpacingType == Infrastructure.LineSpacingTypes.Exactly || LineSpacingType == Infrastructure.LineSpacingTypes.Exactly)
                        {
                            PropertyChanged -= ParagraphSettingsPreviewAdapter_PropertyChanged;
                            LineSpacingMask = "nn.nn pt";
                            ExtentRule rule = LineSpacingType == Infrastructure.LineSpacingTypes.Exactly ? ExtentRule.Exact : ExtentRule.AtLeast;
                            previewParagraphSettings.Spacing.LineSpacing = new LineSpacing(new Extent(LineSpacing, ExtentUnitType.Points), rule);
                            PropertyChanged += ParagraphSettingsPreviewAdapter_PropertyChanged;
                        }
                        else
                        {
                            if (LineSpacingType != Infrastructure.LineSpacingTypes.Multiple)
                                LineSpacingType = Infrastructure.LineSpacingTypes.Multiple;

                            PropertyChanged -= ParagraphSettingsPreviewAdapter_PropertyChanged;
                            LineSpacingMask = "nn.nn";
                            previewParagraphSettings.Spacing.LineSpacing = new LineSpacing(LineSpacing);
                            PropertyChanged += ParagraphSettingsPreviewAdapter_PropertyChanged;
                        }

                    }
                    break;
            }

            string error;

            if (previewParagraphSettings != null)
                Editor.Document.ApplyParagraphSettings(previewParagraphIndexSpan, previewParagraphSettings, out error);
        }

        #endregion //ParagraphSettingsPreviewAdapter_PropertyChanged

        #region OnPropertyChanged

        private void OnPropertyChanged(string propertyName, object oldValue, object newValue)
        {
            switch (propertyName)
            {
                case EditorPropertyName: (newValue as XamRichTextEditor).DocumentContentChanged += ParagraphSettingsPreviewAdapter_DocumentContentChanged; break;
                case InputParagraphSettingsPropertyName: ReadInputParagraphSettings(newValue as ParagraphSettings); break;
            }
        }

        void ParagraphSettingsPreviewAdapter_DocumentContentChanged(object sender, DocumentContentChangedEventArgs e)
        {
            string error;

            var index = PreviewParagraphIndex;

            if (index == -1)
            {
                previewParagraphIndexSpan = DocumentSpan.All;
            }
            else
            {
                var previewParagrapth = Editor.Document.GetParagraphs(DocumentSpan.All).ElementAtOrDefault(PreviewParagraphIndex);

                if (previewParagrapth != null)
                    previewParagraphIndexSpan = previewParagrapth.GetDocumentSpan();

            }

            if (previewParagraphSettings != null)
            {
                Editor.Document.ApplyParagraphSettings(previewParagraphIndexSpan, previewParagraphSettings, out error);
            }
        }

        #endregion //OnPropertyChanged

        #region ReadInputParagraphSettings

        private void ReadInputParagraphSettings(ParagraphSettings paragraphSettings)
        {
            if (paragraphSettings != null)
            {
                PropertyChanged -= ParagraphSettingsPreviewAdapter_PropertyChanged;

                previewParagraphSettings = paragraphSettings;
                // Editor.Document.ApplyParagraphSettings(DocumentSpan.All, previewParagraphSettings, out error);

                this.Alignment = previewParagraphSettings.ParagraphAlignment;

                if (previewParagraphSettings.Indentation != null)
                {
                    if (previewParagraphSettings.Indentation.Start != null)
                        this.IndentationBeforeText = previewParagraphSettings.Indentation.Start.Value.Extent.Value.Centimeters;

                    if (previewParagraphSettings.Indentation.End != null)
                        this.IndentationAfterText = previewParagraphSettings.Indentation.End.Value.Extent.Value.Centimeters;

                    if (previewParagraphSettings.Indentation.FirstLine.HasValue)
                    {
                        if (previewParagraphSettings.Indentation.FirstLine.Value.IsHanging)
                            FirstLineType = Infrastructure.FirstLineType.Hanging;
                        else
                            FirstLineType = Infrastructure.FirstLineType.FirstLine;

                        FirstLineIndent = previewParagraphSettings.Indentation.FirstLine.Value.Indentation.Extent.Value.Centimeters;
                    }
                    else
                    {
                        FirstLineType = Infrastructure.FirstLineType.None;
                    }
                }

                if (previewParagraphSettings.Spacing != null)
                {
                    if (previewParagraphSettings.Spacing.BeforeParagraph != null)
                        this.SpacingBefore = previewParagraphSettings.Spacing.BeforeParagraph.Value.Extent.Value.Points;

                    if (previewParagraphSettings.Spacing.AfterParagraph != null)
                        this.SpacingAfter = previewParagraphSettings.Spacing.AfterParagraph.Value.Extent.Value.Points;

                    if (previewParagraphSettings.Spacing.LineSpacing != null)
                    {
                        var lineSpacingValue = previewParagraphSettings.Spacing.LineSpacing.Value;

                        if (lineSpacingValue.LineFactor.HasValue)
                        {
                            if (lineSpacingValue.LineFactor == 1)
                            {
                                LineSpacingType = Infrastructure.LineSpacingTypes.Single;
                            }
                            else if (lineSpacingValue.LineFactor == 1.5)
                            {
                                LineSpacingType = Infrastructure.LineSpacingTypes.OnePointFiveLines;
                            }
                            else if (lineSpacingValue.LineFactor == 2)
                            {
                                LineSpacingType = Infrastructure.LineSpacingTypes.Double;
                            }
                            else
                            {
                                LineSpacingType = Infrastructure.LineSpacingTypes.Multiple;
                            }

                            LineSpacing = lineSpacingValue.LineFactor.Value;
                            LineSpacingMask = "nn.nn";
                        }
                        else if (lineSpacingValue.Extent.HasValue)
                        {
                            if (lineSpacingValue.Rule == ExtentRule.AtLeast)
                            {
                                LineSpacingType = Infrastructure.LineSpacingTypes.AtLeast;
                            }
                            else if (lineSpacingValue.Rule == ExtentRule.Exact)
                            {
                                LineSpacingType = Infrastructure.LineSpacingTypes.Exactly;
                            }

                            LineSpacing = (float)lineSpacingValue.Extent.Value.Points;
                            LineSpacingMask = "nn.nn pt";
                        }
                    }

                }

                PropertyChanged += ParagraphSettingsPreviewAdapter_PropertyChanged;
            }
        }

        #endregion //ReadInputParagraphSettings

        #region ParagraphSettings Properties

        private double spacingBefore;
        public double SpacingBefore
        {
            get { return spacingBefore; }
            set { spacingBefore = value; NotifyPropertyChanged(); }
        }

        private double spacingAfter;
        public double SpacingAfter
        {
            get { return spacingAfter; }
            set { spacingAfter = value; NotifyPropertyChanged(); }
        }

        public Array FirstLineTypes { get { return Enum.GetValues(typeof(FirstLineType)); } }
        public Array ParagraphAlignments { get { return new ParagraphAlignment[] { ParagraphAlignment.Start, ParagraphAlignment.End, ParagraphAlignment.Justify, ParagraphAlignment.Center }; } }

        private FirstLineType firstLineType;
        public FirstLineType FirstLineType
        {
            get { return firstLineType; }
            set { firstLineType = value; NotifyPropertyChanged(); }
        }

        private double firstLineIndent;
        public double FirstLineIndent
        {
            get { return firstLineIndent; }
            set { firstLineIndent = value; NotifyPropertyChanged(); }
        }

        private double indentationAfterText;
        public double IndentationAfterText
        {
            get { return indentationAfterText; }
            set { indentationAfterText = value; NotifyPropertyChanged(); }
        }

        private double indentationBeforeText;
        public double IndentationBeforeText
        {
            get { return indentationBeforeText; }
            set { indentationBeforeText = value; NotifyPropertyChanged(); }
        }

        private ParagraphAlignment? alignment;
        public ParagraphAlignment? Alignment
        {
            get { return alignment; }
            set { alignment = value; NotifyPropertyChanged(); }
        }

        public Array LineSpacingTypes { get { return Enum.GetValues(typeof(LineSpacingTypes)); } }

        private LineSpacingTypes lineSpacingType;
        public LineSpacingTypes LineSpacingType
        {
            get { return lineSpacingType; }
            set { lineSpacingType = value; NotifyPropertyChanged(); }
        }

        private float lineSpacing;
        public float LineSpacing
        {
            get { return lineSpacing; }
            set { lineSpacing = value; NotifyPropertyChanged(); }
        }

        private string lineSpacingMask;
        public string LineSpacingMask
        {
            get { return lineSpacingMask; }
            set { lineSpacingMask = value; NotifyPropertyChanged(); }
        }
        #endregion //ParagraphSettings Properties

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
