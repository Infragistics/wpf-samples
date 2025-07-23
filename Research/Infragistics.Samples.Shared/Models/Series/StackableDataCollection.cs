using System.Collections.ObjectModel;

namespace Infragistics.Samples.Shared.Models.Series
{
    public class StackableDataCollection : ObservableCollection<Category>
    {
        public StackableDataCollection()
        {
            Add(new Category
            {
                Region = "West",
                Model1 = 238,
                Model2 = 447,
                Model3 = 257,
            });
            Add(new Category
            {
                Region = "Mountain",
                Model1 = 225,
                Model2 = 342,
                Model3 = 285,
            });
            Add(new Category
            {
                Region = "Southwest",
                Model1 = 296,
                Model2 = 229,
                Model3 = 234,
            });
            Add(new Category
            {
                Region = "Midwest",
                Model1 = 119,
                Model2 = 206,
                Model3 = 371,
            });
            Add(new Category
            {
                Region = "Northeast",
                Model1 = 286,
                Model2 = 256,
                Model3 = 225,
            });
            Add(new Category
            {
                Region = "Southeast",
                Model1 = 288,
                Model2 = 258,
                Model3 = 523,
            });
        }
    }

    public class Category
    {
        public string Region { get; set; }
        public double Model1 { get; set; }
        public double Model2 { get; set; }
        public double Model3 { get; set; }
    }
}
