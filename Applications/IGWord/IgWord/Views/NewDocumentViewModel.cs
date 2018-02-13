using IgWord.Business;
using IgWord.Infrastructure;
using IgWord.Services;
using Prism.Commands;
using Prism.Events;
using System.Collections.Generic;

namespace IgWord.Views
{
    public class NewDocumentViewModel : ViewModelBase
    {
        private IEventAggregator eventAggragator;
        private List<DocumentTemplate> documentTemplates;

        public List<DocumentTemplate> DocumentTemplates
        {
            get { return documentTemplates; }
            set { documentTemplates = value; NotifyPropertyChanged(); }
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
