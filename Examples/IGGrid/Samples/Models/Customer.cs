using IGGrid.Models;

namespace IGGrid.Samples.Models
{
    public class Customer : BaseHelper<Customer>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
