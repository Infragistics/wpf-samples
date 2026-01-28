using Infragistics.Samples.Framework;
using Infragistics.Windows.DataPresenter;
using Infragistics.Windows.DataPresenter.Events;

namespace IGDataCards.Samples.Display
{
    /// <summary>
    /// Interaction logic for CollapseEmptyCells_Samp.xaml
    /// </summary>
    public partial class CollapseEmptyCells : SampleContainer
    {
        public CollapseEmptyCells()
        {
            InitializeComponent();
        }

        private void xamDataCards1_InitializeRecord(object sender, InitializeRecordEventArgs e)
        {
            // clear the third cell's value in every other record/card
            if (!e.ReInitialize && e.Record.Index % 2 == 0)
            {
                DataRecord dr = e.Record as DataRecord;

                if (dr != null)
                {
                    dr.Cells["department"].Value = string.Empty;
                }
            }
        }
    }
}
