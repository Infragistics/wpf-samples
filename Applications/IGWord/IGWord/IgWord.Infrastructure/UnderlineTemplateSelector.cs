using Infragistics.Documents.RichText;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace IgWord.Infrastructure
{
    public class UnderlineTemplateSelector : DataTemplateSelector
    {
        public DataTemplate SingleLineTemplate { get; set; }
        public DataTemplate DoubleLineTemplate { get; set; }
        public DataTemplate ThickLineTemplate { get; set; }
        public DataTemplate DottedLineTemplate { get; set; }
        public DataTemplate DashLineTemplate { get; set; }
        public DataTemplate DotDashLineTemplate { get; set; }
        public DataTemplate DotDotDashLineTemplate { get; set; }
        public DataTemplate NoneLineTemplate { get; set; }


        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            UnderlineType type = (UnderlineType)item;

            switch (type)
            {
                case UnderlineType.Single: return SingleLineTemplate;
                case UnderlineType.Double: return DoubleLineTemplate;
                case UnderlineType.Thick: return ThickLineTemplate;
                case UnderlineType.Dotted: return DottedLineTemplate;
                case UnderlineType.Dash: return DashLineTemplate;
                case UnderlineType.DotDash: return DotDashLineTemplate;
                case UnderlineType.DotDotDash: return DotDotDashLineTemplate;
                default: return NoneLineTemplate;
            }
        }
    }
}
