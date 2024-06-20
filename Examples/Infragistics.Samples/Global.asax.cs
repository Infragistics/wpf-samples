using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Infragistics.Web.Core.Framework.Mvc.Routing;
using Infragistics.Web.Core.Framework.Mvc;
using Infragistics.Web.SampleBrowser.Core.Framework;
using Infragistics.Web.Core.Framework.Data.Models;
using System.IO;

namespace Infragistics.Samples
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : System.Web.HttpApplication
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}

		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			routes.IgnoreRoute("{*allaxd}", new { allaxd = @".*\.axd(/.*)?" });
			routes.IgnoreRoute("{resource}.ashx/{*pathInfo}");
																					 
			routes.IgnoreRoute("{folder}/{*pathInfo}", new { folder = "services" });
			routes.IgnoreRoute("{folder}/{*pathInfo}", new { folder = "clientbin" });
			routes.IgnoreRoute("{folder}/{*pathInfo}", new { folder = "samplescommon" });
			routes.IgnoreRoute("{folder}/{*pathInfo}", new { folder = "scripts" });
			routes.IgnoreRoute("{folder}/{*pathInfo}", new { folder = "styles" });


			routes.MapRoute(
				"AppSamples",
				"application-samples",
				new { controller = "Home", action = "AppSamplesLanding" });

			routes.Add(new Route(
				"{component}/sl/{*pathInfo}",
				new RouteValueDictionary(new { platform = "sl" }),
				new RouteHandler()));

			routes.Add(new Route(
				"{component}/{action}/{platform}",
				new RouteValueDictionary(new { platform = "mvc" }),
				new RouteHandler()));

			routes.Add(new Route(
				"{component}",
				new ComponetOverviewRouteHandler("Home", "ComponentOverview")));

			routes.MapRoute(
				"Default", // Route name
				"{controller}/{action}/{id}", // URL with parameters
				new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
			);
		}

		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			ViewEngines.Engines.Clear();
			ViewEngines.Engines.Add(new Infragistics.Web.Core.Framework.Mvc.SamplesViewEngine());

			RegisterGlobalFilters(GlobalFilters.Filters);
			RegisterRoutes(RouteTable.Routes);
		}

		void Application_Error(object sender, EventArgs e)
		{
			if (!Infragistics.Web.SampleBrowser.Core.Framework.AppSettings.ShowErrors)
			{
				Exception objErr = Server.GetLastError().GetBaseException();

				//Catch general SQLException
				if (objErr is System.Data.SqlClient.SqlException)
				{
					this.Server.Transfer("~/sqlerror.aspx");
				}

				if(objErr is HttpException)
				{
					if (((HttpException)objErr).GetHttpCode() ==  404)
					{
						return;
					}
				}

				this.Server.Transfer("~/error.aspx");
			}
		}

		protected void Application_BeginRequest(object sender, EventArgs e)
		{
			try
			{
				System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo(AppSettings.LanguageId);
			}
			catch (ArgumentException)
			{
				return;
			}

			float v = Infragistics.Web.SampleBrowser.Core.Framework.Helpers.UrlHelper.GetInternetExplorerVersionNumber();

			if (v < 7 && v != -1)
			{
				if (!System.Web.HttpContext.Current.Request.Path.EndsWith(".js") &&
					!System.Web.HttpContext.Current.Request.Path.EndsWith(".css") &&
					!System.Web.HttpContext.Current.Request.Path.EndsWith(".png") &&
					!System.Web.HttpContext.Current.Request.Path.EndsWith(".gif"))
				{
					this.Server.Transfer("~/IE6.aspx");
				}
			}
		}
	}
}
