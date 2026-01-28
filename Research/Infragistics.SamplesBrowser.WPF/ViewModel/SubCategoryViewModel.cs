using System.Collections.Generic;
using System.Linq;

namespace Infragistics.SamplesBrowser.ViewModel
{
    public class SubCategoryViewModel : TocItemViewModel
    {
        //MT: added new constructor with assemblyName
        public SubCategoryViewModel(string name, string xamlFilePath, string assemblyName,
            double releaseVersion, IList<SampleViewModel> samples)
            : base(name, xamlFilePath, assemblyName, releaseVersion, string.Empty, samples.Cast<TocItemViewModel>().ToList())
        {

        }
    }
}
