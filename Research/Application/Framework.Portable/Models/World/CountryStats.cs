using System;
using System.Collections.Generic; 
using System.Linq; 

namespace Infragistics.Framework 
{  
    public class CountryStats
    {
        #region Properties

        /// <summary>
        /// Gets or sets Data items  
        /// </summary>
        public List<CountryInfo> Data { get; set; }

        // optional properties with subset of Data items  
        public List<CountryInfo> Top10 { get { return Subset(10); } }
        public List<CountryInfo> Top20 { get { return Subset(20); } }
        public List<CountryInfo> Top50 { get { return Subset(50); } }

        // optional properties with filtered Data items  
        public List<CountryInfo> EconomiesSmall { get { return Filter("Small"); } }
        public List<CountryInfo> EconomiesEmerging { get { return Filter("Emerging"); } }
        public List<CountryInfo> EconomiesDeveloped { get { return Filter("Developed"); } }
        public List<CountryInfo> EconomiesDeveloping { get { return Filter("Developing"); } }

        #endregion

        public CountryStats()
        {
            GenerateData();
        }

        public void GenerateData()
        {
            Data = new List<CountryInfo>();
            #region Data Items
            //symbol, name, population, gdp, continent, region, income, economy
            Data.Add(new CountryInfo("AFG", "Afghanistan", 28400000, 22270, "Asia", "Southern Asia", "Low", "Small"));
            Data.Add(new CountryInfo("AGO", "Angola", 12799293, 110300, "Africa", "Middle Africa", "Upper", "Small"));
            Data.Add(new CountryInfo("ALB", "Albania", 3639453, 21810, "Europe", "Southern Europe", "Lower", "Developing"));
            Data.Add(new CountryInfo("ARE", "United Arab Emirates", 4798491, 184300, "Asia", "Western Asia", "High", "Developing"));
            Data.Add(new CountryInfo("ARG", "Argentina", 40913584, 573900, "South America", "South America", "Upper", "Emerging", "G20"));
            Data.Add(new CountryInfo("ARM", "Armenia", 2967004, 18770, "Asia", "Western Asia", "Lower", "Developing"));
            Data.Add(new CountryInfo("AUS", "Australia", 21266657, 799966, "Oceania", "Australia", "High", "Developed", "nonG7"));
            Data.Add(new CountryInfo("AUT", "Austria", 8210281, 329500, "Europe", "Western Europe", "High", "Developed", "nonG7"));
            Data.Add(new CountryInfo("AZE", "Azerbaijan", 8238672, 77610, "Asia", "Western Asia", "Upper", "Developing"));
            Data.Add(new CountryInfo("BDI", "Burundi", 8988091, 3102, "Africa", "Eastern Africa", "Low", "Small"));
            Data.Add(new CountryInfo("BEL", "Belgium", 10414336, 389300, "Europe", "Western Europe", "High", "Developed", "nonG7"));
            Data.Add(new CountryInfo("BEN", "Benin", 8791832, 12830, "Africa", "Western Africa", "Low", "Small"));
            Data.Add(new CountryInfo("BFA", "Burkina Faso", 15746232, 17820, "Africa", "Western Africa", "Low", "Small"));
            Data.Add(new CountryInfo("BGD", "Bangladesh", 156050883, 224000, "Asia", "Southern Asia", "Low", "Small"));
            Data.Add(new CountryInfo("BGR", "Bulgaria", 7204687, 93750, "Europe", "Eastern Europe", "Upper", "Developed", "nonG7"));
            Data.Add(new CountryInfo("BHS", "Bahamas", 309156, 9093, "North America", "Caribbean", "High", "Developing"));
            Data.Add(new CountryInfo("BIH", "Bosnia and Herz.", 4613414, 29700, "Europe", "Southern Europe", "Upper", "Developing"));
            Data.Add(new CountryInfo("BLR", "Belarus", 9648533, 114100, "Europe", "Eastern Europe", "Upper", "Developing"));
            Data.Add(new CountryInfo("BLZ", "Belize", 307899, 2536, "North America", "Central America", "Lower", "Developing"));
            Data.Add(new CountryInfo("BOL", "Bolivia", 9775246, 43270, "South America", "South America", "Lower", "Emerging", "G20"));
            Data.Add(new CountryInfo("BRA", "Brazil", 198739269, 1993000, "South America", "South America", "Upper", "Emerging", "BRIC"));
            Data.Add(new CountryInfo("BRN", "Brunei", 388190, 20250, "Asia", "South-Eastern Asia", "High", "Developing"));
            Data.Add(new CountryInfo("BTN", "Bhutan", 691141, 3524, "Asia", "Southern Asia", "Lower", "Small"));
            Data.Add(new CountryInfo("BWA", "Botswana", 1990876, 27060, "Africa", "Southern Africa", "Upper", "Developing"));
            Data.Add(new CountryInfo("CAF", "Central African Rep.", 4511488, 3198, "Africa", "Middle Africa", "Low", "Small"));
            Data.Add(new CountryInfo("CAN", "Canada", 33487208, 1300000, "North America", "Northern America", "High", "Developed", "G7"));
            Data.Add(new CountryInfo("CHN", "China", 1346234010, 7972802, "Asia", "Eastern Asia", "Upper", "Emerging", "BRIC"));
            Data.Add(new CountryInfo("CHE", "Switzerland", 7604467, 316700, "Europe", "Western Europe", "High", "Developed", "nonG7"));
            Data.Add(new CountryInfo("CHL", "Chile", 16601707, 244500, "South America", "South America", "Upper", "Emerging", "G20"));
            Data.Add(new CountryInfo("CIV", "C?te d'Ivoire", 20617068, 33850, "Africa", "Western Africa", "Lower", "Developing"));
            Data.Add(new CountryInfo("CMR", "Cameroon", 18879301, 42750, "Africa", "Middle Africa", "Lower", "Developing"));
            Data.Add(new CountryInfo("COD", "Dem. Rep. Congo", 68692542, 20640, "Africa", "Middle Africa", "Low", "Small"));
            Data.Add(new CountryInfo("COG", "Congo", 4012809, 15350, "Africa", "Middle Africa", "Lower", "Developing"));
            Data.Add(new CountryInfo("COL", "Colombia", 45644023, 395400, "South America", "South America", "Upper", "Developing"));
            Data.Add(new CountryInfo("CRI", "Costa Rica", 4253877, 48320, "North America", "Central America", "Upper", "Emerging", "G20"));
            Data.Add(new CountryInfo("CUB", "Cuba", 11451652, 108200, "North America", "Caribbean", "Upper", "Emerging", "G20"));
            Data.Add(new CountryInfo("CYN", "N. Cyprus", 265100, 3600, "Asia", "Western Asia", "Upper", "Developing"));
            Data.Add(new CountryInfo("CYP", "Cyprus", 531640, 22700, "Asia", "Western Asia", "High", "Developing"));
            Data.Add(new CountryInfo("CZE", "Czech Rep.", 10211904, 265200, "Europe", "Eastern Europe", "High", "Developed", "nonG7"));
            Data.Add(new CountryInfo("DEU", "Germany", 82329758, 2918000, "Europe", "Western Europe", "High", "Developed", "G7"));
            Data.Add(new CountryInfo("DJI", "Djibouti", 516055, 1885, "Africa", "Eastern Africa", "Lower", "Small"));
            Data.Add(new CountryInfo("DNK", "Denmark", 6069660, 205700, "Europe", "Northern Europe", "High", "Developed", "nonG7"));
            Data.Add(new CountryInfo("DOM", "Dominican Rep.", 9650054, 78000, "North America", "Caribbean", "Upper", "Developing"));
            Data.Add(new CountryInfo("DZA", "Algeria", 34178188, 232900, "Africa", "Northern Africa", "Upper", "Developing"));
            Data.Add(new CountryInfo("ECU", "Ecuador", 14573101, 107700, "South America", "South America", "Upper", "Developing"));
            Data.Add(new CountryInfo("EGY", "Egypt", 83082869, 443700, "Africa", "Northern Africa", "Lower", "Emerging", "G20"));
            Data.Add(new CountryInfo("ERI", "Eritrea", 5647168, 3945, "Africa", "Eastern Africa", "Low", "Small"));
            Data.Add(new CountryInfo("ESP", "Spain", 40525002, 1403000, "Europe", "Southern Europe", "High", "Developed", "nonG7"));
            Data.Add(new CountryInfo("EST", "Estonia", 1299371, 27410, "Europe", "Northern Europe", "High", "Developed", "nonG7"));
            Data.Add(new CountryInfo("ETH", "Ethiopia", 85237338, 68770, "Africa", "Eastern Africa", "Low", "Small"));
            Data.Add(new CountryInfo("FIL", "Finland", 5277428, 195063, "Europe", "Northern Europe", "High", "Developed", "nonG7"));
            Data.Add(new CountryInfo("FJI", "Fiji", 944720, 3579, "Oceania", "Melanesia", "Lower", "Developing"));
            Data.Add(new CountryInfo("FRN", "France", 64638750, 2136605, "Europe", "Western Europe", "High", "Developed", "G7"));
            Data.Add(new CountryInfo("GAB", "Gabon", 1514993, 21110, "Africa", "Middle Africa", "Upper", "Developing"));
            Data.Add(new CountryInfo("UK ", "United Kingdom", 62741198, 1997889, "Europe", "Northern Europe", "High", "Developed", "G7"));
            Data.Add(new CountryInfo("GEO", "Georgia", 4615807, 21510, "Asia", "Western Asia", "Lower", "Developing"));
            Data.Add(new CountryInfo("GHA", "Ghana", 23832495, 34200, "Africa", "Western Africa", "Lower", "Developing"));
            Data.Add(new CountryInfo("GIN", "Guinea", 10057975, 10600, "Africa", "Western Africa", "Low", "Small"));
            Data.Add(new CountryInfo("GMB", "Gambia", 1782893, 2272, "Africa", "Western Africa", "Low", "Small"));
            Data.Add(new CountryInfo("GNB", "Guinea-Bissau", 1533964, 904, "Africa", "Western Africa", "Low", "Small"));
            Data.Add(new CountryInfo("GNQ", "Eq. Guinea", 650702, 14060, "Africa", "Middle Africa", "High", "Small"));
            Data.Add(new CountryInfo("GRC", "Greece", 10737428, 343000, "Europe", "Southern Europe", "High", "Developed", "nonG7"));
            Data.Add(new CountryInfo("GTM", "Guatemala", 13276517, 68580, "North America", "Central America", "Lower", "Developing"));
            Data.Add(new CountryInfo("GUY", "Guyana", 772298, 2966, "South America", "South America", "Lower", "Developing"));
            Data.Add(new CountryInfo("HND", "Honduras", 7792854, 33720, "North America", "Central America", "Lower", "Developing"));
            Data.Add(new CountryInfo("HRV", "Croatia", 4489409, 82390, "Europe", "Southern Europe", "High", "Developed", "nonG7"));
            Data.Add(new CountryInfo("HTI", "Haiti", 9035536, 11500, "North America", "Caribbean", "Low", "Small"));
            Data.Add(new CountryInfo("HUN", "Hungary", 9905596, 196600, "Europe", "Eastern Europe", "High", "Developed", "nonG7"));
            Data.Add(new CountryInfo("IDN", "Indonesia", 240271522, 914600, "Asia", "South-Eastern Asia", "Lower", "Emerging", "MIKT"));
            Data.Add(new CountryInfo("IND", "India", 1166079220, 3297000, "Asia", "Southern Asia", "Lower", "Emerging", "BRIC"));
            Data.Add(new CountryInfo("IRL", "Ireland", 4203200, 188400, "Europe", "Northern Europe", "High", "Developed", "nonG7"));
            Data.Add(new CountryInfo("IRN", "Iran", 66429284, 841700, "Asia", "Southern Asia", "Upper", "Emerging", "G20"));
            Data.Add(new CountryInfo("IRQ", "Iraq", 31129225, 103900, "Asia", "Western Asia", "Lower", "Developing"));
            Data.Add(new CountryInfo("ISL", "Iceland", 306694, 12710, "Europe", "Northern Europe", "High", "Developed", "nonG7"));
            Data.Add(new CountryInfo("ISR", "Israel", 7233701, 201400, "Asia", "Western Asia", "High", "Developed", "nonG7"));
            Data.Add(new CountryInfo("ITA", "Italy", 58126212, 1823000, "Europe", "Southern Europe", "High", "Developed", "G7"));
            Data.Add(new CountryInfo("JAM", "Jamaica", 2825928, 20910, "North America", "Caribbean", "Upper", "Developing"));
            Data.Add(new CountryInfo("JOR", "Jordan", 6342948, 31610, "Asia", "Western Asia", "Upper", "Developing"));
            Data.Add(new CountryInfo("JPN", "Japan", 127078679, 4329000, "Asia", "Eastern Asia", "High", "Developed", "G7"));
            Data.Add(new CountryInfo("KAZ", "Kazakhstan", 15399437, 175800, "Asia", "Central Asia", "Upper", "Developing"));
            Data.Add(new CountryInfo("KEN", "Kenya", 39002772, 61510, "Africa", "Eastern Africa", "Low", "Emerging", "G20"));
            Data.Add(new CountryInfo("KGZ", "Kyrgyzstan", 5431747, 11610, "Asia", "Central Asia", "Low", "Developing"));
            Data.Add(new CountryInfo("KHM", "Cambodia", 14494293, 27940, "Asia", "South-Eastern Asia", "Low", "Small"));
            Data.Add(new CountryInfo("KOR", "Korea", 48508972, 1335000, "Asia", "Eastern Asia", "High", "Emerging", "MIKT"));
            Data.Add(new CountryInfo("KOS", "Kosovo", 1804838, 5352, "Europe", "Southern Europe", "Lower", "Developing"));
            Data.Add(new CountryInfo("KWT", "Kuwait", 2691158, 149100, "Asia", "Western Asia", "High", "Developing"));
            Data.Add(new CountryInfo("LAO", "Lao PDR", 6834942, 13980, "Asia", "South-Eastern Asia", "Lower", "Small"));
            Data.Add(new CountryInfo("LBN", "Lebanon", 4017095, 44060, "Asia", "Western Asia", "Upper", "Developing"));
            Data.Add(new CountryInfo("LBR", "Liberia", 3441790, 1526, "Africa", "Western Africa", "Low", "Small"));
            Data.Add(new CountryInfo("LBY", "Libya", 6310434, 88830, "Africa", "Northern Africa", "Upper", "Developing"));
            Data.Add(new CountryInfo("LKA", "Sri Lanka", 21324791, 91870, "Asia", "Southern Asia", "Lower", "Developing"));
            Data.Add(new CountryInfo("LSO", "Lesotho", 2130819, 3293, "Africa", "Southern Africa", "Lower", "Small"));
            Data.Add(new CountryInfo("LTU", "Lithuania", 3555179, 63330, "Europe", "Northern Europe", "Upper", "Developed", "nonG7"));
            Data.Add(new CountryInfo("LUX", "Luxembourg", 491775, 39370, "Europe", "Western Europe", "High", "Developed", "nonG7"));
            Data.Add(new CountryInfo("LVA", "Latvia", 2231503, 38860, "Europe", "Northern Europe", "Upper", "Developed", "nonG7"));
            Data.Add(new CountryInfo("MAR", "Morocco", 34859364, 136600, "Africa", "Northern Africa", "Lower", "Developing"));
            Data.Add(new CountryInfo("MDA", "Moldova", 4320748, 10670, "Europe", "Eastern Europe", "Lower", "Developing"));
            Data.Add(new CountryInfo("MDG", "Madagascar", 20653556, 20130, "Africa", "Eastern Africa", "Low", "Small"));
            Data.Add(new CountryInfo("MEX", "Mexico", 111211789, 1563000, "North America", "Central America", "Upper", "Emerging", "MIKT"));
            Data.Add(new CountryInfo("MKD", "Macedonia", 2066718, 18780, "Europe", "Southern Europe", "Low", "Developing"));
            Data.Add(new CountryInfo("MLI", "Mali", 12666987, 14590, "Africa", "Western Africa", "Low", "Small"));
            Data.Add(new CountryInfo("MMR", "Myanmar", 48137741, 55130, "Asia", "South-Eastern Asia", "Low", "Small"));
            Data.Add(new CountryInfo("MNE", "Montenegro", 672180, 6816, "Europe", "Southern Europe", "Upper", "Developing"));
            Data.Add(new CountryInfo("MNG", "Mongolia", 3041142, 9476, "Asia", "Eastern Asia", "Lower", "Developing"));
            Data.Add(new CountryInfo("MOZ", "Mozambique", 21669278, 18940, "Africa", "Eastern Africa", "Low", "Small"));
            Data.Add(new CountryInfo("MRT", "Mauritania", 3129486, 6308, "Africa", "Western Africa", "Low", "Small"));
            Data.Add(new CountryInfo("MWI", "Malawi", 14268711, 11810, "Africa", "Eastern Africa", "Low", "Small"));
            Data.Add(new CountryInfo("MYS", "Malaysia", 25715819, 384300, "Asia", "South-Eastern Asia", "Upper", "Developing"));
            Data.Add(new CountryInfo("NAM", "Namibia", 2108665, 13250, "Africa", "Southern Africa", "Upper", "Developing"));
            Data.Add(new CountryInfo("NER", "Niger", 15306252, 10040, "Africa", "Western Africa", "Low", "Small"));
            Data.Add(new CountryInfo("NGA", "Nigeria", 149229090, 335400, "Africa", "Western Africa", "Lower", "Emerging", "G20"));
            Data.Add(new CountryInfo("NIC", "Nicaragua", 5891199, 16790, "North America", "Central America", "Lower", "Developing"));
            Data.Add(new CountryInfo("NLD", "Netherlands", 16998259, 677496, "Europe", "Western Europe", "High", "Developed", "nonG7"));
            Data.Add(new CountryInfo("NOR", "Norway", 4676305, 276400, "Europe", "Northern Europe", "High", "Developed", "nonG7"));
            Data.Add(new CountryInfo("NPL", "Nepal", 28563377, 31080, "Asia", "Southern Asia", "Low", "Small"));
            Data.Add(new CountryInfo("NZL", "New Zealand", 4242593, 116559, "Oceania", "Australia", "High", "Developed", "G7"));
            Data.Add(new CountryInfo("OMN", "Oman", 3418085, 66980, "Asia", "Western Asia", "High", "Developing"));
            Data.Add(new CountryInfo("PAK", "Pakistan", 176242949, 427300, "Asia", "Southern Asia", "Lower", "Emerging", "G20"));
            Data.Add(new CountryInfo("PAN", "Panama", 3360474, 38830, "North America", "Central America", "Upper", "Developing"));
            Data.Add(new CountryInfo("PER", "Peru", 29546963, 247300, "South America", "South America", "Upper", "Emerging", "G20"));
            Data.Add(new CountryInfo("PHL", "Philippines", 97976603, 317500, "Asia", "South-Eastern Asia", "Lower", "Emerging", "G20"));
            Data.Add(new CountryInfo("PNG", "Papua New Guinea", 6057263, 13210, "Oceania", "Melanesia", "Lower", "Developing"));
            Data.Add(new CountryInfo("POL", "Poland", 38482919, 667900, "Europe", "Eastern Europe", "High", "Developed", "nonG7"));
            Data.Add(new CountryInfo("PRK", "Dem. Rep. Korea", 22665345, 40000, "Asia", "Eastern Asia", "Low", "Small"));
            Data.Add(new CountryInfo("PRT", "Portugal", 10707924, 208627, "Europe", "Southern Europe", "High", "Developed", "nonG7"));
            Data.Add(new CountryInfo("PRY", "Paraguay", 6995655, 28890, "South America", "South America", "Lower", "Emerging", "G20"));
            Data.Add(new CountryInfo("QAT", "Qatar", 833285, 91330, "Asia", "Western Asia", "High", "Developing"));
            Data.Add(new CountryInfo("ROU", "Romania", 22215421, 271400, "Europe", "Eastern Europe", "Upper", "Developed", "nonG7"));
            Data.Add(new CountryInfo("RUS", "Russia", 140041247, 2266000, "Europe", "Eastern Europe", "Upper", "Emerging", "BRIC"));
            Data.Add(new CountryInfo("RWA", "Rwanda", 10473282, 9706, "Africa", "Eastern Africa", "Low", "Small"));
            Data.Add(new CountryInfo("SAH", "W. Sahara", 0, 0, "Africa", "Northern Africa", "Low", "Small"));
            Data.Add(new CountryInfo("SAU", "Saudi Arabia", 28686633, 576500, "Asia", "Western Asia", "High", "Developed", "nonG7"));
            Data.Add(new CountryInfo("SDN", "Sudan", 25946220, 88080, "Africa", "Northern Africa", "Lower", "Developing"));
            Data.Add(new CountryInfo("SDS", "S. Sudan", 10625176, 13227, "Africa", "Eastern Africa", "Low", "Small"));
            Data.Add(new CountryInfo("SEN", "Senegal", 13711597, 21980, "Africa", "Western Africa", "Lower", "Small"));
            Data.Add(new CountryInfo("SLB", "Solomon Is.", 595613, 1078, "Oceania", "Melanesia", "Lower", "Small"));
            Data.Add(new CountryInfo("SLE", "Sierra Leone", 6440053, 4285, "Africa", "Western Africa", "Low", "Small"));
            Data.Add(new CountryInfo("SLV", "El Salvador", 7185218, 43630, "North America", "Central America", "Lower", "Developing"));
            Data.Add(new CountryInfo("SOL", "Somaliland", 3500000, 12250, "Africa", "Eastern Africa", "Lower", "Developing"));
            Data.Add(new CountryInfo("SOM", "Somalia", 9832017, 5524, "Africa", "Eastern Africa", "Low", "Small"));
            Data.Add(new CountryInfo("SRB", "Serbia", 7379339, 80340, "Europe", "Southern Europe", "Upper", "Developing"));
            Data.Add(new CountryInfo("SUR", "Suriname", 481267, 4254, "South America", "South America", "Upper", "Developing"));
            Data.Add(new CountryInfo("SVK", "Slovakia", 5463046, 119500, "Europe", "Eastern Europe", "High", "Developed", "nonG7"));
            Data.Add(new CountryInfo("SVN", "Slovenia", 2005692, 59340, "Europe", "Southern Europe", "High", "Developed", "nonG7"));
            Data.Add(new CountryInfo("SWE", "Sweden", 9059651, 344300, "Europe", "Northern Europe", "High", "Developed", "nonG7"));
            Data.Add(new CountryInfo("SWZ", "Swaziland", 1123913, 5702, "Africa", "Southern Africa", "Lower", "Developing"));
            Data.Add(new CountryInfo("SYR", "Syria", 20178485, 98830, "Asia", "Western Asia", "Lower", "Developing"));
            Data.Add(new CountryInfo("TCD", "Chad", 10329208, 15860, "Africa", "Middle Africa", "Low", "Small"));
            Data.Add(new CountryInfo("TGO", "Togo", 6019877, 5118, "Africa", "Western Africa", "Low", "Small"));
            Data.Add(new CountryInfo("THA", "Thailand", 65905410, 547400, "Asia", "South-Eastern Asia", "Upper", "Emerging", "G20"));
            Data.Add(new CountryInfo("TJK", "Tajikistan", 7349145, 13160, "Asia", "Central Asia", "Low", "Developing"));
            Data.Add(new CountryInfo("TKM", "Turkmenistan", 4884887, 29780, "Asia", "Central Asia", "Upper", "Developing"));
            Data.Add(new CountryInfo("TLS", "Timor-Leste", 1131612, 2520, "Asia", "South-Eastern Asia", "Lower", "Small"));
            Data.Add(new CountryInfo("TTO", "Trinidad and Tobago", 1310000, 29010, "North America", "Caribbean", "High", "Developing"));
            Data.Add(new CountryInfo("TUN", "Tunisia", 10486339, 81710, "Africa", "Northern Africa", "Upper", "Developing"));
            Data.Add(new CountryInfo("TUR", "Turkey", 76805524, 902700, "Asia", "Western Asia", "Upper", "Emerging", "MIKT"));
            Data.Add(new CountryInfo("TWN", "Taiwan", 22974347, 712000, "Asia", "Eastern Asia", "High", "Developed", "nonG7"));
            Data.Add(new CountryInfo("TZA", "Tanzania", 41048532, 54250, "Africa", "Eastern Africa", "Low", "Small"));
            Data.Add(new CountryInfo("UGA", "Uganda", 32369558, 39380, "Africa", "Eastern Africa", "Low", "Small"));
            Data.Add(new CountryInfo("UKR", "Ukraine", 45700395, 339800, "Europe", "Eastern Europe", "Lower", "Developing"));
            Data.Add(new CountryInfo("URY", "Uruguay", 3494382, 43160, "South America", "South America", "Upper", "Emerging", "G20"));
            Data.Add(new CountryInfo("USA", "United States", 318386466, 15169683, "North America", "Northern America", "High", "Developed", "G7"));
            Data.Add(new CountryInfo("UZB", "Uzbekistan", 27606007, 71670, "Asia", "Central Asia", "Lower", "Developing"));
            Data.Add(new CountryInfo("VEN", "Venezuela", 26814843, 357400, "South America", "South America", "Upper", "Emerging", "G20"));
            Data.Add(new CountryInfo("VNM", "Vietnam", 86967524, 241700, "Asia", "South-Eastern Asia", "Lower", "Emerging", "G20"));
            Data.Add(new CountryInfo("VUT", "Vanuatu", 218519, 989, "Oceania", "Melanesia", "Lower", "Small"));
            Data.Add(new CountryInfo("YEM", "Yemen", 23822783, 55280, "Asia", "Western Asia", "Lower", "Small"));
            Data.Add(new CountryInfo("ZAF", "South Africa", 49052489, 491000, "Africa", "Southern Africa", "Upper", "Emerging", "G20"));
            Data.Add(new CountryInfo("ZMB", "Zambia", 11862740, 17500, "Africa", "Eastern Africa", "Lower", "Small"));
            Data.Add(new CountryInfo("ZWE", "Zimbabwe", 12619600, 9323, "Africa", "Eastern Africa", "Low", "Emerging", "G20"));

            #endregion
            Data = Data.OrderByDescending(i => i.GdpTotal).ToList();
        }

        public List<CountryInfo> Filter(string economy)
        {
            return Data.Where(i => i.Economy == economy).ToList();
        }
        public List<CountryInfo> Subset(int count)
        {
            var subset = new List<CountryInfo>();
            for (int i = 0; i < Data.Count; i++)
            {
                if (i <= count) subset.Add(Data[i]);
                else break;
            }
            subset = subset.OrderBy(i => i.Name).ToList();

            return subset;
        }
    }

    public class CountryInfo
    {
        public CountryInfo(string symbol, string name,
            double population, double gdp,
            string continent, string region,
            string income, string economy, string org = null)
        {
            this.Symbol = symbol;
            this.Name = name;
            this.Continent = continent;
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

        public new string ToString()
        {
            var ret = Name + " ";
            ret += string.Format("{0:#,##0,.#} B, ", GdpTotal);
            ret += string.Format("{0:#,##0,,.#} M, ", Population);
            ret += string.Format("{0:#,##0,.#} K", GdpPerCapita);
            return ret;
        }
    }


}
