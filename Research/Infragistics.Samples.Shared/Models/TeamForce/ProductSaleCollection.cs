using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Infragistics.Samples.Shared.Models.TeamForce
{
    public class ProductSaleCollection : ObservableCollection<ProductSale>
    {
        public ProductSaleCollection()
        {
        }

        public ProductSaleCollection(IEnumerable<ProductSale> source)
        {
            this.Append(source);
        }

        public void Append(IEnumerable<ProductSale> source)
        {
            foreach (ProductSale item in source)
            {
                this.Add(item);
            }
        }
    }
}