using System.Collections.Generic;
using System.Windows.Controls;
using Infragistics.Controls.Maps;
using IGNetworkNode.Resources;

namespace IGNetworkNode.Samples.Editing
{
    public partial class NodeSelection : Infragistics.Samples.Framework.SampleContainer
    {
        public NodeSelection()
        {
            InitializeComponent();

            this.xnn.SelectionType = NetworkNodeSelectionType.None;

            var themes = new List<SelectionMode> 
            { 
                new SelectionMode(NetworkNodeSelectionType.None, NetworkNodeStrings.NodeSelection_None),
                new SelectionMode(NetworkNodeSelectionType.Single, NetworkNodeStrings.NodeSelection_Single),
                new SelectionMode(NetworkNodeSelectionType.Multiple, NetworkNodeStrings.NodeSelection_Multiple),
                new SelectionMode(NetworkNodeSelectionType.Extended, NetworkNodeStrings.NodeSelection_Extended),
            };

            this.optionsCombo.ItemsSource = themes;
            this.optionsCombo.DisplayMemberPath = "SelectionTypeName";
            this.optionsCombo.SelectionChanged += OnOptionsComboSelectionChanged;
            this.optionsCombo.SelectedIndex = 2;
        }

        private void OnOptionsComboSelectionChanged(object o, SelectionChangedEventArgs e)
        {
            var s = e.AddedItems[0] as SelectionMode;
            if (s != null)
            {
                this.xnn.SelectionType = s.SelectionType;
            }
        }
    }

    public class SelectionMode
    {
        public NetworkNodeSelectionType SelectionType
        { get; set; }

        public string SelectionTypeName
        { get; set; }

        public SelectionMode(NetworkNodeSelectionType selectionType, string name)
        {
            SelectionType = selectionType;
            SelectionTypeName = name;
        }
    }
}
