using System.Threading;
using Infragistics.Controls.Editors;
using Infragistics.Samples.Framework;

namespace IGComboEditor.Samples.Display
{
    /// <summary>
    /// Interaction logic for ManipulatingDropDownSize.xaml
    /// </summary>
    public partial class ManipulatingDropDownSize : SampleContainer
    {
        public ManipulatingDropDownSize()
        {
            InitializeComponent();
            Loaded += ManipulatingDropDownSize_Loaded;
        }

        private void ManipulatingDropDownSize_Loaded(object sender, System.Windows.RoutedEventArgs e)
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
