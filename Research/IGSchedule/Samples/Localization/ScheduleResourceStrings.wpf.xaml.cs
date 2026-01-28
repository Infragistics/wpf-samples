using System.Windows.Navigation;
using IGSchedule.Resources;
using Infragistics.Controls.Schedules;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Tools;

namespace IGSchedule.Samples.Localization
{
    /// <summary>
    /// Interaction logic for ScheduleResourceStrings.xaml
    /// </summary>
    public partial class ScheduleResourceStrings : SampleContainer
    {
        public ScheduleResourceStrings()
        {
            // Resource strings must be applied before InitializeComponent() is called. 
            // They won't update after the control is initialized.
            Infragistics.Controls.Schedules.ScheduleControlBase.RegisterResources(
                "IGSchedule.Resources.SampleScheduleStrings",
                typeof(SampleScheduleStrings).Assembly);
            Infragistics.Controls.Schedules.ScheduleDialogFactory.RegisterResources(
                "IGSchedule.Resources.SampleScheduleStrings",
                typeof(SampleScheduleStrings).Assembly);
            
            InitializeComponent();
        }
       
        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            //Restore the default strings.
            Infragistics.Controls.Schedules.ScheduleControlBase.UnregisterResources(
                //The name of the embedded resx file that was used for registration.
                "IGSchedule.Resources.SampleScheduleStrings");

            //Restore the default strings.
            Infragistics.Controls.Schedules.ScheduleDialogFactory.UnregisterResources(
                //The name of the embedded resx file that was used for registration.
                "IGSchedule.Resources.SampleScheduleStrings");

            base.OnNavigatingFrom(e);
        }
    }
}
