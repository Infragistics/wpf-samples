using System;
using System.Data;
using System.Windows;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models.Common;
using Infragistics.Windows.DataPresenter;

namespace IGDataGrid.Samples.Organization
{
    /// <summary>
    /// Interaction logic for CustomSummaryCalculators.xaml
    /// </summary>
    public partial class CustomSummaryCalculators : SampleContainer
    {
        public CustomSummaryCalculators()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(CustomSummaryCalculators_Loaded);
            this.Unloaded += new RoutedEventHandler(CustomSummaryCalculators_Unloaded);
        }

        void CustomSummaryCalculators_Unloaded(object sender, RoutedEventArgs e)
        {
            SummaryCalculator.UnRegister("Avg. StrLen.");
        }

        static CustomCalculator g_customCalculator;
        public static CustomCalculator StrLen
        {
            get
            {
                if (null == g_customCalculator)
                    g_customCalculator = new CustomCalculator();

                return g_customCalculator;
            }
        }

        void CustomSummaryCalculators_Loaded(object sender, RoutedEventArgs e)
        {
            SummaryCalculator.Register(new CustomCalculator());

            DataTable ordersTable = NWindDataSource.GetTable(NWindTable.Orders, true);
            ordersTable.DefaultView.RowFilter = "CustomerID <= 'ANTON'";

            this.XamDataGrid1.DataSource = ordersTable.DefaultView;
        }
    }
    #region CustomCalculator

    public class CustomCalculator : SummaryCalculator
    {
        public CustomCalculator()
            : base()
        {
        }
        double _sum, _squaredSum;
        int _count;

        public override void BeginCalculation(Infragistics.Windows.DataPresenter.SummaryResult summaryResult)
        {
            _squaredSum = _sum = 0;
            _count = 0;
        }
        public override bool CanProcessDataType(Type dataType)
        {
            return dataType == typeof(string);
        }
        public override void Aggregate(object dataValue, Infragistics.Windows.DataPresenter.SummaryResult summaryResult, Record record)
        {
            double tempVal;

            if (dataValue != null)
            {
                tempVal = dataValue.ToString().Length;
                _count++;
                _sum += tempVal;
                _squaredSum += tempVal * tempVal;
            }
        }
        public override object EndCalculation(Infragistics.Windows.DataPresenter.SummaryResult summaryResult)
        {
            if (_count < 2 || _sum == 0)
                return 0;

            double theAverage = _sum / _count;
            return theAverage;
        }
        public override string Name
        {
            get
            {
                return "Avg. StrLen.";
            }
        }
        public override string Description
        {
            get
            {
                return "Average String Length";
            }
        }
    }

    #endregion //CustomCalculator
}