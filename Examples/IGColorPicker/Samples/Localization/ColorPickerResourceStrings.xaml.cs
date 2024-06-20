using System.Windows.Navigation;
using IGColorPicker.Resources;
using Infragistics.Controls.Editors;
using Infragistics.Samples.Framework;

namespace IGColorPicker.Samples.Localization
{
    public partial class ColorPickerResourceStrings : SampleContainer
    {
        public ColorPickerResourceStrings()
        {
            // Resource strings must be applied before InitializeComponent() is called. 
            // They won't update after the control is initialized.
            XamColorPicker.RegisterResources("IGColorPicker.Resources.ColorPickerStringsSample", typeof(ColorPickerStringsSample).Assembly);
            InitializeComponent();
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            //Restore the default strings.
            XamColorPicker.UnregisterResources(
                //The name of the embedded resx file that was used for registration.
                "IGColorPicker.Resources.ColorPickerStringsSample");

            base.OnNavigatingFrom(e);
        }
    }
}