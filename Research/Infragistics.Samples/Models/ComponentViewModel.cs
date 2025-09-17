using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Infragistics.Web.SampleBrowser.Core.Framework.Model;
using Infragistics.Web.SampleBrowser.Core.Framework;

namespace Infragistics.Samples.Models
{
	public class ComponentViewModel : ProductFamilyViewModel, IComponentContainer
	{
		public Component Component { get; set; }

		public string ComponentKeyFeatures { get; set; }
	}
}