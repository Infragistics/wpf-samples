using Infragistics.Samples.Framework;
using Infragistics.Windows.DataPresenter;
using Infragistics.Windows.DataPresenter.Events;

namespace IGTreeGrid.Samples.Display
{
    public partial class AddingIcons : SampleContainer
    {
        public AddingIcons()
        {
            InitializeComponent();
        }

        private void xtg_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            this.xtg.Records.ExpandAll(true);
        }

        private void xtg_InitializeRecord(object sender, InitializeRecordEventArgs e)
        {
            if (e.Record is DataRecord)
            {
                var dr = (DataRecord)e.Record;
                if (!bool.Parse(dr.Cells["IsFolder"].Value.ToString()))
                {
                    object obj = dr.Cells["FileSize"].Value;
                    dr.Cells["calcSize"].Value = obj;
                }
            }
        }
    }
}
