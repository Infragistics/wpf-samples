
namespace IgExcel.Business
{
    public class DocumentTemplate : BusinessBase
    {
        private string fileName;

        public string FileName
        {
            get { return fileName; }
            set { fileName = value; NotifyPropertyChanged(); }
        }

        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; NotifyPropertyChanged(); }
        }
        private string smallImagePath;

        public string SmallImagePath
        {
            get { return smallImagePath; }
            set { smallImagePath = value; NotifyPropertyChanged(); }
        }
        private string largeImagePath;

        public string LargeImagePath
        {
            get { return largeImagePath; }
            set { largeImagePath = value; NotifyPropertyChanged(); }
        }
        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; NotifyPropertyChanged(); }
        }
    }
}
