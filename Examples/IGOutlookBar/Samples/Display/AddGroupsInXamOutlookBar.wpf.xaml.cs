using System.Windows;
using IGOutlookBar.Samples.Style;
using Infragistics.Samples.Framework;

namespace IGOutlookBar.Samples.Display
{
    public partial class AddGroupsInXamOutlookBar : SampleContainer
    {
        public AddGroupsInXamOutlookBar()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            Theming.HydrateAllThemeResources("RoyalLight");
            this.xamOutlookBar.Theme = "RoyalLight";
        }
    }
}