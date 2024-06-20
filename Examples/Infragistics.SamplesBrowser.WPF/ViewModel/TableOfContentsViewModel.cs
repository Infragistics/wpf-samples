using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Input;
using System.Xml;
using System.Xml.Linq;
using Infragistics.Samples.Shared.Extensions; 

namespace Infragistics.SamplesBrowser.ViewModel
{
    public class TableOfContentsViewModel : TocItemViewModel
    {
        static XNamespace xs = XNamespace.None;//"urn:toc-schema";
        #region Creation

        public static TableOfContentsViewModel Create(string xmlResourceFilePath)
        {
            xmlResourceFilePath = "/;component/TableOfContents.xml";

            using (Stream stream = App.GetResourceStream(xmlResourceFilePath))
            { 
                var xdoc = XDocument.Load(stream);
                var xDocString = xdoc.ToString();
                xDocString = xDocString.Replace("xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"", "");
                xDocString = xDocString.Replace("xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", "");
                xDocString = xDocString.Replace("xmlns=\"urn:toc-schema\"", "");
                xdoc = XDocument.Parse(xDocString);

                var fieldDisplayName = "displayName";
                var fieldDescription = "description";

                if (Thread.CurrentThread.CurrentCulture.Name.StartsWith("ja"))
                {
                    fieldDisplayName = "displayNameJA";
                    fieldDescription = "descriptionJA";
                }
                else
                {
                    fieldDisplayName = "displayNameEN";
                    fieldDescription = "descriptionEN";
                }

                List<ControlViewModel> controls =
                (from controlElem in xdoc.Descendants(xs+"Controls").Elements(xs+"Control")
                 select new ControlViewModel(
                     controlElem.Element(xs + "icon").GetString(),
                     controlElem.Element(xs + "displayName").GetString(),
                     controlElem.Element(xs + "welcome").GetString(),
                     "Infragistics.SamplesBrowser", 
                     GetReleaseVersion(controlElem),
                     (from subcategoryElem in controlElem.Element(xs + "Categories").Elements(xs + "Category")
                          .Where(c => c.Descendants(xs + "Sample").Count() > 0)
                      select new SubCategoryViewModel(
                          subcategoryElem.Element(xs + fieldDisplayName).GetString(),
                          subcategoryElem.Element(xs + "welcome").GetString(),
                          subcategoryElem.Element(xs + "assembly").GetString(),
                          GetReleaseVersion(subcategoryElem),
                          (from sampleElem in subcategoryElem.Element(xs + "Samples").Elements(xs + "Sample")
                           select new SampleViewModel(
                               sampleElem.Element(xs + fieldDescription).GetString(),
                               sampleElem.Element(xs + fieldDisplayName).GetString(),
                               GetStartFile(sampleElem),
                               sampleElem.Element(xs + "assembly").GetString(),
                               GetReleaseVersion(sampleElem),
                               GetStatus(sampleElem),
                               GetCodeFiles(sampleElem)
                           )
                           ).ToList()
                      )).ToList()
                 )).ToList();

                //System.Diagnostics.Debug.WriteLine("controls=" + controls.Count);
                //foreach (var c in controls)
                //{
                //    var samples = new List<string>();
                //    // categories
                //    if (c.Children != null && c.Children.Count > 0)
                //    {
                //        foreach (var categoory in c.Children)
                //        {
                //            // samples
                //            if (categoory.Children != null && categoory.Children.Count > 0)
                //            {
                //                for (int i = 0; i < categoory.Children.Count; i++)
                //                {
                //                    var s = categoory.Children[i];
                //                    //System.Diagnostics.Debug.WriteLine(i + " s=" + s.Name);
                //                    samples.Add(s.Name);
                //                    if (s.Children != null && s.Children.Count > 0)
                //                    {
                //                        throw new Exception(c.Name + " - '" + s.Name + "' has children " + s.Children.Count);
                //                    }
                //                }
                //            }
                //        }
                //    }                     
                //    System.Diagnostics.Debug.WriteLine(">> '" + c.Name + "' control has " + samples.Count + " samples");
                //    for (int i = 0; i < samples.Count; i++)
                //    {
                //      System.Diagnostics.Debug.WriteLine((i+1).ToString("00") + " " + samples[i]);
                //    }
                //}
           
                var sb = xdoc.Element(xs+"SamplesBrowser");
                var currentVersion = sb.Attribute(xs+"currentVersion").GetDouble();
               
                return new TableOfContentsViewModel(controls, currentVersion);
            }
        }

        static double GetReleaseVersion(XElement elem)
        {
            var releaseVersion = elem.Attribute("releaseVersion");
            if (releaseVersion == null)            
                return double.NaN;            
            else
                return double.Parse(releaseVersion.Value, CultureInfo.InvariantCulture);
        }
        static string GetStatus(XElement elem)
        {  
            var status = elem.Attribute("status");
            if (status == null)
            {
                return string.Empty;
            }
            else
            {
                return status.Value;
            }
        }
        static string GetStartFile(XElement elem)
        {
            if (elem.Element(xs + "CodeFiles") != null)
            {
                            
                // remove the "Samples\" part from the path of the code file
                var startfileElement = elem.Element(xs + "CodeFiles").Elements(xs + "CodeFile").FirstOrDefault(f => f.Attribute("isStartUp").GetBool());
                if (startfileElement != null)
                {
                    var fullpath = startfileElement.Attribute("path").GetString();
                    return fullpath
                        .Substring(fullpath.IndexOf(@"/Samples/") + 1)
                        .Replace(@"/", @"\");
                }
                else
                    return null;               
            }
            else return null;
        }

        static List<string> GetCodeFiles(XElement elem)
        {
            if (elem.Element(xs + "CodeFiles") != null)
            {
                var startupFilename = System.IO.Path.GetFileName(GetStartFile(elem));

               
  // remove the "Samples\" part from the path of the code file               
                var paths = elem.Element(xs + "CodeFiles").Elements(xs + "CodeFile")
                   
                    .Select(cf => cf.Attribute("path").GetString()).ToList()
                    .Select(fp => fp.Substring(fp.IndexOf(@"/Samples/") + 9).Replace(@"/", @"\"))
                    .Where(p => !p.EndsWith(startupFilename) && !p.EndsWith(startupFilename+".cs"))
                        .ToList();
                if (paths == null || paths.Count == 0)
                    return null;
                else
                    return paths;
            }
            else return null;
        }

        public double CurrentVersion { get; set; }

        TableOfContentsViewModel(List<ControlViewModel> controls, double currentVersion)
            : base(string.Empty, string.Empty, double.NaN, string.Empty, null)
        {
            this.CurrentVersion = currentVersion;
            //CurrentVersion = double.Parse(Properties.Settings.Default.CurrentVersion, CultureInfo.InvariantCulture);

            foreach (ControlViewModel control in controls)
                control.Initialize(this, CurrentVersion);

            // sort controls by new, update, ctp, name
            controls.Sort((toc1, toc2) => SortTocItems(toc1, toc2));
            
            var dict = new Dictionary<string, int>();
            dict.Add("Charts", 0);
            dict.Add("Maps", 0);
            dict.Add("Gauges", 0); 
            dict.Add("Grids", 0);
            dict.Add("Editors", 0);
            dict.Add("Other", 0);
            dict.Add("Framework", 0);
            dict.Add("Total", 0);

            foreach (var control in controls)
            {
                // sort categories by new, update, ctp, name
                var categories = control.Children;
                categories.Sort((toc1, toc2) => SortTocItems(toc1, toc2));
                
                var count = 0;
                foreach (var category in categories)
                {
                    // sort samples by new, update, ctp, name
                    var samples = category.Children;
                    samples.Sort((toc1, toc2) => SortTocItems(toc1, toc2));

                    count += category.Children.Count();
                }
                if (control.Name.Contains("Chart") ||
                    control.Name.Contains("Timeline"))
                    dict["Charts"] += count;
                else if (control.Name.Contains("Map") ||
                         control.Name.Contains("Diagram"))
                    dict["Maps"] += count;
                else if (control.Name.Contains("Gauge") ||
                         control.Name.Contains("Graph") ||
                         control.Name.Contains("Display"))
                    dict["Gauges"] += count;
                else if (control.Name.Contains("Grid"))
                    dict["Grids"] += count;
                else if (control.Name.Contains("Editor"))
                    dict["Editors"] += count;
                else if (control.Name.Contains("Framework") ||
                         control.Name.Contains("Washer")  ||
                         control.Name.Contains("Excel") ||
                         control.Name.Contains("Math") ||
                         control.Name.Contains("Word")  ||
                         control.Name.Contains("Engine") ||
                         control.Name.Contains("Manager") ||
                         control.Name.Contains("Reporting"))
                    dict["Framework"] += count;
                
                else dict["Other"] += count;

                dict["Total"] += count;
            }

            foreach (var pair in dict)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0,5:##0}", pair.Value) + " samples for " + pair.Key);
            }

            var childrenList = controls.Cast<TocItemViewModel>().ToList();
            base.Children = new List<TocItemViewModel>(childrenList);

            _searchCommand = new SearchTableOfContentsCommand(this);
            _clearSearchCommand = new ClearTableOfContentsSearchCommand(this);
        } 
        private int SortTocItems(TocItemViewModel toc1, TocItemViewModel toc2)
        { 
            //if (toc1.ReleaseVersion == CurrentVersion ||
            //    toc2.ReleaseVersion == CurrentVersion)
            //{
                if (toc1.IsNew != toc2.IsNew)
                    return toc1.IsNew ? -1 : 1;
                 
                if (toc1.IsUpdated != toc2.IsUpdated)
                    return toc1.IsUpdated ? -1 : 1;

                if (toc1.IsCtp != toc2.IsCtp)
                    return toc1.IsCtp ? -1 : 1;

                if (toc1.IsBETA != toc2.IsBETA)
                    return toc1.IsBETA ? -1 : 1;
            //}

            string name1 = toc1.Name ?? string.Empty;  
            string name2 = toc2.Name ?? string.Empty; 

            return name1.CompareTo(name2);
        }
        #endregion // Creation

        #region Search Logic

        readonly ICommand _clearSearchCommand;
        readonly ICommand _searchCommand;
        List<TocItemViewModel> _searchResults;
        string _searchText = String.Empty;

        #region ClearSearchCommand

        /// <summary>
        /// Returns the command used to remove the search text and search results.
        /// </summary>
        public ICommand ClearSearchCommand
        {
            get { return _clearSearchCommand; }
        }

        private class ClearTableOfContentsSearchCommand : ICommand
        {
            readonly TableOfContentsViewModel _toc;

            public ClearTableOfContentsSearchCommand(TableOfContentsViewModel toc)
            {
                _toc = toc;
            }

            public bool CanExecute(object parameter)
            {
                return _toc.HasSearchText || _toc.HasSearchResults;
            }

            event EventHandler ICommand.CanExecuteChanged
            {
                add { System.Windows.Input.CommandManager.RequerySuggested += value; }
                remove { System.Windows.Input.CommandManager.RequerySuggested -= value; }
            }

            public void Execute(object parameter)
            {
                _toc.SearchText = String.Empty;
                _toc.SearchResults = null;
            }
        }

        #endregion // ClearSearchCommand

        #region SearchCommand

        /// <summary>
        /// Returns the command used to execute a search in the tree.
        /// </summary>
        public ICommand SearchCommand
        {
            get { return _searchCommand; }
        }

        private class SearchTableOfContentsCommand : ICommand
        {
            readonly TableOfContentsViewModel _toc;

            public SearchTableOfContentsCommand(TableOfContentsViewModel toc)
            {
                _toc = toc;
            }

            public bool CanExecute(object parameter)
            {
                return true;
            }

            event EventHandler ICommand.CanExecuteChanged
            {
                add { }
                remove { }
            }

            public void Execute(object parameter)
            {
                _toc.PerformSearch();
            }
        }

        #endregion // SearchCommand

        #region SearchResults

        public List<TocItemViewModel> SearchResults
        {
            get { return _searchResults; }
            private set
            {
                _searchResults = value;

                if (_searchResults != null)
                    _searchResults.Sort((a, b) => a.Name.CompareTo(b.Name));

                this.OnPropertyChanged("SearchResults");
                this.OnPropertyChanged("HasSearchResults");
            }
        }

        public bool HasSearchResults
        {
            get { return _searchResults != null && _searchResults.Count != 0; }
        }

        #endregion // SearchResults

        #region SearchText

        /// <summary>
        /// Gets/sets a fragment of the name to search for.
        /// </summary>
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                if (value == _searchText)
                    return;

                _searchText = value;

                this.OnPropertyChanged("SearchText");
                this.OnPropertyChanged("HasSearchText");
            }
        }

        public bool HasSearchText
        {
            get
            {
                if (String.IsNullOrEmpty(_searchText))
                    return false;

                if (_searchText.Trim() == String.Empty)
                    return false;

                return true;
            }
        }

        #endregion // SearchText

        #region Helper Methods

        void PerformSearch()
        {
            string searchText = _searchText ?? String.Empty;

            if (searchText == String.Empty)
                this.SearchResults = null;
            else
                this.SearchResults = this.FindMatches(_searchText, this).ToList();
        }

        IEnumerable<TocItemViewModel> FindMatches(string searchText, TocItemViewModel item)
        {
            if (item.MatchesSearchText(searchText))
                yield return item;

            if (item.HasChildren)
            {
                foreach (TocItemViewModel child in item.Children)
                    foreach (TocItemViewModel match in this.FindMatches(searchText, child))
                        yield return match;
            }
        }

        #endregion // Helper Methods

        #endregion // Search Logic
    }
}
