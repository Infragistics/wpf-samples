using System.Collections.Generic;
using Infragistics.Samples.Shared.Resources;

namespace Infragistics.Samples.Shared.Models
{
    public class EnergyProductionModel : ObservableModel
    {
        public EnergyProductionModel()
        {
            this.EnergySampleData = new EnergyProductionDataSample();
            this.EnergyHistoricData = new EnergyProductionDataHistory();

            this.StringEnergyNuclear = ModelStrings.XWM_Series_Energy_Nuclear;
            this.StringEnergyCoal = ModelStrings.XWM_Series_Energy_Coal;
            this.StringEnergyOil = ModelStrings.XWM_Series_Energy_Oil;
            this.StringEnergyGas = ModelStrings.XWM_Series_Energy_Gas;
            this.StringEnergyHydro = ModelStrings.XWM_Series_Energy_Hydro;
            this.StringEnergyGeoThermal = ModelStrings.XWM_Series_Energy_GeoThermal;
            this.StringEnergySolar = ModelStrings.XWM_Series_Energy_Solar;
            this.StringEnergyWind = ModelStrings.XWM_Series_Energy_Wind;
            this.StringEnergyHydroVsNuclear = ModelStrings.XWM_Series_Energy_Hydro_vs_Nuclear;
            this.StringEnergyCoalVsOil = ModelStrings.XWM_Series_Energy_Coal_vs_Oil;
            this.StringEnergyRenewable = ModelStrings.XWM_Series_Energy_Renewable;
            this.StringEnergyNonRenewable = ModelStrings.XWM_Series_Energy_NonRenewable;

            this.StringEnergyTotal = ModelStrings.XWM_Series_Energy_TotalEnergy;
            this.StringEnergyProduction = ModelStrings.XWM_Series_Energy_EnergyProduction;
        }
        public EnergyProductionData EnergySampleData { get; set; }
        public EnergyProductionData EnergyHistoricData { get; set; }
        public EnergyProductionData EnergyTop3Countries 
        { 
            get { return GetEnergyProductionData(new List<string> { "USA", "Russia", "China" }); } 
        }
        private EnergyProductionData GetEnergyProductionData(List<string> countries)
        {
            if (countries.Count == 0)
            {
                return EnergySampleData;
            }

            var data = new EnergyProductionData();
            foreach (var item in EnergySampleData)
            {
                if (countries.Contains(item.Id))
                {
                    data.Add(item);
                }
            }
            return data;
        }

        public string StringEnergyNuclear { get; set; }
        public string StringEnergyCoal { get; set; }
        public string StringEnergyOil { get; set; }
        public string StringEnergyGas { get; set; }
        // strings for Renewable Energy Sources
        public string StringEnergyHydro { get; set; }
        public string StringEnergyGeoThermal { get; set; }
        public string StringEnergySolar { get; set; }
        public string StringEnergyWind { get; set; }
        public string StringEnergyCoalVsOil { get; set; }
        public string StringEnergyHydroVsNuclear { get; set; }
        // other strings
        public string StringEnergyTotal { get; set; }
        public string StringEnergyRenewable { get; set; }
        public string StringEnergyNonRenewable { get; set; }
        public string StringEnergyProduction { get; set; }

    }
    
}