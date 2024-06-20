using Infragistics.Samples.Framework;

namespace IGTreeGrid.Samples.Organization
{
    public partial class Filtering : SampleContainer
    {
        public Filtering()
        {
            InitializeComponent();
        }

        private void xtg_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            this.xtg.Records.ExpandAll(true);
        }
    }
}
