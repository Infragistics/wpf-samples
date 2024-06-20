using IGCategoryChart.Samples.Models;
using Infragistics.Controls.Charts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace IGCategoryChart.Samples.ViewModels
{
    public class VariedCollectionViewModel
    {
       
        public ObservableCollection<SixDataValueItem> SmallSixDataPtCollection { get; set; }
        public ObservableCollection<SixDataValueItem> LargeSixDataPtCollection { get; set; }
        public ObservableCollection<List<SingleDataValueItem>> CompoundCollection {get;set;}


        public VariedCollectionViewModel()
        {
            CreateDataCollections();            
        }
        
        private void CreateDataCollections()
        {
            Random random = new Random();
            int year = 2014;
            int month = 10;
            SmallSixDataPtCollection = new ObservableCollection<SixDataValueItem>();           
            LargeSixDataPtCollection = new ObservableCollection<SixDataValueItem>();
          
            CompoundCollection = new ObservableCollection<List<SingleDataValueItem>>();

            for (int i=0;i<40;i++)
            {
                string label = GetLabel(ref year,ref month);
                LargeSixDataPtCollection.Add(
                           new SixDataValueItem(label,random.Next(0,20), random.Next(10,30),
                            random.Next(20,40),random.Next(30,50),random.Next(40,60), random.Next(0,50)));         
                
            }
            year = 2015;
            month = 10;
            for (int i = 0; i < 15; i++)
            {
                string label = GetLabel(ref year, ref month);
                SmallSixDataPtCollection.Add(new SixDataValueItem(label,random.Next(0,20), random.Next(10,30),
                        random.Next(20, 40), random.Next(30, 50), random.Next(40, 60), random.Next(0, 50)));
                
            }
           
            CompoundCollection = new ObservableCollection<List<SingleDataValueItem>>();
            var collection = new List<SingleDataValueItem>();
            year = 2014;
            month = 10;
            for (int i = 0; i < 40; i++)
            {
                string label = GetLabel(ref year, ref month);
                collection.Add(new SingleDataValueItem(label, random.Next(0, 50)));
                
            }
            CompoundCollection.Add(collection);
            year = 2013;
            month = 12;
            collection = new List<SingleDataValueItem>();
            for (int i=0;i<20;i++)
            {
                string label = GetLabel(ref year, ref month);
                collection.Add(new SingleDataValueItem(label,random.Next(0,50)));
                
            }
            CompoundCollection.Add(collection);
            year = 2013;
            month = 12;       
            collection = new List<SingleDataValueItem>();
            for (int i=0;i<35;i++)
            {
                string label = GetLabel(ref year, ref month);
                collection.Add(new SingleDataValueItem(label,random.Next(0,50)));
               
            }
            CompoundCollection.Add(collection);
           

        }
  
        private string GetLabel(ref int year,ref int month)
        {          
            if (month == 12)
            {
                month = 1;
                year++;
            }
            else
                month++;
            string label = String.Format("{0}{1:00}", year, month);
            return label;
        }
    }

   

   

    
}
