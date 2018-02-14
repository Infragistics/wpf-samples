using IgWord.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IgWord.Services
{
    public interface IDocumentTemplateService
    {
        List<DocumentTemplate> GetTemplates();
    }
}
