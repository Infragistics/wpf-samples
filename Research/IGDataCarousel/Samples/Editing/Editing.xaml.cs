using System;
using System.Data;
using System.Windows;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models.Common;
using Infragistics.Windows.DataPresenter;
using Infragistics.Windows.DataPresenter.Events;

namespace IGDataCarousel.Samples.Editing
{
    public partial class Editing : SampleContainer
    {
        public Editing()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(Editing_Samp_Loaded);
        }

        void Editing_Samp_Loaded(object sender, RoutedEventArgs e)
        {
            DataTable table = NWindDataSource.GetTable(NWindTable.Orders);
            this.xamDataCarousel.DataSource = table.DefaultView;
        }

        private void xamDataCarousel_CellChanged(object sender, CellChangedEventArgs e)
        {
            object val = e.Editor.Value;
            if (val != null && e.Cell.Record is DataRecord)
            {
                if (DBNull.Value == val)
                    val = null;
                e.Cell.Value = val;

                if (e.Cell.Field.Name == "OrderDate")
                {
                    xamDateTimeEditor.Value = e.Cell.Value;

                    txtBlock1.Text = e.Cell.Record.Cells["OrderID"].Value.ToString();
                }
                if (e.Cell.Field.Name == "Freight")
                {
                    xamCurrencyEditor.Value = e.Cell.Value;
                    txtBlock2.Text = e.Cell.Record.Cells["OrderID"].Value.ToString();
                }
            }
        }
    }
}
