using System.Collections.ObjectModel;
using System.ComponentModel;
using IGFunnelChart.Resources;

namespace IGFunnelChart.Model
{
    public class TestData : ObservableCollection<TestDataItem>
    {
        public TestData()
        {
            Add(new TestDataItem()
            {
                Label = FunnelChartStrings.XFC_FunnelChart_Impressions,
                Value = 3000
            });
            Add(new TestDataItem()
            {
                Label = FunnelChartStrings.XFC_FunnelChart_Clicks,
                Value = 2000
            });
            Add(new TestDataItem()
            {
                Label = FunnelChartStrings.XFC_FunnelChart_FreeDownloads,
                Value = 1000
            });
            Add(new TestDataItem()
            {
                Label = FunnelChartStrings.XFC_FunnelChart_Purchase,
                Value = 500
            });
            Add(new TestDataItem()
            {
                Label = FunnelChartStrings.XFC_FunnelChart_RepeatPurchase,
                Value = 400
            });
        }
    }


    public class TestDataItem : INotifyPropertyChanged
    {
        private string _label;
        public string Label
        {
            get { return _label; }
            set { _label = value; RaisePropertyChanged("Label"); }
        }

        private double _value;
        public double Value
        {
            get { return _value; }
            set { _value = value; RaisePropertyChanged("Value"); }
        }

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
