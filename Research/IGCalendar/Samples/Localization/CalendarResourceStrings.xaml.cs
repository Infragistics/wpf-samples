using System.Windows.Navigation;
using Infragistics.Controls.Editors;
using Infragistics.Samples.Framework;

namespace IGCalendar.Samples.Localization
{
    public partial class CalendarResourceStrings : SampleContainer
    {
        public CalendarResourceStrings()
        {
            // Resource strings must be applied before InitializeComponent() is called. 
            // They won't update after the control is initialized.
            //"Infragistics.Samples.WPF.xamFeatureBrowser.Samples.XamCalendar.Resources.CalendarResourceStrings", typeof(CalendarResourceStrings).Assembly);
            XamCalendar.RegisterResources(
                "IGCalendar.Resources.CalendarResourceStrings", typeof(IGCalendar.Resources.CalendarResourceStrings).Assembly);

            InitializeComponent();
            myCalendar.ResourceProvider = new CalendarResourceProvider() { ResourceSet = CalendarResourceSet.IGTheme };
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            //Restore the default strings.
            XamCalendar.UnregisterResources(
                //The name of the embedded resx file that was used for registration.
                "IGCalendar.Resources.CalendarResourceStrings");

            base.OnNavigatingFrom(e);
        }
    }
}
