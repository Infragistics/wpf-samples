using IgOutlook.Infrastructure;
using Infragistics.Controls.Menus;
using Infragistics.Windows;
using Infragistics.Windows.OutlookBar;
using System.Windows;

namespace IgOutlook.Modules.Calendar.Menus
{
    public partial class CalendarGroup : OutlookBarGroup, IOutlookBarGroup
    {
        public CalendarGroup(CalendarGroupViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void ActiveNodeChanging(object sender, ActiveNodeChangingEventArgs e)
        {
            if (e.NewActiveTreeNode == null) return;

            var _selectedItem = e.NewActiveTreeNode.Data as INavigationItem;
            if (_selectedItem != null && !_selectedItem.CanNavigate)
                e.Cancel = true;
        }

        public string DefaultNavigationPath
        {
            get { return "CalendarView"; }
        }

        private void tree_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //Simulate MS Outlook selection behaviour
            if (e.OriginalSource is DependencyObject)
            {
                var expansionIndicator = Utilities.GetAncestorFromName((DependencyObject)e.OriginalSource, "ExpansionIndicator");

                if (expansionIndicator != null)
                    return;

                var dataTreeNodeControl = (XamDataTreeNodeControl)Utilities.GetAncestorFromType((DependencyObject)e.OriginalSource, typeof(XamDataTreeNodeControl), false);

                if (dataTreeNodeControl != null)
                {
                    var dataContext = (XamDataTreeNodeDataContext)dataTreeNodeControl.DataContext;

                    if (dataContext == null) return;

                    //Unselect root node
                    if (dataContext.Node.HasChildren && dataContext.Node.IsChecked != false && tree.SelectionSettings.SelectedNodes.Count != 1)
                    {
                        foreach (var childNode in dataContext.Node.Nodes)
                        {
                            if (tree.SelectionSettings.SelectedNodes.Contains(childNode))
                            {
                                tree.SelectionSettings.SelectedNodes.Remove(childNode);
                                childNode.IsActive = false;
                                childNode.IsChecked = false;
                            }
                        }

                        if (tree.SelectionSettings.SelectedNodes.Count == 0 && tree.Nodes.Count > 1 && tree.Nodes[0].HasChildren)
                        {
                            var firstChildNode = tree.Nodes[0].Nodes[0];
                            firstChildNode.IsActive = true;
                            firstChildNode.IsChecked = true;
                            tree.SelectionSettings.SelectedNodes.Add(firstChildNode);
                        }
                    }
                    //Select root node
                    else if (dataContext.Node.HasChildren && (dataContext.Node.IsChecked == false || tree.SelectionSettings.SelectedNodes.Count == 1))
                    {
                        foreach (var childNode in dataContext.Node.Nodes)
                        {
                            if (!tree.SelectionSettings.SelectedNodes.Contains(childNode))
                            {
                                tree.SelectionSettings.SelectedNodes.Add(childNode);
                                childNode.IsActive = true;
                                childNode.IsChecked = true;
                            }
                        }

                        if (tree.SelectionSettings.SelectedNodes.Count == 0 && tree.Nodes.Count > 1 && tree.Nodes[0].HasChildren)
                        {
                            var firstChildNode = tree.Nodes[0].Nodes[0];
                            firstChildNode.IsActive = true;
                            firstChildNode.IsChecked = true;
                            tree.SelectionSettings.SelectedNodes.Add(firstChildNode);
                        }
                    }
                    else
                    {
                        if (tree.SelectionSettings.SelectedNodes.Contains(dataContext.Node))
                        {
                            if (tree.SelectionSettings.SelectedNodes.Count == 1)
                            {
                                e.Handled = true;
                                return;
                            }

                            tree.SelectionSettings.SelectedNodes.Remove(dataContext.Node);
                            dataContext.Node.IsActive = false;
                            dataContext.Node.IsChecked = false;
                        }
                        else
                        {
                            tree.SelectionSettings.SelectedNodes.Add(dataContext.Node);
                            dataContext.Node.IsActive = true;
                            dataContext.Node.IsChecked = true;
                        }
                    }

                }
            }

            e.Handled = true;
        }


    }
}
