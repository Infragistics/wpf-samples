using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Infragistics.Controls.Maps;
using Infragistics.DragDrop;

namespace IGNetworkNode.Custom
{
    /// <summary>
    /// An enumeration with the possible drop actions.
    /// </summary>
    public enum DropAction
    {
        /// <summary>
        /// This drop action means that the dragged node and the drop target node
        /// are not connected, so they will have to be connected.
        /// </summary>
        Connect,

        /// <summary>
        /// This drop action means that the dragged node and the drop target node
        /// are connected, so the will have to be disconnected.
        /// </summary>
        Disconnect
    }

    /// <summary>
    /// An abstract class, which adds drag and drop functionality to the nodes
    /// of <see cref="XamNetworkNode"/>.
    /// </summary>
    public abstract class NetworkNodeConnectionViewModel : DependencyObject
    {
        /// <summary>
        /// Creates a new instance of the <see cref="NetworkNodeConnectionViewModel"/> class.
        /// </summary>
        public NetworkNodeConnectionViewModel()
        {
            _dragPath = new Line();
            _dragPath.StrokeStartLineCap = PenLineCap.Round;
            _dragPath.StrokeEndLineCap = PenLineCap.Round;
            _dragPath.Visibility = Visibility.Collapsed;
            _dragPath.StrokeThickness = 2;
            BindingOperations.SetBinding(_dragPath, Shape.StrokeProperty, CreateBinding("CurrentStroke"));

            _dragCanvas = new Canvas();
            _dragCanvas.IsHitTestVisible = false;
            _dragCanvas.Children.Add(_dragPath);
        }

        #region Properties
        #region Strokes
        #region DragStroke (Dependency Property)
        /// <summary>
        /// Gets or sets the default stroke of the line, which indicates the drag path.
        /// </summary>
        public Brush DragStroke
        {
            get { return (Brush)GetValue(DragStrokeProperty); }
            set { SetValue(DragStrokeProperty, value); }
        }

        public static readonly DependencyProperty DragStrokeProperty = DependencyProperty.Register
            ("DragStroke", typeof(Brush), typeof(NetworkNodeConnectionViewModel), new PropertyMetadata(null));
        #endregion

        #region ConnectStroke (Dependency Property)
        /// <summary>
        /// Gets or sets the stroke of the drag path line when the nodes are going to be connected.
        /// </summary>
        public Brush ConnectStroke
        {
            get { return (Brush)GetValue(ConnectStrokeProperty); }
            set { SetValue(ConnectStrokeProperty, value); }
        }

        public static readonly DependencyProperty ConnectStrokeProperty = DependencyProperty.Register
            ("ConnectStroke", typeof(Brush), typeof(NetworkNodeConnectionViewModel), new PropertyMetadata(null));
        #endregion

        #region DisconnectStroke (Dependency Property)
        /// <summary>
        /// Gets or sets the stroke of the drag path line when the nodes are going to be disconnected.
        /// </summary>
        public Brush DisconnectStroke
        {
            get { return (Brush)GetValue(DisconnectStrokeProperty); }
            set { SetValue(DisconnectStrokeProperty, value); }
        }

        public static readonly DependencyProperty DisconnectStrokeProperty = DependencyProperty.Register
            ("DisconnectStroke", typeof(Brush), typeof(NetworkNodeConnectionViewModel), new PropertyMetadata(null));
        #endregion

        #region CurrentStroke (Dependency Property)
        /// <summary>
        /// Gets the current used stroke of the drag path.
        /// </summary>
        public Brush CurrentStroke
        {
            get { return (Brush)GetValue(CurrentStrokeProperty); }
            protected set { SetValue(CurrentStrokeProperty, value); }
        }

        public static readonly DependencyProperty CurrentStrokeProperty = DependencyProperty.Register
            ("CurrentStroke", typeof(Brush), typeof(NetworkNodeConnectionViewModel), new PropertyMetadata(null));
        #endregion
        #endregion

        #region Drag Templates
        #region DragTemplate (Dependency Property)
        /// <summary>
        /// Gets or sets the default drag template of the dragged node.
        /// </summary>
        public DataTemplate DragTemplate
        {
            get { return (DataTemplate)GetValue(DragTemplateProperty); }
            set { SetValue(DragTemplateProperty, value); }
        }

