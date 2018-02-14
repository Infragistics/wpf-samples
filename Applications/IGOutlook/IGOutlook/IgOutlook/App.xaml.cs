using System.Windows;

namespace IgOutlook
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            //  Uncomment this code to test against other cultures.
            //System.Threading.Thread.CurrentThread.CurrentCulture =
            //    System.Threading.Thread.CurrentThread.CurrentUICulture =
            //        new System.Globalization.CultureInfo("ja-JP");
            //System.Threading.Thread.CurrentThread.CurrentUICulture =
            //    System.Threading.Thread.CurrentThread.CurrentUICulture =
            //        new System.Globalization.CultureInfo("ja-JP");
            

            base.OnStartup(e);
            Bootstrapper bs = new Bootstrapper();
            bs.Run();
        }
    }
}
