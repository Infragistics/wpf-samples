using System.Windows;
using System.Windows.Input;
using Infragistics.Controls.Maps;

namespace IGNetworkNode.Samples.Editing
{
    public partial class NodeRelocation : Infragistics.Samples.Framework.SampleContainer
    {
        private bool _isMoveInEffect; // is the movement in effect?
        private NetworkNodeNodeControl _currentElement; // the element that we are moving
        private Point _currentPosition; // the current position of that element

        public NodeRelocation()
        {
            InitializeComponent();

            xnn.NodeControlAttachedEvent += (sender, e) =>
            {
                e.NodeControl.MouseLeftButtonUp += Element_MouseLeftButtonUp;
                e.NodeControl.MouseLeftButtonDown += Element_MouseLeftButtonDown;
                e.NodeControl.MouseMove += Element_MouseMove;
            };

            xnn.NodeControlDetachedEvent += (sender, e) =>
            {
                e.NodeControl.MouseLeftButtonUp -= Element_MouseLeftButtonUp;
                e.NodeControl.MouseLeftButtonDown -= Element_MouseLeftButtonDown;
                e.NodeControl.MouseMove -= Element_MouseMove;
            };
        }

        private void Element_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var element = (NetworkNodeNodeControl)sender;
            _currentElement = element; // keep track of which node this is
            element.CaptureMouse();
            _isMoveInEffect = true; // initiate the movement effect
            _currentPosition = e.GetPosition(element.Parent as UIElement); // keep track of position
        }

        private void Element_MouseMove(object sender, MouseEventArgs e)
        {
            var element = (NetworkNodeNodeControl)sender;
            if (_currentElement == null || element != _currentElement)
            {
                // this might happen if a node is released outside of the view area.
                // terminate the movement effect.
                _isMoveInEffect = false;
            }
            else if (_isMoveInEffect) // is the movement effect active?
            {
                if (e.GetPosition(xnn).X > xnn.ActualWidth || e.GetPosition(xnn).Y > xnn.ActualHeight || e.GetPosition(xnn).Y < 0.0)
                {
                    // drag is outside of the allowable area, so release the element
                    element.ReleaseMouseCapture();
                    _isMoveInEffect = false;
                }
                else
                {
                    // drag is within the allowable area, so update the element's position
                    var currentPosition = e.GetPosition(element.Parent as UIElement);

                    element.Node.Location = new Point(
                        element.Node.Location.X + (currentPosition.X - this._currentPosition.X) / xnn.ZoomLevel,
                        element.Node.Location.Y + (currentPosition.Y - this._currentPosition.Y) / xnn.ZoomLevel);

                    _currentPosition = currentPosition;
                }
            }
        }

        private void Element_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var element = (NetworkNodeNodeControl)sender;
            element.ReleaseMouseCapture();
            _isMoveInEffect = false; // terminate the movement effect
        }


        private void Redraw_Button_Click(object sender, RoutedEventArgs e)
        {
            // re-draw nodes
            this.xnn.UpdateNodeArrangement();
        }
    }
}
