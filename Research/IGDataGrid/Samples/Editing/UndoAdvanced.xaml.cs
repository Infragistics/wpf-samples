using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Data;
using System.Windows.Documents;
using IGDataGrid.Resources;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models.Common;
using Infragistics.Samples.Shared.Models.DataPresenter;
using Infragistics.Windows;
using Infragistics.Windows.DataPresenter;
using Infragistics.Windows.DataPresenter.Events;
using Infragistics.Windows.Editors;
using Infragistics.Windows.Internal;

namespace IGDataGrid.Samples.Editing
{
    /// <summary>
    /// Interaction logic for UndoAdvanced.xaml
    /// </summary>
    public partial class UndoAdvanced : SampleContainer
    {
        public UndoAdvanced()
        {
            InitializeComponent();

            ComboBoxItemsProvider itemsProvider = new ComboBoxItemsProvider();


            // note, the sample DataTableUndeleteStrategy used to provide the undelete functionality
            // does not deal with cascading deletes and so the data relation constraints are disabled.
            DataSet ds = NWindDataSource.GetDataSet(false);
            ComboBoxDataItem dataItem1 = new ComboBoxDataItem(ds.Tables["Customers"].DefaultView, DataGridStrings.Combo_DataSource_DataTable);
            this.cboDataSources.Items.Add(dataItem1);

            // the ListUndeleteStrategy is used for this datasource. it will reinsert the original 
            // item back into the collection when the undelete is performed
            ComboBoxDataItem dataItem2 = new ComboBoxDataItem(new EmployeeData().EmployeeDataCollection, DataGridStrings.Combo_DataSource_ObservableCollection);
            this.cboDataSources.Items.Add(dataItem2);

            this.cboDataSources.SelectedValuePath = "Value";
            this.cboDataSources.SelectedIndex = 0;
        }

        private void XamDataGrid1_RecordsDeleting(object sender, RecordsDeletingEventArgs e)
        {
            // in order to support undeletion of records, you must set the UndeleteStrategy
            // to a custom object that provides the functionality for actually recreating or 
            // readding the items to the original data source. since each data source/scenario 
            // may be different you may need to create a new one or customize one of the ones
            // in the sample
            if (this.cboDataSources.SelectedIndex == 0)
                e.UndeleteStrategy = new DataTableUndeleteStrategy(e.Records);
            else if (this.cboDataSources.SelectedIndex == 1)
                e.UndeleteStrategy = new ListUndeleteStrategy(e.Records);
        }
    }

    #region UnboundCellRecordUndeleteStrategy
    /// <summary>
    /// Base UndeleteRecordsStrategy that provides storage of unbound cell values.
    /// </summary>
    public abstract class UnboundCellRecordUndeleteStrategy : UndeleteRecordsStrategy
    {
        #region Member Variables

        private List<Field> _unboundFields;
        private Dictionary<object, IList<object>> _unboundValues;
        private Dictionary<object, object> _newToOldMapping;

        #endregion //Member Variables

