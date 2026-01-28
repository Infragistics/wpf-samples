using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Infragistics.Samples.Shared.Models.General
{
    public class DataItem : ObservableModel
    {
        public DataItem()
        {
        }

        private string id;
        public string Id
        {
            get
            {
                return this.id;
            }
            set
            {
                if (this.id != value)
                {
                    this.id = value;
                    this.OnPropertyChanged("Id");
                }
            }
        }

        private string name;
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

        private string value;
        public string Value
        {
            get
            {
                return this.value;
            }
            set
            {
                if (this.value != value)
                {
                    this.value = value;
                    this.OnPropertyChanged("Value");
                }
            }
        }

        private string imagePath;
        public string ImagePath
        {
            get
            {
                return this.imagePath;
            }
            set
            {
                if (this.imagePath != value)
                {
                    this.imagePath = value;
                    this.OnPropertyChanged("ImageUrl");
                }
            }
        }

        private string url;
        public string Url
        {
            get
            {
                return this.url;
            }
            set
            {
                if (this.url != value)
                {
                    this.url = value;
                    this.OnPropertyChanged("Url");
                }
            }
        }

        private bool isCheckable = true;
        public bool IsCheckable
        {
            get
            {
                return this.isCheckable;
            }
            set
            {
                if (this.isCheckable != value)
                {
                    this.isCheckable = value;
                    this.OnPropertyChanged("IsCheckable");
                }
            }
        }

        private DataItemCollection children;
        public DataItemCollection Children
        {
            get
            {
                return this.children;
            }
            set
            {
                if (this.children != value)
                {
                    this.children = value;
                    this.OnPropertyChanged("Children");
                }
            }
        }
    }

    public class DataItemCollection : ObservableCollection<DataItem>
    {
        public DataItemCollection()
        {
        }

        public DataItemCollection(IEnumerable<DataItem> source)
        {
            this.Append(source);
        }

        public void Append(IEnumerable<DataItem> source)
        {
            foreach (DataItem item in source)
            {
                this.Add(item);
            }
        }
    }
}