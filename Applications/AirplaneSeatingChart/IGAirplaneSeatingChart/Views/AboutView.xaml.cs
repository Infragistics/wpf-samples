using System;
using System.Windows;
using System.Windows.Controls;

namespace IGShowcase.AirplaneSeatingChart.Views
{
    public partial class AboutView : UserControl
    {
        public AboutView()
        {
            InitializeComponent();

        }

        public Size GetDesiredSize()
        {
            this.Measure(new Size(750, double.PositiveInfinity));
            return this.DesiredSize;
        }
       
    }
}
