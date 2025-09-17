using System.Windows;
using System.Windows.Media;
using System.Windows.Navigation;
using Infragistics.Controls.Maps;
using Infragistics.Samples.Framework;

namespace IGOrgChart.Samples.Display
{
    public partial class ManuallySelectNodeLayout : SampleContainer
    {
        private OrgChartNodeLayout _methodsLayout;
        private OrgChartNodeLayout _interfacesLayout;
        private OrgChartNodeLayout _propertiesLayout;

        public ManuallySelectNodeLayout()
        {
            InitializeComponent();
            InitializeLayouts();
        }

        private void InitializeLayouts()
        {
            //Initialize the Methods Layout
            var methodStyle = new Style(typeof(OrgChartNodeControl));
            methodStyle.Setters.Add(new Setter(OrgChartNodeControl.BackgroundProperty,
                new SolidColorBrush(Infragistics.ColorConverter.FromString("#FF9FB328"))));
            _methodsLayout = new OrgChartNodeLayout();
            _methodsLayout.Key = "Methods";
            _methodsLayout.DisplayMemberPath = "MethodName";
            _methodsLayout.NodeStyle = methodStyle;

            //Initialize the Interfaces Layout
            var interfaceStyle = new Style(typeof(OrgChartNodeControl));
            interfaceStyle.Setters.Add(new Setter(OrgChartNodeControl.BackgroundProperty,
                new SolidColorBrush(Infragistics.ColorConverter.FromString("#FFF96232"))));
            _interfacesLayout = new OrgChartNodeLayout();
            _interfacesLayout.Key = "Interfaces";
            _interfacesLayout.DisplayMemberPath = "InterfaceName";
            _interfacesLayout.NodeStyle = interfaceStyle;

            //Initialize the Properties Layout
            var propertyStyle = new Style(typeof(OrgChartNodeControl));
            propertyStyle.Setters.Add(new Setter(OrgChartNodeControl.BackgroundProperty,
                new SolidColorBrush(Infragistics.ColorConverter.FromString("#FF2E9CA6"))));
            _propertiesLayout = new OrgChartNodeLayout();
            _propertiesLayout.Key = "Properties";
            _propertiesLayout.DisplayMemberPath = "PropertyName";
            _propertiesLayout.NodeStyle = propertyStyle;
        }

        private void OrgChart_NodeLayoutAssigned(object sender, NodeLayoutAssignedEventArgs e)
        {
            //Manually setting a Node Layout depending on the type of the node.
            switch (e.DataType.Name)
            {
                case "Method":
                    e.NodeLayout = _methodsLayout;
                    break;
                case "Interface":
                    e.NodeLayout = _interfacesLayout;
                    break;
                case "Property":
                    e.NodeLayout = _propertiesLayout;
                    break;
            }
        }
    }
}
