using System.Xml.Linq;

namespace IGTrading.ViewModels
{
    public class IndustryViewModel : FinancialDataBaseViewModel
    { 
        #region Constructor
        /// <summary>
        /// Default constructor.
        /// </summary>
        public IndustryViewModel()
        { }

        /// <summary>
        /// Constructor.
        /// </summary>
        public IndustryViewModel(XElement element) : base(element)
        { }
        #endregion
    }
}
