using IGPropertyGrid.Samples.DataModel;
using Infragistics.Samples.Framework;
using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace IGPropertyGrid.Samples.Display
{
    public partial class Configuring : SampleContainer
    {
        public Configuring()
        {
            InitializeComponent();
            InitializeSampleData();
        }

        void InitializeSampleData()
        {
            ArrayList typesList = new ArrayList();
            typesList.Add(new Button());
            typesList.Add(new Grid());
            typesList.Add(new Page());
            typesList.Add(new StackPanel());
            typesList.Add(new TextBox());

            this.ListOfTypes.ItemsSource = typesList;
            this.ListOfTypes.SelectedIndex = 0;
        }

        private void cbCatDescendingSort_Click(object sender, RoutedEventArgs e)
        {
            if (this.cbCatDescendingSort.IsChecked ?? false)
            {
                this.xamPropertyGrid1.CategorySortComparer = new CategoryCustomComparer();
            }
            else
            {
                this.xamPropertyGrid1.CategorySortComparer = null;
            }
        }

        private void cbPropDescendingSort_Click(object sender, RoutedEventArgs e)
        {
            if (this.cbPropDescendingSort.IsChecked ?? false)
            {
                this.xamPropertyGrid1.PropertySortComparer = new PropertyCustomComparer();
            }
            else
            {
                this.xamPropertyGrid1.PropertySortComparer = null;
            }
        }
    }
}
