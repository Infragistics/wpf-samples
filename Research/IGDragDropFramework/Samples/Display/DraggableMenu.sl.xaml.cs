using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using IGDragDropFramework.CustomControls;
using Infragistics.Controls.Menus;
using Infragistics.DragDrop;
using Infragistics.Samples.Framework;

namespace IGDragDropFramework.Samples.Display
{
    public partial class DraggableMenu : SampleContainer
    {
        public DraggableMenu()
        {
            InitializeComponent();
        }

        private XamMenuItemSeparator _separator = new XamMenuItemSeparator(Colors.Blue);

        private void MenuDragStart(object sender, DragDropStartEventArgs e)
        {
            DependencyObject parent = VisualTreeHelper.GetParent(e.OriginalDragSource);

            while (parent != null)
            {
                if (parent is XamMenuItem)
                {
                    break;
                }

                parent = VisualTreeHelper.GetParent(parent);
            }

            e.DragSnapshotElement = parent as UIElement;
            e.Data = parent;
        }

        private void MenuDragEnter(object sender, DragDropCancelEventArgs e)
        {
            XamMenuItem draggedItem = e.Data as XamMenuItem;

            DependencyObject parent = VisualTreeHelper.GetParent(e.DropTarget);

            while (parent != null)
            {
                if (parent is XamMenuItem)
                {
                    break;
                }

                parent = VisualTreeHelper.GetParent(parent);
            }

            XamMenuItem item = parent as XamMenuItem;
            if (item != null)
            {
                item.IsSubmenuOpen = true;
                draggedItem.ReleaseMouseCapture();
            }
        }

        private void MenuDrop(object sender, DropEventArgs e)
        {
            DependencyObject parent = null;
            XamMenuItem newItem = e.Data as XamMenuItem;

            #region Source Menu Item Parent

            DependencyObject tempInstance = VisualTreeHelper.GetParent(newItem);

            while (tempInstance != null)
            {
                if (tempInstance is XamMenuItem)
                {
                    parent = tempInstance;
                    break;
                }

                parent = tempInstance;
                tempInstance = VisualTreeHelper.GetParent(tempInstance);
            }

            // find parent element of dragged menu item
            XamMenuItem oldParentItem = parent as XamMenuItem;
            if (parent != null)
            {
                if (tempInstance == null && oldParentItem == null)
                {
                    Popup popup = ((FrameworkElement)parent).Parent as Popup;

                    if (popup != null)
                    {
                        parent = VisualTreeHelper.GetParent(popup.Parent);

                        while (parent != null)
                        {
                            if (parent is XamMenuItem)
                            {
                                break;
                            }

                            parent = VisualTreeHelper.GetParent(parent);
                        }

                        oldParentItem = parent as XamMenuItem;
                    }
                }
            }

            #endregion // Source Menu Item Parent

            #region Target Menu Item Parent
            tempInstance = VisualTreeHelper.GetParent(e.DropTarget);
            while (tempInstance != null)
            {
                if (tempInstance is XamMenuItem)
                {
                    parent = tempInstance;
                    break;
                }

                parent = tempInstance;
                tempInstance = VisualTreeHelper.GetParent(tempInstance);
            }

            XamMenuItem newParentItem = parent as XamMenuItem;
            if (parent != null)
            {
                if (tempInstance == null && newParentItem == null)
                {
                    Popup popup = ((FrameworkElement)parent).Parent as Popup;

                    if (popup != null)
                    {
                        parent = VisualTreeHelper.GetParent(popup.Parent);

                        while (parent != null)
                        {
                            if (parent is XamMenuItem)
                            {
                                break;
                            }

                            parent = VisualTreeHelper.GetParent(parent);
                        }

                        newParentItem = parent as XamMenuItem;
                    }
                }
            }

            #endregion // Target Menu Item Parent

            if (oldParentItem != null && newParentItem != null)
            {
                IItemSeparator sep = ItemsSeparator.GetSeparator(newParentItem);
                int dropIndex = 0;

                if (sep != null)
                {
                    dropIndex = sep.DropIndex;
                }

                if (newItem != null && !newItem.Equals(oldParentItem) && !newItem.Equals(newParentItem))
                {
                    if (!oldParentItem.Equals(newParentItem))
                    {
                        if (oldParentItem.Items.Remove(newItem))
                        {
                            newParentItem.Items.Insert(dropIndex, newItem);
                        }
                    }
                    else
                    {
                        // we just reordering
                        int currentIndex = newParentItem.Items.IndexOf(newItem);
                        if (dropIndex != currentIndex)
                        {
                            if (dropIndex > currentIndex)
                            {
                                if (oldParentItem.Items.Remove(newItem))
                                {
                                    newParentItem.Items.Insert(dropIndex - 1, newItem);
                                }
                            }
                            else
                            {
                                if (oldParentItem.Items.Remove(newItem))
                                {
                                    newParentItem.Items.Insert(dropIndex, newItem);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void MenuDragOver(object sender, DragDropMoveEventArgs e)
        {
            DependencyObject parent = null;
            DependencyObject tempInstance = VisualTreeHelper.GetParent(e.DropTarget);
            while (tempInstance != null)
            {
                if (tempInstance is XamMenuItem)
                {
                    parent = tempInstance;
                    break;
                }

                parent = tempInstance;
                tempInstance = VisualTreeHelper.GetParent(tempInstance);
            }

            XamMenuItem newParentItem = parent as XamMenuItem;
            if (parent != null)
            {
                if (tempInstance == null && newParentItem == null)
                {
                    Popup popup = ((FrameworkElement)parent).Parent as Popup;

                    if (popup != null)
                    {
                        parent = VisualTreeHelper.GetParent(popup.Parent);

                        while (parent != null)
                        {
                            if (parent is XamMenuItem)
                            {
                                break;
                            }

                            parent = VisualTreeHelper.GetParent(parent);
                        }

                        newParentItem = parent as XamMenuItem;
                    }
                }
            }

            if (ItemsSeparator.GetSeparator(newParentItem) == null)
            {
                ItemsSeparator.SetSeparator(newParentItem, this._separator);
            }

            if (newParentItem != null)
            {
                ItemsSeparator.ShowSeparator(newParentItem, e.GetPosition(Application.Current.RootVisual));
                DragDropManager.RefreshDragLayout();
            }
        }

        private void MenuDragEnd(object sender, DragDropEventArgs e)
        {
            this._separator.SeparatorPopup.IsOpen = false;
        }

        private void MenuDragLeave(object sender, DragDropEventArgs e)
        {
            this._separator.SeparatorPopup.IsOpen = false;
        }
    }

    public static class DragDropHelper
    {
        public static T GetParent<T>(DependencyObject reference) where T : class
        {
            T result = null;
            DependencyObject parent = VisualTreeHelper.GetParent(reference);

            while (parent != null)
            {
                if (parent is T)
                {
                    result = parent as T;
                    break;
                }

                parent = VisualTreeHelper.GetParent(parent);
            }

            return result;
        }
    }
}