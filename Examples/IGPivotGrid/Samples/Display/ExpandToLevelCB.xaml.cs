using IGPivotGrid.Resources;
using Infragistics.Olap;
using Infragistics.Samples.Framework;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace IGPivotGrid.Samples.Display
{
    public partial class ExpandToLevelCB : SampleContainer
    {
        private DataSourceBase dataSource;

        public ExpandToLevelCB()
        {
            InitializeComponent();

            pivotGrid.DataSource.PropertyChanged += DataSource_PropertyChanged;
            dataSource = pivotGrid.DataSource as DataSourceBase;
        }

        private void ExpandToLevel(object sender, RoutedEventArgs args)
        {
            IFilterViewModel fvm = FindDateViewModel(dataSource);

            if (fvm != null)
            {
                dataSource.ExpandToLevelAsync(fvm, levelDepth.SelectedIndex).ContinueWith(
                    (t) => dataSource.RefreshGrid(), System.Threading.Tasks.TaskContinuationOptions.ExecuteSynchronously);
            }
            else
            {
                MessageBox.Show(PivotGridStrings.XPG_DateHierarchyMissing);
            }
        }


        private void CollapseToLevel(object sender, RoutedEventArgs args)
        {
            IFilterViewModel fvm = FindDateViewModel(dataSource);

            if (fvm != null)
            {
                dataSource.CollapseToLevelAsync(fvm, fvm.Hierarchy.Levels[levelDepth.SelectedIndex]).ContinueWith(
                    (t) => dataSource.RefreshGrid(), System.Threading.Tasks.TaskContinuationOptions.ExecuteSynchronously);
            }
            else
            {
                MessageBox.Show(PivotGridStrings.XPG_DateHierarchyMissing);
            }
        }

        private IFilterViewModel FindDateViewModel(DataSourceBase dataSource)
        {
            var fvm = dataSource.Rows.Concat(dataSource.Columns).FirstOrDefault(
                aivm => aivm is IFilterViewModel && ((IFilterViewModel)aivm).Hierarchy.UniqueName == "[Date].[Date]") as IFilterViewModel;
            return fvm;
        }

        private void DataSource_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Processing")
                isBusyIndicator.Visibility = pivotGrid.DataSource.IsBusy ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
