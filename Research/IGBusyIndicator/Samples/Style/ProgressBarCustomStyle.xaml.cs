using Infragistics.Samples.Framework;

namespace IGBusyIndicator.Samples.Style
{
    /// <summary>
    /// Interaction logic for ProgressBarCustomStyle.xaml
    /// </summary>
    public partial class ProgressBarCustomStyle : SampleContainer
    {
        public ProgressBarCustomStyle()
        {
            InitializeComponent();
        }

        private void TextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                double progressValue;
                if (double.TryParse(this.TxtBox_ProgressValue.Text, out progressValue))
                    this.BusyIndicator.ProgressValue = progressValue;
            }
        }
    }
}
