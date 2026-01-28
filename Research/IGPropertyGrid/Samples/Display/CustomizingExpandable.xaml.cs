using IGPropertyGrid.Samples.DataModel;
using Infragistics.Samples.Framework;
using System.Threading;
using System.Windows;

namespace IGPropertyGrid.Samples.Display
{
    public partial class CustomizingExpandable : SampleContainer
    {
        public CustomizingExpandable()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            // Instantiate test data objects and fill the employees collection
            if (Thread.CurrentThread.CurrentCulture.Name.ToLower() == "ja-jp")
            {
                this.xamPropertyGrid1.SelectedObject = new OrganizationJA();
            }
            else
            {
                this.xamPropertyGrid1.SelectedObject = new OrganizationEN();
            }
        }
    }
}
