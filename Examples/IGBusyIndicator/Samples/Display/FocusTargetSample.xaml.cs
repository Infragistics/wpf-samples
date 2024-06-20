using Infragistics.Samples.Framework;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace IGBusyIndicator.Samples.Display
{
    /// <summary>
    /// Interaction logic for FocusAfterSample.xaml
    /// </summary>
    public partial class FocusTargetSample : SampleContainer
    {
        public FocusTargetSample()
        {
            InitializeComponent();
            Loaded += FocusTargetSampleLoaded;
            
        }

        private void FocusTargetSampleLoaded(object sender, RoutedEventArgs e)
        {
            var collection = new List<Element>
                                 {
                                     new Element() {Name = "DataGrid", PageElement = this.DataGrid},
                                     new Element() {Name = "ContactName", PageElement = this.ContactName},
                                     new Element() {Name = "CompanyName", PageElement = this.CompanyName},
                                     new Element() {Name = "Phone", PageElement = this.Phone},
                                     new Element() {Name = "DeliveryDate", PageElement = this.DeliveryDate},
                                     new Element() {Name = "DeliveryQuantity", PageElement = this.DeliveryQuantity}
                                 };

            this.ElementsCombo.ItemsSource = collection;
        }

        private void BusyIndicator_IsBusyChanged(object sender, RoutedEventArgs e)
        {
            if (!BusyIndicator.IsBusy)
            {
                DataGrid.ActiveCell = null;
            }
        }
    }

    public class Element
    {
        public string Name { get; set; }
        public UIElement PageElement { get; set; }
    }
}
