using Infragistics.Controls.Interactions.Primitives;
using Infragistics.Samples.Framework;
using System.Windows;

namespace IGBusyIndicator.Samples.Display
{
    /// <summary>
    /// Interaction logic for BasicConfiguration.xaml
    /// </summary>
    public partial class Configuration : SampleContainer
    {
        public Configuration()
        {
            InitializeComponent();
            
        }
        
        private void BusyIndicator_IsBusyChanged(object sender, RoutedEventArgs e)
        {
            if (!BusyIndicator.IsBusy)
            {
                DataGrid.ActiveCell = null;
            }
        }
    }
}
