using System.Windows;
using System.Windows.Data;
using Infragistics.Samples.Framework;

namespace IGDataGrid.Samples.Style
{
    /// <summary>
    /// Interaction logic for Filtering_Styling.xaml
    /// </summary>
    public partial class Filtering_Styling : SampleContainer
    {
        public Filtering_Styling()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(Filtering_Styling_Loaded);
        }

        void Filtering_Styling_Loaded(object sender, RoutedEventArgs e)
        {
            XmlDataProvider source = this.TryFindResource("QuarterbackData") as XmlDataProvider;
            source.Source = Infragistics.Samples.Shared.DataProviders.XmlDataProvider.GetXmlUri("Quarterbacks.xml");
            source.XPath = "/QuarterBack";
        }
    }
}
