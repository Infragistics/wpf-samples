using System;
using System.Windows;
using System.Windows.Controls;
using IGExtensions.Common.Assets;
using IGExtensions.Common.Assets.Resources;
using Infragistics.Controls.Interactions;

namespace IGShowcase.FinanceDashboard.Views
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
