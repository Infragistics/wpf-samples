using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using IGContextMenu.Resources;

namespace IGContextMenu.Controls
{
    public class BlueRectangle : Control
    {
        public BlueRectangle() 
        {
            this.DefaultStyleKey = typeof(BlueRectangle);
            this.Resources.MergedDictionaries.Add(new ResourceDictionary(){Source= new Uri("/IGContextMenu;component/Controls/BlueRectangle.xaml", UriKind.Relative)});
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(BlueRectangle), new PropertyMetadata(MenuStrings.CM_Right_Click_Here));

        public Brush TextForeground
        {
            get { return (Brush)GetValue(TextForegroundProperty); }
            set { SetValue(TextForegroundProperty, value); }
        }

        public static readonly DependencyProperty TextForegroundProperty =
            DependencyProperty.Register("TextForeground", typeof(Brush), typeof(BlueRectangle), new PropertyMetadata(new SolidColorBrush(Colors.White)));
    }
}