using Infragistics.Samples.Framework;

namespace IGResourceWasher.Samples.Display
{
    public partial class WasherWashGroups : SampleContainer
    {
        public WasherWashGroups()
        {
            InitializeComponent();
        }

        private void btnWash_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            resWasher1.SourceDictionary = this.Resources;
        }
    }
}