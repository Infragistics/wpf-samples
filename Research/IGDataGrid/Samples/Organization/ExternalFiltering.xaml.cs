using IGDataGrid.DataSources;
using Infragistics.Samples.Framework;
using Infragistics.Windows.DataPresenter;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace IGDataGrid.Samples.Organization
{
    public partial class ExternalFiltering : SampleContainer
    {
        public ExternalFiltering()
        {
            InitializeComponent();

            this.Resources.Add("FilterEvaluationMode", Enum.GetValues(typeof(FilterEvaluationMode)));
            this.Resources.Add("DataSource", new ListCollectionView(new CarsBusinessLogic().Cars));
        }

        private void XamComboEditor_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            string cvRecordCount = string.Empty;

            // Clear the filters in CollectionView
            CollectionView cv = this.XamDataGrid1.DataSource as CollectionView;
            if (cv != null)
            {
                cv.Filter = null;

                // If FilterEvaluationMode is set to Manual you can implement a custom logic like this:
                
                //cv.Filter = o =>
                //{
                //    IGDataGrid.DataSources.Car car = (IGDataGrid.DataSources.Car)o;
                //    return car.BasePrice > 30000;
                //};

                // Note that the CollectionView's Filter property will be used by the xamDataPresenter
                // when FilterEvaluationMode is set to UseCollectionView so any filtering logic defined
                // there will be lost.

                // Obtain the number of filtered records
                cvRecordCount = cv.Count.ToString();
            }

            // Clear the filtered records in xamDataGrid
            this.XamDataGrid1.Records.DataPresenter.FieldLayouts[0].RecordFilters.Clear();

            // Update the counters accordingly
            this.label1.Content = cvRecordCount;
            this.label2.Content = this.XamDataGrid1.RecordManager.GetFilteredInDataRecords().Count().ToString();
        }

        private void XamDataGrid1_RecordFilterChanged(object sender, Infragistics.Windows.DataPresenter.Events.RecordFilterChangedEventArgs e)
        {
            string cvRecordCount = string.Empty;

            // Obtain the number of filtered records
            CollectionView cv = this.XamDataGrid1.DataSource as CollectionView;
            if (cv != null)
            {
                cvRecordCount = cv.Count.ToString();
            }

            // Update the counters accordingly
            this.label1.Content = cvRecordCount;
            this.label2.Content = this.XamDataGrid1.RecordManager.GetFilteredInDataRecords().Count().ToString();
        }
    }
}