        public static readonly DependencyProperty DragTemplateProperty = DependencyProperty.Register
            ("DragTemplate", typeof(DataTemplate), typeof(NetworkNodeConnectionViewModel), new PropertyMetadata(null));
        #endregion

        #region ConnectDragTemplate (Dependency Property)
        /// <summary>
        /// Gets or sets the drag template of the dragged node when the two nodes are going to be connected.
        /// </summary>
        public DataTemplate ConnectDragTemplate
        {
            get { return (DataTemplate)GetValue(ConnectDragTemplateProperty); }
            set { SetValue(ConnectDragTemplateProperty, value); }
        }

        public static readonly DependencyProperty ConnectDragTemplateProperty = DependencyProperty.Register
            ("ConnectDragTemplate", typeof(DataTemplate), typeof(NetworkNodeConnectionViewModel), new PropertyMetadata(null));
        #endregion

        #region DisconnectDragTemplate (Dependency Property)
        /// <summary>
        /// Gets or sets the drag template of the dragged node when the two nodes are going to be disconnected.
        /// </summary>
        public DataTemplate DisconnectDragTemplate
        {
            get { return (DataTemplate)GetValue(DisconnectDragTemplateProperty); }
            set { SetValue(DisconnectDragTemplateProperty, value); }
        }

        public static readonly DependencyProperty DisconnectDragTemplateProperty = DependencyProperty.Register
            ("DisconnectDragTemplate", typeof(DataTemplate), typeof(NetworkNodeConnectionViewModel), new PropertyMetadata(null));
        #endregion

        #region CurrentDragTemplate (Dependency Property)
        /// <summary>
        /// Gets the current used drag template for the dragged node.
        /// </summary>
        public DataTemplate CurrentDragTemplate
        {
            get { return (DataTemplate)GetValue(CurrentDragTemplateProperty); }
            protected set { SetValue(CurrentDragTemplateProperty, value); }
        }

        public static readonly DependencyProperty CurrentDragTemplateProperty = DependencyProperty.Register
            ("CurrentDragTemplate", typeof(DataTemplate), typeof(NetworkNodeConnectionViewModel), new PropertyMetadata(null));
        #endregion
        #endregion

        #region Drop Target Styles
        #region DropTargetStyle (Dependency Property)
        /// <summary>
        /// Gets or sets the drafault drop target style.
        /// </summary>
        public System.Windows.Style DropTargetStyle
        {
            get { return (System.Windows.Style)GetValue(DropTargetStyleProperty); }
            set { SetValue(DropTargetStyleProperty, value); }
        }

        public static readonly DependencyProperty DropTargetStyleProperty = DependencyProperty.Register
            ("DropTargetStyle", typeof(System.Windows.Style), typeof(NetworkNodeConnectionViewModel), new PropertyMetadata(null));
        #endregion

        #region ConnectDropTargetStyle (Dependency Property)
        /// <summary>
        /// Gets or sets the style, which is going to be applied to the drop target
        /// when a connect action is going to be performed.
        /// </summary>
        public System.Windows.Style ConnectDropTargetStyle
        {
            get { return (System.Windows.Style)GetValue(ConnectDropTargetStyleProperty); }
            set { SetValue(ConnectDropTargetStyleProperty, value); }
        }

        public static readonly DependencyProperty ConnectDropTargetStyleProperty = DependencyProperty.Register
            ("ConnectDropTargetStyle", typeof(System.Windows.Style), typeof(NetworkNodeConnectionViewModel), new PropertyMetadata(null));
        #endregion

        #region DisconnectDropTargetStyle (Dependency Property)
        /// <summary>
        /// Gets or sets the style, which is going to be applied ot the drop target
        /// when a disconnect action is going to be performed.
        /// </summary>
        public System.Windows.Style DisconnectDropTargetStyle
        {
            get { return (System.Windows.Style)GetValue(DisconnectDropTargetStyleProperty); }
            set { SetValue(DisconnectDropTargetStyleProperty, value); }
        }

