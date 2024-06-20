using System.Windows.Navigation;
using IGTimeline.Resources.SampleControlStrings;
using Infragistics.Controls.Timelines;

namespace IGTimeline.Samples
{
    public partial class TimelineResourceStrings : Infragistics.Samples.Framework.SampleContainer
    {
        public TimelineResourceStrings()
        {
            //Resource strings must be applied before InitializeComponent() is called.             
            //They won't update after the control is initialized.
            XamTimeline.RegisterResources(
                //The full name of the .resx file to be used.
                "IGTimeline.Resources.SampleControlStrings.SampleTimelineStrings",
                //The assembly in which the .resx file is embedded.
                typeof(SampleTimelineStrings).Assembly);
            InitializeComponent();
        }

        // Executes when the user navigates away from this page.
        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            //Restore the default strings.
            XamTimeline.UnregisterResources(
                //The name of the embedded resx file that was used for registration.
                "IGTimeline.Resources.SampleControlStrings.SampleTimelineStrings");

            base.OnNavigatingFrom(e);
        }

        private void ToggleButton_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            NumericTimeAxis axis = (NumericTimeAxis)timeline.Axis;

            //Turn off the AutoRange of the axis and set
            //a minimum higher than the maximum.
            //This will make the Timeline show an error message.
            axis.AutoRange = false;
            axis.Minimum = 2;
            axis.Maximum = 1;

            //Note that the error message is stored
            //in the .resx file, which was set using
            //the XamTimeline.RegisterResources method
            //in the constructor of the page.
        }
        private void ToggleButton_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            //Turn on the AutoRange of the axis.
            //This will hide the error message.
            timeline.Axis.AutoRange = true;
        }
    }
}
