using System.Windows;
using Infragistics.Samples.Framework;

namespace IGOutlookBar.Samples.Display
{
    public partial class CreatingAnOutlookBar : SampleContainer
    {
        public CreatingAnOutlookBar()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            //SamplesBrowserUtils.HydrateAllThemeResources("IGTheme");
            //this.xamOutlookBar.Theme = "IGTheme";
        }
    }
}
