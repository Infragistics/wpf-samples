using Infragistics.Samples.Framework;
using System.Windows;

namespace IGSlider.Samples.Editing
{
    public partial class AddingTickmarks : Infragistics.Samples.Framework.SampleContainer
    {
        public AddingTickmarks()
        {
            InitializeComponent();
  
            
        }

        private void SnappingEnabledCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            this.HorizontalSlider.Thumb.IsSnapToTickEnabled = this.SnappingEnabledCheckbox.IsChecked.Value;
            this.VerticalSlider.Thumb.IsSnapToTickEnabled = this.SnappingEnabledCheckbox.IsChecked.Value;
        }

        private void SnappingEnabledCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            this.HorizontalSlider.Thumb.IsSnapToTickEnabled = this.SnappingEnabledCheckbox.IsChecked.Value;
            this.VerticalSlider.Thumb.IsSnapToTickEnabled = this.SnappingEnabledCheckbox.IsChecked.Value;
        }
    }
}
