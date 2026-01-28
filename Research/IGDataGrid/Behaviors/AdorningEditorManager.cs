using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using IGDataGrid.Adorners;
using Infragistics.Windows.DataPresenter;
using Infragistics.Windows.DataPresenter.Events;

namespace IGDataGrid.Behaviors
{
    internal class AdorningEditorManager
    {
        #region Fields

        readonly UIElementAdorner<Control> _adorner;
        readonly Infragistics.Windows.DataPresenter.XamDataGrid _xamDataGrid;

        #endregion // Fields

        #region Constructor

        public AdorningEditorManager(Infragistics.Windows.DataPresenter.XamDataGrid dataGrid)
        {
            _xamDataGrid = dataGrid;
            _adorner = new UIElementAdorner<Control>(_xamDataGrid);
            this.AttachToGrid();
        }

        #endregion // Constructor

        #region Event Subscription

        void AttachToGrid()
        {
            _xamDataGrid.CellChanged += this.xamDG_CellChanged;
            _xamDataGrid.CellActivated += this.xamDG_CellActivated;
            _xamDataGrid.EditModeEnded += this.xamDG_EditModeEnded;
            _xamDataGrid.EditModeStarted += this.xamDG_EditModeStarted;
            _xamDataGrid.GroupByArea.Collapsed += this.xamDG_GroupByAreaExpansionStateChanged;
            _xamDataGrid.GroupByArea.Expanded += this.xamDG_GroupByAreaExpansionStateChanged;
            _xamDataGrid.Grouped += this.xamDG_Grouped;
            _xamDataGrid.IsKeyboardFocusWithinChanged += this.xamDG_IsKeyboardFocusWithinChanged;
            _xamDataGrid.PreviewKeyDown += this.xamDG_PreviewKeyDown;
            _xamDataGrid.RecordActivated += this.xamDG_RecordActivated;
            _xamDataGrid.RecordAdded += this.xamDG_RecordAdded;
            _xamDataGrid.RecordCollapsed += this.xamDG_RecordCollapsed;
            _xamDataGrid.RecordExpanded += this.xamDG_RecordExpanded;
            _xamDataGrid.RecordsInViewChanged += this.xamDG_RecordsInViewChanged;
            _xamDataGrid.SizeChanged += this.xamDG_SizeChanged;
            _xamDataGrid.Sorted += this.xamDG_Sorted;
        }

        internal void DetachFromGrid()
        {
            _xamDataGrid.CellChanged -= this.xamDG_CellChanged;
            _xamDataGrid.CellActivated -= this.xamDG_CellActivated;
            _xamDataGrid.EditModeEnded -= this.xamDG_EditModeEnded;
            _xamDataGrid.EditModeStarted -= this.xamDG_EditModeStarted;
            _xamDataGrid.GroupByArea.Collapsed -= this.xamDG_GroupByAreaExpansionStateChanged;
            _xamDataGrid.GroupByArea.Expanded -= this.xamDG_GroupByAreaExpansionStateChanged;
            _xamDataGrid.Grouped -= this.xamDG_Grouped;
            _xamDataGrid.IsKeyboardFocusWithinChanged -= this.xamDG_IsKeyboardFocusWithinChanged;
            _xamDataGrid.PreviewKeyDown -= this.xamDG_PreviewKeyDown;
            _xamDataGrid.RecordActivated -= this.xamDG_RecordActivated;
            _xamDataGrid.RecordAdded -= this.xamDG_RecordAdded;
            _xamDataGrid.RecordCollapsed -= this.xamDG_RecordCollapsed;
            _xamDataGrid.RecordExpanded -= this.xamDG_RecordExpanded;
            _xamDataGrid.RecordsInViewChanged -= this.xamDG_RecordsInViewChanged;
            _xamDataGrid.SizeChanged -= this.xamDG_SizeChanged;
            _xamDataGrid.Sorted -= this.xamDG_Sorted;
        }

        #endregion // Event Subscription

        #region Event Handling Methods

        // This method is invoked when the value of a cell is modified.
        void xamDG_CellChanged(object sender, CellChangedEventArgs e)
        {
            this.VerifyAdorner();
        }

        void xamDG_CellActivated(object sender, CellActivatedEventArgs e)
        {
            this.VerifyAdorner();
        }

        void xamDG_EditModeEnded(object sender, EditModeEndedEventArgs e)
        {
            this.VerifyAdorner();
        }

        void xamDG_EditModeStarted(object sender, EditModeStartedEventArgs e)
        {
            this.VerifyAdorner();
        }

        void xamDG_GroupByAreaExpansionStateChanged(object sender, RoutedEventArgs e)
        {
            this.MoveAdornerWhileGroupByAreaExpansionStateChanges();
        }

