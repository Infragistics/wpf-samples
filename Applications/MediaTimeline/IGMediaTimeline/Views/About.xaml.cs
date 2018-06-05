using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using IGExtensions.Common.Assets;
using IGExtensions.Common.Assets.Resources;
using IGExtensions.Common.Controls;

namespace MediaTimeline.Views
{
    public partial class About : UserControl
    {
        public About()
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
