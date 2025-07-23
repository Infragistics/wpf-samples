using System.Collections.Generic;
using System.Windows;
using IGDataTree.Resources;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models;

namespace IGDataTree.Samples.Performance
{
    public partial class Virtualization : SampleContainer
    {
        //the number of levels in the hierarchy
        private int treeDepth = 4;
        //the number of children of each node
        private int levelNodeCount = 20;
        private SimpleNodeDataSource viewModel;

        public Virtualization()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            viewModel = new SimpleNodeDataSource();
            DataContext = viewModel;
            LoadDataSource();
        }


        private void LoadDataSource()
        {
            IList<SimpleNode> nodes = new List<SimpleNode>();
            SetNodes(nodes, 0);
            viewModel.SimpleNodes = nodes;
        }

        /// <summary>
        /// Recursively populate a tree structure
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="level"></param>
        private void SetNodes(IList<SimpleNode> nodes, int level)
        {
            if (level == treeDepth)
                return;
            SimpleNode sn;
            for (int i = 0; i < levelNodeCount; ++i)
            {
                sn = new SimpleNode();
                sn.Text = DataTreeStrings.XDT_Node + (i + 1).ToString() + " : " + DataTreeStrings.XDT_Level + level.ToString();
                sn.Children = new List<SimpleNode>();
                SetNodes(sn.Children, level + 1);
                nodes.Add(sn);
            }
        }
    }

    #region SimpleNode

    public class SimpleNodeDataSource : ObservableModel
    {
        private IEnumerable<SimpleNode> simpleNodes;
        public IEnumerable<SimpleNode> SimpleNodes
        {
            get { return simpleNodes; }
            set
            {
                if (simpleNodes != value)
                {
                    simpleNodes = value;
                    this.OnPropertyChanged("SimpleNodes");
                }
            }
        }
    }

    public class SimpleNode : ObservableModel
    {
        private string text;
        private List<SimpleNode> children;
        private bool isExpanded = true;

        public bool IsExpanded
        {
            get
            {
                return isExpanded;
            }
            set
            {
                isExpanded = value;
            }
        }
        public List<SimpleNode> Children
        {
            get { return children; }
            set
            {
                if (children != value)
                {
                    children = value;
                    this.OnPropertyChanged("Children");
                }
            }
        }
        public string Text
        {
            get { return text; }
            set
            {
                if (text != value)
                {
                    text = value;
                    this.OnPropertyChanged("Text");
                }
            }
        }
    }
    #endregion // SimpleNode
}