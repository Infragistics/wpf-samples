using System;

namespace Infragistics.Framework
{
    public class DataRowTappedEventArgs : EventArgs
    {
        public DataRowTappedEventArgs(DataCell cell, object item)
        {
            Cell = cell;
            Row = cell.Row;
            Column = cell.Column;
            Item = item;
        }
        /// <summary>
        /// Gets data cell on which a tap gesture was performed
        /// </summary> 
        public DataCell Cell { get; private set; }
        public int Row { get; private set; }
        public int Column { get; private set; }
        /// <summary>
        /// Gets data item associated with cell on which a tap gesture was performed
        /// </summary> 
        public object Item { get; private set; }
    }
}