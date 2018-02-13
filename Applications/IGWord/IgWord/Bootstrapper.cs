using IgWord.Dialogs;
using IgWord.Infrastructure.Dialogs;
using IgWord.Services;
using IgWord.Views;
using Microsoft.Practices.Unity;
using Prism.Regions;
using Prism.Unity;
using System.Windows;

namespace IgWord
{
    public class Bootstrapper : UnityBootstrapper
    {
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            Container.RegisterType<IDialogService, DialogService>();
            Container.RegisterType<IMessageBoxService, MessageBoxService>();
            Container.RegisterType<IFontService, FontService>();
            Container.RegisterType<IRecentFilesService, RecentFilesService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IDocumentTemplateService, DocumentTemplateService>();
        }

        protected override DependencyObject CreateShell()
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
            rm.RegisterViewWithRegion("InfoRegion", typeof(InfoView));

            Container.RegisterTypeForNavigation<RecentFoldersView>("RecentFoldersView");
            Container.RegisterTypeForNavigation<RecentFilesView>("RecentFilesView");

            //Dialogs
            Container.RegisterTypeForNavigation<FontDialogView>("FontDialogView");
            Container.RegisterTypeForNavigation<ParagraphDialogView>("ParagraphDialogView");
            Container.RegisterTypeForNavigation<FindReplaceDialogView>("FindReplaceDialogView");
            Container.RegisterTypeForNavigation<HyperlinkDialogView>("HyperlinkDialogView");
            Container.RegisterTypeForNavigation<InsertTableDialogView>("InsertTableDialogView");

            Application.Current.MainWindow = (Window)Shell;
            Application.Current.MainWindow.ContentRendered += (s, a) => Application.Current.MainWindow.WindowState = WindowState.Maximized;
            Application.Current.MainWindow.Show();
        }


    }
}
