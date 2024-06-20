using System.Windows;
using System.Windows.Data;
using Infragistics.Samples.Framework;

namespace IGDataGrid.Samples.Style
{
    /// <summary>
    /// Interaction logic for CellValuePresenterStylingAdvanced.xaml
    /// </summary>
    public partial class CellValuePresenterStylingAdvanced : SampleContainer
    {
        public CellValuePresenterStylingAdvanced()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(CellValuePresenterStylingAdvanced_Loaded);
        }

        private void CellValuePresenterStylingAdvanced_Loaded(object sender, RoutedEventArgs e)
        {
            XmlDataProvider source = this.LayoutRoot.Resources["QuarterbackData"] as XmlDataProvider;
            source.Source = Infragistics.Samples.Shared.DataProviders.XmlDataProvider.GetXmlUri("Quarterbacks.xml");
            source.XPath = "/QuarterBack";
        }
    }
}
