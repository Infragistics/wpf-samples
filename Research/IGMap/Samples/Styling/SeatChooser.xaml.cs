using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Xml.Linq;
using IGMap.Resources;
using Infragistics.Controls.Maps;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Models;

namespace IGMap.Samples.Styling
{
    public partial class SeatChooser : Infragistics.Samples.Framework.SampleContainer
    {
        private bool _isViewModelLoaded;
        private bool _isMapLayerImported;

        private SeatChooserViewModel _viewModel = new SeatChooserViewModel();

        public SeatChooser()
        {
            InitializeComponent();

            _isViewModelLoaded = false;
            _isMapLayerImported = false;

            _viewModel.DataLoaded += OnViewModelDataLoaded;
            this.Loaded += OnSampleLoaded;
        }

        private void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            theMap.ElementClick += OnMapElementClick;
            theMap.ElementHover += OnMapElementHover;
            theMap.ElementUnhover += OnMapElementUnhover;
            theMap.Layers[0].Imported += OnMapLayerImported;

            this.LayoutRoot.DataContext = _viewModel;
        }

        #region Event Handlers

        private void OnMapElementUnhover(object sender, MapElementHoverEventArgs e)
        {
            e.Element.StrokeThickness = 0;
        }

        private void OnMapElementHover(object sender, MapElementHoverEventArgs e)
        {
            if (_viewModel.IsSeat((int) e.Element.Value))
            {
                e.Element.StrokeThickness = 1;
                e.Element.Stroke = new SolidColorBrush(Color.FromArgb(255, 255, 190, 0));
                _viewModel.SetSelectedSeat((int) e.Element.Value);
            }
        }

        private void OnMapElementClick(object sender, MapElementClickEventArgs e)
        {
            Debug.WriteLine("Element Clicked: " + e.Element.Value);
            _viewModel.SetSelectedSeat((int) e.Element.Value);
        }

        private void OnViewModelDataLoaded(object sender, EventArgs e)
        {
            _isViewModelLoaded = true;
            if (_isMapLayerImported)
            {
                DisplaySeats();
            }
        }

        private void OnMapLayerImported(object sender, MapLayerImportEventArgs e)
        {
            MapLayer layer = sender as MapLayer;


            foreach (MapElement element in layer.Elements)
            {
                // Hide if these are stage elements
                if (element.Value == 402 || element.Value == 401)
                {
                    element.Visibility = Visibility.Collapsed;
                }

                element.Fill = new SolidColorBrush(Colors.Transparent);
                element.Foreground = new SolidColorBrush(Colors.Transparent);
            }

            _isMapLayerImported = true;
            if (_isViewModelLoaded)
            {
                DisplaySeats();
            }
        }

        #endregion Event Handlers

        private void DisplaySeats()
        {
            MapLayer layer = theMap.Layers[0];

            // Set Element Fills based on Seat Availability
            foreach (MapElement element in layer.Elements)
            {
                if (_viewModel.IsSeatSelected(Int32.Parse(element.Value.ToString())))
                    element.Fill = new SolidColorBrush(Color.FromArgb(128, 255, 255, 255));
                else
                    element.Fill = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            }
        }
    }

    #region ViewModel
    public class SeatChooserViewModel : ObservableModel
    {
        private XDocument _doc;
        private readonly XmlDataProvider _xmlDataProvider;
        public SeatChooserViewModel()
        {
            // create service that will load data from xml file
            _xmlDataProvider = new XmlDataProvider();
            _xmlDataProvider.GetXmlDataCompleted += OnGetXmlDataCompleted;
            _xmlDataProvider.GetXmlDataAsync("TheatreSeats.xml"); // xml file name
        }

        private void OnGetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
            _doc = e.Result;
            if (this.DataLoaded != null)
                this.DataLoaded(this, EventArgs.Empty);
        }

        public event EventHandler DataLoaded;

        public bool IsSeat(int value)
        {
            return true;
        }

        public bool IsSeatSelected(int value)
        {
            var entry = from ent in _doc.Descendants("Seats").Descendants("Seat")
                        where Int32.Parse(ent.Attribute("ID").Value.ToString()) == value
                        select ent.Attribute("Available").Value;

            string isAvail = "o";
            if (entry != null && entry.Count() > 0)
                isAvail = entry.Single().ToString();

            if (isAvail == "x")
                return false;
            else
                return true;


        }

        public void SetSelectedSeat(int value)
        {
            if (_doc == null)
                return;

            var entry = from ent in _doc.Descendants("Seats").Descendants("Seat")
                        where Int32.Parse(ent.Attribute("ID").Value.ToString()) == value
                        select new Seat(ent.Attribute("SeatName").Value.ToString(), ent.Attribute("Available").Value);

            if (entry != null && entry.Count() > 0)
            {
                Seat currentSeat = (Seat)entry.Single();
                if (currentSeat == null)
                    return;

                this.SeatName = currentSeat.SeatName;
                if (String.IsNullOrEmpty(this.SeatName))
                    return;

                string section = currentSeat.SeatName.Substring(currentSeat.SeatName.Length - 1);

                switch (section)
                {
                    case "A":
                    case "B":
                    case "C":
                        this.SeatPrice = MapStrings.XWM_SeatChooser_PriceString.Replace("{amount}", "155");
                        this.SeatSection = MapStrings.XWM_SeatChooser_Section_DressCircle;
                        break;

                    case "D":
                    case "E":
                    case "F":
                    case "G":
                    case "H":
                        this.SeatPrice = MapStrings.XWM_SeatChooser_PriceString.Replace("{amount}", "125");
                        this.SeatSection = MapStrings.XWM_SeatChooser_Section_Orchestra;
                        break;

                    case "I":
                    case "J":
                    case "K":
                    case "L":
                        this.SeatPrice = MapStrings.XWM_SeatChooser_PriceString.Replace("{amount}", "95");
                        this.SeatSection = MapStrings.XWM_SeatChooser_Section_GrandTier;
                        break;

                    default:
                        this.SeatPrice = MapStrings.XWM_SeatChooser_PriceString.Replace("{amount}", "65");
                        this.SeatSection = MapStrings.XWM_SeatChooser_Section_Mezzanine;
                        break;
                }
            }
        }

        private ShapeFileReader _reader;
        public ShapeFileReader Reader
        {
            get
            {
                return _reader;
            }
            set
            {
                _reader = value;
                this.OnPropertyChanged("Reader");
            }
        }

        private string _seatName;
        public string SeatName
        {
            get
            {
                return _seatName;
            }
            set
            {
                _seatName = value;
                this.OnPropertyChanged("SeatName");
            }
        }

        private string _seatSection;
        public string SeatSection
        {
            get
            {
                return _seatSection;
            }
            set
            {
                _seatSection = value;
                this.OnPropertyChanged("SeatSection");
            }
        }

        private string _seatPrice;
        public string SeatPrice
        {
            get
            {
                return _seatPrice;
            }
            set
            {
                _seatPrice = value;
                this.OnPropertyChanged("SeatPrice");
            }
        }
    }
    #endregion ViewModel

    public class Seat
    {
        public string SeatName { get; set; }
        public string IsAvailable { get; set; }

        public Seat(string seatName, string isAvailable)
        {
            this.SeatName = seatName;
            this.IsAvailable = isAvailable;
        }
    }
}