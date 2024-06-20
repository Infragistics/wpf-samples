using System.Windows.Media;
using Infragistics.Samples.Framework;
using Infragistics.Windows.Editors;

namespace IGRibbon.Samples.Data
{
    public partial class PopulatingTheComboEditorTool : SampleContainer
    {
        public PopulatingTheComboEditorTool()
        {
            InitializeComponent();

            // Provide the combo with a list of font families on the user's machine
            ComboBoxItemsProvider itemsProvider = new ComboBoxItemsProvider();
            itemsProvider.ItemsSource = Fonts.SystemFontFamilies;
            fontFamilyCombo.ItemsProvider = itemsProvider;
        }
    }
}
