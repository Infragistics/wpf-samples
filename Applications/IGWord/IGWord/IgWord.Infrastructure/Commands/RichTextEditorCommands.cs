using Infragistics.Controls.Editors;
using System.Windows.Input;

namespace IgWord.Infrastructure.Commands
{
    public static class RichTextEditorCommands
    {
        public static RoutedUICommand ToggleSplitEditArea { get; private set; }
        public static RoutedUICommand ToggleHiddenSymbolDisplayMode { get; private set; }

        static RichTextEditorCommands()
        {
            ToggleSplitEditArea = new RoutedUICommand("ToggleSplitEditArea", "ToggleSplitEditArea", typeof(RichTextEditorCommands));
            ToggleHiddenSymbolDisplayMode = new RoutedUICommand("ToggleHiddenSymbolDisplayMode", "ToggleHiddenSymbolDisplayMode", typeof(RichTextEditorCommands));

            CommandManager.RegisterClassCommandBinding(typeof(XamRichTextEditor),
                new CommandBinding(ToggleSplitEditArea, ToggleSplitEditAreaExecuted));

            CommandManager.RegisterClassCommandBinding(typeof(XamRichTextEditor),
               new CommandBinding(ToggleHiddenSymbolDisplayMode, ToggleHiddenSymbolDisplayModeExecuted));
        }

        private static void ToggleHiddenSymbolDisplayModeExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var editor = sender as XamRichTextEditor;
         
            if (editor != null)
            {
                if (editor.HiddenSymbolDisplayMode == HiddenSymbolDisplayMode.DisplayAllHiddenSymbols)
                {
                    editor.HiddenSymbolDisplayMode = HiddenSymbolDisplayMode.DoNotDisplay;
                }
                else
                {
                    editor.HiddenSymbolDisplayMode = HiddenSymbolDisplayMode.DisplayAllHiddenSymbols;
                }
            }
        }

        private static void ToggleSplitEditAreaExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var editor = sender as XamRichTextEditor;
            
            if (editor != null)
            {
                editor.IsDocumentViewSplit = !editor.IsDocumentViewSplit;
            }
        }
    }
}
