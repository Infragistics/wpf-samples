using Infragistics.Samples.Framework;

namespace IGOutlookBar.Samples.Editing
{
    public partial class MinimizationCapabilities : SampleContainer
    {
        public MinimizationCapabilities()
        {
            InitializeComponent();
        }

        private void minimizeCheckBox_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            if ((minimizedWidth != null) && (isMinimized != null) && (xobSL1 != null))
            {
                this.minimizedWidth.IsEnabled = true;
                this.isMinimized.IsEnabled = true;
                this.xobSL1.AllowMinimized = true;
            }
        }

        private void minimizeCheckBox_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            if ((minimizedWidth != null) && (isMinimized != null) && (xobSL1 != null))
            {
                this.minimizedWidth.IsEnabled = false;
                this.isMinimized.IsEnabled = false;
                this.xobSL1.AllowMinimized = false;
            }
        }
    }
}
