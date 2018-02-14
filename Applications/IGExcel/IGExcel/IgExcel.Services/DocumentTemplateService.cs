using IgExcel.Business;
using System.Collections.Generic;

namespace IgExcel.Services
{
    public class DocumentTemplateService : IDocumentTemplateService
    {
        public List<DocumentTemplate> GetTemplates()
        {
            return new List<DocumentTemplate> { 
                new DocumentTemplate { Title = ResourceStrings.ResourceStrings.Text_BlankDocument,  FileName = "", Description = "Create a blank Workbook.", SmallImagePath = "/IgExcel;component/Images/Blank_Small.png" },
                new DocumentTemplate { Title = ResourceStrings.ResourceStrings.Text_ProjectBudget,  FileName = "ProjectBudget", Description = "Create Project Budget with this template.", SmallImagePath = "/IgExcel;component/Images/ProjectBudget_Small.png" },
            };
        }
    }
}
