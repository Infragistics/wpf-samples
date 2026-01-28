using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Infragistics.Controls.Menus;
using Infragistics.Olap;
using Infragistics.Olap.Data.Base;
using Infragistics.Olap.FlatData;
using Infragistics.Samples.Framework;

namespace IGPivotGrid.Samples.Editing
{
    public partial class DynamicMetadataTree : SampleContainer
    {
        private List<HierarchicalItem> checkedHierarchicalItems;

        public DynamicMetadataTree()
        {
            checkedHierarchicalItems = new List<HierarchicalItem>();

            InitializeComponent();

            this.dimentionDataTree.NodeCheckedChanged += DimentionDataTree_NodeCheckedChanged;
            ((DataSourceBase)this.pivotGrid.DataSource).LoadCubesCompleted += DataSource_LoadCubesCompleted;
        }

        #region EventHandlers

        private void DataSource_LoadCubesCompleted(object sender, LoadCubesCompletedEventArgs e)
        {
            // Initialize the helper data tree
            dimentionDataTree.ItemsSource = ((FlatDataSource)pivotGrid.DataSource).Metadata.ToList<HierarchicalItem>();
            foreach (XamDataTreeNode node in dimentionDataTree.Nodes)
            {
                node.IsChecked = true;
            }

            // Handling the following event lets you specify which items to add to the Metadata tree
            ((DataSourceBase)this.pivotGrid.DataSource).MetadataTreeItemAdding += OnMetadataTreeItemAdding;
        }

        private void OnMetadataTreeItemAdding(object sender, Infragistics.Olap.MetadataTreeEventArgs e)
        {
            var hierarchicalItem = FindItemInCheckedList(e.Item);

            // Items not in the checkedHierarchicalItems list are not added to the Metadata tree
            if (hierarchicalItem == null)
                e.Cancel = true;
            // You can modify whether the items is expanded when initialized
            if (hierarchicalItem != null)
                e.Item.ExpandWhenInitialized = hierarchicalItem.ExpandWhenInitialized;
        }

        private void ResetDataSource_Click(object sender, RoutedEventArgs e)
        {
            // Calling the following method causes MetadataTreeItemAdding
            // to be fired again for each item in the Metadata tree
            ((DataSourceBase)this.pivotGrid.DataSource).ResetMetadataTree();
        }

        private void LoadPreset1_Click(object sender, RoutedEventArgs e)
        {
            LoadSpecifiedDimensions(new string[3] { "[City]", "[UnitPrice]", "[NumberOfUnits]" });
        }

        private void LoadPreset2_Click(object sender, RoutedEventArgs e)
        {
            LoadSpecifiedDimensions(new string[3] { "[Date]", "[Product]", "[Seller]" });
        }

        private void DimentionDataTree_NodeCheckedChanged(object sender, NodeEventArgs e)
        {
            if (!e.Node.IsChecked.HasValue) return;

            if (!(bool)e.Node.IsChecked)
            {
                var hierarchicalItem = (HierarchicalItem)e.Node.Data;
                checkedHierarchicalItems.RemoveAll(
                    item => item.ItemType == hierarchicalItem.ItemType
                        && item.Caption == hierarchicalItem.Caption);
            }
            else
            {
                if (FindItemInCheckedList((HierarchicalItem)e.Node.Data) == null)
                {
                    checkedHierarchicalItems.Add((HierarchicalItem)e.Node.Data);

                    AddParentsToCheckedItems(e.Node);
                }
            }
        }
        #endregion // EventHandlers


        #region Helper methods
        private void LoadSpecifiedDimensions(string[] dimensionNames)
        {
            foreach (var node in dimentionDataTree.Nodes[0].Nodes) // iterate through all dimensions
            {
                var item = (HierarchicalItem)node.Data;
                if (item.ItemType == ItemTypes.Dimension && dimensionNames.Contains(((Dimension)item.DataObject).UniqueName))
                {
                    node.IsChecked = true;
                    continue;
                }

                node.IsChecked = false;
            }
        }

        private void AddParentsToCheckedItems(XamDataTreeNode node)
        {
            XamDataTreeNode parent = node.Manager.ParentNode;
            if (parent != null)
            {
                if (FindItemInCheckedList((HierarchicalItem)parent.Data) == null)
                    checkedHierarchicalItems.Add((HierarchicalItem)parent.Data);

                AddParentsToCheckedItems(parent);
            }
        }

        private HierarchicalItem FindItemInCheckedList(HierarchicalItem hierarchicalItem)
        {
            return checkedHierarchicalItems.FirstOrDefault(
                    item => item.Caption == hierarchicalItem.Caption && item.ItemType == hierarchicalItem.ItemType);
        }

        private void AddChildrenItemsToCheckedItems(HierarchicalItem item)
        {
            if (item.Items.Count > 0)
            {
                foreach (var childItem in item.Items)
                {
                    checkedHierarchicalItems.Add(childItem);
                    AddChildrenItemsToCheckedItems(childItem);
                }
            }
        }
        #endregion
    }
}
