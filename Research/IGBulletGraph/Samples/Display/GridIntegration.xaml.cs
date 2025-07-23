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

namespace IGBulletGraph
{
    public partial class GridIntegration : Infragistics.Samples.Framework.SampleContainer
    {
        public GridIntegration()
        {
            InitializeComponent();

            this.DataContext = new ObservableCollection<MyItem>() 
            {
                new MyItem(){ Month=Thread.CurrentThread.CurrentCulture.DateTimeFormat.MonthNames[0], Min=0, Max=750, Consumption=555, Production=550, Ranges = new double[]{500,640} },
                new MyItem(){ Month=Thread.CurrentThread.CurrentCulture.DateTimeFormat.MonthNames[1], Min=0, Max=750, Consumption=670, Production=620, Ranges = new double[]{333,567} },
                new MyItem(){ Month=Thread.CurrentThread.CurrentCulture.DateTimeFormat.MonthNames[2], Min=0, Max=750, Consumption=670, Production=700, Ranges = new double[]{320,567} },
                new MyItem(){ Month=Thread.CurrentThread.CurrentCulture.DateTimeFormat.MonthNames[3], Min=0, Max=750, Consumption=610, Production=666, Ranges = new double[]{320,567} }
            };
        }
    }
}

public class MyItem : INotifyPropertyChanged
{
    string month;
    public string Month
    {
        get { return month; }
        set
        {
            month = value;
            if (PropertyChanged!=null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Month"));
            }
        }
    }

    double min;
    public double Min
    {
        get { return min; }
        set
        {
            min = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Min"));
            }
        }
    }

    double max;
    public double Max
    {
        get { return max; }
        set
        {
            max = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Max"));
            }
        }
    }

    double actualValue;
    public double Consumption
    {
        get { return actualValue; }
        set
        {
            actualValue = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Consumption"));
            }
        }
    }

    double production;
    public double Production
    {
        get { return production; }
        set
        {
            production = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Production"));
            }
        }
    }

    double[] ranges;
    public double[] Ranges
    {
        get { return ranges; }
        set
        {
            ranges = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Ranges"));
            }
        }
    }
    
    public event PropertyChangedEventHandler PropertyChanged;
}


 