using System.Windows.Controls;
using System.Windows.Data;
using Infragistics.Windows.DataPresenter;
using IGDataProviders = Infragistics.Samples.Shared.DataProviders;

namespace IGCalculationManager.Samples.Data
{
    public partial class DataPresenterIntegration : Infragistics.Samples.Framework.SampleContainer
    {
        public DataPresenterIntegration()
        {
            InitializeComponent();
            InitializeDataSource();
        }

        void InitializeDataSource()
        {
            XmlDataProvider source = this.TryFindResource("QuarterbackData") as XmlDataProvider;
            source.Source = IGDataProviders.XmlDataProvider.GetXmlUri("Quarterbacks.xml");
            source.XPath = "/QuarterBack";
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.dataPresenter != null)
            {
                ComboBoxItem cbi = (ComboBoxItem)((ComboBox)sender).SelectedItem;
                if (cbi.Content.ToString().Contains("Carousel"))
                {
                    dataPresenter.View = null;
                    dataPresenter.View = new CarouselView();
                    this.dataPresenter.FieldLayoutSettings.LabelLocation = LabelLocation.InCells;
                }
                else if (cbi.Content.ToString().Contains("Grid"))
                {
                    dataPresenter.View = null;
                    dataPresenter.View = new Infragistics.Windows.DataPresenter.GridView();
                    this.dataPresenter.FieldLayoutSettings.LabelLocation = LabelLocation.Default;
                }
                else
                {
                    dataPresenter.View = null;
                    dataPresenter.View = new CardView();
                    this.dataPresenter.FieldLayoutSettings.LabelLocation = LabelLocation.Default;
                }
            }
        }
    }
}
