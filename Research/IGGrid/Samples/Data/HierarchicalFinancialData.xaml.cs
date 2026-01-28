using IGGrid.Resources;
using Infragistics.Controls.Grids;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace IGGrid.Samples.Data
{
    public partial class HierarchicalFinancialData : SampleContainer
    {
        private System.Windows.Style CellBaseStyle = null;

        public HierarchicalFinancialData()
        {
            InitializeComponent();
            Loaded += OnSampleLoaded;
        }

        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            DownloadDataSource();
            
            this.CellBaseStyle = this.Resources["CellControlStyle"] as System.Windows.Style;
        }

        private void DownloadDataSource()
        {
            var _dataProvider = new XmlDataProvider();
            _dataProvider.GetXmlDataCompleted += OnDataProviderGetXmlDataCompleted;
            _dataProvider.GetXmlDataAsync("Accounts.xml");
        }

        void OnDataProviderGetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
            if (e.Error != null) return;

            XDocument doc = e.Result;

            this.dataGrid.ItemsSource = this.GetAccounts(doc.Root, "");
            this.ExpandRows(this.dataGrid.Rows);
        }

        private IEnumerable<Account> GetAccounts(XElement parent, string parentId)
        {
            return (from d in parent.Descendants("Account")
                    where d.Attribute("ParentID").Value == parentId
                    select new Account
                    {
                        Number = d.Attribute("AccountNumber").Value,
                        Name = d.Attribute("Name").Value,
                        Balance = d.Attribute("Balance").Value,
                        Accounts = this.GetAccounts(d, d.Attribute("AccountNumber").Value)
                    }).ToList<Account>();
        }

        private void ExpandRows(RowCollection rows)
        {
            foreach (Row row in rows)
            {
                row.IsExpanded = true;
                if (row.HasChildren)
                {
                    // Grab this row's RowCollection, then recursively parse this collection //
                    ExpandRows(row.ChildBands[0].Rows);
                }
            }
        }

        private void DataGrid_ColumnLayoutAssigned(object sender, ColumnLayoutAssignedEventArgs e)
        {
            if (e.ColumnLayout.Columns.Count == 0 && e.DataType == typeof(Account))
            {
                // create a new style based on the default cell style of the cell
                System.Windows.Style s = new System.Windows.Style();
                s.TargetType = typeof(CellControl);
                if (this.CellBaseStyle != null)
                {
                    s.BasedOn = this.CellBaseStyle;
                }

                // create a new style based on the default cell style of the cell
                System.Windows.Style s1 = new System.Windows.Style();
                s1.TargetType = typeof(CellControl);
                if (this.CellBaseStyle != null)
                {
                    s1.BasedOn = this.CellBaseStyle;
                }

                s.Setters.Add(new Setter(Control.PaddingProperty, new Thickness(5 + (e.Level * 20), 0, 0, 0)));

                e.ColumnLayout.Columns.Add(new TextColumn
                {
                    HeaderText = GridStrings.XWG_Account_Number,
                    Key = "Number",
                    Width = new ColumnWidth(100, false),
                    CellStyle = s1
                });
                e.ColumnLayout.Columns.Add(new TextColumn
                {
                    HeaderText = GridStrings.XWG_Account_Name,
                    Key = "Name",
                    Width = new ColumnWidth(300, false),
                    CellStyle = s
                });
                e.ColumnLayout.Columns.Add(new TextColumn
                {
                    HeaderText = GridStrings.XWG_Account_Balance,
                    Key = "Balance",
                    Width = new ColumnWidth(250, false),
                    CellStyle = s1
                });
                e.ColumnLayout.Columns.Add(new ColumnLayout { Key = "Accounts", HeaderVisibility = Visibility.Collapsed });
            }
        }
    }
}
