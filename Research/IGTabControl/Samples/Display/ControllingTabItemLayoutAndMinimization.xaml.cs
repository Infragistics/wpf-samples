using System.Windows;
using System.Windows.Controls;
using IGTabControl.Resources;

namespace IGTabControl.Samples.Display
{
    public partial class ControllingTabItemLayoutAndMinimization : Infragistics.Samples.Framework.SampleContainer
    {
        public ControllingTabItemLayoutAndMinimization()
        {
            InitializeComponent();
        }

        void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.AddTabs();

            this.Loaded -= new RoutedEventHandler(Page_Loaded);
        }

        void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            this.AddTabs();
        }

        void AddTabs()
        {
            if (this.numEditorAdd.Value == null) return;

            int count = (int)this.numEditorAdd.Value;

            for (int i = 0; i < count; i++)
            {
                int index = this.tabControl.Items.Count + 1;

                Infragistics.Windows.Controls.TabItemEx newTabItemEx = new Infragistics.Windows.Controls.TabItemEx();

                newTabItemEx.CloseButtonVisibility = Infragistics.Windows.Controls.TabItemCloseButtonVisibility.Visible;
                newTabItemEx.Header = string.Format(TabControlStrings.TabControl_AddRemoveTabs_DisplayArea_Header_Label, index.ToString());
                newTabItemEx.Content = string.Format(TabControlStrings.TabControl_AddRemoveTabs_DisplayArea_Content_Label, index.ToString());

                this.tabControl.Items.Add(newTabItemEx);
            }
        }

        void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            this.tabControl.Items.Clear();
        }

        void cbTabLayoutStyle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.numEditorMaximumTabRows != null)
            {
                switch (this.tabControl.TabLayoutStyle)
                {
                    case Infragistics.Windows.Controls.TabLayoutStyle.MultiRowAutoSize:
                    case Infragistics.Windows.Controls.TabLayoutStyle.MultiRowSizeToFit:
                        this.numEditorMaximumTabRows.IsEnabled = true;
                        this.numEditorMaximumTabRows.ClearValue(TextBlock.ForegroundProperty);
                        break;
                    default:
                        this.numEditorMaximumTabRows.IsEnabled = false;
                        this.numEditorMaximumTabRows.SetValue(TextBlock.ForegroundProperty, SystemColors.GrayTextBrush);
                        break;
                }
            }
        }
    }
}