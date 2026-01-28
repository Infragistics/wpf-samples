
namespace IGDataGrid.Samples.Helpers
{
    public class FavoriteFoodPerson
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public FoodsAll Food { get; set; }
        public BeveragesAll Beverage { get; set; }
    }

    public class FavoriteFoodPersonLimited
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public FoodsLimited Food { get; set; }
        public BeveragesLimited Beverage { get; set; }
    }
}
