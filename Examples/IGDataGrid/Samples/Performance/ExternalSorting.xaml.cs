using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using IGDataGrid.Resources;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Tools;
using Infragistics.Windows.DataPresenter.Events;

namespace IGDataGrid.Samples.Performance
{
    public partial class ExternalSorting : SampleContainer
    {
        #region Private Members
        private IEnumerable<OrderViewModel> _data = null;
        private DateTime _startExtSorting;
        private DateTime _startCommonSorting;
        private string _PreviousSortDirection;
        private string _PreviousSortedField;
        #endregion
        public ExternalSorting()
        {
            InitializeComponent();
        }

        private void GenerateData_Click(object sender, RoutedEventArgs e)
        {
            BackgroundWorker workerThread = new BackgroundWorker();
            workerThread.DoWork += new DoWorkEventHandler(workerThread_DoWork);
            workerThread.RunWorkerCompleted += new RunWorkerCompletedEventHandler(workerThread_RunWorkerCompleted);
            workerThread.RunWorkerAsync((int)this.slDataItems.Value);
        }

        private void workerThread_DoWork(object sender, DoWorkEventArgs e)
        {
            // obtaining an IEnumerable with "OrderViewModel" in it
            var dataSource = (from d in Enumerable.Range(0, (int)e.Argument)
                              select new OrderViewModel
                              {
                                  OrderNumber = string.Format("{0:185000#}", d),
                                  SalesPrice = RandomizeUtility.GenerateRandomNumber(10, 900),
                                  Quantity = RandomizeUtility.GenerateRandomNumber(10, 20)
                              });

            e.Result = new ObservableCollection<OrderViewModel>(dataSource);
        }

        private void workerThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ObservableCollection<OrderViewModel> data = e.Result as ObservableCollection<OrderViewModel>;

            ListCollectionView listCollectionView = new ListCollectionView(data);

            this.dataGridExt.DataSource = listCollectionView;
            this.dataGridCommon.DataSource = data;

            this.panel.Visibility = Visibility.Hidden;
        }

        private void dataGridExt_Sorting(object sender, SortingEventArgs e)
        {
            _startExtSorting = DateTime.Now;
        }

        private void dataGridExt_Sorted(object sender, SortedEventArgs e)
        {
            Label_ExternalSortTime.Text = DataGridStrings.XDG_ExternalSorting_SortingTime + " " + (DateTime.Now - _startExtSorting).TotalSeconds;
        }

        private void dataGridCommon_Sorting(object sender, SortingEventArgs e)
        {
            _startCommonSorting = DateTime.Now;

            if (_data == null)
            { 
                _data = this.dataGridCommon.DataSource as IEnumerable<OrderViewModel>;
            }

            if (e.SortDescription.Direction.ToString() != _PreviousSortDirection
                || e.SortDescription.FieldName != _PreviousSortedField)
            {
                ListSortDirection sortDirection = e.SortDescription.Direction;

                switch (sortDirection)
                {
                    case ListSortDirection.Ascending:
                        this.SortAsc(e.SortDescription.FieldName);
                        break;
                    case ListSortDirection.Descending:
                        this.SortDesc(e.SortDescription.FieldName);
                        break;
                }
            }

            _PreviousSortDirection = e.SortDescription.Direction.ToString();
            _PreviousSortedField = e.SortDescription.FieldName;
        }

        private void dataGridCommon_Sorted(object sender, SortedEventArgs e)
        {
            Label_CommonSortTime.Text = DataGridStrings.XDG_ExternalSorting_SortingTime + " " + (DateTime.Now - _startCommonSorting).TotalSeconds;
        }

        private void SortAsc(string fieldName)
        {
            switch (fieldName)
            {
                case "OrderNumber":
                    this.dataGridCommon.DataSource = _data.OrderBy(order => order.OrderNumber);
                    break;
                case "SalesPrice":
                    this.dataGridCommon.DataSource = _data.OrderBy(order => order.SalesPrice);
                    break;
                case "Quantity":
                    this.dataGridCommon.DataSource = _data.OrderBy(order => order.Quantity);
                    break;
                case "Total":
                    this.dataGridCommon.DataSource = _data.OrderBy(order => order.Total);
                    break;
            }
        }

        private void SortDesc(string fieldName)
        {
            switch (fieldName)
            {
                case "OrderNumber":
                    this.dataGridCommon.DataSource = _data.OrderByDescending(order => order.OrderNumber);
                    break;
                case "SalesPrice":
                    this.dataGridCommon.DataSource = _data.OrderByDescending(order => order.SalesPrice);
                    break;
                case "Quantity":
                    this.dataGridCommon.DataSource = _data.OrderByDescending(order => order.Quantity);
                    break;
                case "Total":
                    this.dataGridCommon.DataSource = _data.OrderByDescending(order => order.Total);
                    break;
            }
        }         
    }
}
