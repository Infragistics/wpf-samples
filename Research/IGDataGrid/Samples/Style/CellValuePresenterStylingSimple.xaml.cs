using System.Windows;
using System.Windows.Data;
using Infragistics.Samples.Framework;

namespace IGDataGrid.Samples.Style
{
    /// <summary>
    /// Interaction logic for CellValuePresenterStylingSimple.xaml
    /// </summary>
    public partial class CellValuePresenterStylingSimple : SampleContainer
    {
        public CellValuePresenterStylingSimple()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(CellValuePresenterStylingSimple_Loaded);
        }

        void CellValuePresenterStylingSimple_Loaded(object sender, RoutedEventArgs e)
        {
            XmlDataProvider source = this.LayoutRoot.Resources["QuarterbackData"] as XmlDataProvider;
            source.Source = Infragistics.Samples.Shared.DataProviders.XmlDataProvider.GetXmlUri("Quarterbacks.xml");
            source.XPath = "/QuarterBack";
        }
    }
}
