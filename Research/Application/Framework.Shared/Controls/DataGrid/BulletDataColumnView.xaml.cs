using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infragistics.Framework; 
using Xamarin.Forms; 

namespace Infragistics.Framework
{ 
    public partial class BulletDataColumnView : ContentView 
    {
        public BulletDataColumnView()
        { 
            InitializeComponent();

            this.PropertyChanged += OnPropertyChanged;
            CompareValue = 50000;
            CompareTarget = 100000;

            // set binding context
            this.GaugeLabel.BindingContext = this;
            this.Gauge.BindingContext = this;
            foreach (var range in Gauge.Ranges)
            {
                range.BindingContext = this;
            }
        }

        void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CompareValue" ||
                e.PropertyName == "CompareTarget")
            {
                CompareTargetRatio = Calc.Ratio(CompareTarget, CompareTarget);
                CompareValueRatio = Calc.Ratio(CompareValue, CompareTarget);
                CompareTargetStart = CompareTargetRatio - 0.05;
                CompareTargetEnd = CompareTargetRatio;

                LabelText = (CompareValueRatio * 100).ToString("#") + "";
            }
        }

        public static readonly BindableProperty CompareValueProperty = BindableProperty.Create
            ("CompareValue", typeof(double), typeof(BulletDataColumnView), 0.5);
        public double CompareValue
        {
            get { return (double)GetValue(CompareValueProperty); }
            set { SetValue(CompareValueProperty, value); }
        }

        public static readonly BindableProperty CompareTargetProperty = BindableProperty.Create
            ("CompareTarget", typeof(double), typeof(BulletDataColumnView), 1.0);
        public double CompareTarget
        {
            get { return (double)GetValue(CompareTargetProperty); }
            set { SetValue(CompareTargetProperty, value); }
        }

        public static readonly BindableProperty LabelTextProperty = BindableProperty.Create
              ("LabelText", typeof(string), typeof(BulletDataColumnView), "50%");
    //        <BulletDataColumnView, string>(o => o.LabelText, "50%", BindingMode.Default);

        public string LabelText
        {
            get { return (string)GetValue(LabelTextProperty); }
            set { SetValue(LabelTextProperty, value); }
        }
        
        public static readonly BindableProperty LabelForegroundProperty = BindableProperty.Create
                  ("LabelForeground", typeof(Color), typeof(BulletDataColumnView), Colors.Black);
  //     <BulletDataColumnView, Color>(o => o.LabelForeground, Colors.Black, BindingMode.Default);

        public Color LabelForeground
        {
            get { return (Color)GetValue(LabelForegroundProperty); }
            set { SetValue(LabelForegroundProperty, value); }
        } 

        public static readonly BindableProperty LabelFontAttributesProperty = BindableProperty.Create
                    ("LabelFontAttributes", typeof(FontAttributes), typeof(BulletDataColumnView), FontAttributes.None);
        //       <BulletDataColumnView, FontAttributes>(o => o.LabelFontAttributes, FontAttributes.None, BindingMode.Default);

        public FontAttributes LabelFontAttributes
        {
            get { return (FontAttributes)GetValue(LabelFontAttributesProperty); }
            set { SetValue(LabelFontAttributesProperty, value); }
        }

        public static readonly BindableProperty LabelFontSizeProperty =
            BindableProperty.Create("LabelFontSize", typeof(double),
            typeof(BulletDataColumnView), -1.0, BindingMode.OneWay, null, null, null, null,
            (bindable) => Device.GetNamedSize(NamedSize.Default, (BulletDataColumnView)bindable));
      
        [TypeConverter(typeof(FontSizeConverter))]
        public double LabelFontSize
        {
            get { return (double)GetValue(LabelFontSizeProperty); }
            set { SetValue(LabelFontSizeProperty, value); }
        }
        
        public static readonly BindableProperty CompareTargetRatioProperty = BindableProperty.Create(
            "CompareTargetRatio", typeof(double), typeof(BulletDataColumnView), 1.0);
        public double CompareTargetRatio 
        {
            get { return (double)GetValue(CompareTargetRatioProperty); }
            private set { SetValue(CompareTargetRatioProperty, value); }
        }

        public static readonly BindableProperty CompareValueRatioProperty = BindableProperty.Create(
            "CompareValueRatio", typeof(double), typeof(BulletDataColumnView), 0.5);
        public double CompareValueRatio
        {
            get { return (double)GetValue(CompareValueRatioProperty); }
            private set { SetValue(CompareValueRatioProperty, value); }
        }
         
        public double CompareTargetStart { get; set; } //{ get { return CompareTargetRatio - 0.02; } }
        public double CompareTargetEnd { get; set; } //{ get { return CompareTargetRatio; } }
        
    }

     

}
