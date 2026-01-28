using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Infragistics.Samples.Shared.Models.TeamForce
{
    public class SalesMonthCollection : ObservableCollection<SalesMonth>
    {
        public SalesMonthCollection()
        {
        }

        public SalesMonthCollection(IEnumerable<SalesMonth> source)
        {
            this.Append(source);
        }

        public void Append(IEnumerable<SalesMonth> source)
        {
            foreach (SalesMonth item in source)
            {
                this.Add(item);
            }
        }
    }
}