        #region Constructor
        /// <summary>
        /// Initializes a new <see cref="RecordsWithUnboundCellUndeleteStrategy"/>
        /// </summary>
        /// <param name="records">The list of records being deleted</param>
        protected UnboundCellRecordUndeleteStrategy(IList<Record> records)
        {
            if (records == null)
                throw new ArgumentNullException("records");

            if (records.Count > 0)
            {
                List<Field> unboundFields = new List<Field>();
                FieldLayout fl = records[0].FieldLayout;

                foreach (Field field in fl.Fields)
                {
                    if (field.BindingType == BindingType.UseNameBinding || field.IsPlaceholderForTreeView)
                        continue;

                    if (field.AlternateBinding != null)
                    {
                        BindingMode? bindingMode = null;

                        if (field.AlternateBinding is Binding)
                            bindingMode = ((Binding)field.AlternateBinding).Mode;
                        else if (field.AlternateBinding is MultiBinding)
                            bindingMode = ((MultiBinding)field.AlternateBinding).Mode;

                        if (bindingMode != null)
                        {
                            switch (bindingMode)
                            {
                                case BindingMode.OneWay:
                                case BindingMode.Default:
                                case BindingMode.OneTime:
                                    continue;
                            }
                        }
                    }

                    unboundFields.Add(field);
                }

                int unboundFieldCount = unboundFields.Count;

                if (unboundFieldCount > 0)
                {
                    _unboundFields = unboundFields;
                    _unboundValues = new Dictionary<object, IList<object>>();

                    foreach (Record record in records)
                    {
                        DataRecord dataRecord = record as DataRecord;

                        if (null == dataRecord)
                            continue;

                        object dataItem = dataRecord.DataItem;

                        if (dataItem == null)
                            continue;

                        object[] unboundValues = new object[unboundFieldCount];

                        for (int i = 0; i < unboundFieldCount; i++)
                            unboundValues[i] = dataRecord.Cells[unboundFields[i]].Value;

                        _unboundValues[dataItem] = unboundValues;
                    }
                }
            }
        }
        #endregion //Constructor

        #region Base class overrides

        #region Undelete
        /// <summary>
        /// Invoked to perform an undeletion of records associated with the specified data items.
        /// </summary>
        /// <param name="oldRecords">A list of objects containing information about the records that were deleted.</param>
        /// <returns>Returns a dictionary that provides a mapping from the old data items to the new data items.</returns>
        public override IDictionary<RecordInfo, object> Undelete(IList<RecordInfo> oldRecords)
        {
            IDictionary<RecordInfo, object> oldToNewMapping = this.UndeleteOverride(oldRecords);

            if (null != oldToNewMapping && oldToNewMapping.Count > 0)
            {
                _newToOldMapping = new Dictionary<object, object>();

                foreach (KeyValuePair<RecordInfo, object> pair in oldToNewMapping)
                    _newToOldMapping[pair.Value] = pair.Key.DataItem;
            }

            return oldToNewMapping;
        }
        #endregion //Undelete

        #region ProcessUndeletedRecords
        /// <summary>
        /// Invoked after the call to <see cref="Undelete(IList{RecordInfo})"/> to allow additional processing of the new records.
        /// </summary>
        /// <param name="recordsCreated">A list of the <see cref="DataRecord"/> instances for the undeleted records based on the specified mapping.</param>
        public override void ProcessUndeletedRecords(IList<DataRecord> recordsCreated)
        {
            if (_newToOldMapping == null || _newToOldMapping.Count == 0)
                return;

            // we're only using this to reset the unbound cell values
            if (_unboundFields == null || _unboundFields.Count == 0)
                return;

            foreach (DataRecord newRecord in recordsCreated)
            {
                object oldDataItem;

                if (!_newToOldMapping.TryGetValue(newRecord.DataItem, out oldDataItem))
                    continue;

                IList<object> unboundValues;

                if (!_unboundValues.TryGetValue(oldDataItem, out unboundValues))
                    continue;

                for (int i = 0; i < unboundValues.Count; i++)
                {
                    Field fld = _unboundFields[i];

                    Debug.Assert(fld.Owner == newRecord.FieldLayout, "Record is associated with a different field layout!");

                    // if somehow the field was removed then skip it
                    if (fld.Owner == newRecord.FieldLayout && fld.Index >= 0)
                        newRecord.Cells[fld].Value = unboundValues[i];
                }
            }
        }
        #endregion //ProcessUndeletedRecords

        #endregion //Base class overrides

        #region Methods

        #region UndeleteOverride

        /// <summary>
        /// Invoked to perform an undeletion of records associated with the specified data items.
        /// </summary>
        /// <param name="oldRecords">A list of objects containing information about the records that were deleted.</param>
        /// <returns>Returns a dictionary that provides a mapping from the old data items to the new data items.</returns>
        protected abstract IDictionary<RecordInfo, object> UndeleteOverride(IList<RecordInfo> oldRecords);

