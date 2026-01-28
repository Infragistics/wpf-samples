using System.Windows.Controls;

namespace IGPieChart.Samples.Display
{
    /// <summary>
    /// Interaction logic for OtherCategory.xaml
    /// </summary>
    public partial class OthersCategory : Infragistics.Samples.Framework.SampleContainer
    {
        public OthersCategory()
        {
            InitializeComponent();
        }
        private void comboBoxOthers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = e.AddedItems[0] as ComboBoxItem;
            if (item != null)
            {
                double db = double.Parse(item.Content.ToString());
                if (this.pieChart != null)
                {
                    this.pieChart.OthersCategoryThreshold = db;
                }
            }
        }
    }
}