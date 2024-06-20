using Infragistics.Controls.Editors;
using System.Collections.Generic;

namespace IGPropertyGrid.Samples.DataModel
{
    public class CategoryCustomComparer : IComparer<PropertyGridCategoryItem>
    {
        public int Compare(PropertyGridCategoryItem x, PropertyGridCategoryItem y)
        {
            return string.Compare(y.DisplayName, x.DisplayName);
        }
    }

    public class PropertyCustomComparer : IComparer<PropertyGridPropertyItem>
    {
        public int Compare(PropertyGridPropertyItem x, PropertyGridPropertyItem y)
        {
            return string.Compare(y.DisplayName, x.DisplayName);
        }
    }
}
