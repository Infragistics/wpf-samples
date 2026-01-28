using System.Windows;
using System.Windows.Data;
using Infragistics.Samples.Framework;

namespace IGDataGrid.Samples.Style
{
    /// <summary>
    /// Interaction logic for GroupByAreaStyling.xaml
    /// </summary>
    public partial class GroupByAreaStyling : SampleContainer
    {
        public GroupByAreaStyling()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(GroupByAreaStyling_Loaded);
        }

        void GroupByAreaStyling_Loaded(object sender, RoutedEventArgs e)
        {
            XmlDataProvider source = this.TryFindResource("QuarterbackData") as XmlDataProvider;
            source.Source = Infragistics.Samples.Shared.DataProviders.XmlDataProvider.GetXmlUri("Quarterbacks.xml");
            source.XPath = "/QuarterBack";
        }
    }
}
