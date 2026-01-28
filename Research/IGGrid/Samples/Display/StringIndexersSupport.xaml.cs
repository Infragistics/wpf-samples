using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Infragistics.Samples.Framework;

namespace IGGrid.Samples.Display
{
    public partial class StringIndexersSupport : SampleContainer
    {
        public string[] FirstName = new string[] { "John", "Arnie", "John", "Jack", "June" };
        public string[] LastName = new string[] { "Rizzo", "Smith", "Young", "Cruz", "Lewis" };
        public string[] Severity = new string[] { "Low", "High", "Medium" };

        public StringIndexersSupport()
        {
            InitializeComponent();
            Random rand = new Random();

            var dataRows = new ObservableCollection<DataRow>();
            for (int i = 0; i < 100; i++)
            {
                DataRow dataRow = new DataRow();
                dataRow["FirstName"] = FirstName[rand.Next(FirstName.Length)];
                dataRow["LastName"] = LastName[rand.Next(LastName.Length)];
                dataRow["Location"] = "Exam " + rand.Next(10);
                dataRow["Severity"] = Severity[rand.Next(Severity.Length)];
                dataRows.Add(dataRow);
            }
            this.MyDataGrid.ItemsSource = dataRows;
        }
    }
    public class DataRow
    {
        private Dictionary<string, object> _data = new Dictionary<string, object>();
        public object this[string index]
        {
            get { return _data[index]; }
            set { _data[index] = value; }
        }
    }
}
