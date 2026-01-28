using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Xml.Serialization;
using IGPivotGrid.Samples.Controls;
using Infragistics.Samples.Framework;
using Microsoft.Win32;
using System.Windows.Controls;
using Infragistics.Olap;
using Infragistics.Olap.FlatData;

namespace IGPivotGrid.Samples.Data
{
    public partial class SaveLoad : SampleContainer
    {
        ObservableCollection<SavedCustomizationsModel> SavedStates;
        FlatDataSource pivotGridDataSource;

        public SaveLoad()
        {
            InitializeComponent();
            SavedStates = new ObservableCollection<SavedCustomizationsModel>();
            this.customizationsList.ItemsSource = SavedStates;

            pivotGridDataSource = this.pivotGrid.DataSource as FlatDataSource;
            pivotGridDataSource.PropertyChanged += DataSource_PropertyChanged;
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            string customizations = pivotGridDataSource.SaveCustomizations();
            SavedCustomizationsModel state = new SavedCustomizationsModel() { Timestamp = DateTime.Now.ToString(), Customizations = customizations };
            SavedStates.Insert(0, state);
            customizationsList.SelectedIndex = 0;
        }

        private void load_Click(object sender, RoutedEventArgs e)
        {
            SavedCustomizationsModel state = this.customizationsList.SelectedItem as SavedCustomizationsModel;
            if (state == null)
                return;
            pivotGridDataSource.LoadCustomizations(state.Customizations, LoadCompletedAction);
        }

        private void SaveXmlFile(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                DefaultExt = "xml",
                Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*",
                FilterIndex = 1
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                using (Stream stream = saveFileDialog.OpenFile())
                {
                    pivotGridDataSource.SaveCustomizations(stream);
                }
            }
        }

        private void LoadXmlFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Multiselect = false,
                Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*",
                FilterIndex = 1
            };

            if (openFileDialog.ShowDialog() != true)
                return;
            try
            {
                using (Stream stream = openFileDialog.OpenFile())
                {
                    pivotGridDataSource.LoadCustomizations(stream, LoadCompletedAction);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("Error: {0}", ex.Message));
            }
        }

        void DataSource_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Processing")
            {
                isBusyIndicator.Visibility = pivotGrid.DataSource.Processing ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        // when passed to one of the LoadCustomizations methods gets invoked when the load is completed
        void LoadCompletedAction(string errorsLog)
        {
            if (!string.IsNullOrEmpty(errorsLog))
            {
                // there were errors - handle this accordingly
            }
        }
    }

    public class SavedCustomizationsModel
    {
        public string Timestamp { get; set; }
        public string Customizations { get; set; }
    }
}
