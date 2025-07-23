using System.ComponentModel;
using System.Windows;
using System.Windows.Navigation;
using IGPivotGrid.Resources;
using Infragistics.Controls.Grids;
using Infragistics.Samples.Framework;

namespace IGPivotGrid.Samples.Localization
{
    public partial class ResourceStrings : SampleContainer
    {
        public ResourceStrings()
        {
            //Resource strings must be applied before InitializeComponent() is called.             
            //They won't update after the control is initialized.
            XamPivotGrid.RegisterResources(
                //The full name of the .resx file to be used.
                "IGPivotGrid.Resources.SamplePivotGridStrings",
                //The assembly in which the .resx file is embedded.
                typeof(SamplePivotGridStrings).Assembly);

            InitializeComponent();
            this.pivotGrid.DataSource.PropertyChanged += DataSource_PropertyChanged;
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            //Restore the default strings.
            XamPivotGrid.UnregisterResources(
                //The name of the embedded resx file that was used for registration.
                "IGPivotGrid.Resources.SamplePivotGridStrings");

            base.OnNavigatingFrom(e);
        }

        private void DataSource_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Processing")
                isBusyIndicator.Visibility = pivotGrid.DataSource.IsBusy ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
