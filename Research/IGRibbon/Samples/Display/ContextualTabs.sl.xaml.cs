using System.Windows;
using Infragistics.Samples.Framework;

namespace IGRibbon.Samples.Display
{
    public partial class ContextualTabs : SampleContainer
    {
        public ContextualTabs()
        {
            InitializeComponent();
        }

        private void Textbox_GotFocus(object sender, RoutedEventArgs e)
        {
            // Display Contextual Tab
            this.MyRibbon.ContextualTabs[0].IsVisible = true;
        }

        private void Textbox_LostFocus(object sender, RoutedEventArgs e)
        {
            // Hide Contextual Tab
            this.MyRibbon.ContextualTabs[0].IsVisible = false;
        }
    }
}
