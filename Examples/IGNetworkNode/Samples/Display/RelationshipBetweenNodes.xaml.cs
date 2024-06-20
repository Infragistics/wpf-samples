using System.Windows;
using System.Windows.Controls;
using IGNetworkNode.Resources;

namespace IGNetworkNode.Samples.Display
{
    public partial class RelationshipBetweenNodes : Infragistics.Samples.Framework.SampleContainer
    {
        #region Fields

        private System.Windows.Style oldEndStyle = null;
        private System.Windows.Style oldStartStyle = null;

        #endregion

        public RelationshipBetweenNodes()
        {
            InitializeComponent();
        this.Loaded += OnSampleLoaded;
        }

        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            // Keep the initial style as the default style
            if (oldEndStyle == null)
            {
                oldEndStyle = xnn.LineEndCapStyle;
                oldStartStyle = xnn.LineStartCapStyle;
            }
        }
        
        private void ComboBoxLineEndCapStyle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem cb = e.AddedItems[0] as ComboBoxItem;
            string newStyle = cb.Content.ToString();

            if (newStyle == NetworkNodeStrings.XNN_LineStartCapStyle)
            {
                xnn.LineStartCapStyle = this.LayoutRoot.Resources["startCap"] as System.Windows.Style;
            }
            else
                if (newStyle == NetworkNodeStrings.XNN_LineEndCapStyle)
                {
                    xnn.LineEndCapStyle = this.LayoutRoot.Resources["endCap"] as System.Windows.Style;
                }
                else
                    if (newStyle == NetworkNodeStrings.XNN_ClearStyle)
                    {
                        xnn.LineEndCapStyle = this.oldEndStyle;
                        xnn.LineStartCapStyle = this.oldStartStyle;
                    }
        }
    }
}
