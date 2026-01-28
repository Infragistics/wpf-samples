using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models.Common;
using Infragistics.Windows.DataPresenter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;

namespace IGDataGrid.Samples.Organization
{
    /// <summary>
    /// Interaction logic for ControlingFilterBehavior.xaml
    /// </summary>
    public partial class ControlingFilterBehavior : SampleContainer
    {
        public ControlingFilterBehavior()
        {
            InitializeComponent();

            Type[] enumTypes = new Type[]
            {
                typeof( RecordFilterScope ),
                typeof( FilterUIType ),
                typeof( FieldSortComparisonType ),
                typeof( Infragistics.Windows.Controls.LogicalOperator ),
            };

            foreach (Type t in enumTypes)
                this.Resources.Add("enum_" + t.Name, Enum.GetValues(t));

            this.cbRecordFilterAction.ItemsSource = new List<RecordFilterAction>()
            {
                RecordFilterAction.Default,
                RecordFilterAction.Disable,
                RecordFilterAction.DoNothing,
                RecordFilterAction.Hide,
                RecordFilterAction.ReduceOpacity
            };
        }

        void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DataTable dt = NWindDataSource.GetTable(NWindTable.Orders);

            this.XamDataGrid1.DataSource = dt.DefaultView;
        }
    }
}