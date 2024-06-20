using System;
using System.Windows;
using Infragistics.Samples.Framework;

namespace IGOutlookBar.Samples.Editing
{
    public partial class MovingTheSelectedGroup : SampleContainer
    {
        public MovingTheSelectedGroup()
        {
            InitializeComponent();
            LayoutUpdated += new EventHandler(MovingTheSelectedGroup_LayoutUpdated);
        }

        void MovingTheSelectedGroup_LayoutUpdated(object sender, EventArgs e)
        {
            if (XamOutlookBar == null)
                return;
            if (XamOutlookBar.SelectedGroup == null)
                btnMoveUp.IsEnabled = btnMoveDown.IsEnabled = false;
            else
            {
                int selectedIndex = XamOutlookBar.Groups.IndexOf(XamOutlookBar.SelectedGroup);
                int lastGroupIndex =
                    XamOutlookBar.NavigationAreaGroups.Count +
                    XamOutlookBar.OverflowAreaGroups.Count +
                    XamOutlookBar.ContextMenuGroups.Count - 1;

                btnMoveUp.IsEnabled = selectedIndex > 0;
                btnMoveDown.IsEnabled = selectedIndex < lastGroupIndex;
            }
        }

        private void btnMoveUp_Click(object sender, RoutedEventArgs e)
        {
            XamOutlookBar.GroupMoveUp(XamOutlookBar.SelectedGroup);
        }

        private void btnMoveDown_Click(object sender, RoutedEventArgs e)
        {
            XamOutlookBar.GroupMoveDown(XamOutlookBar.SelectedGroup);
        }
    }
}
