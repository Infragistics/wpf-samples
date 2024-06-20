using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Infragistics.Controls.Charts;

namespace IGTreemap.Samples
{
    /// <summary>
    /// A <see cref="XamTreemap"/> selection view model, which
    /// pefroms selection in a rectangular manner.
    /// </summary>
    public class TreemapSelectionViewModel : DependencyObject, INotifyPropertyChanged
    {
        /// <summary>
        /// Creates a new <see cref="TreemapSelectionViewModel"/> instance.
        /// </summary>
        public TreemapSelectionViewModel()
        {
            this.SelectedTreemapNodes = new SelectedNodesCollection(this);

            _hitItems = new List<TreemapNode>();

            _selectionGeometry = new RectangleGeometry();
            _selectionGeometry.Rect = Rect.Empty;

            GeometryGroup geometryGroup = new GeometryGroup();
            geometryGroup.Children.Add(_selectionGeometry);

            _selectionPath = new Path();
            _selectionPath.Data = geometryGroup;
            _selectionPath.Fill = new SolidColorBrush(Colors.Black);
            _selectionPath.Opacity = 0.25;

            _selectionRootBounds = Rect.Empty;
        }

        #region Private Members
        /// <summary>
        /// The bounds of the Selection Root.
        /// </summary>
        private Rect _selectionRootBounds;

        /// <summary>
        /// The selection <see cref="Path"/> object,
        /// which is added to the Selection Root's children.
        /// </summary>
        private Path _selectionPath;

        /// <summary>
        /// The selection geomerty rectangle.
        /// </summary>
        private RectangleGeometry _selectionGeometry;

        /// <summary>
        /// The starting point of the selection.
        /// </summary>
        private Point _selectionStart;

        /// <summary>
        /// A collection with <see cref="TreemapNode"/> objects,
        /// which are returned after the HitTest.
        /// </summary>
        private List<TreemapNode> _hitItems;

        #endregion

        #region Properties

        #region SelectionRoot (Dependency Property)

        /// <summary>
        /// The selection root is a <see cref="Grid"/> object,
        /// which contains a <see cref="XamTreemap"/>.
        /// <remarks>
        /// When the property is set, a <see cref="Path"/> object (_selectionPath)
        /// is added to the children of the <see cref="Grid"/> to display
        /// the selection area.
        /// </remarks>
        /// </summary>
        public Grid SelectionRoot
        {
            get { return (Grid)GetValue(SelectionRootProperty); }
            set { SetValue(SelectionRootProperty, value); }
        }

        public static readonly DependencyProperty SelectionRootProperty = DependencyProperty.Register
            ("SelectionRoot", typeof(Grid), typeof(TreemapSelectionViewModel),
             new PropertyMetadata(null, OnSelectionRootChanged));

        private static void OnSelectionRootChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TreemapSelectionViewModel viewModel = (TreemapSelectionViewModel)d;
            viewModel.OnSelectionRootChanged((Grid)e.NewValue, (Grid)e.OldValue);
        }

        /// <summary>
        /// Removes the _selectionPath object from the old Selection Root and
        /// attaches it to the new one.
        /// </summary>
        /// <param name="newValue">The new Selection Root.</param>
        /// <param name="oldValue">The old Selection Root.</param>
        private void OnSelectionRootChanged(Grid newValue, Grid oldValue)
        {
            if (oldValue != null)
            {
                oldValue.Children.Remove(_selectionPath);
                oldValue.MouseLeftButtonDown -= SelectionRoot_MouseLeftButtonDown;
                oldValue.MouseLeftButtonUp -= SelectionRoot_MouseLeftButtonUp;
            }
            if (newValue != null)
            {
                newValue.Children.Add(_selectionPath);
                newValue.MouseLeftButtonDown += SelectionRoot_MouseLeftButtonDown;
                newValue.MouseLeftButtonUp += SelectionRoot_MouseLeftButtonUp;
                _selectionRootBounds = new Rect(new Point(0, 0), this.SelectionRoot.RenderSize);
            }
        }
        #endregion

        #region SelectedNodes

        /// <summary>
        /// A collection containg the selected <see cref="XamTreemap"/> nodes.
        /// </summary>
        protected SelectedNodesCollection SelectedTreemapNodes { get; set; }

        public IEnumerable<string> SelectedNodes { get { return SelectedTreemapNodes.Select(node => node.Text); } }

        #endregion

        #region SelectedStyle (Dependency Property)

        /// <summary>
        /// The <see cref="Style"/>, which is applied to
        /// the selected nodes.
        /// </summary>
        public System.Windows.Style SelectedStyle
        {
            get { return (System.Windows.Style)GetValue(SelectedStyleProperty); }
            set { SetValue(SelectedStyleProperty, value); }
        }

