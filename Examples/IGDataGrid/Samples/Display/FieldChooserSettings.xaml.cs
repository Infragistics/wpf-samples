using System;
using System.Data;
using System.Windows;
using Infragistics.Samples.Shared.Models.Common;
using Infragistics.Windows.DataPresenter;
using Infragistics.Samples.Framework;

namespace IGDataGrid.Samples.Display
{
    /// <summary>
    /// Interaction logic for FieldChooserSettings.xaml
    /// </summary>
    public partial class FieldChooserSettings : SampleContainer
    {
        public FieldChooserSettings()
        {
            InitializeComponent();

            Type[] enumTypes = new Type[]
            {
                typeof( FieldChooserDisplayOrder ),
                typeof( Visibility )
            };

            foreach (Type t in enumTypes)
                this.Resources.Add("enum_" + t.Name, Enum.GetValues(t));

            // Asign a data source.
            // 
            DataTable dt = NWindDataSource.GetTable(NWindTable.Orders);
            this.XamDataGrid1.DataSource = dt.DefaultView;

            Infragistics.Windows.Controls.BoolConverter bc = new Infragistics.Windows.Controls.BoolConverter();
        }
    }
}
