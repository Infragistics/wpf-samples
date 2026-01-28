using Infragistics.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Infragistics.Framework 
{
    public class VisiblityOptionGrid : Grid
    {
        public VisiblityOptionGrid()
        {
            this.AddColumn(GridUnitType.Star);
            this.AddColumn(GridUnitType.Auto);

            var label = new Label();
            label.SetBinding(Label.TextProperty, new Binding { Source = this, Path = "LabelText" });

        }

        public static readonly BindableProperty LabelTextProperty = BindableProperty.Create(
               "LabelText", typeof(string), typeof(VisiblityOptionGrid), "LabelText");
        public string LabelText
        {
            get { return (string)GetValue(LabelTextProperty); }
            set { SetValue(LabelTextProperty, value); }
        }

    }


    public class VisiblityViewCell : ViewCell
    {
        public static VisibilityToBoolConverter Converter = new VisibilityToBoolConverter();
        
        public VisiblityViewCell()
        {          
            var label = new Label();
            label.VerticalOptions = LayoutOptions.CenterAndExpand;
            label.SetBinding(Label.TextProperty, 
                new Binding { Source = this, Path = "LabelText" });
            label.SetBinding(Label.StyleProperty,
                new Binding { Source = this, Path = "LabelStyle" });

            toggle = new Switch();
            toggle.VerticalOptions = LayoutOptions.CenterAndExpand;
            toggle.SetBinding(Switch.IsToggledProperty, 
                new Binding { Source = this, Path = "ToggleValue", Converter = Converter });
            toggle.SetBinding(Switch.StyleProperty,
                new Binding { Source = this, Path = "ToggleStyle" });

            var grid = new Grid();
            grid.AddColumn(GridUnitType.Star);
            grid.AddColumn(GridUnitType.Auto);
            grid.Children.Add(label, 0, 0);
            grid.Children.Add(toggle, 1, 0);
            grid.Margin = new Thickness(10, 0);

            View = grid;            
        }

        protected Switch toggle;

        #region Properties

        public static readonly BindableProperty LabelTextProperty = BindableProperty.Create(
              "LabelText", typeof(string), typeof(VisiblityViewCell), "LabelText");
        public string LabelText
        {
            get { return (string)GetValue(LabelTextProperty); }
            set { SetValue(LabelTextProperty, value); }
        }

        public static readonly BindableProperty LabelStyleProperty = BindableProperty.Create(
           "LabelStyle", typeof(Style), typeof(VisiblityViewCell), null);
        public Style LabelStyle
        {
            get { return (Style)GetValue(LabelStyleProperty); }
            set { SetValue(LabelStyleProperty, value); }
        }


        public static readonly BindableProperty ToggleValueProperty = BindableProperty.Create(
               "ToggleValue", typeof(Visibility), typeof(VisiblityViewCell), Visibility.Visible);
        public Visibility ToggleValue
        {
            get { return (Visibility)GetValue(ToggleValueProperty); }
            set { SetValue(ToggleValueProperty, value); }
        }

        public static readonly BindableProperty ToggleStyleProperty = BindableProperty.Create(
          "ToggleStyle", typeof(Style), typeof(VisiblityViewCell), null);
        public Style ToggleStyle
        {
            get { return (Style)GetValue(ToggleStyleProperty); }
            set { SetValue(ToggleStyleProperty, value); }
        }

        //public static readonly BindableProperty ToggleValueProperty = BindableProperty.Create(
        //       "ToggleValue", typeof(bool), typeof(VisiblityViewCell), true);
        //public bool ToggleValue
        //{
        //    get { return (bool)GetValue(ToggleValueProperty); }
        //    set { SetValue(ToggleValueProperty, value); }
        //}

        #endregion


        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
              
            if (propertyName == "Parent" && Parent != null)
            {
                if (this.BindingContext == null)
                    this.BindingContext = Parent.BindingContext;
            }
        }
    }

    public class NumericViewCell : ViewCell
    {
        public static NumericConverter ValueConverter = new NumericConverter();
        public static DoubleRoundConverter SliderConverter = new DoubleRoundConverter();

        public NumericViewCell()
        {
            var label = new Label();
            //label.BackgroundColor = Colors.LightGray;
            label.VerticalOptions = LayoutOptions.CenterAndExpand;
            label.SetBinding(Label.TextProperty, new Binding { Path = "LabelText", Source = this });
            label.SetBinding(Label.StyleProperty, new Binding { Path = "LabelStyle", Source = this });

            var value = new Label();
            //value.BackgroundColor = Colors.Gray;

            value.VerticalOptions = LayoutOptions.CenterAndExpand;
            value.HorizontalOptions = LayoutOptions.EndAndExpand;
            value.SetBinding(Label.TextProperty, new Binding { Path = "ValueText", Source = this, Converter = ValueConverter });
            value.SetBinding(Label.StyleProperty, new Binding { Path = "ValueStyle", Source = this });

            Slider = new Slider();
            Slider.VerticalOptions = LayoutOptions.CenterAndExpand;
            Slider.SetBinding(Slider.ValueProperty, new Binding { Path = "SliderValue", Source = this, Converter = SliderConverter });
            Slider.SetBinding(Slider.MinimumProperty, new Binding { Path = "SliderMinimum", Source = this, });
            Slider.SetBinding(Slider.MaximumProperty, new Binding { Path = "SliderMaximum", Source = this, });
            Slider.SetBinding(Slider.StyleProperty, new Binding { Path = "SliderStyle", Source = this,  });

            var grid = new Grid();
            grid.AddColumn(GridUnitType.Auto);
            grid.AddColumn(GridUnitType.Star, .3);
            grid.AddColumn(GridUnitType.Star, .7);
            grid.Children.Add(label, 0, 0);
            grid.Children.Add(value, 1, 0);
            grid.Children.Add(Slider, 2, 0);
            grid.Margin = new Thickness(10, 0);

            View = grid;
        }

        protected Slider Slider;

        #region Label Properties

        public static readonly BindableProperty LabelTextProperty = BindableProperty.Create(
              "LabelText", typeof(string), typeof(NumericViewCell), "LabelText");
        public string LabelText
        {
            get { return (string)GetValue(LabelTextProperty); }
            set { SetValue(LabelTextProperty, value); }
        }

        public static readonly BindableProperty LabelStyleProperty = BindableProperty.Create(
           "LabelStyle", typeof(Style), typeof(NumericViewCell), null);
        public Style LabelStyle
        {
            get { return (Style)GetValue(LabelStyleProperty); }
            set { SetValue(LabelStyleProperty, value); }
        }
        #endregion

        #region Label Properties
        public static readonly BindableProperty ValueTextProperty = BindableProperty.Create(
              "ValueText", typeof(string), typeof(NumericViewCell), "ValueText");
        public string ValueText
        {
            get { return (string)GetValue(ValueTextProperty); }
            set { SetValue(ValueTextProperty, value); }
        }

        public static readonly BindableProperty ValueStyleProperty = BindableProperty.Create(
           "ValueStyle", typeof(Style), typeof(NumericViewCell), null);
        public Style ValueStyle
        {
            get { return (Style)GetValue(ValueStyleProperty); }
            set { SetValue(ValueStyleProperty, value); }
        }
        #endregion

        #region Slider Properties

        public static readonly BindableProperty SliderValueProperty = BindableProperty.Create(
               "SliderValue", typeof(double), typeof(NumericViewCell), 10.0);
        public double SliderValue
        {
            get { return (double)GetValue(SliderValueProperty); }
            set { SetValue(SliderValueProperty, value); }
        }

        public static readonly BindableProperty SliderMinimumProperty = BindableProperty.Create(
               "SliderMinimum", typeof(double), typeof(NumericViewCell), 0.0);
        public double SliderMinimum
        {
            get { return (double)GetValue(SliderMinimumProperty); }
            set { SetValue(SliderMinimumProperty, value); }
        }

        public static readonly BindableProperty SliderMaximumProperty = BindableProperty.Create(
               "SliderMaximum", typeof(double), typeof(NumericViewCell), 20.0);
        public double SliderMaximum
        {
            get { return (double)GetValue(SliderMaximumProperty); }
            set { SetValue(SliderMaximumProperty, value); }
        }

        public static readonly BindableProperty SliderStyleProperty = BindableProperty.Create(
          "SliderStyle", typeof(Style), typeof(NumericViewCell), null);
        public Style SliderStyle
        {
            get { return (Style)GetValue(SliderStyleProperty); }
            set { SetValue(SliderStyleProperty, value); }
        }

       

        #endregion


        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == "Parent" && Parent != null)
            {
                if (this.BindingContext == null)
                    this.BindingContext = Parent.BindingContext;
            }
        }
    }
}