        public static readonly DependencyProperty SelectedStyleProperty = DependencyProperty.Register
            ("SelectedStyle", typeof(System.Windows.Style), typeof(TreemapSelectionViewModel), new PropertyMetadata(null));

        #endregion

        #endregion

        #region Event Handlers
        /// <summary>
        /// Initiates the selection of nodes.
        /// </summary>
        private void SelectionRoot_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.SelectedTreemapNodes.Clear();

            _selectionStart = e.GetPosition(this.SelectionRoot);
            _selectionPath.Visibility = Visibility.Visible;
            this.SelectionRoot.MouseMove += SelectionRoot_MouseMove;
            this.SelectionRoot.CaptureMouse();
        }

        /// <summary>
        /// Draws the selection path
        /// </summary>
        private void SelectionRoot_MouseMove(object sender, MouseEventArgs e)
        {
            Point movingPoint = e.GetPosition(this.SelectionRoot);
            Rect selectionRect = new Rect(_selectionStart, movingPoint);
            selectionRect.Intersect(_selectionRootBounds);
            _selectionGeometry.Rect = selectionRect;
            RaisePropertyChanged("SelectedNodes");
        }

        /// <summary>
        /// Ends the selection.
        /// </summary>
        private void SelectionRoot_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _hitItems.Clear();

            if (_selectionGeometry.Rect != Rect.Empty)
            {

                _hitItems = VisualTreeHelper.FindElementsInHostCoordinates(TranslateRect(_selectionGeometry.Rect), this.SelectionRoot)
                    .Where(element => element is TreemapNode)
                    .Where(node => ((TreemapNode)node).Children.Count == 0)
                    .Cast<TreemapNode>().ToList();
            }

            UpdateSelectedNodes(_hitItems);

            SelectionRoot_MouseMove(sender, e);
            _selectionPath.Visibility = Visibility.Collapsed;
            _selectionGeometry.Rect = Rect.Empty;
            this.SelectionRoot.MouseMove -= SelectionRoot_MouseMove;
            this.SelectionRoot.ReleaseMouseCapture();
        }

        #endregion

        #region Helper Methods
        private Rect TranslateRect(Rect rect)
        {
            return new Rect(rect.X + 20, rect.Y + 20, rect.Width, rect.Height);
        }

        private void UpdateSelectedNodes(List<TreemapNode> _hitItems)
        {
            var itemsToRemove = SelectedTreemapNodes.Except(_hitItems);
            for (int i = 0; i < itemsToRemove.Count(); i++)
            {
                SelectedTreemapNodes.Remove(itemsToRemove.ElementAt(i));
            }

            foreach (var item in _hitItems)
            {
                if (!this.SelectedTreemapNodes.Contains(item))
                {
                    this.SelectedTreemapNodes.Add(item);
                }
            }
        }
        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }

    /// <summary>
    /// A special collection, which contains the selected <see cref="XamTreemap"/> nodes.
    /// </summary>
    public class SelectedNodesCollection : ObservableCollection<TreemapNode>
    {
        /// <summary>
        /// Creates a new <see cref="SelectedNodesCollection"/> instance.
        /// </summary>
        /// <param name="viewModel">A selection view model object.</param>
        public SelectedNodesCollection(TreemapSelectionViewModel viewModel)
        {
            this.ViewModel = viewModel;
        }

        /// <summary>
        /// The associated selection view model.
        /// </summary>
        public TreemapSelectionViewModel ViewModel { get; protected set; }

        /// <summary>
        /// Inserts a new selected node.
        /// </summary>
        /// <param name="index">The index of the node.</param>
        /// <param name="item">The node.</param>
        protected override void InsertItem(int index, TreemapNode item)
        {
            if (!this.Contains(item))
            {
                //Set the style of the node.
                item.Style = this.ViewModel.SelectedStyle;

                base.InsertItem(index, item);
            }
        }

        /// <summary>
        /// Removes a selected item.
        /// </summary>
        /// <param name="index">The index of the item.</param>
        protected override void RemoveItem(int index)
        {
            //Restore the selected item to its previous
            //visual style.
            TreemapNode item = this[index];
            item.ClearValue(FrameworkElement.StyleProperty);

            base.RemoveItem(index);
        }

        /// <summary>
        /// Clear the selected items.
        /// </summary>
        protected override void ClearItems()
        {
            //Restore the visual style of the nodes.
            foreach (TreemapNode node in this.Items)
            {
                node.ClearValue(FrameworkElement.StyleProperty);
            }

            base.ClearItems();
        }
    }
}
