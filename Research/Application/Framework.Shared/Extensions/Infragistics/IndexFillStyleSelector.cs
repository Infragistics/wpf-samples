using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Infragistics.Controls.Charts;
 
namespace Infragistics.Extensions
{
    public class IndexFillStyleSelector : StyleSelector
    {
        public IndexFillStyleSelector()
        {
            this.Rule = new EqualToConditionalStyleRule();
            this.Rule.ValueMemberPath = "Index";
        } 
        protected EqualToConditionalStyleRule Rule { get; set; }
        /// <summary>
        /// Returns the Style with fill brush that corresponds to item.Index of Brush Collection
        /// </summary>
        public override Style SelectStyle(object item, DependencyObject container)
        {
            var style = new Style { TargetType = typeof(Path) };

            for (var i = 0; i < Brushes.Count; i++)
            {
                style.Setters.Add(new Setter(Path.FillProperty, Brushes[i]));

                this.Rule.ComparisonValue = i;
                this.Rule.StyleToApply = style;
                var result = Rule.EvaluateCondition(item);
                if (result != null)
                {
                    return result;
                }

                 
            }

            style.Setters.Add(new Setter(Path.FillProperty, new SolidColorBrush(Colors.Gray)));
            return style;

            //return base.SelectStyle(item, container);
        }

        /// <summary>
        /// A collection of brushes from which to pick at random when selecting a style.
        /// </summary>
        public BrushCollection Brushes { get; set; }

    }

}