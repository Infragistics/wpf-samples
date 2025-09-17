using System.Windows;
using Infragistics.Samples.Framework;

#if WPF
using System.Diagnostics;
using Infragistics.Controls.Menus;
#endif

namespace IGTagCloud.Samples.Display
{
    public partial class TagCloudXamlBased : SampleContainer
    {
        public TagCloudXamlBased()
        {
            InitializeComponent();
            Loaded += TagCloudXamlBased_Loaded;
        }       

#if WPF
        private void TagCloudXamlBased_Loaded(object sender, RoutedEventArgs e)
        {
            dataTagCloud.XamTagCloudItemClicked += dataTagCloud_XamTagCloudItemClicked;
        }

        private void dataTagCloud_XamTagCloudItemClicked(object sender, XamTagCloudItemEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.XamTagCloudItem.NavigateUri.AbsoluteUri));
        }
#else
        private void TagCloudXamlBased_Loaded(object sender, RoutedEventArgs e)
        {

        }
#endif
    }
}
