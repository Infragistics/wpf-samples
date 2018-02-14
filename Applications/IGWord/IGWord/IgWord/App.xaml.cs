using Microsoft.Practices.Unity;
using Prism.Mvvm;
using System;
using System.Globalization;
using System.Reflection;
using System.Windows;

namespace IgWord
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            //Uncomment this code to test against other cultures.
            //System.Threading.Thread.CurrentThread.CurrentCulture = System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ja-JP");
            //System.Threading.Thread.CurrentThread.CurrentUICulture = System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ja-JP");

            base.OnStartup(e);

            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver((viewType) =>
            {
                var viewName = viewType.FullName;
                var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
                var viewModelName = String.Format(CultureInfo.InvariantCulture, "{0}Model, {1}", viewName, viewAssemblyName);
                return Type.GetType(viewModelName);
            });

            Bootstrapper bs = new Bootstrapper();

            ViewModelLocationProvider.SetDefaultViewModelFactory((type) =>
            {
                return bs.Container.Resolve(type);
            });

            bs.Run();
        }
    }
}
