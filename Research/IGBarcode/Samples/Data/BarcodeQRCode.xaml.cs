using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using IGBarcode.Resources;
using Infragistics.Controls.Barcodes;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Controls;

namespace IGBarcode.Samples.Data
{
    public partial class BarcodeQRCode : SampleContainer
    {
        public BarcodeQRCode()
        {
            InitializeComponent();
            ComboBoxEncodingMode.ItemsSource = new Dictionary<EncodingMode, string>()
                {
                    {EncodingMode.Undefined, BarcodeStrings.XWB_Undefined},
                    {EncodingMode.Alphanumeric, BarcodeStrings.XWB_Alphanumeric},
                    {EncodingMode.Byte, BarcodeStrings.XWB_Byte},
                    {EncodingMode.Kanji, BarcodeStrings.XWB_Kanji},
                    {EncodingMode.Numeric, BarcodeStrings.XWB_Numeric}
                };
            ComboBoxEncodingMode.SelectedIndex = 0;

            ComboBoxEciNumber.ItemsSource = new List<EciNumberWithSampleData>()
                {
                    new EciNumberWithSampleData(0, "(CP 437)", "Sample string."),
                    new EciNumberWithSampleData(1, "(ISO-8859-1)", "Sample string."),
                    new EciNumberWithSampleData(2, "(CP 437)", "Sample string."),
                    new EciNumberWithSampleData(3, "(ISO-8859-1)", "http://www.infragistics.com/"),
                    new EciNumberWithSampleData(4, "(ISO-8859-2)", "Šta ima novoga? Doviđenja!"), // Western and Central Europe
                    new EciNumberWithSampleData(5, "(ISO-8859-3)", "żerriegħa tal-ġunġliel"), // Western Europe and South European (Turkish, Maltese plus Esperanto)
                    new EciNumberWithSampleData(6, "(ISO-8859-4)", "Ar norite ką nors išgerti?"), // Western Europe and Baltic countries (Lithuania, Estonia and Lapp)
                    new EciNumberWithSampleData(7, "(ISO-8859-5)", "Здравей! Как си?"), // Latin/Cyrillic alphabet
                    new EciNumberWithSampleData(8, "(ISO-8859-6)", "مساء الخير!"), // Latin/Arabic alphabet
                    new EciNumberWithSampleData(9, "(ISO-8859-7)", "Καλή σας μέρα!"), // Latin/Greek alphabet
                    new EciNumberWithSampleData(10, "(ISO-8859-8)", "מה שלומך? תודה!"), // Latin/Hebrew alphabet
                    new EciNumberWithSampleData(11, "(ISO-8859-9)", "İyi akşamlar! Nasılsınız?"), // Turkish
                    new EciNumberWithSampleData(13, "(ISO-8859-11)", "สวัสดี! คุณเป็นอย่างไร?"), // Latin/Thai alphabet
                    new EciNumberWithSampleData(15, "(ISO-8859-13)", "Millal järgmine buss väljub?"), // Estonian plus Polish
                    new EciNumberWithSampleData(17, "(ISO-8859-15)", "Bé, gràcies, i tu? Bé, gràcies, i vostè?"), // Latin - 9
                    new EciNumberWithSampleData(20, "(Shift JIS)", "漢字"), // JIS8 and Shift JIS character sets - Japanese
                    new EciNumberWithSampleData(21, "(Windows-1250)", "Šta ima novoga? Doviđenja!"), // Windows 1250 : Latin 2 (Central Europe)
                    new EciNumberWithSampleData(22, "(Windows-1251)", "Здравей! Как си?"), // Cyrillic (Slavic)
                    new EciNumberWithSampleData(23, "(Windows-1252)", "Sample string."), // Latin 1 (ANSI)
                    new EciNumberWithSampleData(24, "(Windows-1256)", "مرحبا! كيف حالك؟"), // Arabic
                    new EciNumberWithSampleData(25, "(UTF 16)", "水 water"), // UTF16; UCS-2 ( Universal Multiple-Byte Coded Character Set)
                    new EciNumberWithSampleData(26, "(UTF 8)", "Sample string."), // UTF-8 (UCS Transformation Format 8) 
                    new EciNumberWithSampleData(27, "(ISO-646-US)", "Good old ASCII."), // International Reference Version of ISO 7-bit coded character set
                    new EciNumberWithSampleData(28, "(Big5)", "牛鬼蛇神的文字"), // Big 5 (Taiwan) Chinese Character Set
                    new EciNumberWithSampleData(29, "(GB 2312)", "ひらがな"), // GB (PRC) Chinese Character Set - supports both simplified and traditional Chinese characters
                    new EciNumberWithSampleData(30, "(KSC-5601)", "한국어/조선말 Καλή σας μέρα!"), // KS X 1001 (Korean Graphic Character Set for Information Interchange) 
                                                                                                  //previously known as KS C 5601 - contains ASCII, Korean, typography, 
                                                                                                  //Greek, Cyrillic, Japanese (Hiragana and Katakana) and some other characters.
                };
            //If the culture is Japanese, set the EciNumber to 20 - Shift JIS.
            ComboBoxEciNumber.SelectionChanged += (s, e) => Barcode.Data = ((EciNumberWithSampleData)(ComboBoxEciNumber.SelectedItem)).SampleData;
            ComboBoxEciNumber.SelectedIndex = Thread.CurrentThread.CurrentCulture.Name.Contains("ja") ? 16 : 3;

            Cmb_EciHeaderDisplayMode.ItemsSource = EnumValuesProvider.GetEnumValues<HeaderDisplayMode>();
            Cmb_EciHeaderDisplayMode.SelectedIndex = (int)Barcode.EciHeaderDisplayMode;
        }

        private void Cmb_EciHeaderDisplayMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {            
            Barcode.EciHeaderDisplayMode = (HeaderDisplayMode)this.Cmb_EciHeaderDisplayMode.SelectedIndex;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Barcode.Data = SampleDataText.Text;
        }
    }

    public class EciNumberWithSampleData
    {
        public EciNumberWithSampleData(int eciNumber, string description, string sampleData)
        {
            this.EciNumber = eciNumber;
            this.Description = description;
            this.SampleData = sampleData;
        }

        public int EciNumber { get; set; }
        public string Description { get; set; }
        public string SampleData { get; set; }
    }
}