        #endregion //UndeleteOverride

        #endregion //Methods
    }
    #endregion //UnboundCellRecordUndeleteStrategy

    #region DataTableUndeleteStrategy
    /// <summary>
    /// Custom <see cref="UndeleteRecordsStrategy"/> for undeleting records from a DataTable
    /// </summary>
    /// <remarks>
    /// <p class="body">This sample strategy attempts to create new DataRow instances for the deleted DataRows. 
    /// One could possibly implement one using RejectChanges on the deleted rows but then any modifications 
    /// to those rows made before they were deleted and since the last acceptchanges would be lost.
    /// </p>
    /// <p class="note">This sample strategy does not attempt to deal with cascading deletes. If your data source 
    /// is using relations with cascading deletes then you would need to modify this strategy or create a separate 
    /// custom strategy to manage undeleting the child rows that were deleted.</p>
    /// </remarks>
    public class DataTableUndeleteStrategy : UnboundCellRecordUndeleteStrategy
    {
        #region Member Variables

        private Dictionary<DataRow, DataRowInfo> _rowInfo;

        #endregion //Member Variables

        #region Constructor
        /// <summary>
        /// Initializes a new <see cref="DataTableUndeleteStrategy"/>
        /// </summary>
        /// <param name="records">The list of records being deleted</param>
        public DataTableUndeleteStrategy(IList<Record> records)
            : base(records)
        {
            _rowInfo = new Dictionary<DataRow, DataRowInfo>();

            foreach (Record record in records)
            {
                DataRecord dataRecord = record as DataRecord;

                if (record == null)
                    continue;

                DataRow dataRow = DataBindingUtilities.GetObjectForComparison(dataRecord.DataItem) as DataRow;

                if (null == dataRow)
                    throw new InvalidOperationException("The DataTableUndeleteStrategy only works with DataRow objects");

                _rowInfo[dataRow] = new DataRowInfo(dataRow);
            }
        }
        #endregion //Constructor

        #region Base class overrides

        #region CanUndelete
        /// <summary>
        /// Invoked to determine if an undeletion of the specified data items is possible.
        /// </summary>
        /// <param name="oldRecords">A list of objects containing information about the records that were deleted.</param>
        public override bool CanUndelete(IList<RecordInfo> oldRecords)
        {
            return true;
        }
        #endregion //CanUndelete

        #region UndeleteOverride
        /// <summary>
        /// Invoked to perform an undeletion of records associated with the specified data items.
        /// </summary>
        /// <param name="oldRecords">A list of objects containing information about the records that were deleted.</param>
        /// <returns>Returns a dictionary that provides a mapping from the old data items to the new data items.</returns>
        protected override IDictionary<RecordInfo, object> UndeleteOverride(IList<RecordInfo> oldRecords)
        {
            Dictionary<RecordInfo, object> oldToNewMapping = new Dictionary<RecordInfo, object>();

            foreach (RecordInfo ri in oldRecords)
            {
                DataRow dataRow = DataBindingUtilities.GetObjectForComparison(ri.DataItem) as DataRow;
                Debug.Assert(null != dataRow);

                DataRowInfo dataRowInfo;

                if (!_rowInfo.TryGetValue(dataRow, out dataRowInfo))
                    continue;

                RecordManager rm = ri.RecordManager;
                Debug.Assert(null != rm);

                if (null == rm)
                    continue;

                object newDataItem = null;

                if (rm.SourceItems is DataView)
                    newDataItem = dataRowInfo.CreateRow(rm.SourceItems as DataView);
                else if (rm.SourceItems is DataTable)
                    newDataItem = dataRowInfo.CreateRow(rm.SourceItems as DataTable);
                else
                {
                    Debug.Fail("Unexpected source collection");
                }

                if (null != newDataItem)
                {
                    oldToNewMapping[ri] = newDataItem;
                }
            }

            return oldToNewMapping;
        }
        #endregion //UndeleteOverride

