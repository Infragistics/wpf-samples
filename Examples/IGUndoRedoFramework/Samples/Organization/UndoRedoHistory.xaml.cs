﻿using System.Threading;
using System.Windows;
using System.Windows.Threading;
using IGUndoRedoFramework.Resources;
using IGUndoRedoFramework.ViewModel;
using Infragistics.Samples.Framework;
using Infragistics.Undo;

namespace IGUndoRedoFramework.Samples.Organization
{
    public partial class UndoRedoHistory : SampleContainer
    {
        public UndoRedoHistory()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        private void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = new CustomersViewModel();
        }

        private UndoManager UndoManager
        {
            get
            {
                return ((CustomersViewModel)this.DataContext).UndoManager;
            }
        }

        private void DataGrid_RecordAdding(object sender, Infragistics.Windows.DataPresenter.Events.RecordAddingEventArgs e)
        {
            var transaction = this.UndoManager.StartTransaction(UndoRedoStrings.UndoRedo_AddRow, UndoRedoStrings.UndoRedo_AddRow_DetailedDesc);

            new DispatcherSynchronizationContext().Post(new SendOrPostCallback((o) =>
            {
                if (!transaction.IsClosed)
                    transaction.Commit();
            }), transaction);
        }

        private void DataGrid_RecordsDeleted(object sender, Infragistics.Windows.DataPresenter.Events.RecordsDeletedEventArgs e)
        {
            var transaction = this.UndoManager.StartTransaction(UndoRedoStrings.UndoRedo_RemoveRows, UndoRedoStrings.UndoRedo_RemoveRows_DetailedDesc);

            new DispatcherSynchronizationContext().Post(new SendOrPostCallback((o) =>
            {
                if (!transaction.IsClosed)
                    transaction.Commit();
            }), transaction);
        }
    }
}