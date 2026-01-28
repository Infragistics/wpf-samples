using System;
using System.Collections;
using System.Collections.Generic; 
using System.Linq; 

namespace Infragistics.Framework
{
    public class DataFilter
    {
        public DataFilter()
        {
            Condition = FilterCondition.Equals;
            IsCaseSensitive = false;
        }
        /// <summary> Gets or sets ContiondValue </summary>
        public object Value { get; set; }
        public string Property { get; set; }
        public FilterCondition Condition { get; set; }

        public bool IsCaseSensitive { get; set; }
        
        public bool IsValid()
        {
            if (string.IsNullOrEmpty(Property)) return false;
            return Value != null;
        }
        public bool IsMatching(object item)
        {
            if (!IsValid()) return true;

            //if (Value == null) throw new ArgumentException("MemberValue");
            //if (Property == null) throw new ArgumentException("MemberPath");

            if (!item.ContainsProperty(this.Property))
                throw new Exception(item.GetType() + " does not contain " + Property + " property");

            var itemValue = item.GetPropertyValue<object>(Property);
            var itemValueStr = itemValue.ToString();

            var memberValueStr = Value.ToString();
           
            if (!IsCaseSensitive)
            {
                itemValueStr = itemValueStr.ToLower();
                memberValueStr = memberValueStr.ToLower();
            }

            if (Condition == FilterCondition.Equals) return itemValueStr.Equals(memberValueStr);
            if (Condition == FilterCondition.Contains) return itemValueStr.Contains(memberValueStr);
            if (Condition == FilterCondition.NotContains) return !itemValueStr.Contains(memberValueStr);
            if (Condition == FilterCondition.StartsWith) return itemValueStr.StartsWith(memberValueStr);
            if (Condition == FilterCondition.EndsWith) return itemValueStr.EndsWith(memberValueStr);
          
            return true;
        }
    }
    public class DataFilters : List<DataFilter>
    {
        
    }
    
    public enum FilterCondition
    {
        // on numeric
        Equals,
        NotEquals,
        LessThan,
        LessOrEquals,
        GreaterThan,
        GreaterOrEquals,
        // on string
        Contains,
        NotContains,
        StartsWith,
        EndsWith,
    }

    //DrillDownViewModel
    public class GroupDataViewModel : DataViewModel
    {
        public GroupDataViewModel()
        {
            GroupsByName = new Dictionary<string, List<object>>();
        }
        #region Properties
        /// <summary> Gets or sets Level </summary>
        public int DrillDownLevel { get; set; }

        public string DrillDownMember { get; set; }

        private string _groupMembers;
        /// <summary> Gets or sets GroupMember </summary>
        public string GroupMembers
        {
            get { return _groupMembers; }
            set { if (_groupMembers == value) return; _groupMembers = value; OnPropertyChanged("GroupMembers"); }
        }

        /// <summary> Gets or sets GroupDictionary </summary>
        public Dictionary<string, List<object>> GroupsByName { get; set; }

        /// <summary> Gets list of group names   </summary>
        public List<string> GroupsNames { get { return GroupsByName.Keys.ToList(); } }

        /// <summary> Gets or sets Group Aggregate </summary>
        public Func<string, IEnumerable<object>, object> GroupAggregate { get; set; }

        #endregion

        ///// <summary> Gets list of group names   </summary>
        //public List<List<object>> GroupData { get { return GroupDictionary.Values.ToList(); } }

