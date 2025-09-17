using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using Infragistics.Controls.Grids;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Controls;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models;

namespace IGGrid.Samples.Display
{
    public partial class FixedColumns : SampleContainer
    {

        public FixedColumns()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(OnSampleLoaded);
        }

        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            this.lbAllowFixedColumns.ItemsSource = EnumValuesProvider.GetEnumValues<FixedColumnType>();
            this.lbAllowFixedColumns.SelectedIndex = (int)this.dataGrid.FixedColumnSettings.AllowFixedColumns;

            this.lbFixedDropAreaLocation.ItemsSource = EnumValuesProvider.GetEnumValues<FixedDropAreaLocation>();
            this.lbFixedDropAreaLocation.SelectedIndex = (int)this.dataGrid.FixedColumnSettings.FixedDropAreaLocation;

            this.lbFixedIndicatorDirection.ItemsSource = EnumValuesProvider.GetEnumValues<FixedIndicatorDirection>();
            this.lbFixedIndicatorDirection.SelectedIndex = (int)this.dataGrid.FixedColumnSettings.FixedIndicatorDirection;

            this.DownloadDataSource();
        }

        private void DownloadDataSource()
        {
            var _dataProvider = new XmlDataProvider();
            _dataProvider.GetXmlDataCompleted += OnDataProviderGetXmlDataCompleted;
            _dataProvider.GetXmlDataAsync("Patients.xml");
        }

        /// <summary>
        /// Ths methods' XLinq Query uses special extention methods for converting data. 
        /// The extention methods can be found in the Common\XElementExtension class. 
        /// </summary>
        void OnDataProviderGetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
            if (e.Error != null) return;

            XDocument doc = e.Result;
            var dataSource = (from d in doc.Descendants("PatientAdmittance")
                              select new PatientAdmittance
                              {
                                  AdmittanceId = d.Attribute("AdmittanceId").GetString(),
                                  FirstName = d.Attribute("FirstName").GetString(),
                                  LastName = d.Attribute("LastName").GetString(),
                                  DOB = d.Attribute("DOB").GetDateTime(),
                                  Gender = d.Attribute("Gender").GetString(),
                                  AdmittanceDate = d.Attribute("AdmittanceDate").GetDateTime(),
                                  Location = d.Attribute("Location").GetString(),
                                  Severity = d.Attribute("Severity").GetString(),
                              }).ToList<PatientAdmittance>();

            this.dataGrid.ItemsSource = dataSource;
        }

        private void lbAllowFixedColumns_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (FixedColumnType)e.AddedItems[0];
            this.dataGrid.FixedColumnSettings.AllowFixedColumns = selectedItem;

            // Update IsEnabled Properties Based on Current Selection
            switch (this.dataGrid.FixedColumnSettings.AllowFixedColumns)
            {
                case FixedColumnType.Both:
                    this.dropAreaGroup.Visibility = this.indicatorDirectionGroup.Visibility = Visibility.Visible;
                    break;

                case FixedColumnType.Disabled:
                    this.dropAreaGroup.Visibility = this.indicatorDirectionGroup.Visibility = Visibility.Collapsed;
                    break;

                case FixedColumnType.DropArea:
                    this.dropAreaGroup.Visibility = Visibility.Visible;
                    this.indicatorDirectionGroup.Visibility = Visibility.Collapsed;
                    break;

                case FixedColumnType.Indicator:
                    this.dropAreaGroup.Visibility = Visibility.Collapsed;
                    this.indicatorDirectionGroup.Visibility = Visibility.Visible;
                    break;

                default:
                    break;
            }
        }

        private void lbFixedDropAreaLocation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (FixedDropAreaLocation)e.AddedItems[0];
            this.dataGrid.FixedColumnSettings.FixedDropAreaLocation = selectedItem;
        }

        private void lbFixedIndicatorDirection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (FixedIndicatorDirection)e.AddedItems[0];
            this.dataGrid.FixedColumnSettings.FixedIndicatorDirection = selectedItem;
        }
    }
}
