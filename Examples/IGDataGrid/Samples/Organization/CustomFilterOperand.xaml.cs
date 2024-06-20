using System;
using System.Data;
using System.Windows;
using IGDataGrid.Resources;
using Infragistics.Samples.Framework;
using Infragistics.Windows.Controls;

namespace IGDataGrid.Samples.Organization
{
    /// <summary>
    /// Interaction logic for CustomFilterOperand.xaml
    /// </summary>
    public partial class CustomFilterOperand : SampleContainer
    {
        public CustomFilterOperand()
        {
            // Register the custom operands using SpecialFilterOperands.Register method
            // to integrate them with the filtering UI. The data presenter will automatically
            // display these operands as options in the filter drop-down of fields with
            // matching data type.
            // 
            // Register Odd and then Even operands.
            SpecialFilterOperands.Register(EvenOrOddOperand.Even);
            SpecialFilterOperands.Register(EvenOrOddOperand.Odd);

            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!this.Resources.Contains("SampleData"))
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("String", typeof(string));
                dt.Columns.Add("Integer", typeof(int));

                dt.Rows.Add("A", 1);
                dt.Rows.Add("B", 2);
                dt.Rows.Add("C", 3);
                dt.Rows.Add("D", 4);
                dt.Rows.Add("E", 5);

                this.Resources.Add("SampleData", dt.DefaultView);
            }
        }
    }

    /// <summary>
    /// Filter operand used to filter odd or even values.
    /// </summary>
    public class EvenOrOddOperand : SpecialFilterOperandBase
    {
        // These static instances can be used in XAML to specify initial filters.
        // 
        public static readonly EvenOrOddOperand Even = new EvenOrOddOperand(false);
        public static readonly EvenOrOddOperand Odd = new EvenOrOddOperand(true);

        private bool _isOdd;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="isOdd">Whether this instance will filter odd values or even values.</param>
        private EvenOrOddOperand(bool isOdd)
        {
            _isOdd = isOdd;
        }

        // Name of the operand. This is never displayed to the end user. It's a way to
        // identify the operand in code.
        public override string Name
        {
            get
            {
                return _isOdd ? "Odd" : "Even";
            }
        }

        // Description of the operand.
        public override object Description
        {
            get
            {
                return _isOdd
                    ? DataGridStrings.CustomFilterOperand_OddValues // "Odd values"
                    : DataGridStrings.CustomFilterOperand_EvenValues; // "Even values"
            }
        }

        // The text that gets displayed to represent this operand.
        public override object DisplayContent
        {
            get
            {
                return _isOdd
                    ? DataGridStrings.CustomFilterOperand_Odd // "Odd"
                    : DataGridStrings.CustomFilterOperand_Even; // "Even"
            }
        }


        public override bool IsMatch(ComparisonOperator comparisonOperator, object value, ConditionEvaluationContext context)
        {
            // This method will only get called for operators that we indicated as supported in
            // the SupportsOperator method.
            // 
            if (Infragistics.Windows.Controls.ComparisonOperator.Equals == comparisonOperator)
            {
                double valueAsDouble = context.CurrentValue.ValueAsDouble;
                if (!double.IsNaN(valueAsDouble))
                {
                    if (_isOdd)
                        return 1 == (int)valueAsDouble % 2;
                    else
                        return 0 == (int)valueAsDouble % 2;
                }

                // If the value is not a valid number (it's null for example), then return false
                // since it's neither odd nor even.
                // 
                return false;
            }
            else if (Infragistics.Windows.Controls.ComparisonOperator.NotEquals == comparisonOperator)
            {
                // For NotEquals, simply negate the result of Equals.
                // 
                return !this.IsMatch(Infragistics.Windows.Controls.ComparisonOperator.Equals, value, context);
            }
            else
            {
                return false;
            }
        }

        public override bool SupportsDataType(Type type)
        {
            // This operand supports int and nullable int types. Data presenter will automatically
            // show this operand in filter drop-down of fields with int and int? data types. All
            // you have to do is register the operand using SpecialFilterOperands.Register as we
            // are doing in the InitializeComponent.
            // 
            return typeof(int) == type
                || typeof(int?) == type;
        }

        public override bool SupportsOperator(Infragistics.Windows.Controls.ComparisonOperator comparisonOperator)
        {
            // Only Equals and NotEquals operators make sense for this operand. NotEquals
            // is probably not that useful for this operand however for demonstration purposes
            // we'll include it here.
            // 
            return Infragistics.Windows.Controls.ComparisonOperator.Equals == comparisonOperator
                || Infragistics.Windows.Controls.ComparisonOperator.NotEquals == comparisonOperator;
        }

        public override bool UsesAllValues
        {
            get
            {
                // NOTE: This property is only applicable if you want to create advanced operands that
                // rely on data from all records to test a value for match, for example AboveAverage.
                // 
                // UsesAllValues is used to indicate that the operand relies on all the values 
                // of the records to work. Examples of such operands would be AboveAverage, BelowAverage,
                // Top10 etc... With AboveAverage for example, to check if a value is above average,
                // we need to calculate the average of all the values. Such an operand would return true
                // from this property. It would then use the context.AllValues (context is passed into
                // the IsMatch) to calculate the average of all values and check if a particular value
                // is above the calculated average. Note that There is a way to cache the calculated 
                // value via the context.UserCache property - that way the average doesn't have to be 
                // re-calculated for every value that IsMatch will be called for. The data presenter will 
                // manage the cache and clear it when cell data changes so all you have to do is check 
                // if context.UserCache is null and if so recalculate it and cache it again on the 
                // context.UserCache.
                // 

                return false;
            }
        }
    }
}