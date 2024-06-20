using System.ComponentModel;

namespace IGDataGrid.Samples.Helpers
{
    public enum FoodsAll
    {
        [Description("-Not Selected-")]
        NotSelected,

        Fruits,

        Vegetables,

        Legumes,

        Meat,

        Eggs,

        [Description("Bakery and cakes")]
        BakeryCakes,

        Dairy,
    }

    public enum FoodsLimited
    {
        [Description("-Not Selected-")]
        NotSelected,

        Fruits,

        Vegetables,

        Legumes,

        [Browsable(false)]
        Meat,

        [Browsable(false)]
        Eggs,

        [Description("Bakery and cakes")]
        BakeryCakes,

        [Browsable(false)]
        Dairy,
    }

    public enum BeveragesAll
    {
        [Description("-Not Selected-")]
        NotSelected,

        [Description("Apple Juice")]
        JuiceApple,

        [Description("Orange Juice")]
        JuiceOrange,

        Lemonade,

        [Description("Coca-Cola")]
        CocaCola,

        Pepsi,

        [Description("7 Up")]
        Up7,
    }

    public enum BeveragesLimited
    {
        [Description("-Not Selected-")]
        NotSelected,

        [Description("Apple Juice")]
        JuiceApple,

        [Description("Orange Juice")]
        JuiceOrange,

        Lemonade,

        [Browsable(false)]
        [Description("Coca-Cola")]
        CocaCola,

        [Browsable(false)]
        Pepsi,

        [Browsable(false)]
        [Description("7 Up")]
        Up7,
    }
}
