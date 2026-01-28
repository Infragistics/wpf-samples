using System.Windows;
using Infragistics.Samples.Framework;

namespace IGDataTree.Samples.Editing
{
    /// <summary>
    /// Interaction logic for SelectedDataItems.xaml
    /// </summary>
    public partial class SelectedDataItems : SampleContainer
    {
        public SelectedDataItems()
        {
            InitializeComponent();
        }

        private void CheckBox_AvailableBooks_OnChecked(object sender, RoutedEventArgs e)
        {
            var booksNodeLayout = DataTree.NodeLayouts["BooksLayout"];

            if (booksNodeLayout != null)
            {
                booksNodeLayout.IsSelectedMemberPath = "IsAvailable";
                foreach (var node in booksNodeLayout.Tree.Nodes)
                {
                    node.IsExpanded = true;
                }
            }
        }

        private void CheckBox_AvailableBooks_OnUnchecked(object sender, RoutedEventArgs e)
        {
            DataTree.NodeLayouts["BooksLayout"].IsSelectedMemberPath = null;
            DataTree.SelectedDataItems = null;
        }
    }
}
