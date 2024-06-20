using System.Windows.Input;
using Infragistics.Samples.Framework;
using Infragistics.Windows.DockManager;
using Infragistics.Windows.DockManager.Dragging;

namespace IGDockManager.Samples.Organization
{
    /// <summary>
    /// Interaction logic for DynamicallyDisableDropLocations.xaml
    /// </summary>
    public partial class DynamicallyDisableDropLocations : SampleContainer
    {
        public DynamicallyDisableDropLocations()
        {
            InitializeComponent();
        }

        private void OnPaneDragOver(object sender, Infragistics.Windows.DockManager.Events.PaneDragOverEventArgs e)
        {
            // For this sample, we will look for PaneDragActions that are AddToGroupActions.  Then we will check to
            // see if the Group associated with the AddToroupAction is a TabGroupPane.  If so, we will set 
            // IsValidDragAction to False and changethe cursor to a NoDrop cursor.
            //
            // The DragAction passed in the event args is of type PaneDragAction - the abstract base class for all
            // drag actions.  We need to upcast the DragAction a the specific type we are interested in.
            //
            // Possible DragAction types are:
            //			AddToGroupAction, NewTabGroupAction, NewSplitPaneAction, NewRootPaneAction, FloatPaneAction
            //			AddToDocumentHostAction, MoveInGroupAction
            PaneDragAction paneDragAction = e.DragAction;
            if (paneDragAction is AddToGroupAction)
            {
                AddToGroupAction addToGroupAction = paneDragAction as AddToGroupAction;
                if (addToGroupAction.Group is TabGroupPane)
                {
                    e.IsValidDragAction = false;
                    e.Cursor = Cursors.No;
                }
            }
        }
    }
}
