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
		<%= this.GetGlobalResourceObject("SampleBrowserResources", "ErrorPage_Title")%>
	</title>
	<style type="text/css">
		#errorMessage > span, #errorMessage > ul
		{
			color: #656565;
			font-size: 0.857em;
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
			<span>
				<%= this.GetGlobalResourceObject("SampleBrowserResources", "ErrorPage_Options_Message")%></span>
			<ul style="margin: 10px 0px 50px 15px;">
				<li>
					<%= this.GetGlobalResourceObject("SampleBrowserResources", "ErrorPage_Options1")%></li>
				<li>
					<%= this.GetGlobalResourceObject("SampleBrowserResources", "ErrorPage_Options2")%></li>
			</ul>
		</div>
	</div>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="beforeClosingBody" runat="server">
</asp:Content>
