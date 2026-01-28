using System.Collections.Generic;
using Infragistics.Samples.Shared.Models;

namespace IGGrid.Tools
{
    public class MyCustomSort : IComparer<Product>
    {
        public int Compare(Product x, Product y)
        {
            int x1 = x.UnitsInStock + x.UnitsOnOrder;
            int y1 = y.UnitsInStock + y.UnitsOnOrder;

            return x1.CompareTo(y1);
        }
    }
}
