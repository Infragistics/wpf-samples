using System;
using Infragistics.Olap;
using Infragistics.Olap.Data;
using Infragistics.Samples.Framework;
using IGPivotGrid.Resources;

namespace IGPivotGrid.Samples.Display
{
    public partial class CalculatedMembers : SampleContainer
    {
        public CalculatedMembers()
        {
            InitializeComponent();
            this.pivotGrid.DataSource.Initialized += DataSource_Initialized;
        }

        private void DataSource_Initialized(object sender, EventArgs e)
        {
            // Create calculated measure.
            // By default all calculated measures are attached to "[Measures]" dimension.
            // This can be changed(overriden) using the fourth parameter
            ICalculatedItemViewModel calcMeasureViewModel =
                new CalculatedMeasureViewModel(
                    PivotGridStrings.XPG_DoubledAmount,
                    PivotGridStrings.XPG_DoubledSalesAmount,
                    "[Measures].[Internet Sales Amount] * 2");

            // Create calculated member.
            // Creating calculated member requires the hierarchy it is attached to
            // Note: The hierarchy for calculation should not be defined with Rows or Columns.
            //       The calculated members cannot be the same as the members already defined in Rows or Columns.
            IHierarchy dateCalendar = this.pivotGrid.DataSource.Cube.Dimensions["[Date]"].Hierarchies["Calendar"];
            ICalculatedItemViewModel dateCalendarCalcItem = new CalculatedMemberViewModel(
                dateCalendar,
                "2004-2002",
                PivotGridStrings.XPG_Difference2004_2002,
                "[Date].[Calendar].[Calendar Year].[CY 2004] - [Date].[Calendar].[Calendar Year].[CY 2002]",
                true);


            this.pivotGrid.DataSource.Measures.Clear();
            this.pivotGrid.DataSource.Measures.Add(calcMeasureViewModel);

            this.pivotGrid.DataSource.Rows.Clear();
            this.pivotGrid.DataSource.Rows.Add(dateCalendarCalcItem);
        }
    }
}
