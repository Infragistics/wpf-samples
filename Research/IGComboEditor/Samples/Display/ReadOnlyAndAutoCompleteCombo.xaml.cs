using Infragistics.Controls.Editors;
using Infragistics.Samples.Framework;
using System.Threading;
using System.Windows;

namespace IGComboEditor.Samples.Display
{
    public partial class ReadOnlyAndAutoCompleteCombo : SampleContainer
    {
        public ReadOnlyAndAutoCompleteCombo()
        {
            InitializeComponent();           
            this.Loaded += OnSampleLoaded;
        }
        
        private void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            string isoLanguage = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

            if (isoLanguage.ToLower().Equals("ja"))
            {
                this.googleStyleComboFiltered.CustomValueEnteredAction = CustomValueEnteredActions.Allow;
                this.autoCompleteCombo.CustomValueEnteredAction = CustomValueEnteredActions.Allow;
                this.autoCompleteNoPopup.CustomValueEnteredAction = CustomValueEnteredActions.Allow;
            }
        }
    }
}
