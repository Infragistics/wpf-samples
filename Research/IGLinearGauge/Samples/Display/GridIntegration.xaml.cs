using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Resources;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using System.Windows.Media;
using System.Collections.ObjectModel;
using System.Collections;
using System.Linq;
using Infragistics.Controls.Charts;
using System.ComponentModel;
using System.Windows.Data;
using System.Threading;
using System;

namespace IGLinearGauge
{
    public partial class GridIntegration : Infragistics.Samples.Framework.SampleContainer
    {
        public GridIntegration()
        {
            InitializeComponent();

            this.DataContext = new ObservableCollection<MyItem>() 
            {
                new MyItem(){ Date= new DateTime(2013,7,16,10,0,0), WindSpeed= 3.305 },
                new MyItem(){ Date= new DateTime(2013,7,16,11,0,0), WindSpeed= 5.832 },
                new MyItem(){ Date= new DateTime(2013,7,16,12,0,0), WindSpeed= 5.637 },
                new MyItem(){ Date= new DateTime(2013,7,16,13,0,0), WindSpeed= 8.747 },
                new MyItem(){ Date= new DateTime(2013,7,16,14,0,0), WindSpeed= 8.553 },
                new MyItem(){ Date= new DateTime(2013,7,16,15,0,0), WindSpeed= 7.581 },
                new MyItem(){ Date= new DateTime(2013,7,16,16,0,0), WindSpeed= 7.775 },
                new MyItem(){ Date= new DateTime(2013,7,16,17,0,0), WindSpeed= 6.026 },
                new MyItem(){ Date= new DateTime(2013,7,16,18,0,0), WindSpeed= 6.803 },
                new MyItem(){ Date= new DateTime(2013,7,16,19,0,0), WindSpeed= 0.583 },
                new MyItem(){ Date= new DateTime(2013,7,16,20,0,0), WindSpeed= 6.609 },
                new MyItem(){ Date= new DateTime(2013,7,16,21,0,0), WindSpeed= 0.000 },
            };
        }
    }
}

public class MyItem : INotifyPropertyChanged
{
    double windSpeed;
    public double WindSpeed
    {
        get { return windSpeed; }
        set
        {
            windSpeed = value;
            if (PropertyChanged!=null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("WindSpeed"));
            }
        }
    } 

    DateTime date;
    public DateTime Date
    {
        get { return date; }
        set
        {
            date = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Date"));
            }
        }
    }   
    
    public event PropertyChangedEventHandler PropertyChanged;
}


 