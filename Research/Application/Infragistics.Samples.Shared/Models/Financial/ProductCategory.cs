namespace Infragistics.Samples.Shared.Models
{
    public class ProductCategory
    {
        public ProductCategory()
        {
        }

        public string Value { get; set; }

        public int ID { get; set; }

        public enum ProductType
        {
            Red,
            Blue,
            White,
            Green,
            Oranage
        };
    }
}