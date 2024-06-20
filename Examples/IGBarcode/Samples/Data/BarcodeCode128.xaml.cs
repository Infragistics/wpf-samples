using System.Collections.Generic;
using Infragistics.Controls.Barcodes;

namespace IGBarcode.Samples.Data
{
    public partial class BarcodeCode128 : Infragistics.Samples.Framework.SampleContainer
    {
        private List<string> _standardData = new List<string>()
        {
            "Code 128", "xamBarcode", "Lorem ipsum dolor", "!@#$%^&*()",
            "ABCDEFGHIJKLM", "The quick brown fox"
        };

        private List<string> _uccData = new List<string>() 
        { 
            "(11)100518(15)111018(10)17", "(00)193745682315478569", "(30)65487315(11)100518",
            "(21)4R6A8S1D32A1E5AS3", "(403)S321GS6D5F3AA", "(8008)65897458265",
            "(97)SD32F1GS6", "(423)345(7001)321656"
        };

        public BarcodeCode128()
        {
            InitializeComponent();

            ComboBoxCodeType.SelectedIndex = 0;
        }

        private void ComboBoxCodeType_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Code128CodeType codeType = (Code128CodeType)ComboBoxCodeType.SelectedItem;

            switch (codeType)
            {
                case Code128CodeType.Standard: ListBoxData.ItemsSource = _standardData; break;
                case Code128CodeType.Ucc: ListBoxData.ItemsSource = _uccData; break;
            }

            ListBoxData.SelectedIndex = 0;
        }
    }
}
