using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models;
using System.Linq;
using System.Windows;

namespace IGComboEditor.Samples.Editing
{
    public partial class WorkingWithComboEditorItems : SampleContainer
    {
        public WorkingWithComboEditorItems()
        {
            InitializeComponent();
        }

        private void BtnAddItem_Click(object sender, RoutedEventArgs e)
        {
            if (!itemName.Text.Equals(""))
            {
                // Created new business data object
                var person = new Customer {ContactName = itemName.Text};

                // Creates new ComboEditorItem
                var comboEditorItem = ComboEditor.Items.CreateItem(person);
                ComboEditor.Items.Add(comboEditorItem);
                ComboEditor.Items.Last().IsSelected = true;

                itemName.Text = "";
            }
            else
            {
                itemName.Focus();
            }
        }

        private void BtnDeleteItem_Click(object sender, RoutedEventArgs e)
        {
            if (ComboEditor.SelectedIndex > -1)
            {
                // Removes selected item
                ComboEditor.Items.RemoveAt(ComboEditor.SelectedIndex);
            }
        }

        private void BtnDisableItem_Click(object sender, RoutedEventArgs e)
        {
            if (ComboEditor.SelectedIndex > -1)
            {
                // Disables selected item
                ComboEditor.Items[ComboEditor.SelectedIndex].IsEnabled = false;
            }
        }
    }
}