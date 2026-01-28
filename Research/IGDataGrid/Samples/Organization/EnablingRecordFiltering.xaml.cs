using System;
using System.Data;
using System.Windows;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models.Common;
using Infragistics.Windows.DataPresenter;

namespace IGDataGrid.Samples.Organization
{
    /// <summary>
    /// Interaction logic for EnablingRecordFiltering.xaml
    /// </summary>
    public partial class EnablingRecordFiltering : SampleContainer
    {
        public EnablingRecordFiltering()
        {
            InitializeComponent();

            Type[] enumTypes = new Type[]
            {
                typeof( FilterUIType ),
                typeof( FilterLabelIconDropDownType ),
            };

            foreach (Type t in enumTypes)
                this.Resources.Add("enum_" + t.Name, Enum.GetValues(t));
            XamDataGrid1.FieldSettings.FilterLabelIconDropDownType = FilterLabelIconDropDownType.MultiSelectExcelStyle;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DataTable dt = NWindDataSource.GetTable(NWindTable.Orders);

            this.XamDataGrid1.DataSource = dt.DefaultView;
        }
    }
}
