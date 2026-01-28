using System.Windows;
using System.Windows.Data;
using Infragistics.Samples.Framework;

namespace IGDataGrid.Samples.Style
{
    /// <summary>
    /// Interaction logic for AlternatingRowHighlighting.xaml
    /// </summary>
    public partial class AlternatingRowHighlighting : SampleContainer
    {
        public AlternatingRowHighlighting()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(AlternatingRowHighlighting_Loaded);
        }

        private void AlternatingRowHighlighting_Loaded(object sender, RoutedEventArgs e)
        {
            XmlDataProvider source = this.TryFindResource("QuarterbackData") as XmlDataProvider;
            source.Source = Infragistics.Samples.Shared.DataProviders.XmlDataProvider.GetXmlUri("Quarterbacks.xml");
            source.XPath = "/QuarterBack";
        }
    }
}
