using System;
using System.Data;
using Infragistics.Samples.Framework;

namespace IGDataGrid.Samples.Organization
{
    /// <summary>
    /// Interaction logic for FormattingRowSummaries.xaml
    /// </summary>
    public partial class FormattingRowSummaries : SampleContainer
    {
        public FormattingRowSummaries()
        {
            InitializeComponent();
            XamDataGrid1.DataSource = CreateDataSource().DefaultView;
        }

        DataTable CreateDataSource()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Name", typeof(string)));
            dt.Columns.Add(new DataColumn("NegativeSum", typeof(int)));
            dt.Columns.Add(new DataColumn("PositiveSum", typeof(int)));

            Random rnd = new Random();
            int value;
            for (int i = 0; i < 10; i++)
            {
                DataRow row = dt.NewRow();

                value = ((i & 1) == 1 ? -i : i) * 13;

                row[0] = "Item " + i;
                row[1] = value;
                row[2] = -value;

                dt.Rows.Add(row);
            }
            return dt;
        }
    }
}