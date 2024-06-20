using System.Windows;
using System.Windows.Navigation;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;

namespace IGSpellChecker.Samples.Localization
{
    public partial class SpellCheckerResourceStrings : SampleContainer
    {
        public SpellCheckerResourceStrings()
        {
            // Resource strings must be applied before InitializeComponent() is called. 
            // They won't update after the control is initialized.
            Infragistics.Controls.Interactions.XamSpellChecker.RegisterResources(
                "IGSpellChecker.Resources.SpellCheckerResourceStrings",
                typeof(IGSpellChecker.Resources.SpellCheckerResourceStrings).Assembly);

            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            this.Loaded -= OnSampleLoaded;

            customSpellChecker.DictionaryUri = DictionaryFileProvider.GetDictionaryLocalUri("us-english-v2-whole.dict");
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            //Restore the default strings.
            Infragistics.Controls.Interactions.XamSpellChecker.UnregisterResources(
                // The name of the embedded resx file that was used for registration
                "IGSpellChecker.Resources.SpellCheckerResourceStrings");

            base.OnNavigatingFrom(e);
        }

        private void btnCheckSpelling_Click(object sender, RoutedEventArgs e)
        {
            customSpellChecker.SpellCheck();
        }
    }
}
