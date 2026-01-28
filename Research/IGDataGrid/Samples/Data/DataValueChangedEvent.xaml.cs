using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using IGDataGrid.Datasources;
using Infragistics.Samples.Framework;

namespace IGDataGrid.Samples.Data
{
    /// <summary>
    /// Interaction logic for DataValueChangedEvent.xaml
    /// </summary>
    public partial class DataValueChangedEvent : SampleContainer
    {
        private StockPortfolioData _stockPortfolioData = new StockPortfolioData(200);

        public DataValueChangedEvent()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (XamDataGrid1.DataSource == null)
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

        private void XamDataGrid1_DataValueChanged(object sender, Infragistics.Windows.DataPresenter.Events.DataValueChangedEventArgs e)
        {
            // If we don't have any ValueHistory, clear the Background of the CellValuePresenter.
            if (e.ValueHistory == null)
                e.CellValuePresenter.ClearValue(Infragistics.Windows.DataPresenter.CellValuePresenter.BackgroundProperty);
            else
                // If we have more than 1 history value for this CellValuePresenter, compare the current value
                // to the previous value, and if:
                //		- the current value is less than the previous value make the background Red
                //		- the current value is greater than the previous value make the background Green
                //		- the current value is the same as the previous value make the background Cyan
                if (e.CellValuePresenter != null && e.ValueHistory.Count > 1)
                {
                    Infragistics.Windows.DataPresenter.DataValueInfo latestDataValueInfo = e.ValueHistory[0];
                    Infragistics.Windows.DataPresenter.DataValueInfo previousDataValueInfo = e.ValueHistory[1];

                    if ((double)latestDataValueInfo.Value < (double)previousDataValueInfo.Value)
                    {
                        e.CellValuePresenter.Background = this.Resources["RedBrush"] as Brush;
                        e.CellValuePresenter.Foreground = Brushes.White;
                    }
                    else
                        if ((double)latestDataValueInfo.Value > (double)previousDataValueInfo.Value)
                        {
                            e.CellValuePresenter.Background = this.Resources["GreenBrush"] as Brush;
                            e.CellValuePresenter.Foreground = Brushes.White;
                        }
                        else
                            e.CellValuePresenter.Background = Brushes.Cyan;
                }
                else
                    e.CellValuePresenter.ClearValue(Infragistics.Windows.DataPresenter.CellValuePresenter.BackgroundProperty);
        }
    }
}
