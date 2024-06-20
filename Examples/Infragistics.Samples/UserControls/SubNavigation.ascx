<%@ Control Language="C#" Inherits="System.Web.UI.UserControl" %>
<section class="clearfix">
<div id="header">
	<div id="sub_nav_bar">
		<nav class="container clearfix" id="sub_nav_bar_container">
			<div class="left" id="label">
				<img alt="Infragistics Silverlight" src="/samplesbrowser/<%= ConfigurationManager.AppSettings["ProductLogo"] %>"
					class="prodLogo"> 
                    <object data="data:application/x-silverlight-2," type="application/x-silverlight-2" style="visibility:visible; width:0; height:0;" width="0" height="0">
		  <param name="source" value="/samplesbrowser/ClientBin/Preloader.xap"/>		 
		  <param name="background" value="white" />
		  <param name="minRuntimeVersion" value="5.0.61118.0" />
		  <param name="autoUpgrade" value="true" />
		  <a href="http://go.microsoft.com/fwlink/?LinkID=149156&v=5.0.61118.0" style="text-decoration:none">
 			  <img src="http://go.microsoft.com/fwlink/?LinkId=161376" alt="Get Microsoft Silverlight" style="border-style:none"/>
		  </a>
	    </object>
			</div>
			<div class="right" id="menu">
				<ul id="subNavigation">
					<li class="selected last" style="border-bottom: 2px solid #00AEEF;"><a title="<%= this.GetGlobalResourceObject("SampleBrowserResources", "Features_and_samples_String")%>"><%= this.GetGlobalResourceObject("SampleBrowserResources", "Features_and_samples_String")%></a>
					</li>
				</ul>
			</div>
		</nav>
	</div>
</div>
</section>
