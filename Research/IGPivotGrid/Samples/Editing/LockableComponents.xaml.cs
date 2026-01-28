using System;
using System.Windows;
using System.Windows.Controls;
using IGPivotGrid.Samples.Controls;
using Infragistics.Olap;
using Infragistics.Samples.Framework;

namespace IGPivotGrid.Samples.Editing
{
    public partial class LockableComponents : SampleContainer
    {
        public LockableComponents()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            SampleFlatDataSourceForLiveUpdate dataSource = this.Resources["FlatDataSource"] as SampleFlatDataSourceForLiveUpdate;
            if (dataSource != null)
                dataSource.LoadSchemaAsync();
        }

        private void Feature_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            CheckBox cb = sender as CheckBox;
            if (element == null || cb == null)
                return;

            switch (element.Name)
            {
                case "RowEditing":
                    ((DataSourceBase)pivotGrid.DataSource).AreaFieldSettings.AllowRowsEditing = (Boolean)cb.IsChecked;
                    break;
                case "ColumnsEditing":
                    ((DataSourceBase)pivotGrid.DataSource).AreaFieldSettings.AllowColumnsEditing = (Boolean)cb.IsChecked;
                    break;
                case "FiltersEditing":
                    ((DataSourceBase)pivotGrid.DataSource).AreaFieldSettings.AllowFiltersEditing = (Boolean)cb.IsChecked;
                    break;
                case "MeasuresEditing":
                    ((DataSourceBase)pivotGrid.DataSource).AreaFieldSettings.AllowMeasuresEditing = (Boolean)cb.IsChecked;
                    break;
            }
        }
    }
}
