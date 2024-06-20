using Infragistics.Samples.Shared.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Infragistics.Samples.Shared.Models
{
    public class FinancialDataModel
    {

    }
    public class FinancialDataCollection : ObservableCollection<FinancialDataPoint>
    {
        public FinancialDataCollection()
        {
            this.Add(new FinancialDataPoint { Spending = 20, Budget = 60, Label = ModelStrings.XWM_Series_Financial_Administration });
            this.Add(new FinancialDataPoint { Spending = 80, Budget = 40, Label = ModelStrings.XWM_Series_Financial_Sales });
            this.Add(new FinancialDataPoint { Spending = 30, Budget = 60, Label = ModelStrings.XWM_Series_Financial_IT });
            this.Add(new FinancialDataPoint { Spending = 80, Budget = 40, Label = ModelStrings.XWM_Series_Financial_Marketing });
            this.Add(new FinancialDataPoint { Spending = 40, Budget = 60, Label = ModelStrings.XWM_Series_Financial_Development });
            this.Add(new FinancialDataPoint { Spending = 60, Budget = 20, Label = ModelStrings.XWM_Series_Financial_CustomerSupport });
        }

    }
    public class FinancialDataPoint
    {
        public string Label { get; set; }
        public double Spending { get; set; }
        public double Budget { get; set; }

        public string ToolTip { get { return String.Format("{0}, {1} {2}, {3} {4}", Label, ModelStrings.XWM_Series_Financial_Spending, Spending, ModelStrings.XWM_Series_Financial_Budget, Budget); } }

    }

    public class HierarchicalFinancialDataCollection : Collection<HierarchicalFinancialDataPoint>
    {
        public HierarchicalFinancialDataCollection()
        {
            this.Add(new HierarchicalFinancialDataPoint { Spending = 35, Budget = 30, Label = ModelStrings.XWM_Series_Financial_CustomerSupport });
            this.Add(new HierarchicalFinancialDataPoint { Spending = 17, Budget = 20, Label = ModelStrings.XWM_Series_Financial_Administration });
            this.Add(new HierarchicalFinancialDataPoint { Spending = 50, Budget = 50, Label = ModelStrings.XWM_Series_Financial_Sales });
            this.Add(new HierarchicalFinancialDataPoint { Spending = 30, Budget = 30, Label = ModelStrings.XWM_Series_Financial_IT });
            this.Add(new HierarchicalFinancialDataPoint { Spending = 45, Budget = 40, Label = ModelStrings.XWM_Series_Financial_Marketing });
            this.Add(new HierarchicalFinancialDataPoint { Spending = 55, Budget = 60, Label = ModelStrings.XWM_Series_Financial_Development });

            this[0].Children.Add(new HierarchicalFinancialDataPoint { Spending = 12, Budget = 10, Label = ModelStrings.XWM_Series_Financial_CustomerSupport_Chat_Support });
            this[0].Children.Add(new HierarchicalFinancialDataPoint { Spending = 12, Budget = 10, Label = ModelStrings.XWM_Series_Financial_CustomerSupport_Mail_Support });
            this[0].Children.Add(new HierarchicalFinancialDataPoint { Spending = 11, Budget = 10, Label = ModelStrings.XWM_Series_Financial_CustomerSupport_Phone_Support });

            this[1].Children.Add(new HierarchicalFinancialDataPoint { Spending = 7, Budget = 10, Label = ModelStrings.XWM_Series_Financial_Administration_Budgeting });
            this[1].Children.Add(new HierarchicalFinancialDataPoint { Spending = 5, Budget = 6, Label = ModelStrings.XWM_Series_Financial_Administration_Planning });
            this[1].Children.Add(new HierarchicalFinancialDataPoint { Spending = 5, Budget = 4, Label = ModelStrings.XWM_Series_Financial_Administration_Staffing });

            this[2].Children.Add(new HierarchicalFinancialDataPoint { Spending = 36, Budget = 35, Label = ModelStrings.XWM_Series_Financial_Sales_B2B });
            this[2].Children.Add(new HierarchicalFinancialDataPoint { Spending = 14, Budget = 15, Label = ModelStrings.XWM_Series_Financial_Sales_B2C });

            this[3].Children.Add(new HierarchicalFinancialDataPoint { Spending = 25, Budget = 25, Label = ModelStrings.XWM_Series_Financial_IT_Infrastructure });
            this[3].Children.Add(new HierarchicalFinancialDataPoint { Spending = 5, Budget = 5, Label = ModelStrings.XWM_Series_Financial_IT_Support });

            this[4].Children.Add(new HierarchicalFinancialDataPoint { Spending = 22, Budget = 20, Label = ModelStrings.XWM_Series_Financial_Marketing_Strategic });
            this[4].Children.Add(new HierarchicalFinancialDataPoint { Spending = 18, Budget = 15, Label = ModelStrings.XWM_Series_Financial_Marketing_Operational });
            this[4].Children.Add(new HierarchicalFinancialDataPoint { Spending = 5, Budget = 5, Label = ModelStrings.XWM_Series_Financial_Marketing_Creative });

            this[5].Children.Add(new HierarchicalFinancialDataPoint { Spending = 40, Budget = 45, Label = ModelStrings.XWM_Series_Financial_Development_Research });
            this[5].Children.Add(new HierarchicalFinancialDataPoint { Spending = 15, Budget = 15, Label = ModelStrings.XWM_Series_Financial_Development_Quality_Assuarance });

        }
    }

    public class HierarchicalFinancialDataPoint : FinancialDataPoint
    {
        public HierarchicalFinancialDataPoint()
        {
            this.Children = new List<HierarchicalFinancialDataPoint>();
        }

        public List<HierarchicalFinancialDataPoint> Children { get; private set; }
    }

}