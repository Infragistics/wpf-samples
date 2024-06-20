using System.Windows;
using System.Windows.Data;
using Infragistics.Samples.Framework;

namespace IGDataGrid.Samples.Style
{
    /// <summary>
    /// Interaction logic for FixedFieldStyling.xaml
    /// </summary>
    public partial class FixedFieldStyling : SampleContainer
    {
        public FixedFieldStyling()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(FixedFieldStyling_Loaded);
        }

        void FixedFieldStyling_Loaded(object sender, RoutedEventArgs e)
        {
            XmlDataProvider source = this.TryFindResource("QuarterbackData") as XmlDataProvider;
            source.Source = Infragistics.Samples.Shared.DataProviders.XmlDataProvider.GetXmlUri("Quarterbacks.xml");
            source.XPath = "/QuarterBack";
        }
    }
}
