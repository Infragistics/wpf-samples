using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using IGExtensions.Common.Models;
using IGExtensions.Common.Services;
using IGExtensions.Framework;
using Infragistics.Controls.Charts;
using System.Xml.Linq;

namespace IGShowcase.FinanceDashboard.ViewModels
{
    public class HeatmapViewModel : ObservableModel 
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="HeatmapViewModel"/> class.
        /// </summary>
        public HeatmapViewModel()
        {
            _drilledDataLoading = false;

            _stockService = StockYahooDataService.Instance;

            _treemapFilter = new TreemapFilter();
            _treemapFilters = new List<TreemapFilter>();

            this.IsInitialDataLoading = true;

            SynchronizationContext.Current.Post(x => InitnializeSectorSummary(), null);

        }
        #endregion

        #region Private Members
        private StockYahooDataService _stockService;

        private IDictionary<string, SectorViewModel> _sectors;

        private IEnumerable<StockDataViewModel> _companies;

        private string _selectedIndustry;

        private XamTreemap _treemap;

        private bool _drilledDataLoading;

        private IList<TreemapFilter> _treemapFilters;

        private TreemapFilter _treemapFilter;
        #endregion

        #region Public Properties
        /// <summary>
        /// Indicates if the initial data is loaded.
        /// </summary>
        public bool IsInitialDataLoading { get; set; }

        /// <summary>
        /// Gets the selected industry.
        /// </summary>
        public string SelectedIndustry
        {
            get { return _selectedIndustry; }
            set
            {
                _selectedIndustry = value;
                OnPropertyChanged("SelectedIndustry");
            }
        }

        /// <summary>
        /// Gets or sets the description of the HeatmapViewModel instance.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets the market sectors
        /// </summary>
        public IEnumerable<SectorViewModel> Sectors
        {
            get { return _sectors.Values; }
        }

