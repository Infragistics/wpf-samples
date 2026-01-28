using Xamarin.Forms;

namespace Infragistics.Framework
{
    public class DataCell : ExContentView
    {
        public DataCell()
        {
            this.Padding = new Thickness(0,0,0,0);
            //BorderThickness = new Thickness(2);

        }
        public int Row { get; set; }
        public int Column { get; set; }

        public bool IsSelected { get; set; }

        //public Color Background { get; set; }
        //public Color Border { get; set; }
        //public Thickness BorderThickness { get; set; }

        public static readonly BindableProperty BorderThicknessProperty =
            BindableProperty.Create("BorderThickness",
            typeof(Thickness), typeof(DataCell), new Thickness(0));
        public Thickness BorderThickness
        {
            get { return (Thickness)GetValue(BorderThicknessProperty); }
            set { SetValue(BorderThicknessProperty, value); }
        }

        public static readonly BindableProperty BorderProperty =
            BindableProperty.Create("Border",
            typeof(Color), typeof(DataCell), Colors.Gray);
        public Color Border
        {
            get { return (Color)GetValue(BorderProperty); }
            set { SetValue(BorderProperty, value); }
        }

        public static readonly BindableProperty BackgroundProperty =
            BindableProperty.Create("Background",
            typeof(Color), typeof(DataCell), Colors.Gray);
        public Color Background
        {
            get { return (Color)GetValue(BackgroundProperty); }
            set { SetValue(BackgroundProperty, value); }
        }


        public static readonly BindableProperty ItemContextProperty =
            BindableProperty.Create("ItemContext",
            typeof(object), typeof(DataCell), null);
        public object ItemContext
        {
            get { return (object)GetValue(ItemContextProperty); }
            set { SetValue(ItemContextProperty, value); }
        }

    }
   
}