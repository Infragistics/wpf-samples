using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Infragistics.Samples
{
	[DefaultProperty("Text")]
	[ToolboxData("<{0}:JsControl runat=server></{0}:JsControl>")]
	public class JsControl : WebControl
	{
		[Bindable(true)]
		[Category("Appearance")]
		[DefaultValue("")]
		[Localizable(true)]
		public string Path
		{
			get
			{
				String s = (String)ViewState["Path"];
				return (s ?? String.Empty);
			}

			set
			{
				ViewState["Path"] = value;
			}
		}
		protected override void Render(HtmlTextWriter writer)
		{
			//kill the span tag
			RenderContents(writer);
		}

		protected override void RenderContents(HtmlTextWriter output)
		{


			//<script type="text/javascript" src="/js/html5.js"></script>

			string tag = "<script type=\"text/javascript\" src=\"";
			tag += Infragistics.Web.SampleBrowser.Core.Framework.AppSettings.CDN_Path;
			tag += Path;
			tag += "\"></script>";

			output.Write(tag);

		}
	}
}