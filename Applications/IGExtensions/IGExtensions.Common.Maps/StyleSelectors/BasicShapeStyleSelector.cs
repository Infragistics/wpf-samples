using Infragistics.Controls.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace IGExtensions.Common.Maps.StyleSelectors
{
    public class BasicShapeStyleSelector : StyleSelector
    {
        public Style PathStyle { get; set; }
        public Style ShapeControlStyle { get; set; }

        public override Style SelectStyle(object item, DependencyObject container)
        {
            if (PathStyle != null && container is Path)
                return PathStyle;
            if (ShapeControlStyle != null && container is ShapeControl)
                return ShapeControlStyle;

            return base.SelectStyle(item, container);
        }
    }
}
