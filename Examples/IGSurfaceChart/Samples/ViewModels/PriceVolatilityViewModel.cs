using IGSurfaceChart.Resources;
using IGSurfaceChart.Samples.Models;
using Infragistics.Documents.Excel;
using Infragistics.Samples.Shared.Models;
using System.Collections.Generic; 
using System.Reflection;
using System.Windows;

namespace IGSurfaceChart.Samples.ViewModels
{
    public class PriceVolatilityViewModel : ObservableModel
    {
        public List<FinancialModel> DataCollection { get; set; }
        public PriceVolatilityViewModel()
        {
            DataCollection = GenerateFinancialData();
        }

        internal static List<FinancialModel> GenerateFinancialData()
        {
            var data = new List<FinancialModel>();
            var fileName = "SampleFinancialData.xlsx";
            var resourceName = "IGSurfaceChart.Resources.ExcelFiles." + fileName;

            Workbook dataWorkbook;
            Worksheet sheetOne;

            var assembly = Assembly.GetAssembly(typeof(PriceVolatilityViewModel));
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                dataWorkbook = Workbook.Load(stream);
            }

            // Get data from excel file
            if (dataWorkbook != null)
            {
                sheetOne = dataWorkbook.Worksheets[0];

                foreach (var row in sheetOne.Rows)
                {
                    // Skip the headers row
                    if (row.Index == 0) continue;
                    var fm = new FinancialModel
                    {
                        Days = (double)row.Cells[0].Value,
                        Strike = (double)row.Cells[1].Value,
                        Volatility = (double)(row.Cells[2].Value),
                    };

                    data.Add(fm);
                }
            }
            else
            {
                MessageBox.Show(SurfaceChartStrings.FileLoadingFailure);
            }

            return data;
        }
    }
}
