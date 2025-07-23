using System.Windows.Navigation;
using Infragistics.Controls.Layouts;
using Infragistics.Samples.Framework;

namespace IGDockManager.Samples.Localization
{
    public partial class DockManagerResourceStrings : SampleContainer
    {
        public DockManagerResourceStrings()
        {
            // Resource strings must be applied before InitializeComponent() is called. 
            // They won't update after the control is initialized.
            XamDockManager.RegisterResources("IGDockManager.Resources.DockManagerResourceStringsSample", typeof(DockManagerResourceStrings).Assembly);
            InitializeComponent();
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            XamDockManager.UnregisterResources("IGDockManager.Resources.DockManagerResourceStringsSample");
            base.OnNavigatingFrom(e);
        }
    }
}
