using System.Windows.Navigation;
using Infragistics.Controls.Barcodes;

namespace IGBarcode.Samples.Localization
{
    public partial class ResourceStrings : Infragistics.Samples.Framework.SampleContainer
    {
        public ResourceStrings()
        {
            // Resource strings must be applied before InitializeComponent() is called. 
            // They won't update after the control is initialized.
            XamBarcode.RegisterResources(
                "IGBarcode.Resources.BarcodeResourceStrings", typeof(IGBarcode.Resources.BarcodeResourceStrings).Assembly);

            InitializeComponent();
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            //Restore the default strings.
            XamBarcode.UnregisterResources(
                //The name of the embedded resx file that was used for registration.
                "IGBarcode.Resources.BarcodeResourceStrings");

            base.OnNavigatingFrom(e);
        }

        private void ToggleButton_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            //Assign invalid data to the barcodes
            //to make them display a custom error message.
            code39.Data = "Code 39";
            gs1.Data = "Bad Data!";

            //Assign an invalid size version to the QR Code
            //symbol to make it display a custom error message.
            qrBarcode.SizeVersion = SizeVersion.Version1;


            //Note that the error messages are stored
            //in the .resx file, which was set using
            //the XamBarcode.RegisterResources method
            //in the constructor of the page.
        }

        private void ToggleButton_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            //Restore the valid values.

            code39.Data = "CODE 39";
            gs1.Data = "021598745624";
            qrBarcode.SizeVersion = SizeVersion.Undefined;
        }

    }
}