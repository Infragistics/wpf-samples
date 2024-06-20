using System.Windows;
using IGOutlookBar.Resources;
using Infragistics.Samples.Framework;
using Infragistics.Windows.OutlookBar;

namespace IGOutlookBar.Samples.Editing
{
    public partial class UpdatingOrRemovingTheSelectedGroup : SampleContainer
    {
        public UpdatingOrRemovingTheSelectedGroup()
        {
            InitializeComponent();
            this.xamOutlookBar.Groups[0].Key = OutlookBarStrings.Interaction_ChangeOrRemoveSelectedGroup_Group1_Header;
            this.xamOutlookBar.Groups[1].Key = OutlookBarStrings.Interaction_ChangeOrRemoveSelectedGroup_Group3_Header;
            this.xamOutlookBar.Groups[2].Key = OutlookBarStrings.Interaction_ChangeOrRemoveSelectedGroup_Group2_Header;
        }

        private void removeBtn_Click(object sender, RoutedEventArgs e)
        {
            OutlookBarGroup gr = this.xamOutlookBar.SelectedGroup;
            if (gr == null)
                return;

            int i = this.xamOutlookBar.Groups.IndexOf(gr);

            this.xamOutlookBar.Groups.Remove(gr);

            if (i >= this.xamOutlookBar.Groups.Count)
                i--;

            if (i >= 0)
                this.xamOutlookBar.Groups[i].IsSelected = true;
        }

        private bool gotoGroup(string key)
        {
            OutlookBarGroup gr = xamOutlookBar.Groups[key];
            if (gr != null)
            {
                gr.IsSelected = true;
                return true;
            }
            else
                MessageBox.Show(OutlookBarStrings.Interaction_ChangeOrRemoveSelectedGroup_MessageBox_NoGroupWithMatchingKey);
            return false;
        }

        private void selectBtn_Click(object sender, RoutedEventArgs e)
        {
            gotoGroup(textBox1.Text);
        }
    }
}
