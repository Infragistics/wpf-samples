using Infragistics.Windows.DataPresenter;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.ComponentModel;
using Infragistics.Themes;

namespace ConditionalFormatting_DataValueChanged.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetTheme(true);
        }

        private void DarkMode_Clicked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox button)
                SetTheme(button.IsChecked.Value);
        }

        private void IsGrouped_Click(object sender, RoutedEventArgs e)
        {
            var button = (CheckBox)sender;

            var fieldLayout = _grid.FieldLayouts[0];

            if (button.IsChecked.Value)
            {
                fieldLayout.SortedFields.Add(new FieldSortDescription() { FieldName = "Category", Direction = ListSortDirection.Descending, IsGroupBy = true });
                fieldLayout.SortedFields.Add(new FieldSortDescription() { FieldName = "Type", Direction = ListSortDirection.Descending, IsGroupBy = true });
                fieldLayout.SortedFields.Add(new FieldSortDescription() { FieldName = "Contract", Direction = ListSortDirection.Descending, IsGroupBy = true });
                _grid.Records.ExpandAll(true);
            }
            else
            {
                fieldLayout.SortedFields.Clear();
            }
        }

        private void DataValueChanged(object sender, Infragistics.Windows.DataPresenter.Events.DataValueChangedEventArgs e)
        {
            // If we don't have any ValueHistory, clear the Foreground of the CellValuePresenter.
            if (e.ValueHistory == null)
                e.CellValuePresenter.ClearValue(ForegroundProperty);
            else
            {
                // If we have more than 1 history value for this CellValuePresenter, compare the current value
                // to the previous value, and if:
                //		- the current value is less than the previous value make the foreground Red
                //		- the current value is greater than the previous value make the foreground Green
                //		- the current value is the same as the previous value make the foreground Black
                if (e.CellValuePresenter != null && e.ValueHistory.Count > 1)
                {
                    DataValueInfo latestDataValueInfo = e.ValueHistory[0];
                    DataValueInfo previousDataValueInfo = e.ValueHistory[1];

                    if ((double)latestDataValueInfo.Value < (double)previousDataValueInfo.Value)
                    {
                        e.CellValuePresenter.Foreground = this.Resources["RedBrush"] as Brush;
                    }
                    else if ((double)latestDataValueInfo.Value > (double)previousDataValueInfo.Value)
                    {
                        e.CellValuePresenter.Foreground = this.Resources["GreenBrush"] as Brush;
                    }
                    else
                        e.CellValuePresenter.ClearValue(ForegroundProperty);
                }
            }
        }

        void SetTheme(bool useDarkMode)
        {
            ThemeBase theme = (ThemeBase)Application.Current.Resources["Office2013"];
            SolidColorBrush background = (SolidColorBrush)Application.Current.Resources["LightBackground"];

            if (useDarkMode)
            {
                theme = (ThemeBase)Application.Current.Resources["RoyalDark"];
                background = (SolidColorBrush)Application.Current.Resources["DarkBackground"];
            }

            Background = background;
            ThemeManager.SetTheme(this, theme);
        }
    }
}
