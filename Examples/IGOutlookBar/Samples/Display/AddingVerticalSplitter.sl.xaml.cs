using Infragistics.Samples.Framework;

namespace IGOutlookBar.Samples.Display
{
    public partial class AddingVerticalSplitter : SampleContainer
    {
        public AddingVerticalSplitter()
        {
            InitializeComponent();
            LayoutRoot.LayoutUpdated += new System.EventHandler(LayoutRoot_LayoutUpdated);
        }

        void LayoutRoot_LayoutUpdated(object sender, System.EventArgs e)
        {
            if (LayoutRoot != null && LayoutRoot.ActualWidth > 24)
            {
                LayoutRoot.ColumnDefinitions[0].MaxWidth = LayoutRoot.ActualWidth - this.splitter.ActualWidth - 24;
            }
        }
    }
}
