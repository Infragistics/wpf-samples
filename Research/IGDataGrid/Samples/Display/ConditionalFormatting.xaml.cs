using System.Windows.Data;
using Infragistics.Samples.Framework;

namespace IGDataGrid.Samples.Display
{
    /// <summary>
    /// Interaction logic for ConditionalFormatting.xaml
    /// </summary>
    public partial class ConditionalFormatting : SampleContainer
    {
        public ConditionalFormatting()
        {
            InitializeComponent();
            SetDataSourcePath();
        }

        void SetDataSourcePath()
        {
            XmlDataProvider source = this.TryFindResource("QuarterbackData") as XmlDataProvider;
            source.Source = Infragistics.Samples.Shared.DataProviders.XmlDataProvider.GetXmlUri("Quarterbacks.xml");
            source.XPath = "/QuarterBack";
        }
    }
}
