using IGGrid.Resources;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Resources;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Xml.Linq; 

namespace IGGrid.Samples.Organization
{
    public partial class CustomFilterDialog : SampleContainer
    {
        public CustomFilterDialog()
        {
            InitializeComponent();

            this.UseDefaultTheme = true;
            this.Loaded += OnSampleLoaded;
            this.Unloaded += OnSampleUnloaded;
        }

        private void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            this.DownloadDataSource();
            ApplyThemes();
        }
        private void OnSampleUnloaded(object sender, RoutedEventArgs e)
        {
            ClearThemes();
        }

        private void ApplyThemes()
        { 
            ThemeLoader.SetTheme(this, ThemeType.IG); 
        }
        private void ClearThemes()
        { 
            ThemeLoader.SetTheme(this, ThemeType.Default); 
        }

        private void DownloadDataSource()
        {
            var dataProvider = new XmlDataProvider();
            dataProvider.GetXmlDataCompleted += OnDataProviderGetXmlDataCompleted;
            dataProvider.GetXmlDataAsync("Patients.xml");
        }

        private void OnDataProviderGetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
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
                              });

            this.dataGrid.ItemsSource = dataSource.ToList();
        }
    }
    
    public class Severity : ObservableModel
        {
            private string _level;
            public string Level
            {
                get
                {
                    return _level;
                }
                set
                {
                    if (_level != value)
                    {
                        _level = value;
                        this.OnPropertyChanged("Level");
                    }
                }
            }
        }
    public class SeverityLevels : ObservableCollection<Severity>
    {
        public SeverityLevels()
        {
            this.Add(new Severity() { Level = CommonStrings.XG_Severity_Low });
            this.Add(new Severity() { Level = CommonStrings.XG_Severity_Medium });
            this.Add(new Severity() { Level = CommonStrings.XG_Severity_High });
        }
    }

    public class Gender : ObservableModel
    {
        private string _genderID;
        public string GenderID
        {
            get
            {
                return _genderID;
            }
            set
            {
                if (_genderID != value)
                {
                    _genderID = value;
                    this.OnPropertyChanged("GenderID");
                }
            }
        }
        private string _genderText;
        public string GenderText
        {
            get
            {
                return _genderText;
            }
            set
            {
                if (_genderText != value)
                {
                    _genderText = value;
                    this.OnPropertyChanged("GenderText");
                }
            }
        }
    }
    public class GenderIDs : ObservableCollection<Gender>
    {
        public GenderIDs()
        {
            this.Add(new Gender() { GenderID = GridStrings.XG_Gender_Male_Short, GenderText = GridStrings.XG_Gender_Male });
            this.Add(new Gender() { GenderID = GridStrings.XG_Gender_Female_Short, GenderText = GridStrings.XG_Gender_Female });
        }
    }
}

