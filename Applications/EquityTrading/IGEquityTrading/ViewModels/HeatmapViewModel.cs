using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using IGTrading.Controls;
using Infragistics.Controls.Charts;

namespace IGTrading.ViewModels
{
    public class HeatmapViewModel : INotifyPropertyChanged
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="HeatmapViewModel"/> class.
        /// </summary>
        public HeatmapViewModel()
        {
            _treemapFilter = new TreemapFilter();
            _treemapFilters = new List<TreemapFilter>();

        	_itemsSource = new[] {this};

			_drilledNodes = new Stack<object>();
        	_drilledNodes.Push(this);

            InitializeData();
        }
        #endregion

        #region Private Members
        private ObservableCollection<FinancialDataBaseViewModel> _companies = new ObservableCollection<FinancialDataBaseViewModel>();

    	private IEnumerable<HeatmapViewModel> _itemsSource;

    	private Stack<object> _drilledNodes;

        private string _selectedIndustry;

        private XamTreemap _treemap;

        private bool _drilledDataLoading = false;

        private IList<TreemapFilter> _treemapFilters;

        private TreemapFilter _treemapFilter;
        #endregion

        #region Public Properties
		/// <summary>
		/// Gets the items source for the Heatmap.
		/// </summary>
    	public IEnumerable<HeatmapViewModel> ItemsSource
    	{
			get { return _itemsSource; }
    	}

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
                RaisePropertyChanged("SelectedIndustry");
            }
        }

        /// <summary>
        /// Gets or sets the description of the HeatmapViewModel instance.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets the market sectors
        /// </summary>
        public IEnumerable<SectorViewModel> Sectors { get; set; }

        /// <summary>
        /// Gets the drilled company summary
        /// </summary>
        public ObservableCollection<FinancialDataBaseViewModel> Companies
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
        /// Gets or sets the treemap filters.
        /// </summary>
        public IList<TreemapFilter> TreemapFilters
        {
            get { return _treemapFilters; }
        }

        /// <summary>
        /// Gets the current treemap filter
        /// </summary>
        public TreemapFilter TreemapFilter
        {
            get { return _treemapFilter; }
            set
            {
                _treemapFilter = value;

                MultiColorMapper mapper = _treemap.ValueMappers[0] as MultiColorMapper;
                mapper.ValuePath = _treemapFilter.ValuePath;
                mapper.DataMinimum = _treemapFilter.Minimum;
                mapper.DataMaximum = _treemapFilter.Maximum;

                RaisePropertyChanged("TreemapFilter");
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Initializes the treemap control.
        /// </summary>
        /// <param name="treemap">The XamTreemap.</param>
        public void InitializeTreemap(XamTreemap treemap)
        {
			if (_treemap != treemap)
			{
				_treemap = treemap;
				SetItemsSourceRoot();
			}

        	//Trigger a refresh on the color mappers.
            TreemapFilter = _treemapFilter;
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
                RaisePropertyChanged("TreemapFilter");
            }

            RaisePropertyChanged("TreemapFilters");
        }

		/// <summary>
		/// Display the selected node as root.
		/// </summary>
		/// <param name="node"></param>
		public void DrillNode(object node)
		{
			//Check if the sender node is actually a different node from the ItemsSourceRoot
			if (_drilledNodes.Count > 0 && !_drilledNodes.Peek().Equals(node))
			{
				//Push the current ItemsSourceRoot in a stack
				_drilledNodes.Push(node);
				SetItemsSourceRoot();
			}
		}

		/// <summary>
		/// Go back from the drilldown.
		/// </summary>
		public void DrillBack()
		{
			//Check if there are nodes in the stack
			if (_drilledNodes.Count > 0)
			{
				//Push the top node
				_drilledNodes.Pop();
				SetItemsSourceRoot();
			}
		}
        #endregion

        #region Private Methods
        /// <summary>
        /// Initializes the sectors.
        /// </summary>
        private void InitializeData()
        {
            try
            {
                var data = Application.GetResourceStream(new Uri("/Assets/Sectors.xml", UriKind.Relative));

                if (data == null || data.Stream == null)
                {
                    System.Diagnostics.Debug.WriteLine("<Warning> Mising resource file: /Assets/Sectors.xml");
                    return;
                }

                XDocument document = XDocument.Load(data.Stream);

                Sectors = document
                    .Element("Market")
                    .Element("Sectors")
                    .Elements("Sector")
                    .Select(x => new SectorViewModel(x))
					.ToArray();

                data.Stream.Dispose();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("<Exception> " + ex.ToString());
            }
        }

		private void SetItemsSourceRoot()
		{
			_treemap.ItemsSourceRoot = _drilledNodes.Count > 0 ? _drilledNodes.Peek() : this;
		}
        #endregion

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Raises the property changed.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged == null) return;

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }

    public class TreemapFilter
    {
        //public TreemapFilter()
        //{
        //    this.Description = "Description";
        //    this.ValuePath = "";
        //    this.Minimum = 0;
        //    this.Maximum = 1;
        //}
        public string Description { get; set; }

        public string ValuePath { get; set; }

        public double Minimum { get; set; }
        public double Maximum { get; set; }
    }
}
