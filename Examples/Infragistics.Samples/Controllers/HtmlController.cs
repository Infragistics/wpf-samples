using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;

namespace Infragistics.Samples
{
    public class HtmlController : Controller
    {
        //
        // GET: /HtmlSample/

        public ActionResult Index()
        {
			string html_sample_url = this.Request.RequestContext.RouteData.Values["html_sample_url"] as string;

            //Get the html sample stream
			System.IO.FileInfo fi = new System.IO.FileInfo(Server.MapPath(html_sample_url));
            var fileContent = fi.OpenText().ReadToEnd();

            Regex regHead = new Regex(@"<!--#BEGIN SAMPLE HEAD#-->([\s\S.]*?)<!--#END SAMPLE HEAD#-->", RegexOptions.Multiline & RegexOptions.IgnorePatternWhitespace & RegexOptions.CultureInvariant);
            Regex regBody = new Regex(@"<!--#BEGIN SAMPLE BODY#-->([\s\S.]*?)<!--#END SAMPLE BODY#-->", RegexOptions.Multiline & RegexOptions.IgnorePatternWhitespace & RegexOptions.CultureInvariant);

            var headCollection = regHead.Matches(fileContent);
            var bodyCollection = regBody.Matches(fileContent);

            var model = new Models.HtmlSampleModel()
            {
                Head = headCollection[0].Groups[1].Value,
                Body = bodyCollection[0].Groups[1].Value
            };
            return View(model);
        }

    }
}
