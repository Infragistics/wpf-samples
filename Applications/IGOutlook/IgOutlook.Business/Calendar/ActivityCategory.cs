using IgOutlook.Business.Core;
using System.Windows.Media;

namespace IgOutlook.Business.Calendar
{
    public class ActivityCategory : BusinessBase
    {
        private Color _color;
        private object _dataItem;
        private string _description;
        private string _categoryName;

        public Color Color
        {
            get { return _color; }
            set
            {
                if (_color == value)
                    return;

                _color = value;
                NotifyPropertyChanged();
            }
        }
        public object DataItem
        {
            get { return _dataItem; }
            set
            {
                if (_dataItem == value)
                    return;

                _dataItem = value;
                NotifyPropertyChanged();
            }  
        }

        public string Description
        {
            get { return _description; }
            set
            {
                if (_description == value)
                    return;

                _description = value;
                NotifyPropertyChanged();
            }
        }
        public string CategoryName
        {
            get { return _categoryName; }
            set
            {
                if (_categoryName == value)
                    return;

                _categoryName = value;
                NotifyPropertyChanged();
            }
        }
    }
}
