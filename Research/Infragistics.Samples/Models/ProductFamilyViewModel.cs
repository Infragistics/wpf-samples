using System;
using System.Collections.Generic;
using System.Web;
using Infragistics.Web.SampleBrowser.Core.Framework.Model;
using Infragistics.Web.SampleBrowser.Core.Framework;

namespace Infragistics.Samples.Models
{
	public class ProductFamilyViewModel : IProductFamilyContainer
	{
		public ProductFamily ProductFamily { get; set; }

		public CmsContentCollection ProductControls { get; set; }

		public CmsContentCollection ApplicatonSamples { get; set; }
	}
}