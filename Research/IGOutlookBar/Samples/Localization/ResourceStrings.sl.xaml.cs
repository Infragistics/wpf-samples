using System.Windows.Navigation;
using IGOutlookBar.Resources;
using Infragistics.Controls.Menus;
using Infragistics.Samples.Framework;

namespace IGOutlookBar.Samples.Localization
{
    public partial class ResourceStrings : SampleContainer
    {
        public ResourceStrings()
        {
            // Resource strings must be applied before InitializeComponent() is called. 
            // They won't update after the control is initialized.
            XamOutlookBar.RegisterResources("IGOutlookBar.Resources.OutlookBarResourceStringsSample", typeof(OutlookBarResourceStringsSample).Assembly);

            InitializeComponent();
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            XamOutlookBar.UnregisterResources("IGOutlookBar.Resources.OutlookBarResourceStringsSample");
            base.OnNavigatingFrom(e);
        }
    }
}
