using IgExcel.Business;
using System.Collections.Generic;

namespace IgExcel.Services
{
    public interface IFormatsService
    {
        List<NumberFormatInfo> GetNumberFormats();
    }
}
