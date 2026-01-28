using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Infragistics.Controls.Charts;

namespace IGGeographicMap.Custom.StyleSelectors 
{
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
                Style result = rule.EvaluateCondition(item);
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
        /// Evaluates the equality of ComparisonValue and the given value.
        /// </summary>
        /// <param name="value">The value to compare to ComparisonValue.</param>
        /// <returns>True if ComparisonValue equals the value argument, otherwise False.</returns>
        public override bool EvaluateConditionAgainstPropertyValue(object value)
        {
            if (this.ComparisonValue == null || value == null) return false;
            
            double result;
            bool isDouble = Double.TryParse(value.ToString(), out result);
           
            if (this.ComparisonMethod == ComparisonMethod.EqualTo)
            {
                return this.ComparisonValue.Equals(value);
            }
            //else if (this.ComparisonMethod == ComparisonMethod.GreaterOrEqualTo)
            //{

            //    if (isDouble)
            //    {
                    
            //    }
                  
            //}
            //else if (this.ComparisonMethod == ComparisonMethod.GreaterThan)
            //{

            //}
            //else if (this.ComparisonMethod == ComparisonMethod.LessOrEqualTo)
            //{

            //}
            //else if (this.ComparisonMethod == ComparisonMethod.LessThan)
            //{

            //}

            return false;
            //return this.ComparisonValue != null ? this.ComparisonValue.Equals(value) : value == null;
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