using System.Collections.Generic;
using Infragistics.Controls.Charts;

namespace IGTreemap.Samples
{
    public partial class DataDrilldown : Infragistics.Samples.Framework.SampleContainer
    {
        private Stack<object> _drilledNodes = new Stack<object>();

        public DataDrilldown()
        {
            InitializeComponent();
        }

        private void Treemap_NodeMouseLeftButtonDown(object sender, TreemapNodeClickEventArgs e)
        {
            //Check if the sender node is actually a different node from the ItemsSourceRoot
            if (Treemap.ItemsSourceRoot != e.Node.DataContext)
            {
                //Push the current ItemsSourceRoot in a stack
                _drilledNodes.Push(Treemap.ItemsSourceRoot);

                //Set the new ItemsSourceRoot
                Treemap.ItemsSourceRoot = e.Node.DataContext;
            }
        }

        private void Treemap_NodeMouseRightButtonDown(object sender, TreemapNodeClickEventArgs e)
        {
            //Check if there are nodes in the stack
            if (_drilledNodes.Count > 0)
            {
                //Push the top node
                Treemap.ItemsSourceRoot = _drilledNodes.Pop();
            }
        }
    }
}
