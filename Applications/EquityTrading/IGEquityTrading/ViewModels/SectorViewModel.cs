using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace IGTrading.ViewModels
{
    public class SectorViewModel : FinancialDataBaseViewModel
    {
        #region Constructor
        /// <summary>
        /// Default constructor.
        /// </summary>
        public SectorViewModel()
        { }

        /// <summary>
        /// Constructor.
        /// </summary>
        public SectorViewModel(XElement element) : base(element)
        {
            Industries = element
                .Element("Industries")
                .Elements("Industry")
                .Select(x => new IndustryViewModel(x))
				.ToArray();
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the market sectors
        /// </summary>
        public IEnumerable<IndustryViewModel> Industries { get; set; }
        #endregion
    }
}
