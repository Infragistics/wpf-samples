using Infragistics.Controls.Schedules;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Tools;

namespace IGSchedule.Samples.Organization
{
    /// <summary>
    /// Interaction logic for ActivitiesCategorization.xaml
    /// </summary>
    public partial class ActivitiesCategorization : SampleContainer
    {
        public ActivitiesCategorization()
        {
            InitializeComponent();

            // Uncomment to see how you can add activity categories in code-behind
            // this.dataConnector.ActivityCategoryItemsSource = new ActivityCategoryCollection() 
            // {
            //     new ActivityCategory() {
            //         CategoryName = "My custom category", 
            //         Description = "Description for my custom category",
            //         Color = Colors.Cyan
            //     }
            // };
        }
    }
}
