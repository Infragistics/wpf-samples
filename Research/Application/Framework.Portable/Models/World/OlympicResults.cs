using System.Collections.Generic;
using System.Linq;

namespace Infragistics.Framework
{
    /// <summary>
    /// Represents a list of <see cref="OlympicMedals"/> objects
    /// </summary>
    public class OlympicResults : List<OlympicMedals>
    {
        public OlympicResults()
        {
            Summary = new OlympicMedals();
        }

        public OlympicResults(IEnumerable<OlympicMedals> collection) : base(collection)
        {
            Summary = new OlympicMedals();
        }
        
        public OlympicMedals Summary { get; private set; }

        /// <summary> Gets or sets total medals' ranking using formula: 
        /// <para>3pts for each Gold, 2pts for each Silver and 1 for each Bronze medal</para></summary>
        public double TotalRanking { get { return this.Sum(item => item.Ranking); } }

        /// <summary> Gets or sets total Olympics </summary>
        public int TotalOlympics { get { return this.Count; } }

        public override string ToString()
        {
            return TotalRanking + " " + Summary.Total + " medals in " + TotalOlympics + " Olympics";
        }

        /// <summary> Gets or sets Country </summary>
        public string Country { get; set; }

        public void Compute()
        {   
            Summary.Country = this.First().Country;
            Summary.Gold = this.Sum(medals => medals.Gold);
            Summary.Silver = this.Sum(medals => medals.Silver);
            Summary.Bronze = this.Sum(medals => medals.Bronze);             
        } 
    } 
}