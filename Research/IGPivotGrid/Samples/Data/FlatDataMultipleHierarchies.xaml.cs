using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using IGPivotGrid.Samples.Controls;
using Infragistics.Olap;
using Infragistics.Olap.Data;
using Infragistics.Olap.FlatData;
using Infragistics.Samples.Framework;

namespace IGPivotGrid.Samples.Data
{
    public partial class FlatDataMultipleHierarchies : SampleContainer
    {
        public FlatDataMultipleHierarchies()
        {
            InitializeComponent();
            ((FlatDataSource)this.dataSelector.DataSource).ItemsSource = new SalesCollection();

            this.pivotGrid.DataSource.PropertyChanged += DataSource_PropertyChanged;
            this.pivotGrid.DataSource.ResultChanged += DataSource_ResultChanged;
        }

        void DataSource_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Processing")
            {
                isBusyIndicator.Visibility = pivotGrid.DataSource.IsBusy ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        #region Initial hierarchies expand

        void DataSource_ResultChanged(object sender, AsyncCompletedEventArgs e)
        {
            //Sanity check
            if (this.pivotGrid.DataSource.Result == null)
                return;
            if (this.pivotGrid.DataSource.Result.ColumnAxis == null)
                return;
            if (this.pivotGrid.DataSource.Result.ColumnAxis.Tuples.Count <= 0)
                return;
            if (this.pivotGrid.DataSource.Result.ColumnAxis.Tuples[0].Members.Count <= 0)
                return;
            if (this.pivotGrid.DataSource.Result.RowAxis == null)
                return;
            if (this.pivotGrid.DataSource.Result.RowAxis.Tuples.Count <= 0)
                return;
            if (this.pivotGrid.DataSource.Result.RowAxis.Tuples[0].Members.Count <= 0)
                return;
            if (this.pivotGrid.DataSource.Result.ColumnAxis.Tuples[0].Members[0].UniqueName == String.Empty)
                return;
            if (this.pivotGrid.DataSource.Result.RowAxis.Tuples[0].Members[0].UniqueName == String.Empty)
                return;

            Expand();

            this.pivotGrid.DataSource.ResultChanged -= DataSource_ResultChanged;
        }

        private IFilterMember FindParentMember(IEnumerable<IAreaItemViewModel> queryAxis, IMember requestedMember)
        {
            foreach (IAreaItemViewModel set in queryAxis)
            {
                IFilterViewModel filterViewModel = set as IFilterViewModel;
                if (filterViewModel != null)
                {
                    if (filterViewModel.FilterMembers != null)
                    {
                        foreach (IFilterMember filterMember in filterViewModel.FilterMembers)
                        {
                            if (filterMember.Member.UniqueName == requestedMember.UniqueName)
                            {
                                return filterMember;
                            }

                            IFilterMember lookupMember =
                                this.FindParentMember(filterMember.FilterMembers, requestedMember);
                            if (lookupMember != null)
                            {
                                return lookupMember;
                            }
                        }
                    }
                }
            }

            return null;
        }

        private IFilterMember FindParentMember(IEnumerable<IFilterMember> members, IMember requestedMember)
        {
            if (members != null)
            {
                foreach (IFilterMember filterMember in members)
                {
                    if (filterMember.Member.UniqueName == requestedMember.UniqueName)
                    {
                        return filterMember;
                    }

                    IFilterMember lookupMember =
                        this.FindParentMember(filterMember.FilterMembers, requestedMember);
                    if (lookupMember != null)
                    {
                        return lookupMember;
                    }
                }
            }

            return null;
        }

        private void Expand()
        {
            ReadOnlyCollection<ITuple> tuples = this.pivotGrid.DataSource.Result.ColumnAxis.Tuples;
            ITuple tuple =
                tuples.Where(t => t.Members.Where(m => m.LevelDepth == 0).Count() > 0).FirstOrDefault();
            IMember member =
                tuple.Members.Where(m => m.LevelDepth == 0).FirstOrDefault();

            IFilterMember filterMember = FindParentMember(this.pivotGrid.DataSource.Columns, member);

            if (filterMember != null && !filterMember.IsExpanded)
            {
                this.pivotGrid.DataSource.SwitchAxisMember(member);
            }
            else
            {
                tuples = this.pivotGrid.DataSource.Result.ColumnAxis.Tuples;
                tuple =
                    tuples.Where(t => t.Members.Where(m => m.LevelDepth == 0).Count() > 0).FirstOrDefault();
                member =
                    tuple.Members.Where(m => m.LevelDepth == 0).FirstOrDefault();

                this.pivotGrid.DataSource.SwitchAxisMember(member);
            }

            tuples = this.pivotGrid.DataSource.Result.RowAxis.Tuples;
            tuple =
                tuples.Where(t => t.Members.Where(m => m.LevelDepth == 0).Count() > 0).FirstOrDefault();
            member =
                tuple.Members.Where(m => m.LevelDepth == 0).FirstOrDefault();

            filterMember = FindParentMember(this.pivotGrid.DataSource.Rows, member);

            if (filterMember != null && !filterMember.IsExpanded)
            {
                this.pivotGrid.DataSource.SwitchAxisMember(member);
            }
            else
            {
                tuples = this.pivotGrid.DataSource.Result.RowAxis.Tuples;
                tuple =
                    tuples.Where(t => t.Members.Where(m => m.LevelDepth == 0).Count() > 0).FirstOrDefault();
                member =
                    tuple.Members.Where(m => m.LevelDepth == 0).FirstOrDefault();

                this.pivotGrid.DataSource.SwitchAxisMember(member);
            }
        }

        #endregion Initial hierarchies expand
    }
}