        /// <summary>
        /// Gets the drilled company summary
        /// </summary>
        public IEnumerable<StockDataViewModel> Companies
        {
            get { return _companies; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is retrieving data.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is retrieving data; otherwise, <c>false</c>.
        /// </value>
        public bool IsDrilledDataLoad { get { return _drilledDataLoading; } }

        /// <summary>
        /// Gets or sets the tree map filters.
        /// </summary>
        public IList<TreemapFilter> TreemapFilters
        {
            get { return _treemapFilters; }
        }

        /// <summary>
        /// Gets the current tree map filter
        /// </summary>
        public TreemapFilter TreemapFilter
        {
            get { return _treemapFilter; }
            set
            {
                _treemapFilter = value;

                foreach (ColorMapper colorMapper in _treemap.ValueMappers)
                {
                    colorMapper.ValuePath = _treemapFilter.ValuePath;
                    colorMapper.DataMinimum = _treemapFilter.Minimum;
                    colorMapper.DataMaximum = _treemapFilter.Maximum;
                }

                OnPropertyChanged("TreemapFilter");
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Initializes the tree map control.
        /// </summary>
        /// <param name="treemap">The XamTreemap.</param>
        public void InitializeTreemap(XamTreemap treemap)
        {
            if (_treemap == null)
            {
                _treemap = treemap;
                _treemap.ItemsSource = new List<HeatmapViewModel>() { this };
                _treemap.ItemsSourceRoot = this;
            }
            else
            {
                treemap.ItemsSource = _treemap.ItemsSource;
                treemap.ItemsSourceRoot = _treemap.ItemsSourceRoot;
                _treemap = treemap;
            }

            //Trigger a refresh on the color mappers.
            TreemapFilter = _treemapFilter;

            // Initialize Grid with Business Equipment node's information
            //TODO-MT find replacement
            //_stockService.RefreshCompanySummary(313, OnRefreshCompaniesSummary);

            _selectedIndustry = "Business Equipment";
            OnPropertyChanged("SelectedIndustry");

           
        }

        /// <summary>
        /// Refreshes the sector summary
        /// </summary>
        public void RefreshSectorSummary()
        {
            //TODO-MT find replacement
            //_stockService.RefreshSectorSummary(OnRefreshSectorSummary);
        }

        /// <summary>
        /// Refreshes the industry summary per sector
        /// </summary>
        public void RefreshIndustriesSummary()
        {
            //TODO-MT find replacement
            //foreach (var sector in _sectors)
            //{
            //    _stockService.RefreshIndustrySummary(sector.Value.Index, sector.Value.OnRefreshIndustrySummary);
            //}
        }

        /// <summary>
        /// Drills down the selected node.
        /// </summary>
        /// <param name="drilledNode">The drilled node.</param>
        public void DrillDownData(TreemapNode drilledNode)
        {
            IsInitialDataLoading = true;
            OnPropertyChanged("IsInitialDataLoading");

            if (drilledNode.DataContext is SectorViewModel)
            {
                this.IsInitialDataLoading = false;
                OnPropertyChanged("IsInitialDataLoading");
                //_treemap.ItemsSourceRoot = drilledNode.DataContext;
               
            }
            else if (drilledNode.DataContext is IndustryViewModel)
            {
                _companies = null;
                OnPropertyChanged("Companies");

                _selectedIndustry = ((IndustryViewModel)drilledNode.DataContext).Description;
                OnPropertyChanged("SelectedIndustry");

                _drilledDataLoading = true;
                OnPropertyChanged("IsDrilledDataLoad");

                //TODO-MT find replacement
                //_stockService.RefreshCompanySummary(((IndustryViewModel)drilledNode.DataContext).Index,
                //                                    OnRefreshCompaniesSummary);
            }
        }

        /// <summary>
        /// Returns to the previous drilled node.
        /// </summary>
        public void DrillBack()
        {
            if (_treemap.ItemsSourceRoot is SectorViewModel)
            {
                _treemap.ItemsSourceRoot = this;
            }
        }

        /// <summary>
        /// Adds a new TreemapFilter.
        /// </summary>
        /// <param name="description"></param>
        /// /// <param name="valuePath"></param>
        /// <param name="minimum"></param>
        /// <param name="maximum"></param>
        public void AddTreemapFilter
            (string description, string valuePath, double minimum, double maximum)
        {
            _treemapFilters.Add(new TreemapFilter()
                                         {
                                             Description = description,
                                             ValuePath = valuePath,
                                             Minimum = minimum,
                                             Maximum = maximum,
                                         });

            if (_treemapFilter.Description == null)
            {
                _treemapFilter = _treemapFilters[0];
                OnPropertyChanged("TreemapFilter");
            }

            OnPropertyChanged("TreemapFilters");
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Initializes the sectors.
        /// </summary>
        private void InitnializeSectorSummary()
        {
            try
            {
                var sriSectors = Application.GetResourceStream(new Uri("Assets/Data/Sectors.xml", UriKind.Relative));
                if (sriSectors == null || sriSectors.Stream == null)
                {
                    DebugManager.LogWarning("HeatmapViewModel: Missing Stock Sectors file: Assets/Data/Sectors.xml");
                    return;
                }

                XDocument document = XDocument.Load(sriSectors.Stream);

                var sectorSummary = (from sector in document.Element("Sectors").Elements("Sector")
                                     select new SectorViewModel(sector.Elements("Industry").ToList())
                                                {
                                                    Description = sector.Attribute("Description").Value,
                                                    Index = int.Parse(sector.Attribute("Index").Value)
                                                }
                                    ).ToList();

                _sectors = new Dictionary<string, SectorViewModel>(sectorSummary.Count);

                foreach (var sector in sectorSummary)
                {
                    _sectors.Add(sector.Description, sector);
                }

                OnPropertyChanged("Sectors");

                RefreshSectorSummary();

               
               
            }
            catch (Exception ex)
            {
                DebugManager.LogError("HeatmapViewModel: " + ex);
            }
        }

        /// <summary>
        /// Called when [refresh sector summary].
        /// </summary>
        /// <param name="data">The data.</param>
        private void OnRefreshSectorSummary(IDictionary<string, StockData> data)
        {
            if (data.Count > 0)
            {
                foreach (var sector in data)
                {
                    _sectors[sector.Key].SetFinancialData(sector.Value);
                }

                OnPropertyChanged("Sectors");
            }

            RefreshIndustriesSummary();
            
        }

        /// <summary>
        /// Called when [refresh companies summary].
        /// </summary>
        /// <param name="data">The data.</param>
        private void OnRefreshCompaniesSummary(IDictionary<string, StockData> data)
        {
            
            if (data.Count > 0)
            {
                _companies = data.Select(x => new StockDataViewModel(x.Value));
                OnPropertyChanged("Companies");
            }

            this.IsInitialDataLoading = false;
            OnPropertyChanged("IsInitialDataLoading");

            _drilledDataLoading = false;
            OnPropertyChanged("IsDrilledDataLoad");
        }
        #endregion

        #region INotifyPropertyChanged Members
        //public event PropertyChangedEventHandler PropertyChanged;
        ///// <summary>
        ///// Raises the property changed.
        ///// </summary>
        ///// <param name="propertyName">Name of the property.</param>
        //public void OnPropertyChanged(string propertyName)
        //{
        //    if (PropertyChanged == null) return;

        //    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        //}
        #endregion
    }

    public class TreemapFilter
    {
        public string Description { get; set; }

        public string ValuePath { get; set; }

        public double Minimum { get; set; }
        public double Maximum { get; set; }
    }
}
