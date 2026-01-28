using IGComboEditor.Resources;
using Infragistics;
using Infragistics.Controls.Editors;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace IGComboEditor.Samples.Organization
{
    public partial class SpecifyingCustomFilter : SampleContainer
    {
        public SpecifyingCustomFilter()
        {
            InitializeComponent();
            Loaded += SpecifyingCustomFilter_Loaded;
        }

        private void SpecifyingCustomFilter_Loaded(object sender, RoutedEventArgs e)
        {
            JPCustomValueEnteredActionCheck();
        }

        private void Btn_Apply_OnClick(object sender, RoutedEventArgs e)
        {
            var condition = new ComparisonCondition();

            if (OperatorsList.SelectedValue == null)
            {
                MessageBox.Show(ComboEditorStrings.CE_Msg_SelectOperator);
                return;
            }

            if (FieldsList.SelectedValue == null)
            {
                MessageBox.Show(ComboEditorStrings.CE_Msg_SelectField);
                return;
            }

            if (OperatorsList.SelectedValue != null && FieldsList.SelectedValue != null)
            {
                // Set comparison operator
                condition.Operator = (OperatorsList.SelectedItem as ComparisonOperatorItem).Operator;

                // Create filter
                var filter = new ComboItemFilter()
                    {
                        FieldName = FieldsList.SelectedValue.ToString()
                    };
                
                // Add condition the the filter
                filter.Conditions.Add(condition);

                // Add filter to the filters collection
                ComboEditor.ItemFilters.Add(filter);

                string msg = string.Empty;
                var conditions = ComboEditor.ItemFilters.ToObservableCollection();

                for (int i = 0; i < conditions.Count; i++)
                {
                    var itemFilter = conditions[i] as ComboItemFilter;
                    var comparison = itemFilter.Conditions[0] as ComparisonCondition;

                    if (conditions.Count > 1 && i != conditions.Count - 1)
                    {
                        msg += string.Format("{0} {1}... OR ", itemFilter.FieldName, comparison.Operator);
                    }
                    else
                    {
                        msg += string.Format("{0} {1}...", itemFilter.FieldName, comparison.Operator);
                    }
                }

                PrintLog(msg);
            }            
        }

        private void PrintLog(string msg)
        {
            string logMsg = "[" + DateTime.Now.ToString("HH:mm:ss") + "] " + msg + "\n";

            var lstBoxItem = new ListBoxItem
            {
                Content = logMsg,
                FontSize = 11,
                Height = 20
            };

            ListBox_Output.Items.Insert(0, lstBoxItem);
        }

        private void Btn_Clear_OnClick(object sender, RoutedEventArgs e)
        {
            ComboEditor.ItemFilters.Clear();            
            ListBox_Output.Items.Clear();
            OperatorsList.SelectedIndex = -1;
            FieldsList.SelectedIndex = -1;
        }

        private void JPCustomValueEnteredActionCheck()
        {
            string isoLanguage = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

            // customization
            if (isoLanguage.ToLower().Equals("ja"))
            {
                ComboEditor.CustomValueEnteredAction = CustomValueEnteredActions.Allow;
            }
        }
    }

    public class ComparisonOperatorItem
    {
        public string OperatorName { get; set; }
        public ComparisonOperator Operator { get; set; }
        }

    public class DataProvider : ObservableModel
        {
        public List<string> FieldsList { get; set; }
        public List<ComparisonOperatorItem> OperatorsList { get; set; }

        private ObservableCollection<Customer> _data;
        public ObservableCollection<Customer> Data
        {
            get
            {
                return this._data;
            }
            set
            {
                if (this._data != value)
                {
                    this._data = value;
                    this.OnPropertyChanged("Data");
                }
            }
        }

        public DataProvider()
        {
            DownloadDataSource();
            LoadData();
        }

        private void LoadData()
        {
            var fields = new List<string> { "CustomerID", "ContactName", "Country", "City" };
            this.FieldsList = fields;

            var operators = new List<ComparisonOperatorItem>
                {
                    new ComparisonOperatorItem()
                        {
                            OperatorName = ComboEditorStrings.CE_CustomFilter_StartsWith,
                            Operator = ComparisonOperator.StartsWith
                        },
                    new ComparisonOperatorItem()
                        {
                            OperatorName = ComboEditorStrings.CE_CustomFilter_EndsWith,
                            Operator = ComparisonOperator.EndsWith
                        },
                    new ComparisonOperatorItem()
                        {
                            OperatorName = ComboEditorStrings.CE_CustomFilter_Contains,
                            Operator = ComparisonOperator.Contains
                        },
                    new ComparisonOperatorItem()
                        {
                            OperatorName = ComboEditorStrings.CE_CustomFilter_DoesNotContain,
                            Operator = ComparisonOperator.DoesNotContain
                        }
                };

            this.OperatorsList = operators;
        }

        private void DownloadDataSource()
        {
            var xmlDataProvider = new XmlDataProvider();
            xmlDataProvider.GetXmlDataCompleted += new EventHandler<GetXmlDataCompletedEventArgs>(xmlDataProvider_GetXmlDataCompleted);
            xmlDataProvider.GetXmlDataAsync("Customers.xml");
        }

        private void xmlDataProvider_GetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs gxdcea)
        {
            XDocument doc = gxdcea.Result;

            var dataSource = (from d in doc.Descendants("Customers")
                              select new Customer
                              {
                                  CustomerID = d.Element("CustomerID").GetString(),
                                  ContactName = d.Element("ContactName").GetString(),
                                  Country = d.Element("Country").GetString(),
                                  City = d.Element("City").GetString(),
                                  ImageResourcePath = ImageFilePathProvider.GetImageLocalPath("People/" + d.Element("ImageResourcePath").Value)
                              });

            this.Data = dataSource.ToObservableCollection();
        }
    }
}