using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using IGExtensions.Common.Models;
using Infragistics.Documents.Excel;

namespace IGExtensions.Common.Data
{
    /// <summary>
    /// Represents data provider with methods for loading airlines' flights and airports from csv files
    /// </summary>
    public class AirlinesDataProvider : AirlinesDataProviderBase
    {
        public AirlinesDataProvider()
        {
            IndiaFlightTimeMin = DateTime.MaxValue;
            IndiaFlightTimeMax = DateTime.MinValue;

            AmericanFlightTimeMin = DateTime.MaxValue;
            AmericanFlightTimeMax = DateTime.MinValue;
        }
        
        #region Airline Flights for US
        protected static DateTime FlightTimeNow = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
        protected DateTime FlightTimeMin = FlightTimeNow.AddHours(2);
        protected DateTime FlightTimeMax = FlightTimeNow.AddHours(24);
        protected Random FlightTimeGenrator = new Random();

        public DateTime AmericanFlightTimeMin { get; private set; }
        public DateTime AmericanFlightTimeMax { get; private set; }

        public void LoadAmericanFlightsAsync()
        {
            // create data provider for reading data from CSV file
            var dataAirportsProvider = new CsvDataProvider();
            dataAirportsProvider.GetDataCompleted += OnLoadAmericanAirportsCompleted;
            dataAirportsProvider.GetDataAsync("american_airports.csv");
        }
        private void OnLoadAmericanAirportsCompleted(object sender, CsvDataCompletedEventArgs e)
        {
            if (e.Error != null) return;

            this.Airports = ProcessAmericanAirportsData(e.Result);

            var dataFlightsProvider = new CsvDataProvider();
            dataFlightsProvider.GetDataCompleted += OnLoadAmericanFlightsCompleted;
            dataFlightsProvider.GetDataAsync("american_flights.csv");
        }
        private void OnLoadAmericanFlightsCompleted(object sender, CsvDataCompletedEventArgs e)
        {
            if (e.Error != null) return;
            this.Flights = ProcessAmericanFlightsData(e.Result);

            foreach (var airport in Airports)
            {
                foreach (var flight in Flights)
                {
                    if (airport.Code == flight.Origin.Code)
                    {
                        airport.Connections.Add(flight.Destination);
                        airport.ConnectionsTotal++;
                    }
                }
            }
            this.Airports = (from airport in this.Airports where airport.ConnectionsTotal >= 1 select airport).ToList();
            this.Airports.Sort((a1, a2) => -(Comparer<double>.Default.Compare(a1.ConnectionsTotal, a2.ConnectionsTotal)));

            OnLoadDataCompleted(new AirlinesDataCompletedEventArgs(this.Airports, this.Flights));
        }

