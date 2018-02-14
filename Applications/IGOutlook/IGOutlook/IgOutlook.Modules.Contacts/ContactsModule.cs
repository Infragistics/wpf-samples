using IgOutlook.Infrastructure;
using IgOutlook.Infrastructure.Prism;
using IgOutlook.Modules.Contacts.Menus;
using IgOutlook.Modules.Contacts.Views;
using IgOutlook.Services;
using Prism.Regions;
using Microsoft.Practices.Unity;

namespace IgOutlook.Modules.Contacts
{
    public class ContactsModule : ModuleBase
    {
        public ContactsModule(IUnityContainer container, IRegionManager regionManager)
            : base(container, regionManager)
        {

        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<ContactsView>("ContactsView");
            Container.RegisterTypeForNavigation<ContactDetailsView>("ContactDetailsView");

            Container.RegisterType<IContactService, ContactService>();
        }

        protected override void ResolveOutlookGroup()
        {
            RegionManager.Regions[RegionNames.OutlookBarGroupRegion].Add(Container.Resolve<ContactsGroup>());
        }
    }
}
