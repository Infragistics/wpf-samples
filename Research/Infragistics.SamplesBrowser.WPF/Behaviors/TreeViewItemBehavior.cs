using System;
using System.Windows;
using System.Windows.Controls;

namespace Infragistics.SamplesBrowser.Behaviors
{
    /// <summary>
    /// Exposes attached behaviors that can be applied to TreeViewItem objects. 
    /// </summary>
    /// <remarks>
    /// Documentation: http://www.codeproject.com/KB/WPF/AttachedBehaviors.aspx
    /// </remarks>
    public static class TreeViewItemBehavior
    {
        #region IsBroughtIntoViewWhenSelected

        public static bool GetIsBroughtIntoViewWhenSelected(TreeViewItem treeViewItem)
        {
            return (bool)treeViewItem.GetValue(IsBroughtIntoViewWhenSelectedProperty);
        }

        public static void SetIsBroughtIntoViewWhenSelected(TreeViewItem treeViewItem, bool value)
        {
            treeViewItem.SetValue(IsBroughtIntoViewWhenSelectedProperty, value);
        }

        public static readonly DependencyProperty IsBroughtIntoViewWhenSelectedProperty =
            DependencyProperty.RegisterAttached(
            "IsBroughtIntoViewWhenSelected",
            typeof(bool),
            typeof(TreeViewItemBehavior),
            new UIPropertyMetadata(false, OnIsBroughtIntoViewWhenSelectedChanged));

        static void OnIsBroughtIntoViewWhenSelectedChanged(DependencyObject depObj, DependencyPropertyChangedEventArgs e)
        {
            TreeViewItem item = depObj as TreeViewItem;
            if (item == null)
                return;

            if (e.NewValue is bool == false)
                return;

            if ((bool)e.NewValue)
                item.Selected += OnTreeViewItemSelected;
            else
                item.Selected -= OnTreeViewItemSelected;
        }

        static void OnTreeViewItemSelected(object sender, RoutedEventArgs e)
        {
            // Only react to the Selected event raised by the TreeViewItem
            // whose IsSelected property was modified.  Ignore all ancestors
            // who are merely reporting that a descendant's Selected fired.
            if (!Object.ReferenceEquals(sender, e.OriginalSource))
                return;

            TreeViewItem item = e.OriginalSource as TreeViewItem;
            if (item != null)
                item.BringIntoView();
        }

        #endregion // IsBroughtIntoViewWhenSelected

        #region IsBroughtIntoViewWhenClicked

        // This attached behavior exists because the Visual Designers insisted that when you
        // click on an item in the TreeView, it should not be brought into view (i.e. scroll horizontally)
        // but when you navigate the items with the keyboard, they should be brought into view.

        public static bool GetIsBroughtIntoViewWhenClicked(TreeViewItem treeViewItem)
        {
            return (bool)treeViewItem.GetValue(IsBroughtIntoViewWhenClickedProperty);
        }

        public static void SetIsBroughtIntoViewWhenClicked(TreeViewItem treeViewItem, bool value)
        {
            treeViewItem.SetValue(IsBroughtIntoViewWhenClickedProperty, value);
        }

        public static readonly DependencyProperty IsBroughtIntoViewWhenClickedProperty =
            DependencyProperty.RegisterAttached(
            "IsBroughtIntoViewWhenClicked",
            typeof(bool),
            typeof(TreeViewItemBehavior),
            new UIPropertyMetadata(true, OnIsBroughtIntoViewWhenClickedChanged));

        static void OnIsBroughtIntoViewWhenClickedChanged(DependencyObject depObj, DependencyPropertyChangedEventArgs e)
        {
            TreeViewItem item = depObj as TreeViewItem;
            if (item == null)
                return;

            if (e.NewValue is bool == false)
                return;

            if ((bool)e.NewValue)
                item.RequestBringIntoView -= OnTreeViewItemRequestBringIntoView;
            else
                item.RequestBringIntoView += OnTreeViewItemRequestBringIntoView;
        }

        static TreeViewItem _prevItemNotBroughtIntoView;

        static void OnTreeViewItemRequestBringIntoView(object sender, RequestBringIntoViewEventArgs e)
        {
            TreeViewItem tvi = Infragistics.Windows.Utilities.GetAncestorFromType(
                e.OriginalSource as DependencyObject,
                typeof(TreeViewItem),
                false)
                as TreeViewItem;

            if (sender != tvi)
                return;

            // One problem with this logic is that it will prevent an item from being scrolled into
            // view when navigated to via the keyboard, if the mouse cursor happens to be over it.  But
            // considering that the real usefulness of this behavior is to (1) prevent items from moving
            // around when clicked, while (2) allowing items to be scrolled into view from the top or 
            // bottom of the viewport when using keyboard navigation, I'm not concerned about it.  The
            // limitation of this behavior is that the item under the mouse cursor will not horizontally
            // scroll when selected via the arrow keys.
            //
            // Another potential problem with this logic is that it assumes only one TreeView will use
            // it at a time.  If this attached behavior was used by multiple trees at once, the static
            // _prevItemNotBroughtIntoView field would have to be removed and a per-TreeView field would be needed.

            // Check to see if the TreeViewItem that was previously selected is being brought into view again.
            // This can happen if you select an item, give focus to somewhere else, and then click on another 
            // item in the tree.  Before the newly selected item tries to bring itself into view, the previously
            // selected item will also try to bring itself into view.  We must suppress that.
            bool isTryingToBringPreviouslySelectedItemIntoView = _prevItemNotBroughtIntoView == tvi;

            bool doNotBringItemIntoView = isTryingToBringPreviouslySelectedItemIntoView || tvi.IsMouseOver;

            e.Handled = doNotBringItemIntoView;

            if (doNotBringItemIntoView)
                _prevItemNotBroughtIntoView = tvi;
        }

        #endregion // IsBroughtIntoViewWhenClicked
    }
}
