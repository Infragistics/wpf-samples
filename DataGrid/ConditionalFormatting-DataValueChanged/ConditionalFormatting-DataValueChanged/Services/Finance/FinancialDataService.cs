using FinancialDataGrid.Services.Finance.Business;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FinancialDataGrid.Services.Finance
{
    public interface IFinancialDataService
    {
        Task<List<FinancialRecord>> GetDataAsync(int numberOfRecords);
        void StartUpdatingPrices(List<FinancialRecord> data, int frequency);
        void StartUpdatingAllPrices(List<FinancialRecord> data, int frequency);
        void StopUpdatingPrices();
    }

    public class FinancialDataService : IFinancialDataService
    {
        Random _random = new Random();
        Timer _timer;

        public Task<List<FinancialRecord>> GetDataAsync(int numberOfRecords)
        {
            return Task.Run(() =>
            {
                return GetData(numberOfRecords);
            });
        }

        public List<FinancialRecord> GetData(int numberOfRecords)
        {
            List<FinancialRecord> items = new List<FinancialRecord>();

            for (int i = 0; i < numberOfRecords; i++)
            {
                var rand = new Random().Next(FinancialDataStore.FinancialDataItems.Count);
                var dataItem = FinancialDataStore.FinancialDataItems[rand];

                var region = FinancialDataStore.Regions[GenerateRandomNumber(0, 5)];

                var newFinancialRecord = new FinancialRecord()
                {
                    Category = dataItem.Category,                    
                    Type = dataItem.Type,
                    Contract = FinancialDataStore.Contracts[GenerateRandomNumber(0, 4)],
                    Settlement = FinancialDataStore.Settlements[GenerateRandomNumber(0,1)],
                    Region = region,
                    Country = RandomizeCountry(region),
                    OpenPrice = dataItem.OpenPrice,
                    Price = dataItem.Price,
                    Change = dataItem.Change,
                    ChangePercent = dataItem.ChangePercent,
                    Buy = dataItem.Buy,
                    Sell = dataItem.Sell,
                    Spread = dataItem.Spread,
                    Volume = dataItem.Spread,
                    HighD = dataItem.HighD,
                    LowD = dataItem.LowD,
                    HighY = dataItem.HighY,
                    LowY = dataItem.LowY,
                    StartY = dataItem.StartY,
                    IndGrou = "Airlines",
                    IndSect = "Consumer, Cyclical",
                    IndSubg = "Airlines",
                    SecType = "PUBLIC",
                    IssuerN = "AMERICAN AIRLINES GROUP",
                    Moodys = "WR",
                    Fitch = "N.A.",
                    DBRS = "N.A.",
                    CollatT = "NEW MONEY",
                    Curncy = "USD",
                    Security = "001765866 Pfd",
                    Sector = "Pfd",
                    CUSIP = 1765866,
                    Ticker = "AAL",
                    Cpn = 7.875,
                    Maturity = "7/13/1939",
                    KRD_3YR = 6E-05,
                    ZV_SPREAD = 28.302,
                    KRD_5YR = 0,
                    KRD_1YR = -0.00187,
                };

                RandomizePrice(newFinancialRecord);

                items.Add(newFinancialRecord);
            }

            return items;
        }

        public void StartUpdatingPrices(List<FinancialRecord> data, int frequency)
        {
            _timer = new Timer((x) =>
            {
                UpdatePrices(data);
            }, null, 0, frequency);
        }

        public void StartUpdatingAllPrices(List<FinancialRecord> data, int frequency)
        {
            _timer = new Timer((x) =>
            {
                UpdateAllPrices(data);
            }, null, 0, frequency);
        }

        public void StopUpdatingPrices()
        {
            _timer.Dispose();
        }

        void UpdateAllPrices(List<FinancialRecord> data)
        {
            foreach (var item in data)
            {
                RandomizePrice(item);
            }
        }

        void UpdatePrices(List<FinancialRecord> data)
        {
            var indexStart = Convert.ToInt32(Math.Round(_random.NextDouble() * 10));

            for (int x = indexStart; x < data.Count; x += Convert.ToInt32(Math.Round(_random.NextDouble() * 10)))
            {
                RandomizePrice(data[x]);
            }
        }

        int GenerateRandomNumber(int min, int max)
        {
            return new Random().Next(min, max);
        }

        void RandomizePrice(FinancialRecord item)
        {
            var price = GenerateNewPrice(item.Price);
            item.Change = price.Item1 - item.Price;
            item.Price = price.Item1;
            item.ChangePercent = price.Item2;
        }

        private Tuple<double, double> GenerateNewPrice(double oldPrice)
        {
            var rnd = new Random().NextDouble();
            var volatility = 2;

            var changePercent = 2 * volatility * rnd;
            if (changePercent > volatility)
            {
                changePercent -= (2 * volatility);
            }

            var changeAmount = oldPrice * (changePercent / 100);
            double newPrice = oldPrice + changeAmount;
            return new Tuple<double, double>(newPrice, changePercent);
        }

        string RandomizeCountry(string region)
        {
            var countries = FinancialDataStore.Countries[region];
            return countries[GenerateRandomNumber(0, countries.Length - 1)];
        }
    }
}
