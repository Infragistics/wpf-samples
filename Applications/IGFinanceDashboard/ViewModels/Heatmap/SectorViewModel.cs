using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using IGExtensions.Common.Models;

namespace IGShowcase.FinanceDashboard.ViewModels
{
    public class SectorViewModel : StockDataViewModel
    {
        #region Private Members
        private readonly IDictionary<string, IndustryViewModel> _industries;
        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor.
        /// </summary>
        public SectorViewModel()
        { }

        /// <summary>
        /// Constructs a SectorViewModel from XElements.
        /// </summary>
        /// <param name="industries">A list of type XElement</param>
        public SectorViewModel(List<XElement> industries)
        {
            try
            {
                var industrySummary = (from industry in industries
                                       select new IndustryViewModel()
                                                  {
                                                      Description = industry.Attribute("Description").Value,
                                                      Index = int.Parse(industry.Attribute("Index").Value)
                                                  }
                                      ).ToList();

                _industries = new Dictionary<string, IndustryViewModel>(industrySummary.Count);

                foreach (var industry in industrySummary)
                {
                    _industries.Add(industry.Description, industry);
                }

                OnPropertyChanged("Industries");
            }
            catch { }
        }

        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the market sectors
        /// </summary>
        public IEnumerable<IndustryViewModel> Industries
        {
            get { return _industries.Values; }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Called when [refresh sector summary].
        /// </summary>
        /// <param name="data">The data.</param>
        public void OnRefreshIndustrySummary(IDictionary<string, StockData> data)
        {
            if (data.Count > 0)
            {
                foreach (var industry in data)
                {
                    _industries[industry.Key].SetFinancialData(industry.Value);
                }

                OnPropertyChanged("Industries");
            }
        }
        #endregion
    }
}
