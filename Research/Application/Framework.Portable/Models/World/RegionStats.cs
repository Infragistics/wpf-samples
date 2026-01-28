using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infragistics.Framework
{
    public class RegionStats : List<RegionInfo>
    {
        public RegionStats()
        {
            Directory = new Dictionary<string, List<RegionInfo>>();

            Generate();

            foreach (var item in Directory)
            {
                var countries = item.Value;
                var pop = 0.0;
                var gdp = 0.0;
                
                foreach (var country in countries)
                {
                    pop += country.Population;
                    gdp += country.GdpTotal; 
                }

                var region = new RegionInfo();
                region.Name = item.Key;
                region.Count = countries.Count;
                region.Population = pop;
                region.GdpTotal = gdp;
                region.GdpPerCapita = (gdp * 1000000 / pop);

                this.Add(region);
            }
        }

        protected Dictionary<string, List<RegionInfo>> Directory;
          
        public void Group(RegionInfo region)
        {
            if (!Directory.ContainsKey(region.Continent))
            {
                Directory.Add(region.Continent, new List<RegionInfo>());
            }
            Directory[region.Continent].Add(region);
        }

        public void Generate()
        {
            //symbol, name, population, gdp, continent, region, income, economy
            #region Data Items
            //symbol, name, population, gdp, continent, region, income, economy
             Group(new RegionInfo("AFG", "Afghanistan", 28400000, 22270, "Asia", "Southern Asia", "Low", "Small"));
             Group(new RegionInfo("AGO", "Angola", 12799293, 110300, "Africa", "Middle Africa", "Upper", "Small"));
             Group(new RegionInfo("ALB", "Albania", 3639453, 21810, "Europe", "Southern Europe", "Lower", "Developing"));
             Group(new RegionInfo("ARE", "United Arab Emirates", 4798491, 184300, "Asia", "Western Asia", "High", "Developing"));
             Group(new RegionInfo("ARG", "Argentina", 40913584, 573900, "South America", "South America", "Upper", "Emerging", "G20"));
             Group(new RegionInfo("ARM", "Armenia", 2967004, 18770, "Asia", "Western Asia", "Lower", "Developing"));
             Group(new RegionInfo("AUS", "Australia", 21266657, 799966, "Oceania", "Australia", "High", "Developed", "nonG7"));
             Group(new RegionInfo("AUT", "Austria", 8210281, 329500, "Europe", "Western Europe", "High", "Developed", "nonG7"));
             Group(new RegionInfo("AZE", "Azerbaijan", 8238672, 77610, "Asia", "Western Asia", "Upper", "Developing"));
             Group(new RegionInfo("BDI", "Burundi", 8988091, 3102, "Africa", "Eastern Africa", "Low", "Small"));
             Group(new RegionInfo("BEL", "Belgium", 10414336, 389300, "Europe", "Western Europe", "High", "Developed", "nonG7"));
             Group(new RegionInfo("BEN", "Benin", 8791832, 12830, "Africa", "Western Africa", "Low", "Small"));
             Group(new RegionInfo("BFA", "Burkina Faso", 15746232, 17820, "Africa", "Western Africa", "Low", "Small"));
             Group(new RegionInfo("BGD", "Bangladesh", 156050883, 224000, "Asia", "Southern Asia", "Low", "Small"));
             Group(new RegionInfo("BGR", "Bulgaria", 7204687, 93750, "Europe", "Eastern Europe", "Upper", "Developed", "nonG7"));
             Group(new RegionInfo("BHS", "Bahamas", 309156, 9093, "North America", "Caribbean", "High", "Developing"));
             Group(new RegionInfo("BIH", "Bosnia and Herz.", 4613414, 29700, "Europe", "Southern Europe", "Upper", "Developing"));
             Group(new RegionInfo("BLR", "Belarus", 9648533, 114100, "Europe", "Eastern Europe", "Upper", "Developing"));
             Group(new RegionInfo("BLZ", "Belize", 307899, 2536, "North America", "Central America", "Lower", "Developing"));
             Group(new RegionInfo("BOL", "Bolivia", 9775246, 43270, "South America", "South America", "Lower", "Emerging", "G20"));
             Group(new RegionInfo("BRA", "Brazil", 198739269, 1993000, "South America", "South America", "Upper", "Emerging", "BRIC"));
             Group(new RegionInfo("BRN", "Brunei", 388190, 20250, "Asia", "South-Eastern Asia", "High", "Developing"));
             Group(new RegionInfo("BTN", "Bhutan", 691141, 3524, "Asia", "Southern Asia", "Lower", "Small"));
             Group(new RegionInfo("BWA", "Botswana", 1990876, 27060, "Africa", "Southern Africa", "Upper", "Developing"));
             Group(new RegionInfo("CAF", "Central African Rep.", 4511488, 3198, "Africa", "Middle Africa", "Low", "Small"));
             Group(new RegionInfo("CAN", "Canada", 33487208, 1300000, "North America", "Northern America", "High", "Developed", "G7"));
             Group(new RegionInfo("CHN", "China", 1346234010, 7972802, "Asia", "Eastern Asia", "Upper", "Emerging", "BRIC"));
             Group(new RegionInfo("CHE", "Switzerland", 7604467, 316700, "Europe", "Western Europe", "High", "Developed", "nonG7"));
             Group(new RegionInfo("CHL", "Chile", 16601707, 244500, "South America", "South America", "Upper", "Emerging", "G20"));
             Group(new RegionInfo("CIV", "C?te d'Ivoire", 20617068, 33850, "Africa", "Western Africa", "Lower", "Developing"));
             Group(new RegionInfo("CMR", "Cameroon", 18879301, 42750, "Africa", "Middle Africa", "Lower", "Developing"));
             Group(new RegionInfo("COD", "Dem. Rep. Congo", 68692542, 20640, "Africa", "Middle Africa", "Low", "Small"));
             Group(new RegionInfo("COG", "Congo", 4012809, 15350, "Africa", "Middle Africa", "Lower", "Developing"));
             Group(new RegionInfo("COL", "Colombia", 45644023, 395400, "South America", "South America", "Upper", "Developing"));
             Group(new RegionInfo("CRI", "Costa Rica", 4253877, 48320, "North America", "Central America", "Upper", "Emerging", "G20"));
             Group(new RegionInfo("CUB", "Cuba", 11451652, 108200, "North America", "Caribbean", "Upper", "Emerging", "G20"));
             Group(new RegionInfo("CYN", "N. Cyprus", 265100, 3600, "Asia", "Western Asia", "Upper", "Developing"));
             Group(new RegionInfo("CYP", "Cyprus", 531640, 22700, "Asia", "Western Asia", "High", "Developing"));
             Group(new RegionInfo("CZE", "Czech Rep.", 10211904, 265200, "Europe", "Eastern Europe", "High", "Developed", "nonG7"));
             Group(new RegionInfo("DEU", "Germany", 82329758, 2918000, "Europe", "Western Europe", "High", "Developed", "G7"));
             Group(new RegionInfo("DJI", "Djibouti", 516055, 1885, "Africa", "Eastern Africa", "Lower", "Small"));
             Group(new RegionInfo("DNK", "Denmark", 6069660, 205700, "Europe", "Northern Europe", "High", "Developed", "nonG7"));
             Group(new RegionInfo("DOM", "Dominican Rep.", 9650054, 78000, "North America", "Caribbean", "Upper", "Developing"));
             Group(new RegionInfo("DZA", "Algeria", 34178188, 232900, "Africa", "Northern Africa", "Upper", "Developing"));
             Group(new RegionInfo("ECU", "Ecuador", 14573101, 107700, "South America", "South America", "Upper", "Developing"));
             Group(new RegionInfo("EGY", "Egypt", 83082869, 443700, "Africa", "Northern Africa", "Lower", "Emerging", "G20"));
             Group(new RegionInfo("ERI", "Eritrea", 5647168, 3945, "Africa", "Eastern Africa", "Low", "Small"));
             Group(new RegionInfo("ESP", "Spain", 40525002, 1403000, "Europe", "Southern Europe", "High", "Developed", "nonG7"));
             Group(new RegionInfo("EST", "Estonia", 1299371, 27410, "Europe", "Northern Europe", "High", "Developed", "nonG7"));
             Group(new RegionInfo("ETH", "Ethiopia", 85237338, 68770, "Africa", "Eastern Africa", "Low", "Small"));
             Group(new RegionInfo("FIL", "Finland", 5277428, 195063, "Europe", "Northern Europe", "High", "Developed", "nonG7"));
             Group(new RegionInfo("FJI", "Fiji", 944720, 3579, "Oceania", "Melanesia", "Lower", "Developing"));
             Group(new RegionInfo("FRN", "France", 64638750, 2136605, "Europe", "Western Europe", "High", "Developed", "G7"));
             Group(new RegionInfo("GAB", "Gabon", 1514993, 21110, "Africa", "Middle Africa", "Upper", "Developing"));
             Group(new RegionInfo("UK ", "United Kingdom", 62741198, 1997889, "Europe", "Northern Europe", "High", "Developed", "G7"));
             Group(new RegionInfo("GEO", "Georgia", 4615807, 21510, "Asia", "Western Asia", "Lower", "Developing"));
             Group(new RegionInfo("GHA", "Ghana", 23832495, 34200, "Africa", "Western Africa", "Lower", "Developing"));
             Group(new RegionInfo("GIN", "Guinea", 10057975, 10600, "Africa", "Western Africa", "Low", "Small"));
             Group(new RegionInfo("GMB", "Gambia", 1782893, 2272, "Africa", "Western Africa", "Low", "Small"));
             Group(new RegionInfo("GNB", "Guinea-Bissau", 1533964, 904, "Africa", "Western Africa", "Low", "Small"));
             Group(new RegionInfo("GNQ", "Eq. Guinea", 650702, 14060, "Africa", "Middle Africa", "High", "Small"));
             Group(new RegionInfo("GRC", "Greece", 10737428, 343000, "Europe", "Southern Europe", "High", "Developed", "nonG7"));
             Group(new RegionInfo("GTM", "Guatemala", 13276517, 68580, "North America", "Central America", "Lower", "Developing"));
             Group(new RegionInfo("GUY", "Guyana", 772298, 2966, "South America", "South America", "Lower", "Developing"));
             Group(new RegionInfo("HND", "Honduras", 7792854, 33720, "North America", "Central America", "Lower", "Developing"));
             Group(new RegionInfo("HRV", "Croatia", 4489409, 82390, "Europe", "Southern Europe", "High", "Developed", "nonG7"));
             Group(new RegionInfo("HTI", "Haiti", 9035536, 11500, "North America", "Caribbean", "Low", "Small"));
             Group(new RegionInfo("HUN", "Hungary", 9905596, 196600, "Europe", "Eastern Europe", "High", "Developed", "nonG7"));
             Group(new RegionInfo("IDN", "Indonesia", 240271522, 914600, "Asia", "South-Eastern Asia", "Lower", "Emerging", "MIKT"));
             Group(new RegionInfo("IND", "India", 1166079220, 3297000, "Asia", "Southern Asia", "Lower", "Emerging", "BRIC"));
             Group(new RegionInfo("IRL", "Ireland", 4203200, 188400, "Europe", "Northern Europe", "High", "Developed", "nonG7"));
             Group(new RegionInfo("IRN", "Iran", 66429284, 841700, "Asia", "Southern Asia", "Upper", "Emerging", "G20"));
             Group(new RegionInfo("IRQ", "Iraq", 31129225, 103900, "Asia", "Western Asia", "Lower", "Developing"));
             Group(new RegionInfo("ISL", "Iceland", 306694, 12710, "Europe", "Northern Europe", "High", "Developed", "nonG7"));
             Group(new RegionInfo("ISR", "Israel", 7233701, 201400, "Asia", "Western Asia", "High", "Developed", "nonG7"));
             Group(new RegionInfo("ITA", "Italy", 58126212, 1823000, "Europe", "Southern Europe", "High", "Developed", "G7"));
             Group(new RegionInfo("JAM", "Jamaica", 2825928, 20910, "North America", "Caribbean", "Upper", "Developing"));
             Group(new RegionInfo("JOR", "Jordan", 6342948, 31610, "Asia", "Western Asia", "Upper", "Developing"));
             Group(new RegionInfo("JPN", "Japan", 127078679, 4329000, "Asia", "Eastern Asia", "High", "Developed", "G7"));
             Group(new RegionInfo("KAZ", "Kazakhstan", 15399437, 175800, "Asia", "Central Asia", "Upper", "Developing"));
             Group(new RegionInfo("KEN", "Kenya", 39002772, 61510, "Africa", "Eastern Africa", "Low", "Emerging", "G20"));
             Group(new RegionInfo("KGZ", "Kyrgyzstan", 5431747, 11610, "Asia", "Central Asia", "Low", "Developing"));
             Group(new RegionInfo("KHM", "Cambodia", 14494293, 27940, "Asia", "South-Eastern Asia", "Low", "Small"));
             Group(new RegionInfo("KOR", "Korea", 48508972, 1335000, "Asia", "Eastern Asia", "High", "Emerging", "MIKT"));
             Group(new RegionInfo("KOS", "Kosovo", 1804838, 5352, "Europe", "Southern Europe", "Lower", "Developing"));
             Group(new RegionInfo("KWT", "Kuwait", 2691158, 149100, "Asia", "Western Asia", "High", "Developing"));
             Group(new RegionInfo("LAO", "Lao PDR", 6834942, 13980, "Asia", "South-Eastern Asia", "Lower", "Small"));
             Group(new RegionInfo("LBN", "Lebanon", 4017095, 44060, "Asia", "Western Asia", "Upper", "Developing"));
             Group(new RegionInfo("LBR", "Liberia", 3441790, 1526, "Africa", "Western Africa", "Low", "Small"));
             Group(new RegionInfo("LBY", "Libya", 6310434, 88830, "Africa", "Northern Africa", "Upper", "Developing"));
             Group(new RegionInfo("LKA", "Sri Lanka", 21324791, 91870, "Asia", "Southern Asia", "Lower", "Developing"));
             Group(new RegionInfo("LSO", "Lesotho", 2130819, 3293, "Africa", "Southern Africa", "Lower", "Small"));
             Group(new RegionInfo("LTU", "Lithuania", 3555179, 63330, "Europe", "Northern Europe", "Upper", "Developed", "nonG7"));
             Group(new RegionInfo("LUX", "Luxembourg", 491775, 39370, "Europe", "Western Europe", "High", "Developed", "nonG7"));
             Group(new RegionInfo("LVA", "Latvia", 2231503, 38860, "Europe", "Northern Europe", "Upper", "Developed", "nonG7"));
             Group(new RegionInfo("MAR", "Morocco", 34859364, 136600, "Africa", "Northern Africa", "Lower", "Developing"));
             Group(new RegionInfo("MDA", "Moldova", 4320748, 10670, "Europe", "Eastern Europe", "Lower", "Developing"));
             Group(new RegionInfo("MDG", "Madagascar", 20653556, 20130, "Africa", "Eastern Africa", "Low", "Small"));
             Group(new RegionInfo("MEX", "Mexico", 111211789, 1563000, "North America", "Central America", "Upper", "Emerging", "MIKT"));
             Group(new RegionInfo("MKD", "Macedonia", 2066718, 18780, "Europe", "Southern Europe", "Low", "Developing"));
             Group(new RegionInfo("MLI", "Mali", 12666987, 14590, "Africa", "Western Africa", "Low", "Small"));
             Group(new RegionInfo("MMR", "Myanmar", 48137741, 55130, "Asia", "South-Eastern Asia", "Low", "Small"));
             Group(new RegionInfo("MNE", "Montenegro", 672180, 6816, "Europe", "Southern Europe", "Upper", "Developing"));
             Group(new RegionInfo("MNG", "Mongolia", 3041142, 9476, "Asia", "Eastern Asia", "Lower", "Developing"));
             Group(new RegionInfo("MOZ", "Mozambique", 21669278, 18940, "Africa", "Eastern Africa", "Low", "Small"));
             Group(new RegionInfo("MRT", "Mauritania", 3129486, 6308, "Africa", "Western Africa", "Low", "Small"));
             Group(new RegionInfo("MWI", "Malawi", 14268711, 11810, "Africa", "Eastern Africa", "Low", "Small"));
             Group(new RegionInfo("MYS", "Malaysia", 25715819, 384300, "Asia", "South-Eastern Asia", "Upper", "Developing"));
             Group(new RegionInfo("NAM", "Namibia", 2108665, 13250, "Africa", "Southern Africa", "Upper", "Developing"));
             Group(new RegionInfo("NER", "Niger", 15306252, 10040, "Africa", "Western Africa", "Low", "Small"));
             Group(new RegionInfo("NGA", "Nigeria", 149229090, 335400, "Africa", "Western Africa", "Lower", "Emerging", "G20"));
             Group(new RegionInfo("NIC", "Nicaragua", 5891199, 16790, "North America", "Central America", "Lower", "Developing"));
             Group(new RegionInfo("NLD", "Netherlands", 16998259, 677496, "Europe", "Western Europe", "High", "Developed", "nonG7"));
             Group(new RegionInfo("NOR", "Norway", 4676305, 276400, "Europe", "Northern Europe", "High", "Developed", "nonG7"));
             Group(new RegionInfo("NPL", "Nepal", 28563377, 31080, "Asia", "Southern Asia", "Low", "Small"));
             Group(new RegionInfo("NZL", "New Zealand", 4242593, 116559, "Oceania", "Australia", "High", "Developed", "G7"));
             Group(new RegionInfo("OMN", "Oman", 3418085, 66980, "Asia", "Western Asia", "High", "Developing"));
             Group(new RegionInfo("PAK", "Pakistan", 176242949, 427300, "Asia", "Southern Asia", "Lower", "Emerging", "G20"));
             Group(new RegionInfo("PAN", "Panama", 3360474, 38830, "North America", "Central America", "Upper", "Developing"));
             Group(new RegionInfo("PER", "Peru", 29546963, 247300, "South America", "South America", "Upper", "Emerging", "G20"));
             Group(new RegionInfo("PHL", "Philippines", 97976603, 317500, "Asia", "South-Eastern Asia", "Lower", "Emerging", "G20"));
             Group(new RegionInfo("PNG", "Papua New Guinea", 6057263, 13210, "Oceania", "Melanesia", "Lower", "Developing"));
             Group(new RegionInfo("POL", "Poland", 38482919, 667900, "Europe", "Eastern Europe", "High", "Developed", "nonG7"));
             Group(new RegionInfo("PRK", "Dem. Rep. Korea", 22665345, 40000, "Asia", "Eastern Asia", "Low", "Small"));
             Group(new RegionInfo("PRT", "Portugal", 10707924, 208627, "Europe", "Southern Europe", "High", "Developed", "nonG7"));
             Group(new RegionInfo("PRY", "Paraguay", 6995655, 28890, "South America", "South America", "Lower", "Emerging", "G20"));
             Group(new RegionInfo("QAT", "Qatar", 833285, 91330, "Asia", "Western Asia", "High", "Developing"));
             Group(new RegionInfo("ROU", "Romania", 22215421, 271400, "Europe", "Eastern Europe", "Upper", "Developed", "nonG7"));
             Group(new RegionInfo("RUS", "Russia", 140041247, 2266000, "Europe", "Eastern Europe", "Upper", "Emerging", "BRIC"));
             Group(new RegionInfo("RWA", "Rwanda", 10473282, 9706, "Africa", "Eastern Africa", "Low", "Small"));
             Group(new RegionInfo("SAH", "W. Sahara", 0, 0, "Africa", "Northern Africa", "Low", "Small"));
             Group(new RegionInfo("SAU", "Saudi Arabia", 28686633, 576500, "Asia", "Western Asia", "High", "Developed", "nonG7"));
             Group(new RegionInfo("SDN", "Sudan", 25946220, 88080, "Africa", "Northern Africa", "Lower", "Developing"));
             Group(new RegionInfo("SDS", "S. Sudan", 10625176, 13227, "Africa", "Eastern Africa", "Low", "Small"));
             Group(new RegionInfo("SEN", "Senegal", 13711597, 21980, "Africa", "Western Africa", "Lower", "Small"));
             Group(new RegionInfo("SLB", "Solomon Is.", 595613, 1078, "Oceania", "Melanesia", "Lower", "Small"));
             Group(new RegionInfo("SLE", "Sierra Leone", 6440053, 4285, "Africa", "Western Africa", "Low", "Small"));
             Group(new RegionInfo("SLV", "El Salvador", 7185218, 43630, "North America", "Central America", "Lower", "Developing"));
             Group(new RegionInfo("SOL", "Somaliland", 3500000, 12250, "Africa", "Eastern Africa", "Lower", "Developing"));
             Group(new RegionInfo("SOM", "Somalia", 9832017, 5524, "Africa", "Eastern Africa", "Low", "Small"));
             Group(new RegionInfo("SRB", "Serbia", 7379339, 80340, "Europe", "Southern Europe", "Upper", "Developing"));
             Group(new RegionInfo("SUR", "Suriname", 481267, 4254, "South America", "South America", "Upper", "Developing"));
             Group(new RegionInfo("SVK", "Slovakia", 5463046, 119500, "Europe", "Eastern Europe", "High", "Developed", "nonG7"));
             Group(new RegionInfo("SVN", "Slovenia", 2005692, 59340, "Europe", "Southern Europe", "High", "Developed", "nonG7"));
             Group(new RegionInfo("SWE", "Sweden", 9059651, 344300, "Europe", "Northern Europe", "High", "Developed", "nonG7"));
             Group(new RegionInfo("SWZ", "Swaziland", 1123913, 5702, "Africa", "Southern Africa", "Lower", "Developing"));
             Group(new RegionInfo("SYR", "Syria", 20178485, 98830, "Asia", "Western Asia", "Lower", "Developing"));
             Group(new RegionInfo("TCD", "Chad", 10329208, 15860, "Africa", "Middle Africa", "Low", "Small"));
             Group(new RegionInfo("TGO", "Togo", 6019877, 5118, "Africa", "Western Africa", "Low", "Small"));
             Group(new RegionInfo("THA", "Thailand", 65905410, 547400, "Asia", "South-Eastern Asia", "Upper", "Emerging", "G20"));
             Group(new RegionInfo("TJK", "Tajikistan", 7349145, 13160, "Asia", "Central Asia", "Low", "Developing"));
             Group(new RegionInfo("TKM", "Turkmenistan", 4884887, 29780, "Asia", "Central Asia", "Upper", "Developing"));
             Group(new RegionInfo("TLS", "Timor-Leste", 1131612, 2520, "Asia", "South-Eastern Asia", "Lower", "Small"));
             Group(new RegionInfo("TTO", "Trinidad and Tobago", 1310000, 29010, "North America", "Caribbean", "High", "Developing"));
             Group(new RegionInfo("TUN", "Tunisia", 10486339, 81710, "Africa", "Northern Africa", "Upper", "Developing"));
             Group(new RegionInfo("TUR", "Turkey", 76805524, 902700, "Asia", "Western Asia", "Upper", "Emerging", "MIKT"));
             Group(new RegionInfo("TWN", "Taiwan", 22974347, 712000, "Asia", "Eastern Asia", "High", "Developed", "nonG7"));
             Group(new RegionInfo("TZA", "Tanzania", 41048532, 54250, "Africa", "Eastern Africa", "Low", "Small"));
             Group(new RegionInfo("UGA", "Uganda", 32369558, 39380, "Africa", "Eastern Africa", "Low", "Small"));
             Group(new RegionInfo("UKR", "Ukraine", 45700395, 339800, "Europe", "Eastern Europe", "Lower", "Developing"));
             Group(new RegionInfo("URY", "Uruguay", 3494382, 43160, "South America", "South America", "Upper", "Emerging", "G20"));
             Group(new RegionInfo("USA", "United States", 318386466, 15169683, "North America", "Northern America", "High", "Developed", "G7"));
             Group(new RegionInfo("UZB", "Uzbekistan", 27606007, 71670, "Asia", "Central Asia", "Lower", "Developing"));
             Group(new RegionInfo("VEN", "Venezuela", 26814843, 357400, "South America", "South America", "Upper", "Emerging", "G20"));
             Group(new RegionInfo("VNM", "Vietnam", 86967524, 241700, "Asia", "South-Eastern Asia", "Lower", "Emerging", "G20"));
             Group(new RegionInfo("VUT", "Vanuatu", 218519, 989, "Oceania", "Melanesia", "Lower", "Small"));
             Group(new RegionInfo("YEM", "Yemen", 23822783, 55280, "Asia", "Western Asia", "Lower", "Small"));
             Group(new RegionInfo("ZAF", "South Africa", 49052489, 491000, "Africa", "Southern Africa", "Upper", "Emerging", "G20"));
             Group(new RegionInfo("ZMB", "Zambia", 11862740, 17500, "Africa", "Eastern Africa", "Lower", "Small"));
             Group(new RegionInfo("ZWE", "Zimbabwe", 12619600, 9323, "Africa", "Eastern Africa", "Low", "Emerging", "G20"));

            #endregion
            
        }

    }
    public class RegionInfo
    {
        public RegionInfo()
        {

        }
        public RegionInfo(string symbol, string name,
            double population, double gdp,
            string Continent, string region,
            string income, string economy, string org = null)
        {
            this.Symbol = symbol;
            this.Name = name;
            this.Continent = Continent;
            this.Region = region;
            this.Income = income;
            this.Economy = economy;
            this.Organization = org;

            this.Population = population;
            this.GdpTotal = gdp / 1000;
            this.GdpPerCapita = (gdp * 1000000 / population);
        }

        public string Symbol { get; set; }
        public string Name { get; set; }
        public string Continent { get; set; }
        public string Region { get; set; }
        public string Income { get; set; }
        public string Economy { get; set; }
        public string Organization { get; set; }
        public double Population { get; set; }
        public double GdpPerCapita { get; set; }
        public double GdpTotal { get; set; }

        public int Count { get; set; }

        public new string ToString()
        {
            var str = "";

            return str;
        }
    }

}
