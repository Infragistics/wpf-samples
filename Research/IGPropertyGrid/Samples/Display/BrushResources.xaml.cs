using IGPropertyGrid.Resources;
using Infragistics.Controls.Editors.Primitives;
using Infragistics.Samples.Framework;
using System.Windows.Controls;
using System.Windows.Media;

namespace IGPropertyGrid.Samples.Display
{
    public partial class BrushResources : SampleContainer
    {
        public BrushResources()
        {
            InitializeComponent();
            InitializeSampleData();
        }

        void InitializeSampleData()
        {
            this.xamPropertyGrid1.SelectedObject = new Button();
        }

        private PropertyGridBrushResourceItem pgbri1 = new PropertyGridBrushResourceItem()
        {
            GroupType = Infragistics.Controls.Editors.PropertyGridBrushGroupType.Local,
            Name = PropertyGridStrings.clrOrange,
            Brush = new SolidColorBrush(Colors.Orange)
        };

        private PropertyGridBrushResourceItem pgbri2 = new PropertyGridBrushResourceItem()
        {
            GroupType = Infragistics.Controls.Editors.PropertyGridBrushGroupType.Local,
            Name = PropertyGridStrings.clrMagenta,
            Brush = new SolidColorBrush(Colors.Magenta)
        };

        private PropertyGridBrushResourceItem pgbri3 = new PropertyGridBrushResourceItem()
        {
            GroupType = Infragistics.Controls.Editors.PropertyGridBrushGroupType.Local,
            Name = PropertyGridStrings.clrCyan,
            Brush = new SolidColorBrush(Colors.Cyan)
        };

        private void xamPropertyGrid1_BrushResourcesLoading(object sender, Infragistics.Controls.Editors.BrushResourcesEventArgs e)
        {
            if (e.PropertyItem.PropertyName.Equals("Foreground"))
            {
                e.AutomaticallyIncludeSystemBrushResources = true;
                e.BrushResources.Add(pgbri1);
                e.BrushResources.Add(pgbri2);
                e.BrushResources.Add(pgbri3);
            }
        }

        private void xamPropertyGrid1_BrushResourceSelected(object sender, Infragistics.Controls.Editors.BrushResourceEventArgs e)
        {
            this.clrRectangle.Fill = e.BrushResource.Brush;
        }
    }
}
