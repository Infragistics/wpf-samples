using System.Windows.Controls;
using System.Collections.Generic;
using IGTreemap.Resources;
using Infragistics.Samples.Shared.Models;

namespace IGTreemap.Samples
{
    public partial class DataFiltering : Infragistics.Samples.Framework.SampleContainer
    {
        WorldDataModel wdm = null;

        public DataFiltering()
        {
            InitializeComponent();
            this.ListBoxFilters.SelectionChanged += OnListBoxFiltersSelectionChanged;
            this.SampleLoaded += OnSampleLoaded;
        }

        void OnSampleLoaded(object sender, System.EventArgs e)
        {
            wdm = new WorldDataModel();
            wdm.Filters = new Dictionary<string, string>
            {
                {TreemapStrings.XWM_WorldData_Types_BirthLabel, "BirthRate"},
                {TreemapStrings.XWM_WorldData_Types_ElectricLabel, "ElectricityProduction"},
                {TreemapStrings.XWM_WorldData_Types_InternetLabel, "InternetUsers"},
                {TreemapStrings.XWM_WorldData_Types_MedianAgeLabel, "MedianAge"},
                {TreemapStrings.XWM_WorldData_Types_OilLabel, "OilProduction"},
                {TreemapStrings.XWM_WorldData_Types_PopLabel, "Population"},
                {TreemapStrings.XWM_WorldData_Types_DebtLabel, "PublicDebt"},
                {TreemapStrings.XWM_WorldData_Types_TVLabel, "Televisions"},
                {TreemapStrings.XWM_WorldData_Types_UnEmployLabel, "UnemploymentRate"}
            };
            wdm.PropertyChanged += OnWorldDataModelPropertyChanged;
            ListBoxFilters.ItemsSource = wdm.Filters;
            ListBoxFilters.SelectedIndex = 0;
        }

        void OnWorldDataModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Treemap.ItemsSource = wdm.CountryDataRecords;
        }

        private void OnListBoxFiltersSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Treemap.NodeBinders[0].ValuePath = (string)ListBoxFilters.SelectedValue;
            Treemap.ValueMappers[0].ValuePath = (string)ListBoxFilters.SelectedValue;
        }
    }
}
