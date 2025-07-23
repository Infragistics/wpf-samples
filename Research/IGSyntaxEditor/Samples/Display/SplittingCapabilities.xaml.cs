using System.Windows;
using Infragistics.Documents;
using Infragistics.Samples.Framework;

namespace IGSyntaxEditor.Samples.Display
{
    public partial class SplittingCapabilities : SampleContainer
    {
        public SplittingCapabilities()
        {
            InitializeComponent();
            this.UseDefaultTheme = true;
            this.Loaded += OnSampleLoaded;
        }

        public void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            this.xamSyntaxEditor1.Document = new TextDocument();
        }
    }
}
