using Infragistics.Controls.Charts;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IGDataPieChart.WPF.Samples.Display
{
    public partial class OthersSlice : SampleContainer
    {        
        public OthersSlice()
        {
            InitializeComponent();

            FinancialDataCollection data = this.layoutRoot.Resources["data"] as FinancialDataCollection;
            data.Add(new FinancialDataPoint() { Label = "Others 1", Budget = 5, Spending = 5 });
            data.Add(new FinancialDataPoint() { Label = "Others 2", Budget = 5, Spending = 5 });
            data.Add(new FinancialDataPoint() { Label = "Others 3", Budget = 5, Spending = 5 });

           
        }

        
    }
}
