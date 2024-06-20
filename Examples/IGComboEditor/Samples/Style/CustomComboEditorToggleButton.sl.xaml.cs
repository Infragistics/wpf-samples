using Infragistics.Controls.Editors;
using Infragistics.Samples.Framework;
using System.Threading;
using System.Windows;

namespace IGComboEditor.Samples.Style
{
    public partial class CustomComboEditorToggleButton : SampleContainer
    {
        public CustomComboEditorToggleButton()
        {
            InitializeComponent();
            Loaded += OnSampleLoaded;
        }

        private void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            JPCustomValueEnteredActionCheck();
        }

        private void JPCustomValueEnteredActionCheck()
        {
            string isoLanguage = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

            // customization
            if (isoLanguage.ToLower().Equals("ja"))
            {
                ComboEditor.CustomValueEnteredAction = CustomValueEnteredActions.Allow;
            }
        }
    }
}
