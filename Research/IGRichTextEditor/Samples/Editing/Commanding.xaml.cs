using System;
using System.Collections;
using System.IO;
using IGRichTextEditor.Resources;
using IGRichTextEditor.Samples.Helpers;
using Infragistics;
using Infragistics.Controls.Editors;
using Infragistics.Controls.Editors.Primitives;
using Infragistics.Documents.RichText;
using Infragistics.Samples.Framework;

namespace IGRichTextEditor.Samples.Editing
{
    public partial class Commanding : SampleContainer
    {
        public Commanding()
        {
            InitializeComponent();
            InitializeSample();
        }

        void InitializeSample()
        {
            Array values = Enum.GetValues(typeof(RichTextEditorCommandType));
            Array.Sort(values, new RichTextEditorCommandTypeComparer());
            foreach (RichTextEditorCommandType i in values)
            {
                this.CmdCombo.Items.Add(i);
            }
            this.CmdCombo.SelectedIndex = 0;

            Stream fs = FilesProvider.GetStreamForFile(RichTextEditorStrings.fileDocxRichContent);
            RichTextDocument doc = new RichTextDocument();
            doc.LoadFromWord(fs);
            this.xamRichTextEditor1.Document = doc;
        }

        private void cmdCombo_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            this.seCmdSrc.CommandType = (RichTextEditorCommandType)this.CmdCombo.SelectedValue;
            CommandSourceManager.NotifyCanExecuteChanged(this.seCmdSrc.Command.GetType());
        }

        private void ExeCmd_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // use the following lines to invoke a command using code
            //var cmdType = (RichTextEditorCommandType)this.CmdCombo.SelectedValue;
            //var command = new RichTextEditorCommand(cmdType);
            //command.Execute(this.xamRichTextEditor1);

            this.xamRichTextEditor1.Focus();
        }
    }

    public class RichTextEditorCommandTypeComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            if (x is RichTextEditorCommandType && y is RichTextEditorCommandType)
            {
                string sx = ((RichTextEditorCommandType)x).ToString();
                string sy = ((RichTextEditorCommandType)y).ToString();
                return string.Compare(sx, sy);
            }
            return 0;
        }
    }
}
