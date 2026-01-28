using System.Windows;
using System.Windows.Controls;
using IGOutlookBar.Resources;
using Infragistics.Samples.Framework;
using Infragistics.Windows.OutlookBar;

namespace IGOutlookBar.Samples.Editing
{
    public partial class DynamicallyModifyingGroups : SampleContainer
    {
        public DynamicallyModifyingGroups()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            UIElementCollection children = this.stack1.Children;
            foreach (UIElement child in children)
            {
                RadioButton radBtn = (RadioButton)child;
                if ((bool)radBtn.IsChecked)
                {
                    string radBtnName = radBtn.Name;
                    switch (radBtnName)
                    {
                        case "Add":
                            {
                                OutlookBarGroup gr = new OutlookBarGroup();
                                //gr.Header = root.NewAddedGroup_Header + " " + xamOutlookBar.Groups.Count;
                                gr.Header = OutlookBarStrings.Interaction_AddInsertClearGroupsInXamOutlookBar_NewAddedGroup_Header
                                    + " " + xamOutlookBar.Groups.Count;
                                gr.IsSelected = true;
                                this.xamOutlookBar.Groups.Add(gr);
                                if (this.xamOutlookBar.SelectedGroup == null)
                                    gr.IsSelected = true;
                                break;
                            }
                        case "Insert":
                            {
                                OutlookBarGroup gr = this.xamOutlookBar.SelectedGroup;
                                if (gr == null)
                                {
                                    gr = new OutlookBarGroup();
                                    //gr.Header = root.NewInsertedGroup_Header + " " + xamOutlookBar.Groups.Count;
                                    gr.Header = OutlookBarStrings.Interaction_AddInsertClearGroupsInXamOutlookBar_NewInsertedGroup_Header
                                        + " " + xamOutlookBar.Groups.Count;
                                    gr.IsSelected = true;
                                    this.xamOutlookBar.Groups.Add(gr);
                                    if (this.xamOutlookBar.SelectedGroup == null)
                                        gr.IsSelected = true;
                                }
                                else
                                {
                                    int i = this.xamOutlookBar.Groups.IndexOf(gr);
                                    if (i > this.xamOutlookBar.Groups.Count)
                                        i--;
                                    if (i >= 0)
                                    {
                                        gr = new OutlookBarGroup();
                                        int groupCount = xamOutlookBar.Groups.Count;
                                        //gr.Header = root.NewInsertedGroup_Header + " " + groupCount;
                                        gr.Header = OutlookBarStrings.Interaction_AddInsertClearGroupsInXamOutlookBar_NewInsertedGroup_Header
                                            + " " + groupCount;
                                        this.xamOutlookBar.Groups.Insert(i, gr);
                                    }
                                }
                                break;
                            }
                        case "Clear":
                            {
                                //first remove the selected group
                                OutlookBarGroup gr = this.xamOutlookBar.SelectedGroup;
                                if (gr != null)
                                {
                                    this.xamOutlookBar.Groups.Remove(gr);
                                }
                                this.xamOutlookBar.Groups.Clear();
                                break;
                            }
                    }
                }
            }
        }
    }
}
