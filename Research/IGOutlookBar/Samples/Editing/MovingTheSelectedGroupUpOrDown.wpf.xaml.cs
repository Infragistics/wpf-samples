using System.Windows;
using Infragistics.Samples.Framework;
using Infragistics.Windows.OutlookBar;

namespace IGOutlookBar.Samples.Editing
{
    public partial class MovingTheSelectedGroupUpOrDown : SampleContainer
    {
        public MovingTheSelectedGroupUpOrDown()
        {
            InitializeComponent();
        }

        private void moveUpBtn_Click(object sender, RoutedEventArgs e)
        {
            OutlookBarGroup gr = this.xamOutlookBar.SelectedGroup;
            if (gr == null) return;
            this.xamOutlookBar.GroupMoveUp(gr);
        }

        private void moveDownBtn_Click(object sender, RoutedEventArgs e)
        {
            OutlookBarGroup gr = this.xamOutlookBar.SelectedGroup;
            if (gr == null) return;
            this.xamOutlookBar.GroupMoveDown(gr);
        }
    }
}
