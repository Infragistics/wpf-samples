using System;
using System.Windows;
using System.Windows.Controls;
using IGDataGrid.Datasources;
using Infragistics.Samples.Framework;

namespace IGDataGrid.Samples.Data
{
    /// <summary>
    /// Interaction logic for DataValueHistory.xaml
    /// </summary>
    public partial class DataValueHistory : SampleContainer
    {
        private StockPortfolioData _stockPortfolioData = new StockPortfolioData(200);

        public DataValueHistory()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (this.XamDataGrid1.DataSource == null)
                this.XamDataGrid1.DataSource = this._stockPortfolioData.Data;

            this._stockPortfolioData.StartUpdatingData();
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            this._stockPortfolioData.StopUpdatingData();
        }

        private void chkDataValueChangedNotificationsActive_Checked(object sender, RoutedEventArgs e)
        {
            if (this.XamDataGrid1 == null || this.XamDataGrid1.FieldLayouts.Count == 0)
                return;


            bool isChecked = ((CheckBox)sender).IsChecked == true;

            // The following code will turn DataValueChanged notifications on/off for all StockPrice Fields in our FieldLayout.
            this.XamDataGrid1.FieldLayouts[0].Fields["Stock0Price"].Settings.DataValueChangedNotificationsActive = isChecked;
            this.XamDataGrid1.FieldLayouts[0].Fields["Stock1Price"].Settings.DataValueChangedNotificationsActive = isChecked;
            this.XamDataGrid1.FieldLayouts[0].Fields["Stock2Price"].Settings.DataValueChangedNotificationsActive = isChecked;
            this.XamDataGrid1.FieldLayouts[0].Fields["Stock3Price"].Settings.DataValueChangedNotificationsActive = isChecked;
            this.XamDataGrid1.FieldLayouts[0].Fields["Stock4Price"].Settings.DataValueChangedNotificationsActive = isChecked;
        }

        private void sliderHistoryLimit_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.XamDataGrid1 == null || this.XamDataGrid1.FieldLayouts.Count == 0)
                return;

            int newHistoryLimit = Convert.ToInt32(((Slider)sender).Value);

            this.XamDataGrid1.FieldLayouts[0].Fields["Stock0Price"].Settings.DataValueChangedHistoryLimit = newHistoryLimit;
            this.XamDataGrid1.FieldLayouts[0].Fields["Stock1Price"].Settings.DataValueChangedHistoryLimit = newHistoryLimit;
            this.XamDataGrid1.FieldLayouts[0].Fields["Stock2Price"].Settings.DataValueChangedHistoryLimit = newHistoryLimit;
            this.XamDataGrid1.FieldLayouts[0].Fields["Stock3Price"].Settings.DataValueChangedHistoryLimit = newHistoryLimit;
            this.XamDataGrid1.FieldLayouts[0].Fields["Stock4Price"].Settings.DataValueChangedHistoryLimit = newHistoryLimit;

        }

        private void btnClearHistory_Click(object sender, RoutedEventArgs e)
        {
            this.XamDataGrid1.RecordManager.ResetDataValueHistory(true);
        }

        private void btnClearHistoryForSelectedRecords_Click(object sender, RoutedEventArgs e)
        {
            Infragistics.Windows.DataPresenter.SelectedRecordCollection selectedRecords = this.XamDataGrid1.SelectedItems.Records;
            if (selectedRecords.Count > 0)
            {
                foreach (Infragistics.Windows.DataPresenter.Record record in selectedRecords)
                {
                    if (record is Infragistics.Windows.DataPresenter.DataRecord)
                        ((Infragistics.Windows.DataPresenter.DataRecord)record).ResetDataValueHistory(false);
                }
            }
        }

        private void sliderUpdateInterval_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this._stockPortfolioData.UpdateInterval = ((Slider)sender).Value;
        }
    }
}
