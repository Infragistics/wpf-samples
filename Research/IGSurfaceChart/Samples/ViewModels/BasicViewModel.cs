using IGSurfaceChart.Samples.Models;
using Infragistics.Samples.Shared.Models;
using System.Collections.Generic;

namespace IGSurfaceChart.Samples.ViewModels
{
    public class BasicViewModel : ObservableModel
    {
        public List<DataPoint3D> SimpleDataCollection { get; set; }
        public List<DataPoint3D> Formula1DataCollection { get; set; }      
        public List<DataPoint3D> Formula2DataCollection { get; set; }
        public List<DataPoint3D> Formula3DataCollection { get; set; }
        public List<DataPoint3D> Formula4DataCollection { get; set; }

        public List<DataPoint3D> Cone { get; set; }

        public List<DataPoint3D> ConeFlipped { get; set; }
        public List<DataPoint3D> ConeSlice { get; set; }
        public List<DataPoint3D> ConeSliceFlipped { get; set; }

        public List<DataPoint3D> XSqPlusYSqData { get; set; }
        public List<DataPoint3D> XCuPlusYCuData { get; set; }

        public List<DataPoint3D> YPlusSinXSqPlus3YData { get; set; }

        public List<DataPoint3D> Formula6DataCollection { get; set; }

        public BasicViewModel()
        {
            SimpleDataCollection = Data.GenerateSimpleData();

            Formula1DataCollection = Data.GenerateFormula1Data();
            Formula2DataCollection = Data.GenerateFormula2Data();
            Formula3DataCollection = Data.GenerateFormula3Data();
            Formula4DataCollection = Data.GenerateFormula4Data();
            Formula6DataCollection = Data.GenerateFormula6Data();
            Cone = Data.GenerateCone();
            ConeFlipped = Data.GenerateConeFlipped();
            ConeSlice = Data.GenerateConeSlice();
            ConeSliceFlipped = Data.GenerateConeSliceFlipped();
            XSqPlusYSqData = Data.Generate_XSqPlusYSq();
            XCuPlusYCuData = Data.Generate_XCuPlusYCu();
            YPlusSinXSqPlus3YData = Data.Generate_YPlusSinXSqPlus3Y();
        }
    }
}
