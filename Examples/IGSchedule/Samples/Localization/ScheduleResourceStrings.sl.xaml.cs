using System.Windows.Navigation;
using Infragistics.Controls.Schedules;
using Infragistics.Samples.Framework;

namespace IGSchedule.Samples.Localization
{
    public partial class ScheduleResourceStrings : SampleContainer
    {
        public ScheduleResourceStrings()
        {
            // Resource strings must be applied before InitializeComponent() is called. 
            // They won't update after the control is initialized.
            ScheduleControlBase.RegisterResources("IGSchedule.Resources.SampleScheduleStrings", typeof(ScheduleResourceStrings).Assembly);
            ScheduleDialogFactory.RegisterResources("IGSchedule.Resources.SampleScheduleStrings", typeof(ScheduleResourceStrings).Assembly);
            InitializeComponent();

            this.dataManager.ColorScheme = new IGColorScheme();
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            ScheduleControlBase.UnregisterResources("IGSchedule.Resources.SampleScheduleStrings");
            ScheduleDialogFactory.UnregisterResources("IGSchedule.Resources.SampleScheduleStrings");

            base.OnNavigatingFrom(e);
        }
    }
}