        public static readonly DependencyProperty DisconnectDropTargetStyleProperty = DependencyProperty.Register
            ("DisconnectDropTargetStyle", typeof(System.Windows.Style), typeof(NetworkNodeConnectionViewModel), new PropertyMetadata(null));
        #endregion

        #region CurrentDropTargetStyle (Dependency Property)
        /// <summary>
        /// Gets the current used drop target style.
        /// </summary>
        public System.Windows.Style CurrentDropTargetStyle
        {
            get { return (System.Windows.Style)GetValue(CurrentDropTargetStyleProperty); }
            protected set { SetValue(CurrentDropTargetStyleProperty, value); }
        }

        public static readonly DependencyProperty CurrentDropTargetStyleProperty = DependencyProperty.Register
            ("CurrentDropTargetStyle", typeof(System.Windows.Style), typeof(NetworkNodeConnectionViewModel), new PropertyMetadata(null));
        #endregion
        #endregion

        #region NetworkNode (Dependency Property)
        /// <summary>
        /// Gets or sets the <see cref="XamNetworkNode"/> instance, which is going to be used.
        /// </summary>
        public XamNetworkNode NetworkNode
        {
            get { return (XamNetworkNode)GetValue(NetworkNodeProperty); }
            set { SetValue(NetworkNodeProperty, value); }
        }

        public static readonly DependencyProperty NetworkNodeProperty = DependencyProperty.Register
            ("NetworkNode", typeof(XamNetworkNode), typeof(NetworkNodeConnectionViewModel), new PropertyMetadata(null, OnNetworkNodeChanged));

        private static void OnNetworkNodeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            NetworkNodeConnectionViewModel viewModel = (NetworkNodeConnectionViewModel)d;
            viewModel.OnNetworkNodeChanged((XamNetworkNode)e.OldValue, (XamNetworkNode)e.NewValue);
        }

        private void OnNetworkNodeChanged(XamNetworkNode oldValue, XamNetworkNode newValue)
        {
            if (oldValue != null)
            {
                //Remove the Drag Canvas.
                oldValue.Content = null;

                newValue.PropertyChanged -= NetworkNode_PropertyChanged;
                oldValue.NodeControlAttachedEvent -= NetworkNode_NodeControlAttachedEvent;
                newValue.NodeControlDetachedEvent -= NetworkNode_NodeControlDetachedEvent;

                foreach (NetworkNodeNode node in oldValue.Nodes)
                {
                    if (node.Control != null)
                    {
                        DragDropManager.SetDragSource(node.Control, null);
                        DragDropManager.SetDropTarget(node.Control, null);
                    }
                }
            }

            if (newValue != null)
            {
                //Add the Drag Canvas.
                newValue.Content = _dragCanvas;

                newValue.PropertyChanged += NetworkNode_PropertyChanged;
                newValue.NodeControlAttachedEvent += NetworkNode_NodeControlAttachedEvent;
                newValue.NodeControlDetachedEvent += NetworkNode_NodeControlDetachedEvent;

                foreach (NetworkNodeNode node in newValue.Nodes)
                {
                    if (node.Control != null)
                    {
                        SetDragSourceAndDropTarget(node.Control);
                    }
                }
            }
        }
        #endregion
        #endregion

        #region Network Node Events
        /// <summary>
        /// Stores the currently dragged <see cref="NetworkNodeNodeControl"/> instance.
        /// </summary>
        private NetworkNodeNodeControl _draggedNode;

        /// <summary>
        /// Attaches a <see cref="DragSource"/> and a <see cref="DropTarget"/> object to the new node control.
        /// </summary>
        private void NetworkNode_NodeControlAttachedEvent(object sender, NetworkNodeEventArgs e)
        {
            SetDragSourceAndDropTarget(e.NodeControl);
        }