        void xamDG_Grouped(object sender, GroupedEventArgs e)
        {
            this.VerifyAdornerAsync();
        }

        void xamDG_IsKeyboardFocusWithinChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            string msg = String.Format("--* IsKeyboardFocusWithinChanged : NewValue={0} FocusedElement={1}: ", e.NewValue, Keyboard.FocusedElement);
            Debug.WriteLine(msg);

            // This is necessary for when the grid loses focus, and then 
            // gets focus back again.  We need to see if the adorning
            // editor should be displayed for the active cell.
            bool shouldAttemptToShowEditor =
                _xamDataGrid.IsKeyboardFocusWithin &&
                _xamDataGrid.ActiveCell != null &&
                Keyboard.FocusedElement == _xamDataGrid;

            if (shouldAttemptToShowEditor)
                this.VerifyAdorner();
        }

        void xamDG_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = this.ProcessKeyStroke(e.Key, e.KeyboardDevice);
        }

        void xamDG_RecordActivated(object sender, RecordActivatedEventArgs e)
        {
            this.VerifyAdorner();
        }

        void xamDG_RecordAdded(object sender, RecordAddedEventArgs e)
        {
            this.VerifyAdornerAsync();
        }

        void xamDG_RecordCollapsed(object sender, RecordCollapsedEventArgs e)
        {
            this.VerifyAdornerAsync();
        }

        void xamDG_RecordExpanded(object sender, RecordExpandedEventArgs e)
        {
            this.VerifyAdornerAsync();
        }

        void xamDG_RecordsInViewChanged(object sender, RecordsInViewChangedEventArgs e)
        {
            this.VerifyAdorner();
        }

        void xamDG_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.RelocateAdorner();
        }

        void xamDG_Sorted(object sender, SortedEventArgs e)
        {
            _xamDataGrid.Dispatcher.BeginInvoke(DispatcherPriority.Background, (Action)delegate
            {
                if (_adorner.Child != null)
                    this.RelocateAdorner();
                else
                    this.VerifyAdorner();
            });
        }

        #endregion // Event Handling Methods

        #region Private Helpers

        #region AdornerIsVisible

        bool AdornerIsVisible
        {
            get { return _adorner.Visibility == Visibility.Visible; }
        }

        #endregion // AdornerIsVisible

        #region GiveFocusToEditor

        void GiveFocusToEditor()
        {
            if (_adorner.Child == null || !_adorner.Child.IsTabStop)
                return;

            // Setting focus to the adorner first ensures that the controls
            // inside the editor can receive focus properly.
            FocusManager.SetFocusedElement(_xamDataGrid, _adorner);
            FocusManager.SetFocusedElement(_xamDataGrid, _adorner.Child);
        }

        #endregion // GiveFocusToEditor

        #region HideAdorner

        void HideAdorner()
        {
            if (this.AdornerIsVisible)
            {
                _adorner.Visibility = Visibility.Collapsed;
                _adorner.Child = null;
            }
        }

        #endregion // HideAdorner

        #region MoveAdornerToActiveCell

        bool MoveAdornerToActiveCell(Control adorningEditor)
        {
            if (_xamDataGrid.ActiveCell == null)
                return false;

            if (adorningEditor != null && _adorner.Child != adorningEditor)
                _adorner.Child = adorningEditor;

            CellValuePresenter cvp = this.GetCellValuePresenterForActiveCell();
            if (cvp != null)
            {
                Point location = this.CalculateAdornerLocation(cvp);
                _adorner.SetOffsets(location.X, location.Y);
                return true;
            }

            return false;
        }

        CellValuePresenter GetCellValuePresenterForActiveCell()
        {
            Cell activeCell = _xamDataGrid.ActiveCell;

            if (activeCell == null)
                return null;

            CellValuePresenter cvp = CellValuePresenter.FromCell(activeCell);

            if (cvp == null || !cvp.IsDescendantOf(_xamDataGrid) || !cvp.IsVisible)
                return null;

            return cvp;
        }

        Point CalculateAdornerLocation(CellValuePresenter cvp)
        {
            GeneralTransform trans = cvp.TransformToAncestor(_xamDataGrid);
            Point location = trans.Transform(new Point(0, 0));

            this.VerifyAdornerMetrics();

            IScrollInfo scrollInfo = _xamDataGrid.ScrollInfo;

            // Make sure the adorner does not extend past the grid's bottom edge.
            bool isScrollingHorizontally = scrollInfo.ViewportWidth < scrollInfo.ExtentWidth;
            double horizontalScrollbarHeight = isScrollingHorizontally ? SystemParameters.ScrollHeight : 0;

            double bottomEdgeOfAdorner = location.Y + cvp.ActualHeight + _adorner.ActualHeight;
            if ((_xamDataGrid.ActualHeight - horizontalScrollbarHeight) < bottomEdgeOfAdorner)
                location.Offset(0, -_adorner.ActualHeight);
            else
                location.Offset(0, cvp.ActualHeight);

            // Make sure the adorner does not extend past the grid's right edge.
            bool isScrollingVertically = scrollInfo.ViewportHeight < scrollInfo.ExtentHeight;
            double verticalScrollbarWidth = isScrollingVertically ? SystemParameters.ScrollWidth : 0;

            double rightEdgeOfAdorner = location.X + _adorner.ActualWidth;
            double horizontalDiff = rightEdgeOfAdorner - (_xamDataGrid.ActualWidth - verticalScrollbarWidth);
            if (0 < horizontalDiff)
                location.Offset(-horizontalDiff, 0);

            return location;
        }

        void VerifyAdornerMetrics()
        {
            this.VerifyAdornerLayer();

            if (!this.AdornerIsVisible)
                _adorner.Visibility = Visibility.Visible;

            // Force the adorner through a measure pass since the hosted editor control
            // might have changed since the last time the metrics were calculated.
            _adorner.InvalidateMeasure();
            _adorner.UpdateLayout();
        }

        #endregion // MoveAdornerToActiveCell

        #region MoveAdornerWhileGroupByAreaExpansionStateChanges

        void MoveAdornerWhileGroupByAreaExpansionStateChanges()
        {
            if (!this.AdornerIsVisible)
                return;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1);
            int counter = 0;
            timer.Tick += delegate
            {
                if (++counter > 500)
                {
                    timer.Stop();
                    timer = null;
                }
                else if (this.AdornerIsVisible)
                {
                    this.MoveAdornerToActiveCell(null);
                }
            };
            timer.Start();
        }

        #endregion // MoveAdornerWhileGroupByAreaExpansionStateChanges

        #region ProcessKeyStroke

        bool ProcessKeyStroke(Key key, KeyboardDevice device)
        {
            if (!this.AdornerIsVisible)
                return false;

            bool giveEditorFocus =
                key == Key.Tab &&
                device.Modifiers != ModifierKeys.Shift &&
                _xamDataGrid.ActiveCell != null &&
                _adorner.Child != null &&
                _adorner.Child.IsTabStop;

            if (!giveEditorFocus)
                return false;

            this.GiveFocusToEditor();

            return true;
        }

        #endregion // ProcessKeyStroke

        #region RelocateAdorner

        void RelocateAdorner()
        {
            if (this.AdornerIsVisible)
            {
                bool resumeEditing = false;

                if (_adorner.IsKeyboardFocusWithin)
                {
                    resumeEditing = true;

                    // We must take the adorner out of focus otherwise it won't move.
                    _xamDataGrid.Focus();
                }

                // Delay the call that moves the adorner so that the
                // grid has a chance to finish updating its elements.
                _xamDataGrid.Dispatcher.BeginInvoke(
                    DispatcherPriority.Background,
                    (Action)delegate
                    {
                        this.MoveAdornerToActiveCell(null);

                        if (resumeEditing)
                            this.GiveFocusToEditor();
                    });
            }
        }

        #endregion // RelocateAdorner

        #region RequestAdorningEditor

        Control RequestAdorningEditor()
        {
            RequestAdorningEditorRoutedEventArgs e = new RequestAdorningEditorRoutedEventArgs(_xamDataGrid);
            _xamDataGrid.RaiseEvent(e);
            return e.AdorningEditor;
        }

        #endregion // RequestAdorningEditor

        #region VerifyAdorner

        void VerifyAdorner()
        {
            this.VerifyAdornerLayer();

            Control adorningEditor = null;
            if (_xamDataGrid.ActiveCell != null)
                adorningEditor = this.RequestAdorningEditor();

            bool movedAdorner = false;
            if (adorningEditor != null)
                movedAdorner = this.MoveAdornerToActiveCell(adorningEditor);

            if (!movedAdorner)
                this.HideAdorner();
        }

        void VerifyAdornerAsync()
        {
            _xamDataGrid.Dispatcher.BeginInvoke(DispatcherPriority.Background, (Action)delegate
            {
                this.VerifyAdorner();
            });
        }

        #endregion // VerifyAdorner

        #region VerifyAdornerLayer

        /// <summary>
        /// Ensures that the adorner is in the adorner layer.
        /// </summary>
        /// <returns>True if the adorner is in the adorner layer, else false.</returns>
        bool VerifyAdornerLayer()
        {
            if (_adorner.Parent != null)
                return true;

            AdornerLayer layer = AdornerLayer.GetAdornerLayer(_xamDataGrid);
            if (layer == null)
                return false;

            layer.Add(_adorner);
            return true;
        }

        #endregion // VerifyAdornerLayer

        #endregion // Private Helpers
    }
}
