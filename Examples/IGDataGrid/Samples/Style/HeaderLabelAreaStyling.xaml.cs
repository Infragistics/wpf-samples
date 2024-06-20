using System.Windows;
using System.Windows.Data;
using Infragistics.Samples.Framework;

namespace IGDataGrid.Samples.Style
{
    /// <summary>
    /// Interaction logic for HeaderLabelAreaStyling.xaml
    /// </summary>
    public partial class HeaderLabelAreaStyling : SampleContainer
    {
        public HeaderLabelAreaStyling()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(HeaderLabelAreaStyling_Loaded);
        }

        void HeaderLabelAreaStyling_Loaded(object sender, RoutedEventArgs e)
        {
            XmlDataProvider source = this.LayoutRoot.Resources["QuarterbackData"] as XmlDataProvider;
            source.Source = Infragistics.Samples.Shared.DataProviders.XmlDataProvider.GetXmlUri("Quarterbacks.xml");
            source.XPath = "/QuarterBack";
        }
    }
}
