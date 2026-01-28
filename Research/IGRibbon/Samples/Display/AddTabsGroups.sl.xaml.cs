using System.Windows;
using Infragistics.Controls.Menus;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;

namespace IGRibbon.Samples.Display
{
    public partial class AddTabsGroups : SampleContainer
    {
        public AddTabsGroups()
        {
            InitializeComponent();
        }

        private void AddItems_Click(object sender, RoutedEventArgs e)
        {
            // add tab if specified
            XamRibbonTabItem MyTab = null;
            if (!string.IsNullOrEmpty(this.TabName.Text))
            {
                for (int i = 0; i < MyRibbon.Tabs.Count; i++)
                {
                    string tabHeader = MyRibbon.Tabs[i].Header.ToString();
                    if (tabHeader.Equals(this.TabName.Text))
                    {
                        MyTab = MyRibbon.Tabs[i];
                        break;
                    }
                }
                if (MyTab == null)
                {
                    MyTab = new XamRibbonTabItem();
                    MyTab.Header = this.TabName.Text;
                    MyRibbon.Tabs.Add(MyTab);
                }
            }

            if (MyTab != null)
            {
                // add group if specified
                XamRibbonGroup MyGroup = null;
                if (!string.IsNullOrEmpty(this.GroupName.Text))
                {
                    for (int i = 0; i < MyTab.Groups.Count; i++)
                    {
                        string groupCaption = MyTab.Groups[i].Caption;
                        if (MyTab.Groups[i].Caption.Equals(this.GroupName.Text))
                        {
                            MyGroup = MyTab.Groups[i];
                            break;
                        }
                    }
                    if (MyGroup == null)
                    {
                        MyGroup = new XamRibbonGroup();
                        MyGroup.Caption = this.GroupName.Text;
                        MyTab.Groups.Add(MyGroup);
                    }
                }

                if (MyGroup != null)
                {
                    // add button if specified
                    if (!string.IsNullOrEmpty(this.ButtonName.Text))
                    {
                        ButtonTool MyButton = new ButtonTool();
                        MyButton.SmallImage = ImageFilePathProvider.GetImageLocalUri("Ribbon/NewAdd32.png");
                        MyButton.LargeImage = ImageFilePathProvider.GetImageLocalUri("Ribbon/NewAdd32.png");

                        var selectItem = (RibbonToolSizingMode)RibbonToolSizingModeCombo.Value;
                        MyButton.MaximumSize = selectItem;
                        MyButton.Caption = this.ButtonName.Text;
                        MyGroup.Tools.Add(MyButton);
                    }
                }
            }
        }
    }
}