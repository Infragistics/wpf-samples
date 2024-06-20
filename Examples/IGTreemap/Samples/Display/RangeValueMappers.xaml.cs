using System.Windows.Navigation;
using IGTreemap.Resources;

namespace IGTreemap.Samples
{
    public partial class RangeValueMappers : Infragistics.Samples.Framework.SampleContainer
    {
        public RangeValueMappers()
        {
            InitializeComponent();
          
            this.StandardCostLabel.Text = TreemapStrings.XWT_DataMinMax_StandardCost;
            this.RevenueLabel.Text = TreemapStrings.XWT_DataMinMax_Revenue;
        }
    }
}
