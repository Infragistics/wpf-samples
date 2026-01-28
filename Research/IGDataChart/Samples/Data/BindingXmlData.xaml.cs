using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models;
using IGDataChart.Resources;
using System.Windows.Markup;
using System.Globalization;

namespace IGDataChart.Samples.Data
{
    public partial class BindingXmlData : SampleContainer
    {
        public BindingXmlData()
        {
            InitializeComponent();

            // Initialize once the sample has been loaded.  Loaded event is provided by SampleContainer.
            this.Loaded += OnSampleLoaded;
        }

        private BindingXmlDataViewModel _viewModel;

        private void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            // Create the view model.  This will initiate xml data loading and parsing.
            _viewModel = new BindingXmlDataViewModel();
            
            // Bind the xml data to the charts once it has been loaded and parsed by the view model.
            _viewModel.DataParsed += OnViewModelDataLoaded;
            
            // Display loading message to user.  LoadingControl is defined in xaml.
            this.LoadingControl.Visibility = Visibility.Visible;
        }

        private void OnViewModelDataLoaded(object sender, EventArgs e)
        {
            // Stop displaying loading message.
            this.LoadingControl.Visibility = Visibility.Collapsed;
            
            // Bind the xml data to the charts.
            this.PriceChart.DataContext = this.VolumeChart.DataContext = _viewModel;
        }
    }

    #region View Model
    public class BindingXmlDataViewModel
    {
        public string ChartTitle { get; private set; }
        public string ChartSubtitle { get; private set; }

        public List<StockMarketDataPoint> XmlDataSource { get; private set; }

        public event EventHandler DataParsed;
 
        private readonly XmlDataProvider _xmlDataProvider;

        public BindingXmlDataViewModel()
        {
            // Create xml data provider that will load data from xml file.
            _xmlDataProvider = new XmlDataProvider();
            
            // Get the xml data from file storage.
            _xmlDataProvider.GetXmlDataAsync("MicrosoftStockData.xml");
            
            // Parse the data when the xml file has finished loading.
            _xmlDataProvider.GetXmlDataCompleted += OnDataProviderGetXmlDataCompleted;
        }

        void OnDataProviderGetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
            if (e.Error != null) return;

            // Store the xml doc in memory. 
            XDocument doc = e.Result;

            // Sample from MicrosoftStockData.xml
            //
            // <?xml version="1.0" encoding="UTF-8"?>
            // <Document>
            //   <StockInformation>
            //     <Company>Microsoft</Company>
            //     <Ticker>MSFT</Ticker>
            //     <Exchange>NASDAQ</Exchange>
            //     <StartDate>1/3/2012</StartDate>
            //     <EndDate>1/2/2013</EndDate>
            //   </StockInformation>
            //   <StockQuotes>
            //     <StockQuote>
            //       <Date>1/3/2012</Date>
            //       <Open>26.55</Open>
            //       <High>26.96</High>
            //       <Low>26.39</Low>
            //       <Close>26.76</Close>
            //       <Volume>64735391</Volume>
            //     </StockQuote>
            //     <StockQuote>
            //       <Date>1/4/2012</Date>
            //       <Open>26.82</Open>
            //       <High>27.47</High>
            //       <Low>26.78</Low>
            //       <Close>27.4</Close>
            //       <Volume>80519402</Volume>
            //     </StockQuote>
            //   </StockQuotes>
            // </Document>

            // Parse xml data for information about the stock.
            var metaData = (from value in doc.Descendants("StockInformation")
                            select new StockInformation
                            {
                                CompanyName = value.Element("Company").GetString(),
                                StockTicker = value.Element("Ticker").GetString(),
                                StartDate = value.Element("StartDate").GetDateTime(),
                                EndDate = value.Element("EndDate").GetDateTime()
                            }).ToList<StockInformation>();

            // Parse xml data for stock quotes.
            XmlDataSource = (from value in doc.Descendants("StockQuote")
                             select new StockMarketDataPoint
                             {
                                 Open = value.Element("Open").GetDouble(),
                                 Close = value.Element("Close").GetDouble(),
                                 High = value.Element("High").GetDouble(),
                                 Low = value.Element("Low").GetDouble(),
                                 Volume = value.Element("Volume").GetDouble(),
                                 Date = value.Element("Date").GetDateTime()
                             }).ToList<StockMarketDataPoint>();

            // Notify client that the data from the xml file has been parsed.
            if (DataParsed != null) DataParsed(this, EventArgs.Empty);
        }
    }
    # endregion
    
   /// <summary>
   /// Models data about a company's stock.
   /// </summary>
    public class StockInformation
    {
        public string CompanyName;
        public string StockTicker;

        public DateTime StartDate;
        public DateTime EndDate;
    }

    public class LanguageInformation
    {
        public XmlLanguage Language
        {
            get
            {
                CultureInfo currentCulture = CultureInfo.CurrentCulture;
                return XmlLanguage.GetLanguage(currentCulture.Name);
            }
            set { }
        }
    }
}