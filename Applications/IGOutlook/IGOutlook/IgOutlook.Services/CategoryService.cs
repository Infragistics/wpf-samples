using IgOutlook.Business.Calendar;
using IgOutlook.Services.Resources;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace IgOutlook.Services
{
    public class CategoryService : ICategoryService
    {
        public ObservableCollection<ActivityCategory> GetCategories()
        {
            return Categories;
        }

        #region Data

        ObservableCollection<ActivityCategory> Categories = new ObservableCollection<ActivityCategory>()
        {
            new ActivityCategory { Color = Colors.Red, Description = ResourceStrings.RedCatDesc, CategoryName = ResourceStrings.RedCatName },
            new ActivityCategory { Color = Colors.Blue, Description = ResourceStrings.BlueCatDesc, CategoryName = ResourceStrings.BlueCatName },
            new ActivityCategory { Color = Colors.Green, Description = ResourceStrings.GreenCatDesc, CategoryName = ResourceStrings.GreenCatName },
        };

        #endregion //Data
    }
}
