using System.ComponentModel;

namespace HideProperties_ValueChanged.Business
{
    public class Person
    {
        [Category("Basic")]
        public string FirstName { get; set; }

        [Category("Basic")]
        public string LastName { get; set; }

        [Category("Advanced")]
        public int Age { get; set; }

        [Category("Advanced")]
        public string Spouse { get; set; }
    }
}
