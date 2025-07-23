using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Infragistics.Windows;
using Infragistics.Windows.DataPresenter;

namespace IGDataGrid.Controls
{
    /// <summary>
    /// This is the base class for all adorning editor controls
    /// used in the XamDataGrid's 'Adorning Editors' sample. It
    /// has built-in support for keyboard navigation and handling
    /// mouse clicks on TextBox controls.
    /// </summary>
    public class ContactAdornerControlBase : UserControl
    {
        #region Data

        Infragistics.Windows.DataPresenter.XamDataGrid _grid;
        Infragistics.Windows.DataPresenter.Cell _adornedCell;

        #endregion // Data

        #region Constructor

        public ContactAdornerControlBase()
        {
            // Hide the control when the user clicks a Close button.
            this.CommandBindings.Add(new CommandBinding(
                ApplicationCommands.Close,
                (sender, e) => this.IsClosed = true,
                (sender, e) => e.CanExecute = e.Handled = true
                ));

            // When the DataContext changes, that means this control
            // is being shown for a different row than before.  In that
            // case we have to clean out some previous settings associated
            // with the previously adorned cell.
            this.DataContextChanged += delegate
            {
                if (this.IsClosed)
                    this.IsClosed = false;

                _adornedCell = _grid.ActiveCell;
            };
        }

        #endregion // Constructor

        #region Initialize

        public void Initialize(Infragistics.Windows.DataPresenter.XamDataGrid grid)
        {
            if (grid == null)
                throw new ArgumentNullException("grid");

            _grid = grid;

            _grid.CellActivated += (sender, e) =>
            {
                if (this.IsClosed && e.Cell != _adornedCell)
                    this.IsClosed = false;
            };
        }

        #endregion // Initialize

        #region IsClosed

        public bool IsClosed
        {
            get { return base.Visibility != Visibility.Visible; }
            private set
            {
                base.Visibility = value ? Visibility.Collapsed : Visibility.Visible;

                if (value && base.IsKeyboardFocusWithin)
                    _grid.Focus();
            }
        }

        #endregion // IsClosed

        #region ThrowIfUninitialized

        protected void ThrowIfUninitialized()
        {
            if (_grid == null)
                throw new InvalidOperationException("Initialize was never called.");
        }

        #endregion // ThrowIfUninitialized

        #region OnPreviewKeyDown

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            this.ThrowIfUninitialized();

            RoutedCommand cmd = this.GetCommandForKey(e.Key, e.KeyboardDevice);

            if (cmd != null)
            {
                e.Handled = true;

                _grid.ExecuteCommand(cmd);

                // Set focus to the grid because our TextBox might have focus now.
                // This is necessary because none of the grid cells are editable.
                _grid.Focus();
            }

            base.OnPreviewKeyDown(e);
        }

        RoutedCommand GetCommandForKey(Key key, KeyboardDevice device)
        {
            RoutedCommand cmd = null;

            if (key == Key.Tab)
            {
                cmd = device.Modifiers == ModifierKeys.Shift ?
                    // If the Shift key is pressed, give focus back to the active cell.
                    DataPresenterCommands.StartEditMode :
                    // If the user only pressed Tab, move to the next cell.
                    DataPresenterCommands.CellNextByTab;
            }
            else if (!base.IsTabStop)
            {
                // If this control is not a tab stop, it is still possible
                // that the user clicked on a child control to give it focus.
                // In that case, we must manually move focus to the correct cell.
                if (key == Key.Right)
                {
                    cmd = DataPresenterCommands.CellRight;
                }
                else if (key == Key.Left)
                {
                    cmd = DataPresenterCommands.CellLeft;
                }
                else if (key == Key.Up)
                {
                    cmd = DataPresenterCommands.CellAbove;
                }
                else if (key == Key.Down)
                {
                    cmd = DataPresenterCommands.CellBelow;
                }
                else if (key == Key.Escape)
                {
                    // Move input focus back to the active cell.
                    cmd = DataPresenterCommands.StartEditMode;
                }
            }

            return cmd;
        }

        #endregion // OnPreviewKeyDown

        #region OnPreviewMouseDown

        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            // If the user clicks on a TextBox, give it focus and select all the text it contains.
            DependencyObject depObj = e.OriginalSource as DependencyObject;
            TextBox txt = Utilities.GetAncestorFromType(depObj, typeof(TextBox), false, this) as TextBox;
            if (txt != null && !txt.IsFocused)
            {
                txt.Focus();
                txt.SelectAll();
                e.Handled = true;
            }

            base.OnPreviewMouseDown(e);
        }

        #endregion // OnPreviewMouseDown
    }
}