        protected override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == "DataSource")
            {

            }
            else if (propertyName == "DataSorted" ||
                     propertyName == "GroupMembers")
            {
                UpdateGroups();
            }
        }

        protected void UpdateGroups()
        {
            GroupsByName.Clear();

            if (IsEmpty(DataSorted)) return;
             
            var members = string.IsNullOrEmpty(GroupMembers) ? "*" : GroupMembers;
            var groups = members.Split(',').ToList();
            if (groups.Count == 0) groups.Add("*");

            foreach (var group in groups)
            {
                if (!group.Equals("*") && !IsValidProperty(group)) continue;

                foreach (var item in DataSorted)
                {
                    var key = "*";
                    if (!group.Equals("*")) 
                        key = item.GetPropertyValue(group).ToString();

                    if (GroupsByName.ContainsKey(key))
                    {
                        GroupsByName[key].Add(item);
                    }
                    else
                    {
                        GroupsByName.Add(key, new List<object> { item });
                    }
                }
            }


        }
    }

    public class DataViewModel : ObservableModel
    {
        public DataViewModel()
        {
            _filters = new DataFilters();
            Categories = new Dictionary<string, DataColumn>();
            Indicators = new Dictionary<string, DataNumericColumn>();
             

            _dataSource = new List<object>(); // null; //
            //DataSource = new ObservableCollection<object>();
            //DataSource.CollectionChanged += DataSource_CollectionChanged;

            //this.PropertyChanged += OnModelPropertyChanged;
        }
        #region Properties
        /// <summary>
        /// Gets dictionary of numeric data columns of an item in data source
        /// </summary>
        public Dictionary<string, DataNumericColumn> Indicators { get; private set; }
        /// <summary>
        /// Gets list of names for all numeric data columns of an item in data source
        /// </summary>
        public List<string> IndicatorsNames { get { return Indicators.Keys.ToList(); } }
        /// <summary>
        /// Gets list of all numeric data columns of an item in data source
        /// </summary>
        public List<DataNumericColumn> IndicatorsList
        {
            //get { return Indicators.SortByProperty("Key", SortDirection.Ascending).Select(i => i.Value).ToList(); }
            //get { return Indicators.Values.SortByProperty("Label", SortDirection.Ascending).ToList(); }
            get { return Indicators.Values.SortByProperty("IsLogarithmic", "Label", SortDirection.Ascending).ToList(); }
        }

        /// <summary>
        /// Gets dictionary of non-numeric data columns of an item in data source
        /// </summary>
        public Dictionary<string, DataColumn> Categories { get; private set; }
        /// <summary>
        /// Gets list of names for all non-numeric data columns of an item in data source
        /// </summary>
        public List<string> CategoriesNames { get { return Categories.Keys.ToList(); } }
        /// <summary>
        /// Gets list of all non-numeric data columns of an item in data source
        /// </summary>
        public List<DataColumn> CategoriesList
        {
            get { return Categories.SortByProperty("Key", SortDirection.Ascending).Select(i => i.Value).ToList(); }
        }

        private DataFilters _filters;
        /// <summary> Gets or sets Filters </summary>
        public DataFilters Filters
        {
            get { return _filters;}
            set { if (_filters == value) return; _filters = value; OnPropertyChanged("Filters"); }
        }


        private string _sortMember;
        /// <summary>  Gets or sets SortMember </summary>
        public string SortMember
        {
            get { return _sortMember; }
            set { if (_sortMember == value) return; _sortMember = value; OnPropertyChanged("SortMember"); }
        }
        private SortDirection _sortDirection;
        /// <summary> Gets or sets SortDirection </summary>
        public SortDirection SortDirection
        {
            get { return _sortDirection; }
            set { if (_sortDirection == value) return; _sortDirection = value; OnPropertyChanged("SortDirection"); }
        }

        //IEnumerable IList IEnumerable<T> List<object> IEnumerable<object>
        private IEnumerable _dataSource;
        /// <summary> Gets or sets DataSource </summary>
        public IEnumerable DataSource
        {
            get { return _dataSource; }
            set { if (Equals(_dataSource, value)) return; _dataSource = value; OnPropertyChanged("DataSource"); }
        }

        private List<object> _dataView = new List<object>();
        public List<object> DataView
        {
            get
            {
                //if (_dataSource == null || _dataSource.Count() != _DataView.Count())
                //    _DataView = UpdateDataView();

                //else if (_DataView == null)
                //{
                //    _DataView = _dataSource as IEnumerable<object>;
                //}
                return _dataView;
            }
        }

        private List<object> _dataFiltered;
        /// <summary> Gets or sets DataFiltered </summary>
        public List<object> DataFiltered
        {
            get { return _dataFiltered;}
            //set { if (_dataFiltered == value) return; _dataFiltered = value; OnPropertyChanged("DataFiltered"); }
        }

        private List<object> _dataSorted;
        /// <summary> Gets or sets DataSorted </summary>
        public List<object> DataSorted
        {
            get { return _dataSorted;}
            //set { if (_dataSorted == value) return; _dataSorted = value; OnPropertyChanged("DataSorted"); }
        }


        #endregion

        protected override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == "DataSource")
            {
                Categories.Clear();
                Indicators.Clear();

                FilterData();

                if (DataSource != null)
                {
                    foreach (var item in DataSource)
                    {
                        UpdateIndicators(item);
                    }
                }

                OnPropertyChanged("DataFiltered");
                OnPropertyChanged("DataSorted");
                OnPropertyChanged("DataView");

                OnPropertyChanged("Indicators");
                OnPropertyChanged("IndicatorsList");
                OnPropertyChanged("IndicatorsNames");
            }
            else if (propertyName == "Filters")
            {
                FilterData();

                OnPropertyChanged("DataView");
            }
            else if (propertyName == "SortMember" ||
                     propertyName == "SortDirection")
            {
                SortData();

                OnPropertyChanged("DataSorted");
                OnPropertyChanged("DataView");
            }
        }
       
        #region Methods

        private void UpdateIndicators(object item)
        {
            var properties = item.GetPropertiesValues<object>();
            foreach (var property in properties)
            {
                var name = property.Key;
                var value = double.NaN;
                if (property.Value is double) value = (double)property.Value;
                if (property.Value is int) value = (int)property.Value;

                if (double.IsNaN(value))
                {
                    if (!Categories.ContainsKey(name))
                    {
                        var column = new DataColumn { Name = name, Key = name };
                        Categories.Add(name, column);
                    }
                }
                else
                {
                    if (Indicators.ContainsKey(name))
                    {
                        var indicator = Indicators[name];
                        if (!double.IsNaN(value))
                        {
                            indicator.Min = Math.Min(indicator.Min, value);
                            indicator.Max = Math.Max(indicator.Max, value);
                            indicator.Sum += (long)value;
                        }
                        indicator.Average = indicator.Sum / (this.DataView.Count() * 1.0);

                        var logMin = Math.Log10(indicator.Min);
                        var logMax = Math.Log10(indicator.Max);
                        indicator.IsLogarithmic = (logMax - logMin) > 2;
                    }
                    else
                    {
                        var indicator = new DataNumericColumn { Name = name, Key = name };
                        if (!double.IsNaN(value))
                        {
                            indicator.Min = value;
                            indicator.Max = value;
                            indicator.Sum = (long)value;
                        }
                        indicator.Average = indicator.Sum / 1.0;
                        indicator.IsLogarithmic = false;

                        Indicators.Add(name, indicator);
                    }
                }
            }
        }
         
        protected void FilterData()
        {
            _dataFiltered = new List<object>();

            if (IsEmptyDataSource()) return;
       
            if (Filters == null || Filters.Count == 0)
            {
                foreach (var item in DataSource)
                {
                    _dataFiltered.Add(item);
                }
            }
            else
            {
                foreach (var item in DataSource)
                {
                    var match = true;
                    foreach (var filter in Filters)
                    {
                        if (!filter.IsValid()) continue;
                        if (!filter.IsMatching(item))
                        {
                            match = false; break;
                        }
                    }
                    if (match) _dataFiltered.Add(item);
                }
            }

            SortData();

        }

        protected void SortData()
        {
            _dataSorted = _dataFiltered;
            _dataView = _dataSorted;

            if (IsEmpty(_dataFiltered)) return;
            
            var isValid = IsValidProperty(this.SortMember);
            if (!isValid) return;

            _dataSorted = _dataFiltered.SortByProperty(this.SortMember, this.SortDirection).ToList();
            _dataView = _dataSorted;

        }

        public bool IsEmptyDataView()
        {
            return IsEmpty(DataView);
        }
        public bool IsEmptyDataSource()
        {
            return IsEmpty(DataSource);
        }
        public bool IsEmpty(IEnumerable data)
        {
            return data == null || data.Count() == 0;
        }
        /// <summary> Checks if member is a valid property   </summary>
        protected bool IsValidProperty(string dataMember)
        {
            if (string.IsNullOrEmpty(dataMember)) return false;

            if (IsEmptyDataSource()) return false;

            var item = _dataSource.First<object>();
            return item.ContainsProperty(dataMember);
        }
        /// <summary> Checks if member is a valid indicator  </summary>
        protected bool IsValidIndicator(string dataMember)
        {
            if (string.IsNullOrEmpty(dataMember)) return false;
            //var isValid = IsValidProperty(dataMember);
            //if (!isValid) return false;
            
            var isIndicator = this.Indicators.ContainsKey(dataMember);
            return isIndicator;
        }
        #endregion

    }

}