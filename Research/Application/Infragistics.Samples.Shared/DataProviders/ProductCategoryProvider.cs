using System.Collections.Generic;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Resources;

namespace Infragistics.Samples.Shared.DataProviders
{
    public class ProductCategoryProvider : CategoryCollection
    {
        public ProductCategoryProvider()
        {
            this.AddRange(ProductCategoryProvider.SetupEnum());
        }

        internal static CategoryCollection Items;

        private static IEnumerable<ProductCategory> SetupEnum()
        {
            if (ProductCategoryProvider.Items == null)
            {
                //TODO: these strings should be loaded from xml file using the XmlDataProvider
                ProductCategoryProvider.Items = new CategoryCollection();
                Items.Add(new ProductCategory { Value = ModelStrings.XWM_ProductCategory_Beverages, ID = 1});
                Items.Add(new ProductCategory { Value = ModelStrings.XWM_ProductCategory_Condiments, ID = 2 });
                Items.Add(new ProductCategory { Value = ModelStrings.XWM_ProductCategory_Confections, ID = 3 });
                Items.Add(new ProductCategory { Value = ModelStrings.XWM_ProductCategory_DiaryProducts, ID = 4 });
                Items.Add(new ProductCategory { Value = ModelStrings.XWM_ProductCategory_Grains, ID = 5 });
                Items.Add(new ProductCategory { Value = ModelStrings.XWM_ProductCategory_Meat, ID = 6 });
                Items.Add(new ProductCategory { Value = ModelStrings.XWM_ProductCategory_Produce, ID = 7 });
                Items.Add(new ProductCategory { Value = ModelStrings.XWM_ProductCategory_Seafood, ID = 8 });
            }

            return ProductCategoryProvider.Items;
        }
    }

    public class CategoryCollection : List<ProductCategory>
    {
        public CategoryCollection()
        {
        }
    }
}