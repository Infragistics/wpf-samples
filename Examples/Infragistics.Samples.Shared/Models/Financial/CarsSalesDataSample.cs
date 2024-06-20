using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace Infragistics.Samples.Shared.Models
{
    public class CarsSalesDataSample
    {
        
    }
    public class CarsSalesDataProvider
    {
        public CarsSalesDataProvider()
        {
            this.Data = new ObservableCollection<CarsSales>(CarsSales.GenerateData());
        }

        public ObservableCollection<CarsSales> Data { get; set; }
    }
    public class CarsSales
    {

        #region Fields
        private static readonly String[] SellerNames = new[] { "Alex", "Bill", "Bill", "Alex", "Bill", "Kate", "Jason", "John", "John", "Maria", "John" };
        private static readonly String[] Countries = new[] { "Italy", "Germany", "Germany", "Belgium", "US", "Italy", "US", "Italy", "US", "France", "Argentina" };
        private static readonly String[] Cities = new[] { "Milano", "Düsseldorf", "Berlin", "Brussel", "Boston", "Milano", "Boston", "Milano", "Boston", "Paris", "Buenos Aires" };
        //private static readonly int[] SoldUnits = new[] { 1, 1, 2, 3, 2, 4, 2, 1, 1, 1, 2, 3, 1, 1, 2, 3, 2, 4, 2, 1, 1, 1, 2, 3 };

        private static readonly int[] CarsNewUnits = new[] { 1, 1, 2, 3, 2, 4, 2, 1, 1, 1, 2, 3, 1, 1, 2, 3, 2, 4, 2, 1, 1, 1, 2, 3 };
        private static readonly int[] CarsUsedUnits = new[] { 4, 2, 1, 1, 1, 2, 3, 1, 1, 3, 3, 2, 4, 2, 1, 1, 1, 2, 1, 1, 1, 2, 2, 3 };

        private static readonly double[] Costs = new[] { 1500.0, 2000.0, 2200.0, 5200.0, 4000.0, 3100.0, 15100.0, 7100.0, 8900.0, 4900.0, 3400.0 };
        private static readonly double[] Profits = new[] { 1700.0, 2300.0, 2800.0, 5500.0, 4500.0, 4100.0, 15600.0, 7500.0, 9300.0, 5600.0, 4400.0 };
        private static readonly DateTime[] SalesDates = new[] { new DateTime(2009, 12, 03), new DateTime(2010, 11, 12), new DateTime(2007, 07, 15), new DateTime(2010, 11, 03), new DateTime(2006, 06, 12), new DateTime(2008, 02, 07), new DateTime(2007, 08, 14), new DateTime(2009, 07, 01), new DateTime(2007, 06, 23), new DateTime(2010, 10, 03), new DateTime(2009, 10, 13) };

        //cars
        private static readonly String[] CarBrands = new[] { "Peugeot", "Peugeot", "Peugeot", "Peugeot", "Peugeot", "Ford", "Mercedes", "Citroen", "Citroen", "Citroen", "Seat" };
        private static readonly String[] CarModels = new[] { "206", "206", "306", "206", "206", "KA", "Benz", "C3", "C2", "C4", "Toledo" };
        private static readonly String[] CarStyles = new[] { "XS", "XS", "XS", "SW", "SW", "", "", "", "", "", "" };

        private static readonly int[] Years = new[] { 1998, 1999, 2001, 2006, 2006, 2002, 2002, 2005, 2001, 2007, 2008 };

        //sales regions
        private static readonly String[] SalesContinents = new[] { "Europe", "Europe", "Europe", "Europe", "Europe", "North America", "North America", "North America", "North America", "Australia", "South America", "Asia", "Africa" };
        private static readonly String[] SalesCountries = new[] { "Bulgaria", "Germany", "Italy", "France", "Bulgaria", "US", "Canada", "Canada", "Canada", "Australia", "Argentina", "China", "Egypt" };
        private static readonly String[] SalesCities = new[] { "Sofia", "Berlin", "Rome", "Paris", "Plovdiv", "New York", "Toronto", "Vancouver", "Vancouver", "Sidney", "Buenos Aires", "Shanghai", "Kairo" };

        #endregion

        #region Properties
        [DisplayAttribute(Name = "Sold Total Units")]
        public int SoldTotalUnits { get; set; }

        [DisplayAttribute(Name = "Sold Used Units")]
        public int SoldUsedUnits { get; set; }

        [DisplayAttribute(Name = "Sold New Units")]
        public int SoldNewUnits { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double Cost { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public double Profit { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MM yy}")]
        [DisplayAttribute(Name = "Sales Date")]
        public DateTime SalesDate { get; set; }
        
        [DisplayAttribute(Name = "Sales Region")]
        public CarsSalesRegion SalesRegion { get; set; }

        [DisplayAttribute(Name = "Car")]
        public Car Car { get; set; } 

        //[DisplayAttribute(Name = "Sales Person", Description = "The names of the sales person.")]
        //public string Seller { get; set; }
        //[DisplayAttribute(Name = "OriginalCountry")]
        //public string OriginalCountry { get; set; }

        //[DisplayAttribute(Name = "OriginalCity")]
        //public string OriginalCity { get; set; }

        //[DisplayAttribute(Name = " 1mil City")]
        //public string MCity { get; set; }

        //public double Agr { get; set; }

        //[DisplayFormat(DataFormatString = "{0:####}")]
   
        #endregion

        #region Methods
        public static List<Car> GenerateCars()
        {
            List<Car> cars = new List<Car>();

            int year = DateTime.Now.Year - 3;
            //for (int i = 0; i < 11; i++)
            //{
            //    Car car = new Car();
            //    car.Brand = CarBrands[i];
            //    car.Model = CarModels[i];
            //    car.Modification = CarStyles[i];
            //    car.Year = Years[i];
            //    cars.Add(car);
            //}

            cars.Add(new Car { Year = year + 1, Brand = "Audi", Model = "A4" });
            cars.Add(new Car { Year = year + 2, Brand = "Audi", Model = "A4" });
            cars.Add(new Car { Year = year + 3, Brand = "Audi", Model = "A4" });
            cars.Add(new Car { Year = year + 1, Brand = "Audi", Model = "A5" });
            cars.Add(new Car { Year = year + 2, Brand = "Audi", Model = "A5" });
            cars.Add(new Car { Year = year + 3, Brand = "Audi", Model = "A5" });

            cars.Add(new Car { Year = year + 1, Brand = "Ford", Model = "Focus" });
            cars.Add(new Car { Year = year + 2, Brand = "Ford", Model = "Focus" });
            cars.Add(new Car { Year = year + 3, Brand = "Ford", Model = "Focus" });
            cars.Add(new Car { Year = year + 1, Brand = "Ford", Model = "Explorer" });
            cars.Add(new Car { Year = year + 2, Brand = "Ford", Model = "Explorer" });
            cars.Add(new Car { Year = year + 3, Brand = "Ford", Model = "Explorer" });

            cars.Add(new Car { Year = year + 1, Brand = "Peugeot", Model = "206" });
            cars.Add(new Car { Year = year + 2, Brand = "Peugeot", Model = "206" });
            cars.Add(new Car { Year = year + 3, Brand = "Peugeot", Model = "206" });
            cars.Add(new Car { Year = year + 1, Brand = "Peugeot", Model = "306" });
            cars.Add(new Car { Year = year + 2, Brand = "Peugeot", Model = "306" });
            cars.Add(new Car { Year = year + 3, Brand = "Peugeot", Model = "306" });

            cars.Add(new Car { Year = year + 1, Brand = "Toyota", Model = "Camry" });
            cars.Add(new Car { Year = year + 2, Brand = "Toyota", Model = "Camry" });
            cars.Add(new Car { Year = year + 3, Brand = "Toyota", Model = "Camry" });
            cars.Add(new Car { Year = year + 1, Brand = "Toyota", Model = "Prius" });
            cars.Add(new Car { Year = year + 2, Brand = "Toyota", Model = "Prius" });
            cars.Add(new Car { Year = year + 3, Brand = "Toyota", Model = "Prius" });

            return cars;
        }

        public static List<string> GenerateCities()
        {
            List<string> cities = new List<string>();
            for (int i = 0; i < 500; i++)
            {
                cities.Add("City " + i);
            }
            return cities;
        }

        public static List<CarsSalesRegion> GenerateSalesRegions()
        {
            List<CarsSalesRegion> salesRegions = new List<CarsSalesRegion>();

            //for (int i = 0; i < 13; i++)
            //{
            //    CarsSalesRegion region = new CarsSalesRegion();
            //    region.Continent = SalesContinents[i];
            //    region.Country = SalesCountries[i];
            //    region.City = SalesCities[i];
            //    salesRegions.Add(region);
            //}
            salesRegions.Add(new CarsSalesRegion { Continent = "Europe", Country = "Germany", City = "Berlin" });
            salesRegions.Add(new CarsSalesRegion { Continent = "Europe", Country = "Germany", City = "Munich" });
            salesRegions.Add(new CarsSalesRegion { Continent = "Europe", Country = "Germany", City = "Hamburg" });

            salesRegions.Add(new CarsSalesRegion { Continent = "Europe", Country = "France", City = "Paris" });
            salesRegions.Add(new CarsSalesRegion { Continent = "Europe", Country = "France", City = "Lyon" });
            salesRegions.Add(new CarsSalesRegion { Continent = "Europe", Country = "France", City = "Marseille " });

            salesRegions.Add(new CarsSalesRegion { Continent = "Europe", Country = "Italy", City = "Rome" });
            salesRegions.Add(new CarsSalesRegion { Continent = "Europe", Country = "Italy", City = "Milan" });
            salesRegions.Add(new CarsSalesRegion { Continent = "Europe", Country = "Italy", City = "Turin" });

            salesRegions.Add(new CarsSalesRegion { Continent = "North America", Country = "United States", City = "New York" });
            salesRegions.Add(new CarsSalesRegion { Continent = "North America", Country = "United States", City = "Los Angeles" });
            salesRegions.Add(new CarsSalesRegion { Continent = "North America", Country = "United States", City = "Philadelphia" });

            salesRegions.Add(new CarsSalesRegion { Continent = "North America", Country = "Canada", City = "Toronto" });
            salesRegions.Add(new CarsSalesRegion { Continent = "North America", Country = "Canada", City = "Vancouver" });
            salesRegions.Add(new CarsSalesRegion { Continent = "North America", Country = "Canada", City = "Ottawa" });

            salesRegions.Add(new CarsSalesRegion { Continent = "South America", Country = "Brazil", City = "Sao Paulo" });
            salesRegions.Add(new CarsSalesRegion { Continent = "South America", Country = "Brazil", City = "Rio de Janeiro" });
            salesRegions.Add(new CarsSalesRegion { Continent = "South America", Country = "Brazil", City = "Salvador" });

            salesRegions.Add(new CarsSalesRegion { Continent = "Asia", Country = "China", City = "Shanghai" });
            salesRegions.Add(new CarsSalesRegion { Continent = "Asia", Country = "China", City = "Beijing" });
            salesRegions.Add(new CarsSalesRegion { Continent = "Asia", Country = "China", City = "Hong Kong" });

            salesRegions.Add(new CarsSalesRegion { Continent = "Asia", Country = "Japan", City = "Tokyo" });
            salesRegions.Add(new CarsSalesRegion { Continent = "Asia", Country = "Japan", City = "Yokohama" });
            salesRegions.Add(new CarsSalesRegion { Continent = "Asia", Country = "Japan", City = "Osaka" });

            return salesRegions;
        }

        public static IEnumerable<CarsSales> GenerateData()
        {
            List<CarsSales> carsSales = new List<CarsSales>();
            List<Car> cars = GenerateCars();
            List<CarsSalesRegion> salesRegions = GenerateSalesRegions();
            List<string> salesCities = GenerateCities();

            for (int i = 0; i < 100000; i++)
            {
                CarsSales carSale = new CarsSales();
                //carSale.Seller = SellerNames[i % SellerNames.Length];
                //carSale.OriginalCountry = Countries[i % Countries.Length];
                //carSale.OriginalCity = Cities[i % Cities.Length];

                carSale.Cost = Costs[i % Costs.Length];
                carSale.SoldNewUnits = CarsNewUnits[i % CarsNewUnits.Length];
                carSale.SoldUsedUnits = CarsUsedUnits[i % CarsUsedUnits.Length];
                carSale.SoldTotalUnits = carSale.SoldNewUnits + carSale.SoldUsedUnits;

                carSale.Profit = Profits[i % Profits.Length];
                carSale.SalesDate = SalesDates[i % SalesDates.Length];
                carSale.Car = cars[i % cars.Count];
                carSale.SalesRegion = salesRegions[i % salesRegions.Count];
                //carSale.MCity = salesCities[i % salesCities.Count];
                carsSales.Add(carSale);
            }

            //CarsSales tempCar1 = new CarsSales();
            //tempCar1.SalesTerritory = salesTerritory[11];
            //CarsSales.Add(tempCar1);

            //CarsSales tempCar2 = new CarsSales();
            //tempCar2.SalesTerritory = salesTerritory[12];
            //CarsSales.Add(tempCar2);

            return carsSales;
        }

        #endregion
    }
}