using IgOutlook.Infrastructure;
using IgOutlook.Infrastructure.Prism;
using IgOutlook.Modules.Mail.Menus;
using IgOutlook.Modules.Mail.Views;
using IgOutlook.Services;
using Prism.Regions;
using Microsoft.Practices.Unity;

namespace IgOutlook.Modules.Mail
{
    public class MailModule : ModuleBase
    {
        public MailModule(IUnityContainer container, IRegionManager regionManager)
            : base (container, regionManager)
        {

        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<MessageListView>("MessageListView");
            Container.RegisterTypeForNavigation<MessageView>("MessageView");
            Container.RegisterTypeForNavigation<MessageReadOnlyView>("MessageReadOnlyView");

            Container.RegisterType<IMailService, MailService>();
            Container.RegisterType<ICategoryService, CategoryService>();
            Container.RegisterType<IContactService, ContactService>();
        }

        protected override void ResolveOutlookGroup()
        {
            RegionManager.Regions[RegionNames.OutlookBarGroupRegion].Add(Container.Resolve<MailGroup>());
        }
    }
}
