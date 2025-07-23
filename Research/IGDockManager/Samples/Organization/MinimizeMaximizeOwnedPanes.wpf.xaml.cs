using System.Windows;
using System.Windows.Controls;
using Infragistics.Samples.Framework;
using Infragistics.Windows.DockManager;

namespace IGDockManager.Samples.Organization
{
    /// <summary>
    /// Interaction logic for MinMaxOwned.xaml
    /// </summary>
    public partial class MinimizeMaximizeOwnedPanes : SampleContainer
    {
        public MinimizeMaximizeOwnedPanes()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(MinMaxOwned_Loaded);
        }

        private void MinMaxOwned_Loaded(object sender, RoutedEventArgs e)
        {
            // use the following code to independently access or modify the minimize and maximize
            // feature for each dock manager pane
            //bool bool1 = ((PaneToolWindow)((SplitPane)this.floatingOnly.Parent).Parent).AllowMinimize;
            //bool bool2 = ((PaneToolWindow)((SplitPane)this.floatingOnly.Parent).Parent).AllowMaximize;

            // use the following code to determine is a dock manager pane is "owned" or not
            //bool bool3 = ((PaneToolWindow)((SplitPane)this.floatingOnly.Parent).Parent).IsOwnedWindow;

            // use the following code to determine or set the state of a dockmanager pane
            //WindowState state = ((PaneToolWindow)((SplitPane)this.floatingOnly.Parent).Parent).WindowState;
        }

        private void DragModeTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0 && e.AddedItems[0] is FloatingWindowDragMode)
            {
                FloatingWindowDragMode dragMode = (FloatingWindowDragMode)e.AddedItems[0];
                this.dockManager.FloatingWindowDragMode = dragMode;
            }
        }
    }
}
