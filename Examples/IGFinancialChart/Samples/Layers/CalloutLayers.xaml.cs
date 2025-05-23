﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace IGFinancialChart.Samples
{
    public partial class CalloutLayers : Infragistics.Samples.Framework.SampleContainer
    {
        public CalloutLayers()
        {
            InitializeComponent();                
        }
    }
    
    public class StockEvent
    { 
        public int Index { get; set; } 
        public double Value { get; set; } 
        public string Content { get; set; }
    }

    public class StockDataWithEvents : ObservableCollection<StockEvent>
    {
        public StockDataWithEvents()
        {
            Initialize();
        } 
        public List<StockList> DataSources { get; set; }
        public List<StockEvent> DataEvents { get; set; } 
         
        protected Random Rand = new Random();

        private void Initialize()
        {
            var start = DateTime.Now.Year - 2;
            var stop = DateTime.Now.Year + 1;
            var interval = TimeSpan.FromDays(1);
            DataSources = new List<StockList>();
            DataSources.Add(new StockList(start, 1, 1, stop, 1, 1, "TSLA", interval, 300, 30000));
            DataSources.Add(new StockList(start, 1, 1, stop, 1, 1, "MSFT", interval, 200, 20000));
          
            this.DataEvents = new List<StockEvent>(); 
             
            // generating data callouts/events based on multiple data sources
            foreach (StockList dataList in DataSources)
            {
                var intervalSplit = Rand.Next(280, 300); 
                var intervalDiv = Rand.Next(360, 400); 
                int index = 0;
                var priceLowest = new StockEvent { Value = double.MaxValue, Content = "MIN PRICE"};
                var priceHighest = new StockEvent { Value = double.MinValue, Content = "MAX PRICE"};
               
                foreach (StockItem item in dataList)
                {  
                    var stockEvent = new StockEvent { Index = index };

                    // finding item with lowest price
                    if (priceLowest.Value > item.Close)
                    {
                        priceLowest.Value = item.Close;
                        priceLowest.Index = index;  
                    }
                    // finding item with highest price
                    if (priceHighest.Value < item.Close)
                    {
                        priceHighest.Value = item.Close;
                        priceHighest.Index = index;  
                    }
                      
                    // creating SPLIT/DIVIDENT events at specific intervals
                    if (index % intervalSplit == 5)
                    {
                        stockEvent.Value = item.Close;
                        stockEvent.Content = "SPLIT";
                        this.DataEvents.Add(stockEvent);  
                    }  
                    else if (index % intervalDiv == 5)
                    {
                        stockEvent.Value = item.Close;
                        stockEvent.Content = "DIV";
                        this.DataEvents.Add(stockEvent);  
                    }   
                    index++;
                }      
                           
                this.DataEvents.Add(priceLowest);
                this.DataEvents.Add(priceHighest);                
            } 
        }      
    }

}
