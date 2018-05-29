using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Infragistics;
using Infragistics.Controls.Charts;

namespace IGExtensions.Common.Maps.StyleSelectors 
{
    public class StateStyleSelector : StyleSelector
    {
        public StateStyleSelector()
        {
            this.Rules = new ObservableCollection<StateStyleRule>();
        }
        public ObservableCollection<StateStyleRule> Rules { get; set; }
        public override Style SelectStyle(object item, DependencyObject container)
        {
            foreach (StateStyleRule rule in this.Rules)
            {
                var result = rule.EvaluateCondition(item);
                if (result != null)
                {
                    return result;
                }
            }
            return base.SelectStyle(item, container);
        }

    }
    public class StateStyleRule : PropertyValueConditionalStyleRule
    {
        public StateStyleRule()
        {
             
        }
        public override Style EvaluateCondition(object item)
        {
            var reflection = new FastReflectionHelper { PropertyName = this.ValueMemberPath };
            object propertyValue = reflection.GetPropertyValue(item);
            if (this.EvaluateConditionAgainstPropertyValue(propertyValue))
            {
                return this.ValueTrueStyle;
            }
            return ValueFalseStyle;
        }
        /// <summary>
        /// Evaluates the equality of ComparisonValue and the given value of an item
        /// </summary>
        /// <param name="value">The value to compare to ComparisonValue.</param>
        /// <returns>True if ComparisonValue equals the value argument, otherwise False.</returns>
        public override bool EvaluateConditionAgainstPropertyValue(object value)
        {
            if (value == null) return false;
            
            bool boolValue;
            var isBooleanValue = bool.TryParse(value.ToString(), out boolValue);
         
            return isBooleanValue && boolValue;
        }
        #region ComparisonValue

        public Style ValueTrueStyle { get; set; }
        public Style ValueFalseStyle { get; set; }
        #endregion

   
    }
 
    public class ComparisonStyleSelector : StyleSelector
    {
        public ComparisonStyleSelector()
        {
            this.Rules = new ObservableCollection<ComparisonStyleRule>();
        }
        public ObservableCollection<ComparisonStyleRule> Rules { get; set; }

        public override Style SelectStyle(object item, DependencyObject container)
        {
            foreach (ComparisonStyleRule rule in this.Rules)
            {
                var result = rule.EvaluateCondition(item);
                if (result != null)
                {
                    return result;
                }
            }
            return base.SelectStyle(item, container);
        }
    }

    /// <summary>
    /// Rule class used for applying conditional styling for objects which pass an equality comparison.
    /// </summary>
    public class ComparisonStyleRule : PropertyValueConditionalStyleRule
    {
        public ComparisonStyleRule()
        {
            this.ComparisonMethod = ComparisonMethod.EqualTo;
        }
        /// <summary>
        /// Evaluates the equality of ComparisonValue and the given value of an item
        /// </summary>
        /// <param name="value">The value to compare to ComparisonValue.</param>
        /// <returns>True if ComparisonValue equals the value argument, otherwise False.</returns>
        public override bool EvaluateConditionAgainstPropertyValue(object value)
        {
            if (this.ComparisonValue == null || value == null) return false;
            
            if (this.ComparisonMethod == ComparisonMethod.EqualTo)
            {
                return this.ComparisonValue.ToUpper().Equals(value.ToString().ToUpper());
            }
            if (this.ComparisonMethod == ComparisonMethod.NotEqualTo)
            {
                return !this.ComparisonValue.ToUpper().Equals(value.ToString().ToUpper());
            }
            double valueDouble, comparisonDouble;
            var isValueDouble = Double.TryParse(value.ToString(), out valueDouble);
            var isComparisonDouble = Double.TryParse(ComparisonValue, out comparisonDouble);

            if (isValueDouble && isComparisonDouble)
            {
                if (this.ComparisonMethod == ComparisonMethod.LessThan)
                {
                    return comparisonDouble < valueDouble;
                }
                if (this.ComparisonMethod == ComparisonMethod.LessOrEqualTo)
                {
                    return comparisonDouble <= valueDouble;
                }
                if (this.ComparisonMethod == ComparisonMethod.GreaterThan)
                {
                    return comparisonDouble > valueDouble;
                }
                if (this.ComparisonMethod == ComparisonMethod.GreaterOrEqualTo)
                {
                    return comparisonDouble >= valueDouble;
                }
            }
            return false;
        }
        #region ComparisonValue
        private const string ComparisonValuePropertyName = "ComparisonValue";
        /// <summary>
        /// Identifies the ComparisonValue dependency property.
        /// </summary>
        public static readonly DependencyProperty ComparisonValueProperty =
            DependencyProperty.Register(ComparisonValuePropertyName,
            typeof(string), typeof(ComparisonStyleRule),
            new PropertyMetadata(string.Empty, (sender, e) =>
            {
                /* _ */
            }));

        /// <summary>
        /// The value to compare against when evaluating items for equality.
        /// </summary>
        public string ComparisonValue
        {
            get
            {
                return (string)this.GetValue(ComparisonValueProperty);
            }
            set
            {
                this.SetValue(ComparisonValueProperty, value);
            }
        }
        #endregion

        #region ComparisonMethod
        private const string ComparisonMethodPropertyName = "ComparisonMethod";
        /// <summary>
        /// Identifies the ComparisonValue dependency property.
        /// </summary>
        public static readonly DependencyProperty ComparisonMethodProperty =
            DependencyProperty.Register(ComparisonMethodPropertyName,
            typeof(ComparisonMethod), typeof(ComparisonStyleRule),
            new PropertyMetadata(ComparisonMethod.EqualTo, (sender, e) =>
            {
                /* _ */
            }));
        /// <summary>
        /// The value to compare against when evaluating items for equality.
        /// </summary>
        public ComparisonMethod ComparisonMethod
        {
            get
            {
                return (ComparisonMethod)this.GetValue(ComparisonMethodProperty);
            }
            set
            {
                this.SetValue(ComparisonMethodProperty, value);
            }
        }
        #endregion

    }
    public enum ComparisonMethod
    {
        NotEqualTo,
        EqualTo,
        LessThan,
        LessOrEqualTo,
        GreaterThan,
        GreaterOrEqualTo,
    }
}