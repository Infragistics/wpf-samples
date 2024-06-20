using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Infragistics.Samples
{
	[DefaultProperty("Text")]
	[ToolboxData("<{0}:CssControl runat=server></{0}:CssControl>")]
	public class CssControl : WebControl
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

		[Bindable(true)]
		[Category("Appearance")]
		[DefaultValue("")]
		[Localizable(true)]
		public string Media
		{
			get
			{
				String s = (String)ViewState["Media"];
				return (s ?? String.Empty);
			}

			set
			{
				ViewState["Media"] = value;
			}
		}

		protected override void Render(HtmlTextWriter writer)
		{
			//kill the span tag
			RenderContents(writer);
		}

		protected override void RenderContents(HtmlTextWriter output)
		{
			string tag = "<link rel=\"stylesheet\" href=\"";
			tag += Infragistics.Web.SampleBrowser.Core.Framework.AppSettings.CDN_Path;
			tag += Path;
			tag += "\" type=\"text/css\" media=\"";
			tag += Media;
			tag += "\" />";

			output.Write(tag);

		}
	}
}
