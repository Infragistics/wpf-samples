using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Infragistics;
using Infragistics.Controls.Charts;

namespace IGGeographicMap.Custom.StyleSelectors 
{
    /// <summary>
    /// Represent style selector for range conditional rules
    /// </summary>
    public class RangeConditionalStyleSelector : StyleSelector
    {
        public RangeConditionalStyleSelector()
        {
            this.Rules = new RangeConditionalStyleRules();
        }
        public RangeConditionalStyleRules Rules { get; set; }

        public override Style SelectStyle(object item, DependencyObject container)
        {
            foreach (var rule in this.Rules)
            {
                Style result = rule.EvaluateCondition(item);
                if (result != null)
                {
                    return result;
                }
            }
            return base.SelectStyle(item, container);
        }
    }
    public class RangeConditionalStyleRules : ObservableCollection<RangeConditionalStyleRule>
    {

    }
    /// <summary>
    /// Rule class used for applying conditional styling for objects which pass an equality comparison.
    /// </summary>
    public class RangeConditionalStyleRule : PropertyValueConditionalStyleRule
    {

        public override Style EvaluateCondition(object item)
        {
            var frh = new FastReflectionHelper() { PropertyName = this.ValueMemberPath };
            var propertyValue = frh.GetPropertyValue(item);
            if (this.EvaluateConditionAgainstPropertyValue(propertyValue))
            {
                return this.StyleToApply;
            }
            return null;
        }
        /// <summary>
        /// Evaluates if the given value is between RangeMinValue and RangeMaxValue.
        /// </summary>
        /// <param name="value">The value to compare with the RangeMinValue and RangeMaxValue.</param>
        /// <returns>True if the value argument is between RangeMinValue and RangeMaxValue, otherwise False.</returns>
        public override bool EvaluateConditionAgainstPropertyValue(object value)
        {
            // check if the range values are valid
            bool isValidMinRange = !double.IsNaN(this.RangeMinValue);
            bool isValidMaxRange = !double.IsNaN(this.RangeMaxValue);

            if (!isValidMinRange || !isValidMaxRange)
                return false;

            bool isValidRange = this.RangeMinValue <= this.RangeMaxValue;
            if (!isValidRange)
                return false;

            // check if the value is valid
            bool isValidValue = value != null;
            if (!isValidValue)
                return false;

            double result;
            if (!double.TryParse(value.ToString(), out result)) return false;
            if (double.IsNaN(result))
                return false;

            // check if the value is between min/max range values
            bool isValueInRange = this.RangeMinValue <= result && result < this.RangeMaxValue;

            return isValueInRange;
        }

        #region RangeMinValue
        private const string RangeMinValuePropertyName = "RangeMinValue";
        /// <summary>
        /// Identifies the RangeMinValue dependency property.
        /// </summary>
        public static readonly DependencyProperty RangeMinValueProperty =
            DependencyProperty.Register(RangeMinValuePropertyName,
            typeof(double), typeof(RangeConditionalStyleRule),
            new PropertyMetadata(0.0, (sender, e) =>
            {
                /* _ */
            }));

        /// <summary>
        /// The min value to compare against when evaluating if items' values are within range.
        /// </summary>
        public double RangeMinValue
        {
            get
            {
                return (double)this.GetValue(RangeMinValueProperty);
            }
            set
            {
                this.SetValue(RangeMinValueProperty, value);
            }
        }
        #endregion

        #region RangeMaxValue
        private const string RangeMaxValuePropertyName = "RangeMaxValue";
        /// <summary>
        /// Identifies the RangeMaxValue dependency property.
        /// </summary>
        public static readonly DependencyProperty RangeMaxValueProperty =
            DependencyProperty.Register(RangeMaxValuePropertyName,
            typeof(double), typeof(RangeConditionalStyleRule),
            new PropertyMetadata(100.0, (sender, e) =>
            {
                /* _ */
            }));

        /// <summary>
        /// The max value to compare against when evaluating if items' values are within range.
        /// </summary>
        public double RangeMaxValue
        {
            get
            {
                return (double)this.GetValue(RangeMaxValueProperty);
            }
            set
            {
                this.SetValue(RangeMaxValueProperty, value);
            }
        }
        #endregion

    }



}