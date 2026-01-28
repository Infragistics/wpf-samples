using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Models;

namespace IGTimeline.Samples
{
    public partial class BindingDataViewModel : Infragistics.Samples.Framework.SampleContainer
    {
        private DataBindingViewModel _viewModel = new DataBindingViewModel();

        public BindingDataViewModel()
        {
            InitializeComponent();

            this.DataContext = this._viewModel;
            this._viewModel.LoadData();
        }
    }
    #region ViewModel
    public class DataBindingViewModel : ObservableModel
    {
        private List<TimelineData> _entries;
        public DataBindingViewModel()
        {
        }

        #region Events
        public event EventHandler DataLoaded;
        #endregion Events

        #region Properties
        public List<TimelineData> Entries
        {
            get
            {
                return this._entries;
            }
            set
            {
                if (this._entries == value)
                    return;
                this._entries = (List<TimelineData>)value;
                this.OnPropertyChanged("Entries");
            }

        }
        #endregion Properties

        #region Public Methods

        private XmlDataProvider _dataSource = null;
        public void LoadData()
        {
            _dataSource = new XmlDataProvider();
            _dataSource.GetXmlDataCompleted += OnGetXmlDataCompleted;
            _dataSource.GetXmlDataAsync("Timeline.xml");
        }

        #endregion Public Methods

        #region Event Handlers

        void OnGetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
            if (e.HasError) return;

            XDocument doc = e.Result;
            var entries = from entry in doc.Descendants("DateTimeEntry")
                          where entry.Attribute("Type").Value == "Video"
                          select new TimelineData((DateTime)entry.Attribute("Time"),
                              entry.Attribute("Title").Value.ToString(),
                              entry.Attribute("Details").Value.ToString());

            this.Entries = (List<TimelineData>)entries.ToList();

            if (this.DataLoaded != null)
            {
                this.DataLoaded(this, EventArgs.Empty);
            }

        }

        #endregion Event Handlers
    }
    #endregion ViewModel
}
