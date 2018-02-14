using System.Windows;
using System.Windows.Controls;

namespace IGShowcase.HospitalFloorPlan.Views
{
    public partial class AboutView : UserControl
	{
		#region Constructors
        public AboutView()
		{
			InitializeComponent();
		}
		#endregion Constructors

        public Size GetDesiredSize()
        {
            this.Measure(new Size(750, double.PositiveInfinity));
            return this.DesiredSize;
        }

    }
}
