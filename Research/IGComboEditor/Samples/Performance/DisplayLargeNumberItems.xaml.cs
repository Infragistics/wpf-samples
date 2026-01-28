using System.Collections.Generic;
using System.Windows;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models;

namespace IGComboEditor.Samples.Performance
{
    public partial class DisplayLargeNumberItems : SampleContainer
    {
        private List<Order> orderList = new List<Order>();

        public DisplayLargeNumberItems()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            PopulateDataSource();
        }

        private void PopulateDataSource()
        {
            // Creates a list with 5000 entries
            for (int i = 0; i <= 5000; i++)
            {
                Order order = new Order();
                order.OrderId = i.ToString();
                orderList.Add(order);
            }

            // Populates combo Items
            msComboBox.ItemsSource = orderList;
            msComboBox.DisplayMemberPath = "OrderId";

            // Populates combo Items
            igComboBox.ItemsSource = orderList;
            igComboBox.DisplayMemberPath = "OrderId";
        }
    }
}