        private List<Airport> ProcessAmericanAirportsData(List<string> lines)
        {
            var airports = new List<Airport>();
            foreach (var line in lines)
            {
                try
                {
                    var vals = line.Split(',');
                    if (vals.Length >= 7 && line != lines[0])
                    {
                        var airport = new Airport
                        {
                            CodeIATA = vals[0].Replace("\"", "").Trim(),
                            Name = vals[1].Replace("\"", "").Trim(),
                            City = vals[2].Replace("\"", "").Trim(),
                            State = vals[3].Replace("\"", "").Trim(),
                            Country = vals[4].Replace("\"", "").Trim(),
                            Latitude = double.Parse(vals[5]),
                            Longitude = double.Parse(vals[6])
                        };
                        airports.Add(airport);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed Generating Airports at line: " + line, ex);
                }
            }
            return airports;
        }
        private List<AirlineFlight> ProcessAmericanFlightsData(List<string> lines)
        {
            var flights = new List<AirlineFlight>();
            int index = 0;
            int maxFlights = 1000;
            foreach (var line in lines)
            {
                try
                {
                    var vals = line.Split(',');
                    if (vals.Length >= 5 && line != lines[0] && index < maxFlights)
                    {
                        var origin = (from airport in this.Airports where airport.CodeIATA == vals[1] select airport).First();
                        var destination = (from airport in Airports where airport.CodeIATA == vals[2] select airport).First();

                        var flight = new AirlineFlight(origin, destination)
                        {
                            CarrierCode = vals[0].Trim(),
                            CarrierName = vals[4].Trim(),
                            FlightNumber = int.Parse(vals[3]),
                        };
                        flight.DepartureTime = GetDepartureTime();
                        flight.ArrivalTime = GetArrivalTime(flight);

                        var min = System.Math.Min(this.AmericanFlightTimeMin.Ticks, flight.DepartureTime.Ticks);
                        this.AmericanFlightTimeMin = new DateTime(min);

                        var max = System.Math.Max(this.AmericanFlightTimeMax.Ticks, flight.ArrivalTime.Ticks);
                        this.AmericanFlightTimeMax = new DateTime(max);

                        flights.Add(flight);
                        index++;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed Generating Flights at line: " + line, ex);
                }
            }
            return flights;
        }
        private DateTime GetDepartureTime()
        {
            var year = FlightTimeNow.Year;
            var month = FlightTimeNow.Month;
            var day = FlightTimeNow.Day;
            var hour = FlightTimeGenrator.Next(2, 24);
            var minutes = FlightTimeGenrator.Next(0, 60);
            return new DateTime(year, month, day, hour, minutes, 0);
        }
        private DateTime GetArrivalTime(AirlineFlight flight)
        {
            var duration = TimeSpan.FromHours(flight.FlightPath.Distance / 500.0);
            var arrivalTime = flight.DepartureTime.Add(duration);
            if (arrivalTime.Ticks > FlightTimeMax.Ticks)
            {
                //arrivalTime = FlightTimeMax.Subtract(duration);
                arrivalTime = FlightTimeMax.Subtract(TimeSpan.FromHours(1));
                var departureTime = arrivalTime.Subtract(duration);
                //if (departureTime.Day < 12 || arrivalTime.Day < 12)
                //{
                //    bool stop = true;
                //}
                flight.DepartureTime = departureTime;
               
          }
        
            return arrivalTime;
        }

        #endregion

        #region Airline Flights for India
        public DateTime IndiaFlightTimeMin { get; private set; }
        public DateTime IndiaFlightTimeMax { get; private set; }

        public void LoadIndiaFlightsAsync()
        {
            // create data provider for reading data from CSV file
            var dataAirportsProvider = new ExcelDataProvider();
            dataAirportsProvider.GetDataCompleted += OnLoadIndiaFlightsCompleted;
            dataAirportsProvider.GetDataAsync("india_flights.xls");
        }
        private void OnLoadIndiaFlightsCompleted(object sender, ExcelDataCompletedEventArgs e)
        {
            if (e.Error != null) return;

            this.Flights = ProcessIndiaFlightData(e.Result);
           
            this.Airports = new List<Airport>();
            var airportsDictionary = new Dictionary<string, Airport>();
            foreach (var flight in Flights)
            {
                var key = flight.Origin.Code;
                if (!airportsDictionary.ContainsKey(key))
                {
                    airportsDictionary.Add(key, flight.Origin);
                    this.Airports.Add(flight.Origin);
                }
                key = flight.Destination.Code;
                if (!airportsDictionary.ContainsKey(key))
                {
                    airportsDictionary.Add(key, flight.Destination);
                    this.Airports.Add(flight.Destination);
                }
            }
            
            OnLoadDataCompleted(new AirlinesDataCompletedEventArgs(this.Flights));
            //OnLoadDataCompleted(EventArgs.Empty);
        }

        private List<AirlineFlight> ProcessIndiaFlightData(Workbook excelWorkbook)
        {
            var flights = new List<AirlineFlight>();
            var airports = new List<Airport>();

            Worksheet sheet = excelWorkbook.Worksheets[0];
            foreach (WorksheetRow row in sheet.Rows)
            {
                if (row.Index == 0) continue;

                string days = row.Cells[11].Value.ToString();
                if (!days.Contains("1")) continue;

                string departure = row.Cells[12].Value.ToString();
                while (departure.Length < 4) departure = "0" + departure;
                var departureTime = DateTime.ParseExact(departure, "HHmm", CultureInfo.InvariantCulture);

                string arrival = row.Cells[13].Value.ToString();
                while (arrival.Length < 4) arrival = "0" + arrival;
                var arrivalTime = DateTime.ParseExact(arrival, "HHmm", CultureInfo.InvariantCulture);

                var min = System.Math.Min(this.IndiaFlightTimeMin.Ticks, departureTime.Ticks);
                this.IndiaFlightTimeMin = new DateTime(min);

                var max = System.Math.Max(this.IndiaFlightTimeMax.Ticks, arrivalTime.Ticks);
                this.IndiaFlightTimeMax = new DateTime(max);

                var originPoint = new Point(double.Parse(row.Cells[2].Value.ToString()),
                                          double.Parse(row.Cells[3].Value.ToString()));
                var destinationPoint = new Point(double.Parse(row.Cells[6].Value.ToString()),
                                       double.Parse(row.Cells[7].Value.ToString()));
                var origin = new Airport(originPoint) { CodeIATA = row.Cells[0].Value.ToString() };
                var destination = new Airport(destinationPoint) { CodeIATA = row.Cells[4].Value.ToString() };

                var flight = new AirlineFlight(origin, destination)
                {
                    FlightNumber = row.Index,
                    //OriginLatitude = double.Parse(row.Cells[3].Value.ToString()),
                    //OriginLongitude = double.Parse(row.Cells[2].Value.ToString()),
                    //DestinationLatitude = double.Parse(row.Cells[7].Value.ToString()),
                    //DestinationLongitude = double.Parse(row.Cells[6].Value.ToString()),
                    DepartureTime = departureTime,
                    ArrivalTime = arrivalTime,
                    CarrierName = row.Cells[9].Value.ToString(),
                    CarrierCode = row.Cells[10].Value.ToString(),
                    //Origin = row.Cells[0].Value.ToString(),
                    //Destination = row.Cells[4].Value.ToString(),
                };

                flights.Add(flight);
            }
            return flights;
        }

        #endregion
       
        #region Airline Flights for the World
        public void LoadWorldsAirportsAsync(bool onlyMajorAirports = true)
        {
            // create data provider for reading data from CSV file
            var dataAirportsProvider = new CsvDataProvider();
            dataAirportsProvider.GetDataCompleted += OnLoadWorldsAirportsCompleted;
            if (onlyMajorAirports)
                dataAirportsProvider.GetDataAsync("world_airports_major.csv");
            else
                dataAirportsProvider.GetDataAsync("world_airports.csv");
        }
      
        private void OnLoadWorldsAirportsCompleted(object sender, CsvDataCompletedEventArgs e)
        {
            if (e.Error != null) return;
            this.Airports = ProcessWorldAirportsData(e.Result);
            //TODO add logic for generating flights
            OnLoadDataCompleted(new AirlinesDataCompletedEventArgs(this.Airports));
        }

        protected List<Airport> ProcessWorldAirportsData(List<string> lines)
        {
            var airports = new List<Airport>();
            foreach (var line in lines)
            {
                try
                {
                    var vals = line.Split(',');
                    if (vals.Length >= 11 && line != lines[0])
                    {
                        var airport = new Airport
                        {
                            ID = vals[0].Replace("\"", "").Trim(),
                            Name = vals[1].Replace("\"", "").Trim(),
                            City = vals[2].Replace("\"", "").Trim(),
                            Country = vals[3].Replace("\"", "").Trim(),
                            CodeIATA = vals[4].Replace("\"", "").Trim(),
                            CodeICAO = vals[5].Replace("\"", "").Trim(),
                            Latitude = double.Parse(vals[6]),
                            Longitude = double.Parse(vals[7]),
                            Altitude = double.Parse(vals[8]),
                            Timezone = vals[9].Replace("\"", "").Trim(),
                            TimeDST = vals[10].Replace("\"", "").Trim()
                        };
                        if (vals.Length >= 12) 
                            airport.Passangers = double.Parse(vals[11]);
                        //if (airport.Name == "All Airports") airport.Name = airport.City;
                        //if (airport.CodeICAO != string.Empty)
                        //    
                        airports.Add(airport);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed Generating Airports at line: " + line, ex);
                }
            }
            //foreach (var airport in airports)
            //{
            //    UpdateAirports(airport.City + airport.Country, airport);
            //}
            //var sortedAirports = airports; // AirportsDictionary.Values.ToList();
            //sortedAirports.Sort((x, y) => x.Country.CompareTo(y.Country));
            //var index = 1;
            
            //foreach (var airport in sortedAirports)
            //{
            //    var line = index + ",\"" + airport.Name + "\",\"" + airport.City + "\",\"" + airport.Country + "\",\"" +
            //             airport.CodeIATA + "\",\"" + airport.CodeICAO + "\"," + airport.Latitude + "," +
            //             airport.Longitude + "," + airport.Altitude + "," + airport.Timezone + ",\"" + airport.TimeDST + "\"";
            //    Debug.WriteLine(line);
            //    index++;
            //}
            //var dictionary = new Dictionary<string, Airport>();


            return airports;
        }
        protected Dictionary<string, Airport> AirportsDictionary = new Dictionary<string, Airport>();
        public void UpdateAirports(string key, Airport airport)
        {
            if (AirportsDictionary.ContainsKey(key))
            {
                AirportsDictionary[key] = airport;
            }
            else
            {
                AirportsDictionary.Add(key, airport);
                //this.Filters.Add(dataViewSource.FilterSettings);
            }
        }
        #endregion
    }
    public abstract class AirlinesDataProviderBase
    {
        protected AirlinesDataProviderBase()
        {
            Airports = new List<Airport>();
            Flights = new List<AirlineFlight>();
        }
        public List<Airport> Airports { get; protected set; }
        public List<AirlineFlight> Flights { get; protected set; }
      
        public event EventHandler<AirlinesDataCompletedEventArgs> LoadDataCompleted;
        public void OnLoadDataCompleted(AirlinesDataCompletedEventArgs eventArgs)
        {
            if (this.LoadDataCompleted != null)
            {
                this.LoadDataCompleted(this, eventArgs);
            }
        }
    }
    public class AirlinesDataCompletedEventArgs : EventArgs
    {
        public AirlinesDataCompletedEventArgs(List<Airport> airports)
            : this(airports, new List<AirlineFlight>())
        { }
        public AirlinesDataCompletedEventArgs(List<AirlineFlight> flights)
            : this(new List<Airport>(), flights)
        { }
        public AirlinesDataCompletedEventArgs(List<Airport> airports, List<AirlineFlight> flights)
            : this(airports, flights, null)
        { }
        public AirlinesDataCompletedEventArgs(List<Airport> airports, List<AirlineFlight> flights, object userState)
        {
            this.Airports = airports;
            this.Flights = flights;
            this.Error = null;
            this.HasError = false;
            this.UserState = userState;
        }
        public AirlinesDataCompletedEventArgs(Exception error)
        {
            this.Airports = null;
            this.Flights = null;
            this.Error = error;
            this.HasError = true;
            this.UserState = null;
        }

        /// <summary>
        /// Gets loaded Airports  
        /// </summary>
        public List<Airport> Airports { get; private set; }
        /// <summary>
        /// Gets loaded Airline Flights  
        /// </summary>
        public List<AirlineFlight> Flights { get; private set; }
        /// <summary>
        /// Gets an Exception associated with loading the CSV file
        /// </summary>
        public Exception Error { get; private set; }
        /// <summary>
        /// Gets if there were error when loading the CSV file
        /// </summary>
        public bool HasError { get; private set; }
        public object UserState { get; set; }

    }

  
}