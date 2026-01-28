using System.Windows;
using System.Windows.Controls;

namespace IGResourceWasher.Samples.Display
{
    public partial class WasherWashGroups : Page
    {
        public WasherWashGroups()
        {
            InitializeComponent();
        }

        private void btnWash_Click(object sender, RoutedEventArgs e)
        {
            resWasher1.SourceDictionary = this.Resources;
        }
    }
}
