using System.Windows;
using IGGantt.Samples.Models;
using Infragistics.Controls.Schedules;
using Infragistics.Samples.Framework;

namespace IGGantt.Samples.Organization
{
    public partial class CustomResources : SampleContainer
    {
        public CustomResources()
        {
            InitializeComponent();
        }

        private void ButtonOnClick(object sender, RoutedEventArgs rea)
        {
            CustomResource customResource = lbResources.SelectedItem as CustomResource;
            ProjectTaskResource res = new ProjectTaskResource(customResource.Id);

            if (xamGantt.ActiveCell.HasValue)
            {
                for (int i = 0; i < xamGantt.ActiveCell.Value.Row.Task.Resources.Count; i++)
                {
                    if (xamGantt.ActiveCell.Value.Row.Task.Resources[i].ResourceId.Equals(res.ResourceId))
                    {
                        return;
                    }
                }

                xamGantt.ActiveCell.Value.Row.Task.Resources.Add(res);
            }
        }
    }
}
