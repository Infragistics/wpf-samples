using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models.Common;
using Infragistics.Windows.DataPresenter;
using System.Data;
using System.Globalization;
using System.Windows;

namespace IGDataGrid.Samples.Organization
{
    /// <summary>
    /// Interaction logic for CrossFieldFiltering.xaml
    /// </summary>
    public partial class CrossFieldFiltering : SampleContainer
    {
        private CrossFieldRecordFilterGroup cfrfg = null;

        public CrossFieldFiltering()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DataTable dt = NWindDataSource.GetTable(NWindTable.Customers, new CultureInfo("en-US"));
            this.xamDataGrid1.DataSource = dt.DefaultView;
        }

        private void cf_CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (this.cfrfg != null)
            {
                this.xamDataGrid1.FieldLayouts[0].CrossFieldRecordFilters = this.cfrfg;
                this.xamDataGrid1.FieldLayoutSettings.HeaderPrefixAreaMenuOptions = HeaderPrefixAreaMenuOptions.Default;
                this.EnumValuesProvider1.IsEnabled = true;
            }
        }

        private void cf_CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            this.cfrfg = this.xamDataGrid1.FieldLayouts[0].CrossFieldRecordFilters;
            this.xamDataGrid1.FieldLayouts[0].CrossFieldRecordFilters = null;
            this.xamDataGrid1.FieldLayoutSettings.HeaderPrefixAreaMenuOptions = HeaderPrefixAreaMenuOptions.ShowFieldChooserDialog;
            this.EnumValuesProvider1.IsEnabled = false;
        }
    }
}
