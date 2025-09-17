using Infragistics.Samples.Framework;
using System.Windows.Data;
using IGDataProviders = Infragistics.Samples.Shared.DataProviders;

namespace IGEditors.Samples.Styling
{
    /// <summary>
    /// Interaction logic for TextEditor_EditingTemplate.xaml
    /// </summary>

    public partial class TextEditor_EditingTemplate : SampleContainer
    {
        public TextEditor_EditingTemplate()
        {
            InitializeComponent();

            XmlDataProvider source = this.TryFindResource("DepartmentData") as XmlDataProvider;
            source.Source = IGDataProviders.XmlDataProvider.GetXmlUri("Departments.xml");
            source.XPath = "/departments";
        }
    }
}