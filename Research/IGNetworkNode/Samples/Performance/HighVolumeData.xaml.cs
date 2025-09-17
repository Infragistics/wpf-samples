using Infragistics.Samples.Shared.Models;

namespace IGNetworkNode.Samples.Performance
{
    public partial class HighVolumeData : Infragistics.Samples.Framework.SampleContainer
    {
        private NetworkNodeLargeData _data;

        public HighVolumeData()
        {
            InitializeComponent();
        }

        private void Load_Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_data == null)
            {
                _data = new NetworkNodeLargeData();
            }
            this.xnn.ItemsSource = _data.Nodes;
        }
    }
}
