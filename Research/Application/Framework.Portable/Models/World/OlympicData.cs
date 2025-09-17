using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Infragistics.Framework
{
    public class OlympicStats : OlympicResults
    {
        public OlympicStats()
        { 
            this.AddRange(OlympicData.Countries["United Kingdom"].Results);
        }
    } 

    public class OlympicHistory : Dictionary<int, OlympicMedals>
    {
        public OlympicResults Results { get; private set; } 
         
        public void Compute()
        {
            var list = this.Values.ToList();
            list.Sort((x, y) => SortBy.Ascending(x.Year, y.Year));
            Results = new OlympicResults(list);
            Results.Compute();
        }
    } 

    /// <summary>
    /// Represents a provider with Olympics data   
    /// </summary>
    public static class OlympicData
    {
        static OlympicData()
        {
            var csv = DataProvider.GetCsvTable("world-olympics.csv");
            var years = new List<int>();

            Medals = new OlympicHistory();
            Countries = new Dictionary<string, OlympicHistory>();
            
            foreach (var row in csv.Rows)
            {
                if (row == csv.Rows[0])
                    continue; // skip csv header

                var year = int.Parse(row[0]);
                var gold = int.Parse(row[2]);
                var silver = int.Parse(row[3]);
                var bronze = int.Parse(row[4]);

                // add Olympics results for countries that spitted
                var names = row[1].Split(';');
                foreach (var name in names)
                {
                    var item = new OlympicMedals
                    {
                        Gold = gold, Silver = silver, Bronze = bronze, 
                        Country = name, Year = year
                    };
                    
                    if (Countries.ContainsKey(item.Country))
                    {
                        if (!Countries[item.Country].ContainsKey(year))
                             Countries[item.Country].Add(year, item);
                    }
                    else
                    {
                        var history = new OlympicHistory();
                        history.Add(year, item);
                        Countries.Add(item.Country, history);
                    }
                }

                if (!years.Contains(year))
                     years.Add(year);
                
                if (!Medals.ContainsKey(year))
                     Medals.Add(year, new OlympicMedals { Year = year });                   
                
                Medals[year].Silver += silver;
                Medals[year].Bronze += bronze;
                Medals[year].Gold += gold;
            }
            
            Medals.Compute();
            //years.Add(1916);
            //years.Asdd(1940);
            //years.Add(1944);

            // ensure all countries have inizalied medals for all olympic years
            foreach (var history in Countries.Values)
            {
                foreach (var year in years)
                {
                    if (!history.ContainsKey(year))
                    {
                        history.Add(year, new OlympicMedals { Year = year });
                    }
                }
                history.Compute();
            }
            
            var usa = Countries["United States"].Results;
            var ger = Countries["Germany"].Results;
            
            var rankDictionary = new Dictionary<int, OlympicRanking>(); 
            foreach (var result in usa)
            {
                if (!rankDictionary.ContainsKey(result.Year))
                {
                    rankDictionary.Add(result.Year, new OlympicRanking());
                }
                rankDictionary[result.Year].Year = result.Year;
                rankDictionary[result.Year].USA = result.Total;
            }
            foreach (var result in ger)
            {
                if (!rankDictionary.ContainsKey(result.Year))
                {
                    rankDictionary.Add(result.Year, new OlympicRanking());
                }
                rankDictionary[result.Year].Year = result.Year;
                rankDictionary[result.Year].Germany = result.Total;
            } 

            Ranking = rankDictionary.Values.ToList();
            Ranking.Sort((x, y) => SortBy.Ascending(x.Year, y.Year)); 
        }

        /// <summary> Gets or sets results by country </summary>
        public static Dictionary<string, OlympicHistory> Countries { get; set; } 
         
        public static List<OlympicRanking> Ranking { get; set; }


        public static OlympicHistory Medals { get; set; }
    }

    public class OlympicRanking
    {
        public int Year { get; set; }
         
        public int USA { get; set; } 
        public int Germany { get; set; }
    }
}