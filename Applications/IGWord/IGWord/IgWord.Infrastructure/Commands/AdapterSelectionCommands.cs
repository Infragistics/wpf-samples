using IgWord.Infrastructure.Adapters;
using Infragistics.Documents.RichText;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using System.Windows.Media;

namespace IgWord.Infrastructure.Commands
{
    #region AdapterSelectionCommandBase
    internal abstract class AdapterSelectionCommandBase : ICommand
    {
        protected readonly RichTextEditorSelectionAdapter Adapter;

        internal AdapterSelectionCommandBase(RichTextEditorSelectionAdapter adapter)
        {
            this.Adapter = adapter;
        }

        public virtual bool CanExecute(object parameter)
        {
            var editor = this.Adapter.Editor;
            return editor != null
                && editor.Selection != null
                && editor.Document.IsReadOnly == false;
        }

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            var handler = this.CanExecuteChanged;

            if (null != handler)
                handler(this, EventArgs.Empty);
        }

        public abstract void Execute(object parameter);
    }
    #endregion //AdapterSelectionCommandBase

    #region AdapterFontColorCommand
    internal class AdapterFontColorCommand : AdapterSelectionCommandBase
    {
        internal AdapterFontColorCommand(RichTextEditorSelectionAdapter adapter)
            : base(adapter)
        {
        }

        public override void Execute(object parameter)
        {
            if (parameter == null)
                return;

            var color = (Color)parameter;

            ColorInfo colorInfo = null;

            if (color.A == 0 && color != Colors.Transparent)
                return;

            if (color == Colors.Transparent)
                colorInfo = ColorInfo.Automatic;
            else
                colorInfo = new ColorInfo(color);

            var editor = this.Adapter.Editor;
            var selection = editor.Selection;

            if (selection != null)
            {
                selection.ApplyTextForecolor(colorInfo);
            }
        }
    }
    #endregion //AdapterFontColorCommand

    #region AdapterTextHighlightColorCommand
    internal class AdapterTextHighlightColorCommand : AdapterSelectionCommandBase
    {
        internal AdapterTextHighlightColorCommand(RichTextEditorSelectionAdapter adapter)
            : base(adapter)
        {
        }

        public override void Execute(object parameter)
        {
            if (parameter == null)
                return;

            if (!Adapter.ColorToHighlightColorTable.ContainsKey((Color)parameter))
                return;

            var highlightColor = Adapter.ColorToHighlightColorTable[(Color)parameter];

            var editor = this.Adapter.Editor;
            var selection = editor.Selection;

            if (selection != null)
            {
                selection.ApplyTextHighlightColor(highlightColor);
            }
        }
    }
    #endregion //AdapterTextHighlightColorCommand

    #region AdapterShadingCommand
    internal class AdapterShadingCommand : AdapterSelectionCommandBase
    {
        internal AdapterShadingCommand(RichTextEditorSelectionAdapter adapter)
            : base(adapter)
        {
        }

        public override void Execute(object parameter)
        {
            if (parameter == null)
                return;

            var color = (Color)parameter;

            Shading shading;

            if (color.A == 0 && color != Colors.Transparent)
                return;

            if (color == Colors.Transparent)
            {
                shading = new Shading(ColorInfo.Automatic);
            }
            else
            {
                shading = new Shading(color);
            }

            var editor = this.Adapter.Editor;
            var selection = editor.Selection;

            if (selection != null)
            {
                selection.ApplyParagraphShading(shading);
            }
        }
    }
    #endregion //AdapterShadingCommand

    #region AdapterApplyListStyleCommand
    internal class AdapterApplyListStyleCommand : AdapterSelectionCommandBase
    {
        internal AdapterApplyListStyleCommand(RichTextEditorSelectionAdapter adapter)
            : base(adapter)
        {
        }

        public override void Execute(object parameter)
        {
            if (parameter == null)
                return;

            if (parameter is BulletListStyles || parameter is NumberListStyles)
            {
                var editor = this.Adapter.Editor;
                var selection = editor.Selection;

                if (selection != null)
                {
                    selection.ApplyParagraphListStyle(parameter.ToString());
                }
            }
        }
    }
    #endregion //AdapterApplyListStyleCommand

    #region AdapterApplyUnderlineCommand
    internal class AdapterApplyUnderlineCommand : AdapterSelectionCommandBase
    {
        internal AdapterApplyUnderlineCommand(RichTextEditorSelectionAdapter adapter)
            : base(adapter)
        {
        }

        public override void Execute(object parameter)
        {
            if (parameter == null)
                return;

            UnderlineType value;

            if (parameter is string)
                value = (UnderlineType)Enum.Parse(typeof(UnderlineType), (string)parameter, true);
            else
                value = (UnderlineType)Enum.ToObject(typeof(UnderlineType), parameter);

            this.Adapter.LastAppliedUnderline = value;

            var editor = this.Adapter.Editor;
            var selection = editor.Selection;

            if (selection != null)
            {
                selection.ApplyFontUnderlineFormatting(value);
            }
        }
    }
    #endregion //AdapterApplyUnderlineCommand

    #region AdapterToggleUnderlineCommand
    internal class AdapterToggleUnderlineCommand : AdapterSelectionCommandBase
    {
        internal AdapterToggleUnderlineCommand(RichTextEditorSelectionAdapter adapter)
            : base(adapter)
        {
        }

        public override void Execute(object parameter)
        {
            if (parameter == null)
                return;

            UnderlineType value;

            if (parameter is string)
                value = (UnderlineType)Enum.Parse(typeof(UnderlineType), (string)parameter, true);
            else
                value = (UnderlineType)Enum.ToObject(typeof(UnderlineType), parameter);

            this.Adapter.LastAppliedUnderline = value;

            var editor = this.Adapter.Editor;
            var selection = editor.Selection;

            if (selection != null)
            {
                if (this.Adapter.IsUnderline)
                    value = UnderlineType.None;

                selection.ApplyFontUnderlineFormatting(value);
            }
        }
    }
    #endregion //AdapterToggleUnderlineCommand

    #region AdapterFontSizeCommand
    internal class AdapterFontSizeCommand : AdapterSelectionCommandBase
    {
        internal AdapterFontSizeCommand(RichTextEditorSelectionAdapter adapter)
            : base(adapter)
        {
        }

        public override void Execute(object parameter)
        {
            if (parameter == null)
                return;

            double value;

            if (parameter is string)
                value = Double.Parse((string)parameter);
            else
                value = (double)Convert.ToDouble(parameter);

            var editor = this.Adapter.Editor;
            var selection = editor.Selection;

            if (selection != null)
            {
                selection.ApplyFontSize(value);
            }
        }
    }
    #endregion //AdapterFontSizeCommand

    #region AdapterClearAllFormatingCommand
    internal class AdapterClearAllFormatingCommand : AdapterSelectionCommandBase
    {
        internal AdapterClearAllFormatingCommand(RichTextEditorSelectionAdapter adapter)
            : base(adapter)
        {
        }

        public override void Execute(object parameter)
        {
            var selection = this.Adapter.Editor.Selection;
            var charSettings = this.Adapter.Editor.Document.GetCommonCharacterSettings(selection.DocumentSpan);

            var defaultSettings = new CharacterSettings
            {
                FontSize = new Extent(11, ExtentUnitType.Points),
                Color = ColorInfo.Automatic,
                Bold = false,
                Italics = false,
                UnderlineType = UnderlineType.None,
                FontSettings = new FontSettings { Ascii = new RichTextFont("Calibri"), },
                SmallCaps = false,
                AllCaps = false,
                StrikeThrough = false,
                UnderlineColor = ColorInfo.Automatic,
                VerticalAlignment = RunVerticalAlignment.Baseline
            };

            if (selection != null && charSettings != null)
            {
                string error;
                this.Adapter.Editor.Document.ApplyCharacterSettings(selection.DocumentSpan, defaultSettings, out error);

                if (selection.Paragraphs != null)
                {
                    foreach (var paragraph in selection.Paragraphs)
                    {
                        this.Adapter.Editor.Document.ClearParagraphListStyle(paragraph.GetDocumentSpan(), out error);
                    }
                }
            }
        }
    }
    #endregion //AdapterClearAllFormatingCommand

    #region AdapterUpperCaseCommand
    internal class AdapterUpperCaseCommand : AdapterSelectionCommandBase
    {
        internal AdapterUpperCaseCommand(RichTextEditorSelectionAdapter adapter)
            : base(adapter)
        {
        }

        public override void Execute(object parameter)
        {
            var selection = this.Adapter.Editor.Selection;

            if (selection != null)
            {
                List<DocumentSpan> selectedSpans = new List<DocumentSpan>();

                foreach (var range in selection.Ranges)
                {
                    selectedSpans.Add(range.DocumentSpan);
                }

                this.Adapter.Editor.Document.UndoManager.StartTransaction("UpperCase", "UpperCase");

                IEnumerable<RunNode> runs;
                foreach (DocumentSpan selectedSpan in selectedSpans)
                {
                    runs = this.Adapter.Editor.Document.GetRuns(selectedSpan);
                    foreach (RunNode runNode in runs)
                    {
                        runNode.SetText(
                            AdapterCommandsHelper.CharacterCaseSwitchingHelper(
                                runNode.GetText(),
                                runNode.GetDocumentOffset(),
                                selectedSpan,
                                true)
                            );
                    }
                }

                this.Adapter.Editor.Document.UndoManager.CurrentTransaction.Commit();

                selection.UpdateSelectionWithSpans(selectedSpans);
            }
        }
    }
    #endregion //AdapterUpperCaseCommand

    #region AdapterLowerCaseCommand
    internal class AdapterLowerCaseCommand : AdapterSelectionCommandBase
    {
        internal AdapterLowerCaseCommand(RichTextEditorSelectionAdapter adapter)
            : base(adapter)
        {
        }

        public override void Execute(object parameter)
        {
            var selection = this.Adapter.Editor.Selection;

            if (selection != null)
            {
                List<DocumentSpan> selectedSpans = new List<DocumentSpan>();

                foreach (var range in selection.Ranges)
                {
                    selectedSpans.Add(range.DocumentSpan);
                }

                this.Adapter.Editor.Document.UndoManager.StartTransaction("LowerCase", "LowerCase");

                IEnumerable<RunNode> runs;
                foreach (DocumentSpan selectedSpan in selectedSpans)
                {
                    runs = this.Adapter.Editor.Document.GetRuns(selectedSpan);
                    foreach (RunNode runNode in runs)
                    {
                        runNode.SetText(
                            AdapterCommandsHelper.CharacterCaseSwitchingHelper(
                                runNode.GetText(),
                                runNode.GetDocumentOffset(),
                                selectedSpan,
                                false)
                            );
                    }
                }

                this.Adapter.Editor.Document.UndoManager.CurrentTransaction.Commit();

                selection.UpdateSelectionWithSpans(selectedSpans);
            }
        }
    }
    #endregion //AdapterLowerCaseCommand

    #region AdapterToggleCaseCommand
    internal class AdapterToggleCaseCommand : AdapterSelectionCommandBase
    {
        internal AdapterToggleCaseCommand(RichTextEditorSelectionAdapter adapter)
            : base(adapter)
        {
        }

        public override void Execute(object parameter)
        {
            var selection = this.Adapter.Editor.Selection;

            if (selection != null)
            {
                List<DocumentSpan> selectedSpans = new List<DocumentSpan>();

                foreach (Range range in selection.Ranges)
                {
                    selectedSpans.Add(range.DocumentSpan);
                }

                this.Adapter.Editor.Document.UndoManager.StartTransaction("ToggleCase", "ToggleCase");

                IEnumerable<RunNode> runs;
                foreach (DocumentSpan selectedSpan in selectedSpans)
                {
                    runs = this.Adapter.Editor.Document.GetRuns(selectedSpan);
                    foreach (RunNode runNode in runs)
                    {
                        runNode.SetText(
                            AdapterCommandsHelper.CharacterCaseSwitchingHelper(
                                runNode.GetText(),
                                runNode.GetDocumentOffset(),
                                selectedSpan,
                                null)
                            );
                    }
                }

                this.Adapter.Editor.Document.UndoManager.CurrentTransaction.Commit();

                selection.UpdateSelectionWithSpans(selectedSpans);
            }
        }
    }
    #endregion //AdapterToggleCaseCommand

    #region AdapterLineSpacingCommand
    internal class AdapterLineSpacingCommand : AdapterSelectionCommandBase
    {
        internal AdapterLineSpacingCommand(RichTextEditorSelectionAdapter adapter)
            : base(adapter)
        {
        }

        public override void Execute(object parameter)
        {
            if (parameter == null)
                return;

            double value;

            if (parameter is string)
                value = Double.Parse((string)parameter);
            else
                value = (double)Convert.ToDouble(parameter);

            var selection = this.Adapter.Editor.Selection;

            if (selection != null)
            {
                string error;
                this.Adapter.Editor.Document.ApplyParagraphSettings(selection.DocumentSpan,
                    new ParagraphSettings()
                    {
                        Spacing = new ParagraphSpacingSettings()
                        {
                            LineSpacing = new LineSpacing((float)value)
                        }
                    }, out error);
            }
        }
    }
    #endregion //AdapterLineSpacingCommand

    #region AdapterCommandsHelper
    internal static class AdapterCommandsHelper
    {
        //
        // Summary:
        //     Will either:
        //      - toggle current case
        //      - change to upper case
        //      - change to lower case
        //     of the characters of a given string.
        //
        // Parameters:
        //   inputStr:
        //     The string whose characters' case should be changed.
        //
        //   initialInputStringOffset:
        //     The offset of the first character of the given string.
        //
        //   selectionSpan:
        //     The document span which (partially) contains the given string.
        //
        //   operation:
        //     null  - toggle current case
        //     true  - change to upper case
        //     false - change to lower case
        //
        // Returns:
        //     The result string after chaning its character case according to the specified operation.
        //
        internal static string CharacterCaseSwitchingHelper(string inputStr, int initialInputStringOffset, DocumentSpan selectionSpan, bool? operation)
        {
            StringBuilder builder = new StringBuilder(inputStr.Length);
            int offsetInRun = initialInputStringOffset;

            foreach (char c in inputStr)
            {
                // check if the offset of the current string character is intersecting with the selection's current document span
                // (a selection may consists of more than one document span)
                if (selectionSpan.Contains(offsetInRun))
                {
                    // if current character is intersecting with the document span -> execute the operation over that character
                    if (!operation.HasValue)
                    {
                        builder.Append(char.IsUpper(c) ? char.ToLower(c) : char.ToUpper(c));
                    }
                    else if (operation.Value)
                    {
                        builder.Append(char.ToUpper(c));
                    }
                    else
                    {
                        builder.Append(char.ToLower(c));
                    }
                }
                else
                {
                    // if current character is not intersecting with the document span -> don't do anything
                    builder.Append(c);
                }
                offsetInRun++;
            }

            return builder.ToString();
        }
    }
    #endregion AdapterCommandsHelper

}
