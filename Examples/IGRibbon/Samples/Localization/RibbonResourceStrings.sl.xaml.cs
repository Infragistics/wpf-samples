using System.Windows.Navigation;
using Infragistics.Controls.Menus;
using Infragistics.Samples.Framework;

namespace IGRibbon.Samples.Localization
{
    public partial class RibbonResourceStrings : SampleContainer
    {
        public RibbonResourceStrings()
        {
            // Resource strings must be applied before InitializeComponent() is called. 
            // They won't update after the control is initialized.
            XamRibbon.RegisterResources("IGRibbon.Resources.RibbonResourceStringsSample",
                typeof(IGRibbon.Resources.RibbonResourceStringsSample).Assembly);
            InitializeComponent();
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            XamRibbon.UnregisterResources("IGRibbon.Resources.RibbonResourceStringsSample");
            base.OnNavigatingFrom(e);
        }
    }
}