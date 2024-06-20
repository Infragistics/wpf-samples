using System.Windows;
using Infragistics.Controls.Schedules;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Controls;

namespace IGGantt.Samples.Display
{
    public partial class NonWorkingTimeBg : SampleContainer
    {
        public NonWorkingTimeBg()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        private void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            this.Cmb_StyleEnum.ItemsSource = EnumValuesProvider.GetEnumValues<NonWorkingTimeHighlightStyle>();
            this.Cmb_StyleEnum.SelectedIndex = (int)this.gantt.ViewSettings.NonWorkingTimeHighlightStyle;
        }

        private void Cmb_StyleEnum_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (this.gantt.View != null)
            {
                this.gantt.ViewSettings.NonWorkingTimeHighlightStyle = (NonWorkingTimeHighlightStyle)this.Cmb_StyleEnum.SelectedIndex;
            }
        }
    }
}
