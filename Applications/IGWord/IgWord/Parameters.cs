using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IgWord
{
    public class FindReplaceDialogParameters
    {
        public const string DataItemKey = "DataItem";
        public const string DialogModeKey = "DialogMode";

        public const string FindMode = "Find";
        public const string ReplaceMode = "Replace";
    }

    public class HyperlinkDialogParameters
    {
        public const string DocumentKey = "Document";
        public const string SelectionKey = "Selection";
    }

    public class InsertTableDialogParameters
    {
        public const string DocumentKey = "Document";
        public const string SelectionKey = "Selection";
    }

    public class FontDialogParameters
    {
        public const string CharacterSettingsKey = "CharacterSettings";
    }

    public class ParagraphDialogParameters
    {
        public const string ParagraphSettingsKey = "ParagraphSettings";
    }

    public class ShellParameters
    {
        public const string NewTabName = "newTab";
        public const string SaveAsTabName = "saveAsTab";
        public const string FileDialogWordFilter = "Word Document (*.docx)|*.docx|Rich Text File (*.rtf)|*.rtf|Web Page (*.html)|*.html|Plain Text (*.txt)|*.txt|All Files (*.*)|*.*";
        public const string FileDialogImageFilter = "JPEG File (*.jpg; *.jpeg)|*.jpg; *.jpeg|All Files (*.*)|*.*";
    }
}
