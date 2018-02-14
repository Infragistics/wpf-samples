using IgOutlook.Infrastructure.Dialogs;
using IgOutlook.Infrastructure.Prism;
using IgOutlook.Modules.Calendar;
using IgOutlook.Modules.Contacts;
using IgOutlook.Modules.Mail;
using Infragistics.Windows.OutlookBar;
using Infragistics.Windows.Ribbon;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;
using Microsoft.Practices.Unity;
using System.Windows;


namespace IgOutlook
{
    public class Bootstrapper : UnityBootstrapper
    {
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            Container.RegisterType<IDialogService, DialogService>();
            Container.RegisterType<IMessageBoxService, MessageBoxService>();
        }

        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<Shell>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            Application.Current.MainWindow = (Window)Shell;
            Application.Current.MainWindow.ContentRendered += (s, a) => Application.Current.MainWindow.WindowState = WindowState.Maximized;
            Application.Current.MainWindow.Show();
        }


        protected override IModuleCatalog CreateModuleCatalog()
        {
            var catalog = new ModuleCatalog();
            catalog.AddModule(typeof(MailModule));
            catalog.AddModule(typeof(ContactsModule));
            catalog.AddModule(typeof(CalendarModule));
            return catalog;
        }

        protected override RegionAdapterMappings ConfigureRegionAdapterMappings()
        {
            RegionAdapterMappings mappings = base.ConfigureRegionAdapterMappings();
            mappings.RegisterMapping(typeof(XamOutlookBar), Container.Resolve<XamOutlookBarRegionAdapter>());
            mappings.RegisterMapping(typeof(XamRibbon), Container.Resolve<XamRibbonRegionAdapter>());
            return mappings;
        }

        protected override IRegionBehaviorFactory ConfigureDefaultRegionBehaviors()
        {
            IRegionBehaviorFactory behaviors = base.ConfigureDefaultRegionBehaviors();
            behaviors.AddIfMissing(XamRibbonRegionBehavior.BehaviorKey, typeof(XamRibbonRegionBehavior));
            behaviors.AddIfMissing(RegionManagerAwareBehavior.BehaviorKey, typeof(RegionManagerAwareBehavior));
            return behaviors;
        }
    }
}
