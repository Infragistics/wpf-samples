using System.Collections.ObjectModel;

namespace IGPieChart.Model
{
    public class TestData : ObservableCollection<TestDataItem>
    {
        public TestData()
        {
            Add(new TestDataItem { Label = "IBM", Value = 7 });
            Add(new TestDataItem { Label = "SONY", Value = 5 });
            Add(new TestDataItem { Label = "DELL", Value = 3 });
            Add(new TestDataItem { Label = "Apple", Value = 4 });
            Add(new TestDataItem { Label = "Hitachi", Value = 1 });
            Add(new TestDataItem { Label = "Acer", Value = 1 });
            Add(new TestDataItem { Label = "HP", Value = 2 });
            Add(new TestDataItem { Label = "Asus", Value = 2 });
            Add(new TestDataItem { Label = "Gateway", Value = 1 });
        }
    }

    public class TestDataItem
    {
        public string Label { get; set; }
        public double Value { get; set; }
    }
}
