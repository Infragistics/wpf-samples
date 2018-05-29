using System.Collections.Specialized;
using System.Deployment.Application;
using System.Web;
using System.Linq;

namespace IGExtensions.Common.DataProviders
{
    /// <summary>
    /// Represents a provider Http Queries
    /// </summary>
    public class HttpQueryProvider //HttpUtilities
    {
        /// <summary>
        /// Retrieve the query string parameters (useful for click-once deployments).
        /// </summary>
        /// <returns></returns>
        public static NameValueCollection QueryStringParameters()
        {
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                if (ApplicationDeployment.CurrentDeployment.ActivationUri != null)
                {
                    var queryString = ApplicationDeployment.CurrentDeployment.ActivationUri.Query;
                    return HttpUtility.ParseQueryString(queryString);
                }
            }

            return null;
        }
    }
    public class HttpUtilities : HttpQueryProvider
    {
        
    }
}