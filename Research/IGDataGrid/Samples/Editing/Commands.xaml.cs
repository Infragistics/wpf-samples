using System.Windows.Data;
using Infragistics.Samples.Framework;

namespace IGDataGrid.Samples.Editing
{
    /// <summary>
    /// Interaction logic for Commands.xaml
    /// </summary>
    public partial class Commands : SampleContainer
    {
        public Commands()
        {
            InitializeComponent();
            this.Loaded += new System.Windows.RoutedEventHandler(Commands_Loaded);
        }

        void Commands_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            XmlDataProvider source = this.TryFindResource("QuarterbackData") as XmlDataProvider;
            source.Source = Infragistics.Samples.Shared.DataProviders.XmlDataProvider.GetXmlUri("Quarterbacks.xml");
            source.XPath = "/QuarterBack";
        }
    }
}
