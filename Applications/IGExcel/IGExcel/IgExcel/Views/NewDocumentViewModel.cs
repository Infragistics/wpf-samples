using IgExcel.Business;
using IgExcel.Infrastructure;
using IgExcel.Services;
using Prism.Commands;
using Prism.Events;
using System.Collections.Generic;

namespace IgExcel.Views
{
    public class NewDocumentViewModel : ViewModelBase
    {
        private IEventAggregator eventAggragator;
        private List<DocumentTemplate> documentTemplates;

        public List<DocumentTemplate> DocumentTemplates
        {
            get { return documentTemplates; }
            set { SetProperty<List<DocumentTemplate>>(ref documentTemplates, value); }
        }

        public DelegateCommand<string> CreateNewDocumentCommand { get; private set; }

        public NewDocumentViewModel(IEventAggregator eventAggragator, IDocumentTemplateService documentTemplateService)
        {
            DocumentTemplates = documentTemplateService.GetTemplates();
            CreateNewDocumentCommand = new DelegateCommand<string>(CreateNewDocument);
            this.eventAggragator = eventAggragator;
        }

        private void CreateNewDocument(string templateName)
        {
            eventAggragator.GetEvent<NewDocumentEvent>().Publish(templateName);
        }
    }
}
