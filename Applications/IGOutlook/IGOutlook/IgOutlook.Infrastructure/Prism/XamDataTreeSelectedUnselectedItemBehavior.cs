using Infragistics.Controls.Menus;
using System;
using System.Windows;

namespace IgOutlook.Infrastructure.Prism
{
    public class XamDataTreeSelectedUnselectedItemBehavior : DependencyObject
    {
        #region SelectedDataItem

        public static readonly DependencyProperty SelectedDataItemProperty = DependencyProperty.RegisterAttached("SelectedDataItem", typeof(object), typeof(XamDataTreeSelectedUnselectedItemBehavior), new PropertyMetadata(null, OnSelectedDataItemChanged));

        public static void SetSelectedDataItem(XamDataTree tree, object value)
        {
            tree.SetValue(SelectedDataItemProperty, value);
        }

        public static string GetSelectedDataItem(XamDataTree tree)
        {
            return tree.GetValue(SelectedDataItemProperty) as string;
        }

        private static void OnSelectedDataItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            XamDataTree tree = (XamDataTree)d;

            if (e.NewValue == null) return;

            var node = FindTreeNodeFromDataItem(tree, e.NewValue);

            if (node != null)
            {
                if (!tree.SelectionSettings.SelectedNodes.Contains(node))
                {
                    tree.SelectionSettings.SelectedNodes.Add(node);
                    node.IsActive = true;
                    node.IsChecked = true;
                }
            }
        }

        #endregion //SelectedDataItem

        #region UnselectedDataItem

        public static readonly DependencyProperty UnselectedDataItemProperty = DependencyProperty.RegisterAttached("UnselectedDataItem", typeof(object), typeof(XamDataTreeSelectedUnselectedItemBehavior), new PropertyMetadata(null, null, CoerceUnselectedDataItem));

        public static void SetUnselectedDataItem(XamDataTree tree, object value)
        {
            tree.SetValue(UnselectedDataItemProperty, value);
        }

        public static string GetUnselectedDataItem(XamDataTree tree)
        {
            return tree.GetValue(UnselectedDataItemProperty) as string;
        }

        private static object CoerceUnselectedDataItem(DependencyObject d, object value)
        {
            XamDataTree tree = (XamDataTree)d;

            if (value == null) return value;

            var node = FindTreeNodeFromDataItem(tree, value);

            if (tree.SelectionSettings.SelectedNodes.Contains(node))
            {
                tree.SelectionSettings.SelectedNodes.Remove(node);
                node.IsActive = false;
                node.IsChecked = false;
            }

            return value;
        }
        #endregion //UnselectedDataItem

        #region DefaultSelectedDataItemProperty

        public static readonly DependencyProperty DefaultSelectedDataItemProperty = DependencyProperty.RegisterAttached("DefaultSelectedDataItem", typeof(object), typeof(XamDataTreeSelectedUnselectedItemBehavior), new PropertyMetadata(null, OnDefaultSelectedDataItemChanged));

        public static void SetDefaultSelectedDataItem(XamDataTree tree, object value)
        {
            tree.SetValue(DefaultSelectedDataItemProperty, value);
        }

        public static string GetDefaultSelectedDataItem(XamDataTree tree)
        {
            return tree.GetValue(DefaultSelectedDataItemProperty) as string;
        }

        private static void OnDefaultSelectedDataItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            XamDataTree tree = (XamDataTree)d;

            if (e.NewValue == null) return;

            if (tree.SelectionSettings.SelectedNodes.Count > 1) return;

            tree.Loaded += Tree_Loaded;
        }

        private static void Tree_Loaded(object sender, RoutedEventArgs e)
        {
            XamDataTree tree = (XamDataTree)sender;
            tree.Loaded -= Tree_Loaded;

            var node = FindTreeNodeFromDataItem(tree, tree.GetValue(DefaultSelectedDataItemProperty));

            if (node != null)
            {
                if (!node.IsSelected)
                {
                    node.IsSelected = true;
                    node.IsActive = true;
                }
            }
        }

        #endregion //DefaultSelectedDataItemProperty

        #region Private Methods

        public static XamDataTreeNode FindTreeNodeFromDataItem(XamDataTree tree, object dataItem)
        {
            XamDataTreeNode targetNode = null;

            Action<XamDataTreeNode, object> findNode = (n, dItem) =>
            {
                if (n.HasChildren)
                {
                    foreach (var item in n.Nodes)
                    {
                        if (targetNode != null) return;

                        if (item.Data == dItem)
                        {
                            targetNode = item;
                        }
                    }
                }
            };

            foreach (var node in tree.Nodes)
            {
                if (node.Data == dataItem)
                {
                    return node;
                }
            }

            foreach (var node in tree.Nodes)
            {
                if (targetNode == null)
                    findNode(node, dataItem);
            }

            return targetNode;
        }

        #endregion //Private Methods
    }
}
