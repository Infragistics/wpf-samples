using Infragistics.Samples.Framework;
using System.Windows;

namespace IGMultiColumnComboEditor.Samples.Editing
{
    /// <summary>
    /// Interaction logic for SelectedValuesProperty.xaml
    /// </summary>
    public partial class SelectedValuesProperty : SampleContainer
    {
        public SelectedValuesProperty()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            ListBoxCustomers.ItemsSource = ComboEditorProducts.SelectedValues;
        }
    }
}
