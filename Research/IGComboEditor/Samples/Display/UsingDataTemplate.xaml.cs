using System.Threading;
using Infragistics.Controls.Editors;
using Infragistics.Samples.Framework;
using System.Windows;

namespace IGComboEditor.Samples.Display
{
    public partial class UsingDataTemplate : SampleContainer
    {
        public UsingDataTemplate()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        private void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            JPCustomValueEnteredActionCheck();
            ComboEditor.SelectedIndex = 0;
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
