using System.Windows;
using System.Windows.Media.Animation;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models.Common;

namespace IGDataCards.Samples.Navigation
{
    /// <summary>
    /// Interaction logic for RepositioningAnimation_Samp.xaml
    /// </summary>
    public partial class RepositioningAnimation : SampleContainer
    {
        // TODO: Add animation to the sample
        private DoubleAnimationBase originalAnimation;

        public RepositioningAnimation()
        {
            InitializeComponent();

            this.xamDataCards1.DataSource = NWindDataSource.GetTable(NWindTable.Customers, true).DefaultView;

            originalAnimation = this.xamDataCards1.ViewSettings.RepositionAnimation;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            this.xamDataCards1.ViewSettings.RepositionAnimation = this.Resources["customRepositionAnimation"] as DoubleAnimationBase;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            this.xamDataCards1.ViewSettings.RepositionAnimation = originalAnimation;
        }
    }
}
