using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Infragistics.Controls.Interactions;
using Infragistics.Samples.Framework;

namespace IGFormulaEditor.Samples.Display
{
    /// <summary>
    /// Interaction logic for FormulaEditorDialogSample.xaml
    /// </summary>
    public partial class FormulaEditorDialogSample : SampleContainer
    {
        FormulaEditorDialog formulaEditorDialog;

        public FormulaEditorDialogSample()
        {
            InitializeComponent();
            this.Loaded += FormulaEditorDialogSample_Loaded;
        }

        void FormulaEditorDialogSample_Loaded(object sender, RoutedEventArgs e)
        {           
            formulaEditorDialog = new FormulaEditorDialog()
            {              
                ShowDialogButtons = false,
                MaxHeight = 500
            };
            scrollViewer.Content = formulaEditorDialog;           
        }

        private void CommitButton_Click(object sender, RoutedEventArgs e)
        {
            this.formulaEditorDialog.CommitEdit();
            this.grdPanel.Opacity = 0;
        }

        // Through the following EventHandlers the displaying of the default FormulaEditorDialog
        // is cancelled and the custom formulaEditorDialog is displayed instead.
        private void FormulaEditor1_FormulaEditorDialogDisplaying(object sender, FormulaEditorDialogDisplayingEventArgs e)
        {
            e.Cancel = true;
            this.formulaEditorDialog.Target = this.Result1;
            this.grdPanel.Opacity = 100;
        }

        private void FormulaEditor2_FormulaEditorDialogDisplaying(object sender, FormulaEditorDialogDisplayingEventArgs e)
        {
            e.Cancel = true;
            this.formulaEditorDialog.Target = this.Result2;
            this.grdPanel.Opacity = 100;
        }
    }
}
