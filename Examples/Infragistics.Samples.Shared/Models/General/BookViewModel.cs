using System.Collections.Generic;

namespace Infragistics.Samples.Shared.Models.General
{
    public class BookViewModel : ObservableModel
    {
        public BookViewModel()
        {
        }

        private string name = string.Empty;
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (this.name != value)
                {
                    this.name = value;
                    this.OnPropertyChanged("Name");
                }
            }
        }

        private IEnumerable<ChapterViewModel> chapters;
        public IEnumerable<ChapterViewModel> Chapters
        {
            get
            {
                return this.chapters;
            }
            set
            {
                if (this.chapters != value)
                {
                    this.chapters = value;
                    this.OnPropertyChanged("Chapters");
                }
            }
        }
    }
}