        /// <summary>
        /// Attaches a <see cref="DragSource"/> and a <see cref="DropTarget"/> object to the a node control.
        /// </summary>
        private void SetDragSourceAndDropTarget(NetworkNodeNodeControl nodeControl)
        {
            #region Create the Drag Source
            //Create a new Drag Source object.
            DragSource dragSource = new DragSource();

            //Initialize the properties of the Drag Source.
            dragSource.IsDraggable = true;
            dragSource.MoveCursorTemplate = new DataTemplate();
            dragSource.DropNotAllowedCursorTemplate = new DataTemplate();

            //Attach event handlers to the Drag Source.
            dragSource.DragStart += Node_DragStart;
            dragSource.DragEnter += Node_DragEnter;
            dragSource.DragLeave += Node_DragLeave;
            dragSource.Drop += Node_Drop;
            dragSource.DragEnd += Node_DragEnd;

            //Make the node a Drag Source.
            DragDropManager.SetDragSource(nodeControl, dragSource);
            #endregion

            #region Create the Drop Target
            //Create new Drop Target object.
            DropTarget dropTarget = new DropTarget();

            //Initialize the properties of the Drop Target.
            dropTarget.IsDropTarget = true;
            BindingOperations.SetBinding(dropTarget, DropTarget.DropTargetStyleProperty, CreateBinding("CurrentDropTargetStyle"));

            //Make the node a Drop Target.
            DragDropManager.SetDropTarget(nodeControl, dropTarget);
            #endregion
        }

        /// <summary>
        /// Calculates the end point of the dragged node - this will update the
        /// drag path when the mouse moves.
        /// </summary>
        private void NetworkNode_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePosition = e.GetPosition(_dragCanvas);

