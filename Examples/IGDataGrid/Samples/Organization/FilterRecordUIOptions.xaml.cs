using System;
using System.Data;
using System.Windows;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models.Common;
using Infragistics.Windows.DataPresenter;

namespace IGDataGrid.Samples.Organization
{
    /// <summary>
    /// Interaction logic for FilterRecordUIOptions.xaml
    /// </summary>
    public partial class FilterRecordUIOptions : SampleContainer
    {
        public FilterRecordUIOptions()
        {
            InitializeComponent();

            Type[] enumTypes = new Type[]
			{
				typeof( FilterRecordLocation ),
				typeof( FilterOperandUIType ),
				typeof( FilterClearButtonLocation ),
				typeof( Visibility ),
				typeof( Infragistics.Windows.Controls.ComparisonOperator ),
				typeof( FilterEvaluationTrigger ),
			};

            foreach (Type t in enumTypes)
                this.Resources.Add("enum_" + t.Name, Enum.GetValues(t));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DataTable dt = NWindDataSource.GetTable(NWindTable.Orders);
            this.XamDataGrid1.DataSource = dt.DefaultView;
        }
    }
}