using System.Windows.Navigation;
using Infragistics.Controls.Charts;
using IGTreemap.Resources.SampleControlStrings;

namespace IGTreemap.Samples
{
    public partial class TreemapResourceStrings : Infragistics.Samples.Framework.SampleContainer
    {
        public TreemapResourceStrings()
        {
            //Resource strings must be applied before InitializeComponent() is called.             
            //They won't update after the control is initialized.
            XamTreemap.RegisterResources(
                //The full name of the .resx file to be used.
                "IGTreemap.Resources.SampleControlStrings.SampleTreemapStrings",
                //The assembly in which the .resx file is embedded.
                typeof(SampleTreemapStrings).Assembly);

            InitializeComponent();
        }

        // Executes when the user navigates away from this page.
        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            //Restore the default strings.
            XamTreemap.UnregisterResources(
                //The name of the embedded resx file that was used for registration.
                "IGTreemap.Resources.SampleControlStrings.SampleTreemapStrings");

            base.OnNavigatingFrom(e);
        }

        private void ToggleButton_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            //Clear the Value Path of the Node Binder,
            //which binds to the country data.
            //This will cause the Treemap do display a custom
            //error message.
            treemap.NodeBinders[0].ValuePath = string.Empty;

            //Note that the error message is stored
            //in the .resx file, which was set using
            //the XamTreemap.RegisterResources method
            //in the constructor of the page.
        }

        private void ToggleButton_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {   
            //Restore the Value Path of the Node Binder,
            //which binds to the country data.
            //This will hide the error message.
            treemap.NodeBinders[0].ValuePath = "PublicDebt";
        }
    }
}
