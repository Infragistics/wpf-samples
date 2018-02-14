using Infragistics.Documents.RichText;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace IgWord.Infrastructure
{
    public class TextHighlightColorTemplateSelector : DataTemplateSelector
    {
        public DataTemplate RectangleTemplate { get; set; }
        public DataTemplate TextTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            Color color = (Color)item;

            if (color == Colors.Transparent)
            {
                return TextTemplate;
            }
            else
            {
                return RectangleTemplate;
            }
        }
    }
}
