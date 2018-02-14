using IgWord.Business;
using System.Collections.Generic;

namespace IgWord.Services
{
    public class DocumentTemplateService : IDocumentTemplateService
    {
        public List<DocumentTemplate> GetTemplates()
        {
            return new List<DocumentTemplate> { 
                new DocumentTemplate 
                { 
                    Title = ResourceStrings.ResourceStrings.Text_BlankDocument, 
                    FileName = "", 
                    Description = ResourceStrings.ResourceStrings.Text_BlankDocument_Description, 
                    SmallImagePath = "/IgWord;component/Images/Blank_Small .png" 
                },
                new DocumentTemplate 
                { 
                    Title = ResourceStrings.ResourceStrings.Text_ProjectProposal, 
                    FileName = "ProjectProposal", 
                    Description = ResourceStrings.ResourceStrings.Text_ProjectProposal_Description, 
                    SmallImagePath = "/IgWord;component/Images/ProjectProposal_Small.png" 
                },

                new DocumentTemplate
                {
                    Title = ResourceStrings.ResourceStrings.Text_TrainingCourse, 
                    FileName = "TrainingCourse", 
                    Description = ResourceStrings.ResourceStrings.Text_TrainingCourse_Description, 
                    SmallImagePath = "/IgWord;component/Images/TrainingCourse_Small.png" 
                }
            };
        }
    }
}
