using Infragistics.XamarinForms;
using System;
using System.Collections.Generic;
using System.Globalization;

#if Xamarin
using Xamarin.Forms;
#else
using System.Windows;
#endif

namespace Infragistics.Framework
{ 
    public class StringMatchToColorConverter : IValueConverter
    {
        public StringMatchToColorConverter()
        {
            Rules = new StringMatchRules();
        }
        public StringMatchRules Rules { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return Colors.Black;

            var str = value.ToString();
            foreach (var rule in Rules)
            {
                if (rule.IsMatching(str))
                {
                    return rule.Color;
                }
            }
            return Colors.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        } 
    }
    public class StringMatchToBrushConverter : IValueConverter
    {
        public StringMatchToBrushConverter()
        {
            Rules = new StringMatchRules();
        }
        public StringMatchRules Rules { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var color = Colors.Black;
            if (value != null)
            {
                var str = value.ToString();
                foreach (var rule in Rules)
                {
                    if (rule.IsMatching(str))
                    {
                        color = rule.Color;
                        break;
                    }
                }
            } 
            return new SolidColorBrush(color);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class StringMatchRules : List<StringMatchRule>
    {

    }
    public class StringMatchRule
    {
        public StringMatchRule()
        {
            Color = Colors.Black;
            Method = StringMatchMethod.Contains;
        }
        /// <summary> Gets or sets PropertyName </summary>
        public string Text { get; set; }

        public Color Color { get; set; }
         
        public StringMatchMethod Method { get; set; }

        public bool IsMatching(string str)
        {
            if (string.IsNullOrEmpty(str)) return false;
            if (string.IsNullOrEmpty(Text)) return false;

            if (Method == StringMatchMethod.Equals) return str == Text;
            if (Method == StringMatchMethod.StartsWith) return str.StartsWith(Text);
            if (Method == StringMatchMethod.EndsWith) return str.EndsWith(Text);
            if (Method == StringMatchMethod.Contains) return str.Contains(Text);

            return false;
        }
    }
    public enum StringMatchMethod
    {
        Equals,
        Contains,
        StartsWith,
        EndsWith,
    }
    
    public class ConditionalValueConverter : IValueConverter
    {
        public ConditionalValueConverter()
        {
            Rules = new List<ConditionalRule>();
        }
        
        public List<ConditionalRule> Rules { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)  
                throw new Exception(this.GetType().Name + " cannot convert null value");

            if (Rules == null || Rules.Count == 0)
                throw new Exception(this.GetType().Name + " is missing Rules");

            foreach (var rule in Rules)
            {
                if (rule.IsMatching(value))
                { 
                    return rule.Result;
                }
            }

            throw new Exception(this.GetType().Name + " no rule is matching " + value.ToString()); 
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            foreach (var rule in Rules)
            {
                var resultString = rule.Result.ToString(); 
                if (resultString.Equals(value.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    return rule.Value;
                }
            }

            throw new Exception(this.GetType().Name + " cannot convert " + value.ToString());
        }
    }
     
    public class ConditionalRule
    {
        public ConditionalRule()
        {
            Condition = ConditionType.Equals;
        }

        public object Value { get; set; }

        public object Result { get; set; }

        public ConditionType Condition { get; set; }

        public bool IsMatching(object target)
        {
            if (Value == null) return false;
            if (target == null) return false;
            if (Result == null) return false;

            if (Condition == ConditionType.Equals) return Value.Equals(target);
            if (Condition == ConditionType.NotEquals) return !Value.Equals(target);

            return false;
        }
    }

    public enum ConditionType
    {
        Equals,
        NotEquals,
    }
    
            //<ResourceDictionary>
            //    <ic:SchedulerViewScrollDirection x:Key="HorizontalScroll">Horizontal</ic:SchedulerViewScrollDirection>
            //    <ic:SchedulerViewScrollDirection x:Key="VerticalScroll">Vertical</ic:SchedulerViewScrollDirection>
            //    <ix:ConditionalValueConverter x:Key="ScrollDirectionConverter">
            //         <ix:ConditionalValueConverter.Rules>
            //            <ix:ConditionalRule Value = "{StaticResource HorizontalScroll}" Result="True"/>
            //            <ix:ConditionalRule Value = "{StaticResource VerticalScroll}" Result="False"/>
            //        </ix:ConditionalValueConverter.Rules>
            //    </ix:ConditionalValueConverter>
            //</ResourceDictionary>
}
