using System.Windows;
using System.Windows.Controls;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;

namespace IGSpellChecker.Samples.Organization
{
    public partial class MultiLingualDictionaries : SampleContainer
    {
        public MultiLingualDictionaries()
        {
            InitializeComponent();
            Loaded += MultiLingualDictionaries_Loaded;
        }

        void MultiLingualDictionaries_Loaded(object sender, RoutedEventArgs e)
        {
            UsDictionaryRadionButton.IsChecked = true;
        }

        private void Dictionary_Checked(object sender, RoutedEventArgs e)
        {
            string dictValue = ((RadioButton)sender).Tag.ToString();
            if (dictValue != null)
            {
                simpleSpellChecker.DictionaryUri = DictionaryFileProvider.GetDictionaryLocalUri(dictValue);
            }
        }
    }
}