using IGBusyIndicator.Samples.DataProviders;
using Infragistics.Samples.Framework;
using System.Windows;

namespace IGBusyIndicator.Samples.Display
{
    /// <summary>
    /// Interaction logic for DisplayAfterSample.xaml
    /// </summary>
    public partial class DisplayAfterSample : SampleContainer
    {
        private DisplayAfterViewModel _viewmodel = null;

        public DisplayAfterSample()
        {
            InitializeComponent();
            Loaded += DisplayAfterSampleLoaded;           
            
       }

        private void DisplayAfterSampleLoaded(object sender, RoutedEventArgs e)
        {
            if (_viewmodel == null)
            {
                _viewmodel = new DisplayAfterViewModel();
            }

            this.DataContext = _viewmodel;
        }

        private void BusyIndicatorIsIndicatorVisibleChanged(object sender, RoutedEventArgs e)
        {
            if (BusyIndicator.IsIndicatorVisible)
            {
                _viewmodel.StopCommand.Execute(null);
            }
        }
    }

    
}
