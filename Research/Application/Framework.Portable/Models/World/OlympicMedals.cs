
using System.Diagnostics;

namespace Infragistics.Framework
{
    /// <summary>
    /// Represents medals for a country at a given Olympic
    /// </summary>
#if DEBUG
    [DebuggerDisplay("{ToString()}")] 
#endif
    public class OlympicMedals  : ObservableModel
    { 
        public override string ToString()
        {
            return Year + ", " + 
                Ranking.ToString("0000") + ", " + 
                Total.ToString("000") + " (" + 
                Gold.ToString("000") + ", " + 
                Silver.ToString("000") + ", " + 
                Bronze.ToString("000") + ") " + Country;
        }

        /// <summary> Gets or sets Country </summary>
        public string Country { get; set; }

        private int _year;
        /// <summary> Gets or sets Year </summary>
        public int Year
        {
            get { return _year;}
            set { if (_year == value) return; _year = value; OnPropertyChanged("Year"); }
        }

        private int _gold;
        /// <summary> Gets or sets Gold </summary>
        public int Gold
        {
            get { return _gold;}
            set { if (_gold == value) return; _gold = value; OnPropertyChanged("Gold"); }
        }

        private int _silver;
        /// <summary> Gets or sets Silver </summary>
        public int Silver
        {
            get { return _silver; }
            set { if (_silver == value) return; _silver = value; OnPropertyChanged("Silver"); }
        }
        private int _bronze;
        /// <summary> Gets or sets Bronze </summary>
        public int Bronze
        {
            get { return _bronze;}
            set { if (_bronze == value) return; _bronze = value; OnPropertyChanged("Bronze"); }
        }
         
        /// <summary> Gets or sets GoldDelta </summary>
        public int GoldDelta { get; set; }
        /// <summary> Gets or sets SilverDelta </summary>
        public int SilverDelta { get; set; }
        /// <summary> Gets or sets BronzeDelta </summary>
        public int BronzeDelta { get; set; }
        /// <summary> Gets or sets TotalDelta </summary>
        public int TotalDelta { get; set; }

        //public double GoldRatio { get { return Gold / Total * 100; } }
        //public double SilverRatio { get { return Silver / Total * 100; } }
        //public double BronzeRatio { get { return Bronze / Total * 100; } }

        /// <summary> Gets or sets PropertyName </summary>
        public double Ranking { get { return (Gold * 3) + (Silver * 2) + Bronze; } }
  
        /// <summary> Gets or sets PropertyName </summary>
        public int Total { get { return Gold + Silver + Bronze; } }

         
    }
}