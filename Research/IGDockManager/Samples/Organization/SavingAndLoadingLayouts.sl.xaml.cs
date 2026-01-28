using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using IGDockManager.Resources;
using Infragistics.Persistence;
using Infragistics.Samples.Framework;

namespace IGDockManager.Samples.Organization
{
    public partial class SavingAndLoadingLayouts : SampleContainer
    {
        private ObservableCollection<LayoutInfo> _savedLayouts;

        public SavingAndLoadingLayouts()
        {
            InitializeComponent();
        }

        private void Btn_SaveLayout_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // Save xamDockManager layout in MemoryStream
            MemoryStream memoryStream = PersistenceManager.Save(xamDockManager);

            string layoutName = DockManagerStrings.Docking_Layout + (this.SavedLayouts.Count + 1).ToString();

            this.SavedLayouts.Add(new LayoutInfo(layoutName, memoryStream));

            ListBox listBox = this.FindName("ListBox_SavedLayouts") as ListBox;
            listBox.Items.Add(new ListBoxItem { Content = layoutName });
        }

        private void Btn_LoadLayout_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ListBox listBox = this.FindName("ListBox_SavedLayouts") as ListBox;
            if (listBox.Items.Count > 0)
            {
                if (listBox.SelectedIndex != -1)
                {
                    MemoryStream layoutMemoryStream = this.SavedLayouts[listBox.SelectedIndex].LayoutData;
                    layoutMemoryStream.Position = 0;

                    // Restore xamDockManager layout 
                    PersistenceManager.Load(xamDockManager, layoutMemoryStream);
                }
                else
                {
                    MessageBox.Show(DockManagerStrings.Docking_Warning1);
                }
            }
        }

        public ObservableCollection<LayoutInfo> SavedLayouts
        {
            get
            {
                if (this._savedLayouts == null)
                    this._savedLayouts = new ObservableCollection<LayoutInfo>();

                return this._savedLayouts;
            }
        }
    }

    // ==========================================================
    // Class for holding configuration information
    // ==========================================================
    public class LayoutInfo
    {
        private string _layoutName;
        private MemoryStream _layoutData;

        public LayoutInfo(string layoutName, MemoryStream layoutData)
        {
            this._layoutName = layoutName;
            this._layoutData = layoutData;
        }

        public string LayoutName
        {
            get { return this._layoutName; }
        }

        public MemoryStream LayoutData
        {
            get { return this._layoutData; }
        }

        public override string ToString()
        {
            return this.LayoutName;
        }
    }
}
