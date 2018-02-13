using IgExcel.Dialogs;
using IgExcel.Infrastructure.Dialogs;
using IgExcel.Services;
using IgExcel.Views;
using Microsoft.Practices.Unity;
using Prism.Regions;
using Prism.Unity;
using System.Windows;

namespace IgExcel
{
    public class Bootstrapper : UnityBootstrapper
    {
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            Container.RegisterType<IDialogService, DialogService>();
            Container.RegisterType<IMessageBoxService, MessageBoxService>();
            Container.RegisterType<IRecentFilesService, RecentFilesService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IFontService, FontService>();
            Container.RegisterType<IDocumentTemplateService, DocumentTemplateService>();
            Container.RegisterType<IFormatsService, FormatsService>();
            Container.RegisterType<IStyleService, StyleService>();
        }

        protected override System.Windows.DependencyObject CreateShell()
        {
            return Container.Resolve<Shell>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            IRegionManager rm = Container.Resolve<RegionManager>();
            rm.RegisterViewWithRegion("OpenFileRegion", typeof(OpenFileView));
            rm.RegisterViewWithRegion("SaveFileRegion", typeof(SaveFileView));
            rm.RegisterViewWithRegion("NewDocumentRegion", typeof(NewDocumentView));

            Container.RegisterTypeForNavigation<RecentFoldersView>("RecentFoldersView");
            Container.RegisterTypeForNavigation<RecentFilesView>("RecentFilesView");

            //Dialogs 
            Container.RegisterTypeForNavigation<FormatCellsDialogView>("FormatCellsDialogView");
            Container.RegisterTypeForNavigation<FormatAsTableDialogView>("FormatAsTableDialogView");
            Container.RegisterTypeForNavigation<DimensionDialogView>("DimensionDialogView");
            Container.RegisterTypeForNavigation<ZoomDialogView>("ZoomDialogView");
            Container.RegisterTypeForNavigation<PasswordDialogView>("PasswordDialogView");
            Container.RegisterTypeForNavigation<DataValidationDialogView>("DataValidationDialogView");

            Application.Current.MainWindow = (Window)Shell;
            Application.Current.MainWindow.ContentRendered += (s, a) => Application.Current.MainWindow.WindowState = WindowState.Maximized;
            Application.Current.MainWindow.Show();
        }
    }
}
