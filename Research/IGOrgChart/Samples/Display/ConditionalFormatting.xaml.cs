using System;
using System.Windows.Media;
using Infragistics.Controls.Maps;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models;

namespace IGOrgChart.Samples.Display
{
    public partial class ConditionalFormatting : SampleContainer
    {
        public ConditionalFormatting()
        {
            InitializeComponent();
        }

        private void XamOrgChart_NodeControlAttachedEvent(object sender, OrgChartNodeEventArgs e)
        {
            //You can set a different style to the Node Control
            //or a different Item Template.

            //You can also expand or collapse the node.
            //e.Node.Node.IsExpanded = true / false;

            //Currently the sample applies a background to the
            //Node Control depending on the year in which the 
            //employee started working in the company.

            Employee employee = (Employee)e.Node.Node.Data;

            int employeeSince = Int32.Parse(employee.EmployeeSince);

            if(employeeSince < 2003)
            {
                e.Node.Background =
                    new SolidColorBrush(Infragistics.ColorConverter.FromString("#FFF96232"));
            }
            else if(employeeSince <= 2006)
            {
                e.Node.Background =
                    new SolidColorBrush(Infragistics.ColorConverter.FromString("#FF2E9CA6"));
            }
            else
            {
                e.Node.Background =
                    new SolidColorBrush(Infragistics.ColorConverter.FromString("#FFDC3F76"));
            }
        }
    }
}
