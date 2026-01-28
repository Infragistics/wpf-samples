using System.Collections.Generic;
using System.Windows.Controls;
using Infragistics.Controls.Barcodes;
using Infragistics.Samples.Framework;

namespace IGBarcode.Samples.Data
{
    public partial class BarcodeEanUpc : SampleContainer
    {
        private List<string> _ean13Data = new List<string>()
                        {
                            "144178012098", "530228761030", "174100509647", "560909171074",
                            "509647174100", "112457891129", "247566241028", "309738752107",
                            "801685281009", "690627818107", "695256908100", "122658251076"
                        };

        private List<string> _ean8Data = new List<string>()
                       {
                            "1441780", "5302287", "6471509", "5609091",
                            "5096471", "1124578", "2475662", "8752107",
                            "8016852", "6906278", "6952569", "8251076",
                            "9983206", "2457979", "8449736", "9302106",
                            "1326748", "4464661"
                       };

        private List<string> _upcAData = new List<string>()
                       {
                            "14417812098", "30228761030", "50947174100", "56009171074",
                            "74100509647", "12457891129", "24766241028", "30938752107",
                            "80168581009", "90627818107", "69556908100", "12258251076"
                       };

        private List<string> _upcEData = new List<string>()
                       {
                            "0441780", "0302287", "0696471", "0609091",
                            "0196471", "0124578", "0475662", "0752107",
                            "0016852", "0906278", "0952569", "0251076"
                       };

        public BarcodeEanUpc()
        {
            InitializeComponent();
            ComboBoxCodeTypes.SelectedIndex = 0;
        }
        //This method generates the data which
        //the barcode control is going to encode
        private void ComboBoxCodeTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var codeType = (EanUpcCodeType)ComboBoxCodeTypes.SelectedItem;

            switch (codeType)
            {
                case EanUpcCodeType.Ean13: ListBoxData.ItemsSource = _ean13Data; break;
                case EanUpcCodeType.Ean8: ListBoxData.ItemsSource = _ean8Data; break;
                case EanUpcCodeType.UpcA: ListBoxData.ItemsSource = _upcAData; break;
                case EanUpcCodeType.UpcE: ListBoxData.ItemsSource = _upcEData; break;
            }

            ListBoxData.SelectedIndex = 0;
        }
    }
}
