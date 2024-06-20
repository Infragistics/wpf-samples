using System.Windows;
using Infragistics.Controls.Menus;
using Infragistics.Samples.Framework;

namespace IGOutlookBar.Samples.Editing
{
    public partial class DynamicallyModifyingGroups : SampleContainer
    {
        public DynamicallyModifyingGroups()
        {
            InitializeComponent();
        }

        private void btnExecute_Click(object sender, RoutedEventArgs e)
        {
            if (rdAdd.IsChecked == true)
                AddNewGroup(rdAdd.Content.ToString());
            else if (rdInsert.IsChecked == true)
            {
                if (XamOutlookBar.SelectedGroup != null)
                    InsertNewGroup(rdInsert.Content.ToString());
                else
                    AddNewGroup(rdInsert.Content.ToString());
            }
            else if (rdClear.IsChecked == true)
                XamOutlookBar.Groups.Clear();
        }

        private void InsertNewGroup(string headerBase)
        {
            OutlookBarGroup gr = new OutlookBarGroup();
            gr.Header = headerBase + " " + XamOutlookBar.Groups.Count;
            int selectedIndex = XamOutlookBar.Groups.IndexOf(XamOutlookBar.SelectedGroup);
            XamOutlookBar.Groups.Insert(selectedIndex, gr);
        }

        private void AddNewGroup(string headerBase)
        {
            OutlookBarGroup gr = new OutlookBarGroup();
            gr.Header = headerBase + " " + XamOutlookBar.Groups.Count;
            XamOutlookBar.Groups.Add(gr);
        }
    }
}
