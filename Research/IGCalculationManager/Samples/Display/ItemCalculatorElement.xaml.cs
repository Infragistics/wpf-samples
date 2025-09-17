using IGCalculationManager.ViewModels;

namespace IGCalculationManager.Samples.Display
{
    public partial class ItemCalculatorElement : Infragistics.Samples.Framework.SampleContainer
    {
        public ShippingDetail CurrentShippingDetail { get; set; }

        public ItemCalculatorElement()
        {
            InitializeComponent();
            CurrentShippingDetail = new ShippingDetail("Rössle Sauerkraut", 25, (decimal)46.50, 75);
            this.DataContext = this;
        }
    }
}
