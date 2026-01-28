using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Infragistics.Samples.Framework;

namespace IGRibbon.Samples.Editing
{
    public partial class InteractionContextualTabs : SampleContainer
    {
        public InteractionContextualTabs()
        {
            InitializeComponent();
        }
        #region Fields

        private bool isSelectionValid;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets/sets whether any text is currently selected in the associated RichTextBox 
        /// </summary>
        public bool IsSelectionValid
        {
            get
            {
                return this.isSelectionValid;
            }
            set
            {
                if (this.isSelectionValid != value)
                {
                    this.isSelectionValid = value;
                    this.xamRibbon.ContextualTabGroups[0].IsVisible = this.isSelectionValid;
                }
            }
        }

        #endregion Properties

        #region Event Handlers

        /// <summary>
        /// Called when the user selects any text in the associated RichTextBox.
        /// </summary>
        private void OnRichTextBoxSelectionChanged(object sender, RoutedEventArgs e)
        {
            TextSelection selection = this.richTextBox.Selection;
            this.underlineTool.IsChecked = selection.GetPropertyValue(Inline.TextDecorationsProperty) == TextDecorations.Underline;

            // We only want to allow selection-specific actions when there is actually something selected
            this.IsSelectionValid = selection.Start != selection.End;

            this.SynchronizeSelectionCombos();
        }

        #endregion Event Handlers

        #region Private Methods

        private void SynchronizeSelectionCombos()
        {
            this.fontFamilyCombo_Selection.SelectedItem =
                this.richTextBox.Selection.GetPropertyValue(RichTextBox.FontFamilyProperty);

            this.fontSizeCombo_Selection.SelectedItem =
                this.richTextBox.Selection.GetPropertyValue(RichTextBox.FontSizeProperty);
        }

        #endregion Private Methods
    }
}