            _dragPath.X2 = mousePosition.X;
            _dragPath.Y2 = mousePosition.Y;
        }

        private void NetworkNode_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ZoomLevel")
            {
                if (DragDropManager.IsDragging)
                {
                    GeneralTransform generalTransform = _draggedNode.TransformToVisual(_dragCanvas);
                    Point dragStart = generalTransform.Transform(new Point(0, 0));

                    _dragPath.X1 = dragStart.X + (_draggedNode.ActualWidth / 2) * this.NetworkNode.ZoomLevel;
                    _dragPath.Y1 = dragStart.Y + (_draggedNode.ActualHeight / 2) * this.NetworkNode.ZoomLevel;
                }
            }
        }

        /// <summary>
        /// When a <see cref="NetworkNodeNodeControl"/> is detached,
        /// this method checks if the detached node and the dragged node are the same.
        /// If they are the same, the drag process is canceled.
        /// </summary>
        private void NetworkNode_NodeControlDetachedEvent(object sender, NetworkNodeEventArgs e)
        {
            if (e.NodeControl == _draggedNode && DragDropManager.IsDragging)
            {
                DragDropManager.EndDrag(true);
            }
        }
        #endregion

        #region Drag & Drop Events
        /// <summary>
        /// Initializes the objects that are used for the dragging of the node.
        /// </summary>
        private void Node_DragStart(object sender, DragDropStartEventArgs e)
        {
            _draggedNode = (NetworkNodeNodeControl)e.DragSource;

            this.CurrentStroke = this.DragStroke;
            e.DragTemplate = this.DragTemplate;
            this.CurrentDropTargetStyle = this.DropTargetStyle;

            GeneralTransform generalTransform = _draggedNode.TransformToVisual(_dragCanvas);
            Point dragStart = generalTransform.Transform(new Point(0, 0));

            _dragPath.X1 = dragStart.X + (_draggedNode.ActualWidth / 2) * this.NetworkNode.ZoomLevel;
            _dragPath.Y1 = dragStart.Y + (_draggedNode.ActualHeight / 2) * this.NetworkNode.ZoomLevel;

            _dragPath.X2 = _dragPath.X1;
            _dragPath.Y2 = _dragPath.Y1;

            //Show the Drag Path.
            _dragPath.Visibility = Visibility.Visible;

            this.NetworkNode.MouseMove += NetworkNode_MouseMove;
        }

        /// <summary>
        /// When this even occurs (when a dragged node is hovered over another node),
        /// this method determines whether the drag source node
        /// and the drop target node are connected:
        ///     - If they are not connected, they will be connected.
        ///     - If they are connected, they will be disconnected.
        /// </summary>
        private void Node_DragEnter(object sender, DragDropCancelEventArgs e)
        {
            if (e.DragSource != e.DropTarget)
            {
                NetworkNodeNode dragSource = GetNetworkNode(e.DragSource);
                NetworkNodeNode dropTarget = GetNetworkNode(e.DropTarget);

                if (dropTarget.ConnectedNodes.Contains(dragSource))
                {
                    this.CurrentStroke = this.DisconnectStroke;
                    e.DragTemplate = this.DisconnectDragTemplate;
                    this.CurrentDropTargetStyle = this.DisconnectDropTargetStyle;
                }
                else
                {
                    this.CurrentStroke = this.ConnectStroke;
                    e.DragTemplate = this.ConnectDragTemplate;
                    this.CurrentDropTargetStyle = this.ConnectDropTargetStyle;
                }
            }
        }

        /// <summary>
        /// When the dragged node leaves a hovered drop target node,
        /// this method restores the current stroke, the drag template
        /// and the drop target style to their default values.
        /// </summary>
        private void Node_DragLeave(object sender, DragDropEventArgs e)
        {
            this.CurrentStroke = this.DragStroke;
            e.DragTemplate = this.DragTemplate;
            this.CurrentDropTargetStyle = this.DropTargetStyle;
        }

        /// <summary>
        /// Occurs when a node is dropped on another node.
        /// </summary>
        private void Node_Drop(object sender, DropEventArgs e)
        {
            NetworkNodeNode dragSource = GetNetworkNode(e.DragSource);
            NetworkNodeNode dropTarget = GetNetworkNode(e.DropTarget);

            if (dragSource == dropTarget) return;

            DropAction dropAction;
            if (dropTarget.ConnectedNodes.Contains(dragSource))
            {
                dropAction = DropAction.Disconnect;
            }
            else
            {
                dropAction = DropAction.Connect;
            }

            OnNodeDrop(dragSource, dropTarget, dropAction);
        }

        /// <summary>
        /// Handles the drop of the node on the other node.
        /// </summary>
        /// <param name="dragSource">The dragged node.</param>
        /// <param name="dropTarget">The drop target node.</param>
        /// <param name="dropAction">The action that needs to be performed - connect or disconnect.</param>
        /// <remarks>
        /// This method needs to be defined in a derived class depending on the
        /// type of the nodes.
        /// </remarks>
        protected abstract void OnNodeDrop(NetworkNodeNode dragSource, NetworkNodeNode dropTarget, DropAction dropAction);

        /// <summary>
        /// Occurs when the drag is over - this method returns all private objects
        /// to their default values.
        /// </summary>
        private void Node_DragEnd(object sender, DragDropEventArgs e)
        {
            //Hide the Drag Path.
            _dragPath.Visibility = Visibility.Collapsed;

            this.CurrentStroke = this.DragStroke;
            e.DragTemplate = this.DragTemplate;
            this.CurrentDropTargetStyle = this.DropTargetStyle;

            _dragPath.X1 = 0;
            _dragPath.Y1 = 0;

            _dragPath.X2 = 0;
            _dragPath.Y2 = 0;

            this.NetworkNode.MouseMove -= NetworkNode_MouseMove;
        }
        #endregion

        #region Members
        /// <summary>
        /// The canvas, which is added as a content of the Network Node.
        /// This canvas will be used to house the drag path line.
        /// </summary>
        private Canvas _dragCanvas;

        /// <summary>
        /// The line, which represents the drag path.
        /// </summary>
        private Line _dragPath;
        #endregion

        #region Utils
        /// <summary>
        /// Creates a new <see cref="Binding"/> object.
        /// </summary>
        /// <param name="path">The property path.</param>
        /// <returns>A new binding object.</returns>
        protected Binding CreateBinding(string path)
        {
            return new Binding(path) { Source = this };
        }

        /// <summary>
        /// Converts a <see cref="UIElement"/> to a <see cref="NetworkNodeNode"/>.
        /// </summary>
        /// <param name="element">The UIElement.</param>
        /// <returns>A NetworkNodeNode object.</returns>
        protected NetworkNodeNode GetNetworkNode(UIElement element)
        {
            return ((NetworkNodeNodeControl)element).Node;
        }
        #endregion
    }
}
