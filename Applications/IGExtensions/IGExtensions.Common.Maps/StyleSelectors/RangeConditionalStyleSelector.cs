using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Infragistics;
using Infragistics.Controls.Charts;

namespace IGExtensions.Common.Maps.StyleSelectors 
{
    public class RangeConditionalStyleSelector : StyleSelector
    {
        public RangeConditionalStyleSelector()
        {
            this.Rules = new ObservableCollection<RangeConditionalStyleRule>();
        }
        public ObservableCollection<RangeConditionalStyleRule> Rules { get; set; }

        public override Style SelectStyle(object item, DependencyObject container)
        {
            foreach (RangeConditionalStyleRule rule in this.Rules)
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
    public class RangeConditionalStyleRule : PropertyValueConditionalStyleRule
    {

        public override Style EvaluateCondition(object item)
        {
            var frh = new FastReflectionHelper() { PropertyName = this.ValueMemberPath };
            object propertyValue = frh.GetPropertyValue(item);
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
            if (!Double.TryParse(value.ToString(), out result)) return false;
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


    public class RangeBrushScaleStyleSelector : StyleSelector
    {
        public RangeBrushScaleStyleSelector()
        {
            this.Rules = new ObservableCollection<RangeBrushScaleStyleRule>();
        }
        public ObservableCollection<RangeBrushScaleStyleRule> Rules { get; set; }



        public override Style SelectStyle(object item, DependencyObject container)
        {
            foreach (RangeBrushScaleStyleRule rule in this.Rules)
            {
                Style result = rule.EvaluateCondition(item);
                if (result != null)
                {
                    return result;
                }
            }
            return base.SelectStyle(item, container);
        }
        //TODO add a new rule class with ValueBrushScale property 
    }
    //ValueBrushScale
    /// <summary>
    /// Rule class used for applying conditional styling for objects which pass an equality comparison.
    /// </summary>
    public class RangeBrushScaleStyleRule : PropertyValueConditionalStyleRule
    {
        public RangeBrushScaleStyleRule()
        {
            this.ShapeFillScale = new ValueBrushScale() { MinimumValue = 0, MaximumValue = 10, 
                Brushes = NamedColors.Collections.BlueColors.Brushes.ToBrushCollection()};
        }

        public override Style EvaluateCondition(object item)
        {
            var frh = new FastReflectionHelper { PropertyName = this.ValueMemberPath };
            object propertyValue = frh.GetPropertyValue(item);
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
            //TODO: use ShapeFillScale to get fill brush
            //var fill = this.ShapeFillScale.GetBrushByValue()
             
            // check if the range values are valid
            bool isValidMinRange = !double.IsNaN(this.ShapeFillScale.MinimumValue);
            bool isValidMaxRange = !double.IsNaN(this.ShapeFillScale.MaximumValue);

            if (!isValidMinRange || !isValidMaxRange)
                return false;

            bool isValidRange = this.ShapeFillScale.MinimumValue <= this.ShapeFillScale.MaximumValue;
            if (!isValidRange)
                return false;

            // check if the value is valid
            bool isValidValue = value != null;
            if (!isValidValue)
                return false;

            double result;
            if (!Double.TryParse(value.ToString(), out result)) return false;
            if (double.IsNaN(result))
                return false;

            // check if the value is between min/max range values
            bool isValueInRange = this.ShapeFillScale.MinimumValue <= result && result < this.ShapeFillScale.MaximumValue;

          
            return isValueInRange;
        }



        #region Properties
        private const string ShapeFillScalePropertyName = "ShapeFillScale";
        /// <summary>
        /// Identifies the FillScale dependency property.
        /// </summary>
        public static readonly DependencyProperty ShapeFillScaleProperty =
            DependencyProperty.Register(ShapeFillScalePropertyName,
            typeof(ValueBrushScale), typeof(RangeBrushScaleStyleRule),
            new PropertyMetadata(null, (sender, e) =>
            {
                /* _ */
            }));

        /// <summary>
        /// The min value to compare against when evaluating if items' values are within range.
        /// </summary>
        public ValueBrushScale ShapeFillScale
        {
            get { return (ValueBrushScale)this.GetValue(ShapeFillScaleProperty); }
            set { this.SetValue(ShapeFillScaleProperty, value); }
        }

        private const string ShapeStylePropertyName = "ShapeStyle";
        /// <summary>
        /// Identifies the ShapeStyle dependency property.
        /// </summary>
        public static readonly DependencyProperty ShapeStyleProperty =
            DependencyProperty.Register(ShapeStylePropertyName,
            typeof(ShapeStyle), typeof(RangeBrushScaleStyleRule),
            new PropertyMetadata(ShapeStyle.ShapePath, (sender, e) =>
            {
                /* _ */
            }));

        /// <summary>
        /// The min value to compare against when evaluating if items' values are within range.
        /// </summary>
        public ShapeStyle ShapeStyle
        {
            get { return (ShapeStyle)this.GetValue(ShapeStyleProperty); }
            set { this.SetValue(ShapeStyleProperty, value); }
        }
        #endregion

        //#region RangeMaxValue
        //private const string RangeMaxValuePropertyName = "RangeMaxValue";
        ///// <summary>
        ///// Identifies the RangeMaxValue dependency property.
        ///// </summary>
        //public static readonly DependencyProperty RangeMaxValueProperty =
        //    DependencyProperty.Register(RangeMaxValuePropertyName,
        //    typeof(double), typeof(RangeConditionalStyleRule),
        //    new PropertyMetadata(100.0, (sender, e) =>
        //    {
        //        /* _ */
        //    }));

        ///// <summary>
        ///// The max value to compare against when evaluating if items' values are within range.
        ///// </summary>
        //public double RangeMaxValue
        //{
        //    get
        //    {
        //        return (double)this.GetValue(RangeMaxValueProperty);
        //    }
        //    set
        //    {
        //        this.SetValue(RangeMaxValueProperty, value);
        //    }
        //}
        //#endregion

    }
    public enum ShapeStyle
    {
        ShapePath,
        ShapeControl,
    }
}