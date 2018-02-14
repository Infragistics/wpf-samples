using IgOutlook.Business.Calendar;
using System.Collections.ObjectModel;

namespace IgOutlook.Services
{
    public interface ICategoryService
    {
        ObservableCollection<ActivityCategory> GetCategories();
    }
}
