using System;
using System.Collections.Generic;
using System.Linq;

namespace Infragistics.SamplesBrowser.ViewModel
{
    public class ControlViewModel : TocItemViewModel
    {
        //MT: added new constructor with assemblyName
        public ControlViewModel(string icon, string name, string xamlFilePath, string assemblyName,
            double releaseVersion, IList<SubCategoryViewModel> subCategories)
            : base(name, xamlFilePath, assemblyName, releaseVersion, string.Empty, subCategories.Cast<TocItemViewModel>().ToList())
        {
            this.Icon = new Uri(icon, UriKind.Relative);
        }
        public Uri Icon { get; private set; }
    }
}
