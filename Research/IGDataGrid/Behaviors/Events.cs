using System;
using System.Windows;
using System.Windows.Controls;
using Infragistics.Windows.DataPresenter;

namespace IGDataGrid.Behaviors
{
    /// <summary>
    /// The event handler used by the RequestrAdorningEditor attached event of XamDataGridBehavior.
    /// </summary>
    public delegate void RequestAdorningEditorRoutedEventHandler(object sender, RequestAdorningEditorRoutedEventArgs e);

    /// <summary>
    /// The event args used by the RequestrAdorningEditor attached event of XamDataGridBehavior.
    /// </summary>
    public class RequestAdorningEditorRoutedEventArgs : RoutedEventArgs
    {
        internal RequestAdorningEditorRoutedEventArgs(Infragistics.Windows.DataPresenter.XamDataGrid dataGrid)
            : base(XamDataGridBehavior.RequestAdorningEditorEvent, dataGrid)
        {
            if (dataGrid == null)
                throw new ArgumentNullException("dataGrid");

            if (dataGrid.ActiveCell == null)
                throw new ArgumentException("dataGrid must have an active cell.");

            this.AdornedCell = dataGrid.ActiveCell;
        }

        /// <summary>
        /// Returns the cell in the XamDataGrid that will be adorned with 
        /// the control assigned to this object's AdorningEditor property.
        /// </summary>
        public Cell AdornedCell { get; private set; }

        /// <summary>
        /// Gets/sets the control shown next to the adorned cell.
        /// If this property is null, no adorning editor will be displayed.
        /// </summary>
        public Control AdorningEditor { get; set; }
    }
}
