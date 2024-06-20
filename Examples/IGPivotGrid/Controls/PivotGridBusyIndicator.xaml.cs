using System.Windows;
using System.Windows.Controls;

namespace IGPivotGrid.Samples.Controls
{
    public partial class PivotGridBusyIndicator : UserControl
    {
        public PivotGridBusyIndicator()
        {
            InitializeComponent();
#if WPF
            popup.PlacementTarget = this;
            popup.Placement = System.Windows.Controls.Primitives.PlacementMode.Relative;
            popup.AllowsTransparency = true;
#endif
        }

        private Visibility visibility;
        public new Visibility Visibility
        {
            get { return visibility; }
            set
            {

                if (value != visibility)
                {
                    base.Visibility = value;
                    if (value == Visibility.Visible)
                    {
                        this.popup.IsOpen = true;
                        this.busyImage.Width = this.ActualWidth;
                        this.busyImage.Height = this.ActualHeight;
                    }
                    else this.popup.IsOpen = false;
                    visibility = value;
                }
            }
        }
    }
}