        #endregion //Base class overrides

        #region DataRowInfo
        /// <summary>
        /// Helper class for maintaining and recreating a row with a similar state as the original row
        /// </summary>
        private class DataRowInfo
        {
            #region Member Variables

            private int _originalIndex;
            private DataTable _table;
            private DataRowState _state;
            private Dictionary<DataRowVersion, object[]> _cellValues;
            private static DataRowVersion[] RowVersionsInOrder = new DataRowVersion[] { 
                    DataRowVersion.Original, DataRowVersion.Current, DataRowVersion.Proposed
                };

            #endregion //Member Variables

            #region Constructor
            internal DataRowInfo(DataRow row)
            {
                _table = row.Table;
                _originalIndex = _table.Rows.IndexOf(row);
                _state = row.RowState;
                _cellValues = new Dictionary<DataRowVersion, object[]>();

                int columnCount = row.Table.Columns.Count;

                foreach (DataRowVersion version in RowVersionsInOrder)
                {
                    if (!row.HasVersion(version))
                        continue;

                    object[] cellValues = new object[columnCount];

                    for (int i = 0; i < columnCount; i++)
                    {
                        cellValues[i] = row[i, version];
                    }

                    _cellValues[version] = cellValues;
                }
            }
            #endregion //Constructor

            #region Properties

            public int OriginalIndex
            {
                get { return _originalIndex; }
            }

            public DataTable Table
            {
                get { return _table; }
            }

            public DataRowState State
            {
                get { return _state; }
            }

            public Dictionary<DataRowVersion, object[]> CellValues
            {
                get { return _cellValues; }
            }
            #endregion //Properties

            #region Methods

            #region CreateRow
            internal DataRowView CreateRow(DataView dataView)
            {
                if (null == dataView)
                    throw new ArgumentException("dataView");

                DataRowView drv;
                DataRow newRow = CreateRowImpl(dataView.Table, dataView, out drv);

                if (null == drv && null != newRow)
                {
                    for (int i = dataView.Count - 1; i >= 0; i--)
                    {
                        DataRowView temp = dataView[i];

                        if (temp.Row == newRow)
                        {
                            drv = temp;
                            break;
                        }
                    }

                    Debug.Assert(null != drv);
                }

                return drv;
            }

            internal DataRow CreateRow(DataTable table)
            {
                if (null == table)
                    throw new ArgumentException("table");

                DataRowView drv;
                return CreateRowImpl(table, null, out drv);
            }

            private DataRow CreateRowImpl(DataTable table, DataView dataView, out DataRowView drv)
            {
                drv = null;
                DataRow dr = null;

                #region Original
                object[] values;
                if (_cellValues.TryGetValue(DataRowVersion.Original, out values))
                {
                    dr = table.LoadDataRow(values, LoadOption.PreserveChanges);
                }
                #endregion //Original

                #region Current
                if (_state != DataRowState.Unchanged && _cellValues.TryGetValue(DataRowVersion.Current, out values))
                {
                    if (dr == null)
                        dr = table.LoadDataRow(values, LoadOption.Upsert);
                    else
                    {
                        for (int i = 0; i < values.Length; i++)
                            dr[i] = values[i];
                    }
                }
                #endregion //Current

                #region Proposed
                if (_cellValues.TryGetValue(DataRowVersion.Proposed, out values))
                {
                    bool addRow = false;

                    if (dr == null)
                    {
                        if (null != dataView)
                        {
                            drv = dataView.AddNew();
                            dr = drv.Row;
                        }
                        else
                        {
                            addRow = true;
                            dr = table.NewRow();
                        }
                    }
                    else
                    {
                        dr.BeginEdit();
                    }

                    for (int i = 0; i < values.Length; i++)
                        dr[i] = values[i];

                    if (addRow)
                        table.Rows.Add(dr);
                }
                #endregion //Proposed

                return dr;
            }
            #endregion //CreateRow

