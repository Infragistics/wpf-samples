using IgOutlook.Infrastructure;
using IgOutlook.Infrastructure.Prism;
using IgOutlook.Modules.Calendar.Views;
using IgOutlook.Services;
using Infragistics.Controls.Schedules;
using Microsoft.Practices.Unity;
using Prism.Regions;

namespace IgOutlook.Modules.Calendar
{
    public class CalendarModule : ModuleBase
    {
        public CalendarModule(IUnityContainer container, IRegionManager regionManager)
            : base(container, regionManager)
        {

        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<CalendarView>("CalendarView");
            Container.RegisterTypeForNavigation<AppointmentView>("AppointmentView");
            Container.RegisterTypeForNavigation<MeetingView>("MeetingView");

            Container.RegisterType<ICalendarService, CalendarService>();
            Container.RegisterType<ICategoryService, CategoryService>();

            RegisterDataManager();
        }

        private void RegisterDataManager()
        {
            var xamScheduleDataManager = new XamScheduleDataManager { };
            Container.RegisterInstance<XamScheduleDataManager>(xamScheduleDataManager);
        }

        protected override void ResolveOutlookGroup()
        {
            RegionManager.Regions[RegionNames.OutlookBarGroupRegion].Add(Container.Resolve<IgOutlook.Modules.Calendar.Menus.CalendarGroup>());
        }
    }
}
