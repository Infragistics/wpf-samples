using System.Collections.Generic;

namespace Infragistics.Samples.Shared.Models
{
    public class Datum
    {
        public Datum()
        {
        }

        public string Name { get; set; }

        public double Value { get; set; }
    }

    public class DatumCollection : List<Datum>
    {
        public DatumCollection()
        {
        }

        public DatumCollection(IEnumerable<Datum> collection)
            : base(collection)
        {
        }
    }
}
