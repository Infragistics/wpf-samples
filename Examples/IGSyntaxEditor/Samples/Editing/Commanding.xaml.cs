using System;
using IGSyntaxEditor.Resources;
using Infragistics;
using Infragistics.Controls.Editors;
using Infragistics.Documents.Parsing;
using Infragistics.Samples.Framework;

namespace IGSyntaxEditor.Samples.Editing
{
    /// <summary>
    /// Interaction logic for Commanding.xaml
    /// </summary>
    public partial class Commanding : SampleContainer
    {
        public Commanding()
        {
            InitializeComponent();
            InitializeSample();
        }

        public void InitializeSample()
        {
            this.UseDefaultTheme = true;
            // Initialize a text document with C# language content in the xamSyntaxEditor
            this.xamSyntaxEditor1.Document.Language = CSharpLanguage.Instance;
            this.xamSyntaxEditor1.Document.InitializeText(SyntaxEditorStrings.SourceCS);

            Array values = Enum.GetValues(typeof(SyntaxEditorCommandType));
            foreach (SyntaxEditorCommandType i in values)
            {
                this.CmdCombo.Items.Add(i);
            }
            this.CmdCombo.SelectedIndex = 5;
        }

        private void cmdCombo_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            this.seCmdSrc.CommandType = (SyntaxEditorCommandType)this.CmdCombo.SelectedValue;
            CommandSourceManager.NotifyCanExecuteChanged(this.seCmdSrc.Command.GetType());
        }

        private void ExeCmd_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.xamSyntaxEditor1.Focus();
        }
    }
}

