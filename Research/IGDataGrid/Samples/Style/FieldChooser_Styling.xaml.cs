using System;
using System.Data;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models.Common;
using Infragistics.Windows.DataPresenter;

namespace IGDataGrid.Samples.Style
{
    /// <summary>
    /// Interaction logic for FieldChooser_Styling.xaml
    /// </summary>
    public partial class FieldChooser_Styling : SampleContainer
    {
        public FieldChooser_Styling()
        {
            InitializeComponent();
            InitializeSample();
        }

        private void InitializeSample()
        {
            Type[] enumTypes = new Type[]
            {
                typeof( HeaderPrefixAreaDisplayMode )
            };

            foreach (Type t in enumTypes)
                this.Resources.Add("enum_" + t.Name, Enum.GetValues(t));

            // Asign a data source.
            DataTable dt = NWindDataSource.GetTable(NWindTable.Orders);
            this.XamDataGrid1.DataSource = dt.DefaultView;
        }
    }
}
