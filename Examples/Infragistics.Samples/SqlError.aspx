<%@ Page Language="C#" Inherits="System.Web.UI.Page" MasterPageFile="~/Global.Master" %>

<script runat="server">
	protected void Page_Load(object sender, System.EventArgs e)
	{
		Response.Clear();
		Response.TrySkipIisCustomErrors = true;
		Response.StatusCode = 500;

		Server.ClearError();
	}
</script>
<asp:Content ID="Content6" ContentPlaceHolderID="head" runat="server">
	<title>
		<%= this.GetGlobalResourceObject("SampleBrowserResources","SQLServer_Title") %>
	</title>
	<style type="text/css">
		#errorMessage > div > *
		{
			color: #656565;
			font-size: 0.9em;
			line-height: 1.429em;
		}		
	</style>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="content" runat="server">
	<div class="container clearfix content_area" style="padding-bottom: 0px;">
		<div id="errorMessage">
			<h1>
				<%= this.GetGlobalResourceObject("SampleBrowserResources", "ErrorPage_Header")%></h1>
			<p style="padding: 10px 0px 20px 0px; border-bottom: 1px solid #656565;">
				<%= this.GetGlobalResourceObject("SampleBrowserResources", "ErrorPage_SubHeader")%></p>
			<div style="margin: 10px 0px 50px 35px;">
				<p>
					<%= this.GetGlobalResourceObject("SampleBrowserResources","SQLServer_Paragraph1") %></p>
				<ol>
					<li>
						<%= this.GetGlobalResourceObject("SampleBrowserResources","SQLServer_Paragraph3") %></li>
					<li>
						<%= this.GetGlobalResourceObject("SampleBrowserResources","SQLServer_Paragraph4") %></li>
					<li>
						<%= this.GetGlobalResourceObject("SampleBrowserResources","SQLServer_Paragraph5") %></li>
				</ol>
				<br />
				<h3>
					<%= this.GetGlobalResourceObject("SampleBrowserResources","SQLServer_Paragraph6") %></h3>
				<ol>
					<li>
						<%= this.GetGlobalResourceObject("SampleBrowserResources","SQLServer_Paragraph8") %></li>
					<li>
						<%= this.GetGlobalResourceObject("SampleBrowserResources","SQLServer_Paragraph9") %></li>
					<li>
						<%= this.GetGlobalResourceObject("SampleBrowserResources","SQLServer_Paragraph13") %></li>
				</ol>
			</div>
		</div>
	</div>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="beforeClosingBody" runat="server">
</asp:Content>