            #endregion //Methods
        }
        #endregion //DataRowInfo
    }
    #endregion //DataTableUndeleteStrategy

    #region ListUndeleteStrategy
    /// <summary>
    /// Custom <see cref="UndeleteStrategy"/> for IBindingList and INotifyCollectionChanged data sources.
    /// </summary>
    /// <remarks>
    /// <p class="note">This strategy assumes that the objects that were removed from the collection can be 
    /// reinserted into the collection.</p>
    /// </remarks>
    public class ListUndeleteStrategy : UnboundCellRecordUndeleteStrategy
    {
        #region Constructor
        /// <summary>
        /// Initializes a new <see cref="ListUndeleteStrategy"/>
        /// </summary>
        /// <param name="records">The list of records being deleted</param>
        public ListUndeleteStrategy(IList<Record> records)
            : base(records)
        {
            // since this class is set up to reuse the same old items, we can only
            // support a subset of list types. also since we want to ensure the 
            // grid knows when the records are undeleted we're limiting this to 
            // known classes that send collection change notifications as well.
            foreach (Record record in records)
            {
                IEnumerable dataSource = record.RecordManager.SourceItems;

                if (dataSource is DataView || dataSource is DataViewManager)
                    throw new InvalidOperationException("This class cannot be used with datatables/dataviews since it just reinserts the same item back into the collection.");

                if (dataSource is IBindingList)
                    continue;

                if (dataSource is IList && dataSource is INotifyCollectionChanged)
                    continue;

                throw new ArgumentException("The ListUndeleteStrategy is only supported with IBindingList and INotifyCollectionChanged sources");
            }
        }
        #endregion //Constructor

        #region Base class overrides

        #region CanUndelete
        /// <summary>
        /// Invoked to determine if an undeletion of the specified data items is possible.
        /// </summary>
        /// <param name="oldRecords">A list of objects containing information about the records that were deleted.</param>
        public override bool CanUndelete(IList<UndeleteRecordsStrategy.RecordInfo> oldRecords)
        {
            return true;
        }
        #endregion //CanUndelete

        #region UndeleteOverride
        /// <summary>
        /// Invoked to perform an undeletion of records associated with the specified data items.
        /// </summary>
        /// <param name="oldRecords">A list of objects containing information about the records that were deleted.</param>
        /// <returns>Returns a dictionary that provides a mapping from the old data items to the new data items.</returns>
        protected override IDictionary<RecordInfo, object> UndeleteOverride(IList<RecordInfo> oldRecords)
        {
            Dictionary<RecordInfo, object> oldToNewMapping = new Dictionary<RecordInfo, object>();

            RecordInfo[] records = oldRecords.ToArray();
            Comparison<RecordInfo> comparison = delegate(RecordInfo item1, RecordInfo item2)
            {
                return item1.DataItemIndex.CompareTo(item2.DataItemIndex);
            };

            // sort by the original index to ensure we get them in the original order
            Utilities.SortMergeGeneric(records, Utilities.CreateComparer(comparison));

            foreach (RecordInfo record in records)
            {
                RecordManager rm = record.RecordManager;
                IList list = rm.SourceItems as IList;

                if (list == null)
                    continue;

                int newIndex = System.Math.Min(list.Count, record.DataItemIndex);
                object newDataItem = record.DataItem;

                list.Insert(newIndex, newDataItem);

                // since we're reusing the same object the old and new will be the same
                oldToNewMapping[record] = newDataItem;
            }

            return oldToNewMapping;
        }
        #endregion //UndeleteOverride

        #endregion //Base class overrides

    #endregion //ListUndeleteStrategy
    }
}
