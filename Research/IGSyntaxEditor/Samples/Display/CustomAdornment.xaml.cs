using IGSyntaxEditor.Resources;
using IGSyntaxEditor.Samples.CustomAdornments;
using Infragistics.Samples.Framework;

namespace IGSyntaxEditor.Samples.Display
{
    public partial class CustomAdornment : SampleContainer
    {
        private string WhiteSpaceAdornmentServiceKey = "WhiteSpaceAdornment";

        public CustomAdornment()
        {
            InitializeComponent();
            InitializeSample();
            this.SampleLoaded += CustomAdornment_SampleLoaded;
            this.SampleUnloaded += CustomAdornment_SampleUnloaded;
        }

        private void InitializeSample()
        {
            this.UseDefaultTheme = true;
            // add some content in the Syntax Editor's document
            this.xamSyntaxEditor1.Document.InitializeText(SyntaxEditorStrings.SampleWithSpaceAndTabs);
        }

        void CustomAdornment_SampleLoaded(object sender, System.EventArgs e)
        {
            // register the new whitespace adornment
            this.xamSyntaxEditor1.Document.Language.ServicesManager.RegisterService(
                WhiteSpaceAdornmentServiceKey,
                new WhiteSpaceAdornmentProvider(this.DataContext as WhiteSpaceAdornmentArgumentsProvider));
        }

        void CustomAdornment_SampleUnloaded(object sender, System.EventArgs e)
        {
            // unregister the adornment, so that other samples don't instantiate it
            this.xamSyntaxEditor1.Document.Language.ServicesManager.UnregisterService(WhiteSpaceAdornmentServiceKey);
        }
    }
}
