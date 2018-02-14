using System.Collections.Generic;

namespace IgExcel.Services
{
    public interface IStyleService
    {
        List<string> GetAllTableStyles();
        List<string> GetDarkTableStyles();
        List<string> GetLightTableStyles();
        List<string> GetMediumTableStyles();

        List<string> GetAllCellStyles();
        List<string> GetGoodBadAndNeutralCellStyles();
        List<string> GetDataAndModelCellStyles();
        List<string> GetTitlesAndHeadingsCellStyles();
        List<string> GetThemedCellStyles();
        List<string> GetNumberFormatCellStyles();
    }
}
