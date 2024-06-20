using System.Web.Mvc;
using Infragistics.Web.SampleBrowser.Core.Framework.Repository;
using Infragistics.Web.SampleBrowser.Core.Framework;
using Infragistics.Samples.Models;

namespace Infragistics.Samples
{
    public class HomeController : Controller
    {
		public ActionResult Index()
		{
			var model = new ProductFamilyViewModel();
			var productFamily = RepositoryFactory.FrameworkRepository.TableOfContent.GetProductFamily(AppSettings.ProductFamilyName);

			model.ProductFamily = productFamily;
			model.ProductControls = RepositoryFactory.FrameworkRepository.GetProductControls(AppSettings.ProductFamilyName);

			return View(model);
		}

		public ActionResult AppSamplesLanding()
		{
			var model = new ProductFamilyViewModel();
			var productFamily = RepositoryFactory.FrameworkRepository.TableOfContent.GetProductFamily(AppSettings.ProductFamilyName);

			model.ProductFamily = productFamily;
			model.ApplicatonSamples = RepositoryFactory.FrameworkRepository.GetAppSamples(AppSettings.ProductFamilyName);

			return View(model);
		}

		public ActionResult ComponentOverview()
		{
			var model = new ComponentViewModel();
			var productFamily = RepositoryFactory.FrameworkRepository.TableOfContent.GetProductFamily(AppSettings.ProductFamilyName);
			var component = productFamily.GetComponent(HttpUtility.GetComponentName());

			model.ProductFamily = productFamily;
			model.Component = component;
			model.ComponentKeyFeatures = RepositoryFactory.FrameworkRepository.GetComponentKeyFeatures(productFamily.RouteName, component.RouteName);

			return View(model);
		}
    }
}
