using IgExcel.Business;
using System.Collections.Generic;

namespace IgExcel.Services
{
    public interface IDocumentTemplateService
    {
        List<DocumentTemplate> GetTemplates();
    }
}
