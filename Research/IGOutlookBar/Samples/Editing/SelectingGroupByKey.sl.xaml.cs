using System;
using System.Windows;
using Infragistics.Controls.Menus;
using Infragistics.Samples.Framework;

namespace IGOutlookBar.Samples.Editing
{
    public partial class SelectingGroupByKey : SampleContainer
    {
        public SelectingGroupByKey()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
            this.LayoutUpdated += new EventHandler(SelectingGroupByKey_LayoutUpdated);
        }

        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            foreach (OutlookBarGroup gr in XamOutlookBar.Groups)
            {
                gr.Key = gr.Header.ToString();
            }
        }

        void SelectingGroupByKey_LayoutUpdated(object sender, EventArgs e)
        {
            if (XamOutlookBar == null)
                return;
            btnRemove.IsEnabled = XamOutlookBar.SelectedGroup != null;
            btnSelect.IsEnabled = !string.IsNullOrEmpty(txtKey.Text);
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                txtMessage.Text = "";
                string key = txtKey.Text;
                XamOutlookBar.SelectedGroup = XamOutlookBar.Groups[key];
            }
            catch (Exception ex)
            {
                txtMessage.Text = ex.Message;
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            int indexCurrentGroup = XamOutlookBar.Groups.IndexOf(XamOutlookBar.SelectedGroup);
            XamOutlookBar.Groups.Remove(XamOutlookBar.SelectedGroup);
            if (XamOutlookBar.Groups.Count == 0)
            {
                XamOutlookBar.SelectedGroup = null;
            }
            else if (indexCurrentGroup >= XamOutlookBar.Groups.Count)
            {
                XamOutlookBar.SelectedGroup = XamOutlookBar.Groups[indexCurrentGroup - 1];
            }
            else
            {
                XamOutlookBar.SelectedGroup = XamOutlookBar.Groups[indexCurrentGroup];
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            OutlookBarGroup newGroup = new OutlookBarGroup();
            newGroup.Header = this.txtKey.Text;
            newGroup.Key = this.txtKey.Text;

            try
            {
                XamOutlookBar.Groups.Add(newGroup);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
