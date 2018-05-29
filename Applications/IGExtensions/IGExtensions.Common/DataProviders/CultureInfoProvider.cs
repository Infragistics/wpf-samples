using System.Globalization;
using System.Linq;
#if WPF 
using System.Collections.Specialized;
using System;
#endif

namespace IGExtensions.Common.DataProviders
{
    /// <summary>
    /// Represents a data provider for CultureInfo
    /// </summary>
    public static class CultureInfoProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static CultureInfo GetUiCultureInfo()
        {
            CultureInfo uiCulture = CultureInfo.CurrentUICulture;
#if WPF 
            // Let's get the startup parameters to see if we need to "force" a locale.  We can look at the query parameters"
            // for click-once deployments, and command line parameters for a normal execution.  
            // Note: We skip the first command line parameter as it is always the path to the running exe.
            NameValueCollection cmdlineParams = HttpQueryProvider.QueryStringParameters() ??
                              System.Environment.GetCommandLineArgs().Skip(1).ToArray().ToNameValueCollection();
           
            string lc = cmdlineParams["lc"];
            
            if (lc != null)
            {
                uiCulture = new CultureInfo(lc);
                System.Threading.Thread.CurrentThread.CurrentUICulture = uiCulture;
            }
#endif
            return uiCulture;
        }
    }
}