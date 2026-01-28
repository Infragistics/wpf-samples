using System.Collections.Generic;
using IGOrgChart.Resources;
using Infragistics.Samples.Framework;

namespace IGOrgChart.Samples.Editing
{
    public partial class SimpleNodeTooltips : SampleContainer
    {
        public SimpleNodeTooltips()
        {
            InitializeComponent();

            this.SampleLoaded += new System.EventHandler(SimpleNodeTooltips_SampleLoaded);
        }

        void SimpleNodeTooltips_SampleLoaded(object sender, System.EventArgs e)
        {
            //Set the ItemsSource of the combobox.
            ComboBoxTooltipPath.ItemsSource = new Dictionary<string, string>()
                {
                    //The properties, which
                    //the tooltips will display.
                    {OrgChartStrings.OrgChart_Id + " (Id)", "Id"},
                    {OrgChartStrings.OrgChart_Name + " (FullName)", "FullName"}              
                };
            ComboBoxTooltipPath.SelectedIndex = 0;
        }
    }
}
