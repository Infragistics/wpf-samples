using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;


namespace System.Windows.Media
{
    public static class ColorEx
    {
        public static Color Opacity(this Color color, double aplha) 
        {
            var a = (byte)Math.Round(aplha * 255);
            return Color.FromArgb(a, color.R, color.G, color.B);
        }
    }
}

namespace Infragistics.Framework
{
    public class BrushPalleteList : List<Brush>
    {
        public BrushPalleteList()
        {
            var Red1 = Brushes.DarkRed;
            var Red2 = Brushes.Red;
            var Red3 = Brushes.DarkOrange;
            var Red4 = Brushes.Orange;
            var Red5 = Brushes.Yellow;

            var Blue1 = Brushes.DarkBlue;
            var Blue2 = Brushes.Blue;
            var Blue3 = Brushes.DodgerBlue;
            var Blue4 = Brushes.SkyBlue;
            var Blue5 = Brushes.LightBlue;

            var Purple1 = Paint.FromString("#552586").ToBrush();
            var Purple2 = Paint.FromString("#6A359C").ToBrush();
            var Purple3 = Paint.FromString("#804FB3").ToBrush();
            var Purple4 = Paint.FromString("#9969C7").ToBrush();
            var Purple5 = Paint.FromString("#B589D6").ToBrush();

            var Green1 = Brushes.DarkGreen;
            var Green2 = Brushes.Green;
            var Green3 = Brushes.Lime;
            var Green4 = Brushes.Aquamarine;
            var Green5 = Brushes.LightGreen;

            var list = new List<Brush>
            {
                Brushes.Black,
                Brushes.DimGray,
                Brushes.Gray,
                Brushes.LightGray,
                Brushes.White,

                Red1,
                Red2,
                Red3,
                Red4,
                Red5,

                Blue1,
                Blue2,
                Blue3,
                Blue4,
                Blue5,

                Purple1,
                Purple2,
                Purple3,
                Purple4,
                Purple5,

                Green1,
                Green2,
                Green3,
                Green4,
                Green5,
            };

            list.Add(Paint.LinearBrush(true, Colors.Black, Colors.White));
            list.Add(Paint.LinearBrush(true, Red1.Color, Red5.Color));
            list.Add(Paint.LinearBrush(true, Blue1.Color, Blue5.Color));
            list.Add(Paint.LinearBrush(true, Purple1.Color, Purple5.Color));
            list.Add(Paint.LinearBrush(true, Green1.Color, Green5.Color));

            list.Add(Paint.LinearBrush(false, Colors.Black, Colors.White));
            list.Add(Paint.LinearBrush(false, Red1.Color, Red5.Color));
            list.Add(Paint.LinearBrush(false, Blue1.Color, Blue5.Color));
            list.Add(Paint.LinearBrush(false, Purple1.Color, Purple5.Color));
            list.Add(Paint.LinearBrush(false, Green1.Color, Green5.Color));

            list.Add(Paint.RadialBrush(Colors.Black, Colors.White));
            list.Add(Paint.RadialBrush(Red1.Color, Red5.Color));
            list.Add(Paint.RadialBrush(Blue1.Color, Blue5.Color));
            list.Add(Paint.RadialBrush(Purple1.Color, Purple5.Color));
            list.Add(Paint.RadialBrush(Green1.Color, Green5.Color));

            list.Add(new SolidColorBrush { Color = Colors.DarkRed.Opacity(1.0) });
            list.Add(new SolidColorBrush { Color = Colors.DarkRed.Opacity(0.7) });
            list.Add(new SolidColorBrush { Color = Colors.DarkRed.Opacity(0.5) });
            list.Add(new SolidColorBrush { Color = Colors.DarkRed.Opacity(0.2) });
            list.Add(new SolidColorBrush { Color = Colors.DarkRed.Opacity(0.1) });


            var lime = Brushes.LimeGreen;
            list.Add(new SolidColorBrush { Opacity = 0.5, Color = lime.Color });
            list.Add(new SolidColorBrush { Opacity = 0.4, Color = lime.Color });
            list.Add(new SolidColorBrush { Opacity = 0.3, Color = lime.Color });
            list.Add(new SolidColorBrush { Opacity = 0.2, Color = lime.Color });
            list.Add(new SolidColorBrush { Opacity = 0.1, Color = lime.Color });
             

            list.Add(new SolidColorBrush { Opacity = 1.0, Color = Colors.Gray });
            list.Add(new SolidColorBrush { Opacity = 0.75, Color = Colors.Gray });
            list.Add(new SolidColorBrush { Opacity = 0.50, Color = Colors.Gray });
            list.Add(new SolidColorBrush { Opacity = 0.25, Color = Colors.Gray });
            list.Add(Brushes.Transparent);

            this.AddRange(list);

        }
    }

    // https://medium.com/design-bootcamp/definitive-guide-to-wpf-colors-color-spaces-color-pickers-and-creating-your-own-colors-for-mere-f480935c6e94
    public class BrushComboBox : ComboBox
    {
        public BrushComboBox()
        {
            Margin = new Thickness(0, 2, 4, 2);
            Padding = new Thickness(5, 2, 5, 2);
            MinHeight = 25;
            MinWidth = 80;
            VerticalContentAlignment = VerticalAlignment.Stretch;
            HorizontalContentAlignment = HorizontalAlignment.Stretch;
            VerticalContentAlignment = VerticalAlignment.Stretch;
            HorizontalAlignment = HorizontalAlignment.Stretch;

            var items = new BrushPalleteList();

            this.ItemsSource = items;
            this.SelectionChanged += EnumComboBox_SelectionChanged;
        }

        private void EnumComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.SelectedItem != null) //&& this.SelectedItem.ToString() == "NULL")
            {
                var b = this.SelectedItem as Brush;
                if (b.IsFullyTransparent())
                {
                    this.SelectedIndex = -1;
                    this.SelectedItem = null;
                }

            }
        }
    }

}