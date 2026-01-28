using System;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models.Common;
using Infragistics.Windows.DataPresenter.Events;

namespace IGDataGrid.Samples.Display
{
    /// <summary>
    /// Interaction logic for ManualAssignFieldLayouts.xaml
    /// </summary>
    public partial class ManualAssignFieldLayouts : SampleContainer
    {
        private int index = 0;

        public ManualAssignFieldLayouts()
        {
            InitializeComponent();

            this.XamDataGrid1.DataSource = NWindDataSource.GetTable(NWindTable.Customers, true).DefaultView;
        }

        void XamDataGrid1_AssigningFieldLayoutToItem(object sender, AssigningFieldLayoutToItemEventArgs e)
        {
            Console.WriteLine("Assigning FieldLayoutIndex: {0}", index);

            e.FieldLayout = this.XamDataGrid1.FieldLayouts[index];

            index++;

            if (index > 2)
            { index = 0; }
        }
    }
}
