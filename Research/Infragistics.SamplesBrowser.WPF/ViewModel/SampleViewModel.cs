using System;
using System.Collections.Generic;

namespace Infragistics.SamplesBrowser.ViewModel
{
    public class SampleViewModel : TocItemViewModel
    {
        //MT: added new constructor with assemblyName
        public SampleViewModel(string description, string name, string xamlFilePath, string assemblyName,
            double releaseVersion, string status, List<string> codeFiles)
            : base(name, xamlFilePath, assemblyName, releaseVersion, status, null)
        {
            this.Description = description.Trim() ?? String.Empty;
            this.CodeFiles = codeFiles;
            this.HasCodeFiles = codeFiles != null && codeFiles.Count > 0;
        } 

        public override bool MatchesSearchText(string searchText)
        {
            if (string.IsNullOrEmpty(searchText))
                return false;

            if (-1 < base.Name.IndexOf(searchText, StringComparison.CurrentCultureIgnoreCase))
                return true;

            if (-1 < this.Description.IndexOf(searchText, StringComparison.CurrentCultureIgnoreCase))
                return true;

            string controlName = base.Parent.Parent.Name;
            if (-1 < controlName.IndexOf(searchText, StringComparison.CurrentCultureIgnoreCase))
                return true;

            return false;
        }
    }
}
