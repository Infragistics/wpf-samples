using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models;
using Infragistics.Themes;
using Infragistics.Windows.DataPresenter;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq; 
using System.Windows.Controls;
using System.Windows.Data;
using Infragistics.Windows.DataPresenter.Events; 

namespace IGSparkline.Samples.Styling
{
    public partial class Themes : SampleContainer
    {
        public Themes()
        {
            InitializeComponent(); 
        }
          
        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {  
            var theme = this.ThemeSelector.SelectedItem as ThemeInfo;           
            if (theme == null) return;

            // apply selected theme 
            ThemeManager.SetTheme(this.LayoutRoot, theme.Resources);
        }

        
        private void XamDataGrid_InitializeRecord(object sender, InitializeRecordEventArgs e)
        {
            var dataGrid = e.Source as XamDataGrid;
            
            if (e.Record is DataRecord)
            {
                //get the data record and verify it is a parent row:
                var dr = (DataRecord)e.Record;
                if (dr.HasExpandableFieldRecords)
                {                   
                    foreach (ExpandableFieldRecord row in dr.ChildRecords)
                    {
                    var values = new List<double>(); 
                        var fieldLabel = row.Field.Label.ToString();
                        var fieldChildren = fieldLabel + "_Children";         

                        //go through all the child records
                        foreach (Record childRec in row.ChildRecords)
                        { 
                            var cellvalue = (childRec as DataRecord).Cells[0].Value.ToString(); 
                            values.Add(double.Parse(cellvalue)); 
                        }
                        
                        if (values.Count > 0 && dr.FieldLayout == dataGrid.FieldLayouts[0])
                        {
                            var str = string.Join("|", values);
                            var fieldChildrenCount = dataGrid.FieldLayouts[0].Fields.Count(f => f.Name == fieldChildren);
                            if (fieldChildrenCount == 1)
                            {
                                // set that string as value of the parent's Chart cell
                                dr.Cells[fieldChildren].Value = str;
                            } 
                            
                        }
                    }
                     
                }
            }
        }
    }

    
    public class SaleInventory : List<SaleProduct>
    {
        public SaleInventory()
        {
            for (int i = 0; i < 10; i++)
            {
                var product = new SaleProduct();
                 
                product.ID = "00" + i;
                product.Name = GetRandomItem(Products);
                product.SalesTotal  = (int)GetRandomNumber(120000, 150000);
                
                product.SalesMin = (int)(product.SalesTotal * 0.7);
                product.SalesMax = (int)(product.SalesTotal * 1.2);
                 
                product.ProfitMin = -10000;
                product.ProfitMax = 10000;
                product.ProfitTotal = (int)GetRandomNumber(product.ProfitMin, product.ProfitMax);

                for (int m = 0; m < 12; m++)
                {
                    var sale = GetRandomNumber(product.SalesMin, product.SalesMax);
                    var profit = GetRandomNumber(product.ProfitMin, product.ProfitMax); 
                    product.Sales.Add(sale);
                    product.Profits.Add(profit);
                } 
                this.Add(product);
            }
        }


        Random Rand = new Random();
        public double GetRandomNumber(double min, double max) {
            var num = min + (Rand.NextDouble() * (max - min));
            return Math.Round(num);
        }

        public string GetRandomItem(string[] array)
        {
            var num = this.GetRandomNumber(0, array.Length - 1); 
            var index = (int)Math.Round(num);
            return array[index];
        }
         
        string[] Countries = new string[] { "USA", "UK", "France", "Canada", "Poland",
            "Denmark", "Croatia", "Australia",  
            "Sweden", "Germany", "Japan", "Ireland",
            "Barbados", "Jamaica", "Cuba", "Spain" };

        string[] Products = new string[] { "Intel CPU", "AMD CPU",
            "Nvidia GPU", "Gigabyte GPU", "Asus GPU", "AMD GPU", "MSI GPU",
            "Corsair Memory", "Patriot Memory", "Skill Memory",
            "Samsung HDD", "WD HDD", "Seagate HDD", "Intel HDD", "Asus HDD",
            "Samsung SSD", "WD SSD", "Seagate SSD", "Intel SSD", "Asus SSD",
            "Samsung Monitor", "Asus Monitor", "LG Monitor", "HP Monitor"}; 
    }

    public class SaleProduct 
    { 
        public string ID { get; set; }
        public string Name { get; set; } 
        public int ProfitTotal { get; set; } 
        public int SalesTotal  { get; set; } 
         
        public int SalesMin { get; set; }
        public int SalesMax { get; set; }
        public int ProfitMin { get; set; }
        public int ProfitMax { get; set; }

        public List<double> Sales { get; set; }
        public List<double> Profits { get; set; }

        public SaleProduct()
        {
            Sales = new List<double>();
            Profits = new List<double>();
        }
    }

    public class SourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var record = value as DataRecord;
            //skip the first (header) cell
           
            if (record != null && record.DataItemIndex != -1 && parameter != null)
            {
                //get the value, split it and parse each to int
                //var cellValue = (string)record.Cells["Chart"].Value; 
                var fieldName = parameter.ToString();
                var cellValue = (string)record.Cells[fieldName].Value;
                //var cellValue = (string)record.Cells[0].Value;
                if (cellValue != null)
                {
                    //var chartValues = new List<double>();
                    var cellStrings = cellValue.Split('|').ToList();
                    var chartValues = cellStrings.ConvertAll(x => double.Parse(x)); 
                    return chartValues;
                }
                return new List<double>();
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
