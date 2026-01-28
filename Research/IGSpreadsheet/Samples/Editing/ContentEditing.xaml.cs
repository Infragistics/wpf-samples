using IGSpreadsheet.Samples.Shared;
using IGSpreadsheet.Shared;
using Infragistics.Documents.Excel;
using Infragistics.Samples.Framework;
using Infragistics.Windows.Editors;
using Infragistics.Windows.Ribbon;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace IGSpreadsheet.Samples.Editing
{
    public partial class ContentEditing : SampleContainer
    {
        public ContentEditing()
        {
            InitializeComponent();
            InitializeSample();
            this.xamSpreadsheet1.Focus();
            Utils.SetDefaultDialogTitle(xamSpreadsheet1);
  
            // overriding default theme
                 
        }

        void InitializeSample()
        {
            // Provide the combo-box with a list of font families on the user's machine
            ComboBoxItemsProvider itemsProvider = new ComboBoxItemsProvider();
            itemsProvider.ItemsSource = Fonts.SystemFontFamilies;
            fontFamilyCombo.ItemsProvider = itemsProvider;
            LoadFile();
        }

        private void LoadFile()
        {
            Stream stream = Tools.GetLocalizedFileAsStream("Sample2.xlsx");
            Workbook wb = Workbook.Load(stream);
            if (wb != null)
            {
                this.xamSpreadsheet1.Workbook = wb;
            }
        }

        private void ButtonTool_Click(object sender, RoutedEventArgs e)
        {
            if (sender is ButtonTool)
            {
                string s = ((ButtonTool)sender).Tag.ToString();
                switch (s)
                {
                    case "Reload":
                        LoadFile();
                        break;
                }
            }
        }
    }
}
