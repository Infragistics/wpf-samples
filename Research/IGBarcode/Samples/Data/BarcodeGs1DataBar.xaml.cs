using System.Collections.Generic;
using System.Windows.Controls;
using Infragistics.Controls.Barcodes;

namespace IGBarcode.Samples.Data
{
    public partial class BarcodeGs1DataBar : Infragistics.Samples.Framework.SampleContainer
    {
        private List<string> _limitedData = new List<string>() 
        { 
            "021598745624","01609709898","01318654398",
            "02198456624","01926709988","025159759524","16969635089"
        };

        private List<string> _expandedData = new List<string>()
         { 
            "(00)193745682315478569", "(30)65487315(11)100518",
            "(21)4R6A8S1D32A1E5AS3", "(403)S321GS6D5F3AA", "(8008)65897458265",
            "(97)SD32F1GS6", "(423)345(7001)321656"
        };

        public BarcodeGs1DataBar()
        {
            InitializeComponent();

            ComboBoxCodeType.SelectedIndex = 0;
        }

        private void ComboBoxCodeType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GS1CodeType selectedCodeType = (GS1CodeType)ComboBoxCodeType.SelectedItem;

            switch (selectedCodeType)
            {
                case GS1CodeType.Omnidirectional:
                case GS1CodeType.Truncated:
                case GS1CodeType.Stacked:
                case GS1CodeType.StackedOmnidirectional:
                case GS1CodeType.Limited:
                    ListBoxData.ItemsSource = _limitedData;
                    break;
                case GS1CodeType.Expanded:
                    ListBoxData.ItemsSource = _expandedData;
                    break;
            };

            ListBoxData.SelectedIndex = 0;
        }
    }
}
